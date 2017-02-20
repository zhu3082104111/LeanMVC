/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinInRecordRepository.cs
// 文件功能描述：
//          内部成品库入库履历的Repository接口类
//      
// 修改履历：2013/11/24 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Store;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 内部成品库入库履历的Repository接口类
    /// </summary>
    public interface IFinInRecordRepository : IRepository<FinInRecord>
    {
        /// <summary>
        /// 获得待入库一览画面数据
        /// </summary>
        /// <param name="finInStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInStoreForTableShow> GetFinInStoreWithPaging(VM_StoreFinInStoreForSearch finInStore, Paging paging);

        /// <summary>
        /// 获得入库履历一览画面数据
        /// </summary>
        /// <param name="finInRecord">画面数据集合</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForTableShow> GetFinInRecordWithPaging(VM_StoreFinInRecordForSearch finInRecord, Paging paging);

        /// <summary>
        /// 获得入库履历详细画面数据（入库跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页跳转</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordByIdWithPaging(string productWarehouseID, Paging page);

        /// <summary>
        /// 入库履历表数据伪删除
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        bool updateFinInRecord(FinInRecord i);

        /// <summary>
        /// 入库履历详细表数据伪删除
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        bool updateFinInRecordDetail(FinInDetailRecord i);

        /// <summary>
        /// 获得入库详细画面数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordDetailByIdWithPaging(string productWarehouseID, Paging page);

        /// <summary>
        /// 入库履历表添加判断
        /// </summary>
        /// <param name="planID">交仓单号</param>
        /// <returns>入库履历数据集合</returns>
        IQueryable<FinInRecord> GetFinInRecordPlanID(string planID);

        /// <summary>
        /// 更新入库履历表备注
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <returns>true</returns>
        bool UpdateInFinInRecord(FinInRecord finInRecord);

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForTableShow GetFinInRecordInfoById(string productWarehouseID);

    }
}
