// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：PickingListRepositoryImp.cs
// 文件功能描述：生产领料单信息repository实现类
// 
// 创建标识：代东泽 20131127
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model.Produce;
using System.Data.Objects.SqlClient;

namespace Repository
{
    /// <summary>
    /// 代东泽 20131127
    /// </summary>
    class PickingListRepositoryImp : AbstractRepository<DB, ProduceMaterRequest>, IPickingListRepository
    {
        /// <summary>
        /// 代东泽 20131202
        /// </summary>
        /// <param name="pickingList"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProduceMaterRequestForTableShow> GetPickingListsBySearch(VM_ProduceMaterRequestForSearch pickingList, Extensions.Paging paging)
        {
            IQueryable<ProduceMaterDetail> detailList = base.GetAvailableList<ProduceMaterDetail>();//根据条件自动过滤结果
            //IQueryable<MarketOrderDetail> market = base.GetAvailableList<MarketOrderDetail>();
            IQueryable<ProduceMaterRequest> list = base.GetAvailableList<ProduceMaterRequest>().FilterBySearch(pickingList); ;
            IQueryable<MasterDefiInfo> masterList=base.GetAvailableList<MasterDefiInfo>();

            IQueryable<VM_ProduceMaterRequestForTableShow> query=null;

            
            if (pickingList.ComeFromNo != null && !pickingList.ComeFromNo.Equals("")) 
            {
                detailList = detailList.Where(n => n.AssBillID.Contains(pickingList.ComeFromNo) || n.ProcDelivID.Contains(pickingList.ComeFromNo));
            }
            var c = from a in list
                    join d in detailList on a.MaterReqNo equals d.MaterReqNo
                    group d by new { a.MaterReqNo } into _a
                    select new
                    {
                        MaterReqNo = _a.Key.MaterReqNo,
                        ReceQty = _a.Sum(n => n.ReceQty)
                    };
            if (pickingList.Type != null && pickingList.Type.Equals("1"))//未领料
            {
                c = c.Where(n=>n.ReceQty<=0M);
            }
            else if (pickingList.Type != null && pickingList.Type.Equals("2"))//已领料
            {
                c = c.Where(n => n.ReceQty > 0M);
            }
            else 
            {
            
            }
            query = from a in list 
                    join d in detailList on a.MaterReqNo equals d.MaterReqNo
                    join _d in c on a.MaterReqNo equals _d.MaterReqNo
                    join m in masterList on new { id = a.DeptID, dt = Constant.MasterSection.DEPT } equals new { id = m.AttrCd, dt = m.SectionCd }
                    join m2 in masterList on new { id = a.MaterReqType, dt = Constant.MasterSection.PICKING_TYPE } equals new { id = m2.AttrCd, dt = m2.SectionCd }
                    group a by new { a.MaterReqNo, d.ProcDelivID, d.AssBillID, m.AttrValue, a.RequestDate, _d.ReceQty, AttrValue2=m2.AttrValue } into _a
                    select new VM_ProduceMaterRequestForTableShow
                    {
                        PickingNo = _a.Key.MaterReqNo,
                        ComeFromNo = _a.Key.ProcDelivID,
                        ComeFromNoW = _a.Key.AssBillID,
                        PickingUnit = _a.Key.AttrValue,
                        UsePerson = "",
                        CurrentState = "",
                        StoreManager = "",
                        PickingTime = _a.Key.RequestDate,
                        RealPickingCount = _a.Key.ReceQty,
                        ComeFrom = _a.Key.AttrValue2
                    };
            
            paging.total = query.Count();
            return query.ToPageList<VM_ProduceMaterRequestForTableShow>("PickingNo asc", paging);
        }


        /// <summary>
        /// IPickingListRepository.SelectProduceMaterDetail
        /// 杨灿 20131216
        /// </summary>
        /// <param name="produceMaterDetail"></param>
        /// <returns></returns>
        public ProduceMaterDetail SelectProduceMaterDetail(ProduceMaterDetail produceMaterDetail)
        {
            return base.First<ProduceMaterDetail>(a => a.MaterReqNo == produceMaterDetail.MaterReqNo && a.MaterReqDetailNo == produceMaterDetail.MaterReqDetailNo && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        /// <summary>
        /// 代东泽 20131226
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<ProduceMaterDetail> GetDetailListByCondition(System.Linq.Expressions.Expression<Func<ProduceMaterDetail, bool>> condition)
        {
            return base.GetAvailableList<ProduceMaterDetail>().Where(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public void AddDetail(ProduceMaterDetail detail)
        {
            base.Add(detail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public VM_ProduceMaterRequestForTableShow GetProduceMaterRequestForDetail(ProduceMaterRequest p)
        {
            IQueryable<ProduceMaterRequest> list = base.GetAvailableList<ProduceMaterRequest>();
            IQueryable<ProduceMaterDetail> detailList = base.GetAvailableList<ProduceMaterDetail>();
            IQueryable<MasterDefiInfo> masterList=base.GetAvailableList<MasterDefiInfo>();
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();
            var query = from a in list
                        where a.MaterReqNo.Equals(p.MaterReqNo)
                        join d in detailList on a.MaterReqNo equals d.MaterReqNo
                        join pd in prodList on d.MaterialID equals pd.ProductId
                        join m in masterList on new { id = a.DeptID, dt = Constant.MasterSection.DEPT } equals new { id = m.AttrCd, dt = m.SectionCd }
                        join m2 in masterList on new { id = a.MaterReqType, dt = Constant.MasterSection.PICKING_TYPE } equals new { id = m2.AttrCd, dt = m2.SectionCd }
                        group a by new { a.MaterReqNo,d.ProcDelivID,d.AssBillID, a.RequestDate,AttrValue= m.AttrValue, AttrValue2=m2.AttrValue } into _a 
                        select new VM_ProduceMaterRequestForTableShow
                        {
                            PickingNo = _a.Key.MaterReqNo,
                            ComeFrom = _a.Key.AttrValue,
                            ComeFromNo = _a.Key.ProcDelivID,
                            ComeFromNoW = _a.Key.AssBillID,
                            PickingUnit =_a.Key.AttrValue2,
                            PickingTime = _a.Key.RequestDate
                        };
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProduceMaterDetailForDetailShow> GetProduceMaterDetailsForDetail(ProduceMaterRequest p)
        {
            IQueryable<ProduceMaterRequest> list = base.GetAvailableList<ProduceMaterRequest>();
            IQueryable<ProduceMaterDetail> detailList = base.GetAvailableList<ProduceMaterDetail>();
            IQueryable<MasterDefiInfo> masterList = base.GetAvailableList<MasterDefiInfo>();
            IQueryable<PartInfo> prodList = base.GetAvailableList<PartInfo>();
            IQueryable<UserInfo> userList = base.GetAvailableList<UserInfo>();
            IQueryable<Reserve> rsList = base.GetAvailableList<Reserve>();
            IQueryable<ReserveDetail> rsDetailList = base.GetAvailableList<ReserveDetail>();
            var c = from a in rsList
                    join d in rsDetailList on a.OrdeBthDtailListID equals d.OrdeBthDtailListID into _a
                    from r in _a.DefaultIfEmpty()
                    select new
                    {
                        a.OrdeBthDtailListID,
                        a.ClnOdrDtl,
                        a.ClnOdrID,
                        a.PdtID,
                        a.WhID,
                        r.BthID
                    };

            var query = from a in list
                        where a.MaterReqNo.Equals(p.MaterReqNo)
                        //join u in userList.DefaultIfEmpty() on a.MaterHandlerID equals u.UId
                        // join us in userList.DefaultIfEmpty() on a.DeptAuditorID equals us.UId
                        //join up in userList.DefaultIfEmpty() on a.WhPsnID equals up.UId
                        join d in detailList on a.MaterReqNo equals d.MaterReqNo
                        join pd in prodList on d.MaterialID equals pd.PartId into _a
                        from r in _a.DefaultIfEmpty()
                        join _c in c on new { d.BthID, d.CustomerOrderNum, d.CustomerOrderDetails, WHID = d.WHID, MaterialID = d.MaterialID } equals new { _c.BthID, CustomerOrderNum=_c.ClnOdrID, CustomerOrderDetails = _c.ClnOdrDtl, WHID = _c.WhID, MaterialID = _c.PdtID } into _rc
                        from rc in _rc.DefaultIfEmpty()
                        join m in masterList on new { id = a.DeptID, dt = Constant.MasterSection.DEPT } equals new { id = m.AttrCd, dt = m.SectionCd }
                        join m2 in masterList on new { id = a.MaterReqType, dt = Constant.MasterSection.PICKING_TYPE } equals new { id = m2.AttrCd, dt = m2.SectionCd }

                        select new VM_ProduceMaterDetailForDetailShow
                        {
                            PickingNo = a.MaterReqNo,
                            MaterReqDetailNo = d.MaterReqDetailNo,
                            MaterialID = d.MaterialID,
                            BthID = d.BthID,
                            ClnOdrDtl = d.CustomerOrderDetails,
                            ClnOdrID = d.CustomerOrderNum,
                            PartModel = r.PartAbbrevi,
                            PartName = r.PartName,
                            RealPickingCount =d.ReceQty,
                            UnitPrice = d.UnitPrice,
                            PleasePickingCount = d.AppoQty,
                            TotalPrice = d.TotalPrice,
                            Unit = d.PriceUnitID,
                            PdtSpec = d.PdtSpec,
                            WHID = d.WHID,
                            ComeFromNo = d.ProcDelivID,
                            ComeFromNoW = d.AssBillID,

                            ComeFrom = m2.AttrValue,
                            PickingUnit = m.AttrValue,
                            PickingTime = a.RequestDate,
                            Use=a.Purpose,

                            OrdeBthDtailListID =rc.OrdeBthDtailListID!=null? rc.OrdeBthDtailListID:0
                            //Auditor=us.UName,
                            // Picker=u.UName,
                            //  WHManager=up.UName
                        };
            return query;
        }


        public void UpdateDetail(ProduceMaterDetail obj)
        {
            base.UpdateNotNullColumn(obj);
        }
    }
}
