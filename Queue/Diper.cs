using Core.Interface;
using Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    public class Dipper
    {
        private readonly IIoCKernel _kernel;
        public Dipper(IIoCKernel kernel)
        {
            _kernel = kernel;
        }
        public void DipBind()
        {
            _kernel.Bind<IBindContext>().To<PeristalticContext>();
        }
    }
}
