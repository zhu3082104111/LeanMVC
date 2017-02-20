/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProductWarehouseDetailRepository.cs
// 文件功能描述：成品交仓详细资源库接口类
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace Repository
{
    public interface IProductWarehouseDetailRepository : IRepository<ProductWarehouseDetail>
    {
        /// <summary>
        /// 根据成品交仓单号获取详细信息
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns></returns>
        VM_ProductWarehouseDetailHeadData GetDetailData(string productWarehouseID);

        /// <summary>
        /// 更新或新增记录
        /// 根据有无行号判断更新还是新增
        /// </summary>
        /// <param name="warehouseDetail">记录</param>
        /// <returns></returns>
        bool AddOrUpdate(ProductWarehouseDetail warehouseDetail);

        /// <summary>
        /// 删除交仓单详细某条记录
        /// </summary>
        /// <param name="warehouseDetail">交仓单详细</param>
        /// <returns></returns>
        bool Delete(ProductWarehouseDetail warehouseDetail);

        /// <summary>
        /// 获取某一交仓单号的最大行号
        /// </summary>
        /// <param name="productWarehouseId"></param>
        /// <returns></returns>
        int GetMaxLineNo(string productWarehouseId);

        /// <summary>
        /// 成品库入库手动登录检索计划单号是否存在 陈健
        /// </summary>
        /// <param name="productWarehouseID">计划单号</param>
        /// <returns>数据集合</returns>
        IQueryable<ProductWarehouseDetail> GetFinInRecordPlanID(string planID);

        /// <summary>
        /// 成品库入库手动登录检索检验单号是否存在 陈健
        /// </summary>
        /// <param name="productCheckID">检验单号</param>
        /// <returns>数据集合</returns>
        IQueryable<ProductWarehouseDetail> GetFinInRecordProductCheckID(string productCheckID);

        /// <summary>
        /// 成品库入库手动登录检索物料编号是否存在 陈健
        /// </summary>
        /// <param name="orderProductID">物料编号</param>
        /// <returns>数据集合</returns>
        IQueryable<ProductWarehouseDetail> GetFinInRecordOrProductID(string orderProductID);

        /// <summary>
        /// 成品库入库手动登录检索成品交仓单号+批次号+计划单号+检验单号+装配小组+物料编号+规格型号是否存在 陈健
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="planID">计划单号</param>
        /// <param name="productCheckID">检验单号</param>
        /// <param name="teamID">装配小组</param>
        /// <param name="orderProductID">产品ID</param>
        /// <param name="productSpecification">规格型号</param>
        /// <returns>交仓详细数据集合</returns>
        IQueryable<ProductWarehouseDetail> GetFinInRecordDetail(string productWarehouseID, string planID, string productCheckID,
             string teamID, string orderProductID, string productSpecification);
    }
}
