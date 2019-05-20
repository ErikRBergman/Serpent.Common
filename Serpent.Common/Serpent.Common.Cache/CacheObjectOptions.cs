namespace Serpent.Common.Cache
{
    using System;
    using System.Threading.Tasks;

    public class CacheObjectOptions<T>
    {
        /// <summary>
        ///     Data time to live. When the data object is older than this, cache object is cleared when methods ClearIfExpired()
        ///     or GetDataAsync() are executed.
        /// </summary>
        public TimeSpan TimeToLive { get; set; }

        /// <summary>
        ///     The refresh time. When the data object is older than this, but not older than time to live, cause the cache object
        ///     to start fetching new data
        /// </summary>
        public TimeSpan? RefreshTime { get; set; }

        /// <summary>
        ///     Setting this to true, will return the object data even when it's older than "refreshTime" but not older than
        ///     "timeToLive" and start fetching new data in the background
        /// </summary>
        public bool UseLazyRefresh { get; set; }

        /// <summary>
        /// Optional parameter to provide a method that provides current date/time
        /// </summary>
        public Func<DateTime> GetNowFunc { get; set; }

        /// <summary>
        /// The function use to load data to 
        /// </summary>
        public Func<Task<T>> LoadFunc { get; set; }
    }

}