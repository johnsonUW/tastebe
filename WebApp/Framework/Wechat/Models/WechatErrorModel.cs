using Framework.ExceptionHandling;
using Framework.Logging;
using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatErrorModel
    {
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        public void CheckError()
        {
            if (ErrorCode == 0) return;
            LoggingQueue.Add(new LogMessage
            {
                Message = "WechatError",
                PayLoad = ErrorMessage
            });
            throw new ApiException(ApiErrorCode.WechatError, ErrorMessage);
        }
    }
}