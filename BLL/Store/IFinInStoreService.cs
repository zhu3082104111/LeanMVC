/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinInStoreService.cs
// 文件功能描述：
//          内部成品库入库相关画面Service接口类
//
// 修改履历：2013/11/23 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Store;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    /// <summary>
    /// 内部成品库入库相关画面Service接口类
    /// </summary>
    public interface IFinInStoreService
    {
        /// <summary>
        /// 获得待入库一览画面数据
        /// </summary>
        /// <param name="finInStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInStoreForTableShow> GetFinInStoreForSearch(VM_StoreFinInStoreForSearch finInStore, Paging paging);

        /// <summary>
        /// 获得入库履历一览画面数据
        /// </summary>
        /// <param name="finInRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForTableShow> GetFinInRecordForSearch(VM_StoreFinInRecordForSearch finInRecord, Paging paging);

        /// <summary>
        /// 获得入库履历详细画面数据（入库跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordBySearchById(string productWarehouseID, Paging page);

        /// <summary>
        /// 入库履历详细画面登录保存（添加，修改）
        /// </summary>
        /// <param name="finInRecord">更新的数据集合</param>
        /// <param name="pageFlg">标识页面状态</param>
        /// <param name="editFlg">标识编辑状态</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="inRecordList">更新的数据集合</param>
        /// <returns>提示内容</returns>
        [TransactionAop]
        string FinInRecordForLogin(VM_StoreFinInRecordForDetailShow finInRecord, string pageFlg, string editFlg, string uId, Dictionary<string, string>[] inRecordList);

        /// <summary>
        /// 入库履历一览删除
        /// </summary>
        /// <param name="list">删除的数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <returns>true</returns>
        bool DeleteFinInStore(List<string> list, string uId);

        /// <summary>
        /// 获得入库详细画面数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordDetailById(string productWarehouseID, Paging page);

        /// <summary>
        /// 成品库添加入库履历详细数据
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginAddInRecord(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId);

        /// <summary>
        /// 成品库入库登录修改仓库预约表
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginUpdateReserve(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i,string uId);

        /// <summary>
        /// 成品库入库登录新添加仓库预约详细表数据
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginAddReserveDetails(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId);

        /// <summary>
        /// 成品库入库登录修改仓库表
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginUpdateMaterial(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId);

        /// <summary>
        /// 成品库入库登录修改成品交仓单表交仓状态
        /// </summary>
        /// <param name="finInRecord">修改数据集合</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginUpdateProdWarehouse(VM_StoreFinInRecordForDetailShow finInRecord, string uId);

        /// <summary>
        /// 对输入的数据进行检查并更新(入库详细编辑更新成品库入库履历及详细表)
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInRecordForELogin(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId);

        /// <summary>
        /// 成品库入库登录添加批次别库存表（不用）
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        [TransactionAop]
        void FinInForLoginAddBthStockList(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId);

        /// <summary>
        /// 入库详细手工登录状态，根据输入的零件略称自动生成零件名称（暂时不用）
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        List<PartInfo> GetFinInRecordPdtInfoById(string partAbbrevi);

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForTableShow GetDetailInformation(string productWarehouseID);

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（入库新添加跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForDetailShow GetDetailInformations(string productWarehouseID);
    }
}
