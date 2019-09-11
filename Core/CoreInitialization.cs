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
        public CoreInitialization(IIoCKernel kernel)
        {
            _kernel = kernel;
        }
        private readonly IIoCKernel _kernel;
        public void Initialization()
        {
            _kernel.Bind<IGenericEventHandle>().To<GenericEventHandle>();
        }
        public void Initialization<T>()
        {
            _kernel.Bind<IGenericEventHandle<T>>().To<GenericEventHandle<T>>();
        }
        public void Initialization<T, R>()
        {
            _kernel.Bind<IGenericEventHandle<T, R>>().To<GenericEventHandle<T, R>>();
        }
    }
}
