using System;
using TransparentAgent.Interface;

namespace TransparentAgent.Contract
{
    [Serializable]
    public class ServiceResult : IServiceResult
    {
        public ServiceResult() { }
        public ServiceResult(bool resultType)
        {
            ResultType = resultType;
        }
        public ServiceResult(bool resultType, string message, string logMessage)
            : this(resultType)
        {
            Message = message;
            LogMessage = logMessage;
        }
        public ServiceResult(bool resultType, string message, object appendData)
            : this(resultType)
        {
            Message = message;
            AppendData = appendData;
        }
        public ServiceResult(bool resultType, string message, string logMessage, object appendData)
            : this(resultType)
        {
            Message = message;
            LogMessage = logMessage;
            AppendData = appendData;
        }

        public bool ResultType { get; set; }
        public string Message { get; set; }
        public string LogMessage { get; set; }
        public object AppendData { get; set; }
    }
}
