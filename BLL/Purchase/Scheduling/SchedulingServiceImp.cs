/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SchedulingServiceImp.cs
// 文件功能描述：
//          外购排产和外协排产的Service接口的实现类
//      
// 修改履历：2013/12/23 陈阵 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using Extensions;
using BLL_InsideInterface;
using BLL.ServerMessage;

namespace BLL
{
    /// <summary>
    /// 外购排产和外协排产的Service接口的实现类
    /// </summary>
    public class SchedulingServiceImp : AbstractService, ISchedulingService
    {
        // 外购指示表的Repository类
        private IPurchaseInstructionRepository purInstructRepos;

        // 外协指示表的Repository类
        private ISupplierInstructionRepository suppInstructRepos;

        // 采购部门的内部Service接口（更新外购表及外协表用）
        private IPurchase4InernalService purchase4InernalService;

        // 生产部门的外部Service接口（更新外购外协指示表用）
        private IProduceForExternalService produceForExternalService;

        /// <summary>
        /// 外购排产和外协排产的Service接口的实现类的构造方法
        /// </summary>
        /// <param name="purchaseRepository">外购指示表的Repository</param>
        /// <param name="supplierRepository">外协指示表的Repository</param>
        /// <param name="purchase4InernalService">采购部门的内部Service</param>
        /// <param name="produceForExternalService">生产部门的外部Service</param>
        public SchedulingServiceImp(IPurchaseInstructionRepository purInstructRepos, ISupplierInstructionRepository suppInstructRepos, 
            IPurchase4InernalService purchase4InernalService, IProduceForExternalService produceForExternalService)
        {
            this.purInstructRepos = purInstructRepos;
            this.suppInstructRepos = suppInstructRepos;
            this.purchase4InernalService = purchase4InernalService;
            this.produceForExternalService = produceForExternalService;
        }

        /// <summary>
        /// 取得外购排产画面的显示信息
        /// </summary>
        /// <param name="insturctions">外购指示</param>
        /// <returns>外购排产画面的显示信息</returns>
        public IEnumerable<VM_PurchaseScheduling> GetPurchaseSchedulingInfo(string[] instructions)
        {
            // 返回repository的结果
            return purInstructRepos.GetPurchaseSchedulingInfo(instructions);
        }

        /// <summary>
        /// 外购排产 - 生成订单
        /// </summary>
        /// <param name="orderInfoList">排产订单信息</param>
        /// <param name="userID">排产用户ID</param>
        /// <param name="date">排产时间</param>
        /// <returns>排产结果（true:排产成功  false:排产失败）</returns>
        public Boolean MakeOrder4Purchase(Dictionary<string, string>[] orderInfoList, String userID, DateTime date)
        {
            // 向数据库中添加的数据对象
            List<OutSourceOrderInfo> insertDataList = new List<OutSourceOrderInfo>();

            // 对外购排产信息进行业务check
            checkInput4Pur(orderInfoList);

            #region 封装外购信息
            // 外购单号的List
            List<String> outOrderID = new List<String>();

            // 遍历排产订单信息
            for (int i = 0; i < orderInfoList.Length; i++)
            {
                // 生产订单用的外购单对象
                OutSourceOrderInfo insertData = new OutSourceOrderInfo();

                // 是否是新订单Flag
                bool isNewOrder = true;

                // 如果是第一条数据
                if (i == 0)
                {
                    // 封装外购单信息
                    insertData = PurDataPackage(i, orderInfoList, userID, date);
                    
                    // 将addObject添加进list
                    insertDataList.Add(insertData);
                    
                    // 外购单号存放进list，以备下一轮判断
                    outOrderID.Add(orderInfoList[i]["OutOrderID"]);
                }
                // 如果不是第一条数据
                else 
                {
                    // 遍历排产订单信息
                    for (int j = 0; j < outOrderID.Count; j++)
                    {
                        // 外购单号相同时，不需生产新的订单
                        if (orderInfoList[i]["OutOrderID"].Equals(outOrderID[j]))
                        {
                            isNewOrder = false;
                        }
                    }
                    // 需生产新订单时
                    if (isNewOrder)
                    {
                        // 封装外购单信息
                        insertData = PurDataPackage(i, orderInfoList, userID, date);

                        // 将addObject添加进list
                        insertDataList.Add(insertData);

                        // 外购单号存放进list，以备下一轮判断
                        outOrderID.Add(orderInfoList[i]["OutOrderID"]);
                    }
                }
            }
            #endregion

            // 更新外购单表和外购单详细表
            purchase4InernalService.AddOutSourceOrder(insertDataList);

            #region 更新外购指示表
            // 遍历外购对象
            foreach(OutSourceOrderInfo orderInfo in insertDataList)
            {
                // 外购单明细表信息
                List<MCOutSourceOrderDetail> details = orderInfo.OutDetailsList;
                // 遍历外购单明细表信息
                foreach (MCOutSourceOrderDetail detail in details)
                {
                    // 更新外购指示表
                    produceForExternalService.UpdPurchInstruc4Add(detail.CustomerOrderID, detail.CustomerOrderDetailID,
                        detail.ProductPartID, detail.RequestQuantity, userID, date);
                }
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 外购排产信息的业务check
        /// </summary>
        /// <param name="orderList">外购排产信息</param>
        private void checkInput4Pur(Dictionary<string, string>[] orderList)
        {
            // 定义错误Message
            string errMsg = "";

            // 遍历外购排产信息
            for (int i = 0; i < orderList.Length; i++)
            {
                #region 对采购数量的check
                // 采购数量未输入时
                if (String.IsNullOrEmpty(orderList[i]["RequestQuantity"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00071, new string[] { orderList[i]["RowID"] });
                }
                else{
                    // 采购数量 > 待购数量时
                    if (Convert.ToDecimal(orderList[i]["RequestQuantity"]) > Convert.ToDecimal(orderList[i]["WaitingNum"]))
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00072, new string[] { orderList[i]["RowID"] });
                    }
                    // 输入的采购数量的BYTE数 > 10 时
                    if (StringUtil.getByteCnt(orderList[i]["RequestQuantity"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00073, new string[] { orderList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 对供货商的check
                // 供货商未输入时
                if (String.IsNullOrEmpty(orderList[i]["OutCompanyID"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00066, new string[] { orderList[i]["RowID"] });
                }
                #endregion

                #region 对单价的check
                // 单价未输入时，报错
                if (String.IsNullOrEmpty(orderList[i]["UnitPrice"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00067, new string[] { orderList[i]["RowID"] });
                }
                else
                {
                    // 输入的单价的BYTE数 > 10 时
                    if (StringUtil.getByteCnt(orderList[i]["UnitPrice"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00068, new string[] { orderList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 对估价的check
                // 估价未输入时，
                if (String.IsNullOrEmpty(orderList[i]["Evaluate"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00069, new string[] { orderList[i]["RowID"] });
                }
                else
                {
                    // 输入的估价的BYTE数 > 10时
                    if (StringUtil.getByteCnt(orderList[i]["Evaluate"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00070, new string[] { orderList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 交货日期的check
                // 交货日期未输入时
                if (String.IsNullOrEmpty(orderList[i]["DeliveryDate"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { orderList[i]["RowID"] });
                }
                #endregion
            }

            // 若错误Message不为空，抛出异常。
            if (!String.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 封装外购信息
        /// </summary>
        /// <param name="idx">行数（索引）</param>
        /// <param name="orderList">外购信息</param>
        /// <param name="userID">下单人UserID</param>
        /// <param name="date">下单日期</param>
        /// <returns></returns>
        private OutSourceOrderInfo PurDataPackage(int idx, Dictionary<string, string>[] orderList, String userID, DateTime date)
        {
            // 返回结果
            OutSourceOrderInfo result = new OutSourceOrderInfo();

            #region  封装外购单表的数据
            // 外购单表
            MCOutSourceOrder outSourceInfo = new MCOutSourceOrder();
            // 1.外购单号：自动生成的外购单号
            outSourceInfo.OutOrderID = orderList[idx]["OutOrderID"];
            // 2.紧急状态
            string urgentSts = orderList[idx]["UrgentStatus"];
            for (int j = 0; j < orderList.Length; j++)
            {
                // 客户订单号相同时，取最大的紧急状态Code
                if ((orderList[idx]["OutOrderID"].Equals(orderList[j]["OutOrderID"]))
                    && (Convert.ToDecimal(urgentSts) < Convert.ToDecimal(orderList[j]["UrgentStatus"])))
                {
                    urgentSts = orderList[j]["UrgentStatus"];
                }
            }
            outSourceInfo.UrgentStatus = urgentSts;
            // 3.外购部门ID：画面上的生产部门ID
            outSourceInfo.DepartmentID = orderList[idx]["DepartmentID"];
            // 4.外购单位ID：画面上输入的供货商ID
            outSourceInfo.OutCompanyID = orderList[idx]["OutCompanyID"];
            // 5.外购单状态：2（已批准）
            outSourceInfo.OutOrderStatus = Constant.OutOrderStatus.STATUST;
            // 6.编制人
            outSourceInfo.EstablishUID = userID;
            // 7.编制时间
            outSourceInfo.EstablishDate = DateTime.Now;
            // 8.外购单区分：0（生产）
            outSourceInfo.OutOrderFlg = Constant.OutOrderType.PRODUCT;
            // 9.打印区分：0（未打印）
            outSourceInfo.PrintFlag = Constant.GLOBAL_PRINTLAG_OFF;
            // 10.创建人
            outSourceInfo.CreUsrID = userID;
            // 11.创建时间
            outSourceInfo.CreDt = date;
            // 12.修改人
            outSourceInfo.UpdUsrID = userID;
            // 13.修改时间
            outSourceInfo.UpdDt = date;

            // 将外购单表信息添加到返回结果里
            result.OutOrderInfo = outSourceInfo;
            #endregion

            #region  封装外购单详细表的数据
            // 外购单详细表List
            List<MCOutSourceOrderDetail> detailList = new List<MCOutSourceOrderDetail>();

            // 遍历外购信息
            for (int j = 0; j < orderList.Length; j++)
            {
                // 抽取该外购单的详细信息
                if (orderList[idx]["OutOrderID"] == orderList[j]["OutOrderID"])
                {
                    // 外购单详细表
                    MCOutSourceOrderDetail detail = new MCOutSourceOrderDetail();
                    // 1.外购单号：自动生产外购单号
                    detail.OutOrderID = orderList[j]["OutOrderID"];
                    // 2.客户订单号：画面的客户订单号
                    detail.CustomerOrderID = orderList[j]["CustomerOrderID"];
                    // 3.客户订单明细号：画面的客户订单明细号
                    detail.CustomerOrderDetailID = orderList[j]["CustomerOrderDetailID"];
                    // 4.产品零件ID：画面的产品零件ID
                    detail.ProductPartID = orderList[j]["ProductPartID"];
                    // 5.产品ID：画面的产品ID
                    detail.ProductID = orderList[j]["ProductID"];
                    // 6.材料和规格要求：画面的材料规格要求
                    detail.MaterialsSpecReq = orderList[j]["Specifica"];
                    // TODO:调用仓库编码的共通函数
                    // 7.仓库编码 （待修改）
                    detail.WarehouseID = "1";
                    // 8.单价：画面上的单价
                    detail.UnitPrice = Convert.ToDecimal(orderList[j]["UnitPrice"]);
                    // 9.估价：画面上的估价
                    detail.Evaluate = Convert.ToDecimal(orderList[j]["Evaluate"]);
                    // 10.单据要求数量：画面上的采购数量
                    detail.RequestQuantity = Convert.ToDecimal(orderList[j]["RequestQuantity"]);
                    // 11.实际入库数量
                    detail.ActualQuantity = 0;
                    // 12.其他数量
                    detail.OtherQuantity = 0;
                    // 13.完成状态：0（未完成）
                    detail.CompleteStatus = Constant.PurchaseCompleteStatus.UNCOMPLETED;
                    // 14.交货日期：画面上的交货日期
                    detail.DeliveryDate = Convert.ToDateTime(orderList[j]["DeliveryDate"]);
                    // 15.创建人
                    detail.CreUsrID = userID;
                    // 16.创建日期
                    detail.CreDt = date;
                    // 17.修改人
                    detail.UpdUsrID = userID;
                    // 18.修改日期
                    detail.UpdDt = date;

                    detailList.Add(detail);
                }
            }
            //数据封装进addObject
            result.OutDetailsList = detailList;
            #endregion

            // 返回封装结果
            return result;
        }

        /// <summary>
        /// 取得外协排产画面的显示信息
        /// </summary>
        /// <param name="schedulings">外协指示</param>
        /// <returns>外协排产画面的显示信息</returns>
        public IEnumerable<VM_SupplierScheduling> GetSupplierSchedulingInfo(string[] schedulings)
        {
            // 返回repository的结果
            return suppInstructRepos.GetSupplierSchedulingInfo(schedulings);
        }

        /// <summary>
        /// 外协排产 - 生成订单
        /// </summary>
        /// <param name="orderList">排产订单信息</param>
        /// <param name="userID">排产用户ID</param>
        /// <param name="date">排产时间</param>
        /// <returns>排产结果（true:排产成功  false:排产失败）</returns>
        public Boolean MakeOrder4Supplier(Dictionary<string, string>[] orderInfoList, String userID, DateTime date)
        {
            // 向数据库中添加的数据对象
            List<SupplierOrderInfo> insertDataList = new List<SupplierOrderInfo>();

            // 对外协排产信息进行业务check
            checkInput4Supp(orderInfoList);

            #region 封装外协信息
            // 外协单号的List
            List<String> suppOrderID = new List<String>();

            // 遍历排产订单信息
            for (int i = 0; i < orderInfoList.Length; i++)
            {
                // 生成订单用的外协单对象
                SupplierOrderInfo insertData = new SupplierOrderInfo();

                // 是否是新订单Flag
                bool isNewOrder = true;

                // 如果是第一条数据
                if (i == 0)
                {
                    // 封装外协单信息
                    insertData = SuppDataPackage(i, orderInfoList, userID, date);

                    // 将addObject添加进list
                    insertDataList.Add(insertData);

                    // 外协单号存放进list，以备下一轮判断
                    suppOrderID.Add(orderInfoList[i]["SuppOrderID"]);
                }
                // 如果不是第一条数据
                else
                {
                    // 遍历排产订单信息
                    for (int j = 0; j < suppOrderID.Count; j++)
                    {
                        // 外购单号相同时，不需生产新的订单
                        if (orderInfoList[i]["SuppOrderID"].Equals(suppOrderID[j]))
                        {
                            isNewOrder = false;
                        }
                    }
                    // 需生产新订单时
                    if (isNewOrder)
                    {
                        // 封装外协单信息
                        insertData = SuppDataPackage(i, orderInfoList, userID, date);

                        // 将addObject添加进list
                        insertDataList.Add(insertData);

                        // 外协单号存放进list，以备下一轮判断
                        suppOrderID.Add(orderInfoList[i]["SuppOrderID"]);
                    }
                }
            }
            #endregion
            
            // 更新外购单表和外购单详细表
            purchase4InernalService.AddSupplierOrder(insertDataList);

            #region 更新外协指示表
            // 遍历外协对象
            foreach (SupplierOrderInfo orderInfo in insertDataList)
            {
                // 外协单明细表信息
                List<MCSupplierOrderDetail> details = orderInfo.SupDetailsList;
                // 遍历外协单明细表信息
                foreach (MCSupplierOrderDetail detail in details)
                {
                    // 更新外协指示表
                    produceForExternalService.UpdAssiInstruc4Add(detail.CustomerOrderID, detail.CustomerOrderDetailID,
                        detail.ProductPartID, detail.RequestQuantity, userID, date);
                }
            }
            #endregion

            return true;          
        }

        /// <summary>
        /// 外协排产信息的业务check
        /// </summary>
        /// <param name="orderList">外协排产信息</param>
        private void checkInput4Supp(Dictionary<string, string>[] orderInfoList)
        {
            // 定义错误Message
            string errMsg = "";

            // 遍历外购排产信息
            for (int i = 0; i < orderInfoList.Length; i++)
            {
                #region 对采购数量的check
                // 采购数量未输入时
                if (String.IsNullOrEmpty(orderInfoList[i]["RequestQuantity"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00074, new string[] { orderInfoList[i]["RowID"] });
                }
                else
                {
                    // 采购数量 > 待购数量时
                    if (Convert.ToDecimal(orderInfoList[i]["RequestQuantity"]) > Convert.ToDecimal(orderInfoList[i]["WaitingNum"]))
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00075, new string[] { orderInfoList[i]["RowID"] });
                    }
                    // 输入的采购数量的BYTE数 > 10 时
                    if (StringUtil.getByteCnt(orderInfoList[i]["RequestQuantity"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00076, new string[] { orderInfoList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 对供货商的check
                // 供货商未输入时
                if (String.IsNullOrEmpty(orderInfoList[i]["OutCompanyID"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00077, new string[] { orderInfoList[i]["RowID"] });
                }
                #endregion

                #region 对单价的check
                // 单价未输入时，报错
                if (String.IsNullOrEmpty(orderInfoList[i]["UnitPrice"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00067, new string[] { orderInfoList[i]["RowID"] });
                }
                else
                {
                    // 输入的单价的BYTE数 > 10 时
                    if (StringUtil.getByteCnt(orderInfoList[i]["UnitPrice"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00068, new string[] { orderInfoList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 对估价的check
                // 估价未输入时，
                if (String.IsNullOrEmpty(orderInfoList[i]["Evaluate"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00069, new string[] { orderInfoList[i]["RowID"] });
                }
                else
                {
                    // 输入的估价的BYTE数 > 10时
                    if (StringUtil.getByteCnt(orderInfoList[i]["Evaluate"]) > 10)
                    {
                        errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00070, new string[] { orderInfoList[i]["RowID"], "10" });
                    }
                }
                #endregion

                #region 交货日期的check
                // 交货日期未输入时
                if (String.IsNullOrEmpty(orderInfoList[i]["DeliveryDate"]))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { orderInfoList[i]["RowID"] });
                }
                #endregion
            }

            // 若错误Message不为空，抛出异常。
            if (!String.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
        }

        /// <summary>
        /// 封装外协信息
        /// </summary>
        /// <param name="idx">行数（索引）</param>
        /// <param name="orderList">外协信息</param>
        /// <param name="userID">下单人UserID</param>
        /// <param name="date">下单日期</param>
        /// <returns></returns>
        private SupplierOrderInfo SuppDataPackage(int idx, Dictionary<string, string>[] orderInfoList, String userID, DateTime date)
        {
            // 返回结果
            SupplierOrderInfo result = new SupplierOrderInfo();

            #region  封装外协单表的数据
            // 外协单表
            MCSupplierOrder supplierInfo = new MCSupplierOrder();
            // 1.外协单号：自动生成的外协单号
            supplierInfo.SupOrderID = orderInfoList[idx]["SuppOrderID"];
            // 2.紧急状态
            string urgentSts = orderInfoList[idx]["UrgentStatus"];
            for (int j = 0; j < orderInfoList.Length; j++)
            {
                // 客户订单号相同时，取最大的紧急状态Code
                if ((orderInfoList[idx]["SuppOrderID"].Equals(orderInfoList[j]["SuppOrderID"]))
                    && (Convert.ToDecimal(urgentSts) < Convert.ToDecimal(orderInfoList[j]["UrgentStatus"])))
                {
                    urgentSts = orderInfoList[j]["UrgentStatus"];
                }
            }
            supplierInfo.UrgentStatus = urgentSts;
            // 3.外协部门ID：画面上的生产部门ID
            supplierInfo.DepartmentID = orderInfoList[idx]["DepartmentID"];
            // 4.外协单位ID：画面上输入的供货商ID
            supplierInfo.OutCompanyID = orderInfoList[idx]["OutCompanyID"];
            // 5.外协单状态：2（已批准）
            supplierInfo.SupOrderStatus = Constant.OutOrderStatus.STATUST;
            // 6.制单人
            supplierInfo.MarkUID = userID;
            // 7.制单时间
            supplierInfo.MarkSignDate = DateTime.Now;
            // 8.打印区分：0（未打印）
            supplierInfo.PrintFlag = Constant.GLOBAL_PRINTLAG_OFF;
            // 9.创建人
            supplierInfo.CreUsrID = userID;
            // 10.创建时间
            supplierInfo.CreDt = date;
            // 11.修改人
            supplierInfo.UpdUsrID = userID;
            // 12.修改时间
            supplierInfo.UpdDt = date;

            // 将外购单表信息添加到返回结果里
            result.SupOrderInfo = supplierInfo;
            #endregion

            #region  封装外协单详细表的数据
            // 外协单详细表List
            List<MCSupplierOrderDetail> detailList = new List<MCSupplierOrderDetail>();

            // 遍历外协信息
            for (int j = 0; j < orderInfoList.Length; j++)
            {
                // 抽取该外协单的详细信息
                if (orderInfoList[idx]["SuppOrderID"] == orderInfoList[j]["SuppOrderID"])
                {
                    // 外协单详细表
                    MCSupplierOrderDetail detail = new MCSupplierOrderDetail();
                    // 1.外购单号：自动生产外购单号
                    detail.SupOrderID = orderInfoList[j]["SuppOrderID"];
                    // 2.客户订单号：画面的客户订单号
                    detail.CustomerOrderID = orderInfoList[j]["CustomerOrderID"];
                    // 3.客户订单明细号：画面的客户订单明细号
                    detail.CustomerOrderDetailID = orderInfoList[j]["CustomerOrderDetailID"];
                    // 4.产品零件ID：画面的产品零件ID
                    detail.ProductPartID = orderInfoList[j]["ProductPartID"];
                    // 5.产品ID：画面的产品ID
                    detail.ProductID = orderInfoList[j]["ProductID"];
                    // 6.材料和规格要求：画面的材料规格要求
                    detail.MaterialsSpecReq = orderInfoList[j]["Specifica"];
                    // 7.加工工艺：画面的本道工序名
                    detail.PdProcID = orderInfoList[j]["PdProcID"];
                    // TODO:调用仓库编码的共通函数
                    // 8.仓库编码 （待修改）
                    detail.WarehouseID = "1";
                    // 9.单价：画面上的单价
                    detail.UnitPrice = Convert.ToDecimal(orderInfoList[j]["UnitPrice"]);
                    // 10.估价：画面上的估价
                    detail.Evaluate = Convert.ToDecimal(orderInfoList[j]["Evaluate"]);
                    // 11.单据要求数量：画面上的采购数量
                    detail.RequestQuantity = Convert.ToDecimal(orderInfoList[j]["RequestQuantity"]);
                    // 12.实际入库数量
                    detail.ActualQuantity = 0;
                    // 13.其他数量
                    detail.OtherQuantity = 0;
                    // 14.完成状态：0（未完成）
                    detail.CompleteStatus = Constant.PurchaseCompleteStatus.UNCOMPLETED;
                    // 15.交货日期：画面上的交货日期
                    detail.DeliveryDate = Convert.ToDateTime(orderInfoList[j]["DeliveryDate"]);
                    // 16.创建人
                    detail.CreUsrID = userID;
                    // 17.创建日期
                    detail.CreDt = date;
                    // 18.修改人
                    detail.UpdUsrID = userID;
                    // 19.修改日期
                    detail.UpdDt = date;

                    detailList.Add(detail);
                }
            }
            //数据封装进addObject
            result.SupDetailsList = detailList;
            #endregion

            // 返回封装结果
            return result;

        }
    }
}
