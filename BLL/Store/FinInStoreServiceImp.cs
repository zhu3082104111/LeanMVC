/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinInStoreServiceImp.cs
// 文件功能描述：
//          内部成品库入库相关画面的Service接口的实现
//
// 修改履历：2013/11/23 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model.Store;
using Model;
using Extensions;
using BLL.ServerMessage;
using System.Text.RegularExpressions;
using BLL_InsideInterface;

namespace BLL.Store
{
    /// <summary>
    /// 内部成品库入库相关画面的Service接口的实现
    /// </summary>
    public class FinInStoreServiceImp : AbstractService, IFinInStoreService 
    {
        //引入需要调用的Repository类
        private IFinInRecordRepository finInRecordRepository;
        private IFinInDetailRecordRepository finInDetailRecordRepository;
        private IReserveRepository reserveRepository;
        private IProdInfoRepository prodInfoRepository;
        private IMaterialRepository materialRepository;
        private IProductWarehouseRepository productWarehouseRepository;
        private IBthStockListRepository bthStockListRepository;
        private IProductWarehouseDetailRepository productWarehouseDetailRepository;
        private IPartInfoRepository partInfoRepository;
        private IReserveDetailRepository reserveDetailRepository;

        private IStoreExternalService storeExternalService;
       
        /// <summary>
        /// 方法实现，引入调用的Repository
        /// </summary>
        /// <param name="finInStoreRepository">入库履历</param>
        /// <param name="finInDetailRecordRepository">入库履历详细</param>
        /// <param name="reserveRepository">仓库预约</param>
        /// <param name="prodInfoRepository">产品信息</param>
        /// <param name="materialRepository">仓库</param>
        /// <param name="productWarehouseRepository">成品交仓单</param>
        public FinInStoreServiceImp(IFinInRecordRepository finInStoreRepository, IFinInDetailRecordRepository finInDetailRecordRepository, 
            IReserveRepository reserveRepository, IProdInfoRepository prodInfoRepository, IMaterialRepository materialRepository,
            IProductWarehouseRepository productWarehouseRepository, IBthStockListRepository bthStockListRepository,
            IProductWarehouseDetailRepository productWarehouseDetailRepository, IPartInfoRepository partInfoRepository, IStoreExternalService storeExternalService,
            IReserveDetailRepository reserveDetailRepository) 
        {
            this.finInRecordRepository = finInStoreRepository;
            this.finInDetailRecordRepository = finInDetailRecordRepository;
            this.reserveRepository = reserveRepository;
            this.prodInfoRepository = prodInfoRepository;
            this.materialRepository = materialRepository;
            this.productWarehouseRepository = productWarehouseRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.productWarehouseDetailRepository = productWarehouseDetailRepository;
            this.partInfoRepository = partInfoRepository;
            this.storeExternalService = storeExternalService;
            this.reserveDetailRepository = reserveDetailRepository;
        }

        /// <summary>
        /// 获得待入库一览画面数据
        /// </summary>
        /// <param name="finInStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInStoreForTableShow> GetFinInStoreForSearch(Model.Store.VM_StoreFinInStoreForSearch finInStore, Paging paging)
        {
            return finInRecordRepository.GetFinInStoreWithPaging(finInStore, paging);
        }

        /// <summary>
        /// 获得入库履历一览画面数据
        /// </summary>
        /// <param name="finInRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForTableShow> GetFinInRecordForSearch(Model.Store.VM_StoreFinInRecordForSearch finInRecord, Paging paging)
         {
             return finInRecordRepository.GetFinInRecordWithPaging(finInRecord, paging);
         }

        /// <summary>
        /// 获得入库履历详细画面数据（入库跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordBySearchById(string productWarehouseID, Paging page)
         {
             return finInRecordRepository.GetFinInRecordByIdWithPaging(productWarehouseID,page);
         }

        /// <summary>
        /// 入库履历详细画面登录保存（检查，添加，修改)
        /// </summary>
        /// <param name="finInRecord">更新的数据集合</param>
        /// <param name="pageFlg">页面状态标识</param>
        /// <param name="editFlg">编辑标识</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="inRecordList">更新的数据集合</param>
        /// <returns>提示内容</returns>
        public string FinInRecordForLogin(VM_StoreFinInRecordForDetailShow finInRecord, string pageFlg, string editFlg, string uId, Dictionary<string, string>[] inRecordList)
        {
            //手动登录（暂时不用）
            if (pageFlg == "0" && editFlg == "0")
            {
                //判断成品交仓单号是否输入
                if (finInRecord.ProductWarehouseID == null)
                {
                    //成品交仓单号未输入
                    throw new Exception(SM_Store.SMSG_STORE_E00002);
                }
                else
                {
                    ////判断输入的成品交仓单号是否是数字和字符
                    //if (Regex.IsMatch(finInRecord.ProductWarehouseID, "[0-9]") == false || Regex.IsMatch(finInRecord.ProductWarehouseID, "[/u0000-/u00FF]") == false)
                    //{
                        ////成品交仓单号输入不合法
                        //throw new Exception(SM_Store.SMSG_STORE_E00007);
                   // }

                    //检查成品交仓单号是否存在
                    var endPWList = productWarehouseRepository.GetFinInRecordPdtWhID(finInRecord.ProductWarehouseID).ToList();

                    if (endPWList.Count == 0)
                    {
                        //成品交仓单号不存在
                        throw new Exception(SM_Store.SMSG_STORE_E000016);
                    }
                }

                //判断批次号是否输入
                if (finInRecord.BatchID == null)
                {
                    //批次号未输入
                    throw new Exception(SM_Store.SMSG_STORE_E00003);
                }
                else
                {
                    ////判断输入的批次号是否是数字和字符
                    //if (Regex.IsMatch(finInRecord.BatchID, "[0-9]") == false || Regex.IsMatch(finInRecord.BatchID, "[/u0000-/u00FF]") == false)
                    //{
                    //    //批次号输入不合法    加参数
                    //    errorList += SM_Store.SMSG_STORE_E00008;
                    //}

                    //检查批次号是否存在
                    var endPBList = productWarehouseRepository.GetFinInRecordBtchID(finInRecord.BatchID).ToList();

                    if (endPBList.Count == 0)
                    {
                        //批次号不存在    加参数
                        throw new Exception(SM_Store.SMSG_STORE_E000017);
                    }
                }

                for (int i = 0; i < inRecordList.Length; i++)
                {
                    //判断计划单号是否输入
                    if (inRecordList[i]["PlanID"].Length == 0)
                    {
                        //第【N】行的计划单号未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E00004, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        ////判断输入的计划单号是否是数字和字符
                        //if (Regex.IsMatch(inRecordList[i]["PlanID"], "[0-9]") == false || Regex.IsMatch(inRecordList[i]["PlanID"], "[/u0000-/u00FF]") == false)
                        //{
                        //    //第【N】行的计划单号输入不合法    加参数
                        //    errorList += ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E00009, new string[] { inRecordList[i]["RowIndex"] });
                        //}

                        //检查计划单号是否存在
                        var endPList = productWarehouseDetailRepository.GetFinInRecordPlanID(inRecordList[i]["PlanID"]).ToList();

                        if (endPList.Count == 0)
                        {
                            //第【N】行的计划单号不存在    加参数
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000018, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //判断检验单号是否输入
                    if (inRecordList[i]["ProductCheckID"].Length == 0)
                    {
                        //第【N】行的检验单号未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E00005, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        ////判断输入的检验单号是否是数字和字符
                        //if (Regex.IsMatch(inRecordList[i]["ProductCheckID"], "[0-9]") == false || Regex.IsMatch(inRecordList[i]["ProductCheckID"], "[/u0000-/u00FF]") == false)
                        //{
                        //    //第【N】行的检验单号输入不合法    加参数
                        //    errorList += ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000010, new string[] { inRecordList[i]["RowIndex"] });
                        //}

                        //检查检验单号是否存在
                        var endPCList = productWarehouseDetailRepository.GetFinInRecordProductCheckID(inRecordList[i]["ProductCheckID"]).ToList();

                        if (endPCList.Count == 0)
                        {
                            //第【N】行的检验单号不存在    加参数
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000019, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    
                    }

                    //判断物料编号是否输入
                    if (inRecordList[i]["OrderProductID"].Length == 0)
                    {
                        //第【N】行的物料编号未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E00006, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        ////判断输入的物料编号是否是数字和字符
                        //if (Regex.IsMatch(inRecordList[i]["OrderProductID"], "[0-9]") == false || Regex.IsMatch(inRecordList[i]["OrderProductID"], "[/u0000-/u00FF]") == false)
                        //{
                        //    //第【N】行的物料编号输入不合法    加参数
                        //    errorList += ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000011, new string[] { inRecordList[i]["RowIndex"] });
                        //}

                        ////由零件略称得到其ID
                        //var endList = partInfoRepository.GetPartID(inRecordList[i]["OrderProductID"]).ToList();
                        //inRecordList[i]["OrderProductID"] = endList[0].PartId;
                       
                        //检查物料编号是否存在
                        var endPPList = productWarehouseDetailRepository.GetFinInRecordOrProductID(inRecordList[i]["OrderProductID"]).ToList();

                        if (endPPList.Count == 0)
                        {
                            //第【N】行的物料编号不存在    加参数
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000020, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //判断第【N】行的合格数量是否输入
                    if (inRecordList[i]["QualifiedQuantity"].Length == 0)
                    {
                        //第【N】行的合格数量未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000022, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        //判断输入的合格数量是否是数字
                        if (Regex.IsMatch(inRecordList[i]["QualifiedQuantity"].ToString(), @"^[0-9]*$") == false)
                        {
                            //第【N】行的合格数量输入不合法    加参数
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000012, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //判断第【N】行的每箱数量是否输入
                    if (inRecordList[i]["EachBoxQuantity"].Length == 0)
                    {
                        //第【N】行的每箱数量未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000023, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        //判断输入的每箱数量是否是数字
                        if (Regex.IsMatch(inRecordList[i]["EachBoxQuantity"].ToString(), "[0-9]") == false)
                        {
                            //第【N】行的每箱数量输入不合法    加参数
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000013, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //判断第【N】行的箱数是否输入
                    if (inRecordList[i]["BoxQuantity"].Length == 0)
                    {
                        //第【N】行的箱数未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000024, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        //判断输入的箱数是否是数字
                        if (Regex.IsMatch(inRecordList[i]["BoxQuantity"], "[0-9]") == false)
                        {
                            //第【N】行的箱数输入不合法    加参数
                         throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000014, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //判断第【N】行的零头是否输入
                    if (inRecordList[i]["RemianQuantity"].Length == 0)
                    {
                        //第【N】行的零头未输入    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000025, new string[] { inRecordList[i]["RowIndex"] }));
                    }
                    else
                    {
                        //判断输入的零头是否是数字
                        if (Regex.IsMatch(inRecordList[i]["RemianQuantity"].ToString(), "[0-9]") == false)
                        {
                            //第【N】行的零头输入不合法    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000015, new string[] { inRecordList[i]["RowIndex"] }));
                        }
                    }

                    //根据成品交仓单号+批次号+计划单号+检验单号+装配小组+物料编号+规格型号检索
                    var endPDList = productWarehouseDetailRepository.GetFinInRecordDetail(finInRecord.ProductWarehouseID, inRecordList[i]["PlanID"],
                        inRecordList[i]["ProductCheckID"], inRecordList[i]["TeamID"], inRecordList[i]["OrderProductID"], inRecordList[i]["ProductSpecification"]).ToList();

                    if (endPDList.Count == 0)
                    {
                        //第【N】行的交仓详细数据不存在    加参数
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E000021, new string[] { inRecordList[i]["RowIndex"] }));
                    }


                    //成品库添加入库履历数据
                    FinInForLoginAddInRecord(finInRecord,inRecordList,i, uId);
                }
            }
            //详细跳转
            else if (editFlg == "1")
            {
                for (int i = 0; i < inRecordList.Length; i++)
                {
                    ////由零件略称得到其ID
                    //var endList = partInfoRepository.GetPartID(inRecordList[i]["OrderProductID"]).ToList();
                    //inRecordList[i]["OrderProductID"] = endList[0].PartId;
                    //入库详细编辑
                    FinInRecordForELogin(finInRecord, inRecordList, i, uId);
                }
            }
            //入库跳转
            else
            {
                ////由零件略称得到其ID
                //for (int i = 0; i < inRecordList.Length; i++)
                //{
                //    var endList = partInfoRepository.GetPartID(inRecordList[i]["OrderProductID"]).ToList();
                //    inRecordList[i]["OrderProductID"] = endList[0].PartId;
                //}
                List<BthStockList> bsl = new List<BthStockList>();

                //入库履历表添加判断
                var finInList = finInRecordRepository.GetFinInRecordPlanID(finInRecord.ProductWarehouseID).ToList();
                if (finInList.Count == 0)
                {
                    //成品库入库履历添加
                    bool ire = finInRecordRepository.Add(new FinInRecord
                    {
                        PlanID = finInRecord.ProductWarehouseID,
                        BatchID = finInRecord.BatchID,
                        WareHouseID = finInRecord.WareHouseID,
                        WareHousePositionID = "12",
                        InMoveCls = finInRecord.InMoveCls,
                        FsInID = finInRecord.ProductWarehouseID,
                        InDate = finInRecord.InDate,
                        WareHouseKpID = finInRecord.WareHouseKpID,
                        Remarks = finInRecord.MRemarks,
                        EffeFlag = "0",
                        DelFlag = "0",
                        CreUsrID = uId,
                        CreDt = DateTime.Today,
                        UpdUsrID = uId,
                        UpdDt = DateTime.Today
                    });
                    if (ire == false)
                    {
                        throw new Exception("添加失败");
                    }

                }
                else
                {
                    throw new Exception("添加失败");
                }

                for (int i = 0; i < inRecordList.Length; i++)
                {
                    //成品库添加入库履历详细数据
                    FinInForLoginAddInRecord(finInRecord, inRecordList, i, uId);

                    //成品库入库登录修改仓库预约表
                    FinInForLoginUpdateReserve(finInRecord, inRecordList, i, uId);

                    //成品库入库登录新添加仓库预约详细表数据
                    FinInForLoginAddReserveDetails(finInRecord, inRecordList, i, uId);

                    //成品库入库登录修改仓库表
                    FinInForLoginUpdateMaterial(finInRecord, inRecordList, i, uId);

                    //成品库入库登录添加批次别库存表
                    BthStockList bs = new BthStockList();
                    bs.BillType = "01";
                    bs.PrhaOdrID = finInRecord.ProductWarehouseID;
                    bs.BthID = finInRecord.BatchID;
                    bs.WhID = finInRecord.WareHouseID;
                    bs.PdtID = inRecordList[i]["OrderProductID"];
                    bs.PdtSpec = inRecordList[i]["ProductSpecification"];
                    bs.GiCls = "";
                    bs.Qty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
                    bs.OrdeQty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
                    bs.CmpQty = 0;
                    bs.DisQty = 0;
                    bs.InDate = DateTime.Today;
                    bs.DiscardFlg = "0";
                    bs.EffeFlag = "0";
                    bs.DelFlag = "0";
                    bs.CreUsrID = uId;
                    bs.CreDt = DateTime.Today;
                    bs.UpdUsrID = uId;
                    bs.UpdDt = DateTime.Today;

                    bsl.Add(bs);
                }

                //成品库入库登录添加批次别库存表
                bool ibsl = storeExternalService.WhInInsertBthStockList(uId, bsl);
                if (ibsl == false)
                {
                    throw new Exception("修改失败");
                }

                //成品库入库登录修改成品交仓单表交仓状态
                FinInForLoginUpdateProdWarehouse(finInRecord, uId);
            }

            return "更新成功";
        }

        #region 入库详细点击编辑更新成品库入库履历及详细表

        /// <summary>
        /// 对输入的数据进行检查并更新(入库详细编辑更新成品库入库履历及详细表)
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInRecordForELogin(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId)
        {
            //修改成品库入库履历及详细表(字段赋值)
            FinInDetailRecord finInDetailRecord = new FinInDetailRecord();
            FinInRecord ufinInRecord = new FinInRecord();
            finInDetailRecord.FsInID = finInRecord.ProductWarehouseID;
            finInDetailRecord.IsetRepID = inRecordList[i]["ProductCheckID"];
            finInDetailRecord.ProductID = inRecordList[i]["OrderProductID"];
            var clientOrder = inRecordList[i]["PlanID"].Split('/');
            finInDetailRecord.TecnProcess = clientOrder[0];
            finInDetailRecord.ClientOrderDetail = clientOrder[1];

            finInDetailRecord.Quantity = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
            finInDetailRecord.ProScrapQuantity = Convert.ToDecimal(inRecordList[i]["EachBoxQuantity"]);
            finInDetailRecord.ProMaterscrapQuantity = Convert.ToDecimal(inRecordList[i]["BoxQuantity"]);
            finInDetailRecord.ProOverQuantity = Convert.ToDecimal(inRecordList[i]["RemianQuantity"]);
            finInDetailRecord.UpdDt = DateTime.Today;
            finInDetailRecord.UpdUsrID = uId;

            ufinInRecord.PlanID = finInRecord.ProductWarehouseID;
            ufinInRecord.Remarks = finInRecord.MRemarks;
            ufinInRecord.UpdDt = DateTime.Today;
            ufinInRecord.UpdUsrID = uId;
            //修改成品库入库履历及详细表
            bool uir = finInRecordRepository.UpdateInFinInRecord(ufinInRecord);
            if (uir == false)
            {
                throw new Exception("修改失败");
            }
            bool uidr = finInDetailRecordRepository.UpdateInFinInDetailRecord(finInDetailRecord);
            if (uidr == false)
            {
                throw new Exception("修改失败");
            }

        }
        
        #endregion

        #region 成品库添加入库履历数据（插入入库履历详细表）

        /// <summary>
        /// 成品库添加入库履历详细数据
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginAddInRecord(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId)
        {
            //入库履历详细表添加判断
            var endDList = finInDetailRecordRepository.GetFinInRecordDetailList(finInRecord.ProductWarehouseID, inRecordList, i).ToList();

            if (endDList.Count == 0)
            {
                var clientOrder = inRecordList[i]["PlanID"].Split('/');

                //成品库入库履历详细添加
                FinInDetailRecord detail = new FinInDetailRecord();
                detail.FsInID = finInRecord.ProductWarehouseID;
                detail.IsetRepID = inRecordList[i]["ProductCheckID"];
                detail.GiCls = inRecordList[i]["GiclsProduct"];
                detail.ProductID = inRecordList[i]["OrderProductID"];
                detail.ProductSpec = inRecordList[i]["ProductSpecification"];

                detail.TecnProcess = clientOrder[0];
                detail.ClientOrderDetail = clientOrder[1];
                detail.Quantity = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
                detail.ProScrapQuantity = Convert.ToDecimal(inRecordList[i]["EachBoxQuantity"]);
                detail.ProMaterscrapQuantity = Convert.ToDecimal(inRecordList[i]["BoxQuantity"]);
                detail.ProOverQuantity = Convert.ToDecimal(inRecordList[i]["RemianQuantity"]);
                detail.ValuatUp = 2;
                detail.PrchsUp = 12;
                detail.NotaxAmt = 22;
                detail.InDate = finInRecord.InDate;
                detail.WareHouseKpID = finInRecord.WareHouseKpID;
                detail.Remarks = finInRecord.MRemarks;
                detail.EffeFlag = "0";
                detail.DelFlag = "0";
                detail.CreUsrID = uId;
                detail.CreDt = DateTime.Today;
                detail.UpdUsrID = uId;
                detail.UpdDt = DateTime.Today;
                bool idre = finInDetailRecordRepository.Add(detail);

                if (idre == false)
                {
                    throw new Exception("添加失败");
                }
            }
            else
            {
                throw new Exception("添加失败");
            }
        }

        #endregion

        #region 成品库入库登录修改仓库预约表

        /// <summary>
        /// 成品库入库登录修改仓库预约表
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginUpdateReserve(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId)
        {
            //修改仓库预约表(字段赋值)
            Reserve reser = new Reserve();
            reser.WhID = finInRecord.WareHouseID;
            var clientOrder = inRecordList[i]["PlanID"].Split('/');
            reser.ClnOdrID = clientOrder[0];
            reser.ClnOdrDtl = clientOrder[1];
            reser.PdtID = inRecordList[i]["OrderProductID"];

            reser.RecvQty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
            reser.UpdDt = DateTime.Today;
            reser.UpdUsrID = uId;
              
            //修改仓库预约表
            bool uir = reserveRepository.UpdateInReserveRecvQuantity(reser);
            if (uir == false)
            {
                throw new Exception("修改失败");
            }
   
            }

        #endregion

        #region 成品库入库登录新添加仓库预约详细表数据

        /// <summary>
        /// 成品库入库登录新添加仓库预约详细表数据
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginAddReserveDetails(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId)
        {
            Reserve reser = new Reserve();
            reser.WhID = finInRecord.WareHouseID;
            var clientOrder = inRecordList[i]["PlanID"].Split('/');
            reser.ClnOdrID = clientOrder[0];
            reser.ClnOdrDtl = clientOrder[1];
            reser.PdtID = inRecordList[i]["OrderProductID"];
            //获得预约批次详细单号
            var reserveDetailListID = reserveRepository.GetReserveDetailList(reser).ToList();

           //成品库入库仓库预约详细添加
           ReserveDetail detail = new ReserveDetail();
           detail.OrdeBthDtailListID = reserveDetailListID[0].OrdeBthDtailListID;
           detail.BthID = finInRecord.BatchID;
           detail.OrderQty = reserveDetailListID[0].OrdeQty;
           detail.PickOrdeQty = reserveDetailListID[0].PickOrdeQty;
           detail.CmpQty = 0;
           detail.EffeFlag = "0";
           detail.DelFlag = "0";
           detail.CreUsrID = uId;
           detail.CreDt = DateTime.Today;
           detail.UpdUsrID = uId;
           detail.UpdDt = DateTime.Today;
           bool rdr = reserveDetailRepository.Add(detail);

           if (rdr == false)
           {
               throw new Exception("添加失败");
           }
        }

        #endregion

        #region 成品库入库登录修改仓库表

        /// <summary>
        /// 成品库入库登录修改仓库表
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="inRecordList">更新数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginUpdateMaterial(VM_StoreFinInRecordForDetailShow finInRecord, Dictionary<string, string>[] inRecordList, int i, string uId)
        {
            //修改仓库表(字段赋值)
            Material material = new Material();
            material.WhID = finInRecord.WareHouseID;
            material.PdtID = inRecordList[i]["OrderProductID"];

            ProdInfo productInfo = prodInfoRepository.GetProdInfoById(inRecordList[i]["OrderProductID"]);

            material.CurrentQty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
            material.TotalAmt = productInfo.Pricee * Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
            material.TotalValuatUp = productInfo.Evaluate * Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]);
            material.UpdDt = DateTime.Today;
            material.UpdUsrID = uId;

            //修改仓库表
            bool uim=materialRepository.updateInMaterialFinIn(material);
            if (uim == false)
            {
                throw new Exception("修改失败");
            }

        }

        #endregion

        #region 成品库入库登录修改成品交仓单表交仓状态

        /// <summary>
        /// 成品库入库登录修改成品交仓单表交仓状态
        /// </summary>
        /// <param name="finInRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginUpdateProdWarehouse(VM_StoreFinInRecordForDetailShow finInRecord, string uId)
        {
            //修改成品交仓单表(字段赋值)
            ProductWarehouse productWarehouse = new ProductWarehouse();
            productWarehouse.ProductWarehouseID = finInRecord.ProductWarehouseID;

            //修改交仓状态为2（已入库）
            productWarehouse.WarehouseState = "2";
            productWarehouse.UpdDt = DateTime.Today;
            productWarehouse.UpdUsrID = uId;

            //修改成品交仓单表
            bool uipw = productWarehouseRepository.updateInProductWarehouse(productWarehouse);
            if(uipw == false)
            {
                throw new Exception("修改失败");
            }
        }

        #endregion

        #region 入库履历详细添加批次别库存表（不用）

        /// <summary>
        /// 成品库入库登录添加批次别库存表
        /// </summary>
        /// <param name="finInRecord">添加数据集合</param>
        /// <param name="inRecordList">添加数据集合</param>
        /// <param name="i">行参数</param>
        /// <param name="uId">登录人员ID</param>
        public void FinInForLoginAddBthStockList(VM_StoreFinInRecordForDetailShow finInRecord,Dictionary<string, string>[] inRecordList, int i,string uId)
        {
            //批次别库存表添加
            bool abs = bthStockListRepository.Add(new BthStockList
            {
                BillType = "01",
                PrhaOdrID = finInRecord.ProductWarehouseID,
                BthID = finInRecord.BatchID,
                WhID = finInRecord.WareHouseID,
                PdtID = inRecordList[i]["OrderProductID"],
                PdtSpec = inRecordList[i]["ProductSpecification"],
                GiCls = "",
                Qty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]),
                OrdeQty = Convert.ToDecimal(inRecordList[i]["QualifiedQuantity"]),
                CmpQty = 0,
                DisQty = 0,
                InDate = DateTime.Today,
                DiscardFlg = "0",
                EffeFlag= "0",
                DelFlag = "0",
                CreUsrID = uId,
                CreDt = DateTime.Today,
                UpdUsrID = uId,
                UpdDt = DateTime.Today 

            });
            if (abs == false)
            {
                throw new Exception("修改失败");
            }

        }

        #endregion

        /// <summary>
        /// 入库履历一览删除（伪删除）
        /// </summary>
        /// <param name="list">删除的数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <returns>true</returns>
        public bool DeleteFinInStore(List<string> list,string uId)
        {
            foreach (var Id in list)
            {
                //删除入库履历详细表里数据
                finInRecordRepository.updateFinInRecordDetail(new FinInDetailRecord { FsInID = Id, DelFlag = "1",UpdUsrID = uId, UpdDt = DateTime.Today });
                //删除入库履历表里数据
                finInRecordRepository.updateFinInRecord(new FinInRecord { PlanID = Id, DelFlag = "1", UpdUsrID = uId, UpdDt = DateTime.Today });
            }
            return true;
        }

        /// <summary>
        /// 获得入库详细画面数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_StoreFinInRecordForDetailShow> GetFinInRecordDetailById(string productWarehouseID, Paging page)
        {
            return finInRecordRepository.GetFinInRecordDetailByIdWithPaging(productWarehouseID, page);
        }

        /// <summary>
        /// 入库详细手工登录状态，根据输入的零件略称自动生成零件名称（暂时不用）
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        public List<PartInfo> GetFinInRecordPdtInfoById(string partAbbrevi)
        {
            var productInfo = partInfoRepository.GetFinRecordProductInfoById(partAbbrevi);
            return productInfo;
        }

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（详细跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForTableShow IFinInStoreService.GetDetailInformation(string productWarehouseID)
        {
            return finInRecordRepository.GetFinInRecordInfoById(productWarehouseID);
        }

        /// <summary>
        /// 入库详细画面加载  根据交仓单号取其余数据（入库新添加跳转）
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>入库详细数据</returns>
        VM_StoreFinInRecordForDetailShow IFinInStoreService.GetDetailInformations(string productWarehouseID)
        {
            return productWarehouseRepository.GetFinInRecordInfosById(productWarehouseID);
        }

    }
}
