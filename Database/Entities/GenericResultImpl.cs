using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    internal class GenericResultImpl : GenericResult
    {
        public GenericResultImpl(ResultType resultType) : base(resultType) { }

        public GenericResultImpl(ResultType resultType, object appendData) : base(resultType, appendData) { }

        public GenericResultImpl(ResultType resultType, string logMessage) : base(resultType, logMessage) { }

        public GenericResultImpl(ResultType resultType, string logMessage, object appendData) : base(resultType, logMessage, appendData) { }
    }
    internal class GenericResultImpl<T> : GenericResult<T>
    {

        public GenericResultImpl(ResultType resultType, T appendData) : base(resultType, appendData) { }

        public GenericResultImpl(ResultType resultType, string message, T item) : base(resultType, message, item) { }
    }
}
