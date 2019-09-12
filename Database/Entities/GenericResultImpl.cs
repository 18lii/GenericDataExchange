using Core.Entities;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    /// <summary>
    /// 通用返回值本地实现
    /// </summary>
    [Serializable]
    public class GenericResultImpl : GenericResult, IGenericResult
    {
        public GenericResultImpl(ResultType resultType) : base(resultType) { }
        public GenericResultImpl(ResultType resultType, object appendData) : base(resultType, appendData) { }
        public GenericResultImpl(ResultType resultType, string logMessage) : base(resultType, logMessage) { }
        public GenericResultImpl(ResultType resultType, string logMessage, object appendData) : base(resultType, logMessage, appendData) { }
    }
    [Serializable]
    public class GenericResultImpl<T> : GenericResult<T>, IGenericResult<T>
    {

        public GenericResultImpl(ResultType resultType, T appendData) : base(resultType, appendData) { }

        public GenericResultImpl(ResultType resultType, string message, T item) : base(resultType, message, item) { }
    }
}
