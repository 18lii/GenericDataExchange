using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Infrastructure
{
    public interface ITestInterface
    {
        string Name { get; set; }
        string Test(bool b);
    }
    public class TestClass : ITestInterface
    {
        public string Name { get; set; }

        public string Test(bool b)
        {
            return b ? "OK" : "Fail";
        }
    }
}
