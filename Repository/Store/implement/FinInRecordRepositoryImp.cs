/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinInRecordRepositoryImp.cs
// 文件功能描述：
//          内部成品库入库履历的Repository接口的实现
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
    /// 内部成品库入库履历的Repository接口的实现
    /// </summary>
    class FinInRecordRepositoryImp : AbstractRepository<DB, FinInRecord>, IFinInRecordRepository
    {
        /// <summary>
        /// 获得待入库一览画面数据
        /// </summary>
        /// <param name="finInStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInStoreForTableShow> GetFinInStoreWithPaging(VM_StoreFinInStoreForSearch finInStore, Paging paging)
        {

            //交仓单号  根据条件自动过滤结果
            IQueryable<ProductWarehouse> endList = base.GetAvailableList<ProductWarehouse>().FilterBySearch(finInStore);

            IQueryable<VM_StoreFinInStoreForTableShow> query = from o in endList
                                                               where o.WarehouseState == "1"
                                                               select new VM_StoreFinInStoreForTableShow
                                                            {
                                                                ProductWarehouseID = o.ProductWarehouseID,
                                                                WarehouseDT=o.WarehouseDT
                                                            };
            paging.total = query.Count();
            IEnumerable<VM_StoreFinInStoreForTableShow> result = query.ToPageList<VM_StoreFinInStoreForTableShow>("WarehouseDT asc", paging);
            return result;

        }

        /// <summary>
        /// 获得入库履历一览画面数据
        /// </summary>
        /// <param name="finInRecord">画面数据集合</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForTableShow> GetFinInRecordWithPaging(VM_StoreFinInRecordForSearch finInRecord, Paging paging)
        {

            //得到入库履历数据 根据条件自动过滤结果
            IQueryable<FinInRecord> finInRecordInfo = base.GetAvailableList<FinInRecord>().FilterBySearch(finInRecord);

            //产品ID 根据条件自动过滤结果
            IQueryable<PartInfo> partInfo = base.GetAvailableList<PartInfo>().FilterBySearch(finInRecord);

            //得到入库履历详细数据 根据条件自动过滤结果
            IQueryable<FinInDetailRecord> finInDetailRecordInfo = base.GetAvailableList<FinInDetailRecord>().FilterBySearch(finInRecord);

            //订单号查询判断 订单号=订单号+订单明细
            if (!String.IsNullOrEmpty(finInRecord.ClientOrderID))
            {
                finInDetailRecordInfo = finInDetailRecordInfo.Where(u => (u.TecnProcess + "/" + u.ClientOrderDetail).Contains(finInRecord.ClientOrderID));
            }

            IQueryable<VM_StoreFinInRecordForTableShow> query = (from o in finInRecordInfo
                                                                 join ol in finInDetailRecordInfo on o.FsInID equals ol.FsInID
                                                                 join p in partInfo on ol.ProductID equals p.PartId
                                                                select new VM_StoreFinInRecordForTableShow
                                                                  {
                                                                      PlanID = o.PlanID,
                                                                      BatchID = o.BatchID,
                                                                      WareHouseID=o.WareHouseID,
                                                                      WareHousePositionID=o.WareHousePositionID,
                                                                      InMoveCls=o.InMoveCls,
                                                                      InDate=o.InDate,
                                                                      WareHouseKpID=o.WareHouseKpID,
                                                                      Remarks=o.Remarks

                                                     }).Distinct();
            paging.total = query.Count();
            IEnumerable<VM_StoreFinInRecordForTableShow> result = query.ToPageList<VM_StoreFinInRecordForTableShow>("InDate desc", paging);
            return result;

        }

        /// <summary>
        /// 获得入库履历详细画面数据（入库跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页跳转</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordByIdWithPaging(string productWarehouseID, Paging page)
        {
         
            //成品交仓表
            IQueryable<ProductWarehouse> endList = base.GetList<ProductWarehouse>().Where(h =>  h.ProductWarehouseID.Equals(productWarehouseID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //成品交仓详细表
            IQueryable<ProductWarehouseDetail> endDList = base.GetList<ProductWarehouseDetail>().Where(h =>  h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //零件信息表
            IQueryable<PartInfo> endPList = base.GetList<PartInfo>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0")); 

            IQueryable<VM_StoreFinInRecordForDetailShow> query = from o in endList
                                                                    join ol in endDList on o.ProductWarehouseID equals ol.ProductWarehouseID
                                                                    join op in endPList on ol.OrderProductID equals op.PartId into pname
                                                                    from op in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                    where o.ProductWarehouseID == productWarehouseID

                                                                 select new VM_StoreFinInRecordForDetailShow
                                                                   {
                                                                       ProductWarehouseID = o.ProductWarehouseID,
                                                                       BatchID = o.BatchID,
                                                                       WareHouseID = "03"+o.DepartmentID,
                                                                       InMoveCls = "01",
                                                                       PlanID = ol.ClientOrderID+"/"+ol.ClientOrderDetail,
                                                                       ProductCheckID = ol.ProductCheckID,
                                                                       TeamID = ol.TeamID ,

                                                                       OrderProductID = op.PartAbbrevi,
                                                                       ProductName = op.PartName,
                                                                       PartID = ol.OrderProductID,
                                                                       ProductSpecification = ol.ProductSpecification,
                                                                       QualifiedQuantity = ol.QualifiedQuantity,
                                                                       EachBoxQuantity = ol.EachBoxQuantity,
                                                                       BoxQuantity = ol.BoxQuantity,
                                                                       ClientOrderID=ol.ClientOrderID,
                                                                       ClientOrderDetail=ol.ClientOrderDetail,

                                                                       RemianQuantity = ol.RemianQuantity,
                                                                       GiclsProduct="未使用",
                                                                       MRemarks=o.Remark,
                                                                       Remarks = ol.Remark,
                                                                       InDate = DateTime.Today,
                                                                       WareHouseKpID="CJ"

                                                                   };

            page.total = query.Count();
            IEnumerable<VM_StoreFinInRecordForDetailShow> result = query.ToPageList<VM_StoreFinInRecordForDetailShow>("ProductWarehouseID asc", page);
            return result;

        }

        /// <summary>
        /// 入库履历表数据伪删除
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        public bool updateFinInRecord(FinInRecord i)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_IN_RECORD set DEL_FLG={0},UPD_USR_ID={1},UPD_DT={2} where PLAN_ID={3} ", i.DelFlag, i.UpdUsrID, i.UpdDt, i.PlanID);
        }

        /// <summary>
        /// 入库履历详细表数据伪删除
        /// </summary>
        /// <param name="i">删除数据集合</param>
        /// <returns>true</returns>
        public bool updateFinInRecordDetail(FinInDetailRecord i)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_IN_DETAIL_RECORD set DEL_FLG={0},UPD_USR_ID={1},UPD_DT={2} where FS_IN_ID={3} ", i.DelFlag, i.UpdUsrID, i.UpdDt, i.FsInID);
        }

        /// <summary>
        /// 获得入库详细画面数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordDetailByIdWithPaging(string productWarehouseID, Paging page)
        {
            //入库履历表
            IQueryable<FinInRecord> endList = base.GetList<FinInRecord>().Where(h =>h.PlanID.Equals(productWarehouseID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //入库履历详细表
            IQueryable<FinInDetailRecord> endDList = base.GetList<FinInDetailRecord>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
            //零件信息表
            IQueryable<PartInfo> endPList = base.GetList<PartInfo>().Where(h => h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));

            IQueryable<VM_StoreFinInRecordForDetailShow> query = from o in endList
                                                                 join ol in endDList on o.FsInID equals ol.FsInID
                                                                 join op in endPList on ol.ProductID equals op.PartId
                                                                 where o.PlanID == productWarehouseID

                                                                 select new VM_StoreFinInRecordForDetailShow
                                                                 {
                                                                     ProductWarehouseID = o.PlanID,
                                                                     BatchID = o.BatchID,
                                                                     WareHouseID = o.WareHouseID,
                                                                     InMoveCls = o.InMoveCls,
                                                                     PlanID = ol.TecnProcess + "/" + ol.ClientOrderDetail,
                                                                     ProductCheckID = ol.IsetRepID,
                                                                     ClientOrderID = ol.TecnProcess,
                                                                     ClientOrderDetail = ol.ClientOrderDetail,

                                                                     TeamID = "A",
                                                                     OrderProductID = op.PartAbbrevi,
                                                                     ProductName = op.PartName,
                                                                     PartID = ol.ProductID,
                                                                     ProductSpecification = ol.ProductSpec,
                                                                     QualifiedQuantity = ol.Quantity,
                                                                     EachBoxQuantity = ol.ProScrapQuantity,
                                                                     BoxQuantity = ol.ProMaterscrapQuantity,
                                                                     RemianQuantity = ol.ProOverQuantity,
                                                                     GiclsProduct = "未使用",
                                                                     Remarks = ol.Remarks,
                                                                     InDate = o.InDate,
                                                                     WareHouseKpID = o.WareHouseKpID,
                                                                     MRemarks=o.Remarks

                                                                 };
           
            page.total = query.Count();
            IEnumerable<VM_StoreFinInRecordForDetailShow> result = query.ToPageList<VM_StoreFinInRecordForDetailShow>("ProductWarehouseID asc", page);
            return result;

        }

        /// <summary>
        /// 入库履历表添加判断
        /// </summary>
        /// <param name="planID">交仓单号</param>
        /// <returns>入库履历数据集合</returns>
        public IQueryable<FinInRecord> GetFinInRecordPlanID(string planID)
        {
            
            return base.GetList().Where(h => h.PlanID.Equals(planID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 更新入库履历表备注
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <returns>true</returns>
        public bool UpdateInFinInRecord(FinInRecord finInRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_IN_RECORD set RMRS={0},UPD_USR_ID={1},UPD_DT={2} where PLAN_ID={3}", finInRecord.Remarks, finInRecord.UpdUsrID, finInRecord.UpdDt, finInRecord.PlanID);
        }

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForTableShow IFinInRecordRepository.GetFinInRecordInfoById(string productWarehouseID)
        {
            //入库履历表
            IQueryable<FinInRecord> endList = base.GetList<FinInRecord>().Where(h => h.PlanID.Equals(productWarehouseID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));

            IQueryable<VM_StoreFinInRecordForTableShow> query = from o in endList

                                                                select new VM_StoreFinInRecordForTableShow
                                                      {
                                                         PlanID=o.PlanID,  //成品交仓单号
                                                         BatchID=o.BatchID,  //批次号
                                                         WareHouseID=o.WareHouseID,  //仓库编号
                                                         InMoveCls=o.InMoveCls,  //入库移动区分
                                                         InDate=o.InDate,  //入库日期
                                                         WareHouseKpID=o.WareHouseKpID,  //仓管员ID
                                                         Remarks=o.Remarks  //备注

                                                      };

            IEnumerable<VM_StoreFinInRecordForTableShow> result = query.AsEnumerable();
            return result.First();

        }

    }
}
