using System;
using System.Runtime.Serialization;

namespace RoverInTheMars
{
    [Serializable]
    internal class InvalidCommandException : Exception
    {
        public InvalidCommandException()
        {
        }

        public InvalidCommandException(string message) : base(message)
        {
        }

        public InvalidCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidCommandException(Exception innerException) : base("Invalid Command is being sent", innerException)
        {
        }
    }
}