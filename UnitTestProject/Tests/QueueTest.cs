using Core.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;

namespace UnitTestProject.Tests
{
    [TestClass]
    public class QueueTest
    {
        public QueueTest()
        {

        }
        /// <summary>
        /// des双参数解密测试
        /// </summary>
        [TestMethod]
        public void ServiceParamTest()
        {
            var connStr = new string[2]
            {
                "FF50E1525C650A58AEAB16969808322F8A05F4F054F803F39A9EB1CA3EABBC36E55E402825C50E5BD28A79D5A17761F232CBE1657DA924823180E0634289856C59B54F3B24BEE5D2A58CEE37309BA49BD2509C331F7CC98E",
                "30E9AA65BA938F545A6CC1DB3027CED9"
            };
            var result = connStr[0].Decryptogram(connStr[1].Decryptogram());
            //Initialization.PeristalticStart(new PeristalticConfiguration(connStr));
            Assert.AreEqual(result, "Data Source=192.168.1.2\\SQL2008;uid=sa;pwd=123456a;database=xfzb;Connect Timeout=30");
        }
    }
}
