using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions
{
    public class ConfigurationException : CustomException
    {
        public ConfigurationException(string message)
            : base(StatusCodes.Status404NotFound, message) { }
    }
}
