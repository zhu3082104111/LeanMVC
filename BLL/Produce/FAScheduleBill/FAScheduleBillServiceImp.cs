/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FAScheduleBillServiceImp.cs
// 文件功能描述：总装调度单的Service接口实现
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Common;
using BLL.ServerMessage;
using Extensions;
using Model;
using Model.Produce;
using Repository;
using Repository.Base;

namespace BLL
{
    class FAScheduleBillServiceImp : AbstractService, IFAScheduleBillService
    {
        private IAssemBillRepository assemBillRepository;
        private IAssemblyDispatchRepository assemblyDispatchBillRepository;
        private IProdCompositionRepository prodCompositionRepository;
        private IProdInfoRepository prodInfoRepository;
        private IOrderDetailRepository orderDetailRepository;

        private IMarketOrderRepository marketOrderRepository;
        private IMarketOrderDetailPrintRepository marketOrderDetailPrintRepository;
        private IAssemblyDispatchDetailRepository assemblyDispatchDetailRepository;

        private IProcessDetailRepository processDetailRepository;
        private ITeamRepository teamRepository;
        private IUserInfoRepository userInfoRepository;

        public FAScheduleBillServiceImp(IAssemBillRepository assemBillRepository, IAssemblyDispatchRepository assemblyDispatchBillRepository, IProdCompositionRepository prodCompositionRepository, IProdInfoRepository prodInfoRepository, IOrderDetailRepository orderDetailRepository, IMarketOrderRepository marketOrderRepository, IMarketOrderDetailPrintRepository marketOrderDetailPrintRepository, IAssemblyDispatchDetailRepository assemblyDispatchDetailRepository, IProcessDetailRepository processDetailRepository, ITeamRepository teamRepository, IUserInfoRepository userInfoRepository)
        {
            this.assemBillRepository = assemBillRepository;
            this.assemblyDispatchBillRepository = assemblyDispatchBillRepository;
            this.prodCompositionRepository = prodCompositionRepository;
            this.prodInfoRepository = prodInfoRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.marketOrderRepository = marketOrderRepository;
            this.marketOrderDetailPrintRepository = marketOrderDetailPrintRepository;
            this.assemblyDispatchDetailRepository = assemblyDispatchDetailRepository;
            this.processDetailRepository = processDetailRepository;
            this.teamRepository = teamRepository;
            this.userInfoRepository = userInfoRepository;
        }


        public IEnumerable<VM_AssemblyDispatch> GetFAScheduleBillSearch(VM_FAScheduleBillForSearch inProcessingRate, Paging paging)
        {
            IEnumerable<VM_AssemblyDispatch> list = assemblyDispatchBillRepository.GetAllAssemblyScheduleBill(inProcessingRate, paging);
            return list;
        }

        public AssemblyDispatch GetFAScheduleBillByID(String id)
        {
            //组织数据
            var entity = assemblyDispatchBillRepository.GetAssemblyScheduleBillById(id);
            entity.ProdAbbrev = prodInfoRepository.GetProdAbbrev(entity.ProductID);
            //取得班组名称
            entity.TeamName = teamRepository.GetTeamName(entity.Team);
            //调度员、质检员、交仓人、产成品仓管员的姓名
            var dispatcher = userInfoRepository.getUserInfoById(entity.Dispatcher);
            var inspector = userInfoRepository.getUserInfoById(entity.Inspector);
            var wareHousePerson = userInfoRepository.getUserInfoById(entity.WareHousePerson);
            var wareHouseKeeperID = userInfoRepository.getUserInfoById(entity.WareHouseKeeperID);
            entity.DispatcherName = dispatcher != null ? dispatcher.RealName : "";
            entity.InspectorName = inspector != null ? inspector.RealName : "";
            entity.WareHousePersonName = wareHousePerson != null ? wareHousePerson.RealName : "";
            entity.WareHouseKeeperIDName = wareHouseKeeperID != null ? wareHouseKeeperID.RealName : "";
            return entity;
        }

        public IEnumerable<VM_NewBillDataGrid> GetNewBillDataGrid(string productId, string clientOrderID, string clientOrderDetail)
        {
            //组织数据
            return prodCompositionRepository.GetNewBillDataGrid(productId, clientOrderID, clientOrderDetail);
        }

        public IEnumerable<VM_NewBillDataGrid> GetAssemblyDispatchDetail(String productId)
        {
            return assemblyDispatchDetailRepository.GetEditBillDataGrid(productId);
        }

        public IQueryable<VM_AssemblyDispatch> GetAutoCompleteSearch(VM_FAScheduleBillForSearch inProcessingRate)
        {
            var pag = new Paging();
            IQueryable<VM_AssemblyDispatch> list = assemblyDispatchBillRepository.GetAllAssemblyScheduleBill(inProcessingRate, pag).AsQueryable();
            return list;
        }



        public string DeleteSchedulingBill(List<string> deleList, string userId)
        {
            for (int i = 0; i < deleList.Count; i++)
            {
                var entity = assemblyDispatchBillRepository.GetAssemblyScheduleBillById(deleList[i]);

                if (entity.DispatchStatus != Constant.FAScheduleBill.DISPATCH_STATUS)
                {
                    //非未生产工票状态
                    var message = ResourceHelper.ConvertMessage(SM_Produce.WAssemStaGTone,
                        new string[] { entity.AssemblyDispatchID });
                    throw new Exception(message);
                }

                //如果实际装配数量大于零，则提示不能进行删除
                if (entity.ActualAssemblyNum > 0)
                {
                    var message = ResourceHelper.ConvertMessage(SM_Produce.WActualNumber,
                    new string[] { entity.AssemblyDispatchID, entity.ActualAssemblyNum.ToString() });
                    //抛出错误,进行回滚操作
                    throw new Exception(message);
                }

                //注意：由于业务逻辑发生变化，此代码暂时注释
                //var assDisBilCount = assemblyDispatchBillRepository.GetAssemblyTicketIdQuantity(entity.AssemblyTicketID);
                //IList<AssemBillDetail> assBilDetList = assemBillRepository.GetAssemBillDetail(entity.AssemblyTicketID).ToList();
                //Boolean hasOperatorID = false;
                //foreach (var assemBillDetail in assBilDetList)
                //{
                //    if (assemBillDetail.OperatorID != "")
                //    {
                //        hasOperatorID = true;
                //        break;
                //    }
                //}
                //if (hasOperatorID == true && assDisBilCount == 1)
                //{
                //     //总装工票只对应一张总装调度单，且存在对应的工票详细操作员ID则，不可以删除
                //    var message = ResourceHelper.ConvertMessage(SM_Produce.WExistOperator,
                //    new string[] { entity.AssemblyDispatchID});
                //    //抛出错误,进行回滚操作
                //    throw new Exception(message);
                //}

                //删除总装调度单
                assemblyDispatchBillRepository.DeleteAssemblyDispatchById(deleList[i], userId);

                //级联删除
                //删除调度单详细
                assemblyDispatchDetailRepository.DeleteAssemblyDispatchDetailById(deleList[i], userId);

                //注意：由于业务逻辑发生变化，此代码暂时注释
                //if (hasOperatorID == false && assDisBilCount == 1)
                //{
                //    //总装工票只对应一张总装调度单，不存在对应的工票详细操作员ID则,删除对应总装工票和总装工票详细
                //    assemBillRepository.DeleteAssemBill(entity.AssemblyTicketID, userId);
                //    assemBillRepository.DeleteAssemBillDetail(entity.AssemblyTicketID, userId);
                //}
            }

            return SM_Produce.MDeleteSuccess;
        }

        public string NewFAWorkticket(List<string> createList, List<string> assembBillIDList, string userId)
        {
            string message = SM_Produce.MCreateSuccess;
            var productIds = new List<AssemblyDispatch>();
            foreach (string id in createList)
            {
                var entity = assemblyDispatchBillRepository.GetAssemblyScheduleBillById(id);
                var assemblyTicketId = "";
                if (entity != null)
                {
                    assemblyTicketId = entity.AssemblyTicketID;
                }
                //已存在大工票
                if (!string.IsNullOrEmpty(assemblyTicketId) || entity.DispatchStatus != Constant.FAScheduleBill.DISPATCH_STATUS)
                {
                    //已经存在对应的总装工票
                    message = ResourceHelper.ConvertMessage(SM_Produce.WAssemBillExist,
                        new string[] { entity.AssemblyDispatchID });
                    return message;
                }
                productIds.Add(entity);
            }

            //当都不存在对应的总装工票则生成对应的大工票
            //取出不重复的产品型号（由于相同产品型号必须生成同一张大工票及其详细）
            var uniqueProductId = (from proId in productIds.AsQueryable() select proId.ProductID).Distinct().ToList();
            for (int i = 0; i < uniqueProductId.Count(); i++)
            {
                //相同产品型号的总装调度单
                IList<AssemblyDispatch> assemblyScheduleBill = productIds.Where(a => a.ProductID.Equals(uniqueProductId[i])).ToList();
                var asssemBill = new AssemBill();
                asssemBill.ProductID = assemblyScheduleBill[0].ProductID;
                asssemBill.AssemBillID = assembBillIDList[i];
                //取得工序号
                var subProcID = prodCompositionRepository.GetSubProcID(assemblyScheduleBill[0].ProductID);
                asssemBill.AssemProcessID = subProcID;
                asssemBill.CreDt = DateTime.Now;
                asssemBill.CreUsrID = userId;

                if (subProcID != "")
                {
                    //生成合并的一张大工票和对应的总装工票详细
                    assemBillRepository.Add(asssemBill);
                    //生成总装工票详细表
                    IList<int> SeqNoList = processDetailRepository.GetSeqNo(subProcID).ToList();
                    foreach (var seqNo in SeqNoList)
                    {
                        //每个顺序号对应生成一条总装工票详细表
                        var processDetail = new AssemBillDetail();
                        processDetail.AssemBillID = asssemBill.AssemBillID;
                        processDetail.ProcessOrderNO = seqNo;
                        //默认项目序号"1"
                        processDetail.ProjectNO = Constant.FAScheduleBill.PROJECT_NO;
                        //设置修改时间和修改者
                        processDetail.CreDt = DateTime.Now;
                        processDetail.CreUsrID = userId;
                        assemBillRepository.AddAssemBigBillDetail(processDetail);
                    }

                    foreach (var scheduleBill in assemblyScheduleBill)
                    {
                        //修改对应的总装工票ID
                        scheduleBill.AssemblyTicketID = asssemBill.AssemBillID;
                        //修改总装调度单的状态为未领料
                        scheduleBill.DispatchStatus = Constant.FAScheduleBill.DISP_STA_Not_Pick;
                        scheduleBill.UpdDt = DateTime.Now;
                        scheduleBill.UpdUsrID = userId;
                        assemblyDispatchBillRepository.UpdateAssemblyScheduleBill(scheduleBill);
                    }
                }
                else
                {
                    var error = ResourceHelper.ConvertMessage(SM_Produce.ESubProcIDNotExist,
                        new string[] { assemblyScheduleBill[0].AssemblyDispatchID });
                    throw new Exception(error);
                }
            }
            return message;
        }

        public string DelFAWorkticket(List<string> deleList)
        {
            throw new NotImplementedException();
        }


        public string SaveNewAndUpdate(Boolean symbol, string assemblyTicketId, Dictionary<string, string>[] assemblyDispatch, string userId)
        {
            try
            {

                var entity = new AssemblyDispatch();

                if (symbol)
                {
                    //添加新调度单
                    entity.AssemblyDispatchID = assemblyTicketId;
                    entity.ProductID = assemblyDispatch[0]["ProductID"];
                    entity.CustomerOrderNum = assemblyDispatch[0]["CustomerOrderNum"];
                    entity.CustomerOrderDetails = assemblyDispatch[0]["CustomerOrderDetails"];
                    entity.CustomerName = assemblyDispatch[0]["CustomerName"];
                    entity.SchedulingOrderDate = DateTime.Now;
                    entity.ActualAssemblyNum = 0;
                    entity.PlanDeliveryDate = DateTime.Parse(assemblyDispatch[0]["PlanDeliveryDate"]);
                    entity.DispatchStatus = Constant.FAScheduleBill.DISPATCH_STATUS;
                }
                else
                {
                    //修改调度单
                    entity = assemblyDispatchBillRepository.GetAssemblyScheduleBillById(assemblyTicketId);
                }

                //进行逻辑判断           
                //经理：取原料数量（由仓库那边取得）与当前统计的实际装配数量进行比较，
                //输入的计划数量<取原料数量-计的实际装配数量(排除当前记录的实际装配值）&&计划数量大于记录的实际装配值

                if (decimal.Parse(assemblyDispatch[0]["AssemblyPlanNum"]) < entity.ActualAssemblyNum)
                {
                    return SM_Produce.WPlanLessThenActual;
                }


                entity.Team = assemblyDispatch[0]["Team"];
                entity.AssemblyPlanNum = decimal.Parse(assemblyDispatch[0]["AssemblyPlanNum"]);

                entity.SchedulingOrderDate = DateTime.Parse(assemblyDispatch[0]["SchedulingOrderDate"]);

                entity.Dispatcher = assemblyDispatch[0]["Dispatcher"];
                entity.Inspector = assemblyDispatch[0]["Inspector"];
                entity.WareHousePerson = assemblyDispatch[0]["WareHousePerson"];
                entity.WareHouseKeeperID = assemblyDispatch[0]["WareHouseKeeperID"];

                entity.TypingContent = assemblyDispatch[0]["TypingContent"];
                entity.PackingFirst = assemblyDispatch[0]["PackingFirst"];
                entity.PackingSecond = assemblyDispatch[0]["PackingSecond"];
                entity.PackingThird = assemblyDispatch[0]["PackingThird"];
                entity.Remarks = assemblyDispatch[0]["Remarks"];

                entity.WareHouseBoxCer = decimal.Parse(assemblyDispatch[0]["WareHouseBoxCer"]);
                entity.WareHouseOdd = decimal.Parse(assemblyDispatch[0]["WareHouseOdd"]);
                entity.TotalNumberOfSets = decimal.Parse(assemblyDispatch[0]["TotalNumberOfSets"]);

                //装箱入库数量_箱数
                if (entity.WareHouseBoxCer > 0)
                {
                    entity.WareHouseBoxQuantity = (int)(entity.TotalNumberOfSets / entity.WareHouseBoxCer);
                    if (entity.TotalNumberOfSets % entity.WareHouseBoxCer > 0)
                    {
                        entity.WareHouseBoxQuantity = entity.WareHouseBoxQuantity + 1;
                    }
                }
                //如果是新增则保存当前日期
                //entity.WareHouseDate = DateTime.Parse(assemblyDispatch[0]["WareHouseDate"]);

                if (symbol)
                {
                    //添加
                    entity.WareHouseDate = DateTime.Now;
                    entity.CreUsrID = userId;
                    entity.CreDt = DateTime.Now;
                    assemblyDispatchBillRepository.AddAssemblyScheduleBill(entity);
                    IList<VM_NewBillDataGrid> nbdEntitys = prodCompositionRepository.GetNewBillDataGrid(entity.ProductID, entity.CustomerOrderNum, entity.CustomerOrderDetails).ToList();
                    int i = 1;
                    foreach (var nbdEntity in nbdEntitys)
                    {
                        var detail = new AssemblyDispatchDetail();
                        detail.CreDt = DateTime.Now;
                        detail.CreUsrID = userId;
                        detail.AssemblyDispatchID = entity.AssemblyDispatchID;
                        detail.SequenceNum = i.ToString();
                        detail.PartID = nbdEntity.SubItemID;
                        detail.PartName = nbdEntity.PartName;
                        detail.UnitQuantity = nbdEntity.ConstQty;
                        detail.BatchNum = nbdEntity.BatchNumber;
                        detail.Specifica = nbdEntity.Specifica;
                        detail.Remark = nbdEntity.Remark;
                        assemblyDispatchDetailRepository.SaveAssemblyDispatchDetail(detail);
                        i = i + 1;
                    }

                    return SM_Produce.MSaveSuccess;
                }
                else
                {
                    //修改
                    entity.UpdDt = DateTime.Now;
                    entity.UpdUsrID = userId;
                    if (assemblyDispatchBillRepository.UpdateAssemblyScheduleBill(entity))
                    {
                        return SM_Produce.MSaveSuccess;
                    }
                }

            }
            catch (Exception)
            {
                return SM_Produce.ESaveError;
            }
            return SM_Produce.ESaveError;
        }

        public AssemblyDispatch GetBasicInformation(AssemblyDispatch entity)
        {
            entity.SchedulingOrderDate = DateTime.Now;
            //entity.WareHouseDate = DateTime.Now;新增时入库日期不显示
            entity.ActualAssemblyNum = 0;

            //取得计划交期和装箱数
            var orderDetail = orderDetailRepository.GetOrderDetail(entity.CustomerOrderNum, entity.CustomerOrderDetails);
            if (orderDetail != null)
            {
                entity.PlanDeliveryDate = orderDetail.DeliveryDate;
                entity.WareHouseBoxCer = orderDetail.PackageQuantity;
            }
            //取得客户名称
            entity.CustomerName = marketOrderRepository.GetClientName(entity.CustomerOrderNum);
            //取得打字的信息
            IEnumerable<MarketOrderDetailPrint> printList = marketOrderDetailPrintRepository.GetTyping(entity.ProductID,
                entity.CustomerOrderNum, entity.CustomerOrderDetails);


            var typingContent = new StringBuilder();
            foreach (var order in printList)
            {
                typingContent.Append(SM_Produce.MTyping1);
                typingContent.Append(order.Position);
                typingContent.Append(Constant.FAScheduleBill.TYPE_STYLE_ONE);
                typingContent.Append(SM_Produce.MTyping2);
                typingContent.Append(order.Content);
                typingContent.Append(Constant.FAScheduleBill.TYPE_STYLE_SECOND);
            }

            entity.TypingContent = typingContent.ToString();
            return entity;
        }
    }
}
