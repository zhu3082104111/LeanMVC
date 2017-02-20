/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinOutStoreService.cs
// 文件功能描述：
//          内部成品库出库相关画面Service接口类
//
// 修改履历：2013/12/02 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model.Store;
using Model;
using Repository;

namespace BLL
{
    /// <summary>
    /// 内部成品库出库相关画面Service接口类
    /// </summary>
    public interface IFinOutStoreService
    {
        /// <summary>
        /// 获得待出库一览画面数据
        /// </summary>
        /// <param name="finOutStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutStoreForTableShow> GetFinOutStoreForSearch(VM_storeFinOutStoreForSearch finOutStore, Paging paging);

        /// <summary>
        /// 获得出库履历一览画面数据
        /// </summary>
        /// <param name="finOutRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutRecordForTableShow> GetFinOutRecordForSearch(VM_storeFinOutRecordForSearch finOutRecord, Paging paging);

        /// <summary>
        /// 出库履历一览删除
        /// </summary>
        /// <param name="list">删除的数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <returns>true</returns>
        bool DeleteFinOutStore(List<string> list, string uId);

        /// <summary>
        /// 获得出库履历详细画面（表示）
        /// </summary>
        /// <param name="inerFinOutID">成品送货单号</param>
        /// <param name="uId">登录人员</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutRecordDetailForTableShow> GetFinOutRecordDetailById(string inerFinOutID, string uId,Paging page);

        /// <summary>
        /// 出库履历详细画面登录保存（添加，修改）
        /// </summary>
        /// <param name="finOutRecord">更新的数据集合</param>
        /// <param name="pageFlg">页面状态标识</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="outRecordList">更新的数据集合</param>
        /// <returns>提示消息</returns>
        [TransactionAop]
        string FinOutRecordForLogin(VM_storeFinOutRecordDetailForTableShow finOutRecord, string pageFlg, string uId, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 数据更新保存(出库详细编辑更新成品库出库履历表及其详细表)
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        [TransactionAop]
        void FinOutRecordForELogin(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 获得内部成品送货单画面数据
        /// </summary>
        /// <param name="inerFinOutID">内部成品送货单号</param>
        /// <param name="clientId">客户订单号</param>
        /// <param name="OPdtId">产品ID</param>
        /// <param name="batchId">批次号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutPrintIndexForTableShow> GetFinOutPrintIndexById(string inerFinOutID,string clientId,string OPdtId,string batchId, Paging page);

        /// <summary>
        /// 获得内部成品送货单打印选择画面数据
        /// </summary>
        /// <param name="finOutPrint">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutPrintForTableShow> GetFinOutPrintForSearch(VM_storeFinOutPrintForSearch finOutPrint, Paging paging);

        /// <summary>
        /// 成品库添加出库履历详细数据
        /// </summary>
        /// <param name="finOutRecord">添加数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        [TransactionAop]
        void FinOutForLoginAddOutRecord(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 成品库出库登录修改仓库表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        [TransactionAop]
        void FinOutForLoginUpdateMaterial(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 成品库出库登录更新仓库预约表及预约详细表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        [TransactionAop]
        void FinOutForLoginUpdateReserve(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 成品库出库登录修改批次别库存表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        [TransactionAop]
        void FinOutForLoginUpdateBthStockList(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList);

        /// <summary>
        /// 出库详细新增状态，根据输入的零件略称自动生成零件信息
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        List<PartInfo> GetFinOutRecordPdtInfoById(string partAbbrevi);

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        IEnumerable<VM_ProdAndPartInfo> GetProdAndPartInfo(VM_ProdAndPartInfo searchConditon, Paging paging);
    }
}
