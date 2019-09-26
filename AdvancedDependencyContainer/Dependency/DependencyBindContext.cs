using AdvancedDependencyContainer.Configurations;
using AdvancedDependencyContainer.Helper;
using AdvancedDependencyContainer.Infrastructure;
using AdvancedDependencyContainer.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// 依赖绑定上下文类，内部类
    /// </summary>
    internal class DependencyBindContext : IDependencyBindContext
    {
        public IIoCKernel IoCKernel { get; set; }
        /// <summary>
        /// 应用app.config配置文件中的自定义section-> dependencyConfiguration节作为绑定依据
        /// </summary>
        public void UseAppConfiguration(string sectionName)
        {
            try
            {
                var section = ConfigurationManager.GetSection(sectionName).CastTo<DependencyComponentAppConfiguration>();
                if (section != null && section.Dependency.Count > 0)
                {
                    foreach (AppCompositionCollection component in section.Dependency)
                    {
                        foreach (AppComponentElement element in component)
                        {
                            if (string.IsNullOrEmpty(component.Provider))
                            {
                                continue;
                            }
                            else
                            {
                                var assembly = Assembly.Load(component.Provider);
                                if (string.IsNullOrEmpty(element.Contract.Name)
                                    || string.IsNullOrEmpty(element.Contract.Location)
                                    || string.IsNullOrEmpty(element.Realizer.Name)
                                    || string.IsNullOrEmpty(element.Realizer.Location)) continue;
                                var key = string.Format("{0}.{1}.{2}", component.Provider, element.Contract.Location, element.Contract.Name);
                                var val = string.Format("{0}.{1}.{2}", component.Provider, element.Realizer.Location, element.Realizer.Name);
                                IoCKernel.Bind(assembly.GetType(key)).To(assembly.GetType(val));
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return;
            }
        }
        /// <summary>
        /// 应用xxxx.cfg文件作为依赖绑定依据
        /// 参数<see cref="string"/> path 为配置文件路径(含文件名)，
        /// </summary>
        /// <param name="fileName"></param>
        public void UseXmlConfiguration(string fileName)
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;
            if (File.Exists(path))
            {
                var config = XmlUtil.Deserialize<DependencyComponentXmlConfiguration>(path);
                if(config != null)
                {
                    foreach (var item in config.Dependency.Assemblies)
                    {
                        {
                            foreach (var element in item.Binds.BindElements)
                            {
                                var key = string.Format("{0}.{1}", item.Provider, element.Contract);
                                var val = string.Format("{0}.{1}", item.Provider, element.Realization);
                                try
                                {
                                    var assembly = Assembly.Load(item.Provider);
                                    IoCKernel.Bind(assembly.GetType(key)).To(assembly.GetType(val));
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            else//文件不存在时自动创建模板文件
            {
                var xmlString = XmlUtil.Serializer(new DependencyComponentXmlConfiguration
                {
                    Dependency = new XmlAssemblyCollection
                    {
                        Assemblies = new List<XmlAssemblyElement>
                        {
                            new XmlAssemblyElement
                            {
                                Provider ="Write assembly name here...",
                                Binds = new XmlBindCollection
                                {
                                    BindElements = new List<XmlBindElement>
                                    {
                                        new XmlBindElement
                                        {
                                            Contract = "Write interface location and name here...",
                                            Realization = "Write realization location and name here..."
                                        },
                                        new XmlBindElement
                                        {
                                            Contract = "Write interface location and name here...",
                                            Realization = "Write realization location and name here..."
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                });
                var buffer = Encoding.Default.GetBytes(xmlString);
                using (var stream = File.Create(path))
                {
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Close();
                }
            }
        }
    }
}
