/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProductWarehouseRepository.cs
// 文件功能描述：成品交仓资源库接口类
// 
// 创建标识：20131123 杜兴军
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Store;

namespace Repository
{
    public interface IProductWarehouseRepository:IRepository<ProductWarehouse>
    {
        /// <summary>
        /// 获取成品交仓单 一览数据
        /// </summary>
        /// <param name="search">检索条件</param>
        /// <param name="paging">检索条件</param>
        /// <returns></returns>
        IEnumerable<VM_ProductWarehouseShow> GetWarehouseShowByPage(VM_ProductWarehouseSearch search,Paging paging);
         /// <summary>
        /// 成品待交仓一览画面读取数据  潘军
        /// </summary>
        /// <param name="sezrch"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable GetPWaitingWarehouseListBySearchByPage(VM_PWaitingWarehouseListForSearch search, Paging paging);


        /// <summary>
        /// 添加或更新成品交仓单信息
        /// </summary>
        /// <param name="warehouse">成品交仓单</param>
        /// <returns></returns>
        bool AddOrUpdateWarehouse(ProductWarehouse warehouse);

        /// <summary>
        /// 成品库入库登录修改成品交仓单表交仓状态 陈健
        /// </summary>
        /// <param name="productWarehouse">成品交仓单号</param>
        /// <returns></returns>
        bool updateInProductWarehouse(ProductWarehouse productWarehouse);

        /// <summary>
        /// 删除指定交仓单信息
        /// </summary>
        /// <param name="warehouseIds">交仓单号</param>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        bool DeleteWarehouse(string warehouseIds,string userId);

        /// <summary>
        /// 暂时无用
        /// 删除交仓单及详细
        /// </summary>
        /// <param name="warehouseList">成品交仓单集</param>
        /// <param name="warehouseDetailList">成品交仓单详细集</param>
        /// <returns></returns>
        bool DeleteWarehouse(List<ProductWarehouse> warehouseList, List<ProductWarehouseDetail> warehouseDetailList);

        /// <summary>
        /// 成品库入库手动登录检索成品交仓单号是否存在 陈健
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>数据集合</returns>
        IQueryable<ProductWarehouse> GetFinInRecordPdtWhID(string productWarehouseID);

        /// <summary>
        /// 成品库入库手动登录检索批次号是否存在 陈健
        /// </summary>
        /// <param name="batchI">批次号</param>
        /// <returns>数据集合</returns>
        IQueryable<ProductWarehouse> GetFinInRecordBtchID(string batchID);

        /// <summary>
        /// 成品库入库详细登录根据交仓单号查询 陈健
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>数据集合</returns>
        VM_StoreFinInRecordForDetailShow GetFinInRecordInfosById(string productWarehouseID);


    }
}
