/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinOutDetailRecordRepository.cs
// 文件功能描述：
//          内部成品库出库履历详细的Repository接口类
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
    /// 内部成品库出库履历详细的Repository接口类
    /// </summary>
    public interface IFinOutDetailRecordRepository : IRepository<FinOutDetailRecord>
    {
        /// <summary>
        /// 更新出库履历详细表
        /// </summary>
        /// <param name="finOutDetailRecord">更新数据</param>
        /// <returns>true</returns>
        bool UpdateInFinOutDetailRecord(FinOutDetailRecord finOutDetailRecord);

        /// <summary>
        /// 获得内部成品送货单画面数据
        /// </summary>
        /// <param name="inerFinOutID">内部成品送货单号</param>
        /// <param name="clientId">客户订单号</param>
        /// <param name="OPdtId">产品ID</param>
        /// <param name="batchId">批次号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        IEnumerable<VM_storeFinOutPrintIndexForTableShow> GetFinOutPrintIndexByIdWithPaging(string inerFinOutID, string clientId, string OPdtId, string batchId, Paging page);

        /// <summary>
        /// 出库履历详细表添加数据判断
        /// </summary>
        /// <param name="inerFinOutID">送货单号</param>
        /// <param name="outRecordList">出库履历数据集合</param>
        /// <param name="i">行参数</param>
        /// <returns>出库履历详细添加判断数据集合</returns>
        IQueryable<FinOutDetailRecord> GetFinOutRecordDetailList(string inerFinOutID, Dictionary<string, string>[] outRecordList, int i);
    }
}
