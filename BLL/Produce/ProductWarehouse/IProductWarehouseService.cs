/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProductWarehouseService.cs
// 文件功能描述：成品交仓业务层接口类
// 
// 创建标识：20131125 杜兴军
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    public interface IProductWarehouseService
    {
        /// <summary>
        /// 按条件检索成品交仓单一览页面
        /// </summary>
        /// <param name="search">检索条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_ProductWarehouseShow> GetWarehouseByPage(VM_ProductWarehouseSearch search, Paging paging);
        
        /// <summary>
        /// 按条件检索成品待交仓单一览页面 潘军
        /// </summary>
        /// <param name="search"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable GetPWaitingWarehouseListByPage(VM_PWaitingWarehouseListForSearch search, Paging paging);

        /// <summary>
        /// 根据交仓单号获取详细信息
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <returns></returns>
        VM_ProductWarehouseDetailHeadData GetWarehouseDetail(string productWarehouseID);

        /// <summary>
        /// 获取某一交仓单号的最大行数
        /// </summary>
        /// <param name="productWarehouseId">交仓单号</param>
        /// <returns></returns>
        int GetMaxLineNo(string productWarehouseId);

        /// <summary>
        /// 更新成品交仓单
        /// </summary>
        /// <param name="warehouse">交仓单</param>
        /// <param name="warehouseDetailList">交仓单详细</param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateWarehouse(ProductWarehouse warehouse, List<ProductWarehouseDetail> warehouseDetailList);

        /// <summary>
        /// 删除交仓单详细某条记录
        /// </summary>
        /// <param name="warehouseDetail">交仓单详细信息</param>
        /// <returns></returns>
        [TransactionAop]
        bool DeleteWarehouseDetail(ProductWarehouseDetail warehouseDetail);

        /// <summary>
        /// 删除指定的交仓单
        /// </summary>
        /// <param name="warehouseIds">交仓单号（数组）</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [TransactionAop]
        bool DeleteWarehouse(string warehouseIds,string userId);

        /// <summary>
        /// 添加交仓单数据
        /// </summary>
        /// <param name="warehouse">交仓单</param>
        /// <param name="warehouseDetails">交仓单详细</param>
        /// <returns></returns>
        [TransactionAop]
        bool AddProductWarehouse(ProductWarehouse warehouse, List<ProductWarehouseDetail> warehouseDetails);

        /// <summary>
        /// 修改交仓单状态为列印
        /// </summary>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateWarehouseState(ProductWarehouse warehouse);

    }
}
