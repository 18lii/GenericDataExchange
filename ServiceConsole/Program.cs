using Core.Entities;
using Core.Ignition;
using Database.Interface;
using Queue;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFService;

namespace ServiceConsole
{
    class Program
    {
        private static DbFactoryImpl _db = new DbFactoryImpl();
        //private static IDbUtil<Class> _du = new SqlServerDbUtil<Class>("Data Source=1.1.1.1;uid=sa;pwd=wangbadan@1982;database=xfzb;Connect Timeout=30");
        private static AutoResetEvent signal = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            //Uri baseAddress = new Uri("http://localhost:8000/Service");
            ServiceHost host = new ServiceHost(typeof(DataExchangeService));
            //host.AddServiceEndpoint(typeof(IService), new WSHttpBinding(), "Service");
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //host.Description.Behaviors.Add(smb);
            host.Open();
            Console.ReadLine();
            host.Close();
            var s = 0;
            var f = 0;
            var sql = new StringBuilder();
            sql.AppendLine("select a. *,b.*,c.F_name as ItemName,p.Name as 门店名称 ");
            sql.AppendLine(",p.Name,d.F_Name as 供应商,q.Name 配送门店 ,c.f_dosage 剂型  ");
            sql.AppendLine("from t_ChainComSend a ");
            sql.AppendLine("left join  t_ChainComSendDetail b on a.F_BillID = b.F_BillID  ");
            sql.AppendLine("left join t_Item c on b.F_ItemID = c.F_ID ");
            sql.AppendLine("left join Theatre_Info p on a.F_StoreID = p.ID ");
            sql.AppendLine("left join t_Supplier d on d.f_ID = b.F_SupplierID ");
            sql.AppendLine("left join Theatre_Info q on a.F_QHStoreID = q.ID  ");
            sql.AppendLine("where a.F_Date >= '2019-01-01 00:00:00' ");
            sql.AppendLine("and   a.F_Date <= '2019-12-31 23:59:59'  ");
            sql.AppendLine("and   (F_Check = 2 or 2 = 2) and isnull(a.F_LCCheck,0) = 1 ");
            sql.AppendLine("and (((select top 1 isnull(F_N45,Convert(bit,0)) from t_Parm)=1 and   ");
            sql.AppendLine("a.F_BillID not in ( select F_BillID from t_ChainComSendDetail where F_ItemID in ( ");
            sql.AppendLine("select F_ID from t_Item where isnull(F_Square,0)=0))) or ");
            sql.AppendLine("(select top 1 isnull(F_N45,Convert(bit,0)) from t_Parm)=0 )");
            try
            {
                //Initialization.PeristalticStart(new PeristalticConfiguration("Data Source=1.1.1.1;uid=sa;pwd=wangbadan@1982;database=xfzb;Connect Timeout=30"));
                Console.WriteLine("Queue启动成功");
                s++;
            }
            catch (Exception e)
            {
                Console.WriteLine("Queue启动失败," + e.Message);
                f++;
            }
            Thread.Sleep(1000);
            try
            {
                //var dbr = _db.ExecuteReader("", "", "select * from v_sell_report where F_storeid = 2");
                //Console.WriteLine("Factory测试查询成功" + dbr.ToString());
                s++;
            }
            catch (Exception e)
            {
                Console.WriteLine("Factory测试查询失败，" + e.Message);
                f++;
            }
            //Thread.Sleep(1000);
            try
            {
                var t1 = DateTime.Now;
                //var dur = _du.GetList();
                var t2 = DateTime.Now;
                //Console.WriteLine("Dapper扩展查询成功, 共计" + dur.Count + "条记录，开始时间：" + t1 + ";结束时间：" + t2);
                //int i = 0;
                //foreach (var entity in dur)
                //{
                //    var str = new StringBuilder();
                //    foreach (var item in entity.GetType().GetProperties())
                //    {
                //        str.Append(item.Name + ": " + item.GetValue(entity, null) + ";");
                //    }
                //    i++;
                //    Console.WriteLine("行号:" + i + str.ToString());
                //    Thread.Sleep(200);
                //}
                s++;
            }
            catch (Exception e)
            {
                Console.WriteLine("Dapper扩展查询失败，" + e.Message);
                f++;
            }
            try
            {
                var time = DateTime.Now;
                //var dur = _du.GetDataTabel(new PeristalticEventArg<string> { Item = sql.ToString() });
                var t2 = DateTime.Now;
                //Console.WriteLine("Dapper扩展查询成功, 共计" + dur.Item.Rows.Count + "条记录，开始时间：" + time + ";结束时间：" + t2);
            }
            catch
            {

            }
            //Thread.Sleep(1000);
            try
            {

                //var context = new EntryContext();
                //var guids = new List<Guid>();
                //var time = DateTime.Now;
                //Thread.Sleep(50);
                //var ssss = new ManualResetEvent(false);
                //List<Thread> la = new List<Thread>();
                //for(var i = 0; i < 10; i++)
                //{
                //    var t = new Thread(() =>
                //    {
                //        ssss.WaitOne();
                //        var arg = new GenericEventArg<IFactoryContext> { UserId = "1", Id = Guid.NewGuid(), Item = new FactoryContext { SqlText = sql.ToString() } };
                //        var esr = context.Start(arg);
                //        guids.Add(arg.Id);
                //    });
                //    la.Add(t);
                //}
                //foreach(var item in la)
                //{
                //    item.Start();
                //}
                //ssss.Set();
                //Thread.Sleep(500);
                //foreach(var id in guids)
                //{
                //    var etb = _db.Result(id);
                //    var t2 = DateTime.Now;
                //    var tb = etb.AppendData as DataTable;
                //    Console.WriteLine("Queue扩展查询成功，共计" + tb.Rows.Count + "条记录，开始时间：" + time + ";结束时间：" + t2);
                //}
                
                s++;
            }
            catch (Exception e)
            {
                Console.WriteLine("队列查询失败，" + e.Message);
                f++;
            }
            Console.WriteLine(s.ToString() + "个测试成功，" + f.ToString() + "测试失败");
        }
        private static void LoadTest()
        {
            
        }
    }
}
