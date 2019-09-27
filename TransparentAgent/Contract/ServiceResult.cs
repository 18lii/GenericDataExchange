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
            Message = resultType ? "操作成功" : "操作失败";
        }
        public ServiceResult(bool resultType, object resultData)
            : this(resultType)
        {
            if (resultType)
            {
                AppendData = resultData;
            }
            else
            {
                LogMessage = (string)resultData;
            }
        }
        

        public bool ResultType { get; set; }
        public string Message { get; set; }
        public string LogMessage { get; set; }
        public object AppendData { get; set; }
    }
}
