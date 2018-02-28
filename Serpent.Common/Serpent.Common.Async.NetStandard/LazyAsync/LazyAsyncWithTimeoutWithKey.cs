// ReSharper disable CheckNamespace
namespace Serpent.Common.Async
{
    using System;
    using System.Threading.Tasks;

    public class LazyAsyncWithTimeout<TKey, TValue>
    {
        private readonly TKey key;

        private readonly Func<TKey, Task<TValue>> loadValueFunc;

        private readonly object lockObject = new object();

        private readonly bool resetTimeoutOnGet;

        private readonly TimeSpan timeToLive;

        private Task<TValue> value = Task.FromResult(default(TValue));

        private DateTime loadTime = DateTime.MinValue;

        public LazyAsyncWithTimeout(TKey key, Func<TKey, Task<TValue>> loadValueFunc, TimeSpan timeToLive, bool resetTimeoutOnGet = true)
        {
            this.key = key;
            this.loadValueFunc = loadValueFunc;
            this.timeToLive = timeToLive;
            this.resetTimeoutOnGet = resetTimeoutOnGet;
        }

        public bool IsLoadRequired
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.value == null || DateTime.Now - this.loadTime > this.timeToLive;
                }
            }
        }

        public Task<TValue> ValueAsync
        {
            get
            {
                lock (this.lockObject)
                {
                    var now = DateTime.Now;
                    if (this.value == null || now - this.loadTime > this.timeToLive)
                    {
                        this.loadTime = now;
                        return this.value = this.loadValueFunc.Invoke(this.key);
                    }

                    if (this.resetTimeoutOnGet)
                    {
                        this.loadTime = now;
                    }

                    return this.value;
                }
            }
        }

        public void Clear(bool onlyIfLoadIsRequired = false)
        {
            lock (this.lockObject)
            {
                if (onlyIfLoadIsRequired == false || DateTime.Now - this.loadTime > this.timeToLive)
                {
                    this.loadTime = DateTime.MinValue;
                    this.value = null;
                }
            }
        }
    }
}