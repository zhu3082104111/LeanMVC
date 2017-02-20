/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IDeliveryOrderService.cs
// 文件功能描述：
//          送货单相关的Service接口
//      
// 修改履历：2013/12/10 刘云 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 送货单画面的Service接口
    /// </summary>
    public interface IDeliveryOrderService
    {
        /// <summary>
        /// 送货单一览画面的显示数据的取得函数
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页情报</param>
        /// <returns>送货单情报List</returns>
        IEnumerable GetDeliveryOrderListBySearchByPage(VM_DeliveryOrderListForSearch searchCondition, Paging paging);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">登录用户UserID</param>
        /// <returns>删除结果</returns>
        [TransactionAop]
        bool Delete(string deliveryOrderID, string uId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryOrderList">外购单号List</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>删除结果</returns>
        [TransactionAop]
        bool Delete(List<String> deliveryOrderList, string uId);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>审核结果</returns>
        [TransactionAop]
        bool Audit(string deliveryOrderID, string uId);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="deliveryOrderList">送货单号List</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>审核结果</returns>
        [TransactionAop]
        bool Audit(List<String> deliveryOrderList, string uId);

        /// <summary>
        /// 获取送货单详细信息
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <returns>送货单详细情报List</returns>
        IEnumerable GetDeliveryOrderDetailBySearchById(VM_DeliveryOrderForShow searchConditon);

        /// <summary>
        /// 导入显示的数据
        /// </summary>
        /// <param name="orderNo">外购（外协）计划单号</param>
        /// <returns>外购（外协）信息</returns>
        List<VM_DeliveryOrderForTableShow> GetImportInfo(string orderNo);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <param name="orderList"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        [TransactionAop]
        string Add(VM_DeliveryOrderForShow deliveryOrder, Dictionary<string, string>[] orderList, string uID);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deliveryOrder">筛选条件</param>
        /// <param name="orderList">筛选条件</param>
        /// <param name="uID"></param>
        /// <returns></returns>
        [TransactionAop]
        string Update(VM_DeliveryOrderForShow deliveryOrder, Dictionary<string, string>[] orderList, string uID);        

        /// <summary>
        /// 得到送货单号对应数据
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <returns></returns>
        MCDeliveryOrder getDeliveryOrderById(string deliveryOrderID);

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        string printInfo(string deliveryOrderID, string uId);

    }
}