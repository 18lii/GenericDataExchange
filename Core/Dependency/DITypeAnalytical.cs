using Core.Interface;
using System;
using System.Collections.Generic;

namespace Core.Dependency
{
    /// <summary>
    /// 反射实例化
    /// </summary>
    public class DITypeAnalytical : IDITypeAnalytical
    {
        
        public T GetValue<T>()
        {
            object analytical(Type type)
            {
                var constructorInfos = type.GetConstructors();
                object instance = null;
                foreach (var conInfo in constructorInfos)
                {
                    if (conInfo.GetParameters().Length > 0)
                    {
                        var paras = conInfo.GetParameters();
                        var args = new List<object>();

                        foreach (var para in paras)
                        {
                            if (IoCContext.Context.DIManager.ContainsKey(para.ParameterType))
                            {
                                args.Add(analytical(IoCContext.Context.DIManager.GetTypeInfo(para.ParameterType)));
                            }
                        }
                        instance = Activator.CreateInstance(type, args.ToArray());
                        break;
                    }
                    else
                    {
                        return Activator.CreateInstance(type);
                    }
                }
                return instance;
            };
            var targetType = typeof(T);
            var obj = analytical(targetType);
            return (T)obj;
        }
    }
}
