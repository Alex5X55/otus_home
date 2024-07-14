using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    static class Common
    {
        public static bool ComapreLists<T>(IList<T> par1, IList<T> par2)
        {
            bool _areEqual = false;
            if (par2.Count == par1.Count)
            {
                var filteredSequence = par2.Where(x => par1.Contains(x));
                if (filteredSequence.Count() == par2.Count)
                {
                    _areEqual = true;
                }
            }
            return _areEqual;
        }
    }
}
