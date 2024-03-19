using System.Text.Json.Serialization;

namespace CareerMate.Abstractions.Exceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException()
            : base()
        {
        }

        [JsonConstructor]
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}
