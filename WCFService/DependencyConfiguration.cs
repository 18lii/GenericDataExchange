using AdvancedDependencyContainer.Interface;
using SI = Sequencer.Interface;
using WCFService.Interface;
using WCFService.DbUnitOfWork;
using System.Configuration;
using WCFService.Infrastructure;
using AdvancedDependencyContainer.Entities;

namespace WCFService
{
    /// <summary>
    /// WCF提供的全部服务所需组件在此装配
    /// </summary>
    public class DependencyConfiguration : IDependencyConfiguration
    {
        public IDependencyBindContext DependencyBindContext { get; set; }
        
        /// <summary>
        /// 服务组件配置方法
        /// </summary>
        /// <param name="codes"></param>
        public IDependencyBindContext BindDependency()
        {
            //DependencyBindContext.UseXmlConfiguration("DependencyComponentConfiguration");
            //使用app.config配置文件进行依赖组件绑定
            //DependencyBindContext.UseConfiguration(DependencyConfigurationOption.App, "dependencyComponentConfiguration");
            //使用编码进行依赖组件绑定
            DependencyBindContext
                .Bind<IDbUnitOfWork>().To<UnitOfwork>()//绑定数据库工作单元
                .Bind<SI::IPeristalticConfiguration>().To<PeristalticConfiguration>(new object[]
                {
                    ConfigurationManager.AppSettings["DESString"].Decryptogram(ConfigurationManager.AppSettings["DESKey"].Decryptogram())
                })//绑定定序器配置程序依赖
                .UseConfiguration(DependencyConfigurationOption.App, "dependencyComponentConfiguration");
            return DependencyBindContext;
        }
    }
}
