using System;

namespace Framework.Logging
{
    public class LogMessage
    {
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string RequestUrl { get; set; }
        public string SessionToken { get; set; }
        public string UserId { get; set; }
        public string PayLoad { get; set; }
    }
}