using Core.Helper;
using Core.Interface;

namespace Core.Entities
{
    public abstract class GenericResult : IGenericResult
    {
        public GenericResult(ResultType resultType)
        {
            ResultType = resultType;
            Message = resultType.ToDescription();
        }
        public GenericResult(ResultType resultType, object appendData)
            : this(resultType)
        {
            AppendData = appendData;
        }
        public GenericResult(ResultType resultType, string logMessage)
            : this(resultType)
        {
            LogMessage = logMessage;
        }
        public GenericResult(ResultType resultType, string logMessage, object appendData)
            : this(resultType, logMessage)
        {
            AppendData = appendData;
        }
        /// <summary>
        ///  获取或设置操作结果类型
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        ///  获取或设置操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  获取或设置操作返回的日志消息，用于记录日志
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        ///  获取或设置操作结果附加信息
        /// </summary>
        public object AppendData { get; set; }
    }
    public abstract class GenericResult<T> : IGenericResult<T>
    {
        public GenericResult(ResultType resultType, T item)
        {
            Message = resultType.ToDescription();
            Item = item;
        }
        public GenericResult(ResultType resultType, string logMessage, T item)
            : this(resultType, item)
        {
            LogMessage = logMessage;
            Item = item;
        }
        /// <summary>
        ///  获取或设置操作结果类型
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        ///  获取或设置操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  获取或设置操作返回的日志消息，用于记录日志
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        ///  获取或设置操作结果附加信息
        /// </summary>
        public T Item { get; set; }
    }
}
