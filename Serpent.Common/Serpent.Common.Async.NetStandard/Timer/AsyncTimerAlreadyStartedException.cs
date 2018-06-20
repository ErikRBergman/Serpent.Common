namespace Serpent.Common.Async.Timer
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AsyncTimerAlreadyStartedException : Exception
    {
        public AsyncTimerAlreadyStartedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public AsyncTimerAlreadyStartedException(string message)
            : base(message)
        {
        }
    }
}