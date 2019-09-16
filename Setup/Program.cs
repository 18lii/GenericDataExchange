using Database.Helper;
using Database.Interface;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using WCFService.Infrastructure;

namespace WindowsServiceSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            var codes = new string[2];
            Console.WriteLine("       GSP数据交换服务注册程序 v1.0");
            Console.WriteLine("*----*----*----*----*----*----*----*----*----*");
            Console.WriteLine();
            Console.Write("请输入注册码：");
            var inputs = Console.ReadLine();
            Console.WriteLine("*----*----*----*----*----*----*----*----*----*");
            Console.WriteLine();
            Console.WriteLine("正在验证，请稍后......");
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
                    if(new ConnectionTest().Test(strs[0].Decryptogram(strs[1].Decryptogram())))
                    {
                        var path = Path.GetDirectoryName(new Program().GetType().Assembly.Location);
                        var config = ConfigurationManager.OpenExeConfiguration(path + "\\WindowsService.exe");
                        config.AppSettings.Settings["DESString"].Value = strs[0];
                        config.AppSettings.Settings["DESKey"].Value = strs[1];
                        config.Save();
                        using (var process = new Process())
                        {
                            Console.WriteLine("*----*----*----*----*----*----*----*----*----*");
                            Console.WriteLine();
                            Console.Write("请输入.NET FramWork在服务器上的安装路径：");
                            var str = Console.ReadLine();
                            Console.WriteLine();
                            process.StartInfo.WorkingDirectory = @str;
                            process.StartInfo.FileName = @str + "\\InstallUtil.exe";
                            process.StartInfo.Arguments = path + "\\WindowsService.exe";
                            process.StartInfo.UseShellExecute = false;
                            process.Start();
                            Console.WriteLine();
                            Console.WriteLine("已成功安装服务，按任意键退出...");
                            process.WaitForExit();
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("注册码与服务器不匹配，请更正确的注册码或联系发行商！");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("注册失败，请重试或联系发行商！");
                Console.WriteLine("按任意键退出......");
                Console.ReadKey();
            }
        }
    }
}
