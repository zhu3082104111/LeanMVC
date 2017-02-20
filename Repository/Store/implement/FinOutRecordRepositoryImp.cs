/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutRecordRepositoryImp.cs
// 文件功能描述：
//          内部成品库出库履历的Repository接口的实现
//      
// 修改履历：2013/11/24 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Model.Store;
using Extensions;


namespace Repository.Store.implement
{
    /// <summary>
    /// 内部成品库出库履历的Repository接口的实现
    /// </summary>
    class FinOutRecordRepositoryImp : AbstractRepository<DB, FinOutRecord>, IFinOutRecordRepository
    {

        #region 待出库一览画面（不用）

        /// <summary>
        /// 待出库一览画面临时数据
        /// </summary>
        private static IEnumerable<FinOutRecord> list = new List<FinOutRecord>{
            
                new FinOutRecord(){WareHouseID="01",OutMoveCls="01",InerFinOutID="XC",LadiID="000001",WareHouseKpID="S2",Remarks=""},
                new FinOutRecord(){WareHouseID="02",OutMoveCls="02",InerFinOutID="XD",LadiID="000002",WareHouseKpID="U2",Remarks=""},
                new FinOutRecord(){WareHouseID="03",OutMoveCls="03",InerFinOutID="FC",LadiID="000003",WareHouseKpID="S5",Remarks=""},
                new FinOutRecord(){WareHouseID="04",OutMoveCls="04",InerFinOutID="GC",LadiID="000004",WareHouseKpID="R2",Remarks=""},
                new FinOutRecord(){WareHouseID="05",OutMoveCls="05",InerFinOutID="TC",LadiID="000005",WareHouseKpID="SY",Remarks=""},
                new FinOutRecord(){WareHouseID="06",OutMoveCls="06",InerFinOutID="XR",LadiID="000006",WareHouseKpID="S6",Remarks=""},
                new FinOutRecord(){WareHouseID="07",OutMoveCls="07",InerFinOutID="XE",LadiID="000007",WareHouseKpID="S2",Remarks=""},
                new FinOutRecord(){WareHouseID="08",OutMoveCls="08",InerFinOutID="TC",LadiID="000008",WareHouseKpID="I7",Remarks=""}
            
            };

        /// <summary>
        /// 待出库一览画面数据（不用）
        /// </summary>
        /// <param name="finOutStore"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable<VM_storeFinOutStoreForTableShow> GetFinOutStoreWithPaging(VM_storeFinOutStoreForSearch finOutStore, Extensions.Paging paging)
        {
            IQueryable<FinOutRecord> endList = list.AsQueryable<FinOutRecord>();

            if (!String.IsNullOrEmpty(finOutStore.ladiID))
            {
                endList = endList.Where(u => u.LadiID.Contains(finOutStore.ladiID));
            }

            IQueryable<VM_storeFinOutStoreForTableShow> query = from o in endList
                                                                select new VM_storeFinOutStoreForTableShow
                                                                  {
                                                                      ladiID = o.LadiID 
                                                                  };
            paging.total = query.Count();
            IEnumerable<VM_storeFinOutStoreForTableShow> result = query.ToPageList<VM_storeFinOutStoreForTableShow>("ladiID asc", paging);
            return result;

        }

        #endregion

        /// <summary>
        /// 获得出库履历一览画面数据
        /// </summary>
        /// <param name="finOutRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutRecordForTableShow> GetFinOutRecordWithPaging(VM_storeFinOutRecordForSearch finOutRecord, Paging paging)
        {
            
            //得到出库履历数据 根据条件自动过滤结果
            IQueryable<FinOutRecord> finOutRecordInfo = base.GetAvailableList<FinOutRecord>().FilterBySearch(finOutRecord);

            //得到出库履历详细数据 根据条件自动过滤结果
            IQueryable<FinOutDetailRecord> finOutDetailRecordInfo = base.GetAvailableList<FinOutDetailRecord>().FilterBySearch(finOutRecord);

            //判断订单号是否输入为空
            if (!String.IsNullOrEmpty(finOutRecord.ClientOrderID))
            {
                finOutDetailRecordInfo = finOutDetailRecordInfo.Where(u => (u.ClientOrderID + "/" + u.ClientOrderDetail).Contains(finOutRecord.ClientOrderID));
            }

            //产品ID 根据条件自动过滤结果
            IQueryable<PartInfo> partInfo = base.GetAvailableList<PartInfo>().FilterBySearch(finOutRecord);

            IQueryable<VM_storeFinOutRecordForTableShow> query = (from o in finOutRecordInfo
                                                                  join ol in finOutDetailRecordInfo on o.InerFinOutID equals ol.InerFinOutID
                                                                  join p in partInfo on ol.OrdPdtID equals p.PartId 
                                                                  select new VM_storeFinOutRecordForTableShow
                                                                 {
                                                                     InerFinOutID=o.InerFinOutID,
                                                                     OutDate=o.OutDate,
                                                                     Remarks=o.Remarks,
                                                                     LadiID=o.LadiID,
                                                                     WhkpID=o.WareHouseKpID

                                                                 }).Distinct();
            paging.total = query.Count();
            IEnumerable<VM_storeFinOutRecordForTableShow> result = query.ToPageList<VM_storeFinOutRecordForTableShow>("OutDate desc", paging);
            return result;

        }

        /// <summary>
        /// 出库履历一览删除出库履历详细表里数据
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        public bool updateFinOutRecordDetail(FinOutDetailRecord i)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_OUT_DETAIL_RECORD set DEL_FLG={0},UPD_USR_ID={1},UPD_DT={2} where INER_FIN_OUT_ID={3} ", i.DelFlag, i.UpdUsrID, i.UpdDt, i.InerFinOutID);
        }

        /// <summary>
        /// 出库履历一览删除出库履历表里数据
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        public bool updateFinOutRecord(FinOutRecord i)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_OUT_RECORD set DEL_FLG={0},UPD_USR_ID={1},UPD_DT={2} where INER_FIN_OUT_ID={3} ", i.DelFlag, i.UpdUsrID, i.UpdDt, i.InerFinOutID);
        }

        /// <summary>
        /// 获得出库履历详细画面数据
        /// </summary>
        /// <param name="inerFinOutID">送货单号</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据集合</returns>
        public IEnumerable<VM_storeFinOutRecordDetailForTableShow> GetFinOutRecordDetailByIdWithPaging(string inerFinOutID,string uId, Paging page)
        {
            //出库履历表
            IQueryable<FinOutRecord> endList = base.GetList().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //出库履历详细表
            IQueryable<FinOutDetailRecord> endDList = base.GetList<FinOutDetailRecord>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //零件信息表
            IQueryable<PartInfo> endPList = base.GetList<PartInfo>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));

            IQueryable<VM_storeFinOutRecordDetailForTableShow> query = from o in endList
                                                                 join ol in endDList on o.InerFinOutID equals ol.InerFinOutID
                                                                 join op in endPList on ol.OrdPdtID equals op.PartId into pname
                                                                 from op in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null 
                                                                 where o.InerFinOutID == inerFinOutID

                                                                  select new VM_storeFinOutRecordDetailForTableShow
                                                                 {
                                                                     ClientOrderID=ol.ClientOrderID+"/"+ol.ClientOrderDetail,//订单号
                                                                     OrdPdtID=op.PartAbbrevi,//物料编号
                                                                     ProductName = op.PartName,  //物料名称
                                                                     BatchID=ol.BatchID,  //批次号
                                                                     ProductSpec=ol.ProductSpec,  //规格型号
                                                                     Quantity=ol.Quantity,  //数量
                                                                     PackPrePieceQuantity=ol.PackPrePieceQuantity, //每件数量
                                                                     PackPieceQuantity=ol.PackPieceQuantity,  //件数
                                                                     FracQuantity=ol.FracQuantity, //零头
                                                                     PrchsUp=ol.PrchsUp,  //单价
                                                                     NotaxAmt=ol.NotaxAmt, //金额

                                                                     Remarks=ol.Remarks,//备注
                                                                     OutWorks=uId, //出库人
                                                                     MRemarks=o.Remarks, //备注

                                                                     InerFinOutID=ol.InerFinOutID,//送货单号
                                                                     LadiID=o.LadiID //提货单号

                                                                 };

            page.total = query.Count();
            IEnumerable<VM_storeFinOutRecordDetailForTableShow> result = query.ToPageList<VM_storeFinOutRecordDetailForTableShow>("OrdPdtID asc", page);
            return result;

        }

        #region 不用
        /// <summary>
        /// 查询出库履历表备注是否修改(暂时不用)
        /// </summary>
        /// <param name="finOutRecord">查询数据</param>
        /// <returns></returns>
        public bool UpdateMRemark(VM_storeFinOutRecordDetailForTableShow finOutRecord)
        {
            IQueryable<FinOutRecord> endList = base.GetList().Where(h => h.LadiID.Equals(finOutRecord.LadiID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));

            //如果备注修改返回真，没修改返回假
            if (endList.First().Remarks == finOutRecord.MRemarks)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

        /// <summary>
        /// 更新出库履历表
        /// </summary>
        /// <param name="finInDetailRecord">更新数据</param>
        /// <returns>true</returns>
        public bool UpdateInFinOutRecord(FinOutRecord finOutRecordT)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_OUT_RECORD set RMRS={0} where LADI_ID={5}", finOutRecordT.Remarks,finOutRecordT.LadiID);
        }

        /// <summary>
        /// 获得内部成品送货单打印选择画面数据
        /// </summary>
        /// <param name="finOutPrint">画面数据集合</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据集合</returns>
        public IEnumerable<VM_storeFinOutPrintForTableShow> GetFinOutPrintWithPaging(VM_storeFinOutPrintForSearch finOutPrint, Paging paging)
        {
           
            //得到出库履历数据 根据条件自动过滤结果
            IQueryable<FinOutRecord> finOutRecordInfo = base.GetAvailableList<FinOutRecord>().FilterBySearch(finOutPrint);

            //得到出库履历详细数据 根据条件自动过滤结果
            IQueryable<FinOutDetailRecord> finOutDetailRecordInfo = base.GetAvailableList<FinOutDetailRecord>().FilterBySearch(finOutPrint);

            //订单号查询判断 订单号=订单号+订单明细
            //if (!String.IsNullOrEmpty(finOutPrint.ClientOrderID))
            //{
            //    finOutDetailRecordInfo = finOutDetailRecordInfo.Where(u => (u.ClientOrderID+"/"+u.ClientOrderDetail).Contains(finOutPrint.ClientOrderID));
            //}

            IQueryable<VM_storeFinOutPrintForTableShow> query = from o in finOutRecordInfo
                                                                join ol in finOutDetailRecordInfo on o.InerFinOutID equals ol.InerFinOutID
                                                                  select new VM_storeFinOutPrintForTableShow
                                                                  {
                                                                      InerFinOutID = o.InerFinOutID,
                                                                      ClientOrderID = ol.ClientOrderID + "/" + ol.ClientOrderDetail,
                                                                      OutDate=o.OutDate,
                                                                      WareHouseKpID=o.WareHouseID,
                                                                      Remarks = ol.Remarks,
                                                                      OrdPdtID=ol.OrdPdtID,
                                                                      BatchID=ol.BatchID
                                                                      
                                                                  };
            paging.total = query.Count();
            IEnumerable<VM_storeFinOutPrintForTableShow> result = query.ToPageList<VM_storeFinOutPrintForTableShow>("InerFinOutID desc", paging);
            return result;

        }

        /// <summary>
        /// 出库履历表添加判断
        /// </summary>
        /// <param name="ladiID">提货单号</param>
        /// <returns>出库履历表添加判断数据集合</returns>
        public IQueryable<FinOutRecord> GetFinOutRecordLadiID(string ladiID)
        {

            return base.GetList().Where(h => h.LadiID.Equals(ladiID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        public IEnumerable<VM_ProdAndPartInfo> GetProdAndPartInfo(VM_ProdAndPartInfo searchConditon, Paging paging)
        {
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();

            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName,
                                        pricee = prod.Pricee
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName,
                                       pricee = part.Pricee
                                   }
                               );

            // 物料编号（产品零件略称）
            if (!String.IsNullOrEmpty(searchConditon.Abbrev))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.abbrev.Contains(searchConditon.Abbrev));
            }

            // 物料名称（产品零件名称）
            if (!String.IsNullOrEmpty(searchConditon.Name))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.name.Contains(searchConditon.Name));
            }

            IQueryable<VM_ProdAndPartInfo> query = from pp in prodAndPartsList
                                                   select new VM_ProdAndPartInfo
                                                       {
                                                           Id = pp.id,
                                                           Abbrev = pp.abbrev,
                                                           Name = pp.name,
                                                           Pricee = pp.pricee
                                                       };

            paging.total = query.Count();
            IEnumerable<VM_ProdAndPartInfo> result = query.ToPageList<VM_ProdAndPartInfo>("Abbrev asc", paging);
            return result;
        }

    }
}
