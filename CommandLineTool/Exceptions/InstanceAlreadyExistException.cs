using System;
using System.Runtime.Serialization;

namespace CommandLineTool.Exceptions
{
    [Serializable]
    public sealed class InstanceAlreadyExistException : Exception
    {
        public InstanceAlreadyExistException() { }
        public InstanceAlreadyExistException(string message) : base(message)
        {
        }

        private InstanceAlreadyExistException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
