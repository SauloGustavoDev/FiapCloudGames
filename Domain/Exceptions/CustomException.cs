
namespace Domain.Exceptions;
    public abstract class CustomException : Exception
    {
        public int StatusCode { get; }

        protected CustomException(int statusCode, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        protected CustomException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
