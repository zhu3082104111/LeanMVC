/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPartInfoRepository.cs
// 文件功能描述：
//          零件信息的Repository接口类
//      
// 修改履历：2013/12/12 陈健 新建
// 修改履历：2013/12/16 宋彬磊  添加（产品零件查询（子查询画面专用））
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 零件信息的Repository接口类
    /// </summary>
    public interface IPartInfoRepository : IRepository<PartInfo>
    {
        /// <summary>
        /// 成品库入库履历详细手动登录根据产品ID生成产品名称 陈健
        /// </summary>
        /// <param name="partAbbrevi">产品略称</param>
        /// <returns>产品名称</returns>
        List<PartInfo> GetFinRecordProductInfoById(string partAbbrevi);

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        IEnumerable<VM_ProdAndPartInfo4Sel> GetProdAndPartInfo4Sel(VM_ProdAndPartInfo4Sel searchConditon, Paging paging);

        /// <summary>
        /// 成品库出库履历详细画面 根据零件略称获得其ID 陈健
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>数据集合</returns>
        IEnumerable<PartInfo> GetPartID(string partAbbrevi);
    }
}
