namespace Serpent.Common.Async
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncTimerSettings
    {
        public TimeSpan DueTime { get; set; }

        public TimeSpan Period { get; set; }

        public Func<CancellationToken, Task> InvokationFunc { get; set; }

        public bool StopTimerOnException { get; set; }

        public Func<Exception, Task<bool>> ExceptionAsyncFunc { get; set; }

        public AsyncTimerSettings Clone()
        {
            return new AsyncTimerSettings
            {
                DueTime = this.DueTime,
                InvokationFunc = this.InvokationFunc,
                Period = this.Period,
                StopTimerOnException = this.StopTimerOnException,
                ExceptionAsyncFunc = this.ExceptionAsyncFunc
            };

        }
    }
}