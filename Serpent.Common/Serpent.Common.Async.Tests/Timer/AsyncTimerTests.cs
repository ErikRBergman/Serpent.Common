namespace Serpent.Common.Async.Tests.Timer
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    public class AsyncTimerTests
    {
        [Fact]
        public async Task Normal()
        {
            int counter = 0;

            var timer = AsyncTimer.StartNew(
                new AsyncTimerSettings
                {
                    Period = TimeSpan.FromMilliseconds(10),
                    InvokationFunc = async c =>
                        {
                            Interlocked.Increment(ref counter);
                        }
                });

            await Task.Delay(100);

            Assert.InRange(counter, 7, 13);

            await timer.StopAsync();
        }

        [Fact]
        public async Task PassThroughException()
        {
            int counter = 0;

            var timer = AsyncTimer.StartNew(
                new AsyncTimerSettings
                {
                    Period = TimeSpan.FromSeconds(100),
                    StopTimerOnException = true,
                    InvokationFunc = c => throw new FaxMachineException("FAXMACHINE!")
                });

            try
            {
                await timer.StopAsync(false);
            }
            catch (FaxMachineException e)
            {

            }

        }

        private class FaxMachineException : Exception
        {
            public FaxMachineException(string message) : base(message)
            {

            }
        }


        [Fact]
        public async Task DelayedStart()
        {
            int counter = 0;

            var timer = AsyncTimer.StartNew(
                new AsyncTimerSettings
                {
                    DueTime = TimeSpan.FromMilliseconds(300),
                    Period = TimeSpan.FromSeconds(100),
                    StopTimerOnException = true,
                    InvokationFunc = async c =>
                        {
                            Interlocked.Increment(ref counter);
                        }
                });

            await Task.Delay(TimeSpan.FromMilliseconds(20));

            Assert.Equal(0, counter);

            await Task.Delay(TimeSpan.FromSeconds(0.5));

            await timer.StopAsync(false);
        }
    }
}
