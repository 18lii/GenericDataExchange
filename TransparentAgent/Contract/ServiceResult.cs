using System;
using TransparentAgent.Interface;

namespace TransparentAgent.Contract
{
    [Serializable]
    public class ServiceResult : IServiceResult
    {
        public ServiceResult() { }
        public ServiceResult(int resultType)
        {
            ResultType = resultType;
        }
        public ServiceResult(int resultType, string message, string logMessage, object appendData)
            : this(resultType)
        {
            Message = message;
            LogMessage = logMessage;
            AppendData = appendData;
        }

        public int ResultType { get; set; }
        public string Message { get; set; }
        public string LogMessage { get; set; }
        public object AppendData { get; set; }
    }
}
