using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BLL.Common
{
    public interface IAutoSearchService
    {
        List<Searcher> GetBySearcher(Searcher searcher);

    }
}
