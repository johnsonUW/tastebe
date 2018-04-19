using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Framework.Logging
{
    public static class LoggingQueue
    {
        private static readonly BlockingCollection<LogMessage> MessageQueue = new BlockingCollection<LogMessage>();

        //public static void Add(Func<LogMessage> func)
        //{
        //    //MessageQueue.Add(func);
        //}

        public static void Add(LogMessage message)
        {
            MessageQueue.Add(message);
        }

        public static void Start()
        {
            Task.Factory.StartNew(Process);
        }

        public static void Stop()
        {
            MessageQueue.CompleteAdding();
        }

        private static void Process()
        {
            while (!MessageQueue.IsAddingCompleted || MessageQueue.Count > 0)
            {
                var msg = MessageQueue.Take();
                //var msg = func();
                Trace.TraceInformation($"Message:{msg.Message} RequestUrl:{msg.RequestUrl} SessionToken:{msg.SessionToken} UserId:{msg.UserId} Payload:{msg.PayLoad}");
            }
        }
    }
}