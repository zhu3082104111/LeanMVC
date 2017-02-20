/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFAScheduleBillService.cs
// 文件功能描述：总装调度单的Service接口
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository;

namespace BLL
{
     public interface IFAScheduleBillService
    {
        /// <summary>
        /// 按条件查询总装调度单信息
        /// </summary>
        /// <param name="inProcessingRate">输入的查询信息</param>
        /// <param name="paging">分页</param>
        /// <returns>总装调度单一览数据</returns>
         IEnumerable<VM_AssemblyDispatch> GetFAScheduleBillSearch(VM_FAScheduleBillForSearch inProcessingRate, Paging paging);

         /// <summary>
         /// 取得总装调度单信息
         /// </summary>
         /// <param name="id">总装调度单ID</param>
         /// <returns>返回的产品ID寄存的是产品略称</returns>
         AssemblyDispatch GetFAScheduleBillByID(String id);

         /// <summary>
         /// 新增总装调度单时，取得调度单详细数据
         /// </summary>
         /// <param name="productId">产品ID</param>
         /// <param name="clientOrderID">客户订单号</param>
         /// <param name="clientOrderDetail">客户订单详细</param>
         /// <returns></returns>
         IEnumerable<VM_NewBillDataGrid> GetNewBillDataGrid(String productId,String clientOrderID,String clientOrderDetail);

         /// <summary>
         /// 取得总装调度单详细表中得数据
         /// </summary>
         /// <returns>产品型号</returns>
         IEnumerable<VM_NewBillDataGrid> GetAssemblyDispatchDetail(String productId);

         /// <summary>
         /// 对选择的总装调度单进行删除
         /// </summary>
         /// <param name="deleList">总装调度单号集</param>
         /// <param name="userId">用户ID</param>
         /// <returns>执行结果</returns>
         [TransactionAop]
         string  DeleteSchedulingBill(List<string> deleList,string userId);

         /// <summary>
         /// 取得自动填充数据
         /// </summary>
         /// <param name="inProcessingRate">如何的查询信息</param>
         /// <returns>查询结果</returns>
         IQueryable<VM_AssemblyDispatch> GetAutoCompleteSearch(VM_FAScheduleBillForSearch inProcessingRate);

         /// <summary>
         /// 生成大工票功能
         /// </summary>
         /// <param name="deleList">总装调度单号集</param>
         /// <param name="assembBillIDList">新增总装大工票号集</param>
         /// <param name="userId">用户ID</param>
         /// <returns>执行结果</returns>
         [TransactionAop]
         string NewFAWorkticket(List<string> deleList,List<string> assembBillIDList, string userId);

         /// <summary>
         /// 删除大工票功能
         /// </summary>
         /// <param name="deleList">总装调度单号集</param>
         /// <returns>执行结果</returns>
         string DelFAWorkticket(List<string> deleList);

         /// <summary>
         /// 更新总装调度单
         /// </summary>
         /// <param name="symbol">添加和编辑标识符</param>
         /// <param name="assemblyTicketId">总装调度单号</param>
         /// <param name="assemblyDispatch">编辑项</param>
         /// <param name="userId">用户ID</param>
         /// <returns>执行结果</returns>
         [TransactionAop]
         string SaveNewAndUpdate(Boolean symbol, string assemblyTicketId, Dictionary<string, string>[] assemblyDispatch, string userId);

         /// <summary>
         /// 根据已输入的调度单信息，查找完整的总装调度单信息
         /// </summary>
         /// <param name="entity">总装调度单实体</param>
         /// <returns></returns>
         AssemblyDispatch GetBasicInformation(AssemblyDispatch entity);
    }
}
