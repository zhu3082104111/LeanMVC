/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccStoreServiceImp.cs
// 文件功能描述：
//          仓库部门附件库Service实现类、附件库业务代码
//      
// 修改履历：2013/11/15 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using Util;
using System.Collections;
namespace BLL
{

    public class AccStoreServiceImp : AbstractService, IAccStoreService
    {

        //需要使用的Repository类
        private IAccInRecordRepository accInRecordRepository;
        private IAccInDetailRecordRepository accInDetailRecordRepository;
        private IAccOutRecordRepository accOutRecordRepository;
        private IAccOutDetailRecordRepository accOutDetailRecordRepository;
        private IWipStoreRepository wipStoreRepository;
        private IWipStoreDetilRepository wipStoreDetilRepository;
        private IReserveRepository reserveRepository;
        private IReserveDetailRepository reserveDetailRepository;
        private IBthStockListRepository bthStockListRepository;
        private IMCOutSourceOrderRepository mcOutSourceOrderRepository;
        private IMCOutSourceOrderDetailRepository mcOutSourceOrderDetailRepository;
        private IMaterialRepository materialRepository;
        private IGiMaterialRepository giMaterialRepository;
        private IGiReserveRepository giReserveRepository;
        private ICompMaterialInfoRepository compMaterialInfoRepository;
        private IPickingListRepository pickingListRepository;
        private ISupplierOrderDetailRepository supplierOrderDetailRepository;


        //构造方法，必须要，参数为需要注入的属性
        public AccStoreServiceImp(IWipStoreRepository wipStoreRepository, IAccInRecordRepository accInRecordRepository,
                                  IAccOutRecordRepository accOutRecordRepository, IWipStoreDetilRepository wipStoreDetilRepository,
                                  IAccInDetailRecordRepository accInDetailRecordRepository, IReserveRepository reserveRepository,
                                  IBthStockListRepository bthStockListRepository, IMCOutSourceOrderRepository mcOutSourceOrderRepository,
                                  IMCOutSourceOrderDetailRepository mcOutSourceOrderDetailRepository, IMaterialRepository materialRepository,
                                  IReserveDetailRepository reserveDetailRepository, IGiMaterialRepository giMaterialRepository,
                                  ICompMaterialInfoRepository compMaterialInfoRepository, IAccOutDetailRecordRepository accOutDetailRecordRepository,
                                  IPickingListRepository pickingListRepository, IGiReserveRepository giReserveRepository,
                                  ISupplierOrderDetailRepository supplierOrderDetailRepository)
        {
            this.wipStoreRepository = wipStoreRepository;
            this.accInRecordRepository = accInRecordRepository;
            this.accInDetailRecordRepository = accInDetailRecordRepository;
            this.accOutRecordRepository = accOutRecordRepository;
            this.accOutDetailRecordRepository = accOutDetailRecordRepository;
            this.wipStoreDetilRepository = wipStoreDetilRepository;          
            this.reserveRepository = reserveRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.mcOutSourceOrderRepository = mcOutSourceOrderRepository;
            this.mcOutSourceOrderDetailRepository = mcOutSourceOrderDetailRepository;
            this.materialRepository = materialRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.giMaterialRepository = giMaterialRepository;
            this.compMaterialInfoRepository = compMaterialInfoRepository;
            this.pickingListRepository = pickingListRepository;
            this.giReserveRepository = giReserveRepository;
            this.supplierOrderDetailRepository = supplierOrderDetailRepository;
        }
         //附件库仓库编码
         public string accWhID = "0002";
         //外购单据类型
         public string outSourceBillType = "01";
         //当前用户
         public string LoginUserID = "201228";


        #region IAccStoreService 成员（附件库待入库一览）

        public IEnumerable GetAccInStoreBySearchByPage(VM_AccInStoreForSearch accInStoreForSearch, Paging paging)
        {
            return accInRecordRepository.GetAccInStoreBySearchByPage(accInStoreForSearch, paging);
        }

        #endregion

        #region IAccStoreService 成员


        public IEnumerable GetAccInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {
            //List<VM_AccInLoginStoreForTableShow> xx = new List<VM_AccInLoginStoreForTableShow>();
            //if (string.IsNullOrEmpty(deliveryOrderID) || string.IsNullOrEmpty(isetRepID))
            //{
            //    //return Content("未选择数据");
            //}
            //var deliveryOrderIDs = deliveryOrderID.Split(',');
            //var isetRepIDs = isetRepID.Split(',');
            //List<VM_AccInStoreForSearch> accInStoreForSearch = new List<VM_AccInStoreForSearch>();
            //for (int i = 0; i < deliveryOrderIDs.Count(); i++) {
            //    xx.Add(accInRecordRepository.GetAccInStoreForLoginBySearchByPage(deliveryOrderIDs[i], isetRepIDs[i], paging));
            //}
            //paging.total = xx.Count();
            // IEnumerable<VM_AccInLoginStoreForTableShow> result = xx.AsQueryable().ToPageList<VM_AccInLoginStoreForTableShow>("InDate desc", paging);
            // return result;
            return accInRecordRepository.GetAccInStoreForLoginBySearchByPage(deliveryOrderID, isetRepID, paging);
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览）


        public IEnumerable GetAccInRecordBySearchByPage(VM_AccInRecordStoreForSearch accInRecordStoreForSearch, Paging paging)
        {
            return accInRecordRepository.GetAccInRecordBySearchByPage(accInRecordStoreForSearch, paging);
        }

        #endregion

        /// <summary>
        /// 实例方法（测试登录页面产生方式）
        /// </summary>
        /// <param name="Parameter_id"></param>
        /// <returns></returns>
        /// 

        public IEnumerable GetWipStoreBySearchByPageTest(string mcIsetInListID, string isetRepID, Paging paging)
        {
            return accInRecordRepository.GetWipStoreBySearchByPageTest(mcIsetInListID, isetRepID, paging);
        }

        public bool UpdateWipStore(List<string> list)
        {
            foreach (var Id in list)
            {
                wipStoreRepository.updateWipStoreBySQL(new WipStore { Id = Id });
            }
            return true;
        }

        public bool AddAccStore(WipStoreDetil wipStoreDetil)
        {
            return wipStoreDetilRepository.Add(wipStoreDetil);
        }

        public bool UpdateWipStore(WipStore wipStore)
        {
            return wipStoreRepository.updateWipStore(wipStore);
        }

        #region IAccStoreService 成员(附件库入库登录功能)

        //于2013/11/26 yc 添加

        public bool AccInForLogin(List<VM_AccInLoginStoreForTableShow> accInLoginStore)
        {
            string pdtSpecState = "";

            foreach (var accInLoginStoreCopy in accInLoginStore)
            {
                //添加入库履历数据
                AccInForLoginAddInRecord(accInLoginStoreCopy);
                //添加批次别库存表
                AccInForLoginAddBthStockList(accInLoginStoreCopy);
                //修改进货检验表的入库状态
                //UpdatePurChkListForStoStat(accInLoginStoreCopy.IsetRepID,"1")


               
                #region 无规格型号的合格品
                if (accInLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(accInLoginStoreCopy.PdtSpec))
                {
                     pdtSpecState = "";
                    //修改仓库预约表和外购单明细表
                    AccInForLoginUpdateReserve(accInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    AccInForLoginUpdateMaterial(accInLoginStoreCopy);                   
                    return true;
                }
                else
                {

                }

                #endregion

                #region 有规格型号的合格品
                if (accInLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(accInLoginStoreCopy.PdtSpec))
                {
                    //修改仓库预约表、修改仓库预约详细表和外购单明细表
                    pdtSpecState = "0";
                    AccInForLoginUpdateReserve(accInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    AccInForLoginUpdateMaterial(accInLoginStoreCopy);
                    return true;
                }
                else
                {
                }

                #endregion

                #region 让步品
                if (accInLoginStoreCopy.GiCls!= "999")
                {
                    //添加让步仓库表
                    AccInForLoginAddGiMaterial(accInLoginStoreCopy);
                    return true;
                }
                else
                {
                }               
                #endregion
                //return true;
            }
            return true;
        }


        #endregion

        #region IAccStoreService 成员(附件库添加入库履历数据)

        public void AccInForLoginAddInRecord(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            //查询入库履历表中有无该数据
            AccInRecord accInRecord = new AccInRecord();
            accInRecord.DlvyListID = accInLoginStore.DeliveryOrderID;

            AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
            accInDetailRecord.McIsetInListID = accInLoginStore.McIsetInListID;
            accInDetailRecord.PdtID = accInLoginStore.PdtID;

            AccInRecord accInRecordCopy = accInRecordRepository.SelectAccInRecord(accInRecord);
            AccInDetailRecord accInDetailRecordCopy = accInDetailRecordRepository.SelectAccInDetailRecord(accInDetailRecord);
            if (accInRecordCopy == null)
            {
                //附件库入库履历添加           
                accInRecord.PrhaOdrID = accInLoginStore.PrhaOdrID;
                accInRecord.BthID = accInLoginStore.BthID;
                accInRecord.DlvyListID = accInLoginStore.DeliveryOrderID;
                accInRecord.WhID = accWhID;
                accInRecord.WhPosiID = "000";
                accInRecord.InMvCls = "00";
                accInRecord.McIsetInListID = accInLoginStore.DeliveryOrderID + accWhID;//根据送货单号+仓库编码生成
                accInRecord.CreUsrID = LoginUserID;
                accInRecordRepository.Add(accInRecord);
             
            }else{
            }
            if (accInDetailRecordCopy == null)
            {
                //附件库入库履历详细添加
                    accInDetailRecord.McIsetInListID = accInLoginStore.McIsetInListID;
                    accInDetailRecord.IsetRepID = accInLoginStore.IsetRepID;
                    accInDetailRecord.GiCls = accInLoginStore.GiCls;
                    accInDetailRecord.PdtName = accInLoginStore.PdtName;
                    accInDetailRecord.PdtSpec = accInLoginStore.PdtSpec;
                    accInDetailRecord.Qty = accInLoginStore.Qty;
                   //单价
                    if (accInLoginStore.AccLoginPriceFlg == "1")
                    {
                        accInDetailRecord.PrchsUp = accInLoginStore.PrchsUp;
                        accInDetailRecord.NotaxAmt = accInLoginStore.Qty * accInLoginStore.PrchsUp;
                    }
                    //估价
                    else {
                        accInDetailRecord.ValuatUp = accInDetailRecord.PrchsUp;
                        accInDetailRecord.NotaxAmt = accInLoginStore.Qty * accInLoginStore.ValuatUp;
                    }
                    
                    accInDetailRecord.WhkpID = "201228";//仓管员
                    accInDetailRecord.InDate = DateTime.Now;//格式YYYYMMDD
                    accInDetailRecord.CreDt = DateTime.Now;
                    accInDetailRecord.PrintFlg = "0";
                    accInDetailRecord.CreUsrID = LoginUserID;
                    accInDetailRecordRepository.Add(accInDetailRecord);
            }else { 
            }          
        }

        #endregion

        #region IAccStoreService 成员(附件库入库登录修改仓库预约表、修改仓库预约详细表和外购单明细表)

        public void AccInForLoginUpdateReserve(VM_AccInLoginStoreForTableShow accInLoginStore, string pdtSpecState)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accInLoginStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = accInLoginStore.PrhaOdrID;
            mcOutSourceOrderDetail.ProductPartID = accInLoginStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accInLoginStore.BthID;
            reserveDetail.PickOrdeQty = '0';
            reserveDetail.CmpQty = '0';

            var mcOutSourceOrderDetailForListAsc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListAsc(mcOutSourceOrderDetail);
            //入库剩余数量
            decimal inStoreSurplus = '0';
            inStoreSurplus = accInLoginStore.Qty;

            for (int i = 0; i < mcOutSourceOrderDetailForListAsc.Count(); i++)
            {
                mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;
                reser.ClnOdrID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                reser.ClnOdrDtl = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;

                //单据要求数量
                decimal requestQuantity = mcOutSourceOrderDetailForListAsc.ElementAt(i).RequestQuantity;
                //实际入库数量
                decimal actualQuantity = mcOutSourceOrderDetailForListAsc.ElementAt(i).ActualQuantity;
                if (requestQuantity > actualQuantity)
                {
                    if (requestQuantity - actualQuantity >= inStoreSurplus)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = inStoreSurplus;
                        reser.RecvQty = inStoreSurplus;
                        reserveDetail.OrderQty = inStoreSurplus;
                        inStoreSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        //本次剩余入库数量
                        inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                        mcOutSourceOrderDetail.ActualQuantity = requestQuantity - actualQuantity;
                        reser.RecvQty = requestQuantity - actualQuantity;
                        reserveDetail.OrderQty = requestQuantity - actualQuantity;
                    }

                    //该剩余即为该客户订单实际入库数量（外购单明细表）
                    //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）
                }
                else
                {
                }
                //修改外购单明细表
                mcOutSourceOrderDetail.UpdUsrID = LoginUserID;
                mcOutSourceOrderDetail.UpdDt = DateTime.Now;
                mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailActColumns(mcOutSourceOrderDetail);
                //修改仓库预约表
                reserveRepository.UpdateInReserveColumns(reser);

                //有规格型号的合格品修改仓库预约详细表
                if (pdtSpecState != "")
                {
                    var reserEntity = reserveRepository.SelectReserve(reser);
                    reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                    //创建人
                    reserveDetail.CreUsrID = LoginUserID;
                    reserveDetailRepository.Add(reserveDetail);
                }
                else
                {
                }
                if (forFlg == "0")
                {
                    break;
                }
                else
                {
                    //结束循环
                }
            }
           
        }

        #endregion

        #region IAccStoreService 成员(附件库入库登录修改外购单明细表)


        public void AccInForLoginUpdateMCOutSourceOrderDetail(VM_AccInLoginStoreForTableShow accInLoginStore, string pdtSpecState)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员(附件库入库登录修改仓库表)


        public void AccInForLoginUpdateMaterial(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            decimal unitPrice='0';
            decimal evaluate = '0';

            Material material = new Material();
            material.WhID = accWhID;
            material.PdtID = accInLoginStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var CnsmQty = materialCopy.CnsmQty;
                var TotalValuatUp=materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;

                //从外购单表外购单区分中判断有无客户订单
                MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInLoginStore.PrhaOdrID);
                //供货商供货信息表取的单价
                CompMaterialInfo compMaterialInfo = compMaterialInfoRepository.SelectCompMaterialInfoForPrice(accInLoginStore);
                if (compMaterialInfo != null)
                {
                    unitPrice = compMaterialInfo.UnitPrice;
                    evaluate = compMaterialInfo.Evaluate;
                }
                else
                {

                }             
                    if (mcOutSourceOrder.OutOrderFlg == "0")
                    {
                        //累加被预约数量
                        material.AlctQty = AlctQty + accInLoginStore.Qty;
                        //累加实际在库数量
                        material.CurrentQty = CurrentQty + accInLoginStore.Qty;
                        //可用在库数量
                        material.UseableQty = UseableQty;
                      
                    }

                         //否则累加可用在库数量 
                    else
                    {
                        //累加可用在库数量  
                        material.UseableQty = UseableQty + accInLoginStore.Qty;
                        //累加实际在库数量
                        material.CurrentQty = CurrentQty + accInLoginStore.Qty;
                        //被预约数量
                        material.AlctQty = AlctQty;
                    }
                    //供货商供货信息表判断单价取得
                    if (unitPrice != '0')
                    {
                        material.TotalAmt = TotalAmt + accInLoginStore.Qty * unitPrice;
                        material.TotalValuatUp = TotalValuatUp;
                    }
                    else if (unitPrice == '0' && evaluate != '0')
                    {
                        material.TotalValuatUp = TotalValuatUp + accInLoginStore.Qty * evaluate;
                        material.TotalAmt = TotalAmt;
                    }
                    else if (unitPrice == '0' && evaluate == '0')
                    {
                        material.TotalAmt = TotalAmt + '0';
                    }
                    material.CnsmQty = CnsmQty;
                    //修改人
                    material.UpdUsrID= LoginUserID;                  
                    materialRepository.updateMaterialForStoreLogin(material);
               
            }
            else
            {
                //仓库为空插入数据
                material.AlctQty = accInLoginStore.Qty;
                material.RequiteQty = '0';
                material.OrderQty = accInLoginStore.Qty;
                material.CnsmQty = '0';
                material.ArrveQty = accInLoginStore.Qty;
                material.IspcQty = accInLoginStore.Qty;
                material.UseableQty = accInLoginStore.Qty;
                material.CurrentQty = accInLoginStore.Qty;
                material.CreUsrID = LoginUserID;
                //供货商供货信息表判断单价取得
                if (unitPrice != '0')
                {
                    material.TotalAmt = accInLoginStore.Qty * unitPrice;
                    material.TotalValuatUp = '0';
                }
                else if (unitPrice == '0' && evaluate != '0')
                {
                    material.TotalValuatUp = accInLoginStore.Qty * evaluate;
                    material.TotalAmt = '0';
                }
                else if (unitPrice == '0' && evaluate == '0')
                {
                    material.TotalAmt = '0';
                }

                //总价
                materialRepository.Add(material);
            }
        }

        #endregion

        #region IAccStoreService 成员(附件库入库登录添加批次别库存表)

        public void AccInForLoginAddBthStockList(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.BillType = outSourceBillType;            
            bthStockList.BthID = accInLoginStore.BthID;
            bthStockList.WhID = accWhID;
            bthStockList.PdtID = accInLoginStore.PdtID;
            BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);
            bthStockList.PrhaOdrID = bthStockListCopy.PrhaOdrID;
            bthStockList.PdtSpec = bthStockListCopy.PdtSpec;
            bthStockList.GiCls = bthStockListCopy.GiCls;
            bthStockList.Qty = accInLoginStore.Qty;
            bthStockList.InDate = DateTime.Now;
            bthStockList.CreUsrID = LoginUserID;
      
                //从外购单表外购单区分中判断有无客户订单
                MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInLoginStore.PrhaOdrID);
                //客户有预定(从外购单表外购单区分判断有无客户订单)
                if (mcOutSourceOrder.OutOrderFlg == "0")
                {
                    bthStockList.OrdeQty = '0';
                }
                else
                {
                    bthStockList.OrdeQty = accInLoginStore.Qty;
                }
              
                bthStockListRepository.Add(bthStockList);
          
        }

        #endregion

        #region IAccStoreService 成员(附件库入库登录添加让步仓库表)

        public void AccInForLoginAddGiMaterial(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = accWhID;
            giMaterial.ProductID = accInLoginStore.PdtID;
            giMaterial.ProductSpec = accInLoginStore.PdtSpec;
            giMaterial.BatchID = accInLoginStore.BthID;
            giMaterial.AlctQuantity = '0';
            giMaterial.UserableQuantity = accInLoginStore.Qty;
            giMaterial.CurrentQuantity = accInLoginStore.Qty;
            giMaterial.GiCls = accInLoginStore.GiCls;
            giMaterial.CreUsrID = LoginUserID;
            if (accInLoginStore.AccLoginPriceFlg == "0")
            {
                giMaterial.TotalAmt = accInLoginStore.Qty * accInLoginStore.PrchsUp;
            }
            else
            {
              
            }

            giMaterialRepository.Add(giMaterial);

        }

        #endregion

        #region IAccStoreService 成员(入库履历一览删除功能)


        public string AccInRecordForDel(List<VM_AccInRecordStoreForTableShow> accInRecordStore)
        {
            string pdtSpecState = "";
            //删除
            foreach (var accInRecordStoreCopy in accInRecordStore)
            {              
                BthStockList bthStockList = new BthStockList();
                bthStockList.BillType = outSourceBillType;
                bthStockList.BthID = accInRecordStoreCopy.BthID;
                bthStockList.WhID = accWhID;
                bthStockList.PdtID = accInRecordStoreCopy.PdtID;
                //查询添加批次别库存表中有无该对象数据
                decimal CmpQty = '0';
                BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);
                if (bthStockListCopy != null)
                {
                    CmpQty = bthStockListCopy.CmpQty;

                    //查询批次别库存表是否有出库数量有则不执行以下删除
                    if (CmpQty == '0')
                    {
                        //无规格型号的合格品(1)
                        if (accInRecordStoreCopy.GiCls == "999" && string.IsNullOrEmpty(accInRecordStoreCopy.PdtSpec))
                        {
                            pdtSpecState = "";
                            //1.减去仓库预约表中的实际在库数量、减去外购单明细表中实际入库的数据                            
                            AccInRecordForDelReserve(accInRecordStoreCopy, pdtSpecState);
                            //2.减去仓库表中的数据
                            AccInRecordForDelMaterial(accInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        //有规格型号的合格品(2)
                        if (accInRecordStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(accInRecordStoreCopy.PdtSpec))
                        {
                            //1.减去仓库预约表中的实际在库数量、减去仓库预约详细表、减去外购单明细表中实际入库的数据
                            pdtSpecState = "0";
                            AccInRecordForDelReserve(accInRecordStoreCopy, pdtSpecState);
                            //2.减去仓库表中的数据
                            AccInRecordForDelMaterial(accInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        // 让步品(3)
                        if (accInRecordStoreCopy.GiCls != "999")
                        {
                            //1.删除让步仓库表该条数据
                            AccInRecordForDelGiMaterial(accInRecordStoreCopy);
                        }
                        else
                        {
                        }
                            //删去批次别库存表中的数据
                            bthStockList.DelUsrID = LoginUserID;
                            bthStockList.DelDt = DateTime.Now;
                            bthStockListRepository.DelBthStockList(bthStockList);
                            //删除履历中的数据
                            AccInRecordForDelInRecord(accInRecordStoreCopy);
                            //修改进货检验表的入库状态（假注释）
                            //UpdatePurChkListForStoStat(accInLoginStoreCopy.IsetRepID,"0")
                    }
                    else
                    {
                        return "提示所选行中有已出库数量";//提示所选行中有已出库数量（批次别库存表）
                    }

                }
                else
                {
                    //无该批次数据
                }
             
            }
            return "";
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览(删除功能)修改履历表）


        public void AccInRecordForDelInRecord(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
            accInDetailRecord.McIsetInListID = accInRecordStore.McIsetInListID;
            accInDetailRecord.PdtID = accInRecordStore.PdtID;
            accInDetailRecord.DelUsrID = LoginUserID;


            var accInDetailRecordForList = accInDetailRecordRepository.GetAccInDetailRecordForList(accInDetailRecord).ToList();
            if (accInDetailRecordForList.Count > 1)
            {
                //删除附件库入库履历详细中的数据
                accInDetailRecordRepository.AccInDetailRecordForDel(accInDetailRecord);
            }
            else
            {
                //删除附件库入库履历及附件库入库履历详细中的数据                 
                accInDetailRecordRepository.AccInDetailRecordForDel(accInDetailRecord);
                accInRecordRepository.AccInRecordForDel(new AccInRecord { DlvyListID = accInRecordStore.DeliveryOrderID, DelUsrID = LoginUserID });
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览(删除功能)修改仓库预约表、仓库预约详细表和外购单明细表）


        public void AccInRecordForDelReserve(VM_AccInRecordStoreForTableShow accInRecordStore,string pdtSpecState)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = accInRecordStore.PrhaOdrID;
            mcOutSourceOrderDetail.ProductPartID = accInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accInRecordStore.BthID;

            var mcOutSourceOrderDetailForListDesc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListDesc(mcOutSourceOrderDetail);
            //需删除的数量
            decimal delSurplus = '0';
            delSurplus = accInRecordStore.Qty;

            for (int i = 0; i < mcOutSourceOrderDetailForListDesc.Count(); i++)
            {
                mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;
                reser.ClnOdrID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                reser.ClnOdrDtl = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;
                //实际入库数量
                decimal actualQuantity = mcOutSourceOrderDetailForListDesc.ElementAt(i).ActualQuantity;
                if (actualQuantity > 0)
                {
                    //当前删除数据数量<=外购单实际入库数量
                    if (delSurplus <= actualQuantity)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = delSurplus;
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcOutSourceOrderDetail.ActualQuantity = actualQuantity;
                        reser.RecvQty = actualQuantity;

                    }

                    //修改外购单明细表
                    mcOutSourceOrderDetail.UpdUsrID = LoginUserID;
                    mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailForDelActColumns(mcOutSourceOrderDetail);

                    //修改仓库预约表
                    reserveRepository.UpdateReserveForDelRecvColumns(reser);

                    //有规格型号的合格品修改仓库预约详细表
                    if (pdtSpecState != "")
                    {
                        var reserEntity = reserveRepository.SelectReserve(reser);
                        reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                        reserveDetail.DelUsrID = LoginUserID;
                        //删除预约详细表中的该条数据
                        reserveDetailRepository.UpdateReserveDetailForDel(reserveDetail);
                    }
                    if (forFlg == "0")
                    {
                        break;
                    }
                    else
                    {
                        //结束循环
                    }
                }
                else
                {   //==================
                    //实际入库数量等于0
                }   //==================
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览（删除功能）修改仓库表）


        public void AccInRecordForDelMaterial(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            decimal prchsUp = '0';

            //仓库主键字段赋值
            Material material = new Material();
            material.WhID = accWhID;
            material.PdtID = accInRecordStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var CnsmQty = materialCopy.CnsmQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;
                //从外购单表外购单区分中判断有无客户订单
                MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInRecordStore.PrhaOdrID);
                //单价
                prchsUp = accInRecordStore.PrchsUp;
              
                    if (mcOutSourceOrder.OutOrderFlg == "0")
                    {
                        //减去被预约数量
                        material.AlctQty = AlctQty - accInRecordStore.Qty;
                        //减去实际在库数量
                        material.CurrentQty = CurrentQty - accInRecordStore.Qty;
                        //可用在库数量
                        material.UseableQty = UseableQty;                           
                    }
                    else
                    {
                        //减去可用在库数量    
                        material.UseableQty = UseableQty - accInRecordStore.Qty;
                        //减去实际在库数量
                        material.CurrentQty = CurrentQty - accInRecordStore.Qty;
                        //可用在库数量
                        material.AlctQty = AlctQty;                          
                    }
                    //履历表中取得单价
                    if (accInRecordStore.AccInRecordPriceFlg == "0")
                    {
                        material.TotalValuatUp = TotalValuatUp - accInRecordStore.Qty * prchsUp;
                        material.TotalAmt = TotalAmt;                      
                    }
                    else
                    {
                        material.TotalAmt = TotalAmt - accInRecordStore.Qty * prchsUp;
                        material.TotalValuatUp = TotalValuatUp;
                    }
                    material.CnsmQty = CnsmQty;
                    //修改人
                    material.UpdUsrID = LoginUserID;                    
                    materialRepository.updateMaterialForStoreLogin(material);                  
                
            }
            else
            {
                //没有该条数据不做操作
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览（删除功能）删除让步仓库表让步品操作）


        public void AccInRecordForDelGiMaterial(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            //让步仓库表字段赋值
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = accWhID;
            giMaterial.ProductID = accInRecordStore.PdtID;
            giMaterial.BatchID = accInRecordStore.BthID;
            giMaterialRepository.UpdateGiMaterialForDel(giMaterial);
        }

        #endregion

        #region IAccStoreService 成员(入库履历一览修改功能)


        public bool AccInRecordForUpdate(List<VM_AccInRecordStoreForTableShow> accInRecordStore)
        {
            string pdtSpecState = "";
            foreach (var accInRecordStoreCopy in accInRecordStore)
            {               
                AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
                accInDetailRecord.McIsetInListID = accInRecordStoreCopy.McIsetInListID;
                accInDetailRecord.PdtID = accInRecordStoreCopy.PdtID;
                AccInDetailRecord accInDetailRecordCopy= accInDetailRecordRepository.SelectAccInDetailRecord(accInDetailRecord);
                if (accInDetailRecordCopy != null)
                {
                    decimal oldQty = accInDetailRecordCopy.Qty;
                    decimal oldPrchsUp = accInDetailRecordCopy.PrchsUp;
                    decimal oldValuatUp = accInDetailRecordCopy.ValuatUp;
                    string oldRmrs = accInDetailRecordCopy.Rmrs;
                    //修改了数量或单价（单价和数量同时修改只针对让部品）
                    if (accInRecordStoreCopy.Qty != oldQty || accInRecordStoreCopy.AccInRecordPriceFlg == "1" && accInRecordStoreCopy.PrchsUp != oldPrchsUp ||
                        accInRecordStoreCopy.AccInRecordPriceFlg == "0" && accInRecordStoreCopy.PrchsUp != oldValuatUp || !accInRecordStoreCopy.Rmrs.Equals(oldRmrs))
                    {
                        //无规格型号的合格品(1)
                        if (accInRecordStoreCopy.GiCls == "999" && string.IsNullOrEmpty(accInRecordStoreCopy.PdtSpec))
                        {
                            //1.修改仓库预约表和外购单明细表
                            pdtSpecState = "";
                            AccInRecordForUpdateReserve(accInRecordStoreCopy, pdtSpecState);
                            //2.修改仓库表
                            AccInRecordForUpdateMaterial(accInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        //有规格型号的合格品(2)
                        if (accInRecordStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(accInRecordStoreCopy.PdtSpec))
                        {
                            pdtSpecState = "0";
                            //1.修改仓库预约表、仓库预约详细表和外购单明细表
                            AccInRecordForUpdateReserve(accInRecordStoreCopy, pdtSpecState);
                            //2.修改仓库表
                            AccInRecordForUpdateMaterial(accInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        // 让步品(3)
                        if (accInRecordStoreCopy.GiCls != "999")
                        {
                            //1.修改让步仓库表
                            AccInRecordForUpdateGiMaterial(accInRecordStoreCopy);

                        }
                        else
                        {
                        }
                        //修改批次别库存表
                        AccInRecordForUpdateBthStockList(accInRecordStoreCopy);
                        //修改履历表
                        AccInRecordForUpdateInRecord(accInRecordStoreCopy);
                    }
                    else
                    {
                        //没做修改
                    }
                }
                else
                {
                }
            }
            return true;
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览(修改功能)修改履历表）

        public void AccInRecordForUpdateInRecord(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            //初始化附件库入库履历详细对象并对属性赋值
            AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
            accInDetailRecord.McIsetInListID = accInRecordStore.McIsetInListID;
            accInDetailRecord.PdtID = accInRecordStore.PdtID;
            accInDetailRecord.Rmrs = accInRecordStore.PdtID;
            //从入库履历详细表中取出修改前的数量及单价
            AccInDetailRecord accInDetailRecordCopy = accInDetailRecordRepository.SelectAccInDetailRecord(accInDetailRecord);

            if (accInDetailRecordCopy != null)
            {
                decimal oldQty = accInDetailRecordCopy.Qty;
                decimal oldPrchsUp = accInDetailRecordCopy.PrchsUp;
                decimal oldValuatUp = accInDetailRecordCopy.ValuatUp;
                string oldRmrs = accInDetailRecordCopy.Rmrs;
                //修改了数量或单价（单价和数量同时修改只针对让部品）
                if (accInRecordStore.Qty != oldQty || accInRecordStore.AccInRecordPriceFlg == "1" && accInRecordStore.PrchsUp != oldPrchsUp ||
                    accInRecordStore.AccInRecordPriceFlg == "0" && accInRecordStore.PrchsUp != oldValuatUp || !accInRecordStore.Rmrs.Equals(oldRmrs))
                {
                    //decimal oldQty = accInDetailRecordCopy.Qty;
                    if (accInRecordStore.AccInRecordPriceFlg == "1")
                    {                        
                        accInDetailRecord.PrchsUp = accInRecordStore.PrchsUp;
                        accInDetailRecord.NotaxAmt = accInRecordStore.Qty * accInRecordStore.PrchsUp;
                    }
                    //估价
                    else
                    {
                        accInDetailRecord.ValuatUp = accInDetailRecord.PrchsUp;
                        accInDetailRecord.NotaxAmt = accInRecordStore.Qty * accInRecordStore.PrchsUp;
                    }
                    accInDetailRecord.Qty = accInRecordStore.Qty;
                    accInDetailRecord.Rmrs = accInRecordStore.Rmrs;
                    accInDetailRecord.UpdUsrID = LoginUserID;
                    //修改附件库入库履历详细中的数据
                    accInDetailRecordRepository.AccInRecordForUpdate(accInDetailRecord);

                }
                else
                {
                    //履历中无该条数据
                }
            }
            else
            {
                //履历中无该条数据
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外购单明细表）


        public void AccInRecordForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore,string pdtSpecState)
        {
            decimal addQty = '0';
            decimal delQty = '0';
            //初始化附件库入库履历详细对象并对属性赋值
            AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
            accInDetailRecord.McIsetInListID = accInRecordStore.McIsetInListID;
            accInDetailRecord.PdtID = accInRecordStore.PdtID;
            //从入库履历详细表中取出修改前的数量及单价
            AccInDetailRecord accInDetailRecordForQty = accInDetailRecordRepository.SelectAccInDetailRecord(accInDetailRecord);
            if (accInDetailRecordForQty != null)
            {
                //履历表中的物料数量
                decimal oldQty = accInDetailRecordForQty.Qty;

                if (oldQty < accInRecordStore.Qty)
                {
                    //设置添加数量
                    addQty = accInRecordStore.Qty - oldQty;
                    //执行添加操作
                    AccInRecordAddForUpdateReserve(accInRecordStore, pdtSpecState, addQty);

                }
                else
                {  
                    //设置减去数量
                    delQty = oldQty - accInRecordStore.Qty;
                    //执行减去操作
                    AccInRecordDelForUpdateReserve(accInRecordStore, pdtSpecState, delQty);
                }
            }
        }

        #endregion

        #region IAccStoreService 成员(附件库入库履历（修改功能)）修改仓库预约表、仓库预约详细表和外购单明细表（添加）)

        public void AccInRecordAddForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore, string pdtSpecState, decimal addQty)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = accInRecordStore.PrhaOdrID;
            mcOutSourceOrderDetail.ProductPartID = accInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accInRecordStore.BthID;

            var mcOutSourceOrderDetailForListAsc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListAsc(mcOutSourceOrderDetail);
            //入库剩余数量
            decimal inStoreSurplus = '0';
            inStoreSurplus = addQty;
       
            for (int i = 0; i < mcOutSourceOrderDetailForListAsc.Count(); i++)
            {
                mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;
                reser.ClnOdrID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                reser.ClnOdrDtl = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;

                //单据要求数量
                decimal requestQuantity = mcOutSourceOrderDetailForListAsc.ElementAt(i).RequestQuantity;
                //实际入库数量
                decimal actualQuantity = mcOutSourceOrderDetailForListAsc.ElementAt(i).ActualQuantity;
                if (requestQuantity > actualQuantity)
                {
                    if (requestQuantity - actualQuantity >= inStoreSurplus)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = inStoreSurplus;
                        reser.RecvQty = inStoreSurplus;
                        reserveDetail.OrderQty = inStoreSurplus;
                        inStoreSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        //本次剩余入库数量
                        inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                        mcOutSourceOrderDetail.ActualQuantity = requestQuantity - actualQuantity;
                        reser.RecvQty = requestQuantity - actualQuantity;
                        reserveDetail.OrderQty = requestQuantity - actualQuantity;
                    }

                    //该剩余即为该客户订单实际入库数量（外购单明细表）
                    //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）
                }
                else
                {
                }
                //修改外购单明细表
                mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailActColumns(mcOutSourceOrderDetail);
                //修改仓库预约表
                reserveRepository.UpdateInReserveColumns(reser);

                //有规格型号的合格品修改仓库预约详细表            
                if (pdtSpecState != "")
                {
                    var reserEntity = reserveRepository.SelectReserve(reser);
                    reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                    reserveDetail.UpdUsrID = LoginUserID;
                    reserveDetail.UpdDt = DateTime.Now;
                    reserveDetailRepository.UpdateReserveDetailForUpdate(reserveDetail);
                }
                else
                {
                }
                if (forFlg == "0")
                {
                    break;
                }
                else
                {
                    //结束循环
                }
            }
        }

        #endregion

        #region IAccStoreService 成员(附件库入库履历（修改功能)）修改仓库预约表、仓库预约详细表和外购单明细表（减去）)

        public void AccInRecordDelForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore, string pdtSpecState,decimal delQty)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = accInRecordStore.PrhaOdrID;
            mcOutSourceOrderDetail.ProductPartID = accInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accInRecordStore.BthID;

            var mcOutSourceOrderDetailForListDesc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListDesc(mcOutSourceOrderDetail);
            //需删除的数量
            decimal delSurplus = '0';
            delSurplus = delQty;

            for (int i = 0; i < mcOutSourceOrderDetailForListDesc.Count(); i++)
            {
                mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;
                reser.ClnOdrID = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                reser.ClnOdrDtl = mcOutSourceOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;

                MCOutSourceOrderDetail mcOutSourceOrderDetailCopy = mcOutSourceOrderDetailRepository.SelectMCOutSourceOrderDetail(mcOutSourceOrderDetail);
                Reserve reserCopy = reserveRepository.SelectReserve(reser);
                reserveDetail.OrdeBthDtailListID = reserCopy.OrdeBthDtailListID;
                ReserveDetail reserveDetailCopy = reserveDetailRepository.SelectReserveDetail(reserveDetail);

                //实际入库数量
                decimal actualQuantity = mcOutSourceOrderDetailForListDesc.ElementAt(i).ActualQuantity;

                //当数量减少的情况要确认实际入库数量是否为0，找到第一个不为0的客户订单后进行减少操作
                if (actualQuantity > 0)
                {
                    if (delSurplus <= actualQuantity)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = delSurplus;
                        reser.RecvQty = delSurplus;
                        reserveDetail.OrderQty = reserveDetailCopy.OrderQty - delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcOutSourceOrderDetail.ActualQuantity = mcOutSourceOrderDetailCopy.ActualQuantity;
                        reserveDetail.OrderQty = '0';
                        reser.RecvQty = reserCopy.RecvQty;
                    }

                    //修改外购单明细表
                    mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailForDelActColumns(mcOutSourceOrderDetail);

                    //修改仓库预约表
                    reserveRepository.UpdateReserveForDelRecvColumns(reser);
                }
                else
                {

                }
                //有规格型号的合格品修改仓库预约详细表
                if (pdtSpecState != "")
                {
                    reserveDetail.UpdUsrID = LoginUserID;
                    reserveDetail.UpdDt = DateTime.Now;
                    reserveDetailRepository.UpdateReserveDetailForUpdate(reserveDetail);
                }
                else
                {
                }
                if (forFlg == "0")
                {
                    break;
                }
                else
                {
                    //结束循环
                }
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览（修改功能）修改仓库表）


        public void AccInRecordForUpdateMaterial(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            decimal prchsUp = '0';
            decimal oldQty='0';

            //仓库主键字段赋值
            Material material = new Material();
            material.WhID = accWhID;
            material.PdtID = accInRecordStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var CnsmQty = materialCopy.CnsmQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;
                //从外购单表外购单区分中判断有无客户订单
                MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInRecordStore.PrhaOdrID);
                //初始化附件库入库履历详细对象并对属性赋值
                AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
                accInDetailRecord.McIsetInListID = accInRecordStore.McIsetInListID;
                accInDetailRecord.PdtID = accInRecordStore.PdtID;
        
                if (accInDetailRecord != null)
                {
                    //履历表中的物料数量
                    oldQty = accInRecordStore.Qty;
                    //供货商供货信息表取的单价
                    //单价
                    prchsUp = accInRecordStore.PrchsUp;

                    if (mcOutSourceOrder.OutOrderFlg == "0")
                    {
                        //减去被预约数量
                        material.AlctQty = AlctQty + accInRecordStore.Qty - oldQty;
                        //减去实际在库数量
                        material.CurrentQty = CurrentQty + accInRecordStore.Qty - oldQty;
                        //可用在库数量
                        material.UseableQty = UseableQty;

                    }
                    else
                    {
                        //减去可用在库数量    
                        material.UseableQty = UseableQty + accInRecordStore.Qty - oldQty;
                        //减去实际在库数量
                        material.CurrentQty = CurrentQty + accInRecordStore.Qty - oldQty;
                        //被预约数量
                        material.AlctQty = AlctQty;

                    }
                    if (accInRecordStore.AccInRecordPriceFlg == "0")
                    {
                        material.TotalValuatUp = TotalValuatUp + (accInRecordStore.Qty - oldQty) * prchsUp;
                        material.TotalAmt = TotalAmt;
                    }
                    else
                    {
                        material.TotalAmt = TotalAmt + (accInRecordStore.Qty - oldQty) * prchsUp;
                        material.TotalValuatUp = TotalValuatUp;
                    }
                    material.CnsmQty = CnsmQty;
                    material.UpdUsrID = LoginUserID;
                    materialRepository.updateMaterialForStoreLogin(material);

                }
                else
                {
                    //履历中无该条数据
                }
            }
            else
            {
                //仓库表中无该条数据，不做操作
            }
        }

        #endregion

        #region IAccStoreService 成员(附件库入库履历一览（修改功能）修改批次别库存表)


        public void AccInRecordForUpdateBthStockList(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.BillType = outSourceBillType;
            bthStockList.BthID = accInRecordStore.BthID;
            bthStockList.WhID = accWhID;
            bthStockList.PdtID = accInRecordStore.PdtID;
            bthStockList.CmpQty = '0';
            BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);
            //从外购单表外购单区分中判断有无客户订单
            MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInRecordStore.PrhaOdrID);
            if (bthStockListCopy != null)
            {
                if (mcOutSourceOrder.OutOrderFlg == "0")
                {
                    bthStockList.OrdeQty = accInRecordStore.Qty;
                }
                else
                {
                    bthStockList.OrdeQty = bthStockListCopy.Qty;
                }
                bthStockList.Qty = accInRecordStore.Qty;
                bthStockList.UpdUsrID = LoginUserID;
                bthStockListRepository.UpdateBthStockListForStore(bthStockList);
            }
            else
            {
                //无该条数据不做修改
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库入库履历一览（修改功能）修改让步仓库表）


        public void AccInRecordForUpdateGiMaterial(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = accWhID;
            giMaterial.ProductID = accInRecordStore.PdtID;
            giMaterial.BatchID = accInRecordStore.BthID;
            GiMaterial giMaterialCopy = giMaterialRepository.SelectGiMaterial(giMaterial);
            //从外购单表外购单区分中判断有无客户订单
            MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(accInRecordStore.PrhaOdrID);
            if (giMaterialCopy != null)
            {
               
                if (mcOutSourceOrder.OutOrderFlg == "0")
                {
                    giMaterial.AlctQuantity = accInRecordStore.Qty;
                }
                else
                {
                    if (accInRecordStore.Qty < giMaterialCopy.AlctQuantity)
                    {
                        giMaterial.UserableQuantity = '0';
                    }
                    else
                    {
                        giMaterial.UserableQuantity = accInRecordStore.Qty - giMaterialCopy.AlctQuantity;
                    }                    
                }              
                giMaterial.CurrentQuantity =  accInRecordStore.Qty;
                giMaterial.UpdUsrID = LoginUserID;
                if (accInRecordStore.AccInRecordPriceFlg == "0")
                {
                    giMaterial.TotalValuatUp = accInRecordStore.Qty * accInRecordStore.PrchsUp;
                    giMaterial.TotalAmt = '0';
                }
                else
                {
                    giMaterial.TotalAmt = accInRecordStore.Qty * accInRecordStore.PrchsUp;
                    giMaterial.TotalValuatUp = '0';
                }
                giMaterialRepository.UpdateGiMaterialForStore(giMaterial);
            }
            else { 
                 //无该条数据
            }

        }

        #endregion

        #region 待出库一览(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)待出库一览结果集
        /// </summary>
        /// <param name="accOutStoreForSearch">VM_AccOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetAccOutStoreBySearchByPage(VM_AccOutStoreForSearch accOutStoreForSearch, Paging paging)
        {
            return accOutRecordRepository.GetAccOutStoreBySearchByPage(accOutStoreForSearch, paging);
        } //end GetAccOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)出库单打印选择结果集
        /// </summary>
        /// <param name="accOutPrintForSearch">VM_AccOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetAccOutPrintBySearchByPage(VM_AccOutPrintForSearch accOutPrintForSearch, Paging paging)
        {
            return accOutRecordRepository.GetAccOutPrintBySearchByPage(accOutPrintForSearch, paging);
        } //end GetAccOutPrintBySearchByPage

        #endregion

        #region 材料领用出库单(附件库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_AccOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_AccOutPrintIndexForInfoShow GetAccOutPrintForInfoShow(string pickListID)
        {
            return accOutRecordRepository.GetAccOutPrintForInfoShow(pickListID);
        } //end GetAccOutPrintByInfoShow

        /// <summary>
        /// 根据 AccOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="accOutDetailRecordList">AccOutDetailRecord 泛型结果集</param>
        /// <returns>VM_AccOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public List<VM_AccOutPrintIndexForTableShow> GetAccOutPrintForTableShow(List<AccOutDetailRecord> accOutDetailRecordList)
        {
            List<VM_AccOutPrintIndexForTableShow> accOutPrintIndexForTableShowList = new List<VM_AccOutPrintIndexForTableShow>();

            foreach (AccOutDetailRecord accOutDetailRecord in accOutDetailRecordList)
            {
                accOutPrintIndexForTableShowList.Add(accOutRecordRepository.GetAccOutPrintForTableShow(accOutDetailRecord));
            }

            return accOutPrintIndexForTableShowList;

        } //GetAccOutPrintForTableShow

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览初始化页面）


        public IEnumerable GetAccOutRecordBySearchByPage(VM_AccOutRecordStoreForSearch accOutRecordStoreForSearch, Paging paging)
        {
            return accOutRecordRepository.GetAccOutRecordBySearchByPage(accOutRecordStoreForSearch, paging);
        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录画面数据表示）

        /// <summary>
        /// 附件库出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        public IEnumerable GetAccOutStoreForLoginBySearchByPage(string pickListID, string materielID,string pickListDetNo, Paging paging)
        {
            return accOutRecordRepository.GetAccOutStoreForLoginBySearchByPage(pickListID, materielID,pickListDetNo, paging);
        }

        #endregion

        #region IAccStoreService 成员(附件库出库登录业务)

        //于2013/11/27 yc 添加
        /// <summary>
        /// 附件库出库登录保存 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public bool AccOutForLogin(List<VM_AccOutLoginStoreForTableShow> accOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            string pdtSpecState = "";
            foreach (var accOutLoginStoreCopy in accOutLoginStore)
            {
                //添加出库履历数据
                //AccOutForLoginAddOutRecord(accOutLoginStoreCopy, selectOrderList);

                //更新生产领料单及外协领料单表
                AccOutForLoginUpdateMaterReq(accOutLoginStoreCopy);

            //    #region 无规格型号的合格品
            //    if (accOutLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(accOutLoginStoreCopy.PdtSpec))
            //    {
            //        pdtSpecState = "0";

            //        //修改仓库预约表
            //        AccOutForLoginUpdateReserve(accOutLoginStoreCopy, pdtSpecState, selectOrderList);
            //        //修改仓库表
            //        AccOutForLoginUpdateMaterial(accOutLoginStoreCopy, selectOrderList);
            //        //修改批次别库存表
            //        AccOutForLoginUpdateBthStockList(accOutLoginStoreCopy, pdtSpecState, selectOrderList);

            //        return true;
            //    }
            //    else
            //    {
            //    }

            //    #endregion

            //    #region 有规格型号的合格品
            //    if (accOutLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(accOutLoginStoreCopy.PdtSpec))
            //    {
            //        pdtSpecState = "1";
            //        //修改仓库预约表及仓库预约详细表
            //        AccOutForLoginUpdateReserve(accOutLoginStoreCopy, pdtSpecState, selectOrderList);
            //        //修改仓库表
            //        AccOutForLoginUpdateMaterial(accOutLoginStoreCopy, selectOrderList);
            //        //修改批次别库存表
            //        AccOutForLoginUpdateBthStockList(accOutLoginStoreCopy, pdtSpecState, selectOrderList);

            //        return true;
            //    }
            //    else
            //    {
            //    }

            //    #endregion

            //    #region 让步品
            //    if (accOutLoginStoreCopy.GiCls != "999")
            //    {
            //        pdtSpecState = "2";
            //        //修改让步仓库表
            //        AccOutForLoginUpdateGiMaterial(accOutLoginStoreCopy, selectOrderList);
            //        //修改让步仓库预约表
            //        AccOutForLoginUpdateGiReserve(accOutLoginStoreCopy, selectOrderList);
            //        //修改批次别库存表
            //        AccOutForLoginUpdateBthStockList(accOutLoginStoreCopy, pdtSpecState, selectOrderList);

            //        return true;
            //    }
            //    else
            //    {
            //    }
            //#endregion
            }
            return true;
            //throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录添加出库履历数据）

        /// <summary>
        /// 附件库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginAddOutRecord(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {

            //查询出库履历表中有无该数据
            AccOutRecord accOutRecord = new AccOutRecord();
            accOutRecord.PickListID = accOutLoginStore.PickListID;

            AccOutDetailRecord accOutDetailRecord = new AccOutDetailRecord();
            accOutDetailRecord.SaeetID = accOutLoginStore.SaeetID;
            accOutDetailRecord.PdtID = accOutLoginStore.MaterielID;

            AccOutRecord accOutRecordCopy = accOutRecordRepository.SelectAccOutRecord(accOutRecord);
            if (accOutRecordCopy == null)
            {
                //附件库出库履历添加           
                accOutRecord.PickListID = accOutLoginStore.PickListID;
                accOutRecord.WhID = accWhID;
                accOutRecord.OutMvCls = "00";
                accOutRecord.SaeetID = accOutLoginStore.SaeetID;
                accOutRecord.CallinUnitID = accOutLoginStore.CallinUnitID;
                accOutRecord.EffeFlag = "0";
                accOutRecord.DelFlag = "0";
                accOutRecord.CreDt = DateTime.Today;
                accOutRecord.CreUsrID = LoginUserID;

                accOutRecordRepository.Add(accOutRecord);

            }
            else
            {
            }

            for (int i = 0; i < selectOrderList.Length; i++)
            {
                if (accOutLoginStore.PickListID == selectOrderList[i]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[i]["PickListDetNo"])
                {

                    AccOutDetailRecord accOutDetailRecordCopy = accOutDetailRecordRepository.SelectAccOutDetailRecord(accOutDetailRecord, selectOrderList[i]["BthID"]);
                    if (accOutDetailRecordCopy == null)
                    {
                        accOutDetailRecord.SaeetID = accOutLoginStore.SaeetID;
                        accOutDetailRecord.BthID = selectOrderList[i]["BthID"];
                        accOutDetailRecord.PdtID = accOutLoginStore.MaterielID;
                        accOutDetailRecord.PdtName = accOutLoginStore.MaterielName;
                        accOutDetailRecord.PdtSpec = accOutLoginStore.PdtSpec;
                        accOutDetailRecord.GiCls = accOutLoginStore.GiCls;
                        accOutDetailRecord.TecnProcess = accOutLoginStore.TecnProcess;
                        accOutDetailRecord.Qty = Convert.ToDecimal(selectOrderList[i]["UserQty"]);
                        accOutDetailRecord.PrchsUp = Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                        accOutDetailRecord.SellPrc = 0;
                        accOutDetailRecord.NotaxAmt = Convert.ToDecimal(selectOrderList[i]["UserQty"]) * Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                        accOutDetailRecord.WhkpID = "201228";
                        accOutDetailRecord.OutDate = DateTime.Today;
                        accOutDetailRecord.Rmrs = accOutLoginStore.Rmrs;
                        accOutDetailRecord.EffeFlag = "0";
                        accOutDetailRecord.DelFlag = "0";
                        accOutDetailRecord.CreUsrID = LoginUserID;
                        accOutDetailRecord.CreDt = DateTime.Today;

                        accOutDetailRecordRepository.Add(accOutDetailRecord);
                    }
                    else
                    {
                    }

                }
            }

        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录修改仓库预约表、仓库预约详细表）

        /// <summary>
        /// 附件库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginUpdateReserve(VM_AccOutLoginStoreForTableShow accOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accOutLoginStore.MaterielID;
            reser.CmpQty = accOutLoginStore.Qty;
            reser.UpdDt = DateTime.Today;
            reser.UpdUsrID = LoginUserID;

            //仓库预约详细表字段赋值
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accOutLoginStore.BthID;
            reserveDetail.PickOrdeQty = accOutLoginStore.Qty;
            reserveDetail.CmpQty = accOutLoginStore.Qty;
            reserveDetail.UpdDt = DateTime.Today;
            reserveDetail.UpdUsrID = LoginUserID;

            if (accOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = accOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = accOutLoginStore.MaterielID;

                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail).ToList();

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count; i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    reser.ClnOdrID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    reser.ClnOdrDtl = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;

                    //修改仓库预约表
                    reserveRepository.UpdateInReserveCmpQty(reser);

                    //有规格型号的合格品修改仓库预约详细表
                    if (pdtSpecState == "1")
                    {
                        var reserEntity = reserveRepository.SelectReserve(reser);
                        reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;

                        for (int j = 0; j < selectOrderList.Length; j++)
                        {
                            if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                            {
                                reserveDetail.BthID = selectOrderList[j]["BthID"];
                                //修改仓库预约详细表
                                reserveDetailRepository.UpdateInReserveDetailQty(reserveDetail);
                            }

                        }

                    }
                    else
                    {
                    }
                }
            }
            else if (accOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = accOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = accOutLoginStore.PickListDetNo;

                var produceMaterDetailForListDesc = accOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
                for (int i = 0; i < produceMaterDetailForListDesc.Count; i++)
                {
                    produceMaterDetail.CustomerOrderNum = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    produceMaterDetail.CustomerOrderDetails = produceMaterDetailForListDesc[i].CustomerOrderDetails;
                    reser.ClnOdrID = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    reser.ClnOdrDtl = produceMaterDetailForListDesc[i].CustomerOrderDetails;

                    //修改仓库预约表
                    reserveRepository.UpdateInReserveCmpQty(reser);

                    //有规格型号的合格品修改仓库预约详细表
                    if (pdtSpecState == "1")
                    {
                        var reserEntity = reserveRepository.SelectReserve(reser);
                        reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;

                        for (int j = 0; j < selectOrderList.Length; j++)
                        {
                            if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                            {
                                reserveDetail.BthID = selectOrderList[j]["BthID"];
                                //修改仓库预约详细表
                                reserveDetailRepository.UpdateInReserveDetailQty(reserveDetail);
                            }
                        }

                    }
                    else
                    {
                    }
                }
            }

        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录修改仓库表）

        /// <summary>
        /// 附件库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginUpdateMaterial(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            Material material = new Material();
            material.WhID = accWhID;
            material.PdtID = accOutLoginStore.MaterielID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var CnsmQty = materialCopy.CnsmQty;
                //总价和估价总价
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;
                decimal totalPrchsUp = 0;  //单价总价
                decimal totalValuatUp = 0;  //估价总价
                for (int j = 0; j < selectOrderList.Length; j++)
                {
                    if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                    {
                        if (selectOrderList[j]["AccLoginPriceFlg"] == "1")
                        {
                            totalPrchsUp = totalPrchsUp + Convert.ToDecimal(selectOrderList[j]["UserQty"]) * Convert.ToDecimal(selectOrderList[j]["SellPrc"]);
                        }
                        else
                        {
                            totalValuatUp = totalValuatUp + Convert.ToDecimal(selectOrderList[j]["UserQty"]) * Convert.ToDecimal(selectOrderList[j]["SellPrc"]);
                        }
                    }

                }

                material.TotalAmt = TotalAmt - totalPrchsUp;  //更新总价
                material.TotalValuatUp = TotalValuatUp - totalValuatUp;  //更新估价总价

                //减去被预约数量
                material.AlctQty = AlctQty - accOutLoginStore.Qty;
                //减去实际在库数量
                material.CurrentQty = CurrentQty - accOutLoginStore.Qty;
                //修改外协取料数量
                material.CnsmQty = accOutLoginStore.Qty;
                //修改最终出库日
                material.LastWhoutYmd = DateTime.Today;

                //修改人
                material.UpdUsrID = LoginUserID;
                material.UpdDt = DateTime.Today;
                materialRepository.updateMaterialForOutLogin(material);

            }
        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录修改让步仓库表）

        /// <summary>
        /// 附件库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginUpdateGiMaterial(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = accWhID;
            giMaterial.ProductID = accOutLoginStore.MaterielID;
            GiMaterial giMaterialCopy = giMaterialRepository.WipSelectGiMaterial(giMaterial);
            if (giMaterialCopy != null)
            {
                var AlctQty = giMaterialCopy.AlctQuantity;
                var CurrentQty = giMaterialCopy.CurrentQuantity;
                //总价和估价总价
                var TotalValuatUp = giMaterialCopy.TotalValuatUp;
                var TotalAmt = giMaterialCopy.TotalAmt;

                decimal totalPrchsUp = 0;  //单价总价
                decimal totalValuatUp = 0;  //估价总价
                for (int j = 0; j < selectOrderList.Length; j++)
                {
                    if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                    {
                        if (selectOrderList[j]["AccLoginPriceFlg"] == "1")
                        {
                            totalPrchsUp = totalPrchsUp + Convert.ToDecimal(selectOrderList[j]["UserQty"]) * Convert.ToDecimal(selectOrderList[j]["SellPrc"]);
                        }
                        else
                        {
                            totalValuatUp = totalValuatUp + Convert.ToDecimal(selectOrderList[j]["UserQty"]) * Convert.ToDecimal(selectOrderList[j]["SellPrc"]);
                        }
                    }

                }

                giMaterial.TotalAmt = TotalAmt - totalPrchsUp;  //更新总价
                giMaterial.TotalValuatUp = TotalValuatUp - totalValuatUp;  //更新估价总价

                //减去被预约数量
                giMaterial.AlctQuantity = AlctQty - accOutLoginStore.Qty;
                //减去实际在库数量
                giMaterial.CurrentQuantity = CurrentQty - accOutLoginStore.Qty;
                //修改最终出库日
                giMaterial.LastWhOutYMD = DateTime.Today;

                //修改人
                giMaterial.UpdUsrID = LoginUserID;
                giMaterial.UpdDt = DateTime.Today;

                //修改让步仓库表
                giMaterialRepository.UpdateGiMaterialForOut(giMaterial);

            }
        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录修改让步仓库预约表）

        /// <summary>
        /// 附件库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginUpdateGiReserve(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiReserve giReserve = new GiReserve();
            giReserve.WareHouseID = accWhID;
            giReserve.ProductID = accOutLoginStore.MaterielID;
            giReserve.UpdUsrID = LoginUserID;

            if (accOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = accOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = accOutLoginStore.MaterielID;

                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail).ToList();

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count; i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    giReserve.PrhaOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    giReserve.ClientOrderDetail = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - accOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - accOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }
                    }

                }
            }
            else if (accOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = accOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = accOutLoginStore.PickListDetNo;

                var produceMaterDetailForListDesc = accOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
                for (int i = 0; i < produceMaterDetailForListDesc.Count; i++)
                {
                    produceMaterDetail.CustomerOrderNum = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    produceMaterDetail.CustomerOrderDetails = produceMaterDetailForListDesc[i].CustomerOrderDetails;
                    giReserve.PrhaOrderID = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    giReserve.ClientOrderDetail = produceMaterDetailForListDesc[i].CustomerOrderDetails;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - accOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - accOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }

                    }

                }
            }
        }
        #endregion

        #region IAccStoreService 成员（附件库出库登录修改批次别库存表）

        /// <summary>
        /// 附件库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void AccOutForLoginUpdateBthStockList(VM_AccOutLoginStoreForTableShow accOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.WhID = accWhID;
            bthStockList.PdtID = accOutLoginStore.MaterielID;
            bthStockList.UpdUsrID = LoginUserID;
            bthStockList.UpdDt = DateTime.Today;
            if (accOutLoginStore.OsSupProFlg == "000")
            {
                bthStockList.BillType = "01";
            }
            else if (accOutLoginStore.OsSupProFlg == "002")
            {
                bthStockList.BillType = "03";
            }

            for (int j = 0; j < selectOrderList.Length; j++)
            {
                if (accOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && accOutLoginStore.PickListDetNo == selectOrderList[j]["PickListDetNo"])
                {
                    bthStockList.BthID = selectOrderList[j]["BthID"];

                    BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);

                    if (bthStockListCopy != null)
                    {
                        var OrdeQty = bthStockListCopy.OrdeQty;
                        var CmpQty = bthStockList.CmpQty;

                        if (pdtSpecState == "2")
                        {
                            bthStockList.OrdeQty = OrdeQty;
                        }
                        else
                        {
                            bthStockList.OrdeQty = OrdeQty - accOutLoginStore.Qty;
                        }
                        bthStockList.CmpQty = CmpQty + accOutLoginStore.Qty;
                        //修改批次库存表
                        bthStockListRepository.UpdateBthStockListForOut(bthStockList);
                    }
                }
            }

        }

        #endregion

        #region IAccStoreService 成员（附件库出库登录修改生产领料单及外协领料单表）

        /// <summary>
        /// 附件库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        public void AccOutForLoginUpdateMaterReq(VM_AccOutLoginStoreForTableShow accOutLoginStore)
        {
            //来自外协
            if (accOutLoginStore.OsSupProFlg == "002")
            {
                MCSupplierCnsmInfo mcSupplierCnsmInfo = new MCSupplierCnsmInfo();
                mcSupplierCnsmInfo.MaterReqNo = accOutLoginStore.PickListID;
                mcSupplierCnsmInfo.MaterialsSpecReq = accOutLoginStore.PickListDetNo;
                mcSupplierCnsmInfo.ReceiveQuantity = accOutLoginStore.Qty;
                mcSupplierCnsmInfo.UpdUsrID = LoginUserID;
                mcSupplierCnsmInfo.UpdDt = DateTime.Today;

                accOutRecordRepository.UpdateSupplierCnsmInfoForOut(mcSupplierCnsmInfo);

            }
            //来自生产
            else if (accOutLoginStore.OsSupProFlg == "000")
            {
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = accOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = accOutLoginStore.PickListDetNo;
                produceMaterDetail.ReceQty = accOutLoginStore.Qty;
                produceMaterDetail.UpdUsrID = LoginUserID;
                produceMaterDetail.UpdDt = DateTime.Today;

                accOutRecordRepository.UpdateProduceMaterDetailForOut(produceMaterDetail);
            }
        }

        #endregion

        #region IAccStoreService 成员（出库批次选择画面初始化）

        /// <summary>
        /// 附件库出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        public IEnumerable SelectAccOutStoreForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging)
        {
            //if (osSupProFlg == "")
            //{
            //    return accOutRecordRepository.SelectAccOutRecordForBthSelect(qty, pickListID, paging);
            //}
            //来自生产
            if (osSupProFlg == "000")
            {
                var accOutRecordInfo = accOutRecordRepository.AccOutRecordInfo(pickListID, pickListDetNo);
                //没有指定批次号
                if (accOutRecordInfo.BthID == "")
                {
                    return accOutRecordRepository.SelectAccOutRecordProNForBthSelect(qty, pdtID, pickListID, pickListDetNo, osSupProFlg, paging);
                }
                else
                {
                    return accOutRecordRepository.SelectAccOutRecordProForBthSelect(qty, pickListID, pickListDetNo, paging);
                }

            }
            else
            {
                var accOutRecordSInfo = accOutRecordRepository.AccOutRecordSInfo(pickListID, pickListDetNo);
                //没有指定批次号
                if (accOutRecordSInfo.BatchID == "")
                {
                    return accOutRecordRepository.SelectAccOutRecordSupNForBthSelect(qty, pdtID, pickListID, pickListDetNo, osSupProFlg, paging);
                }
                else
                {
                    return accOutRecordRepository.SelectAccOutRecordSupForBthSelect(qty, pickListID, pickListDetNo, paging);
                }
            }
        }

        #endregion
     
        #region IAccStoreService 成员(附件库出库履历一览删除功能)

        public string AccOutRecordForDel(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore)
        {
            string pdtSpecState = "";
            string pdtSpecFlg = "";
            //删除
            foreach (var accOutRecordStoreCopy in accOutRecordStore)
            {
                BthStockList bthStockList = new BthStockList();
                bthStockList.BillType = outSourceBillType;
                bthStockList.BthID = accOutRecordStoreCopy.BthID;
                bthStockList.WhID = accWhID;
                bthStockList.PdtID = accOutRecordStoreCopy.MaterielID;
                //查询添加批次别库存表中有无该对象数据
                decimal oldCmpQty = '0';
                decimal oldOrdeQty = '0';
                BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);
                if (bthStockListCopy != null)
                {
                    //已出库数量
                    oldCmpQty = bthStockListCopy.CmpQty - accOutRecordStoreCopy.Qty;


                    //查询批次别库存表是否有出库数量有则不执行以下删除
                    //if (CmpQty == '0')
                    //{
                        //无规格型号的合格品(1)
                        if (accOutRecordStoreCopy.GiCls == "999" && string.IsNullOrEmpty(accOutRecordStoreCopy.PdtSpec))
                        {
                            pdtSpecState = "";
                            //1.减去仓库预约表中的已出库库数量                           
                            AccOutRecordForDelReserve(accOutRecordStoreCopy, pdtSpecState);
                            //2.增加仓库表中的数据
                            AccOutRecordForDelMaterial(accOutRecordStoreCopy);
                            //3.删除生产领料单详细表中的实领数量
                            AccOutRecordForDelProduceMaterDetail(accOutRecordStoreCopy);
                            //4.删除外协领料单信息表中的实领数量
                            AccOutRecordForDelMCSupplierCnsmInfo(accOutRecordStoreCopy);
                            //5. 修改批次别库存表中的预约数量和已出库数量
                            //预约数量
                            bthStockList.OrdeQty = bthStockListCopy.OrdeQty + accOutRecordStoreCopy.Qty;
                            bthStockListRepository.UpdateBthStockListForStore(bthStockList);
                        }
                        else
                        {
                        }
                        //有规格型号的合格品(2)
                        if (accOutRecordStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(accOutRecordStoreCopy.PdtSpec))
                        {
                            //1.减去仓库预约表中的已出库库数量、修改仓库预约详细表
                            pdtSpecState = "0";
                            AccOutRecordForDelReserve(accOutRecordStoreCopy, pdtSpecState);
                            //2.增加仓库表中的数据
                            AccOutRecordForDelMaterial(accOutRecordStoreCopy);
                            //3.删除生产领料单详细表中的实领数量
                            AccOutRecordForDelProduceMaterDetail(accOutRecordStoreCopy);
                            //4.删除外协领料单信息表中的实领数量
                            AccOutRecordForDelMCSupplierCnsmInfo(accOutRecordStoreCopy);
                            //5. 修改批次别库存表中的预约数量和已出库数量
                            //预约数量
                            bthStockList.OrdeQty = bthStockListCopy.OrdeQty + accOutRecordStoreCopy.Qty;
                            bthStockListRepository.UpdateBthStockListForStore(bthStockList);
                        }
                        else
                        {
                        }
                        // 让步品(3)
                        if (accOutRecordStoreCopy.GiCls != "999")
                        {                          
                            //1.修改让步仓库表该条数据
                            AccOutRecordForDelGiMaterial(accOutRecordStoreCopy);
                            //2.修改让步仓库预约表
                            AccOutRecordForDelGiReserve(accOutRecordStoreCopy);
                            //3. 修改批次别库存表中的已出库数量
                            //预约数量
                            bthStockList.OrdeQty = bthStockListCopy.OrdeQty;
                            bthStockListRepository.UpdateBthStockListForStore(bthStockList);
                        }
                        else
                        {
                        }
                       
                        //删除履历中的数据
                        AccOutRecordForDelInRecord(accOutRecordStoreCopy);
                        //修改进货检验表的入库状态（假注释）
                        //UpdatePurChkListForStoStat(accInLoginStoreCopy.IsetRepID,"0")
                    //}
                    //else
                    //{
                    //    return "提示所选行中有已出库数量";//提示所选行中有已出库数量（批次别库存表）
                    //}

                }
                else
                {
                    //无该批次数据
                }

            }
            return "";
        }

        #endregion

        #region IAccStoreService 成员(附件库出库履历一览(删除功能)修改履历表)


        public void AccOutRecordForDelInRecord(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            AccOutDetailRecord accOutDetailRecord = new AccOutDetailRecord();
            accOutDetailRecord.SaeetID = accOutRecordStore.SaeetID;
            accOutDetailRecord.PdtID = accOutRecordStore.MaterielID;
            accOutDetailRecord.PickListDetNo = accOutRecordStore.PickListDetNo;
            accOutDetailRecord.BthID = accOutRecordStore.BthID;
            accOutDetailRecord.DelUsrID = LoginUserID;


            var accOutDetailRecordForList = accOutDetailRecordRepository.GetAccOutDetailRecordForList(accOutDetailRecord).ToList();
            if (accOutDetailRecordForList.Count > 1)
            {
                //删除附件库出库履历详细中的数据
                accOutDetailRecordRepository.AccOutDetailRecordForDel(accOutDetailRecord);
            }
            else
            {
                //删除附件库出库履历及附件库出库履历详细中的数据                 
                accOutDetailRecordRepository.AccOutDetailRecordForDel(accOutDetailRecord);
                accOutRecordRepository.AccOutRecordForDel(new AccOutRecord { PickListID = accOutRecordStore.PickListID, DelUsrID = LoginUserID });
            }
        }

        #endregion

        #region IAccStoreService 成员(附件库出库履历一览（删除功能）删除生产领料单详细表)


        public void AccOutRecordForDelProduceMaterDetail(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            //假删除(根据参数领料单号、领料单详细号修改生产领料单详细表实领数量)
            //UpdateProduceMaterDetailForReceQty(accOutRecordStore.PickListID, accOutRecordStore.PickListDetNo, '0');
        }

        #endregion

        #region IAccStoreService 成员(附件库出库履历一览（删除功能）删除外协领料单信息表)


        public void AccOutRecordForDelMCSupplierCnsmInfo(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            //假删除(根据参数领料单号、领料单详细号修改外协领料单信息表实领数量)
            //UpdateMCSupplierCnsmInfoForReceQty(accOutRecordStore.PickListID, accOutRecordStore.PickListDetNo, '0');
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览(删除功能)修改仓库预约表、仓库预约详细表）


        public void AccOutRecordForDelReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore, string pdtSpecState)
        {
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = accWhID;
            reser.PdtID = accOutRecordStore.MaterielID;

            ////从外购单明细表主键字段赋值获得客户订单号，客户订单明细号
            //MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            //mcOutSourceOrderDetail.OutOrderID = accInRecordStore.PrhaOdrID;
            //mcOutSourceOrderDetail.ProductPartID = accInRecordStore.PdtID;
            //？？？？？？？未完成待修改

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = accOutRecordStore.BthID;

            //生产领料单详细表
            ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
            produceMaterDetail.MaterReqNo = accOutRecordStore.PickListID;
            produceMaterDetail.MaterReqDetailNo = accOutRecordStore.PickListDetNo;
            ProduceMaterDetail produceMaterDetailCopy = pickingListRepository.SelectProduceMaterDetail(produceMaterDetail);
            string proCustomerOrderNum = "";
            string proCustomerOrderDetails = "";
            //来自生产客户订单号
            proCustomerOrderNum = produceMaterDetailCopy.CustomerOrderNum;
            //来自生产客户订单明细
            proCustomerOrderDetails = produceMaterDetailCopy.CustomerOrderDetails;

            //外协领料单信息表
            MCSupplierCnsmInfo mcSupplierCnsmInfo = new MCSupplierCnsmInfo();
            mcSupplierCnsmInfo.MaterReqNo = accOutRecordStore.PickListID;
            mcSupplierCnsmInfo.No = accOutRecordStore.PickListDetNo;

            reser.ClnOdrID = proCustomerOrderNum;
            reser.ClnOdrDtl = proCustomerOrderDetails;
            //实领数量总和
            decimal totalReceQty = '0';
            totalReceQty = produceMaterDetailCopy.ReceQty;//还需加上外协实领数量
            if (totalReceQty > 0)
            {
                reser.RecvQty = totalReceQty;
                //修改仓库预约表
                reserveRepository.UpdateReserveForDelRecvColumns(reser);

                //有规格型号的合格品修改仓库预约详细表
                if (pdtSpecState != "")
                {
                    var reserEntity = reserveRepository.SelectReserve(reser);
                    reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                    reserveDetail.DelUsrID = LoginUserID;
                    //删除预约详细表中的该条数据
                    reserveDetailRepository.UpdateReserveDetailForDel(reserveDetail);
                }
            }
            else
            {   //==================
                //实际出库数量等于0
            }   //==================
        }

        #endregion

        #region IAccStoreService 成员（附件库出库库履历一览（删除功能）修改仓库表）


        public void AccOutRecordForDelMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            decimal prchsUp = '0';

            //仓库主键字段赋值
            Material material = new Material();
            material.WhID = accWhID;
            material.PdtID = accOutRecordStore.MaterielID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CnsmQty = materialCopy.CnsmQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;

                //此处不用判断判断有无客户订单             
                //单价
                prchsUp = accOutRecordStore.PrchsUp;

                //加上减去的被预约数量
                material.AlctQty = AlctQty + accOutRecordStore.Qty;
                if (accOutRecordStore.PickListTypeID == "04")
                {
                    material.CnsmQty = CnsmQty + accOutRecordStore.Qty;
                }
                else
                {
                    material.CnsmQty = CnsmQty;
                }
                
                //加上减去的实际在库数量
                material.CurrentQty = CurrentQty + accOutRecordStore.Qty;
                //可用在库数量
                material.UseableQty = UseableQty;

                material.TotalAmt = TotalAmt + accOutRecordStore.Qty * prchsUp;
                material.TotalValuatUp = TotalValuatUp;
                
                //修改人
                material.UpdUsrID = LoginUserID;
                materialRepository.updateMaterialForStoreLogin(material);

            }
            else
            {
                //没有该条数据不做操作
            }
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（删除功能）删除让步仓库表）


        public void AccOutRecordForDelGiMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（删除功能）删除让步仓库预约表）


        public void AccOutRecordForDelGiReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            //生产领料单详细表
            ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
            produceMaterDetail.MaterReqNo = accOutRecordStore.PickListID;
            produceMaterDetail.MaterReqDetailNo = accOutRecordStore.PickListDetNo;
            ProduceMaterDetail produceMaterDetailCopy = pickingListRepository.SelectProduceMaterDetail(produceMaterDetail);
            string proCustomerOrderNum = "";
            string proCustomerOrderDetails = "";
            //来自生产客户订单号
            proCustomerOrderNum = produceMaterDetailCopy.CustomerOrderNum;
            //来自生产客户订单明细
            proCustomerOrderDetails = produceMaterDetailCopy.CustomerOrderDetails;
            GiReserve giReserve = new GiReserve();
            giReserve.WareHouseID = accWhID;
            giReserve.PrhaOrderID = proCustomerOrderNum;
            giReserve.ClientOrderDetail = proCustomerOrderDetails;
            giReserve.ProductID = accOutRecordStore.MaterielID;
            giReserve.BatchID = accOutRecordStore.BthID;
            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
            giReserve.PickOrderQuantity = giReserveCopy.PickOrderQuantity + accOutRecordStore.Qty;
            giReserve.CmpQuantity = giReserveCopy.CmpQuantity + accOutRecordStore.Qty;
            giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);


        }

        #endregion
       
        #region IAccStoreService 成员(附件库出库履历一览修改功能)

        public bool AccOutRecordForUpdate(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore)
        {
            foreach (var accOutRecordStoreCopy in accOutRecordStore)
            {

            }
            return true;
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览(修改功能)修改履历表）


        public void AccOutRecordForUpdateInRecord(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（修改功能）修改生产领料单详细表）


        public void AccOutRecordForUpdateProduceMaterDetail(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            //假删除(根据参数领料单号、领料单详细号修改生产领料单详细表实领数量)
            //UpdateProduceMaterDetailForReceQty(accOutRecordStore.PickListID, accOutRecordStore.PickListDetNo, accOutRecordStore.Qty);
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（修改功能）修改外协领料单信息表）


        public void AccOutRecordForUpdateMCSupplierCnsmInfo(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            //假删除(根据参数领料单号、领料单详细号修改外协领料单信息表实领数量)
            //UpdateMCSupplierCnsmInfoForReceQty(accOutRecordStore.PickListID, accOutRecordStore.PickListDetNo, accOutRecordStore.Qty);
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览(修改功能)修改仓库预约表、仓库预约详细表）


        public void AccOutRecordForUpdateReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore, string pdtSpecState)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库库履历一览（修改功能）修改仓库表）


        public void AccOutRecordForUpdateMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（修改功能）修改让步仓库表）


        public void AccOutRecordForUpdateGiMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员（附件库出库履历一览（修改功能）修改让步仓库预约表）


        public void AccOutRecordForUpdateGiReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccStoreService 成员(查询产品单价)

        public decimal SelectCompMaterialInfoForPrice(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            decimal Price = '0';
            CompMaterialInfo compMaterialInfoForPriceList = compMaterialInfoRepository.SelectCompMaterialInfoForPrice(accInLoginStore);
            if (compMaterialInfoForPriceList != null)
            {
                var unitPrice = compMaterialInfoForPriceList.UnitPrice;
                var evaluate = compMaterialInfoForPriceList.Evaluate;
                if (unitPrice == 0 || unitPrice.ToString() == "")
                {
                    Price = evaluate;
                }
                else
                {
                    Price = unitPrice;
                }

            }
            return Price;
        }

        #endregion

        #region IAccStoreService 成员(入库单打印选择画面显示)


        public IEnumerable GetAccInPrintBySearchByPage(VM_AccInPrintForSearch accInPrintForSearch, Paging paging)
        {
          return accInRecordRepository.GetAccInPrintBySearchByPage(accInPrintForSearch, paging);
        }

        #endregion

        #region IAccStoreService 成员（物资验收入库单显示）


        public IEnumerable SelectForAccInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            return accInRecordRepository.SelectForAccInPrintPreview(pdtID, deliveryOrderID, paging);
        }

        #endregion

        #region IAccStoreService 成员（附件库履历删除暂用方法（一期测试））


        public string AccInRecordForDelTest(List<VM_AccInRecordStoreForTableShow> accInRecordStore)
        {
            foreach (var accInRecordStoreCopy in accInRecordStore)
            {
                AccInDetailRecord accInDetailRecord = new AccInDetailRecord();
                accInDetailRecord.McIsetInListID = accInRecordStoreCopy.McIsetInListID;
                accInDetailRecord.PdtID = accInRecordStoreCopy.PdtID;
                accInDetailRecord.DelUsrID = LoginUserID;


                var accInDetailRecordForList = accInDetailRecordRepository.GetAccInDetailRecordForList(accInDetailRecord).ToList();
                if (accInDetailRecordForList.Count > 1)
                {
                    //删除附件库入库履历详细中的数据
                    accInDetailRecordRepository.AccInDetailRecordForDel(accInDetailRecord);
                }
                else
                {
                    //删除附件库入库履历及附件库入库履历详细中的数据                 
                    accInDetailRecordRepository.AccInDetailRecordForDel(accInDetailRecord);
                    accInRecordRepository.AccInRecordForDel(new AccInRecord { DlvyListID = accInRecordStoreCopy.DeliveryOrderID, DelUsrID = LoginUserID });
                }
            }
            return "删除成功";
        }


        public string AccOutRecordForDelTest(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore)
        {
            foreach (var accOutRecordStoreCopy in accOutRecordStore)
            {
                AccOutDetailRecord accOutDetailRecord = new AccOutDetailRecord();
                accOutDetailRecord.SaeetID = accOutRecordStoreCopy.SaeetID;
                accOutDetailRecord.PdtID = accOutRecordStoreCopy.MaterielID;
                accOutDetailRecord.PickListDetNo = accOutRecordStoreCopy.PickListDetNo;
                accOutDetailRecord.BthID = accOutRecordStoreCopy.BthID;
                accOutDetailRecord.DelUsrID = LoginUserID;


                var accOutDetailRecordForList = accOutDetailRecordRepository.GetAccOutDetailRecordForList(accOutDetailRecord).ToList();
                if (accOutDetailRecordForList.Count > 1)
                {
                    //删除附件库出库履历详细中的数据
                    accOutDetailRecordRepository.AccOutDetailRecordForDel(accOutDetailRecord);
                }
                else
                {
                    //删除附件库出库履历及附件库出库履历详细中的数据                 
                    accOutDetailRecordRepository.AccOutDetailRecordForDel(accOutDetailRecord);
                    accOutRecordRepository.AccOutRecordForDel(new AccOutRecord { PickListID = accOutRecordStoreCopy.PickListID, DelUsrID = LoginUserID });
                }
            }
            return "删除成功";
        }
        #endregion

        #region IAccStoreService 成员（附件库入库登录暂用方法（一期测试）


        public bool AccInForLoginTest(List<VM_AccInLoginStoreForTableShow> accInLoginStore)
        {
            foreach (var accInLoginStoreCopy in accInLoginStore)
            {
                //修改进货检验表的入库状态
               accInRecordRepository.UpdatePurChkListForStoStat(accInLoginStoreCopy.IsetRepID, "1");
            }
            
            return true;
        }

        #endregion

    } //end AccStoreServiceImp
}
