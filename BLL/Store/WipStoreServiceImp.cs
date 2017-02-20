/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipStoreServiceImp.cs
// 文件功能描述：
//          仓库部门在制品库Service实现类、在制品库业务代码
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

    public class WipStoreServiceImp : AbstractService, IWipStoreService
    {

       //需要使用的Repository类
        private IWipStoreRepository wipStoreRepository;
        private IWipInRecordRepository wipInRecordRepository;
        private IAccInRecordRepository accInRecordRepository;//一期测试
        private IWipInDetailRecordRepository wipInDetailRecordRepository;
        private IWipOutRecordRepository wipOutRecordRepository;
        private IWipOutDetailRecordRepository wipOutDetailRecordRepository;
        private IBthStockListRepository bthStockListRepository;
        private IMCOutSourceOrderRepository mcOutSourceOrderRepository;
        private ISupplierOrderDetailRepository supplierOrderDetailRepository;
        private IReserveRepository reserveRepository;
        private IReserveDetailRepository reserveDetailRepository;
        private IMaterialRepository materialRepository;
        private ICompMaterialInfoRepository compMaterialInfoRepository;
        private IGiMaterialRepository giMaterialRepository;
        private IMCOutSourceOrderDetailRepository mcOutSourceOrderDetailRepository;
        public IGiReserveRepository giReserveRepository;
             

        //构造方法，必须要，参数为需要注入的属性
        public WipStoreServiceImp(IWipStoreRepository wipStoreRepository, IWipInRecordRepository wipInRecordRepository, IWipOutRecordRepository wipOutRecordRepository,
                                  IBthStockListRepository bthStockListRepository, IWipInDetailRecordRepository wipInDetailRecordRepository, IMCOutSourceOrderRepository mcOutSourceOrderRepository,
                                  ISupplierOrderDetailRepository supplierOrderDetailRepository, IReserveRepository reserveRepository, IReserveDetailRepository reserveDetailRepository,
                                  IMaterialRepository materialRepository, ICompMaterialInfoRepository compMaterialInfoRepository, IGiMaterialRepository giMaterialRepository,
                                  IMCOutSourceOrderDetailRepository mcOutSourceOrderDetailRepository,IWipOutDetailRecordRepository wipOutDetailRecordRepository,
                                  IGiReserveRepository giReserveRepository,IAccInRecordRepository accInRecordRepository) 
        {
            this.wipStoreRepository = wipStoreRepository;
            this.wipInRecordRepository = wipInRecordRepository;
            this.wipOutRecordRepository = wipOutRecordRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.wipInDetailRecordRepository = wipInDetailRecordRepository;
            this.mcOutSourceOrderRepository = mcOutSourceOrderRepository;
            this.supplierOrderDetailRepository = supplierOrderDetailRepository;
            this.reserveRepository = reserveRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.materialRepository = materialRepository;
            this.compMaterialInfoRepository = compMaterialInfoRepository;
            this.giMaterialRepository = giMaterialRepository;
            this.mcOutSourceOrderDetailRepository = mcOutSourceOrderDetailRepository;
            this.wipOutDetailRecordRepository = wipOutDetailRecordRepository;
            this.giReserveRepository = giReserveRepository;
            this.accInRecordRepository = accInRecordRepository;
        }

        //在制品库仓库编码
        public String wipWhID = "0102";
        //当前用户
        public string LoginUserID = "201228";
        //外购单据类型
        public string outSourceBillType = "01";


        #region IWipStoreService 成员（在制品库待入库一览初始化页面）

        public IEnumerable GetWipInStoreBySearchByPage(VM_WipInStoreForSearch wipInStoreForSearch, Paging paging)
        {
            return wipInRecordRepository.GetWipInStoreBySearchByPage(wipInStoreForSearch, paging);
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库登录画面数据表示）


        public IEnumerable GetWipInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {
            return wipInRecordRepository.GetWipInStoreForLoginBySearchByPage(deliveryOrderID, isetRepID, paging);
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库履历一览初始化页面)


        public IEnumerable GetWipInRecordBySearchByPage(VM_WipInRecordStoreForSearch wipInRecordStoreForSearch, Paging paging)
        {
            return wipInRecordRepository.GetWipInRecordBySearchByPage(wipInRecordStoreForSearch, paging);
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库登录业务）


        public bool WipInForLogin(List<VM_WipInLoginStoreForTableShow> wipInLoginStore)
        {
            string pdtSpecState = "";
            foreach (var wipInLoginStoreCopy in wipInLoginStore)
            {
                //添加入库履历数据
                WipInForLoginAddInRecord(wipInLoginStoreCopy);
                //添加批次别库存表
                WipInForLoginAddBthStockList(wipInLoginStoreCopy);
                //修改进货检验表的入库状态(假注释)
                //UpdatePurChkListForStoStat(wipInLoginStoreCopy.IsetRepID,"1")
                //修改过程检验表的入库状态
                //UpdateProcChkListForStoStat(wipInLoginStoreCopy.IsetRepID,"1")

                
                #region 无规格型号的合格品
                if (wipInLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(wipInLoginStoreCopy.PdtSpec))
                {
                    pdtSpecState = "";
                    //修改仓库预约表和外购单明细表和外协加工调度单详细表
                    WipInForLoginUpdateReserve(wipInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    WipInForLoginUpdateMaterial(wipInLoginStoreCopy);
                    return true;
                }
                else
                {

                }

                #endregion

                #region 有规格型号的合格品
                if (wipInLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(wipInLoginStoreCopy.PdtSpec))
                {
                    //修改仓库预约表、修改仓库预约详细表、外购单明细表和外协加工调度单详细表
                    pdtSpecState = "0";
                    WipInForLoginUpdateReserve(wipInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    WipInForLoginUpdateMaterial(wipInLoginStoreCopy);
                    return true;
                }
                else
                {
                }

                #endregion

                #region 让步品
                if (wipInLoginStoreCopy.GiCls != "999")
                {
                    //添加让步仓库表
                    WipInForLoginAddGiMaterial(wipInLoginStoreCopy);
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

        #region IWipStoreService 成员（在制品库入库登录添加入库履历数据）


        public void WipInForLoginAddInRecord(VM_WipInLoginStoreForTableShow wipInLoginStore)
        {
            //查询入库履历表中有无该数据
            WipInRecord wipInRecord = new WipInRecord();
            wipInRecord.DlvyListID = wipInLoginStore.DeliveryOrderID;

            WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
            wipInDetailRecord.TecnPdtInID = wipInLoginStore.McIsetInListID;
            wipInDetailRecord.PdtID = wipInLoginStore.PdtID;

            WipInRecord wipInRecordCopy = wipInRecordRepository.SelectWipInRecord(wipInRecord);
            WipInDetailRecord wipInDetailRecordCopy = wipInDetailRecordRepository.SelectWipInDetailRecord(wipInDetailRecord);
            if (wipInRecordCopy == null)
            {
                //附件库入库履历添加           
                wipInRecord.PlanID = wipInLoginStore.PlanID;
                wipInRecord.PlanCls = "";//？？？？
                wipInRecord.BthID = wipInLoginStore.BthID;
                wipInRecord.DlvyListID = wipInLoginStore.DeliveryOrderID;
                wipInRecord.WhID = wipWhID;
                wipInRecord.WhPosiID = "000";
                wipInRecord.InMvCls = "00";
                wipInRecord.TecnPdtInID = wipInLoginStore.DeliveryOrderID + wipWhID;//根据送货单号+仓库编码生成
                wipInRecord.ProcUnit = wipInLoginStore.ProcUnit;
                wipInRecord.CreUsrID = LoginUserID;
                wipInRecordRepository.Add(wipInRecord);

            }
            else
            {
            }
            if (wipInDetailRecordCopy == null)
            {
                //附件库入库履历详细添加
                wipInDetailRecord.TecnPdtInID = wipInLoginStore.McIsetInListID;
                wipInDetailRecord.IsetRepID = wipInLoginStore.IsetRepID;
                wipInDetailRecord.GiCls = wipInLoginStore.GiCls;
                wipInDetailRecord.PdtName = wipInLoginStore.PdtName;
                wipInDetailRecord.PdtSpec = wipInLoginStore.PdtSpec;
                wipInDetailRecord.TecnPdtInID = wipInLoginStore.TecnProcess;
                wipInDetailRecord.Qty = wipInLoginStore.Qty;
                wipInDetailRecord.ProScrapQty = wipInLoginStore.ProScrapQty;
                wipInDetailRecord.ProMaterscrapQty = wipInLoginStore.ProMaterscrapQty;
                wipInDetailRecord.ProOverQty = wipInLoginStore.ProOverQty;
                wipInDetailRecord.ProLackQty = wipInLoginStore.ProLackQty;
                wipInDetailRecord.ProTotalQty = wipInLoginStore.ProTotalQty;

                //单价
                if (wipInLoginStore.WipLoginPriceFlg == "1")
                {
                    wipInDetailRecord.PrchsUp = wipInLoginStore.PrchsUp;
                    wipInDetailRecord.NotaxAmt = wipInLoginStore.Qty * wipInLoginStore.PrchsUp;
                }
                //估价
                else
                {
                    wipInDetailRecord.ValuatUp = wipInDetailRecord.PrchsUp;
                    wipInDetailRecord.NotaxAmt = wipInLoginStore.Qty * wipInLoginStore.ValuatUp;
                }

                wipInDetailRecord.WhkpID = "201228";//仓管员？？？？？
                wipInDetailRecord.InDate = DateTime.Now;//格式YYYYMMDD
                wipInDetailRecord.CreDt = DateTime.Now;
                wipInDetailRecord.PrintFlg = "0";
                wipInDetailRecord.CreUsrID = LoginUserID;
                wipInDetailRecordRepository.Add(wipInDetailRecord);
            }
            else
            {
            }          
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库登录修改仓库预约表、修改仓库预约详细表、外购单明细表和外协加工调度单详细表）


        public void WipInForLoginUpdateReserve(VM_WipInLoginStoreForTableShow wipInLoginStore, string pdtSpecState)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = wipWhID;
            reser.PdtID = wipInLoginStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = wipInLoginStore.PlanID;
            mcOutSourceOrderDetail.ProductPartID = wipInLoginStore.PdtID;

            //修改外协加工调度单详细表（实际入库数量）主键字段赋值
            MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
            mcSupplierOrderDetail.SupOrderID = wipInLoginStore.PlanID;
            mcSupplierOrderDetail.ProductPartID = wipInLoginStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = wipInLoginStore.BthID;
            reserveDetail.PickOrdeQty = '0';
            reserveDetail.CmpQty = '0';

            //客户订单流转卡关系表(附初始值)
            List<CustomTranslateInfo> customTranslateInfoListAsc = new List<CustomTranslateInfo>();
            CustomTranslateInfo customTranslateInfo = new CustomTranslateInfo();
            customTranslateInfo.CustomerOrderNum = "";
            customTranslateInfo.CustomerOrderDetails = "";
            customTranslateInfo.WarehQty = 10;
            customTranslateInfo.PlnQty = 20;
            customTranslateInfoListAsc.Add(customTranslateInfo);
                        
            if (wipInLoginStore.OsSupProFlg == "000")
            {
                //来自生产（提供参数加工送货单号、零件ID获得客户订单号和客户订单明细号对应list）
                //假删除
                //var proListAsc = proRepository.GetCustomerOrderForListAsc(wipInLoginStore.DeliveryOrderID,wipInLoginStore.PdtID);

                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = wipInLoginStore.Qty;
                for (int i = 0; i < customTranslateInfoListAsc.Count; i++)
                {
                    customTranslateInfo.CustomerOrderNum = customTranslateInfoListAsc[i].CustomerOrderNum;//***
                    customTranslateInfo.CustomerOrderDetails = customTranslateInfoListAsc[i].CustomerOrderDetails;//***                  
                    reser.ClnOdrID = customTranslateInfoListAsc[i].CustomerOrderNum;
                    reser.ClnOdrDtl = customTranslateInfoListAsc[i].CustomerOrderDetails;

                    //单次交仓数量
                    decimal warehQty = customTranslateInfoListAsc[i].WarehQty;
                    if (inStoreSurplus < warehQty)
                    {
                        customTranslateInfo.WarehQty = inStoreSurplus;//***
                        reser.RecvQty = inStoreSurplus;
                        reserveDetail.OrderQty = inStoreSurplus;
                        inStoreSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        //本次剩余入库数量
                        inStoreSurplus = inStoreSurplus - warehQty;
                        customTranslateInfo.WarehQty = warehQty;//***
                        reser.RecvQty =  warehQty;
                        reserveDetail.OrderQty =  warehQty;
                    }

                        //该剩余即为该客户订单实际入库数量（外购单明细表）
                        //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）

                    //修改客户订单流转卡关系表(加注释)
                    //customTranslateInfoRepository.UpdateCustomTranslateInfo(customTranslateInfo);
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
                        //继续循环
                    }
                }
            }
            //来自外购
            else if (wipInLoginStore.OsSupProFlg == "001")
            {
                var mcOutSourceOrderDetailForListAsc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListAsc(mcOutSourceOrderDetail);
                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = wipInLoginStore.Qty;

                for (int i = 0; i < mcOutSourceOrderDetailForListAsc.Count(); i++)
                {
                    mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderID;//***
                    mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;//***                  
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
                            mcOutSourceOrderDetail.ActualQuantity = inStoreSurplus;//***
                            reser.RecvQty = inStoreSurplus;
                            reserveDetail.OrderQty = inStoreSurplus;
                            inStoreSurplus = '0';
                            forFlg = "0";
                        }
                        else
                        {
                            //本次剩余入库数量
                            inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                            mcOutSourceOrderDetail.ActualQuantity = requestQuantity - actualQuantity;//***
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
                        //继续循环
                    }
                }
            }
            //来自外协
            else if (wipInLoginStore.OsSupProFlg == "002")
            {
                var mcSupplierOrderDetailForListAsc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListAsc(mcSupplierOrderDetail);
                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = wipInLoginStore.Qty;

                for (int i = 0; i < mcSupplierOrderDetailForListAsc.Count(); i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;
                    reser.ClnOdrID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                    reser.ClnOdrDtl = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;

                    //单据要求数量
                    decimal requestQuantity = mcSupplierOrderDetailForListAsc.ElementAt(i).RequestQuantity;
                    //实际入库数量
                    decimal actualQuantity = mcSupplierOrderDetailForListAsc.ElementAt(i).ActualQuantity;
                    if (requestQuantity > actualQuantity)
                    {
                        if (requestQuantity - actualQuantity >= inStoreSurplus)
                        {
                            mcSupplierOrderDetail.ActualQuantity = inStoreSurplus;
                            reser.RecvQty = inStoreSurplus;
                            reserveDetail.OrderQty = inStoreSurplus;
                            inStoreSurplus = '0';
                            forFlg = "0";
                        }
                        else
                        {
                            //本次剩余入库数量
                            inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                            mcSupplierOrderDetail.ActualQuantity = requestQuantity - actualQuantity;
                            reser.RecvQty = requestQuantity - actualQuantity;
                            reserveDetail.OrderQty = requestQuantity - actualQuantity;
                        }

                        //该剩余即为该客户订单实际入库数量（外购单明细表）
                        //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）
                    }
                    else
                    {
                    }
                    //修改外协加工调度单详细表
                    supplierOrderDetailRepository.UpdateMCSupplierOrderDetailActColumns(mcSupplierOrderDetail);
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
                        //继续循环
                    }
                }
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库登录添加批次别库存表）


        public void WipInForLoginAddBthStockList(VM_WipInLoginStoreForTableShow wipInLoginStore)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.BillType = outSourceBillType;
            bthStockList.PrhaOdrID = wipInLoginStore.PlanID;
            bthStockList.BthID = wipInLoginStore.BthID;
            bthStockList.WhID = wipWhID;
            bthStockList.PdtID = wipInLoginStore.PdtID;
            bthStockList.PdtSpec = wipInLoginStore.PdtSpec;
            bthStockList.GiCls = wipInLoginStore.GiCls;
            bthStockList.Qty = wipInLoginStore.Qty;
            bthStockList.InDate = DateTime.Now;
            bthStockList.CreUsrID = LoginUserID;
            //bthStockList.EffeFlag = "0";
            //bthStockList.DelFlag = "0";

            //从外购单表外购单区分中判断有无客户订单
            //MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInLoginStore.PlanID);
            //客户有预定(从外购单表外购单区分判断有无客户订单)
           // if (mcOutSourceOrder.OutOrderFlg == "0")
            //{
                bthStockList.OrdeQty = wipInLoginStore.Qty;
                
            //}
            // else
            //{
            //  bthStockList.OrdeQty = '0';
            // }

            bthStockListRepository.Add(bthStockList);
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库登录修改仓库表）


        public void WipInForLoginUpdateMaterial(VM_WipInLoginStoreForTableShow wipInLoginStore)
        {
            decimal unitPrice = '0';
            decimal evaluate = '0';

            Material material = new Material();
            material.WhID = wipWhID;
            material.PdtID = wipInLoginStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;

                //从外购单表外购单区分中判断有无客户订单
               // MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInLoginStore.PlanID);
                //供货商供货信息表取的单价
                CompMaterialInfo compMaterialInfo = compMaterialInfoRepository.SelectCompMaterialInfoForPrice(wipInLoginStore);
                if (compMaterialInfo != null)
                {
                    unitPrice = compMaterialInfo.UnitPrice;
                    evaluate = compMaterialInfo.Evaluate;
                }
                else
                {

                }
               // if (mcOutSourceOrder.OutOrderFlg == "0")
               // {
                    //累加被预约数量
                    material.AlctQty = AlctQty + wipInLoginStore.Qty;
                    //累加实际在库数量
                    material.CurrentQty = CurrentQty + wipInLoginStore.Qty;
                    //可用在库数量
                    material.UseableQty = UseableQty;

               // }

                    //否则累加可用在库数量       //暂不考虑仓库自满足状态
                //else
                //{
                //    //累加可用在库数量  
                //    material.UseableQty = UseableQty + wipInLoginStore.Qty;
                //    //累加实际在库数量
                //    material.CurrentQty = CurrentQty + wipInLoginStore.Qty;
                //    //被预约数量
                //    material.AlctQty = AlctQty;
                //}
                //供货商供货信息表判断单价取得
                if (unitPrice != '0')
                {
                    material.TotalAmt = TotalAmt + wipInLoginStore.Qty * unitPrice;
                    material.TotalValuatUp = '0';
                }
                else if (unitPrice == '0' && evaluate != '0')
                {
                    material.TotalValuatUp = TotalValuatUp + wipInLoginStore.Qty * evaluate;
                    material.TotalAmt = '0';
                }
                else if (unitPrice == '0' && evaluate == '0')
                {
                    material.TotalAmt = TotalAmt + '0';
                }

                //修改人
                material.UpdUsrID = LoginUserID;
                materialRepository.updateMaterialForStoreLogin(material);

            }
            else
            {
                //仓库为空插入数据
                material.AlctQty = wipInLoginStore.Qty;
                material.RequiteQty = '0';
                material.OrderQty = wipInLoginStore.Qty;
                material.CnsmQty = '0';
                material.ArrveQty = wipInLoginStore.Qty;
                material.IspcQty = wipInLoginStore.Qty;
                material.UseableQty = wipInLoginStore.Qty;
                material.CurrentQty = wipInLoginStore.Qty;
                material.CreUsrID = LoginUserID;
                //供货商供货信息表判断单价取得
                if (unitPrice != '0')
                {
                    material.TotalAmt = wipInLoginStore.Qty * unitPrice;
                    material.TotalValuatUp = '0';
                }
                else if (unitPrice == '0' && evaluate != '0')
                {
                    material.TotalValuatUp = wipInLoginStore.Qty * evaluate;
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

        #region IWipStoreService 成员（在制品库入库登录添加让步仓库表）


        public void WipInForLoginAddGiMaterial(VM_WipInLoginStoreForTableShow wipInLoginStore)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = wipWhID;
            giMaterial.ProductID = wipInLoginStore.PdtID;
            giMaterial.ProductSpec = wipInLoginStore.PdtSpec;
            giMaterial.BatchID = wipInLoginStore.BthID;
            giMaterial.AlctQuantity = '0';
            giMaterial.UserableQuantity = wipInLoginStore.Qty;
            giMaterial.CurrentQuantity = wipInLoginStore.Qty;
            giMaterial.GiCls = wipInLoginStore.GiCls;
            giMaterial.CreUsrID = LoginUserID;
            if (wipInLoginStore.WipLoginPriceFlg == "0")
            {
                giMaterial.TotalAmt = wipInLoginStore.Qty * wipInLoginStore.PrchsUp;
            }
            else
            {

            }

            giMaterialRepository.Add(giMaterial);
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库履历一览删除功能)


        public string WipInRecordForDel(List<VM_WipInRecordStoreForTableShow> wipInRecordStore)
        {
            string pdtSpecState = "";
            //删除
            foreach (VM_WipInRecordStoreForTableShow wipInRecordStoreCopy in wipInRecordStore)
            {
                BthStockList bthStockList = new BthStockList();
                bthStockList.BillType = outSourceBillType;
                bthStockList.BthID = wipInRecordStoreCopy.BthID;
                bthStockList.WhID = wipWhID;
                bthStockList.PdtID = wipInRecordStoreCopy.PdtID;
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
                        if (wipInRecordStoreCopy.GiCls == "999" && string.IsNullOrEmpty(wipInRecordStoreCopy.PdtSpec))
                        {
                             pdtSpecState = "";
                             //1.减去仓库预约表中的实际在库数量、减去外购单明细表中实际入库的数据、减去外协加工调度单详细表中实际入库的数据
                            WipInRecordForDelReserve(wipInRecordStoreCopy, pdtSpecState);
                            //2.减去仓库表中的数据
                            WipInRecordForDelMaterial(wipInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        //有规格型号的合格品(2)
                        if (wipInRecordStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(wipInRecordStoreCopy.PdtSpec))
                        {
                            //1.减去仓库预约表中的实际在库数量、减去仓库预约详细表、减去外协加工调度单详细表中实际入库的数据
                            pdtSpecState = "0";
                            WipInRecordForDelReserve(wipInRecordStoreCopy, pdtSpecState);
                            //2.减去仓库表中的数据
                            WipInRecordForDelMaterial(wipInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        // 让步品(3)
                        if (wipInRecordStoreCopy.GiCls != "999")
                        {
                            //1.删除让步仓库表该条数据
                            WipInRecordForDelGiMaterial(wipInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        //删去批次别库存表中的数据
                        bthStockList.DelUsrID = LoginUserID;
                        bthStockList.DelDt = DateTime.Now;
                        bthStockListRepository.DelBthStockList(bthStockList);
                        //删除履历中的数据
                        WipInRecordForDelInRecord(wipInRecordStoreCopy);
                        //修改进货检验表的入库状态（假注释）
                        //UpdatePurChkListForStoStat(wipInRecordStoreCopy.IsetRepID,"0")
                        //修改过程检验表的入库状态
                        //UpdateProcChkListForStoStat(wipInRecordStoreCopy.IsetRepID,"0")
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

        #region IWipStoreService 成员（在制品库入库履历一览(删除功能)修改履历表）


        public void WipInRecordForDelInRecord(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
            wipInDetailRecord.TecnPdtInID = wipInRecordStore.McIsetInListID;
            wipInDetailRecord.PdtID = wipInRecordStore.PdtID;
            wipInDetailRecord.DelUsrID = LoginUserID;


            var wipInDetailRecordForList = wipInDetailRecordRepository.GetWipInDetailRecordForList(wipInDetailRecord).ToList();
            if (wipInDetailRecordForList.Count > 1)
            {
                //删除附件库入库履历详细中的数据
                wipInDetailRecordRepository.WipInDetailRecordForDel(wipInDetailRecord);
            }
            else
            {
                //删除附件库履历及附件库履历详细中的数据                 
                wipInDetailRecordRepository.WipInDetailRecordForDel(wipInDetailRecord);
                wipInRecordRepository.WipInRecordForDel(new WipInRecord { DlvyListID = wipInRecordStore.DeliveryOrderID, DelUsrID = LoginUserID });
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库履历一览(删除功能)修改仓库预约表、仓库预约详细表、外购单明细表和外协加工调度单详细表）


        public void WipInRecordForDelReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState)
        {
            string forFlg = "";

            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = wipWhID;
            reser.PdtID = wipInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = wipInRecordStore.PlanID;
            mcOutSourceOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //修改外协加工调度单详细表（实际入库数量）主键字段赋值
            MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
            mcSupplierOrderDetail.SupOrderID = wipInRecordStore.PlanID;
            mcSupplierOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = wipInRecordStore.BthID;


            //客户订单流转卡关系表(附初始值)
            List<CustomTranslateInfo> customTranslateInfoListDesc = new List<CustomTranslateInfo>();
            CustomTranslateInfo customTranslateInfo = new CustomTranslateInfo();
            customTranslateInfo.CustomerOrderNum = "";
            customTranslateInfo.CustomerOrderDetails = "";
            customTranslateInfo.WarehQty = 10;
            customTranslateInfo.PlnQty = 20;
            customTranslateInfoListDesc.Add(customTranslateInfo);

            if (wipInRecordStore.OsSupProFlg == "000")
            {
                //来自生产（提供参数加工送货单号、零件ID获得客户订单号和客户订单明细号对应list）
                //假删除
                //var proListAsc = proRepository.GetCustomerOrderForListDesc(wipInLoginStore.DeliveryOrderID,wipInLoginStore.PdtID);

                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = wipInRecordStore.Qty;
                for (int i = 0; i < customTranslateInfoListDesc.Count; i++)
                {
                    customTranslateInfo.CustomerOrderNum = customTranslateInfoListDesc[i].CustomerOrderNum;//***
                    customTranslateInfo.CustomerOrderDetails = customTranslateInfoListDesc[i].CustomerOrderDetails;//***                  
                    reser.ClnOdrID = customTranslateInfoListDesc[i].CustomerOrderNum;
                    reser.ClnOdrDtl = customTranslateInfoListDesc[i].CustomerOrderDetails;

                    //单次交仓数量
                    decimal warehQty = customTranslateInfoListDesc[i].WarehQty;
                    if (delSurplus <= warehQty)
                    {
                        customTranslateInfo.WarehQty = delSurplus;//***
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - warehQty;
                        customTranslateInfo.WarehQty = warehQty;//***
                        reser.RecvQty = warehQty;
                    }

                    //该剩余即为该客户订单实际入库数量（外购单明细表）                      
                    //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）
                 
                    //修改客户订单流转卡关系表(加注释)
                    //customTranslateInfoRepository.UpdateCustomTranslateInfo(customTranslateInfo);
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
                        //继续循环
                    }
                }
            }
            //来自外购
            else if (wipInRecordStore.OsSupProFlg == "001")
            {
                var mcOutSourceOrderDetailForListDesc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListDesc(mcOutSourceOrderDetail).ToList();
                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = wipInRecordStore.Qty;

                for (int i = 0; i < mcOutSourceOrderDetailForListDesc.Count; i++)
                {
                    mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderID;//***
                    mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderDetailID;//***
                    reser.ClnOdrID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderID;
                    reser.ClnOdrDtl = mcOutSourceOrderDetailForListDesc[i].CustomerOrderDetailID;
                    //实际入库数量
                    decimal actualQuantity = mcOutSourceOrderDetailForListDesc[i].ActualQuantity;
                    if (delSurplus <= actualQuantity)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = delSurplus;//***
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcOutSourceOrderDetail.ActualQuantity = actualQuantity;//***
                        reser.RecvQty = actualQuantity;

                    }
                    //修改外购单明细表
                    mcOutSourceOrderDetail.UpdUsrID = LoginUserID;

                    mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailForDelActColumns(mcOutSourceOrderDetail);//***
                    //修改仓库预约表
                    reserveRepository.UpdateReserveForDelRecvColumns(reser);
                    //有规格型号的合格品修改仓库预约详细表
                    if (pdtSpecState != "")
                    {
                        var reserEntity = reserveRepository.SelectReserve(reser);
                        reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                        //删除预约详细表中的该条数据
                        reserveDetailRepository.UpdateReserveDetailForDel(reserveDetail);
                    }
                    if (forFlg == "0")
                    {
                        break;
                    }
                    else
                    {
                        //继续循环
                    }
                }
            }
            //来自外协
            else if (wipInRecordStore.OsSupProFlg == "002")
            {
                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail).ToList();
                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = wipInRecordStore.Qty;

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count; i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    reser.ClnOdrID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    reser.ClnOdrDtl = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    //实际入库数量
                    decimal actualQuantity = mcSupplierOrderDetailForListDesc[i].ActualQuantity;
                    if (delSurplus <= actualQuantity)
                    {
                        mcSupplierOrderDetail.ActualQuantity = delSurplus;
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcSupplierOrderDetail.ActualQuantity = actualQuantity;
                        reser.RecvQty = actualQuantity;

                    }

                    //修改外协加工调度单详细表
                    supplierOrderDetailRepository.UpdateMCSupplierOrderDetailForDelActColumns(mcSupplierOrderDetail);
                    //有规格型号的合格品修改仓库预约详细表
                    if (pdtSpecState != "")
                    {
                        var reserEntity = reserveRepository.SelectReserve(reser);
                        reserveDetail.OrdeBthDtailListID = reserEntity.OrdeBthDtailListID;
                        //删除预约详细表中的该条数据
                        reserveDetailRepository.UpdateReserveDetailForDel(reserveDetail);
                    }
                  
                    if (forFlg == "0")
                    {
                        break;
                    }
                    else
                    {
                        //继续循环
                    }
                }
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库履历一览（删除功能）修改仓库表）


        public void WipInRecordForDelMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            decimal prchsUp = '0';

            //仓库主键字段赋值
            Material material = new Material();
            material.WhID = wipWhID;
            material.PdtID = wipInRecordStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;
                //从外购单表外购单区分中判断有无客户订单
                //MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInRecordStore.PlanID);
                //单价
                prchsUp = wipInRecordStore.PrchsUp;
               // if (mcOutSourceOrder.OutOrderFlg == "0")
                //{
                    //减去被预约数量
                    material.AlctQty = AlctQty - wipInRecordStore.Qty;
                    //减去实际在库数量
                    material.CurrentQty = CurrentQty - wipInRecordStore.Qty;
                    //可用在库数量
                    material.UseableQty = UseableQty;
               // }
               // else
                //{
                //    //减去可用在库数量    
                //    material.UseableQty = UseableQty - wipInRecordStore.Qty;
                //    //减去实际在库数量
                //    material.CurrentQty = CurrentQty - wipInRecordStore.Qty;
                //    //可用在库数量
                //    material.AlctQty = AlctQty;
                //}
                //履历表中取得单价
                if (wipInRecordStore.WipInRecordPriceFlg == "0")
                {
                    material.TotalValuatUp = TotalValuatUp - wipInRecordStore.Qty * prchsUp;
                    material.TotalAmt = TotalAmt;
                }
                else
                {
                    material.TotalAmt = TotalAmt - wipInRecordStore.Qty * prchsUp;
                    material.TotalValuatUp = TotalValuatUp;
                }

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

        #region IWipStoreService 成员（在制品库入库履历一览（删除功能）删除让步仓库表）


        public void WipInRecordForDelGiMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            //让步仓库表字段赋值
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = wipWhID;
            giMaterial.ProductID = wipInRecordStore.PdtID;
            giMaterial.BatchID = wipInRecordStore.BthID;
            giMaterialRepository.UpdateGiMaterialForDel(giMaterial);
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库履历一览修改功能)

        public bool WipInRecordForUpdate(List<VM_WipInRecordStoreForTableShow> wipInRecordStore)
        {
            string pdtSpecState = "";
            foreach (var wipInRecordStoreCopy in wipInRecordStore)
            {
                WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
                wipInDetailRecord.TecnPdtInID = wipInRecordStoreCopy.McIsetInListID;
                wipInDetailRecord.PdtID = wipInRecordStoreCopy.PdtID;
                WipInDetailRecord wipInDetailRecordCopy = wipInDetailRecordRepository.SelectWipInDetailRecord(wipInDetailRecord);
                if (wipInDetailRecordCopy != null)
                {
                    decimal oldQty = wipInDetailRecordCopy.Qty;
                    decimal oldPrchsUp = wipInDetailRecordCopy.PrchsUp;
                    decimal oldValuatUp = wipInDetailRecordCopy.ValuatUp;
                    string oldRmrs = wipInDetailRecordCopy.Rmrs;
                    //修改了数量或单价（单价和数量同时修改只针对让部品）
                    if (wipInRecordStoreCopy.Qty != oldQty || wipInRecordStoreCopy.WipInRecordPriceFlg == "1" && wipInRecordStoreCopy.PrchsUp != oldPrchsUp ||
                        wipInRecordStoreCopy.WipInRecordPriceFlg == "0" && wipInRecordStoreCopy.PrchsUp != oldValuatUp || !wipInRecordStoreCopy.Rmrs.Equals(oldRmrs))
                    {
                        //无规格型号的合格品(1)
                        if (wipInRecordStoreCopy.GiCls == "999" && string.IsNullOrEmpty(wipInRecordStoreCopy.PdtSpec))
                        {
                            //1.修改仓库预约表、外购单明细表和外协加工调度单详细表
                            pdtSpecState = "";
                            WipInRecordForUpdateReserve(wipInRecordStoreCopy, pdtSpecState);
                            //2.修改仓库表
                            WipInRecordForUpdateMaterial(wipInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        //有规格型号的合格品(2)
                        if (wipInRecordStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(wipInRecordStoreCopy.PdtSpec))
                        {
                            pdtSpecState = "0";
                            //1.修改仓库预约表、仓库预约详细表、外购单明细表和外协加工调度单详细表
                            WipInRecordForUpdateReserve(wipInRecordStoreCopy, pdtSpecState);
                            //2.修改仓库表
                            WipInRecordForUpdateMaterial(wipInRecordStoreCopy);
                        }
                        else
                        {
                        }
                        // 让步品(3)
                        if (wipInRecordStoreCopy.GiCls != "999")
                        {
                            //1.修改让步仓库表
                            WipInRecordForUpdateGiMaterial(wipInRecordStoreCopy);

                        }
                        else
                        {
                        }
                        //修改批次别库存表
                        WipInRecordForUpdateBthStockList(wipInRecordStoreCopy);
                        //修改履历表
                        WipInRecordForUpdateInRecord(wipInRecordStoreCopy);
                    }
                    else
                    {
                        //没做修改
                    }
                }
                else
                {
                    //履历中无该条数据
                }
            }
            return true;
        }

        #endregion

        #region IWipStoreService 成员（在制品入库履历一览(修改功能)修改履历表）


        public void WipInRecordForUpdateInRecord(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            //初始化附件库入库履历详细对象并对属性赋值
            WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
            wipInDetailRecord.TecnPdtInID = wipInRecordStore.TecnProcess;
            wipInDetailRecord.PdtID = wipInRecordStore.PdtID;
            //从入库履历详细表中取出修改前的数量及单价
            WipInDetailRecord wipInDetailRecordCopy = wipInDetailRecordRepository.SelectWipInDetailRecord(wipInDetailRecord);
            if (wipInDetailRecordCopy != null)
            {
                decimal oldQty = wipInDetailRecordCopy.Qty;
                decimal oldPrchsUp = wipInDetailRecordCopy.PrchsUp;
                decimal oldValuatUp = wipInDetailRecordCopy.ValuatUp;
                string oldRmrs = wipInDetailRecordCopy.Rmrs;

                //修改了数量或单价（单价和数量同时修改只针对让部品）
                if (wipInRecordStore.Qty != oldQty || wipInRecordStore.WipInRecordPriceFlg == "1" && wipInRecordStore.PrchsUp != oldPrchsUp ||
                    wipInRecordStore.WipInRecordPriceFlg == "0" && wipInRecordStore.PrchsUp != oldValuatUp || !wipInRecordStore.Rmrs.Equals(oldRmrs))
                {
                    //decimal oldQty = wipInDetailRecordCopy.Qty;
                    if (wipInRecordStore.WipInRecordPriceFlg == "1")
                    {
                        wipInDetailRecord.PrchsUp = wipInRecordStore.PrchsUp;
                        wipInDetailRecord.NotaxAmt = wipInRecordStore.Qty * wipInRecordStore.PrchsUp;
                    }
                    //估价
                    else
                    {
                        wipInDetailRecord.ValuatUp = wipInDetailRecord.PrchsUp;
                        wipInDetailRecord.NotaxAmt = wipInRecordStore.Qty * wipInRecordStore.PrchsUp;
                    }
                    wipInDetailRecord.Qty = wipInRecordStore.Qty;
                    wipInDetailRecord.Rmrs = wipInRecordStore.Rmrs;
                    wipInDetailRecord.UpdUsrID = LoginUserID;
                    //修改附件库入库履历详细中的数据
                    wipInDetailRecordRepository.WipInRecordForUpdate(wipInDetailRecord);

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

        #region IWipStoreService 成员（在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表、外购单明细表和外协加工调度单详细表）


        public void WipInRecordForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState)
        {
            decimal addQty = '0';
            decimal delQty = '0';
            //初始化附件库入库履历详细对象并对属性赋值
            WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
            wipInDetailRecord.TecnPdtInID = wipInRecordStore.McIsetInListID;
            wipInDetailRecord.PdtID = wipInRecordStore.PdtID;
            //从入库履历详细表中取出修改前的数量及单价
            WipInDetailRecord wipInDetailRecordForQty = wipInDetailRecordRepository.SelectWipInDetailRecord(wipInDetailRecord);
            if (wipInDetailRecordForQty != null)
            {
                //履历表中的物料数量
                decimal oldQty = wipInDetailRecordForQty.Qty;

                if (oldQty < wipInRecordStore.Qty)
                {
                    //设置添加数量
                    addQty = wipInRecordStore.Qty - oldQty;
                    //执行添加操作
                    WipInRecordAddForUpdateReserve(wipInRecordStore, pdtSpecState, addQty);

                }
                else
                {
                    //设置减去数量
                    delQty = oldQty - wipInRecordStore.Qty;
                    //执行减去操作
                    WipInRecordAddForUpdateReserve(wipInRecordStore, pdtSpecState, delQty);
                }
            }
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表（添加）)


        public void WipInRecordAddForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState, decimal addQty)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = wipWhID;
            reser.PdtID = wipInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = wipInRecordStore.PlanID;
            mcOutSourceOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //修改外协加工调度单详细表（实际入库数量）主键字段赋值
            MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
            mcSupplierOrderDetail.SupOrderID = wipInRecordStore.PlanID;
            mcSupplierOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = wipInRecordStore.BthID;

            //客户订单流转卡关系表(附初始值)
            List<CustomTranslateInfo> customTranslateInfoListAsc = new List<CustomTranslateInfo>();
            CustomTranslateInfo customTranslateInfo = new CustomTranslateInfo();
            customTranslateInfo.CustomerOrderNum = "";
            customTranslateInfo.CustomerOrderDetails = "";
            customTranslateInfo.WarehQty = 10;
            customTranslateInfo.PlnQty = 20;
            customTranslateInfoListAsc.Add(customTranslateInfo);

            if (wipInRecordStore.OsSupProFlg == "000")
            {
                //来自生产（提供参数加工送货单号、零件ID获得客户订单号和客户订单明细号对应list）
                //假删除
                //var proListAsc = proRepository.GetCustomerOrderForListAsc(wipInLoginStore.DeliveryOrderID,wipInLoginStore.PdtID);

                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = addQty;
                for (int i = 0; i < customTranslateInfoListAsc.Count; i++)
                {
                    customTranslateInfo.CustomerOrderNum = customTranslateInfoListAsc[i].CustomerOrderNum;//***
                    customTranslateInfo.CustomerOrderDetails = customTranslateInfoListAsc[i].CustomerOrderDetails;//***                  
                    reser.ClnOdrID = customTranslateInfoListAsc[i].CustomerOrderNum;
                    reser.ClnOdrDtl = customTranslateInfoListAsc[i].CustomerOrderDetails;

                    //单次交仓数量
                    decimal warehQty = customTranslateInfoListAsc[i].WarehQty;
                    if (inStoreSurplus < warehQty)
                    {
                        customTranslateInfo.WarehQty = inStoreSurplus;//***
                        reser.RecvQty = inStoreSurplus;
                        reserveDetail.OrderQty = inStoreSurplus;
                        inStoreSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        //本次剩余入库数量
                        inStoreSurplus = inStoreSurplus - warehQty;
                        customTranslateInfo.WarehQty = warehQty;//***
                        reser.RecvQty = warehQty;
                        reserveDetail.OrderQty = warehQty;
                    }

                    //该剩余即为该客户订单实际入库数量（外购单明细表）
                    //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）

                    //修改客户订单流转卡关系表(加注释)
                    //customTranslateInfoRepository.UpdateCustomTranslateInfo(customTranslateInfo);
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
                        //继续循环
                    }
                }
            }
             //来自外购
            else if (wipInRecordStore.OsSupProFlg == "001") {
                var mcOutSourceOrderDetailForListAsc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListAsc(mcOutSourceOrderDetail).ToList();
                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = addQty;

                for (int i = 0; i < mcOutSourceOrderDetailForListAsc.Count; i++)
                {
                    mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListAsc[i].CustomerOrderID;//***
                    mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListAsc[i].CustomerOrderDetailID;//***
                    reser.ClnOdrID = mcOutSourceOrderDetailForListAsc[i].CustomerOrderID;
                    reser.ClnOdrDtl = mcOutSourceOrderDetailForListAsc[i].CustomerOrderDetailID;

                    //单据要求数量
                    decimal requestQuantity = mcOutSourceOrderDetailForListAsc[i].RequestQuantity;
                    //实际入库数量
                    decimal actualQuantity = mcOutSourceOrderDetailForListAsc[i].ActualQuantity;
                    if (requestQuantity > actualQuantity)
                    {
                        if (requestQuantity - actualQuantity >= inStoreSurplus)
                        {
                            mcOutSourceOrderDetail.ActualQuantity = inStoreSurplus;//***
                            reser.RecvQty = inStoreSurplus;
                            reserveDetail.OrderQty = inStoreSurplus;
                            inStoreSurplus = '0';
                            forFlg = "0";
                        }
                        else
                        {
                            //本次剩余入库数量
                            inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                            mcOutSourceOrderDetail.ActualQuantity = requestQuantity - actualQuantity;//***
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
            //来自外协
            else if (wipInRecordStore.OsSupProFlg == "002")
            {
                var mcSupplierOrderDetailForListAsc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListAsc(mcSupplierOrderDetail);
                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = addQty;

                for (int i = 0; i < mcSupplierOrderDetailForListAsc.Count(); i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;
                    reser.ClnOdrID = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderID;
                    reser.ClnOdrDtl = mcSupplierOrderDetailForListAsc.ElementAt(i).CustomerOrderDetailID;

                    //单据要求数量
                    decimal requestQuantity = mcSupplierOrderDetailForListAsc.ElementAt(i).RequestQuantity;
                    //实际入库数量
                    decimal actualQuantity = mcSupplierOrderDetailForListAsc.ElementAt(i).ActualQuantity;
                    if (requestQuantity > actualQuantity)
                    {
                        if (requestQuantity - actualQuantity >= inStoreSurplus)
                        {
                            mcSupplierOrderDetail.ActualQuantity = inStoreSurplus;
                            reser.RecvQty = inStoreSurplus;
                            reserveDetail.OrderQty = inStoreSurplus;
                            inStoreSurplus = '0';
                            forFlg = "0";
                        }
                        else
                        {
                            //本次剩余入库数量
                            inStoreSurplus = inStoreSurplus - (requestQuantity - actualQuantity);
                            mcSupplierOrderDetail.ActualQuantity = requestQuantity - actualQuantity;
                            reser.RecvQty = requestQuantity - actualQuantity;
                            reserveDetail.OrderQty = requestQuantity - actualQuantity;
                        }

                        //该剩余即为该客户订单实际入库数量（外购单明细表）
                        //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）
                    }
                    else
                    {
                    }
                    //修改外协加工调度单详细表
                    supplierOrderDetailRepository.UpdateMCSupplierOrderDetailActColumns(mcSupplierOrderDetail);
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
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表（减去）)


        public void WipInRecordDDelForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState, decimal delQty)
        {
            string forFlg = "";

            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = wipWhID;
            reser.PdtID = wipInRecordStore.PdtID;

            //修改外购单明细表（实际入库数量）主键字段赋值
            MCOutSourceOrderDetail mcOutSourceOrderDetail = new MCOutSourceOrderDetail();
            mcOutSourceOrderDetail.OutOrderID = wipInRecordStore.PlanID;
            mcOutSourceOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //修改外协加工调度单详细表（实际入库数量）主键字段赋值
            MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
            mcSupplierOrderDetail.SupOrderID = wipInRecordStore.PlanID;
            mcSupplierOrderDetail.ProductPartID = wipInRecordStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = wipInRecordStore.BthID;


            //客户订单流转卡关系表(附初始值)
            List<CustomTranslateInfo> customTranslateInfoListDesc = new List<CustomTranslateInfo>();
            CustomTranslateInfo customTranslateInfo = new CustomTranslateInfo();
            customTranslateInfo.CustomerOrderNum = "";
            customTranslateInfo.CustomerOrderDetails = "";
            customTranslateInfo.WarehQty = 10;
            customTranslateInfo.PlnQty = 20;
            customTranslateInfoListDesc.Add(customTranslateInfo);

            if (wipInRecordStore.OsSupProFlg == "000")
            {
                //来自生产（提供参数加工送货单号、零件ID获得客户订单号和客户订单明细号对应list）
                //假删除
                //var proListAsc = proRepository.GetCustomerOrderForListDesc(wipInLoginStore.DeliveryOrderID,wipInLoginStore.PdtID);

                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = delQty;
                for (int i = 0; i < customTranslateInfoListDesc.Count; i++)
                {
                    customTranslateInfo.CustomerOrderNum = customTranslateInfoListDesc[i].CustomerOrderNum;//***
                    customTranslateInfo.CustomerOrderDetails = customTranslateInfoListDesc[i].CustomerOrderDetails;//***                  
                    reser.ClnOdrID = customTranslateInfoListDesc[i].CustomerOrderNum;
                    reser.ClnOdrDtl = customTranslateInfoListDesc[i].CustomerOrderDetails;

                    //单次交仓数量
                    decimal warehQty = customTranslateInfoListDesc[i].WarehQty;
                    if (delSurplus <= warehQty)
                    {
                        customTranslateInfo.WarehQty = delSurplus;//***
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - warehQty;
                        customTranslateInfo.WarehQty = warehQty;//***
                        reser.RecvQty = warehQty;
                    }

                    //该剩余即为该客户订单实际入库数量（外购单明细表）                      
                    //该剩余即为该客户订单实际入库数量+仓库剩余数量即直接锁库数量（仓库预约表）

                    //修改客户订单流转卡关系表(加注释)
                    //customTranslateInfoRepository.UpdateCustomTranslateInfo(customTranslateInfo);
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
                        //继续循环
                    }
                }
            }
             //来自外购
            else if (wipInRecordStore.OsSupProFlg == "001")
            {
                var mcOutSourceOrderDetailForListDesc = mcOutSourceOrderDetailRepository.GetMCOutSourceOrderDetailForListDesc(mcOutSourceOrderDetail).ToList();
                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = delQty;

                for (int i = 0; i < mcOutSourceOrderDetailForListDesc.Count; i++)
                {
                    mcOutSourceOrderDetail.CustomerOrderID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderID;//***
                    mcOutSourceOrderDetail.CustomerOrderDetailID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderDetailID;//***
                    reser.ClnOdrID = mcOutSourceOrderDetailForListDesc[i].CustomerOrderID;
                    reser.ClnOdrDtl = mcOutSourceOrderDetailForListDesc[i].CustomerOrderDetailID;

                    MCOutSourceOrderDetail mcOutSourceOrderDetailCopy = mcOutSourceOrderDetailRepository.SelectMCOutSourceOrderDetail(mcOutSourceOrderDetail);
                    Reserve reserCopy = reserveRepository.SelectReserve(reser);
                    reserveDetail.OrdeBthDtailListID = reserCopy.OrdeBthDtailListID;
                    ReserveDetail reserveDetailCopy = reserveDetailRepository.SelectReserveDetail(reserveDetail);
                    //实际入库数量
                    decimal actualQuantity = mcOutSourceOrderDetailForListDesc[i].ActualQuantity;
                    if (delSurplus <= actualQuantity)
                    {
                        mcOutSourceOrderDetail.ActualQuantity = delSurplus;//***
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcOutSourceOrderDetail.ActualQuantity = mcOutSourceOrderDetailCopy.ActualQuantity;//***
                        reser.RecvQty = actualQuantity;

                    }
                    //修改外购单明细表
                    mcOutSourceOrderDetailRepository.UpdateMCOutSourceOrderDetailForDelActColumns(mcOutSourceOrderDetail);
                    //修改仓库预约表
                    reserveRepository.UpdateReserveForDelRecvColumns(reser);

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
            //来自外协
             else if (wipInRecordStore.OsSupProFlg == "002")
            {
                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail);
                //需删除的数量
                decimal delSurplus = '0';
                delSurplus = delQty;

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count(); i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;
                    reser.ClnOdrID = mcSupplierOrderDetailForListDesc.ElementAt(i).CustomerOrderID;
                    reser.ClnOdrDtl = mcSupplierOrderDetailForListDesc.ElementAt(i).CustomerOrderDetailID;

                    MCOutSourceOrderDetail mcOutSourceOrderDetailCopy = mcOutSourceOrderDetailRepository.SelectMCOutSourceOrderDetail(mcOutSourceOrderDetail);
                    Reserve reserCopy = reserveRepository.SelectReserve(reser);
                    reserveDetail.OrdeBthDtailListID = reserCopy.OrdeBthDtailListID;
                    ReserveDetail reserveDetailCopy = reserveDetailRepository.SelectReserveDetail(reserveDetail);
                    //实际入库数量
                    decimal actualQuantity = mcSupplierOrderDetailForListDesc.ElementAt(i).ActualQuantity;
                    if (delSurplus <= actualQuantity)
                    {
                        mcSupplierOrderDetail.ActualQuantity = delSurplus;
                        reser.RecvQty = delSurplus;
                        delSurplus = '0';
                        forFlg = "0";
                    }
                    else
                    {
                        delSurplus = delSurplus - actualQuantity;
                        mcSupplierOrderDetail.ActualQuantity = actualQuantity;
                        reser.RecvQty = actualQuantity;
                    }                  
                    //修改外协加工调度单详细表
                    supplierOrderDetailRepository.UpdateMCSupplierOrderDetailForDelActColumns(mcSupplierOrderDetail);

                    //修改仓库预约表
                    reserveRepository.UpdateReserveForDelRecvColumns(reser);

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
             
        }

        #endregion

        #region IWipStoreService 成员（在制品入库履历一览（修改功能）修改仓库表）


        public void WipInRecordForUpdateMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {

            decimal prchsUp = '0';
            decimal oldQty = '0';

            //仓库主键字段赋值
            Material material = new Material();
            material.WhID = wipWhID;
            material.PdtID = wipInRecordStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;
                //从外购单表外购单区分中判断有无客户订单
                //MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInRecordStore.PlanID);
                //初始化附件库入库履历详细对象并对属性赋值
                WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
                wipInDetailRecord.TecnPdtInID = wipInRecordStore.McIsetInListID;
                wipInDetailRecord.PdtID = wipInRecordStore.PdtID;

                if (wipInDetailRecord != null)
                {
                    //履历表中的物料数量
                    oldQty = wipInRecordStore.Qty;
                    //供货商供货信息表取的单价
                    //单价
                    prchsUp = wipInRecordStore.PrchsUp;

                    //if (mcOutSourceOrder.OutOrderFlg == "0")
                    //{
                        //减去被预约数量
                        material.AlctQty = AlctQty + wipInRecordStore.Qty - oldQty;
                        //减去实际在库数量
                        material.CurrentQty = CurrentQty + wipInRecordStore.Qty - oldQty;
                        //可用在库数量
                        material.UseableQty = UseableQty;

                   // }
                    //else
                    //{
                    //    //减去可用在库数量    
                    //    material.UseableQty = UseableQty + wipInRecordStore.Qty - oldQty;
                    //    //减去实际在库数量
                    //    material.CurrentQty = CurrentQty + wipInRecordStore.Qty - oldQty;
                    //    //被预约数量
                    //    material.AlctQty = AlctQty;

                    //}
                    if (wipInRecordStore.WipInRecordPriceFlg == "0")
                    {
                        material.TotalValuatUp = TotalValuatUp + (wipInRecordStore.Qty - oldQty) * prchsUp;
                        material.TotalAmt = TotalAmt;
                    }
                    else
                    {
                        material.TotalAmt = TotalAmt + (wipInRecordStore.Qty - oldQty) * prchsUp;
                        material.TotalValuatUp = TotalValuatUp;
                    }
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

        #region IWipStoreService 成员（在制品入库履历一览（修改功能）修改批次别库存表）


        public void WipInRecordForUpdateBthStockList(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {

            BthStockList bthStockList = new BthStockList();
            bthStockList.BillType = outSourceBillType;
            bthStockList.BthID = wipInRecordStore.BthID;
            bthStockList.WhID = wipWhID;
            bthStockList.PdtID = wipInRecordStore.PdtID;
            BthStockList bthStockListCopy = bthStockListRepository.SelectBthStockList(bthStockList);
            //从外购单表外购单区分中判断有无客户订单
           // MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInRecordStore.PlanID);
            if (bthStockListCopy != null)
            {
                // if (mcOutSourceOrder.OutOrderFlg == "0")
                //{
                    bthStockList.OrdeQty = wipInRecordStore.Qty;
                //}
                //else
                //{
                // bthStockList.OrdeQty = bthStockListCopy.Qty;
                // }
                bthStockList.Qty = wipInRecordStore.Qty;
                bthStockList.UpdUsrID = LoginUserID;
                bthStockList.UpdDt = DateTime.Now;
                bthStockListRepository.UpdateBthStockListForStore(bthStockList);
            }
            else
            {
                //无该条数据不做修改
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库入库履历一览（修改功能）修改让步仓库表）


        public void WipInRecordForUpdateGiMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = wipWhID;
            giMaterial.ProductID = wipInRecordStore.PdtID;
            giMaterial.BatchID = wipInRecordStore.BthID;
            GiMaterial giMaterialCopy = giMaterialRepository.SelectGiMaterial(giMaterial);
            //从外购单表外购单区分中判断有无客户订单
            MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(wipInRecordStore.PlanID);
            if (giMaterialCopy != null)
            {

                if (mcOutSourceOrder.OutOrderFlg == "0")
                {
                    giMaterial.AlctQuantity = wipInRecordStore.Qty;
                }
                else
                {
                    if (wipInRecordStore.Qty < giMaterialCopy.AlctQuantity)
                    {
                        giMaterial.UserableQuantity = '0';
                    }
                    else
                    {
                        giMaterial.UserableQuantity = wipInRecordStore.Qty - giMaterialCopy.AlctQuantity;
                    }
                }
                giMaterial.CurrentQuantity = wipInRecordStore.Qty;
                giMaterial.UpdUsrID = LoginUserID;
                if (wipInRecordStore.WipInRecordPriceFlg == "0")
                {
                    giMaterial.TotalValuatUp = wipInRecordStore.Qty * wipInRecordStore.PrchsUp;
                    giMaterial.TotalAmt = '0';
                }
                else
                {
                    giMaterial.TotalAmt = wipInRecordStore.Qty * wipInRecordStore.PrchsUp;
                    giMaterial.TotalValuatUp = '0';
                }
                giMaterialRepository.UpdateGiMaterialForStore(giMaterial);
            }
            else
            {
                //无该条数据
            }
        }

        #endregion

        #region 待出库一览(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)待出库一览结果集
        /// </summary>
        /// <param name="wipOutStoreForSearch">VM_WipOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetWipOutStoreBySearchByPage(VM_WipOutStoreForSearch wipOutStoreForSearch, Paging paging)
        {
            return wipOutRecordRepository.GetWipOutStoreBySearchByPage(wipOutStoreForSearch, paging);
        } //end GetWipOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipOutPrintForSearch">VM_WipOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetWipOutPrintBySearchByPage(VM_WipOutPrintForSearch wipOutPrintForSearch, Paging paging)
        {
            return wipOutRecordRepository.GetWipOutPrintBySearchByPage(wipOutPrintForSearch, paging);

        } //end GetWipOutPrintBySearchByPage

        #endregion

        #region 材料领用出库单(在制品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_WipOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_WipOutPrintIndexForInfoShow GetWipOutPrintForInfoShow(string pickListID)
        {
            return wipOutRecordRepository.GetWipOutPrintForInfoShow(pickListID);
        } //end GetWipOutPrintByInfoShow

        /// <summary>
        /// 根据 WipOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="wipOutDetailRecordList">WipOutDetailRecord 泛型结果集</param>
        /// <returns>VM_WipOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public List<VM_WipOutPrintIndexForTableShow> GetWipOutPrintForTableShow(List<WipOutDetailRecord> wipOutDetailRecordList)
        {
            List<VM_WipOutPrintIndexForTableShow> wipOutPrintIndexForTableShowList = new List<VM_WipOutPrintIndexForTableShow>();

            foreach (WipOutDetailRecord wipOutDetailRecord in wipOutDetailRecordList)
            {
                wipOutPrintIndexForTableShowList.Add(wipOutRecordRepository.GetWipOutPrintForTableShow(wipOutDetailRecord));
            }

            return wipOutPrintIndexForTableShowList;

        } //GetWipOutPrintForTableShow

        #endregion

        #region IWipStoreService 成员（在制品出库登录画面数据表示）

        /// <summary>
        /// 在制品出库登录画面数据表示 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>在制品库出库登录画面数据集合</returns>
        public IEnumerable GetWipOutStoreForLoginBySearchByPage(string pickListID, string materielID,string materReqDetailNo, Paging paging)
        {
            return wipOutRecordRepository.GetWipOutStoreForLoginBySearchByPage(pickListID, materielID,materReqDetailNo,paging);
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录业务）

        /// <summary>
        /// 在制品出库登录保存 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public bool WipOutForLogin(List<VM_WipOutLoginStoreForTableShow> wipOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            string pdtSpecState = "";
            foreach (var wipOutLoginStoreCopy in wipOutLoginStore)
            {
                //添加出库履历数据
                //WipOutForLoginAddOutRecord(wipOutLoginStoreCopy, selectOrderList);
                
                //更新生产领料单及外协领料单表
                WipOutForLoginUpdateMaterReq(wipOutLoginStoreCopy);

                //#region 无规格型号的合格品
                //if (wipOutLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(wipOutLoginStoreCopy.PdtSpec))
                //{
                //    pdtSpecState = "0";
                //    //修改仓库预约表
                //    WipOutForLoginUpdateReserve(wipOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    //修改仓库表
                //    WipOutForLoginUpdateMaterial(wipOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    WipOutForLoginUpdateBthStockList(wipOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    return true;
                //}
                //else
                //{

                //}

                //#endregion

                //#region 有规格型号的合格品
                //if (wipOutLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(wipOutLoginStoreCopy.PdtSpec))
                //{
                //    pdtSpecState = "1";
                //    //修改仓库预约表及仓库预约详细表
                //    WipOutForLoginUpdateReserve(wipOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    //修改仓库表
                //    WipOutForLoginUpdateMaterial(wipOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    WipOutForLoginUpdateBthStockList(wipOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    return true;
                //}
                //else
                //{
                //}

                //#endregion

                //#region 让步品
                //if (wipOutLoginStoreCopy.GiCls != "999")
                //{
                //    pdtSpecState = "2";
                //    //修改让步仓库表
                //    WipOutForLoginUpdateGiMaterial(wipOutLoginStoreCopy, selectOrderList);
                //    //修改让步仓库预约表
                //    WipOutForLoginUpdateGiReserve(wipOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    WipOutForLoginUpdateBthStockList(wipOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    return true;
                //}
                //else
                //{
                //}
                //#endregion

            
            }
            return true;
            //throw new NotImplementedException();
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录添加出库履历数据）

        /// <summary>
        /// 在制品库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginAddOutRecord(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {

            //查询出库履历表中有无该数据
            WipOutRecord wipOutRecord = new WipOutRecord();
            wipOutRecord.PickListID = wipOutLoginStore.PickListID;
           
            WipOutDetailRecord wipOutDetailRecord = new WipOutDetailRecord();
            wipOutDetailRecord.TecnPdtOutID = wipOutLoginStore.SaeetID;
            wipOutDetailRecord.PdtID = wipOutLoginStore.MaterielID;

            WipOutRecord wipOutRecordCopy = wipOutRecordRepository.SelectWipOutRecord(wipOutRecord);
            if (wipOutRecordCopy == null)
            {
                //在制品库出库履历添加           
                wipOutRecord.PickListID = wipOutLoginStore.PickListID;
                wipOutRecord.WhID = wipWhID;
                wipOutRecord.OutMvCls = "00";
                wipOutRecord.TecnPdtOutID = wipOutLoginStore.SaeetID;
                wipOutRecord.CallinUnitID = wipOutLoginStore.CallinUnitID;
                wipOutRecord.EffeFlag = "0";
                wipOutRecord.DelFlag = "0";
                wipOutRecord.CreDt = DateTime.Today;
                wipOutRecord.CreUsrID = LoginUserID;

                wipOutRecordRepository.Add(wipOutRecord);

            }
            else
            { 
            }
            
            for (int i = 0; i < selectOrderList.Length; i++)
                {
                    if (wipOutLoginStore.PickListID == selectOrderList[i]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[i]["MaterReqDetailNo"])
                    {

                        WipOutDetailRecord wipOutDetailRecordCopy = wipOutDetailRecordRepository.SelectWipOutDetailRecord(wipOutDetailRecord, selectOrderList[i]["BthID"]);
                        if (wipOutDetailRecordCopy == null)
                        {
                            wipOutDetailRecord.TecnPdtOutID = wipOutLoginStore.SaeetID;
                            wipOutDetailRecord.BthID = selectOrderList[i]["BthID"];
                            wipOutDetailRecord.PdtID = wipOutLoginStore.MaterielID;
                            wipOutDetailRecord.PdtName = wipOutLoginStore.MaterielName;
                            wipOutDetailRecord.PdtSpec = wipOutLoginStore.PdtSpec;
                            wipOutDetailRecord.GiCls = wipOutLoginStore.GiCls;
                            wipOutDetailRecord.TecnProcess = wipOutLoginStore.TecnProcess;
                            wipOutDetailRecord.Qty = Convert.ToDecimal(selectOrderList[i]["UserQty"]);
                            wipOutDetailRecord.PrchsUp = Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                            wipOutDetailRecord.NotaxAmt = Convert.ToDecimal(selectOrderList[i]["UserQty"]) * Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                            wipOutDetailRecord.WhkpID = "201228";
                            wipOutDetailRecord.OutDate = DateTime.Today;
                            wipOutDetailRecord.Rmrs = wipOutLoginStore.Rmrs;
                            wipOutDetailRecord.EffeFlag = "0";
                            wipOutDetailRecord.DelFlag = "0";
                            wipOutDetailRecord.CreUsrID = LoginUserID;
                            wipOutDetailRecord.CreDt = DateTime.Today;

                            wipOutDetailRecordRepository.Add(wipOutDetailRecord);
                        }
                        else
                        {
                        }

                    }   
            }
             
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录修改仓库预约表及仓库预约详细表）

        /// <summary>
        /// 在制品库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginUpdateReserve(VM_WipOutLoginStoreForTableShow wipOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {
           
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = wipWhID;
            reser.PdtID = wipOutLoginStore.MaterielID;
            reser.CmpQty = wipOutLoginStore.Qty;
            reser.UpdDt=DateTime.Today;
            reser.UpdUsrID=LoginUserID;

            //仓库预约详细表字段赋值
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = wipOutLoginStore.BthID;
            reserveDetail.PickOrdeQty=wipOutLoginStore.Qty;
            reserveDetail.CmpQty=wipOutLoginStore.Qty;
            reserveDetail.UpdDt=DateTime.Today;
            reserveDetail.UpdUsrID=LoginUserID;

            if (wipOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = wipOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = wipOutLoginStore.MaterielID;

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
                            if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
            else if (wipOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = wipOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = wipOutLoginStore.MaterReqDetailNo;

                var produceMaterDetailForListDesc = wipOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
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
                            if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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

        #region IWipStoreService 成员（在制品库出库登录修改仓库表）

        /// <summary>
        /// 在制品库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginUpdateMaterial(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            Material material = new Material();
            material.WhID = wipWhID;
            material.PdtID = wipOutLoginStore.MaterielID;
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
                    if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                    {
                        if (selectOrderList[j]["WipLoginPriceFlg"] == "1")
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
                material.AlctQty = AlctQty - wipOutLoginStore.Qty;
                //减去实际在库数量
                material.CurrentQty = CurrentQty - wipOutLoginStore.Qty;
                //修改外协取料数量
                material.CnsmQty = wipOutLoginStore.Qty;
                //修改最终出库日
                material.LastWhoutYmd = DateTime.Today;
               
                //修改人
                material.UpdUsrID = LoginUserID;
                material.UpdDt=DateTime.Today;
                materialRepository.updateMaterialForOutLogin(material);

            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录修改让步仓库表）

        /// <summary>
        /// 在制品库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginUpdateGiMaterial(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = wipWhID;
            giMaterial.ProductID = wipOutLoginStore.MaterielID;
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
                    if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                    {
                        if (selectOrderList[j]["WipLoginPriceFlg"] == "1")
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
                giMaterial.AlctQuantity = AlctQty - wipOutLoginStore.Qty;
                //减去实际在库数量
                giMaterial.CurrentQuantity = CurrentQty - wipOutLoginStore.Qty;
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

        #region IWipStoreService 成员（在制品库出库登录修改让步仓库预约表）

        /// <summary>
        /// 在制品库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginUpdateGiReserve(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiReserve giReserve = new GiReserve();
            giReserve.WareHouseID = wipWhID;
            giReserve.ProductID = wipOutLoginStore.MaterielID;
            giReserve.UpdUsrID = LoginUserID;
           
            if (wipOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = wipOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = wipOutLoginStore.MaterielID;

                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail).ToList();

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count; i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    giReserve.PrhaOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    giReserve.ClientOrderDetail = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - wipOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - wipOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }
                    }
                   
                }
            }
            else if (wipOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = wipOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = wipOutLoginStore.MaterReqDetailNo;

                var produceMaterDetailForListDesc = wipOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
                for (int i = 0; i < produceMaterDetailForListDesc.Count; i++)
                {
                    produceMaterDetail.CustomerOrderNum = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    produceMaterDetail.CustomerOrderDetails = produceMaterDetailForListDesc[i].CustomerOrderDetails;
                    giReserve.PrhaOrderID = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    giReserve.ClientOrderDetail = produceMaterDetailForListDesc[i].CustomerOrderDetails;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - wipOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - wipOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }
                   
                    }
                   
                }
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录修改批次别库存表）

        /// <summary>
        /// 在制品库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void WipOutForLoginUpdateBthStockList(VM_WipOutLoginStoreForTableShow wipOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.WhID = wipWhID;
            bthStockList.PdtID = wipOutLoginStore.MaterielID;
            bthStockList.UpdUsrID = LoginUserID;
            bthStockList.UpdDt = DateTime.Today;
            if (wipOutLoginStore.OsSupProFlg == "000")
            {
                bthStockList.BillType = "01";
            }
            else if (wipOutLoginStore.OsSupProFlg == "002")
            {
                bthStockList.BillType = "03";
            }

            for (int j = 0; j < selectOrderList.Length; j++)
            {
                if (wipOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && wipOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
                            bthStockList.OrdeQty = OrdeQty - wipOutLoginStore.Qty;
                        }
                        bthStockList.CmpQty = CmpQty + wipOutLoginStore.Qty;
                        //修改批次库存表
                        bthStockListRepository.UpdateBthStockListForOut(bthStockList);
                    }
                }
            }
           
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库登录修改生产领料单及外协领料单表）

        /// <summary>
        /// 在制品库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        public void WipOutForLoginUpdateMaterReq(VM_WipOutLoginStoreForTableShow wipOutLoginStore)
        {
           //来自外协
            if (wipOutLoginStore.OsSupProFlg == "002")
            {
                MCSupplierCnsmInfo mcSupplierCnsmInfo = new MCSupplierCnsmInfo();
                mcSupplierCnsmInfo.MaterReqNo = wipOutLoginStore.PickListID;
                mcSupplierCnsmInfo.MaterialsSpecReq = wipOutLoginStore.MaterReqDetailNo;
                mcSupplierCnsmInfo.ReceiveQuantity = wipOutLoginStore.Qty;
                mcSupplierCnsmInfo.UpdUsrID = LoginUserID;
                mcSupplierCnsmInfo.UpdDt = DateTime.Today;

                wipOutRecordRepository.UpdateSupplierCnsmInfoForOut(mcSupplierCnsmInfo);

            }
            //来自生产
            else if (wipOutLoginStore.OsSupProFlg == "000")
            {
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = wipOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = wipOutLoginStore.MaterReqDetailNo;
                produceMaterDetail.ReceQty = wipOutLoginStore.Qty;
                produceMaterDetail.UpdUsrID = LoginUserID;
                produceMaterDetail.UpdDt = DateTime.Today;

                wipOutRecordRepository.UpdateProduceMaterDetailForOut(produceMaterDetail);
            }
        }

        #endregion

        #region IWipStoreService 成员（在制品出库履历一览初始化页面）

        public IEnumerable GetWipOutRecordBySearchByPage(VM_WipOutRecordStoreForSearch wipOutRecordStoreForSearch, Paging paging)
        {
            return wipOutRecordRepository.GetWipOutRecordBySearchByPage(wipOutRecordStoreForSearch, paging);
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库履历一览删除功能）


        public bool WipOutRecordForDel(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库履历一览修改功能）


        public bool WipOutRecordForUpdate(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore)
        {
            throw new NotImplementedException();
        }

        #endregion
  
        public IEnumerable GetWipStoreBySearchByPage(WipStore wipstore, Paging paging)
        {           
            return wipStoreRepository.GetWipStoreBySearchByPage(wipstore, paging);
        }

        public IEnumerable GetWipStoreBySearchById(string Parameter_id)
        {         
            return wipStoreRepository.GetWipStoreBySearchById(Parameter_id);
        }

        public bool UpdateWipStore(List<string> list)
        {
            foreach (var Id in list)
            {
                wipStoreRepository.updateWipStoreColumns(new WipStore { Id = Id, DelFlag= "1" });              
            }
            return true;
        }

        public bool AddWipStore(WipStore WipStore) {
          
            wipStoreRepository.addWipStore(WipStore);

            return true;
        }

        public bool UpdateWipStore(WipStore WipStore)
        {

            wipStoreRepository.updateWipStore(WipStore);

            return true;
        }

        #region IWipStoreService 成员


        public IEnumerable SelectWipStoreForId(string pid, Paging paging)
        {
            return wipStoreRepository.SelectWipStoreForId(pid, paging);
        }

        #endregion

        #region IWipStoreService 成员（在制品库出库批次选择画面）

        /// <summary>
        /// 出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        public IEnumerable SelectWipStoreForBthSelect(decimal qty, string pdtID,string pickListID,string materReqDetailNo,string osSupProFlg, Paging paging)
        {
            //if (osSupProFlg == "")
            //{
            //    return wipOutRecordRepository.SelectWipOutRecordForBthSelect(qty, pickListID,paging);
            //}
             //来自生产
            if (osSupProFlg == "000")
            {
                var wipOutRecordInfo = wipOutRecordRepository.WipOutRecordInfo(pickListID, materReqDetailNo);
                //判断有没有指定批次号
                if (wipOutRecordInfo.BthID == "")
                {
                    return wipOutRecordRepository.SelectWipOutRecordProNForBthSelect(qty, pdtID,pickListID, materReqDetailNo, osSupProFlg, paging);
                }
                else
                {
                    return wipOutRecordRepository.SelectWipOutRecordProForBthSelect(qty, pickListID, materReqDetailNo, paging);
                }

            }
             //来自外协
            else
            {
                var wipOutRecordSInfo = wipOutRecordRepository.WipOutRecordSInfo(pickListID, materReqDetailNo);
                //判断有没有指定批次号
                if (wipOutRecordSInfo.BatchID == "")
                {
                    return wipOutRecordRepository.SelectWipOutRecordSupNForBthSelect(qty, pdtID, pickListID, materReqDetailNo, osSupProFlg, paging);
                }
                else
                {
                    return wipOutRecordRepository.SelectWipOutRecordSupForBthSelect(qty, pickListID, materReqDetailNo, paging);
                }
            }
        }

        #endregion

        #region IWipStoreService 成员(入库单打印选择画面显示)


        public IEnumerable GetWipInPrintBySearchByPage(VM_WipInPrintForSearch wipInPrintForSearch, Paging paging)
        {
          return wipInRecordRepository.GetWipInPrintBySearchByPage(wipInPrintForSearch, paging);
        }

        #endregion

        #region IWipStoreService 成员 (加工产品出库单显示)


        public IEnumerable SelectForWipInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            return wipInRecordRepository.SelectForWipInPrintPreview(pdtID, deliveryOrderID, paging);
        }

        #endregion

        #region IWipStoreService 成员


        public IEnumerable SelectWipStore(string pid)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWipStoreService 成员（在制品库履历删除暂用方法（一期测试））


        public string WipInRecordForDelTest(List<VM_WipInRecordStoreForTableShow> wipInRecordStore)
        {
            foreach (var wipInRecordStoreCopy in wipInRecordStore)
            {
                WipInDetailRecord wipInDetailRecord = new WipInDetailRecord();
                wipInDetailRecord.TecnPdtInID = wipInRecordStoreCopy.McIsetInListID;
                wipInDetailRecord.PdtID = wipInRecordStoreCopy.PdtID;
                wipInDetailRecord.DelUsrID = LoginUserID;


                var wipInDetailRecordForList = wipInDetailRecordRepository.GetWipInDetailRecordForList(wipInDetailRecord).ToList();
                if (wipInDetailRecordForList.Count > 1)
                {
                    //删除附件库入库履历详细中的数据
                    wipInDetailRecordRepository.WipInDetailRecordForDel(wipInDetailRecord);
                }
                else
                {
                    //删除附件库履历及附件库履历详细中的数据                 
                    wipInDetailRecordRepository.WipInDetailRecordForDel(wipInDetailRecord);
                    wipInRecordRepository.WipInRecordForDel(new WipInRecord { DlvyListID = wipInRecordStoreCopy.DeliveryOrderID, DelUsrID = LoginUserID });
                }
            }
            return "删除成功";
        }

        public string WipOutRecordForDelTest(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore)
        {
            foreach (var wipOutRecordStoreCopy in wipOutRecordStore)
            {
                WipOutDetailRecord wipOutDetailRecord = new WipOutDetailRecord();
                wipOutDetailRecord.TecnPdtOutID = wipOutRecordStoreCopy.SaeetID;
                wipOutDetailRecord.PdtID = wipOutRecordStoreCopy.MaterielID;
                wipOutDetailRecord.PickListDetNo = wipOutRecordStoreCopy.PickListDetNo;
                wipOutDetailRecord.BthID = wipOutRecordStoreCopy.BthID;
                wipOutDetailRecord.DelUsrID = LoginUserID;


                var wipOutDetailRecordForList = wipOutDetailRecordRepository.GetWipOutDetailRecordForList(wipOutDetailRecord).ToList();
                if (wipOutDetailRecordForList.Count > 1)
                {
                    //删除附件库出库履历详细中的数据
                    wipOutDetailRecordRepository.WipOutDetailRecordForDel(wipOutDetailRecord);
                }
                else
                {
                    //删除附件库出库履历及附件库出库履历详细中的数据                 
                    wipOutDetailRecordRepository.WipOutDetailRecordForDel(wipOutDetailRecord);
                    wipOutRecordRepository.WipOutRecordForDel(new WipOutRecord { PickListID = wipOutRecordStoreCopy.PickListID, DelUsrID = LoginUserID });
                }
            }
            return "删除成功";
        }

        #endregion

        #region IWipStoreService 成员(在制品库入库登录暂用方法（一期测试）)


        public bool WipInForLoginTest(List<VM_WipInLoginStoreForTableShow> wipInLoginStore)
        {
            foreach (var wipInLoginStoreCopy in wipInLoginStore) {
                if (wipInLoginStoreCopy.OsSupProFlg == "001" || wipInLoginStoreCopy.OsSupProFlg == "002")
                //修改进货检验单
                accInRecordRepository.UpdatePurChkListForStoStat(wipInLoginStoreCopy.IsetRepID,"1");
                //修改过程检验单
                else if (wipInLoginStoreCopy.OsSupProFlg == "000")
                {
                    accInRecordRepository.UpdateProcChkListForStoStat(wipInLoginStoreCopy.IsetRepID, "1");
                }
            }
            return true;
        }

        #endregion

    } //end WipStoreServiceImp
}
