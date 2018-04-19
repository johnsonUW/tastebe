using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Framework.ExceptionHandling;
using Framework.Logging;
using Framework.Models;
using Framework.Utils;

namespace WebApp
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);

            var exception = context.Exception;

            ErrorModel errorModel;

            if (exception is ApiException)
            {
                var apiException = exception as ApiException;
                errorModel = GetErrorModel(apiException);
            }
            else
            {
                errorModel = new ErrorModel
                {
                    ErrorCode = ApiErrorCode.Unknown,
                    Message = ApiErrorCode.Unknown.ToString()
                };
            }

            var message = exception is ApiException ? errorModel.Message : exception.Message;

            Exception innerEx = exception.InnerException;
            while (innerEx?.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            LoggingQueue.Add(new LogMessage
            {
                LogLevel = LogLevel.Error,
                Message = $"Error: Message: { message } Stacktrace: {exception.StackTrace} InnerException: {innerEx?.Message} InnerExceptionStackTrace: {innerEx?.StackTrace}",
                RequestUrl = context.Request.RequestUri.ToString(),
                TimeStamp = DateTimeExtensions.BeiJingNow(),
                PayLoad = context.Request.Content?.ReadAsStringAsync().Result ?? ""
            });

            context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.OK, errorModel));
        }

        private static ErrorModel GetErrorModel(ApiException exception)
        {
            return new ErrorModel
            {
                ErrorCode = exception.ErrorCode,
                Message = exception.ErrorMessage ?? exception.ErrorCode.ToString()
            };
        }
    }
}