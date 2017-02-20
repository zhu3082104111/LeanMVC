/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessDeliveryRepositoryImp.cs
// 文件功能描述：加工送货单表的Repository接口实现
//     
// 修改履历：2013/12/09 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository.database;

namespace Repository
{
    class ProcessDeliveryRepositoryImp : AbstractRepository<DB, ProcessDelivery>, IProcessDeliveryRepository
    {


        public bool AddProcessDeliveryDetail(ProcessDeliveryDetail entity)
        {
            return base.Add<ProcessDeliveryDetail>(entity);
        }

        public bool AddProcessDelivBill(ProcessDelivBill entity)
        {
            return base.Add<ProcessDelivBill>(entity);
        }

        public IEnumerable<VM_ProProductWarehouseView> SearchProProductWarehouse(VM_ProProductWarehouseForSearch entity, Paging paging)
        {

            IQueryable<ProcessDelivery> proDels = base.GetAvailableList<ProcessDelivery>();//加工送货单
            IQueryable<ProcessDeliveryDetail> proDelDets = base.GetAvailableList<ProcessDeliveryDetail>();//加工送货单详细
            IQueryable<ProcessDelivBill> proDelBils = base.GetAvailableList<ProcessDelivBill>();//流转加工对应关系表

            IQueryable<ProdComposition> proComs = base.GetAvailableList<ProdComposition>();//成品构成信息表
            IQueryable<PartInfo> partInfos = base.GetAvailableList<PartInfo>();//零件信息表

            //选取入库状态
            IQueryable<MasterDefiInfo> warehouseStatus =
                base.GetAvailableList<MasterDefiInfo>().Where(t => t.SectionCd.Equals(Constant.Warehouse.WAREH_STA_SECTION_CD));

            //选取让步状态
            IQueryable<MasterDefiInfo> concessionParts = base.GetAvailableList<MasterDefiInfo>().Where(t => t.SectionCd.Equals(Constant.Warehouse.CONCESS_PARTS_SECTION_CD));

            //由于ProcDelivID流转表和送货表中命名相同的错误此处需注意
            var minDate = DateTime.MinValue;
            //默认值未提交
            var wareStatus = Constant.Warehouse.WAREH_STA;
            IQueryable<VM_ProProductWarehouseView> proProWarView = from pdd in proDelDets
                                                                   join pdb in proDelBils on pdd.ProcessDeliveryID equals pdb.ProcDelivID into gpdb
                                                                   from proDelBil in gpdb.DefaultIfEmpty()
                                                                   join pd in proDels on pdd.ProcessDeliveryID equals pd.ProcDelivID into gpd
                                                                   from proDel in gpd.DefaultIfEmpty()
                                                                   join pc in proComs on pdd.PartID equals pc.ParItemId into gpc
                                                                   from proCom in gpc.DefaultIfEmpty()
                                                                   join pi in partInfos on proCom.SubItemId equals pi.PartId into gpi
                                                                   from partInfo in gpi.DefaultIfEmpty()
                                                                   join ws in warehouseStatus on proDel.WarehouseStatus equals ws.AttrCd into gws
                                                                   from warehouseStatu in gws.DefaultIfEmpty()
                                                                   join cp in concessionParts on pdd.ConcessionPart equals cp.AttrCd into gcp
                                                                   from concessionPart in gcp.DefaultIfEmpty()
                                                                   select new VM_ProProductWarehouseView
                                                                   {
                                                                       ProcDelivID = pdd.ProcessDeliveryID,
                                                                       ProcessTranID = (proDelBil != null ? proDelBil.ProcessTranID : ""),
                                                                       PartID = pdd.PartID,
                                                                       PartName = pdd.PartName,
                                                                       SubItemID = (proCom != null ? proCom.SubItemId : ""),
                                                                       SubItemName = (partInfo != null ? partInfo.PartName : ""),
                                                                       WarehQtySubmitted = pdd.PlanTotal,
                                                                       WarehouseStatusNo = (proDel != null ? proDel.WarehouseStatus : ""),
                                                                       WarehouseStatus = (warehouseStatu != null ? warehouseStatu.AttrValue : ""),
                                                                       ConcessionPart = (concessionPart != null ? concessionPart.AttrValue : ""),
                                                                       BillDt = (proDel != null ? proDel.BillDt : minDate)
                                                                   };

            IQueryable<VM_ProProductWarehouseView> resultData = proProWarView.FilterBySearch(entity);
            paging.total = resultData.Count();
            return resultData.ToPageList("ProcDelivID asc", paging);
        }

        public ProcessDelivery GetProcessDeliveryByID(string id)
        {
            return base.GetAvailableList<ProcessDelivery>().FirstOrDefault(t => t.ProcDelivID.Equals(id));
        }

        public ProcessDeliveryDetail GetProcessDeliveryDetailByID(string id)
        {
            return base.GetAvailableList<ProcessDeliveryDetail>().FirstOrDefault(t => t.ProcessDeliveryID.Equals(id));
        }

        public ProcessDelivBill GetProcessDelivBillByID(string id)
        {
            return base.GetAvailableList<ProcessDelivBill>().FirstOrDefault(t => t.ProcDelivID.Equals(id));
        }

        public bool DeleteProcessDelivery(ProcessDelivery entity)
        {
            return base.Update(entity);
        }

        public bool DeleteProcessDeliveryDetail(ProcessDeliveryDetail entity)
        {
            return base.Update<ProcessDeliveryDetail>(entity);
        }

        public bool DeleteProcessDelivBill(ProcessDelivBill entity)
        {
            return base.Update<ProcessDelivBill>(entity);
        }

        public VM_ProcessDelivery GetVMProcessDeliveryByID(string id)
        {
            IQueryable<ProcessDelivery> proDels = base.GetAvailableList<ProcessDelivery>().Where(t => t.ProcDelivID.Equals(id));//加工送货单
            IQueryable<ProcessDeliveryDetail> proDelDets = base.GetAvailableList<ProcessDeliveryDetail>().Where(t => t.ProcessDeliveryID.Equals(id));//加工送货单详细

            //取得部门ID信息
            IQueryable<MasterDefiInfo> masterDefInfos =
                base.GetAvailableList<MasterDefiInfo>().Where(t => t.SectionCd.Equals(Constant.Warehouse.PRODUCE_UNIT_SECTION_CD));

            //用户信息表，用于取得仓管员的姓名
            IQueryable<UserInfo> userInfos = base.GetAvailableList<UserInfo>();


            IQueryable<VM_ProcessDelivery> vmProcessDeliveries = from pd in proDels
                                                                 join pdd in proDelDets on pd.ProcDelivID equals pdd.ProcessDeliveryID into gpdd
                                                                 from proDelDet in gpdd.DefaultIfEmpty()
                                                                 join md in masterDefInfos on pd.DepartID equals md.AttrCd into gmd
                                                                 from masterDefInfo in gmd.DefaultIfEmpty()
                                                                 join ui in userInfos on pd.WarehKprId equals ui.UId into gui
                                                                 from userInfo in gui.DefaultIfEmpty()
                                                                 select new VM_ProcessDelivery()
                                                                 {
                                                                     ProcDelivID = pd.ProcDelivID,
                                                                     DepartID = pd.DepartID,
                                                                     DepartName = (masterDefInfo != null ? masterDefInfo.AttrValue : ""),
                                                                     ConcessionPart = (proDelDet != null ? proDelDet.ConcessionPart : ""),
                                                                     BillDt = pd.BillDt,
                                                                     WarehKprId = pd.WarehKprId,
                                                                     WarehKprName = (userInfo != null ? userInfo.UName : "")
                                                                 };


            return vmProcessDeliveries.FirstOrDefault();
        }

        public IEnumerable<ProcessDeliveryDetail> GetProDelDetViewByID(string id)
        {
            IQueryable<ProcessDeliveryDetail> proDelDets = base.GetAvailableList<ProcessDeliveryDetail>().Where(t => t.ProcessDeliveryID.Equals(id));//加工送货单详细
            IQueryable<UnitInfo> unitInfos = base.GetAvailableList<UnitInfo>();
            IQueryable<ProcessDeliveryDetail> proDelDetView = from pdd in proDelDets.ToList().AsQueryable()
                                                              join ui in unitInfos.ToList().AsQueryable() on pdd.UnitID equals ui.UnitId into gui
                                                              from unitInfo in gui.DefaultIfEmpty()
                                                              select new ProcessDeliveryDetail
                                                              {
                                                                  ProcessDeliveryID = pdd.ProcessDeliveryID,
                                                                  PartID = pdd.PartID,
                                                                  PartName = pdd.PartName,
                                                                  ProcessID = pdd.ProcessID,
                                                                  ProcessName = pdd.ProcessName,
                                                                  UnitID = pdd.UnitID,
                                                                  UnitName = (unitInfo != null ? unitInfo.UnitName : ""),
                                                                  QualifiedQuantity = pdd.QualifiedQuantity,
                                                                  ScrapQuantity = pdd.ScrapQuantity,
                                                                  WasteQuantity = pdd.WasteQuantity,
                                                                  ExcessQuantity = pdd.ExcessQuantity,
                                                                  LackQuantity = pdd.LackQuantity,
                                                                  PlanTotal = pdd.PlanTotal,
                                                                  CheckID = pdd.CheckID,
                                                                  WarehouseID = pdd.WarehouseID,
                                                                  ConcessionPart = pdd.ConcessionPart,
                                                                  Remark = pdd.Remark
                                                              };

            return proDelDetView;
        }

        public bool UpdateProcessDeliveryDetail(ProcessDeliveryDetail entity)
        {
            return base.Update<ProcessDeliveryDetail>(entity);
        }

        public List<VM_ClnOdrList> GetClnListProcDel(string procDelivId)
        {
            var vmClnOdrList = new List<VM_ClnOdrList>();

            //加工送货详细
            ProcessDeliveryDetail proDelDet =
                base.GetAvailableList<ProcessDeliveryDetail>().FirstOrDefault(t => t.ProcessDeliveryID.Equals(procDelivId));

            //加工送货单工票对应关系
            ProcessDelivBill processDelivBill =
                base.GetAvailableList<ProcessDelivBill>().FirstOrDefault(t => t.ProcDelivID.Equals(procDelivId));
            if (processDelivBill != null && proDelDet!=null)
            {
                //客户订单流转卡关系表,并按加工数量进行升序排列
                IEnumerable<CustomTranslateInfo> cusTrInf =
                    base.GetAvailableList<CustomTranslateInfo>()
                        .Where(t => t.ProcDelivID.Equals(processDelivBill.ProcessTranID)).OrderBy(t=>t.PlnQty);
                //加工数量合计
                decimal total = (from cti in cusTrInf
                                 group cti by cti.ProcDelivID
                                     into gcti
                                     select gcti.Sum(t => t.PlnQty)).FirstOrDefault();
                //统计加工数量
                decimal sumWarehQty = 0;

                for (int i = 0; i < cusTrInf.Count(); i++)
                {
                    var vmClnOdr = new VM_ClnOdrList();
                    vmClnOdr.ClnOrderID = cusTrInf.ElementAt(i).CustomerOrderNum;
                    vmClnOdr.ClnOrderDetail = cusTrInf.ElementAt(i).CustomerOrderDetails;
                    if (i != cusTrInf.Count()-1){
                        //按百分比，向下取整取得值
                        vmClnOdr.WarehQty = total > 0 ? Math.Floor(( cusTrInf.ElementAt(i).PlnQty / total) *proDelDet.QualifiedQuantity) : 0;
                        //累计交仓数量
                        sumWarehQty = sumWarehQty + vmClnOdr.WarehQty;
                    }
                    else
                    {
                        //给最后一项，即计划数量最大项，赋值剩余合格数
                        vmClnOdr.WarehQty = proDelDet.QualifiedQuantity - sumWarehQty;
                    }
                    vmClnOdrList.Add(vmClnOdr);
                }
            }

            return vmClnOdrList;
        }
    }
}
