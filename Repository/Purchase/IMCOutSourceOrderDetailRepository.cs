/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IpurchaseOrderListRepository.cs
// 文件功能描述：产品外购单一览画面的Repository接口
//      
// 修改履历：2013/10/28 刘云 新建
/*****************************************************************************/
using Model;
using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 产品外购单一览画面的Repository接口
    /// </summary>
    public interface IMCOutSourceOrderDetailRepository : IRepository<MCOutSourceOrderDetail>
    {
        /// <summary>
        /// 取得产品外购单一览画面的显示信息
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns>产品外购单信息List</returns>
        IEnumerable GetPurchaseOrderListInfoByPage(VM_PurchaseOrderListForSearch searchConditon, Paging paging);

        /// <summary>
        /// 对外购单的删除处理（外购单详细表的处理）
        /// </summary>
        /// <param name="detail">外购详细实体</param>
        /// <param name="delUID">删除人UserID</param>
        /// <param name="delDate">删除时间</param>
        /// <returns>删除处理结果</returns>
        bool Delete(MCOutSourceOrderDetail detail, string delUID, DateTime delDate);


        /// <summary>
        /// 更新可以修改的数据
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        bool UpdateDetail(MCOutSourceOrderDetail purchaseOrder,string uId);
        



        #region  关联外购指示表
        /*
        //修改外购指示表 删除
        bool EditInstrucDel(string customerOrderID, string customerOrderDetailID, string productPartID);
        //修改外购指示表 批准
        bool EditInstrucApp(string customerOrderID, string customerOrderDetailID, string productPartID);
        */
        #endregion


        IEnumerable<MCOutSourceOrderDetail> GetMCOutSourceOrderDetailForListAsc(MCOutSourceOrderDetail mcOutSourceOrderDetail);

        IEnumerable<MCOutSourceOrderDetail> GetMCOutSourceOrderDetailForListDesc(MCOutSourceOrderDetail mcOutSourceOrderDetail);

        MCOutSourceOrderDetail SelectMCOutSourceOrderDetail(MCOutSourceOrderDetail mcOutSourceOrderDetail);

        //入库登录时修改外购单明细实际入库数量
        bool UpdateMCOutSourceOrderDetailActColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail);

        //入库履历删除时修改外购单明细实际入库数量
        bool UpdateMCOutSourceOrderDetailForDelActColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail);

        //入库修改时修改外购单明细实际入库数量
        bool UpdateMCOutSourceOrderDetailColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail);

    }
}
