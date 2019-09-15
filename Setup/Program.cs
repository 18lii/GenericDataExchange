using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using WCFService.Infrastructure;

namespace Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            var codes = new string[2];
            Console.WriteLine("GSP数据交换服务注册程序 v1.0");
            Console.Write("请数据注册码：");
            var inputs = Console.ReadLine();
            Console.WriteLine("正在验证，请稍后......");
            try
            {
                var strs = inputs.Split('-');
                if (string.IsNullOrEmpty(strs[1].Decryptogram()) && string.IsNullOrEmpty(strs[0].Decryptogram(strs[1].Decryptogram())))
                {
                    Console.WriteLine("注册码错误，请验证后重新输入或重新索取！");
                }
                else
                {
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["DESString"].Value = strs[0];
                    config.AppSettings.Settings["DESKey"].Value = strs[1];
                    config.Save();
                    Console.WriteLine("已成功注册，按任意键退出...");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("注册失败，请重试或联系发行商！");
                Console.WriteLine("失败原因：{0}", e.Message);
            }
        }
    }
}
