using System;
using System.Runtime.Serialization;

namespace CommandLineTool.Exceptions
{
    [Serializable]
    public sealed class MissingAppAttributeException : Exception
    {
        public MissingAppAttributeException() { }
        public MissingAppAttributeException(string message) : base(message)
        {
        }

        private MissingAppAttributeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
