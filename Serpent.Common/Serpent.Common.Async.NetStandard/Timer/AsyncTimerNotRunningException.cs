namespace Serpent.Common.Async.Timer
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AsyncTimerNotRunningException : Exception
    {
        public AsyncTimerNotRunningException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public AsyncTimerNotRunningException(string message)
            : base(message)
        {
        }
    }
}