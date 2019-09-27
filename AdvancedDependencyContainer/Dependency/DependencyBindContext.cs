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
                    foreach (AppComponentCollection component in section.Dependency)
                    {
                        foreach (AppComponentElement element in component)
                        {
                            if (string.IsNullOrEmpty(component.Provider))
                            {
                                return;
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
            catch { }
        }
        /// <summary>
        /// 应用xxxx.cfg文件作为依赖绑定依据
        /// 参数<see cref="string"/> path 为配置文件路径(含文件名)，
        /// </summary>
        /// <param name="fileName"></param>
        public void UseXmlConfiguration(string fileName)
        {
            var path = string.Format("{0}\\{1}.cfg", AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                try
                {
                    var dependency = XmlUtil.Deserialize<DependencyComponentXmlConfiguration>(path).Dependency;
                    if (dependency != null)
                    {
                        foreach (var composition in dependency.Compositions)
                        {
                            if (string.IsNullOrEmpty(composition.Provider)) return;
                            foreach (var element in composition.Components)
                            {
                                var assembly = Assembly.Load(composition.Provider);
                                if (string.IsNullOrEmpty(element.Contract.Name)
                                    || string.IsNullOrEmpty(element.Contract.Location)
                                    || string.IsNullOrEmpty(element.Realizer.Name)
                                    || string.IsNullOrEmpty(element.Realizer.Location)) return;
                                var key = string.Format("{0}.{1}.{2}", composition.Provider, element.Contract.Location, element.Contract.Name);
                                var val = string.Format("{0}.{1}.{2}", composition.Provider, element.Realizer.Location, element.Realizer.Name);
                                
                                IoCKernel.Bind(assembly.GetType(key)).To(assembly.GetType(val));
                            }
                        }
                    }
                }
                catch { }
            }
            else//文件不存在时自动创建模板文件
            {
                var xmlString = XmlUtil.Serializer(new DependencyComponentXmlConfiguration
                {
                    Dependency = new XmlCompositionCollection
                    {
                        Compositions = new List<XmlComponentCollection>
                        {
                            new XmlComponentCollection
                            {
                                Provider ="Write assembly name here...",
                                Components = new List<XmlComponentElement>
                                {
                                    new XmlComponentElement
                                    {
                                        Contract = new XmlComponentProperty
                                        {
                                            Name = "Write interface name here...",
                                            Location = "Write interface location here..."
                                        },
                                        Realizer = new XmlComponentProperty
                                        {
                                            Name = "Write realizer name here...",
                                            Location = "Write realizer location here..."
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
