using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseFactory.Entity
{
    /// <summary>
    /// 通用事件参数信息类本地实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GenericEventArgs<T> : IGenericEventArg<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PeristalticName { get; }

        public string UserId { get; }

        public T Item { get; set; }
        public object AttachData { get; set; }
        public string Message { get; set; }

        public GenericEventArgs(string name, string userId)
        {
            PeristalticName = name;
            UserId = userId;
        }
        public GenericEventArgs(string name, T item)
        {
            PeristalticName = name;
            Item = item;
        }
        public GenericEventArgs(string name, string userId, T item)
            : this(name, userId)
        {
            Item = item;
        }
        public GenericEventArgs(string name, string userId, T item, object attachData)
            : this(name, userId, item)
        {
            AttachData = attachData;
        }
    }
}
