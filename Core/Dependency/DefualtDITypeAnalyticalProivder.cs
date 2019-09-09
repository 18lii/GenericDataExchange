using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dependency
{
    public class DefualtDITypeAnalyticalProivder : IDITypeAnalyticalProvider
    {
        public IDITypeAnalytical CreateDITypeAnalaytical()
        {
            return new DITypeAnalytical();
        }
    }
}
