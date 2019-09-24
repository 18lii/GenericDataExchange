using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// 注入类型关系管理类
    /// </summary>
    internal class DIManager
    {
        private Dictionary<Type, Type> _DITypeInfo;
        public DIManager()
        {
            if(_DITypeInfo == null)
            {
                _DITypeInfo = new Dictionary<Type, Type>();
            }
        }

        /// <summary>
        /// 添加DI类型关系
        /// </summary>
        /// <param name="key">抽象类型</param>
        /// <param name="value">实现类型</param>
        public void AddTypeInfo(Type key, Type value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (_DITypeInfo.ContainsKey(key))
            {
                return;
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _DITypeInfo.Add(key, value);
        }

        /// <summary>
        /// 获取DI类型关系的实现类型
        /// </summary>
        /// <param name="key">抽象类型</param>
        /// <returns></returns>
        public Type GetTypeInfo(Type key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (_DITypeInfo.ContainsKey(key))
            {
                return _DITypeInfo[key];
            }
            return null;
        }

        public bool ContainsKey(Type key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return _DITypeInfo.ContainsKey(key);
        }
    }
}
