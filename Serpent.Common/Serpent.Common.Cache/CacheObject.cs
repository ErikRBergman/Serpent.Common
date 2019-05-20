namespace Serpent.Common.Cache
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides a cached object of type <see cref="T" /> with time to live, refresh time and lazy refresh
    ///     The cache object will clear it's value when methods GetDataAsync or ClearIfExpired are executed if data is older
    ///     than time to live, but not automatically. Call ClearIfExpired periodically if you need to free memory.
    /// </summary>
    /// <typeparam name="T">The data type contained in this cache object</typeparam>
    public class CacheObject<T>
    {
        private readonly Func<DateTime> getCurrentDateAndTimeFunc;

        private readonly Func<Task<T>> loadFunc;

        private readonly object lockObject = new object();

        private readonly TimeSpan refreshTime;

        private readonly TimeSpan timeToLive;

        private readonly bool useLazyRefresh;

        private DateTime expireTime = DateTime.MinValue;

        private Task<T> lazyRefreshDataTask;

        private Task<T> loadingTask;

        private DateTime nextRefreshTime = DateTime.MinValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CacheObject{T}" /> class.
        /// </summary>
        /// <param name="loadFunc">
        ///     The function used to load data
        /// </param>
        /// <param name="timeToLive">
        ///     Data time to live. When the data object is older than this, cache object is cleared when methods ClearIfExpired()
        ///     or GetDataAsync() are executed.
        /// </param>
        /// <param name="refreshTime">
        ///     The refresh time. When the data object is older than this, but not older than time to live, cause the cache object
        ///     to start fetching new data
        /// </param>
        /// <param name="useLazyRefresh">
        ///     Setting this to true, will return the object data even when it's older than "refreshTime" but not older than
        ///     "timeToLive" and start fetching new data in the background
        /// </param>
        /// <param name="getNowFunc">Optional parameter to provide a method that provides current date/time</param>
        public CacheObject(Func<Task<T>> loadFunc, TimeSpan timeToLive, TimeSpan? refreshTime = null, bool useLazyRefresh = false, Func<DateTime> getNowFunc = null)
        {
            this.loadFunc = loadFunc;
            this.timeToLive = timeToLive;
            this.refreshTime = refreshTime ?? timeToLive;
            this.useLazyRefresh = useLazyRefresh;
            this.getCurrentDateAndTimeFunc = getNowFunc ?? (() => DateTime.UtcNow);
        }

        /// <summary>
        ///     Returns true if data has expired (time to live has passed or data has not yet been loaded)
        /// </summary>
        public bool HasExpired
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.HasExpiredUnsafe;
                }
            }
        }

        /// <summary>
        ///     Returns true if data requires a refresh (or a refresh is in progress)
        /// </summary>
        public bool RequiresRefresh
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.RequiresRefreshUnsafe;
                }
            }
        }

        private bool HasDataUnsafe => this.loadingTask != null && this.loadingTask.IsCompleted;

        private bool HasExpiredUnsafe => !this.HasDataUnsafe || this.getCurrentDateAndTimeFunc() >= this.expireTime;

        private bool IsLazyRefreshDataAvailable => this.useLazyRefresh && this.lazyRefreshDataTask != null;

        private bool IsLoadingUnsafe => this.loadingTask != null && this.loadingTask.IsCompleted == false;

        private bool RequiresRefreshUnsafe => this.getCurrentDateAndTimeFunc() >= this.nextRefreshTime;

        /// <summary>
        ///     Clears the current cache object, releasing the reference to it's data.
        ///     This will cause next call to GetDataAsync() to fetch data
        /// </summary>
        public void Clear()
        {
            lock (this.lockObject)
            {
                this.ClearUnsafe();
            }
        }

        /// <summary>
        ///     Clears the cache object if data is older than the configured time to live
        /// </summary>
        public void ClearIfExpired()
        {
            lock (this.lockObject)
            {
                this.ClearIfExpiredUnsafe();
            }
        }

        /// <summary>
        ///     Gets the data from the inner object
        /// </summary>
        /// <returns>A task encapsulating the data</returns>
        public Task<T> GetDataAsync()
        {
            lock (this.lockObject)
            {
                // Are we still loading?
                if (this.IsLoadingUnsafe)
                {
                    if (this.IsLazyRefreshDataAvailable)
                    {
                        return this.lazyRefreshDataTask;
                    }

                    return this.loadingTask;
                }

                this.ClearIfExpiredUnsafe();

                if (!this.HasDataUnsafe || this.RequiresRefreshUnsafe)
                {
                    // initiate loading
                    this.loadingTask = this.LoadDataAsync();

                    if (this.IsLazyRefreshDataAvailable)
                    {
                        return this.lazyRefreshDataTask;
                    }
                }

                return this.loadingTask;
            }
        }

        /// <summary>
        ///     Sets data and timestamp
        /// </summary>
        /// <param name="data">The data to set</param>
        public void SetData(T data)
        {
            lock (this.lockObject)
            {
                this.SetDataUnsafe(data);
            }
        }

        private void ClearIfExpiredUnsafe()
        {
            if (this.HasExpiredUnsafe)
            {
                this.ClearUnsafe();
            }
        }

        private void ClearUnsafe()
        {
            this.nextRefreshTime = DateTime.MinValue;
            this.loadingTask = null;
            this.lazyRefreshDataTask = null;
        }

        private async Task<T> LoadDataAsync()
        {
            var result = await this.loadFunc();

            // Store a new task containing the data - Free any resources from the original loading task 
            this.SetDataUnsafe(result);

            return result;
        }

        private void SetAbsoluteExpirationUnsafe()
        {
            this.expireTime = this.getCurrentDateAndTimeFunc() + this.timeToLive;
        }

        private void SetAbsoluteRefreshTimeUnsafe()
        {
            this.nextRefreshTime = this.getCurrentDateAndTimeFunc() + this.refreshTime;
        }

        private void SetDataUnsafe(T result)
        {
            this.lazyRefreshDataTask = this.loadingTask = Task.FromResult(result);
            this.SetAbsoluteExpirationUnsafe();
            this.SetAbsoluteRefreshTimeUnsafe();
        }
    }
}