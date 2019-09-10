using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dependency
{
    public class DITypeAnalytical : IDITypeAnalytical
    {
        
        public T GetValue<T>()
        {
            Func<Type, object> analytical = null;
            analytical = delegate (Type type)
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
