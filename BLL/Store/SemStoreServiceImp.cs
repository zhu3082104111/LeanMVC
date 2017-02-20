/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemInStoreServiceImp.cs
// 文件功能描述：
//          半成品库入库相关画面的Service接口的实现
//
// 修改履历：2013/11/23 汪腾飞 新建
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
    public class SemStoreServiceImp : AbstractService, ISemStoreService
    {
        //需要使用的Repository类
        private IWipStoreRepository semStoreRepository;
        private ISemInRecordRepository semInRecordRepository;
        private ISemInDetailRecordRepository semInDetailRecordRepository;
        private ISemOutRecordRepository semOutRecordRepository;
        private ISemOutDetailRecordRepository semOutDetailRecordRepository;
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

        //构造方法，参数为需要注入的属性



        public SemStoreServiceImp(IWipStoreRepository semStoreRepository, ISemInRecordRepository semInRecordRepository, ISemOutRecordRepository semOutRecordRepository,
                                  IBthStockListRepository bthStockListRepository, ISemInDetailRecordRepository semInDetailRecordRepository, IMCOutSourceOrderRepository mcOutSourceOrderRepository,
                                  ISupplierOrderDetailRepository supplierOrderDetailRepository, IReserveRepository reserveRepository, IReserveDetailRepository reserveDetailRepository,
                                  IMaterialRepository materialRepository, ICompMaterialInfoRepository compMaterialInfoRepository, IGiMaterialRepository giMaterialRepository,
                                  IMCOutSourceOrderDetailRepository mcOutSourceOrderDetailRepository, ISemOutDetailRecordRepository semOutDetailRecordRepository,
                                  IGiReserveRepository giReserveRepository) 
        {
            this.semStoreRepository = semStoreRepository;
            this.semInRecordRepository = semInRecordRepository;
            this.semOutRecordRepository = semOutRecordRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.semInDetailRecordRepository = semInDetailRecordRepository;
            this.mcOutSourceOrderRepository = mcOutSourceOrderRepository;
            this.supplierOrderDetailRepository = supplierOrderDetailRepository;
            this.reserveRepository = reserveRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.materialRepository = materialRepository;
            this.compMaterialInfoRepository = compMaterialInfoRepository;
            this.giMaterialRepository = giMaterialRepository;
            this.mcOutSourceOrderDetailRepository = mcOutSourceOrderDetailRepository;
            this.semOutDetailRecordRepository = semOutDetailRecordRepository;
            this.giReserveRepository = giReserveRepository;
        }

        //半成品库仓库编码
        public String semWhID = "0202";
        //当前用户
        public string LoginUserID = "201228";
        //外协单据类型
        public string outSourceBillType = "02";


        #region ISemStoreService (半成品库待入库一览画面)
        /// <summary>
        /// 半成品库待入库一览数据显示
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public IEnumerable GetSemStoreBySearchByPage(VM_SemInStoreForSearch searchCondition, Paging paging)
        {
            return semInRecordRepository.GetSemStoreBySearchByPage(searchCondition, paging);
        }
        #endregion

        #region ISemStoreService (半成品库打印选择画面)
        public IEnumerable GetSemInPrintBySearchByPage(VM_SemInPrintForSearch seminprint, Paging paging)
        {
            return semInRecordRepository.GetSemInPrintBySearchByPage(seminprint, paging);
        }
        #endregion

        #region ISemStoreService (半成品库打印预览画面)
        public IEnumerable SelectSemStore(string pdtID, string deliveryOrderID, Paging paging)
        {
            return semInRecordRepository.SelectSemStore(pdtID, deliveryOrderID, paging);
        }
        #endregion

        #region ISemStoreService (半成品库入库履历一览初始画面)
        public IEnumerable GetSemInRecordBySearchByPage(VM_SemInRecordStoreForSearch semInRecordStoreForSearch, Paging paging)
        {
            return semInRecordRepository.GetSemInRecordBySearchByPage(semInRecordStoreForSearch, paging);
        }
        #endregion

        #region ISemStoreService (半成品库入库登陆初始画面)

        public IEnumerable GetSemInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {
            return semInRecordRepository.GetSemInStoreForLoginBySearchByPage( deliveryOrderID,  isetRepID,  paging);
        }

        #endregion

        #region ISemStoreService 成员（半成品库入库登录业务）


        public bool SemInForLogin(List<VM_SemInLoginStoreForTableShow> semInLoginStore)
        {
            string pdtSpecState = "";
            foreach (var semInLoginStoreCopy in semInLoginStore)
            {
                //添加入库履历数据
                SemInForLoginAddInRecord(semInLoginStoreCopy);
                //添加批次别库存表
                SemInForLoginAddBthStockList(semInLoginStoreCopy);
                //修改进货检验表的入库状态(假注释)
                //UpdatePurChkListForStoStat(semInLoginStoreCopy.IsetRepID,"1")
                //修改过程检验表的入库状态
                //UpdateProcChkListForStoStat(semInLoginStoreCopy.IsetRepID,"1")


                #region 无规格型号的合格品
                if (semInLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(semInLoginStoreCopy.PdtSpec))
                {
                    pdtSpecState = "";
                    //修改仓库预约表和外购单明细表和外协加工调度单详细表
                    SemInForLoginUpdateReserve(semInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    SemInForLoginUpdateMaterial(semInLoginStoreCopy);
                    return true;
                }
                else
                {

                }

                #endregion

                #region 有规格型号的合格品
                if (semInLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(semInLoginStoreCopy.PdtSpec))
                {
                    //修改仓库预约表、修改仓库预约详细表、外购单明细表和外协加工调度单详细表
                    pdtSpecState = "0";
                    SemInForLoginUpdateReserve(semInLoginStoreCopy, pdtSpecState);
                    //修改仓库表
                    SemInForLoginUpdateMaterial(semInLoginStoreCopy);
                    return true;
                }
                else
                {
                }

                #endregion

                #region 让步品
                if (semInLoginStoreCopy.GiCls != "999")
                {
                    //添加让步仓库表
                    SemInForLoginAddGiMaterial(semInLoginStoreCopy);
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

        #region ISemStoreService 成员(半成品库入库登录添加入库履历数据)


        public void SemInForLoginAddInRecord(VM_SemInLoginStoreForTableShow semInLoginStore)
        {
            //查询入库履历表中有无该数据
            SemInRecord semInRecord = new SemInRecord();
            semInRecord.DlvyListId = semInLoginStore.DeliveryOrderID;

            SemInDetailRecord semInDetailRecord = new SemInDetailRecord();
            semInDetailRecord.TecnPdtInId = semInLoginStore.McIsetInListID;
            semInDetailRecord.PdtId = semInLoginStore.PdtID;

            SemInRecord semInRecordCopy = semInRecordRepository.SelectSemInRecord(semInRecord);
            SemInDetailRecord semInDetailRecordCopy = semInDetailRecordRepository.SelectSemInDetailRecord(semInDetailRecord);
            if (semInRecordCopy == null)
            {
                //附件库入库履历添加           
                semInRecord.PlanId = semInLoginStore.PlanID;
                semInRecord.PlanCls = "";//？？？？
                semInRecord.BthId = semInLoginStore.BthID;
                semInRecord.DlvyListId = semInLoginStore.DeliveryOrderID;
                semInRecord.WhId = semWhID;
                semInRecord.WhPostId = "000";
                semInRecord.InMvCls = "00";
                semInRecord.TecnPdtInId = semInLoginStore.DeliveryOrderID + semWhID;//根据送货单号+仓库编码生成
                semInRecord.ProcUnit = semInLoginStore.CompID;
                semInRecord.CreUsrID = LoginUserID;
                semInRecordRepository.Add(semInRecord);

            }
            else
            {
            }
            if (semInDetailRecordCopy == null)
            {
                //附件库入库履历详细添加
                semInDetailRecord.TecnPdtInId = semInLoginStore.McIsetInListID;
                semInDetailRecord.IsetRepId = semInLoginStore.IsetRepID;
                semInDetailRecord.GiCls = semInLoginStore.GiCls;
                semInDetailRecord.PdtName = semInLoginStore.PdtName;
                semInDetailRecord.PdtSpec = semInLoginStore.PdtSpec;
                semInDetailRecord.TecnPdtInId = semInLoginStore.TecnProcess;
                semInDetailRecord.Qty = semInLoginStore.Qty;
                semInDetailRecord.ProScrapQty = semInLoginStore.ProScrapQty;
                semInDetailRecord.ProMaterscrapQty = semInLoginStore.ProMaterscrapQty;
                semInDetailRecord.ProOverQty = semInLoginStore.ProOverQty;
                semInDetailRecord.ProLackQty = semInLoginStore.ProLackQty;
                semInDetailRecord.ProTotalQty = semInLoginStore.ProTotalQty;

                //单价
                if (semInLoginStore.SemLoginPriceFlg == "1")
                {
                    semInDetailRecord.PrchsUp = semInLoginStore.PrchsUp;
                    semInDetailRecord.NotaxAmt = semInLoginStore.Qty * semInLoginStore.PrchsUp;
                }
                //估价
                else
                {
                    semInDetailRecord.ValuatUp = semInDetailRecord.PrchsUp;
                    semInDetailRecord.NotaxAmt = semInLoginStore.Qty * semInLoginStore.ValuatUp;
                }

                semInDetailRecord.WhkpId = "201228";//仓管员？？？？？
                semInDetailRecord.InDate = DateTime.Now;//格式YYYYMMDD
                semInDetailRecord.CreDt = DateTime.Now;
                semInDetailRecord.PrintFlag = "0";
                semInDetailRecord.CreUsrID = LoginUserID;
                semInDetailRecordRepository.Add(semInDetailRecord);
            }
            else
            {
            }
        }

        #endregion

        #region ISemStoreService 成员（半成品库入库登录修改仓库预约表、修改仓库预约详细表和外协加工调度单详细表）


        public void SemInForLoginUpdateReserve(VM_SemInLoginStoreForTableShow semInLoginStore, string pdtSpecState)
        {
            string forFlg = "";
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = semWhID;
            reser.PdtID = semInLoginStore.PdtID;

            //修改外协加工调度单详细表（实际入库数量）主键字段赋值
            MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
            mcSupplierOrderDetail.SupOrderID = semInLoginStore.PlanID;
            mcSupplierOrderDetail.ProductPartID = semInLoginStore.PdtID;

            //仓库预约详细表字段赋值(有规格型号要求)
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = semInLoginStore.BthID;
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

            if (semInLoginStore.OsSupProFlg == "000")
            {
                //来自生产（提供参数加工送货单号、零件ID获得客户订单号和客户订单明细号对应list）
                //假删除
                //var proListAsc = proRepository.GetCustomerOrderForListAsc(semInLoginStore.DeliveryOrderID,semInLoginStore.PdtID);

                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = semInLoginStore.Qty;
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

            //来自外协
            else if (semInLoginStore.OsSupProFlg == "002")
            {
                var mcSupplierOrderDetailForListAsc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListAsc(mcSupplierOrderDetail);
                //入库剩余数量
                decimal inStoreSurplus = '0';
                inStoreSurplus = semInLoginStore.Qty;

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

        #region ISemStoreService 成员(半成品库入库登录修改仓库表)


        public void SemInForLoginUpdateMaterial(VM_SemInLoginStoreForTableShow semInLoginStore)
        {
            decimal unitPrice = '0';
            decimal evaluate = '0';

            Material material = new Material();
            material.WhID = semWhID;
            material.PdtID = semInLoginStore.PdtID;
            Material materialCopy = materialRepository.selectMaterial(material);
            if (materialCopy != null)
            {
                var AlctQty = materialCopy.AlctQty;
                var CurrentQty = materialCopy.CurrentQty;
                var UseableQty = materialCopy.UseableQty;
                var TotalValuatUp = materialCopy.TotalValuatUp;
                var TotalAmt = materialCopy.TotalAmt;

                //从外购单表外购单区分中判断有无客户订单？？？？（应该不是从外购判断判断）
                //MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(semInLoginStore.PlanID);
                //供货商供货信息表取的单价
                CompMaterialInfo compMaterialInfo = compMaterialInfoRepository.SelectCompMaterialInfoForPrice(semInLoginStore);
                if (compMaterialInfo != null)
                {
                    unitPrice = compMaterialInfo.UnitPrice;
                    evaluate = compMaterialInfo.Evaluate;
                }
                else
                {

                }
                //if (mcOutSourceOrder.OutOrderFlg == "0")
                //{
                    //累加被预约数量
                    material.AlctQty = AlctQty + semInLoginStore.Qty;
                    //累加实际在库数量
                    material.CurrentQty = CurrentQty + semInLoginStore.Qty;
                    //可用在库数量
                    material.UseableQty = UseableQty;

                //}

                    //否则累加可用在库数量       //暂不考虑仓库自满足状态
                //else
                //{
                //    //累加可用在库数量  
                //    material.UseableQty = UseableQty + semInLoginStore.Qty;
                //    //累加实际在库数量
                //    material.CurrentQty = CurrentQty + semInLoginStore.Qty;
                //    //被预约数量
                //    material.AlctQty = AlctQty;
                //}
                //供货商供货信息表判断单价取得
                if (unitPrice != '0')
                {
                    material.TotalAmt = TotalAmt + semInLoginStore.Qty * unitPrice;
                    material.TotalValuatUp = '0';
                }
                else if (unitPrice == '0' && evaluate != '0')
                {
                    material.TotalValuatUp = TotalValuatUp + semInLoginStore.Qty * evaluate;
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
                material.AlctQty = semInLoginStore.Qty;
                material.RequiteQty = '0';
                material.OrderQty = semInLoginStore.Qty;
                material.CnsmQty = '0';
                material.ArrveQty = semInLoginStore.Qty;
                material.IspcQty = semInLoginStore.Qty;
                material.UseableQty = semInLoginStore.Qty;
                material.CurrentQty = semInLoginStore.Qty;
                material.CreUsrID = LoginUserID;
                //供货商供货信息表判断单价取得
                if (unitPrice != '0')
                {
                    material.TotalAmt = semInLoginStore.Qty * unitPrice;
                    material.TotalValuatUp = '0';
                }
                else if (unitPrice == '0' && evaluate != '0')
                {
                    material.TotalValuatUp = semInLoginStore.Qty * evaluate;
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

        #region ISemStoreService 成员(半成品库入库登录添加批次别库存表)


        public void SemInForLoginAddBthStockList(VM_SemInLoginStoreForTableShow semInLoginStore)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.BillType = outSourceBillType;
            bthStockList.PrhaOdrID = semInLoginStore.PlanID;
            bthStockList.BthID = semInLoginStore.BthID;
            bthStockList.WhID = semWhID;
            bthStockList.PdtID = semInLoginStore.PdtID;
            bthStockList.PdtSpec = semInLoginStore.PdtSpec;
            bthStockList.GiCls = semInLoginStore.GiCls;
            bthStockList.Qty = semInLoginStore.Qty;
            bthStockList.InDate = DateTime.Now;
            bthStockList.CreUsrID = LoginUserID;
            //bthStockList.EffeFlag = "0";
            //bthStockList.DelFlag = "0";

            //从外购单表外购单区分中判断有无客户订单(？？？？不应该)
            //MCOutSourceOrder mcOutSourceOrder = mcOutSourceOrderRepository.GetMCOutSourceOrderById(semInLoginStore.PlanID);
            //客户有预定(从外购单表外购单区分判断有无客户订单)
            // if (mcOutSourceOrder.OutOrderFlg == "0")//？？？？？
            //{
                 bthStockList.OrdeQty = semInLoginStore.Qty;
            //}
            // else
            //{              
            //   bthStockList.OrdeQty = '0';
            // }

            bthStockListRepository.Add(bthStockList);
        }

        #endregion

        #region ISemStoreService 成员（半成品库入库登录添加让步仓库表）


        public void SemInForLoginAddGiMaterial(VM_SemInLoginStoreForTableShow semInLoginStore)
        {

            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = semWhID;
            giMaterial.ProductID = semInLoginStore.PdtID;
            giMaterial.ProductSpec = semInLoginStore.PdtSpec;
            giMaterial.BatchID = semInLoginStore.BthID;
            giMaterial.AlctQuantity = '0';
            giMaterial.UserableQuantity = semInLoginStore.Qty;
            giMaterial.CurrentQuantity = semInLoginStore.Qty;
            giMaterial.GiCls = semInLoginStore.GiCls;
            giMaterial.CreUsrID = LoginUserID;
            if (semInLoginStore.SemLoginPriceFlg == "0")
            {
                giMaterial.TotalAmt = semInLoginStore.Qty * semInLoginStore.PrchsUp;
            }
            else
            {

            }

            giMaterialRepository.Add(giMaterial);
        }

        #endregion

        #region IAccStoreService 成员（半成品库出库履历一览初始化页面（yc添加））


        public IEnumerable GetSemOutRecordBySearchByPage(VM_SemOutRecordStoreForSearch semOutRecordStoreForSearch, Paging paging)
        {
            return semOutRecordRepository.GetSemOutRecordBySearchByPage(semOutRecordStoreForSearch, paging);
        }

        #endregion

        #region ISemStoreService 成员（半成品库履历删除暂用方法（一期测试））


        public string SemInRecordForDelTest(List<VM_SemInRecordStoreForTableShow> semInRecordStore)
        {
            foreach (var semInRecordStoreCopy in semInRecordStore)
            {
                SemInDetailRecord semInDetailRecord = new SemInDetailRecord();
                semInDetailRecord.TecnPdtInId = semInRecordStoreCopy.McIsetInListID;
                semInDetailRecord.PdtId = semInRecordStoreCopy.PdtID;
                semInDetailRecord.DelUsrID = LoginUserID;


                var semInDetailRecordForList = semInDetailRecordRepository.GetSemInDetailRecordForList(semInDetailRecord).ToList();
                if (semInDetailRecordForList.Count > 1)
                {
                    //删除附件库入库履历详细中的数据
                    semInDetailRecordRepository.SemInDetailRecordForDel(semInDetailRecord);
                }
                else
                {
                    //删除附件库履历及附件库履历详细中的数据                 
                    semInDetailRecordRepository.SemInDetailRecordForDel(semInDetailRecord);
                    semInRecordRepository.SemInRecordForDel(new SemInRecord { DlvyListId = semInRecordStoreCopy.DeliveryOrderID, DelUsrID = LoginUserID });
                }
            }
            return "删除成功";
        }
        #endregion

        #region ISemStoreService 成员（半成品库入库登陆保存方法（一期测试））

        public string SemInStoreForDelTest(List<VM_SemInLoginStoreForTableShow> semInStore)
        {

            foreach (var semInStoreCopy in semInStore)
            {

                if (semInStoreCopy.OsSupProFlg == "000")
                {
                    semInRecordRepository.SemInStoreForDel(semInStoreCopy.IsetRepID, "1");

                }
                else
                {
                    semInRecordRepository.SemInStoreForDelProc(semInStoreCopy.IsetRepID, "1");
                }
            }
            return "保存成功";
        }


        #endregion

        #region 待出库一览(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)待出库一览结果集
        /// </summary>
        /// <param name="semOutStoreForSearch">VM_SemOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetSemOutStoreBySearchByPage(VM_SemOutStoreForSearch semOutStoreForSearch, Paging paging)
        {
            return semOutRecordRepository.GetSemOutStoreBySearchByPage(semOutStoreForSearch, paging);
        } //end GetSemOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipInPrintForSearch">VM_SemOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetSemOutPrintBySearchByPage(VM_SemOutPrintForSearch semOutPrintForSearch, Paging paging)
        {
            return semOutRecordRepository.GetSemOutPrintBySearchByPage(semOutPrintForSearch, paging);
        } //end GetSemOutPrintBySearchByPage

        #endregion

        #region 材料领用出库单(半成品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_SemOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_SemOutPrintIndexForInfoShow GetSemOutPrintForInfoShow(string pickListID)
        {
            return semOutRecordRepository.GetSemOutPrintForInfoShow(pickListID);
        } //end GetSemOutPrintByInfoShow

        /// <summary>
        /// 根据 SemOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="semOutDetailRecordList">SemOutDetailRecord 泛型结果集</param>
        /// <returns>VM_SemOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public List<VM_SemOutPrintIndexForTableShow> GetSemOutPrintForTableShow(string pickListID, List<SemOutDetailRecord> semOutDetailRecordList)
        {
            List<VM_SemOutPrintIndexForTableShow> semOutPrintIndexForTableShowList = new List<VM_SemOutPrintIndexForTableShow>();

            foreach (SemOutDetailRecord semOutDetailRecord in semOutDetailRecordList)
            {
                //semOutPrintIndexForTableShowList.Add(semOutRecordRepository.GetSemOutPrintForTableShow(semOutDetailRecord));
                semOutPrintIndexForTableShowList.Add(semOutRecordRepository.GetSemOutPrintForTableShow(pickListID, semOutDetailRecordRepository.Find(semOutDetailRecord)));
            }

            return semOutPrintIndexForTableShowList;

        } //GetSemOutPrintForTableShow

        #endregion

        #region ISemStoreService 成员（半成品出库登录画面数据表示）

        /// <summary>
        /// 半成品库出库登录画面数据表示 陈健
        /// </summary>
        /// <param name="materReqNO">领料单号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        public IEnumerable GetSemOutStoreForLoginBySearchByPage(string materReqNO, Paging paging)
        {
            return semOutRecordRepository.GetSemOutStoreForLoginBySearchByPage(materReqNO, paging);
        }

        #endregion

        #region ISemStoreService 成员（半成品库出库批次选择画面）

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
        public IEnumerable SelectSemStoreForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging)
        {
            //if (osSupProFlg == "")
            //{
            //    return semOutRecordRepository.SelectSemOutRecordForBthSelect(qty, pickListID, paging);
            //}
            //来自生产
            if (osSupProFlg == "000")
            {
                var semOutRecordInfo = semOutRecordRepository.SemOutRecordInfo(pickListID, materReqDetailNo);
                //没有指定批次号
                if (semOutRecordInfo.BthID == "")
                {
                    return semOutRecordRepository.SelectSemOutRecordProNForBthSelect(qty, pdtID, pickListID, materReqDetailNo, osSupProFlg, paging);
                }
                else
                {
                    return semOutRecordRepository.SelectSemOutRecordProForBthSelect(qty, pickListID, materReqDetailNo, paging);
                }

            }
            else
            {
                var semOutRecordSInfo = semOutRecordRepository.SemOutRecordSInfo(pickListID, materReqDetailNo);
                //没有指定批次号
                if (semOutRecordSInfo.BatchID == "")
                {
                    return semOutRecordRepository.SelectSemOutRecordSupNForBthSelect(qty, pdtID, pickListID, materReqDetailNo, osSupProFlg, paging);
                }
                else
                {
                    return semOutRecordRepository.SelectSemOutRecordSupForBthSelect(qty, pickListID, materReqDetailNo, paging);
                }
            }
        }

        #endregion

        #region ISemStoreService 成员（半成品库出库登录业务）

        /// <summary>
        /// 半成品出库登录保存 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public bool SemOutForLogin(List<VM_SemOutLoginStoreForTableShow> semOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            string pdtSpecState = "";
            foreach (var semOutLoginStoreCopy in semOutLoginStore)
            {
                //添加出库履历数据
                //SemOutForLoginAddOutRecord(semOutLoginStoreCopy, selectOrderList);

                //更新生产领料单及外协领料单表
                SemOutForLoginUpdateMaterReq(semOutLoginStoreCopy);

                //#region 无规格型号的合格品
                //if (semOutLoginStoreCopy.GiCls == "999" && string.IsNullOrEmpty(semOutLoginStoreCopy.PdtSpec))
                //{
                //    pdtSpecState = "0";
                //    //修改仓库预约表
                //    SemOutForLoginUpdateReserve(semOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    //修改仓库表
                //    SemOutForLoginUpdateMaterial(semOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    SemOutForLoginUpdateBthStockList(semOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    return true;
                //}
                //else
                //{

                //}

                //#endregion

                //#region 有规格型号的合格品
                //if (semOutLoginStoreCopy.GiCls == "999" && !string.IsNullOrEmpty(semOutLoginStoreCopy.PdtSpec))
                //{
                //    pdtSpecState = "1";
                //    //修改仓库预约表及仓库预约详细表
                //    SemOutForLoginUpdateReserve(semOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    //修改仓库表
                //    SemOutForLoginUpdateMaterial(semOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    SemOutForLoginUpdateBthStockList(semOutLoginStoreCopy, pdtSpecState, selectOrderList);
                //    return true;
                //}
                //else
                //{
                //}

                //#endregion

                //#region 让步品
                //if (semOutLoginStoreCopy.GiCls != "999")
                //{
                //    pdtSpecState = "2";
                //    //修改让步仓库表
                //    SemOutForLoginUpdateGiMaterial(semOutLoginStoreCopy, selectOrderList);
                //    //修改让步仓库预约表
                //    SemOutForLoginUpdateGiReserve(semOutLoginStoreCopy, selectOrderList);
                //    //修改批次别库存表
                //    SemOutForLoginUpdateBthStockList(semOutLoginStoreCopy, pdtSpecState, selectOrderList);
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

        #region ISemStoreService 成员（半成品库出库登录添加出库履历数据）

        /// <summary>
        /// 半成品库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginAddOutRecord(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {

            //查询出库履历表中有无该数据
            SemOutRecord semOutRecord = new SemOutRecord();
            semOutRecord.PickListId = semOutLoginStore.PickListID;

            SemOutDetailRecord semOutDetailRecord = new SemOutDetailRecord();
            semOutDetailRecord.TecnProductOutID = semOutLoginStore.SaeetID;
            semOutDetailRecord.ProductID = semOutLoginStore.MaterielID;

            SemOutRecord semOutRecordCopy = semOutRecordRepository.SelectSemOutRecord(semOutRecord);
            if (semOutRecordCopy == null)
            {
                //半成品库出库履历添加           
                semOutRecord.PickListId = semOutLoginStore.PickListID;
                semOutRecord.WhId = semWhID;
                semOutRecord.OutMvCls = "00";
                semOutRecord.TecnPdtOutId = semOutLoginStore.SaeetID;
                semOutRecord.CallinUnitId = semOutLoginStore.CallinUnitID;
                semOutRecord.EffeFlag = "0";
                semOutRecord.DelFlag = "0";
                semOutRecord.CreDt = DateTime.Today;
                semOutRecord.CreUsrID = LoginUserID;

                semOutRecordRepository.Add(semOutRecord);

            }
            else
            {
            }

            for (int i = 0; i < selectOrderList.Length; i++)
            {
                if (semOutLoginStore.PickListID == selectOrderList[i]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[i]["MaterReqDetailNo"])
                {

                    SemOutDetailRecord semOutDetailRecordCopy = semOutDetailRecordRepository.SelectSemOutDetailRecord(semOutDetailRecord, selectOrderList[i]["BthID"]);
                    if (semOutDetailRecordCopy == null)
                    {
                        semOutDetailRecord.TecnProductOutID = semOutLoginStore.SaeetID;
                        semOutDetailRecord.BatchID = selectOrderList[i]["BthID"];
                        semOutDetailRecord.ProductID = semOutLoginStore.MaterielID;
                        semOutDetailRecord.ProductName = semOutLoginStore.MaterielName;
                        semOutDetailRecord.ProductSpec = semOutLoginStore.PdtSpec;
                        semOutDetailRecord.GiCls = semOutLoginStore.GiCls;
                        semOutDetailRecord.TecnProcess = semOutLoginStore.TecnProcess;
                        semOutDetailRecord.Quantity = Convert.ToDecimal(selectOrderList[i]["UserQty"]);
                        semOutDetailRecord.PrchsUp = Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                        semOutDetailRecord.NotaxAmt = Convert.ToDecimal(selectOrderList[i]["UserQty"]) * Convert.ToDecimal(selectOrderList[i]["SellPrc"]);
                        semOutDetailRecord.WareHouseKpID = "201228";
                        semOutDetailRecord.OutDate = DateTime.Today;
                        semOutDetailRecord.Remarks = semOutLoginStore.Rmrs;
                        semOutDetailRecord.EffeFlag = "0";
                        semOutDetailRecord.DelFlag = "0";
                        semOutDetailRecord.CreUsrID = LoginUserID;
                        semOutDetailRecord.CreDt = DateTime.Today;

                        semOutDetailRecordRepository.Add(semOutDetailRecord);
                    }
                    else
                    {
                    }

                }
            }

        }

        #endregion

        #region ISemStoreService 成员（半成品库出库登录修改仓库预约表及仓库预约详细表）

        /// <summary>
        /// 半成品库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginUpdateReserve(VM_SemOutLoginStoreForTableShow semOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {

            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = semWhID;
            reser.PdtID = semOutLoginStore.MaterielID;
            reser.CmpQty = semOutLoginStore.Qty;
            reser.UpdDt = DateTime.Today;
            reser.UpdUsrID = LoginUserID;

            //仓库预约详细表字段赋值
            ReserveDetail reserveDetail = new ReserveDetail();
            reserveDetail.BthID = semOutLoginStore.BthID;
            reserveDetail.PickOrdeQty = semOutLoginStore.Qty;
            reserveDetail.CmpQty = semOutLoginStore.Qty;
            reserveDetail.UpdDt = DateTime.Today;
            reserveDetail.UpdUsrID = LoginUserID;

            if (semOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = semOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = semOutLoginStore.MaterielID;

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
                            if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
            else if (semOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = semOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = semOutLoginStore.MaterReqDetailNo;

                var produceMaterDetailForListDesc = semOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
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
                            if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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

        #region ISemStoreService 成员（半成品库出库登录修改仓库表）

        /// <summary>
        /// 半成品库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginUpdateMaterial(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            Material material = new Material();
            material.WhID = semWhID;
            material.PdtID = semOutLoginStore.MaterielID;
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
                    if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
                material.AlctQty = AlctQty - semOutLoginStore.Qty;
                //减去实际在库数量
                material.CurrentQty = CurrentQty - semOutLoginStore.Qty;
                //修改外协取料数量
                material.CnsmQty = semOutLoginStore.Qty;
                //修改最终出库日
                material.LastWhoutYmd = DateTime.Today;

                //修改人
                material.UpdUsrID = LoginUserID;
                material.UpdDt = DateTime.Today;
                materialRepository.updateMaterialForOutLogin(material);

            }
        }

        #endregion

        #region ISemStoreService 成员（半成品库出库登录修改让步仓库表）

        /// <summary>
        /// 半成品库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginUpdateGiMaterial(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiMaterial giMaterial = new GiMaterial();
            giMaterial.WareHouseID = semWhID;
            giMaterial.ProductID = semOutLoginStore.MaterielID;
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
                    if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
                giMaterial.AlctQuantity = AlctQty - semOutLoginStore.Qty;
                //减去实际在库数量
                giMaterial.CurrentQuantity = CurrentQty - semOutLoginStore.Qty;
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

        #region ISemStoreService 成员（半成品库出库登录修改让步仓库预约表）

        /// <summary>
        /// 半成品库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginUpdateGiReserve(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList)
        {
            GiReserve giReserve = new GiReserve();
            giReserve.WareHouseID = semWhID;
            giReserve.ProductID = semOutLoginStore.MaterielID;
            giReserve.UpdUsrID = LoginUserID;

            if (semOutLoginStore.OsSupProFlg == "002")
            {
                //外协加工调度单详细表主键字段赋值
                MCSupplierOrderDetail mcSupplierOrderDetail = new MCSupplierOrderDetail();
                mcSupplierOrderDetail.SupOrderID = semOutLoginStore.PickListID;
                mcSupplierOrderDetail.ProductPartID = semOutLoginStore.MaterielID;

                var mcSupplierOrderDetailForListDesc = supplierOrderDetailRepository.GetMCSupplierOrderDetailForListDesc(mcSupplierOrderDetail).ToList();

                for (int i = 0; i < mcSupplierOrderDetailForListDesc.Count; i++)
                {
                    mcSupplierOrderDetail.CustomerOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    mcSupplierOrderDetail.CustomerOrderDetailID = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;
                    giReserve.PrhaOrderID = mcSupplierOrderDetailForListDesc[i].CustomerOrderID;
                    giReserve.ClientOrderDetail = mcSupplierOrderDetailForListDesc[i].CustomerOrderDetailID;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - semOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - semOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }
                    }

                }
            }
            else if (semOutLoginStore.OsSupProFlg == "000")
            {
                //生产加工调度单详细表主键字段赋值
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = semOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = semOutLoginStore.MaterReqDetailNo;

                var produceMaterDetailForListDesc = semOutRecordRepository.GetProduceMaterDetailForListDesc(produceMaterDetail).ToList();
                for (int i = 0; i < produceMaterDetailForListDesc.Count; i++)
                {
                    produceMaterDetail.CustomerOrderNum = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    produceMaterDetail.CustomerOrderDetails = produceMaterDetailForListDesc[i].CustomerOrderDetails;
                    giReserve.PrhaOrderID = produceMaterDetailForListDesc[i].CustomerOrderNum;
                    giReserve.ClientOrderDetail = produceMaterDetailForListDesc[i].CustomerOrderDetails;

                    for (int j = 0; j < selectOrderList.Length; j++)
                    {
                        if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
                        {
                            giReserve.BatchID = selectOrderList[j]["BthID"];

                            GiReserve giReserveCopy = giReserveRepository.SelectGiReserve(giReserve);
                            if (giReserveCopy != null)
                            {
                                var pickOrderQuantity = giReserveCopy.PickOrderQuantity;
                                var cmpQuantity = giReserveCopy.CmpQuantity;

                                giReserve.PickOrderQuantity = pickOrderQuantity - semOutLoginStore.Qty;  //领料单开具数量
                                giReserve.CmpQuantity = cmpQuantity - semOutLoginStore.Qty;  //已出库数量

                                //修改让步仓库预约表
                                giReserveRepository.UpdateGiReserveForOutStoreQty(giReserve);
                            }
                        }

                    }

                }
            }
        }

        #endregion

        #region ISemStoreService 成员（半成品库出库登录修改批次别库存表）

        /// <summary>
        /// 半成品库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        public void SemOutForLoginUpdateBthStockList(VM_SemOutLoginStoreForTableShow semOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList)
        {
            BthStockList bthStockList = new BthStockList();
            bthStockList.WhID = semWhID;
            bthStockList.PdtID = semOutLoginStore.MaterielID;
            bthStockList.UpdUsrID = LoginUserID;
            bthStockList.UpdDt = DateTime.Today;
            if (semOutLoginStore.OsSupProFlg == "000")
            {
                bthStockList.BillType = "01";
            }
            else if (semOutLoginStore.OsSupProFlg == "002")
            {
                bthStockList.BillType = "03";
            }

            for (int j = 0; j < selectOrderList.Length; j++)
            {
                if (semOutLoginStore.PickListID == selectOrderList[j]["PickListID"] && semOutLoginStore.MaterReqDetailNo == selectOrderList[j]["MaterReqDetailNo"])
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
                            bthStockList.OrdeQty = OrdeQty - semOutLoginStore.Qty;
                        }
                        bthStockList.CmpQty = CmpQty + semOutLoginStore.Qty;
                        //修改批次库存表
                        bthStockListRepository.UpdateBthStockListForOut(bthStockList);
                    }
                }
            }

        }

        #endregion

        #region ISemStoreService 成员（半成品库出库登录修改生产领料单及外协领料单表）

        /// <summary>
        /// 半成品库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        public void SemOutForLoginUpdateMaterReq(VM_SemOutLoginStoreForTableShow semOutLoginStore)
        {
            //来自外协
            if (semOutLoginStore.OsSupProFlg == "002")
            {
                MCSupplierCnsmInfo mcSupplierCnsmInfo = new MCSupplierCnsmInfo();
                mcSupplierCnsmInfo.MaterReqNo = semOutLoginStore.PickListID;
                mcSupplierCnsmInfo.MaterialsSpecReq = semOutLoginStore.MaterReqDetailNo;
                mcSupplierCnsmInfo.ReceiveQuantity = semOutLoginStore.Qty;
                mcSupplierCnsmInfo.UpdUsrID = LoginUserID;
                mcSupplierCnsmInfo.UpdDt = DateTime.Today;

                semOutRecordRepository.UpdateSupplierCnsmInfoForOut(mcSupplierCnsmInfo);

            }
            //来自生产
            else if (semOutLoginStore.OsSupProFlg == "000")
            {
                ProduceMaterDetail produceMaterDetail = new ProduceMaterDetail();
                produceMaterDetail.MaterReqNo = semOutLoginStore.PickListID;
                produceMaterDetail.MaterReqDetailNo = semOutLoginStore.MaterReqDetailNo;
                produceMaterDetail.ReceQty = semOutLoginStore.Qty;
                produceMaterDetail.UpdUsrID = LoginUserID;
                produceMaterDetail.UpdDt = DateTime.Today;

                semOutRecordRepository.UpdateProduceMaterDetailForOut(produceMaterDetail);
            }
        }

        #endregion

    } //end SemStoreServiceImp
}
        



