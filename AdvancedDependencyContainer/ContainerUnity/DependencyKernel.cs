using AdvancedDependencyContainer.Event;
using System;

namespace AdvancedDependencyContainer.ContainerUnity
{
    public static class DependencyKernel
    {
        public static T Resolve<T>(object parameter = null) where T : class
        {
            return DependencyEventHandle.OnResolveEvent<T>(parameter);
        }
        public static object Resolve(Type type, object parameter = null)
        {
            return DependencyEventHandle.OnResolveEvent(type, parameter);
        }
    }
}
