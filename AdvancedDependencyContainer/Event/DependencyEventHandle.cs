using AdvancedDependencyContainer.Helper;
using System;

namespace AdvancedDependencyContainer.Event
{
    internal delegate T ResolveEventHandle<T>(object parameter = null);
    internal delegate object ResolveEventHandle(Type type, object parameter = null);
    internal static class DependencyEventHandle<T>
    {
        private static event ResolveEventHandle<T> ResolveEvent;
        internal static void Register(ResolveEventHandle<T> method)
        {
            if(ResolveEvent == null)
            {
                ResolveEvent += method;
            }
        }
        public static T OnResolveEvent(object parameter = null)
        {
            return (T)ResolveEvent.Invoke(parameter);
        }
    }
    internal class DependencyEventHandle
    {
        private static event ResolveEventHandle ResolveEvent;
        internal static void Register(ResolveEventHandle method)
        {
            if (ResolveEvent == null)
            {
                ResolveEvent += method;
            }
        }
        public static object OnResolveEvent(Type type, object parameter = null)
        {
            return ResolveEvent.Invoke(type, parameter);
        }
        public static T OnResolveEvent<T>(object parameter = null)
        {
            return ResolveEvent.Invoke(typeof(T), parameter).CastTo<T>();
        }
    }
}
