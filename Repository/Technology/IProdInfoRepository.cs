using Extensions;
using Model;
using Model.Technology;
using System.Collections.Generic;

namespace Repository
{
    public interface IProdInfoRepository : IRepository<ProdInfo>
    {
        /// <summary>
        /// 根据产品略称获取产品ID
        /// </summary>
        /// <param name="prodAbbrev">产品略称</param>
        /// <returns>产品ID</returns>
        /// 创建者：朱静波
        string GetProduceId(string prodAbbrev);

        /// <summary>
        /// 根据产品ID获取产品略称
        /// </summary>
        /// <param name="produceId">产品ID</param>
        /// <returns>产品略称</returns>
        /// 创建者：朱静波
        string GetProdAbbrev(string produceId);

        /// <summary>
        /// 根据产品ID获取产品单价和估价
        /// </summary>
        /// <param name="produceId"></param>
        /// <returns></returns>
        ProdInfo GetProdInfoById(string productID);


        /// <summary>
        /// 获取表 PD_PROD_INFO 查询记录
        /// </summary>
        /// <param name="paraPIFSTPI">VM_ProdInfoForSearchTableProdInfo 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_ProdInfoForTableProdInfo 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_ProdInfoForTableProdInfo> GetProdInfoListRepository(VM_ProdInfoForSearchTableProdInfo paraPIFSTPI, Paging paraPage);
    
    } //end IProdInfoRepository
}
