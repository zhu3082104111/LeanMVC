/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseOrderServiceImp.cs
// 文件功能描述：
//          外购产品计划的Service接口类的实现类
//      
// 修改履历：2013/11/1 刘云 新建
/*****************************************************************************/
using Model;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ServerMessage;
using Extensions;
using BLL_InsideInterface;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// 外购产品计划的Service接口类的实现类
    /// </summary>
    public class PurchaseOrderServiceImp : AbstractService, IPurchaseOrderService
    {
        // 产品外购计划单表的Repository类
        private IMCOutSourceOrderRepository outSourceOrderRepository;

        // 产品外购单详细表的Repository类
        private IMCOutSourceOrderDetailRepository outSourceOrderDetailRepository;
        
        // 采购部门的内部共通的Service接口的实现类
        private IPurchase4InernalService purchase4InernalService;

        // 生产部门的外部共通的Service接口的实现类
        private IProduceForExternalService produce4ExternalService;

        /// <summary>
        /// 外购产品计划的Service接口类的实现类的构造函数
        /// </summary>
        /// <param name="outSourceOrderRepository">产品外购计划单表的Repository</param>
        /// <param name="outSourceOrderDetailRepository">产品外购单详细表的Repository</param>
        /// <param name="purchase4InernalService">采购部门的内部共通的Service</param>
        /// <param name="produce4ExternalService">产部门的外部共通的Service</param>
        public PurchaseOrderServiceImp(IMCOutSourceOrderRepository outSourceOrderRepository, IMCOutSourceOrderDetailRepository outSourceOrderDetailRepository, 
            IPurchase4InernalService purchase4InernalService, IProduceForExternalService produce4ExternalService)
        {
            this.outSourceOrderRepository = outSourceOrderRepository;
            this.outSourceOrderDetailRepository = outSourceOrderDetailRepository;
            this.purchase4InernalService = purchase4InernalService;
            this.produce4ExternalService = produce4ExternalService;
        }

        #region 取得产品外购单一览画面的信息
        /// <summary>
        /// 取得产品外购单一览画面的信息
        /// </summary>
        /// <param name="purchaseOrderList">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        public IEnumerable GetPurchaseOrderListInfoByPage(VM_PurchaseOrderListForSearch searchConditon, Extensions.Paging paging)
        {
            // 返回Repository的处理结果
            return outSourceOrderDetailRepository.GetPurchaseOrderListInfoByPage(searchConditon, paging);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pOrderList">外购单号List</param>
        /// <param name="delUID">删除人UserID</param>
        /// <returns>删除处理结果</returns>
        public bool Delete(List<String> pOrderList, string delUID)
        {
            // 返回结果
            bool ret = true;

            // 错误list
            string errorList = "";

            // 遍历外购单号List
            for (int i = 0; i < pOrderList.Count; i++)
            {
                // 取得当前外购单的外购单状态
                string orderStaus = outSourceOrderRepository.GetMCOutSourceOrderById(pOrderList[i]).OutOrderStatus;
                // 外购单状态=3（已签收）时，报错
                if (orderStaus.Equals(Constant.OutOrderStatus.STATUSS))
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00024, new string[] { pOrderList[i] });
                }

                // 系统当前时间
                DateTime sysDate = DateTime.Now;

                // 删除MCOutSourceOrder表中的数据(伦理删除)
                ret = outSourceOrderRepository.Delete(pOrderList[i], delUID, sysDate);
                if (!ret)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }

                // 抽取外购详细表数据
                List<MCOutSourceOrderDetail> detailList = outSourceOrderDetailRepository.GetListByCondition(n => n.OutOrderID.Equals(pOrderList[i])).ToList();
                
                // 遍历外购详细表数据
                foreach (MCOutSourceOrderDetail detail in detailList)
                {
                    // 删除MCOutSourceOrderDetail表中的数据(伦理删除)
                    ret = outSourceOrderDetailRepository.Delete(detail, delUID, sysDate);
                    if (!ret)
                    {
                        throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                    }

                    // 更新外购指示表
                    ret = produce4ExternalService.UpdPurchInstruc4Del(detail.CustomerOrderID, detail.CustomerOrderDetailID, detail.ProductPartID,
                        detail.RequestQuantity, delUID, sysDate);
                    if (!ret)
                    {
                        throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                    }
                }
            }

            // 如果存在错误，则抛出错误
            if (!String.IsNullOrEmpty(errorList))
            {
                throw new Exception(errorList);
            }

            // 返回处理结果
            return ret;
        }
        #endregion

        #region 批准
        /// <summary>
        /// 批准
        /// </summary>
        /// <param name="pOrderList">外购单号的List</param>
        /// <param name="approveUID">批准人的UserID</param>
        /// <returns>批准处理结果</returns>
        public bool Approve(List<String> pOrderList, string approveUID)
        {
            // 返回结果
            bool result = true;
            // 错误Message
            string errMsg = "";

            // 遍历外购单号List
            for (int i = 0; i < pOrderList.Count; i++)
            {
                // 取得当前外购单的外购单状态
                string orderStaus = outSourceOrderRepository.GetMCOutSourceOrderById(pOrderList[i]).OutOrderStatus;
                // 外购单状态 = 3（已签收）或2（已批准）时，报错
                if ((orderStaus.Equals(Constant.OutOrderStatus.STATUSS))
                    || (orderStaus.Equals(Constant.OutOrderStatus.STATUST)))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00022, new string[] { pOrderList[i] });
                    continue;
                }

                // 调用repository的批准处理
                result = outSourceOrderRepository.Approve(pOrderList[i], approveUID, DateTime.Now);
                if (result == false)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }

            // 如果存在错误，则抛出错误
            if (!String.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            // 返回处理结果
            return result;
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="pOrderList">外购单号List</param>
        /// <param name="verifyUID">审核人UserID</param>
        /// <returns>审核处理结果</returns>
        public bool Audit(List<String> pOrderList, string verifyUID)
        {
            // 返回结果
            bool result = true;
            // 错误Message
            string errMsg = "";
            
            // 遍历外购单号List
            for (int i = 0; i < pOrderList.Count; i++)
            {
                // 获取当前外购单的外购单状态
                string orderStaus = outSourceOrderRepository.GetMCOutSourceOrderById(pOrderList[i]).OutOrderStatus;
                // 外购单状态 = 3(已签收) 或 2(已批准) 或 1(已审核)时处理中断，提示错误Message
                if ((orderStaus.Equals(Constant.OutOrderStatus.STATUSS))
                    || (orderStaus.Equals(Constant.OutOrderStatus.STATUST))
                    || (orderStaus.Equals(Constant.OutOrderStatus.STATUSO)))
                {
                    errMsg += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00023, new string[] { pOrderList[i] });
                    continue;
                }

                // 调用repository的审核处理
                result = outSourceOrderRepository.Audit(pOrderList[i], verifyUID, DateTime.Now);
                if (result == false)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }

            // 如果存在错误，则抛出错误
            if (!String.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }

            // 返回处理结果
            return result;
        }
        #endregion

        #region 根据外购单号取得外购单详细信息
        /// <summary>
        /// 根据外购单号取得外购单详细信息
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>外购单详细信息</returns>
        public IEnumerable GetPurchaseOrderDetailInfoByID(string outOrderID)
        {
            return outSourceOrderRepository.GetPurchaseOrderInfoByID(outOrderID);
        }
        #endregion

        #region 根据外购单号取得外购单信息
        /// <summary>
        /// 根据外购单号取得外购单信息
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>外购单信息</returns>
        public MCOutSourceOrder GetOrderInfoByID(string outOrderID)
        {
            MCOutSourceOrder order = outSourceOrderRepository.getOrderById(outOrderID);
            return order;
        }
        #endregion

        #region 新建外购单
        /// <summary>
        /// 新建外购单
        /// </summary>
        /// <param name="purchaseOrder">外购单基础信息</param>
        /// <param name="orderList">外购单详细信息</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>处理结果</returns>
        public bool Add(VM_PurchaseOrderForShow purchaseOrder, Dictionary<string, string>[] orderList,string uId)
        {
            // 系统当前时间
            DateTime sysDate = DateTime.Now;

            try
            {
                // 对外购单基础信息的check
                checkInput4BaseInfo(purchaseOrder, true);

                // 对外购单详细信息的check
                checkInput4DetailInfo(orderList, true);

                // 封装外购单表的实体
                MCOutSourceOrder order = packageOutOrderInfo(purchaseOrder, uId, sysDate);

                // 封装外购单详细表的实体
                List <MCOutSourceOrderDetail>details = packageOutOrderDetailInfo(purchaseOrder.OutOrderID, orderList, uId, sysDate);

                List<OutSourceOrderInfo> list = new List<OutSourceOrderInfo>();
                OutSourceOrderInfo addObj = new OutSourceOrderInfo();
                addObj.OutOrderInfo = order;
                addObj.OutDetailsList = details;
                list.Add(addObj);

                // 调用采购部门内部接口，添加新的外购单
                bool result = purchase4InernalService.AddOutSourceOrder(list);
                if (result)
                {
                    return result;
                }
                else
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        /// <summary>
        /// 对更新操作进行检查并更新
        /// </summary>
        /// <param name="purchaseOrder">筛选条件</param>
        /// <param name="orderList">筛选条件</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        public bool Update(VM_PurchaseOrderForShow purchaseOrder, Dictionary<string, string>[] orderList,string uId)
        {
            //错误list
            string errorList = "";
            string sUID = "";
            #region 对detail表进行更新
            for (int i = 0; i < orderList.Length; i++)
            {
                #region 对单价、交货日期、版本号、备注进行验证
                //单价判断
                if (String.IsNullOrEmpty(orderList[i]["UnitPrice"]))
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { Convert.ToString(i) });
                }
                else
                {
                    //验证有 1-2 位小数的正实数
                    Match m = Regex.Match(orderList[i]["UnitPrice"], "^[1-9]+(.[0-9]{1,2})?$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { Convert.ToString(i) });
                    }
                    if (orderList[i]["UnitPrice"].Length > 10)
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00008, new string[] { Convert.ToString(i) });
                    }
                }

                //估价输入不正确未做
                if (String.IsNullOrEmpty(orderList[i]["Evaluate"]))
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00040, new string[] { Convert.ToString(i) });
                }
                else
                {
                    //验证有 1-2 位小数的正实数
                    Match m = Regex.Match(orderList[i]["Evaluate"], "^[1-9]+(.[0-9]{1,2})?$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00040, new string[] { Convert.ToString(i) });
                    }
                    if (orderList[i]["Evaluate"].Length > 10)
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00039, new string[] { Convert.ToString(i) });
                    }
                }

                if (String.IsNullOrEmpty(orderList[i]["DeliveryDate"]))
                {
                    //第【N】行的交货日期未输入，请您输入正确的交货日期
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { Convert.ToString(i) });
                }
                if (orderList[i]["PlanNo"].Length > 20)
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00041, new string[] { Convert.ToString(i) });
                }
                if (!String.IsNullOrEmpty(orderList[i]["VersionNum"]))//版本号
                {
                    if (orderList[i]["VersionNum"].Length > 40)
                    {
                        // 第【N】行输入的版本号字节数超过？？，请重新输入
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00012, new string[] { Convert.ToString(i) });
                    }
                }
                if (!String.IsNullOrEmpty(orderList[i]["Remarks"]))//备注
                {
                    if (orderList[i]["Remarks"].Length > 512)
                    {
                        //第【N】行输入的备注的字节数超过？？，请重新输入
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00013, new string[] { Convert.ToString(i) });
                    }
                }
                if (errorList != "")
                {
                    throw new Exception(errorList);
                }
                #endregion

                var custOrder = orderList[i]["CustomerOrder"].Split(',');
                var custOrderDetail = orderList[i]["CustomerOrderDetail"].Split(',');
                for (int j = 0; j < custOrder.Length; j++)
                {   //更新单价、交货日期、版本号、备注
                    MCOutSourceOrderDetail detail = new MCOutSourceOrderDetail();
                    detail.OutOrderID = purchaseOrder.OutOrderID;
                    detail.CustomerOrderID = custOrder[j];
                    detail.CustomerOrderDetailID = custOrderDetail[j];
                    detail.ProductPartID = orderList[i]["ProductPartID"];
                    detail.UnitPrice = Convert.ToDecimal(orderList[i]["UnitPrice"]);
                    detail.Evaluate = Convert.ToDecimal(orderList[i]["Evaluate"]);
                    detail.DeliveryDate = Convert.ToDateTime(orderList[i]["DeliveryDate"]);
                    detail.PlanNo = orderList[i]["PlanNo"];
                    detail.VersionNum = orderList[i]["VersionNum"];
                    detail.Remarks = orderList[i]["Remarks"];
                    bool re = outSourceOrderDetailRepository.UpdateDetail(detail, uId);
                    if (re == false)
                    {
                        throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                    }
                }
            }
            #endregion


            #region 对order表进行条件判断
            if (purchaseOrder.Remarks2 != null)//注有值,没有值则直接判断批准人
            {
                if (purchaseOrder.Remarks2.Length > 512)//注的长度大于512
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00014); //您输入的注释超过了512个字节，请您重新输入
                }
            }
            if (purchaseOrder.ApproveUID != null)//批准人有值,没有值则直接判断审核人
            {
                if (purchaseOrder.ApproveUID.Length > 10)//批准人的长度大于10
                {
                    //您输入的批准人超过了10个字节，请您重新输入
                    errorList += SM_Purchase.SMSG_PUR_E00017;
                }
            }
            if (purchaseOrder.VerifyUID != null)//审核人有值,没有值则直接判断编制人
            {
                if (purchaseOrder.VerifyUID.Length > 10)//审核人的长度大于10
                {
                    //您输入的审核人超过了10个字节，请您重新输入
                    errorList += SM_Purchase.SMSG_PUR_E00018;
                }
            }
            if (purchaseOrder.EstablishUID != null)//编制人有值,没有值则直接判断签收人
            {
                if (purchaseOrder.EstablishUID.Length > 10)//编制人的长度大于10
                {
                    //您输入的编制人超过了10个字节，请您重新输入
                    errorList += SM_Purchase.SMSG_PUR_E00019;
                }
            }
            if (!String.IsNullOrEmpty(purchaseOrder.SignUID))// 签收人有值
            {
                string orderStatus = outSourceOrderRepository.GetMCOutSourceOrderById(purchaseOrder.OutOrderID).OutOrderStatus;
                if ((orderStatus == Constant.OutOrderStatus.STATUSZ) || (orderStatus == Constant.OutOrderStatus.STATUSO))
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00047);
                }
                if (purchaseOrder.SignUID.Length > 10)//签收人的长度大于10
                {
                    //您输入的签收人超过了10个字节，请您重新输入
                    errorList += SM_Purchase.SMSG_PUR_E00020;
                }
                sUID = purchaseOrder.SignUID;
            }
            if (errorList != "")
            {
                throw new Exception(errorList);
            }
            #endregion
            //更新注、FAX
            MCOutSourceOrder order = new MCOutSourceOrder();
            order.OutOrderID = purchaseOrder.OutOrderID;
            order.Remarks = purchaseOrder.Remarks2;
            order.FaxNo = purchaseOrder.FaxNo;
            order.SignUID = sUID;
            bool sult = outSourceOrderRepository.UpdateOrder(order,uId);
            if (sult)
            {
                return sult;
            }
            else
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00034);
            }

        }

        /// <summary>
        /// 打印（更新外购单表的打印flg）
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>处理结果</returns>
        public bool PrintInfo(string outOrderID, string uId)
        {
            // 返回结果
            bool result = true;

            // 获取当前外购单的外购单状态
            string orderStaus = outSourceOrderRepository.GetMCOutSourceOrderById(outOrderID).OutOrderStatus;
            // 外购单状态≠3（已签收）时，不可打印
            if (orderStaus != Constant.OutOrderStatus.STATUSS)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00051);
            }

            // 调用Repository的打印函数
            result = outSourceOrderRepository.Print(outOrderID, uId);
            if (result != true)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00034);
            }
            return result;
        }

        /// <summary>
        /// 对外购单基本信息的check
        /// </summary>
        /// <param name="purchaseOrder">外购单基本信息</param>
        /// <param name="model">true：新建模式   false：编辑模式</param>
        /// <returns></returns>
        private void checkInput4BaseInfo(VM_PurchaseOrderForShow purchaseOrder, bool model)
        {
            // 错误list
            string errorList = "";

            // 新建模式的check
            if (model)
            {
                // 画面的外购单位未输入时，报错
                if (String.IsNullOrEmpty(purchaseOrder.OutCompanyID))
                {
                    errorList += SM_Purchase.SMSG_PUR_E00002;
                }

                // 画面的所属部门未输入时，报错
                if (String.IsNullOrEmpty(purchaseOrder.DepartmentID))
                {
                    errorList += SM_Purchase.SMSG_PUR_E00038;
                }

                // 画面的紧急状态未输入时，报错
                if (String.IsNullOrEmpty(purchaseOrder.UrgentStatus))
                {
                    errorList += SM_Purchase.SMSG_PUR_E00038;
                }

                // 签收人的输入时，报错
                if (!String.IsNullOrEmpty(purchaseOrder.SignUID))
                {
                    errorList += SM_Purchase.SMSG_PUR_E00038;
                }
            }
            // 编辑模式的check
            else
            {
                // 当前
                int orderStausCd = getOrderStatusCd(purchaseOrder.OutOrderID);
                
                // 外购单状态未批准时，签收人输入时，报错
                if (orderStausCd < 2 && (!String.IsNullOrEmpty(purchaseOrder.SignUID)))
                {
                    errorList += SM_Purchase.SMSG_PUR_E00038;
                }
                else
                {
                    // 签收人超过10个字节时，报错
                    if (StringUtil.getByteCnt(purchaseOrder.SignUID) > 10)
                    {
                        errorList += SM_Purchase.SMSG_PUR_E00020;
                    }
                }
            }

            // 新建模式和编辑模式的check
            // 备注
            if (!String.IsNullOrEmpty(purchaseOrder.Remarks2))
            {
                // 备注超过512个字节时，报错
                if(StringUtil.getByteCnt(purchaseOrder.Remarks2) > 512)
                {
                    errorList += SM_Purchase.SMSG_PUR_E00014;
                }
            }
            // Fax
            if (!String.IsNullOrEmpty(purchaseOrder.FaxNo))
            {
                // Fax超过20个字节时
                if (StringUtil.getByteCnt(purchaseOrder.FaxNo) > 20)
                {
                    errorList += SM_Purchase.SMSG_PUR_E00016;
                }
            }
            
            // 如果存在check错误，返回错误Message
            if (errorList != "")
            {
                throw new Exception(errorList);
            }

            return;
        }

        /// <summary>
        /// 对外购单详细信息的check
        /// </summary>
        /// <param name="detailInfoList">详细信息List</param>
        /// <param name="model">>true：新建模式   false：编辑模式</param>
        private void checkInput4DetailInfo(Dictionary<string, string>[] detailInfoList, bool model)
        {
            // 错误list
            string errorList = "";
            
            // 详细信息List不为空时，遍历详细信息List
            if(detailInfoList != null)
            {
                for (int i = 0; i < detailInfoList.Length; i++){
                    // 新建模式时
                    if (model)
                    {
                        // 物料编号为空时，报错
                        if(String.IsNullOrEmpty(detailInfoList[i]["ProductPartID"]))
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00005, new string[] { Convert.ToString( i + 1) });
                        }

                        // 数量为空时，报错
                        if (String.IsNullOrEmpty(detailInfoList[i]["RequestQuantity"]))
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00006, new string[] { Convert.ToString( i + 1) });
                        }
                        // 数量不为空时，
                        else
                        {
                            // 验证数量的有效性（数字）
                            Match m = Regex.Match(detailInfoList[i]["RequestQuantity"], "^[1-9][0-9]*$");
                            // 无效数值时，报错
                            if (m.Length == 0)
                            {
                                errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00006, new string[] { Convert.ToString( i + 1) });
                            }
                            // 数量超过10个字节时，报错
                            if (StringUtil.getByteCnt(detailInfoList[i]["RequestQuantity"]) > 10)
                            {
                                errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00042, new string[] { Convert.ToString( i + 1) });
                            }
                        }

                        // 材料规格及要求超过200字节时，报错
                        if (!String.IsNullOrEmpty(detailInfoList[i]["MaterialsSpecReq"]))
                        {
                            if (StringUtil.getByteCnt(detailInfoList[i]["MaterialsSpecReq"]) > 200)
                            {   
                                errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00006, new string[] { Convert.ToString(i + 1) });
                            }
                        }
                    }

                    // 新建模式和编辑模式的check
                    // 单价为空时，报错
                    if (String.IsNullOrEmpty(detailInfoList[i]["UnitPrice"]))
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { Convert.ToString(i + 1) });
                    }
                    else
                    {
                        // 验证单价有效性（1-2 位小数的正实数）
                        Match m = Regex.Match(detailInfoList[i]["UnitPrice"], "^[1-9]+(.[0-9]{1,2})?$");
                        // 无效时，报错
                        if (m.Length == 0)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { Convert.ToString(i + 1) });
                        }
                        // 单价超过10个字节时，报错
                        if (StringUtil.getByteCnt(detailInfoList[i]["UnitPrice"]) > 10)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00008, new string[] { Convert.ToString(i + 1) });
                        }
                    }

                    // 估价为空时，报错
                    if (String.IsNullOrEmpty(detailInfoList[i]["Evaluate"]))
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00040, new string[] { Convert.ToString(i + 1) });
                    }
                    else
                    {
                        // 验证估价有效性（1-2 位小数的正实数）
                        Match m = Regex.Match(detailInfoList[i]["Evaluate"], "^[1-9]+(.[0-9]{1,2})?$");
                        // 无效时，报错
                        if (m.Length == 0)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00040, new string[] { Convert.ToString(i + 1) });
                        }
                        // 估价超过10个字节时，报错
                        if (StringUtil.getByteCnt(detailInfoList[i]["Evaluate"]) > 10)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00039, new string[] { Convert.ToString(i + 1) });
                        }
                    }

                    // 交货日期为空时，报错
                    if (String.IsNullOrEmpty(detailInfoList[i]["DeliveryDate"]))
                    {
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { Convert.ToString(i + 1) });
                    }

                    // 计划单号超过20字节时，报错
                    if (!String.IsNullOrEmpty(detailInfoList[i]["PlanNo"]))
                    {
                        if(StringUtil.getByteCnt(detailInfoList[i]["PlanNo"]) > 20)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00041, new string[] { Convert.ToString(i + 1) });
                        }
                    }

                    // 版本号超过40字节时，报错
                    if (!String.IsNullOrEmpty(detailInfoList[i]["VersionNum"]))//版本号
                    {
                        if (StringUtil.getByteCnt(detailInfoList[i]["VersionNum"]) > 40)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00012, new string[] { Convert.ToString(i + 1) });
                        }
                    }

                    // 备注栏超过512字节时，报错
                    if (!String.IsNullOrEmpty(detailInfoList[i]["Remarks"]))//备注
                    {
                        if (StringUtil.getByteCnt(detailInfoList[i]["Remarks"]) > 512)
                        {
                            errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00013, new string[] { Convert.ToString(i + 1) });
                        }
                    }
                }
            }

            // 如果存在check错误，返回错误Message
            if (errorList != "")
            {
                throw new Exception(errorList);
            }

            return;
        }

        #region 封装外购单表的实体类
        /// <summary>
        /// 封装外购单表的实体类
        /// </summary>
        /// <param name="purchaseOrder">外购单基本信息</param>
        /// <param name="uId">用户ID</param>
        /// <param name="sysDate">系统时间</param>
        /// <returns></returns>
        private MCOutSourceOrder packageOutOrderInfo(VM_PurchaseOrderForShow purchaseOrder, string uId, DateTime sysDate)
        {
            MCOutSourceOrder order = new MCOutSourceOrder();
            // 外购单号
            order.OutOrderID = purchaseOrder.OutOrderID;
            // 外购单紧急状态（画面.紧急状态）
            order.UrgentStatus = purchaseOrder.UrgentStatus;
            // 外购部门ID（画面.生产部门）
            order.DepartmentID = purchaseOrder.DepartmentID;
            // 外购单位ID（画面.承接商）
            order.OutCompanyID = purchaseOrder.OutCompanyID;
            // 外购单状态(0:未处理)
            order.OutOrderStatus = Constant.OutOrderStatus.STATUSZ;
            // 编制人（登录用户ID）
            order.EstablishUID = uId;
            // 编制日期（当前时间）
            order.EstablishDate = sysDate;
            // 外购单区分（1：仓库）
            order.OutOrderFlg = Constant.OutOrderType.STORE;
            // 备注（画面.注）
            order.Remarks = purchaseOrder.Remarks2;
            // FAX（画面.Fax）
            order.FaxNo = purchaseOrder.FaxNo;
            // 删除Flg
            order.DelFlag = Constant.GLOBAL_DELFLAG_ON;
            // 打印区分
            order.PrintFlag = Constant.GLOBAL_PRINTLAG_OFF;
            // 有效Flg
            order.EffeFlag = Constant.GLOBAL_EffEFLAG_ON;
            // 创建人
            order.CreUsrID = uId;
            // 创建时间
            order.CreDt = sysDate;
            // 修改人
            order.UpdUsrID = uId;
            // 修改时间
            order.UpdDt = sysDate;

            return order;
        }
        #endregion

        /// <summary>
        /// 封装外购单详细表的实体类
        /// </summary>
        /// <param name="outOrderNo">外购单号</param>
        /// <param name="detailInfoList">详细信息List</param>
        /// <param name="uId">用户ID</param>
        /// <param name="sysDate">系统时间</param>
        /// <returns></returns>
        private List<MCOutSourceOrderDetail> packageOutOrderDetailInfo(string outOrderNo, Dictionary<string, string>[] detailInfoList, string uId, DateTime sysDate)
        {
            // 返回结果
            List<MCOutSourceOrderDetail> detailList = new List<MCOutSourceOrderDetail>();
            
            // 遍历详细信息List
            for (int i = 0; (detailInfoList != null) && (i < detailInfoList.Length); i++)
            {
                MCOutSourceOrderDetail detail = new MCOutSourceOrderDetail();
                // 外购单号
                detail.OutOrderID = outOrderNo;
                // 客户订单号
                detail.CustomerOrderID = "0000000000";
                // 客户订单详细号
                detail.CustomerOrderDetailID = "00";
                // 产品零件ID
                detail.ProductPartID = detailInfoList[i]["ProductPartID"];
                // 产品ID
                // 材料和规格要求
                detail.MaterialsSpecReq = detailInfoList[i]["MaterialsSpecReq"];
                // 仓库编码
                detail.WarehouseID = "0";//仓库编码 必须字段
                // 单价
                detail.UnitPrice = Convert.ToDecimal(detailInfoList[i]["UnitPrice"]);
                // 估价
                detail.Evaluate = Convert.ToDecimal(detailInfoList[i]["Evaluate"]);
                // 单据要求数量
                detail.RequestQuantity = Convert.ToDecimal(detailInfoList[i]["RequestQuantity"]);
                // 实际入库数量
                detail.ActualQuantity = 0;
                // 其他数量
                detail.OtherQuantity = 0;
                // 完成状态
                detail.CompleteStatus = "0";//完成状态 必须字段
                // 交货日期
                detail.DeliveryDate = Convert.ToDateTime(detailInfoList[i]["DeliveryDate"]);
                // 计划单号
                detail.PlanNo = detailInfoList[i]["PlanNo"];
                // 版本号
                detail.VersionNum = detailInfoList[i]["VersionNum"];
                // 备注
                detail.Remarks = detailInfoList[i]["Remarks"];
                // 删除Flg
                detail.DelFlag = Constant.GLOBAL_DELFLAG_ON;
                // 有效Flg
                detail.EffeFlag = Constant.GLOBAL_EffEFLAG_ON;
                // 创建人
                detail.CreUsrID = uId;
                // 创建时间
                detail.CreDt = sysDate;
                // 修改人
                detail.UpdUsrID = uId;
                // 修改时间
                detail.UpdDt = sysDate;

                // 将外购详细实体类添加到返回List
                detailList.Add(detail);
            }

            return detailList;
        }

        /// <summary>
        /// 获取指定外购单的外购单状态
        /// </summary>
        /// <param name="outOrderID"></param>
        /// <returns></returns>
        private int getOrderStatusCd(string outOrderID)
        {
            string status = outSourceOrderRepository.GetMCOutSourceOrderById(outOrderID).OutOrderStatus;
            return Int32.Parse(status);
        }
    }
}
