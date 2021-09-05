using System;
using System.Runtime.Serialization;

namespace RoverInTheMars
{
    [Serializable]
    internal class DriveOffException : Exception
    {
        public DriveOffException()
        {
        }

        public DriveOffException(string message) : base(message)
        {
        }

        public DriveOffException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DriveOffException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DriveOffException(Exception innerException) : base("drive off command is being sent", innerException)
        {
        }
    }
}