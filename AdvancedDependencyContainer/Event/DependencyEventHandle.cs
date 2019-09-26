using AdvancedDependencyContainer.Helper;
using System;

namespace AdvancedDependencyContainer.Event
{
    /// <summary>
    /// 依赖组件获取委托
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    internal delegate object ResolveEventHandle(Type type, object parameter = null);
    /// <summary>
    /// 依赖组件获取事件定义类
    /// </summary>
    internal static class DependencyEventHandle
    {
        private static ResolveEventHandle _resolveDelegate;
        internal static event ResolveEventHandle ResolveEvent
        {
            add
            {
                if(_resolveDelegate == null)
                {
                    _resolveDelegate += value;
                }
            }
            remove { }
        }
        
        public static object OnResolveEvent(this Type type, object parameter = null)
        {
            return _resolveDelegate.Invoke(type, parameter);
        }
    }
}
