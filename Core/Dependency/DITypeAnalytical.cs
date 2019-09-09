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
            var type = typeof(T);
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
                            args.Add(para);
                        }
                    }
                    instance = Activator.CreateInstance(type, args);
                    break;
                }
            }
            return (T)instance;
        }
    }
}
