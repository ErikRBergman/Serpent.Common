﻿// ReSharper disable once CheckNamespace
namespace Serpent.Common.MessageBus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageHandlerRetry<in TMessageType>
    {
        Task HandleRetryAsync(TMessageType message, Exception exception, int attemptNumber, int maxNumberOfAttemps, TimeSpan delay, CancellationToken cancellationToken);

        Task MessageHandledSuccessfullyAsync(TMessageType message, int attemptNumber, int maxNumberOfAttemps, TimeSpan delay);
    }
}