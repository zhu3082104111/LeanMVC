/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFAScheduleBillService.cs
// 文件功能描述：加工交仓的的Service接口实现
//     
// 修改履历：2013/12/06 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Common;
using BLL.ServerMessage;
using Extensions;
using Model;
using Model.Produce;
using Repository;

namespace BLL
{
    class IWaitingWarehouseListServiceImp : AbstractService,IIWaitingWarehouseListService
    {
        private IProcessTranslateCardRepository processTranslateCardRepository;
        private ICustomTranslateInfoRepository customTranslateInfoRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IProcessRepository processRepository;
        private IProcessDeliveryRepository processDeliveryRepository;

        public IWaitingWarehouseListServiceImp(IProcessTranslateCardRepository processTranslateCardRepository, ICustomTranslateInfoRepository customTranslateInfoRepository, IOrderDetailRepository orderDetailRepository, IProcessRepository processRepository, IProcessDeliveryRepository processDeliveryRepository)
        {
            this.processTranslateCardRepository = processTranslateCardRepository;
            this.customTranslateInfoRepository = customTranslateInfoRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.processRepository = processRepository;
            this.processDeliveryRepository = processDeliveryRepository;
        }

        public IEnumerable<VM_IWaitingWarehouseView> SearchTranslateCard(VM_IWaitingWarehouseForSearch entity, Paging paging)
        {
            return processTranslateCardRepository.SearchTranslateCard(entity, paging);
        }

        public IEnumerable<VM_ProProductWarehouseView> SearchProProductWarehouse(VM_ProProductWarehouseForSearch entity, Paging paging)
        {
            return processDeliveryRepository.SearchProProductWarehouse(entity, paging);
        }

        public string DeleteWarehouse(List<string> deleList, string userId)
        {
            for (int i = 0; i < deleList.Count; i++)
            {
                //取得加工送货单记录
                var proDel = processDeliveryRepository.GetProcessDeliveryByID(deleList[i]);
                var proDelDet = processDeliveryRepository.GetProcessDeliveryDetailByID(deleList[i]);
                var proDelBil = processDeliveryRepository.GetProcessDelivBillByID(deleList[i]);

                if (proDel == null || proDelDet == null || proDelBil == null)
                {
                    var error = ResourceHelper.ConvertMessage(SM_Produce.WWarehouseHasBeenDeleted,
                    new string[] { deleList[i] });
                    throw new Exception(error);
                }
                if (proDel.WarehouseStatus != Constant.Warehouse.WAREH_STA)
                {
                    var error = ResourceHelper.ConvertMessage(SM_Produce.MWarehouseNotBeDeleted,
                    new string[] { deleList[i] });
                    throw new Exception(error);
                }
                proDel.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
                proDel.DelDt = DateTime.Now;
                proDel.DelUsrID = userId;

                proDelDet.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
                proDelDet.DelDt = DateTime.Now;
                proDelDet.DelUsrID = userId;

                proDelBil.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
                proDelBil.DelDt = DateTime.Now;
                proDelBil.DelUsrID = userId;

                processDeliveryRepository.DeleteProcessDelivery(proDel);
                processDeliveryRepository.DeleteProcessDeliveryDetail(proDelDet);
                processDeliveryRepository.DeleteProcessDelivBill(proDelBil);

                //对加工流转卡的交仓合计进行修改
                processTranslateCardRepository.ReduceWarehTalQty(proDelBil.ProcessTranID, proDelDet.PlanTotal, userId);
            }

            return SM_Produce.MWarehouseDeleteSucc;
        }



        public bool DeliveryWarehouse(VM_IWaitingWarehouseView entity, string userId, string procDelivID)
        {
            //取得客户订单流转卡关系表
            CustomTranslateInfo cusTraInf=customTranslateInfoRepository.GetCustomTranslateInfo(entity.ProcDelivID);

            //取得加工部门ID--对应订单明细表的生产单元区分
            var produceCellId = "";
            if (cusTraInf != null)
            {
                var cusOrdDet=orderDetailRepository.GetOrderDetail(cusTraInf.CustomerOrderNum, cusTraInf.CustomerOrderDetails);
                if (cusOrdDet != null)
                {
                    produceCellId = cusOrdDet.ProduceCellID;
                }
            }

            //根据工序号取得工序名称
            var processName = "";
            var processEntity=processRepository.GetEntityById(entity.ProcessID);
            if (processEntity != null)
            {
                processName = processEntity.ProcName;
            }

            //产品单位ID已经在视图中取得，并传入

            var proDel = new ProcessDelivery();
            var proDelDet = new ProcessDeliveryDetail();
            var proDelBil = new ProcessDelivBill();

            //加工送货单
            proDel.ProcDelivID = procDelivID;
            proDel.DepartID = produceCellId;
            proDel.BillDt = DateTime.Now;
            proDel.CreUsrID = userId;
            proDel.CreDt = DateTime.Now;
            //默认未提交状态
            proDel.WarehouseStatus = Constant.Warehouse.WAREH_STA;
            processDeliveryRepository.Add(proDel);

            //加工送货单详细
            proDelDet.ProcessDeliveryID = proDel.ProcDelivID;
            proDelDet.PartID = entity.ExportID;
            proDelDet.PartName = entity.ProdAbbrev;
            proDelDet.ProcessID = entity.ProcessID;
            proDelDet.ProcessName = processName;
            proDelDet.UnitID = entity.UnitId;

            //合格品数量和预计交仓合计都等于可交仓数
            proDelDet.QualifiedQuantity = entity.WarehQtyAvailable;
            proDelDet.PlanTotal = entity.WarehQtyAvailable;
            proDelDet.ConcessionPart = entity.ConcessionPart;
            proDelDet.CreUsrID = userId;
            proDelDet.CreDt = DateTime.Now;

            processDeliveryRepository.AddProcessDeliveryDetail(proDelDet);

            //加工送货单流转卡对应关系表
            proDelBil.ProcDelivID = proDel.ProcDelivID;
            proDelBil.PartID = entity.ExportID;
            proDelBil.ProcessTranID = entity.ProcDelivID;
            proDelBil.CreUsrID = userId;
            proDelBil.CreDt = DateTime.Now;

            processDeliveryRepository.AddProcessDelivBill(proDelBil);

            //更新加工流转卡的交仓数合计
            return processTranslateCardRepository.AddWarehTalQty(entity, userId);           
        }

        public ProcessDelivery GetProcessDeliveryByID(string id)
        {
            return processDeliveryRepository.GetProcessDeliveryByID(id);
        }

        public VM_ProcessDelivery GetVMProcessDelivery(string id)
        {
            return processDeliveryRepository.GetVMProcessDeliveryByID(id);
        }

        public IEnumerable<ProcessDeliveryDetail> GetProDelDetViewByID(string id)
        {
            return processDeliveryRepository.GetProDelDetViewByID(id);
        }

        public string SaveConcession(string procDelivID, string concessionPart)
        {
            ProcessDeliveryDetail proDelDet = processDeliveryRepository.GetProcessDeliveryDetailByID(procDelivID);
            proDelDet.ConcessionPart = concessionPart;
            if(processDeliveryRepository.UpdateProcessDeliveryDetail(proDelDet))
            {
                return SM_Produce.MSaveSuccess;
            }
            return SM_Produce.ESaveError;           
        }
    }
}
