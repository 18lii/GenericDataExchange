using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Infrastructure
{
    public class IoCEntity
    {
        public ITestInterface Ti { get; set; }
        public IoCEntity(ITestInterface ti)
        {
            Ti = ti;
        }
    }
}
