using AdvancedDependencyContainer.Interface;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// 分析类型，提供程序
    /// </summary>
    internal class DefualtDITypeAnalyticalProivder : IDITypeAnalyticalProvider
    {
        /// <summary>
        /// 创建类型分析类实例
        /// </summary>
        /// <returns></returns>
        public IDITypeAnalytical CreateDITypeAnalaytical()
        {
            return new DITypeAnalytical();
        }
    }
}
