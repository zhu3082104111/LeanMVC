/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISupplierOrderDetailRepository.cs
// 文件功能描述：
//          外协调度单明细表的Repository接口类
//      
// 修改履历：2013/11/22 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace Repository
{
    /// <summary>
    /// 外协调度单明细表的Repository接口类
    /// </summary>
    public interface ISupplierOrderDetailRepository:IRepository<MCSupplierOrderDetail>
    {        
        /// <summary>
        /// 获取明细表信息
        /// </summary>
        /// <param name="supplierOrderId">调度单号</param>
        /// <param name="page">分页排序</param>
        /// <returns></returns>
        IEnumerable<VM_SupplierOrder> GetSupplierOrderDetailByIdForSearch(string supplierOrderId,Paging page);
       
        /// <summary>
        /// 获取ID相对应的数据
        /// </summary>
        /// <param name="supplierOrderId">调度单号</param>
        /// <returns></returns>
        VM_SupplierOrderInfor GetSupplierOrderDetailInforById(string supplierOrderId);

        /// <summary>
        ///  修改保存数据
        /// </summary>
        /// <param name="supplierOrder">对应实体</param>
        /// <returns></returns>
        bool UpdateSupplierOrderDetail(MCSupplierOrderDetail supplierOrder);

        /// <summary>
        /// 根据其中之一的主键获取与改主键相等的所有实体集合
        /// </summary>
        /// <param name="supplierOrderId">多主键中的一主键</param>
        /// <returns></returns>
        IQueryable<MCSupplierOrderDetail> GetSupplierOrderDetailList(string supplierOrderId);

        /// <summary>
        /// 删除调度单明细表里的数据
        /// </summary>
        /// <param name="s">将要删除的实体</param>
        /// <returns></returns>
        bool DeleteSupplierOrderDetail(MCSupplierOrderDetail s);

        //Asc取得外协加工调度单详细表List（yc添加）
        IEnumerable<MCSupplierOrderDetail> GetMCSupplierOrderDetailForListAsc(MCSupplierOrderDetail mcSupplierOrderDetail);

        //Desc取得外协加工调度单详细表List（yc添加）
        IEnumerable<MCSupplierOrderDetail> GetMCSupplierOrderDetailForListDesc(MCSupplierOrderDetail mcSupplierOrderDetail);

        //入库登录时修改外协加工调度单详细表实际入库数量（yc添加）
        bool UpdateMCSupplierOrderDetailActColumns(MCSupplierOrderDetail mcSupplierOrderDetail);

        //入库履历删除时修改外协加工调度单详细表实际入库数量(yc添加）
        bool UpdateMCSupplierOrderDetailForDelActColumns(MCSupplierOrderDetail mcSupplierOrderDetail);
    }
}
