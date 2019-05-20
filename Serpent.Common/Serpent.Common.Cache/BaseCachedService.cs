namespace Serpent.Common.Cache
{
    using System;
    using System.Threading.Tasks;

    public abstract class BaseCacheService<TKey, TDataModel>
    {
        protected BaseCacheService()
        {
            this.Cache = new CacheObjectDictionary<TKey, TDataModel>(this.GetDataModelByKeyAsync, TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(10));
        }

        protected BaseCacheService(TimeSpan cacheTimeToLive, TimeSpan cacheRefreshTime, TimeSpan cacheFlushInterval, bool useLazyRefresh)
        {
            this.Cache = new CacheObjectDictionary<TKey, TDataModel>(this.GetDataModelByKeyAsync, cacheTimeToLive, cacheRefreshTime, cacheFlushInterval, useLazyRefresh);
        }

        protected CacheObjectDictionary<TKey, TDataModel> Cache { get; }

        protected Task<TDataModel> GetAsync(TKey key)
        {
            return this.Cache.GetDataAsync(key);
        }

        protected abstract Task<TDataModel> GetDataModelByKeyAsync(TKey key);
    }
}