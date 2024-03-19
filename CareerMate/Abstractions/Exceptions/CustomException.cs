using System;
using System.Text.Json.Serialization;

namespace CareerMate.Abstractions.Exceptions
{
    public class CustomException : ApplicationException
    {
        public CustomException()
          : base(string.Empty)
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        [JsonConstructor]
        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
