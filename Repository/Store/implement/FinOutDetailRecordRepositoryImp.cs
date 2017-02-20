/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutDetailRecordRepositoryImp.cs
// 文件功能描述：
//          内部成品库出库履历详细的Repository接口的实现
//      
// 修改履历：2013/11/24 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;
using Extensions;
using Model.Store;

namespace Repository
{
    /// <summary>
    /// 内部成品库出库履历详细的Repository接口的实现
    /// </summary>
    public class FinOutDetailRecordRepositoryImp : AbstractRepository<DB, FinOutDetailRecord>, IFinOutDetailRecordRepository
    {
        /// <summary>
        /// 更新出库履历详细表
        /// </summary>
        /// <param name="finOutDetailRecord">更新数据</param>
        /// <returns>true</returns>
        public bool UpdateInFinOutDetailRecord(FinOutDetailRecord finOutDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_OUT_DETAIL_RECORD set QTY={0},PACK_PRE_PIECE_QTY={1},PACK_PIECE_QTY={2},FRAC_QTY={3},NOTAX_AMT={4},UPD_USR_ID={5},UPD_DT={6} where INER_FIN_OUT_ID={7} and CLN_ODR_ID={8} and CLN_ODR_DTL={9} and ORD_PDT_ID={10} and BTH_ID={11}",
                finOutDetailRecord.Quantity, finOutDetailRecord.PackPrePieceQuantity, finOutDetailRecord.PackPieceQuantity, finOutDetailRecord.FracQuantity, finOutDetailRecord.NotaxAmt,
                finOutDetailRecord.UpdUsrID,finOutDetailRecord.UpdDt,
                finOutDetailRecord.InerFinOutID, finOutDetailRecord.ClientOrderID, finOutDetailRecord.ClientOrderDetail, finOutDetailRecord.OrdPdtID, finOutDetailRecord.BatchID);
        }

        /// <summary>
        /// 获得内部成品送货单画面数据
        /// </summary>
        /// <param name="inerFinOutID">内部成品送货单号</param>
        /// <param name="clientId">客户订单号</param>
        /// <param name="OPdtId">产品ID</param>
        /// <param name="batchId">批次号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutPrintIndexForTableShow> GetFinOutPrintIndexByIdWithPaging(string inerFinOutID, string clientId, string OPdtId, string batchId, Paging page)
        {
            //出库履历详细表
            IQueryable<FinOutDetailRecord> endDList = base.GetList().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //零件信息表
            IQueryable<PartInfo> endPList = base.GetList<PartInfo>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //送货单号
            if (!String.IsNullOrEmpty(inerFinOutID))
            {
                endDList = endDList.Where(u => u.InerFinOutID.Equals(inerFinOutID));
            }
            //客户订单号
            if (!String.IsNullOrEmpty(clientId))
            {
                endDList = endDList.Where(u => (u.ClientOrderID+"/"+u.ClientOrderDetail).Equals(clientId));
            }
           
            //产品略称
            if (!String.IsNullOrEmpty(OPdtId))
            {
                endPList = endPList.Where(u => u.PartId.Equals(OPdtId));
            }
            //批次号
            if (!String.IsNullOrEmpty(batchId))
            {
                endDList = endDList.Where(u => u.BatchID.Equals(batchId));
            }

            IQueryable<VM_storeFinOutPrintIndexForTableShow> query = from o in endDList
                                                                     join op in endPList on o.OrdPdtID equals op.PartId
                                                                     select new VM_storeFinOutPrintIndexForTableShow
                                                                       {
                                                                           ClientOrderID = o.ClientOrderID+"/"+o.ClientOrderDetail,//订单号
                                                                           OrdPdtID = op.PartAbbrevi,//物料编号
                                                                           ProductName = op.PartName,  //物料名称
                                                                           Quantity = o.Quantity,  //数量
                                                                           PackPrePieceQuantity = o.PackPrePieceQuantity, //每件数量
                                                                           PackPieceQuantity = o.PackPieceQuantity,  //件数
                                                                           FracQuantity = o.FracQuantity, //零头
                                                                          
                                                                           Remarks = o.Remarks,//备注
                                                                       };

            page.total = query.Count();
            IEnumerable<VM_storeFinOutPrintIndexForTableShow> result = query.ToPageList<VM_storeFinOutPrintIndexForTableShow>("OrdPdtID asc", page);
            return result;

        }

        /// <summary>
        /// 出库履历详细表添加数据判断
        /// </summary>
        /// <param name="inerFinOutID">送货单号</param>
        /// <param name="outRecordList">出库履历数据集合</param>
        /// <param name="i">行参数</param>
        /// <returns>出库履历详细添加判断数据集合</returns>
        public IQueryable<FinOutDetailRecord> GetFinOutRecordDetailList(string inerFinOutID, Dictionary<string, string>[] outRecordList, int i)
        {
            var clientOrder = outRecordList[i]["ClientOrderID"].Split('/');
            var a = clientOrder[0];
            var b = clientOrder[1];
            var c = outRecordList[i]["OrdPdtID"];
            var d = outRecordList[i]["BatchID"];

            return base.GetList().Where(h => h.InerFinOutID.Equals(inerFinOutID) && h.ClientOrderID.Equals(a) &&
                h.ClientOrderDetail.Equals(b) && h.OrdPdtID.Equals(c) &&
                h.BatchID.Equals(d) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }
    }
}
