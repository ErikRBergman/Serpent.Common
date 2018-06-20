// ReSharper disable once CheckNamespace
namespace Serpent.Common.Async
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Serpent.Common.Async.Timer;

    public class AsyncTimer
    {
        private readonly object lockObject = new object();

        private Task invokationTask;

        private CancellationTokenSource shutdownCancellationTokenSource;

        private AsyncTimerSettings settings;

        private AsyncTimer(AsyncTimerSettings settings, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.settings = settings.Clone();

            if (this.settings.Period.Ticks <= 0)
            {
                throw new ArgumentException("Period must have a positive value.", nameof(this.settings) + "." + nameof(this.settings.Period));
            }

            if (this.settings.InvokationFunc == null)
            {
                throw new ArgumentException("InvokationFunc must not be null", nameof(this.settings) + "." + nameof(this.settings.InvokationFunc));
            }

            if (this.settings.DueTime >= TimeSpan.Zero)
            {
                this.Start();
            }   

            if (cancellationToken != CancellationToken.None)
            {
                cancellationToken.Register(() => this.StopAsync(false));
            }
        }

        public bool IsRunning
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField - we're only making a copy here so no thread sync is necessary
                var task = this.invokationTask;
                return task != null && !task.IsCompleted;
            }
        }

        public Func<Exception, bool> OnExceptionFunc { get; set; }

        /// <summary>
        ///     Creates and starts a new async timer
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static AsyncTimer StartNew(AsyncTimerSettings settings)
        {
            if (settings.DueTime < TimeSpan.Zero)
            {
                settings.DueTime = TimeSpan.Zero;
            }

            return new AsyncTimer(settings);
        }

        public async Task ForceStartAsync(bool awaitRunningTimerShutdown = true, bool overwriteRunningTimer = false)
        {
            if (awaitRunningTimerShutdown)
            {
                await this.StopAsync();
            }

            lock (this.lockObject)
            {
                if (overwriteRunningTimer == false && this.IsRunning)
                {
                    throw new Exception("The async timer is already started.");
                }

                this.shutdownCancellationTokenSource = new CancellationTokenSource();
                this.invokationTask = Task.Run(
                    () => TimerInvokationAsync(this.settings, this.shutdownCancellationTokenSource.Token));
            }
        }

        public void Start()
        {
            lock (this.lockObject)
            {
                if (this.IsRunning)
                {
                    throw new AsyncTimerAlreadyStartedException("The async timer is already started.");
                }

                this.shutdownCancellationTokenSource = new CancellationTokenSource();
                this.invokationTask = Task.Run(
                    () => TimerInvokationAsync(this.settings, this.shutdownCancellationTokenSource.Token));
            }
        }

        public Task StopAsync(bool throwExceptionIfNotRunning = true)
        {
            lock (this.lockObject)
            {
                if (throwExceptionIfNotRunning && this.IsRunning)
                {
                    throw new AsyncTimerNotRunningException("The async timer is not running");
                }

                var tokenSource = this.shutdownCancellationTokenSource;
                var task = this.invokationTask;

                this.shutdownCancellationTokenSource = null;
                this.invokationTask = null;

                if (tokenSource != null && task != null)
                {
                    tokenSource.Cancel();
                    return task;
                }
            }

            return Task.CompletedTask;
        }

        public Task WaitAsync()
        {
            var task = this.invokationTask;
            return task ?? Task.CompletedTask;
        }

        private static async Task TimerInvokationAsync(
            AsyncTimerSettings settings,
            CancellationToken cancellationToken)
        {
            try
            {
                if (settings.DueTime > TimeSpan.Zero)
                {
                    await Task.Delay(settings.DueTime, cancellationToken);
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await settings.InvokationFunc(cancellationToken);
                    }
                    catch (Exception e)
                    {
                        if (settings.ExceptionAsyncFunc != null)
                        {
                            if (await settings.ExceptionAsyncFunc(e) == false)
                            {
                                throw;
                            }
                        }

                        if (settings.StopTimerOnException)
                        {
                            throw;
                        }
                    }

                    await Task.Delay(settings.Period, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}