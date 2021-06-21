using System;
using System.Runtime.Serialization;

namespace CommandLineTool.Exceptions
{
    [Serializable]
    public sealed class ConsoleAleadyRunningException : Exception
    {
        public ConsoleAleadyRunningException() { }
        public ConsoleAleadyRunningException(string message) : base(message)
        {
        }

        public ConsoleAleadyRunningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private ConsoleAleadyRunningException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
