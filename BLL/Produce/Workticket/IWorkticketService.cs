// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IWorkticketService.cs
// 文件功能描述：工票service接口
// 
// 创建标识：代东泽 20131126
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using Extensions;
using Model.Produce;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace BLL
{
    /// <summary>
    /// 代东泽 20131126
    /// </summary>
    public interface IWorkticketService
    {

        //IEnumerable<VM_AssemSmallBillForTableShow> GetAssemSmallBillsForSearch(VM_AssemSmallBillForSearch bill, Paging paging);

        //VM_AssemSmallBillForDetailShow GetAssemSmallBillDetailInfo(VM_AssemSmallBillForTableShow workticket);

        /// <summary>
        /// 获取大工票一览数据
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemBigBillForTableShow> GetAssemBigBillsForSearch(VM_AssemBigBillForSearch bill, Paging paging);
        /// <summary>
        /// 获取大工票信息
        /// </summary>
        /// <param name="workticket"></param>
        /// <returns></returns>
        VM_AssemBigBillForDetailShow GetAssemBigBillInfo(AssemBill workticket);

        /// <summary>
        /// 获取一个大工票详细信息
        /// </summary>
        /// <param name="workticket"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemBigBillPartForDetailShow> GetAssemBigBillsDetailInfo(AssemBill workticket);

        /// <summary>
        /// 获取大工票对应的所有客户订单
        /// </summary>
        /// <param name="assemBill"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemblyDispatch> GetCustomOrdersForAssemBigBill(AssemBill assemBill);

        /// <summary>
        /// 保存总装大工票
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="bds"></param>
        [TransactionAop]
        void SaveAssemBigBill(AssemBill bill,IEnumerable<AssemBillDetail> bds,IEnumerable<AssemblyDispatch> ads,IList<string> flags );



    }
}
