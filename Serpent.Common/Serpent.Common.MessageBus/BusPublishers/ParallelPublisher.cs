namespace Serpent.Common.MessageBus.BusPublishers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ParallelPublisher<T> : BusPublisher<T>
    {
        public static BusPublisher<T> Default { get; } = new ParallelPublisher<T>();

        public override Task PublishAsync(IEnumerable<ISubscription<T>> subscriptions, T message)
        {
            return Task.WhenAll(
                subscriptions.Select(
                    s =>
                        {
                            var receiveEventAsync = s.EventInvocationFunc;
                            return receiveEventAsync == null ? Task.CompletedTask : receiveEventAsync(message);
                        }));
        }
    }
}