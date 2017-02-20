/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProdCompositionRepository.cs
// 文件功能描述：成品信息构成表的Repository接口实现
//     
// 修改履历：2013/11/22 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;

namespace Repository
{
    public interface IProdCompositionRepository : IRepository<ProdComposition>
    {
        #region 物料分解功能 C：梁龙飞
        /// <summary>
        /// 分解一个产品，返回此产品的分解结构
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        IEnumerable<ProdComposition> DecomposeProduct(string productID);

        /// <summary>
        /// 从技术资料中生成物料分解信息，相当于初次排产的数据。
        /// C：梁龙飞：存在测试代码
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        IEnumerable<MaterialDecompose> GeneralMatDecompose(string orderID, string orderDetail, string version);

        #endregion

        /// <summary>
        /// 根据产品型号捞取(名称代码、型号、批次号、数量、备注)
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <param name="clientOrderID">客户订单号</param>
        /// <param name="clientOrderDetail">客户订单详细</param>
        /// <returns>总装调度单详细集</returns>
        IEnumerable<VM_NewBillDataGrid> GetNewBillDataGrid(string id, string clientOrderID, string clientOrderDetail);

        /// <summary>
        /// 根据产品ID取得父工序ID为空的成品构成表记录的子工序ID
        /// </summary>
        /// <param name="id">产品ID</param>
        /// <returns>工序ID or ""</returns>
        String GetSubProcID(string id);
    }
}
