using Extensions;
using Model.Technology;
using System.Collections.Generic;

namespace BLL
{
    public interface IProdInfoService
    {
        //根据条件获取表  数据
        IEnumerable<VM_ProdInfoForTableProdInfo> GetProdInfoListService(VM_ProdInfoForSearchTableProdInfo paraPIFSTPI, Paging paraPage);
    }
}
