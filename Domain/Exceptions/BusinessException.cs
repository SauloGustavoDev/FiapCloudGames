using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions;
    public class BusinessException : CustomException
    {
    public BusinessException(string message)
            : base(StatusCodes.Status400BadRequest, message) { }
    }
