using AdvancedDependencyContainer.Helper;
using AdvancedDependencyContainer.Interface;
using System;
using System.Collections.Generic;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// 结构解析类
    /// </summary>
    internal class DITypeAnalytical : IDITypeAnalytical
    {
        /// <summary>
        /// 解析<see cref="{T}"/>类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T GetValue<T>()
        {
            object analytical(Type type)
            {
                var constructorInfos = type.GetConstructors();
                object instance = null;
                foreach (var conInfo in constructorInfos)
                {
                    var parameters = conInfo.GetParameters();
                    if (parameters.Length > 0)
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
                        if (IoCContext.Context.DIManager.ContainsKey(type))
                        {
                            foreach (var arg in IoCContext.Context.DIManager.GetArgsInfo(type))
                            {
                                args.Add(arg);
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
            return analytical(typeof(T)).CastTo<T>();
        }
        /// <summary>
        /// 解析指定<see cref="Type"/>类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object GetValue(Type type)
        {
            try
            {
                object analytical(Type argType)
                {
                    var constructorInfos = argType.GetConstructors();
                    object instance = null;
                    foreach (var conInfo in constructorInfos)
                    {
                        var parameters = conInfo.GetParameters();
                        if (parameters.Length > 0)
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
                            if (IoCContext.Context.DIManager.ContainsKey(argType))
                            {
                                foreach (var arg in IoCContext.Context.DIManager.GetArgsInfo(argType))
                                {
                                    args.Add(arg);
                                }
                            }
                            instance = Activator.CreateInstance(argType, args.ToArray());
                            break;
                        }
                        else
                        {
                            return Activator.CreateInstance(argType);
                        }
                    }
                    return instance;
                };
                return analytical(type);
            }
            catch
            {
                return default;
            }
        }
    }
}
