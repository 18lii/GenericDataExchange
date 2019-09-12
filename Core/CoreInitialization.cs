using Core.Entities;
using Core.Events;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CoreInitialization
    {
        //注入依赖核心
        public CoreInitialization(IIoCKernel kernel)
        {
            _kernel = kernel;
        }
        private readonly IIoCKernel _kernel;
        /// <summary>
        /// 绑定消息处理无返回值事件依赖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Initialization<T>()
        {
            _kernel.Bind<IGenericEventHandle<T>>().To<GenericEventHandle<T>>();
        }
        /// <summary>
        /// 绑定消息处理有返回值事件依赖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        public void Initialization<T, R>()
        {
            _kernel.Bind<IGenericEventHandle<T, R>>().To<GenericEventHandle<T, R>>();
        }
    }
}
