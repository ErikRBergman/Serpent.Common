namespace Serpent.Common.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class CacheObjectDictionary<TKey, TDataModel>
    {
        private readonly ConcurrentDictionary<TKey, CacheObject<TDataModel>> cache = new ConcurrentDictionary<TKey, CacheObject<TDataModel>>();

        private readonly TimeSpan cacheRefreshTime;

        private readonly TimeSpan cacheTimeToLive;

        private readonly TaskCompletionSource<bool> cleanShutdownCompletionSource = new TaskCompletionSource<bool>();

        private readonly Func<TKey, Task<TDataModel>> loadFunc;

        private readonly TimeSpan? periodicCleanInterval;

        private readonly bool useLazyRefresh;

        private Task periodicCleanTask;

        private long totalGets;

        private long cacheMisses;

        public CacheObjectDictionary(
            Func<TKey, Task<TDataModel>> loadFunc,
            TimeSpan cacheTimeToLive,
            TimeSpan cacheRefreshTime,
            TimeSpan? periodicCleanInterval = null,
            bool useLazyRefresh = false)
        {
            this.loadFunc = key =>
            {
                Interlocked.Increment(ref this.cacheMisses);
                return loadFunc(key);
            };
            this.cacheTimeToLive = cacheTimeToLive;
            this.cacheRefreshTime = cacheRefreshTime;
            this.periodicCleanInterval = periodicCleanInterval;
            this.useLazyRefresh = useLazyRefresh;

            if (this.periodicCleanInterval != null)
            {
                this.StartPeriodicClean();
            }
        }

        public long TotalGets => this.totalGets;

        public long CacheMisses => this.cacheMisses;

        public long CacheHits => this.totalGets - this.cacheMisses;

        public Task<TDataModel> GetDataAsync(TKey key)
        {
            var item = this.GetOrAddCacheItem(key);
            return item.GetDataAsync();
        }

        public Task<TDataModel> GetDataUncachedAsync(TKey key)
        {
            return this.loadFunc(key);
        }

        private void CleanUp()
        {
            foreach (var cacheItem in this.cache)
            {
                if (cacheItem.Value.HasExpired)
                {
                    this.cache.TryRemove(cacheItem.Key, out _);
                    cacheItem.Value.ClearIfExpired();
                }
            }
        }

        private CacheObject<TDataModel> GetOrAddCacheItem(TKey key)
        {
            Interlocked.Increment(ref this.totalGets);
            return this.cache.GetOrAdd(key, k => new CacheObject<TDataModel>(() => this.loadFunc(k), this.cacheTimeToLive, this.cacheRefreshTime, this.useLazyRefresh));
        }

        private async Task RunPeriodicCleanAsync()
        {
            do
            {
                var task = await Task.WhenAny(this.cleanShutdownCompletionSource.Task, Task.Delay(this.periodicCleanInterval ?? TimeSpan.FromSeconds(10)));

                if (task == this.cleanShutdownCompletionSource.Task)
                {
                    break;
                }

                this.CleanUp();
            }
            while (!this.cleanShutdownCompletionSource.Task.IsCompleted);
        }

        private void StartPeriodicClean()
        {
            if (this.periodicCleanTask == null || this.periodicCleanTask.IsCompleted)
            {
                this.periodicCleanTask = Task.Run(this.RunPeriodicCleanAsync);
            }
        }
    }
}