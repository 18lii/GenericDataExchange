using AdvancedDependencyContainer.Configurations;
using AdvancedDependencyContainer.Entities;
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
    internal sealed class DependencyBindContext : IDependencyBindContext
    {
        public DependencyBindContext()
        {
            this.IoCKernel = new IoCKernel();
        }
        public IoCKernel IoCKernel { get; set; }

        /// <summary>
        /// 泛型绑定，以<see cref="{T}"/>类型作为依赖注入契约
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDependencyBindContext Bind<T>()
        {
            IoCKernel.Bind<T>();
            return this;
        }
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为注入契约
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IDependencyBindContext Bind(Type type)
        {
            IoCKernel.Bind(type);
            return this;
        }
        /// <summary>
        /// 泛型绑定，以<see cref="{U}"/>类型作为依赖注入实现
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        public IDependencyBindContext To<U>(object[] args = null) where U : class
        {
            IoCKernel.To<U>(args);
            return this;
        }
        /// <summary>
        /// 类型绑定，以<see cref="Type"/>类型作为依赖注入实现
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IDependencyBindContext To(Type type, object[] args = null)
        {
            IoCKernel.To(type, args);
            return this;
        }
        public void UseConfiguration(DependencyConfigurationOption option, string keyWord)
        {
            switch (option)
            {
                case DependencyConfigurationOption.App:
                    UseAppConfiguration(keyWord);
                    break;
                case DependencyConfigurationOption.Xml:
                    UseXmlConfiguration(keyWord);
                    break;
            }
        }
        /// <summary>
        /// 应用app.config配置文件中的自定义section-> dependencyConfiguration节作为绑定依据
        /// </summary>
        private void UseAppConfiguration(string sectionName)
        {
            try
            {
                DependencyComponentAppConfiguration section;
                section = ConfigurationManager.GetSection(sectionName).CastTo<DependencyComponentAppConfiguration>();
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
                                var key = string.Format("{0}.{1}", element.Contract.Location, element.Contract.Name);
                                var val = string.Format("{0}.{1}", element.Realizer.Location, element.Realizer.Name);
                                IoCKernel.Bind(assembly.GetType(key)).To(assembly.GetType(val));
                            }
                        }
                    }
                }
                else
                {
                    section = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection(sectionName).CastTo<DependencyComponentAppConfiguration>();
                }
            }
            catch { }
        }
        /// <summary>
        /// 应用xxxx.cfg文件作为依赖绑定依据
        /// 参数<see cref="string"/> path 为配置文件路径(含文件名)，
        /// </summary>
        /// <param name="fileName"></param>
        private void UseXmlConfiguration(string fileName)
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
                                var key = string.Format("{0}.{1}", element.Contract.Location, element.Contract.Name);
                                var val = string.Format("{0}.{1}", element.Realizer.Location, element.Realizer.Name);
                                
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
                FileUtil.FileCreate(Encoding.Default.GetBytes(xmlString), path);
            }
        }
    }
}
