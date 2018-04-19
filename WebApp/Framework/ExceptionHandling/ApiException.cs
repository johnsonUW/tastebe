using System;

namespace Framework.ExceptionHandling
{
    public class ApiException : Exception
    {
        public ApiErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiException(ApiErrorCode errorCode, string message = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
        }
    }
}