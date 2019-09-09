using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConsole
{
    class GenericEventArg<T> : IGenericEventArg<T>
    {
        public Guid Id { get; set; }

        public string PeristalticName { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }
        public T Item { get; set; }
        public object AttachData { get; set; }
    }
}
