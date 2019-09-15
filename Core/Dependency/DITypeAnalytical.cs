using Core.Interface;
using System;
using System.Collections.Generic;

namespace Core.Dependency
{
    /// <summary>
    /// 结构解析类
    /// </summary>
    public class DITypeAnalytical : IDITypeAnalytical
    {
        
        public T GetValue<T>(object parameter)
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
                        if(parameter != null)
                        {
                            args.Add(parameter);
                        }
                        instance = Activator.CreateInstance(type, args.ToArray());
                        break;
                    }
                    else
                    {
                        if(parameter != null)
                        {
                            return Activator.CreateInstance(type, parameter);
                        }
                        else
                        {
                            return Activator.CreateInstance(type);
                        }
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
