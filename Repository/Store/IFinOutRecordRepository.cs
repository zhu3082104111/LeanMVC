/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinOutRecordRepository.cs
// 文件功能描述：
//          内部成品库出库履历的Repository接口类
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
    /// 内部成品库出库履历的Repository接口类
    /// </summary>
    public interface IFinOutRecordRepository : IRepository<FinOutRecord>
    {
        /// <summary>
        /// 获得待出库一览画面数据（不用）
        /// </summary>
        /// <param name="finOutStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutStoreForTableShow> GetFinOutStoreWithPaging(VM_storeFinOutStoreForSearch finOutStore, Paging paging);

        /// <summary>
        /// 获得出库履历一览画面数据
        /// </summary>
        /// <param name="finOutRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutRecordForTableShow> GetFinOutRecordWithPaging(VM_storeFinOutRecordForSearch finOutRecord, Paging paging);

        /// <summary>
        /// 出库履历一览删除出库履历详细表里数据
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        bool updateFinOutRecordDetail(FinOutDetailRecord i);

        /// <summary>
        /// 出库履历一览删除出库履历表里数据
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        bool updateFinOutRecord(FinOutRecord i);

        /// <summary>
        /// 获得出库履历详细画面数据
        /// </summary>
        /// <param name="inerFinOutID">送货单号</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据集合</returns>
        IEnumerable<VM_storeFinOutRecordDetailForTableShow> GetFinOutRecordDetailByIdWithPaging(string inerFinOutID, string uId,Paging page);

        /// <summary>
        /// 查询出库履历表备注是否修改
        /// </summary>
        /// <param name="finOutRecord">查询数据</param>
        /// <returns>true</returns>
        bool UpdateMRemark(VM_storeFinOutRecordDetailForTableShow finOutRecord);

        /// <summary>
        /// 更新出库履历表
        /// </summary>
        /// <param name="finOutRecordT">更新数据</param>
        /// <returns>true</returns>
        bool UpdateInFinOutRecord(FinOutRecord finOutRecordT);
        IEnumerable<VM_storeFinOutPrintForTableShow> GetFinOutPrintWithPaging(VM_storeFinOutPrintForSearch finOutPrint, Paging paging);

        /// <summary>
        /// 出库履历表添加判断
        /// </summary>
        /// <param name="ladiID">提货单号</param>
        /// <returns>出库履历表添加判断数据集合</returns>
        IQueryable<FinOutRecord> GetFinOutRecordLadiID(string ladiID);

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        IEnumerable<VM_ProdAndPartInfo> GetProdAndPartInfo(VM_ProdAndPartInfo searchConditon, Paging paging);
    }
}
