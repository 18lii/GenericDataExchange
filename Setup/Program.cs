using Database.Helper;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Configuration;
using WCFService.Infrastructure;

namespace WindowsServiceSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("     GSP数据交换服务注册程序 v1.0");
            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.Write("请输入注册码：");
            var inputs = Console.ReadLine();
            Console.WriteLine("======================================");
            Console.WriteLine();
            try
            {
                var strs = inputs.Split('-');
                if (string.IsNullOrEmpty(strs[1].Decryptogram()) && string.IsNullOrEmpty(strs[0].Decryptogram(strs[1].Decryptogram())))
                {
                    Console.WriteLine();
                    Console.WriteLine("注册码错误，请验证后重新输入或重新索取！");
                }
                else
                {
                    //测试连接字符串正确性
                    if(new ConnectionTest().Test(strs[0].Decryptogram(strs[1].Decryptogram())))
                    {
                        Console.Write("请输入服务器IPv4地址：");
                        var address = Console.ReadLine();
                        Console.Write("请指定本服务使用的端口号：");
                        var port = Console.ReadLine();
                        var path = Path.GetDirectoryName(new Program().GetType().Assembly.Location) + "\\";
                        //打开配置文件并配置密文及服务地址
                        var config = ConfigurationManager.OpenExeConfiguration(path + "WindowsService.exe");
                        config.AppSettings.Settings["DESString"].Value = strs[0];
                        config.AppSettings.Settings["DESKey"].Value = strs[1];
                        config.AppSettings.Settings["baseAddress"].Value = address;
                        config.AppSettings.Settings["port"].Value = port;
                        config.Save();
                        
                        //执行Windows服务安装程序
                        using (var process = new Process())
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            
                            Console.Write("请输入.NET FramWork在服务器上的安装路径：");
                            var str = Console.ReadLine();
                            if (!str.EndsWith("\\")) { str += "\\"; }
                            Console.WriteLine();
                            process.StartInfo.WorkingDirectory = @str;
                            process.StartInfo.FileName = @str + "InstallUtil.exe";
                            process.StartInfo.Arguments = path + "WindowsService.exe";
                            process.StartInfo.UseShellExecute = false;
                            process.Start();
                            process.WaitForExit();
                        }
                        using(var process = new Process())
                        {
                            process.StartInfo.WorkingDirectory = path;
                            process.StartInfo.FileName = "Net";
                            process.StartInfo.Arguments = "Start WindowsService";
                            process.Start();
                            process.WaitForExit();
                        }
                        using(var process = new Process())
                        {
                            process.StartInfo.WorkingDirectory = path;
                            process.StartInfo.FileName = "sc";
                            process.StartInfo.Arguments = "config WindowsService start=auto";
                            process.Start();
                            process.WaitForExit();
                        }
                        Console.WriteLine("已成功安装服务");
                        Console.WriteLine();
                        Console.WriteLine("服务地址为：net.tcp://{0}:{1}/DataExchangeService", address, port);
                        Console.WriteLine();
                        Console.WriteLine("请于客户端配置文件中配置此地址，以调用此服务");
                        Console.WriteLine();
                        Console.WriteLine("按任意键退出......");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("注册码与服务器不匹配，请更改正确的注册码或联系发行商！");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("注册失败，请重试或联系发行商！" + e.Message);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("按任意键退出......");
                Console.ReadKey();
            }
        }
    }
}
