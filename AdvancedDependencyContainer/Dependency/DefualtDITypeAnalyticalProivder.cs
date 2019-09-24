using AdvancedDependencyContainer.Interface;

namespace AdvancedDependencyContainer.Dependency
{
    /// <summary>
    /// 分析类型，提供程序
    /// </summary>
    internal class DefualtDITypeAnalyticalProivder : IDITypeAnalyticalProvider
    {
        public IDITypeAnalytical CreateDITypeAnalaytical()
        {
            return new DITypeAnalytical();
        }
    }
}
