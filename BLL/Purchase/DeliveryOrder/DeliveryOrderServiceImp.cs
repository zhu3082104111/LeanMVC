/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：DeliveryOrderServiceImp.cs
// 文件功能描述：送货单画面的Service实现
//      
// 修改履历：2013/12/10 刘云 新建
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
using BLL_InsideInterface;
using Extensions;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// 送货单画面的Service实现
    /// </summary>
    public class DeliveryOrderServiceImp : AbstractService, IDeliveryOrderService
    {
        //送货单的Repository类
        private IDeliveryOrderDetailRepository deliveryOrderRepository;

        //送货单详细的Repository类
        private IDeliveryOrderRepository deliveryOrderListRepository;

        //采购部门的内部共通的Service接口的实现类
        private IPurchase4InernalService purchase4InernalService;

        //外购单的Repository类
        private IMCOutSourceOrderRepository outSourceOrderRepository;

        //外协加工调度单的Repository类
        private ISupplierOrderRepository supplierOrderRepository;

        /// <summary>
        /// 送货单画面的构造函数
        /// </summary>
        /// <param name="deliveryOrderRepository"></param>
        /// <param name="deliveryOrderListRepository"></param>
        public DeliveryOrderServiceImp(IDeliveryOrderDetailRepository deliveryOrderRepository, IDeliveryOrderRepository deliveryOrderListRepository,
            IPurchase4InernalService purchase4InernalService, IMCOutSourceOrderRepository outSourceOrderRepository, ISupplierOrderRepository supplierOrderRepository)
        {
            this.deliveryOrderRepository = deliveryOrderRepository;
            this.deliveryOrderListRepository = deliveryOrderListRepository;
            this.purchase4InernalService = purchase4InernalService;
            this.outSourceOrderRepository = outSourceOrderRepository;
            this.supplierOrderRepository = supplierOrderRepository;
        }

        /// <summary>
        /// 送货单一览画面的显示数据的取得函数
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页情报</param>
        /// <returns>送货单情报List</returns>
        public IEnumerable GetDeliveryOrderListBySearchByPage(VM_DeliveryOrderListForSearch searchCondition, Paging paging)
        {
            return deliveryOrderListRepository.GetDeliveryOrderListBySearchByPage(searchCondition, paging);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">登录用户UserID</param>
        /// <returns>删除结果</returns>
        public bool Delete(string deliveryOrderID, string uId)
        {
            // 封装送货单号
            List<string> deliveryOrderList = new List<string>();
            deliveryOrderList.Add(deliveryOrderID);

            // 进行删除操作
            return Delete(deliveryOrderList, uId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryOrderList">外购单号List</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>删除结果</returns>
        public bool Delete(List<String> deliveryOrderList, string uId)
        {
            // 取得系统时间
            DateTime systime = System.DateTime.Now;            
            // 循环遍历送货单号List
            for (int i = 0; i < deliveryOrderList.Count; i++)
            {
                // 没有选择送货单时，报错
                if (String.IsNullOrEmpty(deliveryOrderList[i]))
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00021);
                }

                // 根据送货单号取得送货单实体
                MCDeliveryOrder deliveryOrder = deliveryOrderListRepository.GetEntityById(deliveryOrderList[i]);
                // 送货单实体不存在时，报错
                if (deliveryOrder == null)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00043);
                }

                // 如果该送货单已打印的状况下，报错
                if (Constant.GLOBAL_PRINTLAG_ON.Equals(deliveryOrder.PrintFlag))
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00045, new string[] { deliveryOrderList[i] }));
                }

                // 更新送货单表的信息
                bool re = deliveryOrderListRepository.Delete(deliveryOrder, uId, systime);
                
                // 更新送货单详细表的信息
                bool sult = deliveryOrderRepository.Delete(deliveryOrderList[i], uId, systime);
                
                // 判断处理结果
                if ((re & sult) != true)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }
            // 返回操作成功信息
            return true;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>审核结果</returns>
        public bool Audit(string deliveryOrderID, string uId)
        {
            List<string> deliveryOrderList = new List<string>();
            deliveryOrderList.Add(deliveryOrderID);
            return Audit(deliveryOrderList, uId);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="deliveryOrderList">送货单号List</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>审核结果</returns>
        public bool Audit(List<String> deliveryOrderList, string uId)
        {
            // 取得系统时间
            DateTime systime = System.DateTime.Now;

            for (int i = 0; i < deliveryOrderList.Count; i++)
            {
                if (String.IsNullOrEmpty(deliveryOrderList[i]))
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00023);
                }

                // 根据送货单号去的送货单实体
                MCDeliveryOrder deliveryOrder = deliveryOrderListRepository.GetEntityById(deliveryOrderList[i]);
                // 送货单实体不存在时，报错
                if (deliveryOrder == null)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00043);
                }

                // 该送单的送货状态
                int deliveryOrderStatus = Int32.Parse(deliveryOrder.DeliveryOrderStatus);
                // 已审核状态的Code
                int auditedStatus = Int32.Parse(Constant.DeliveryOrderStatus.AUDITED);
                // 若该送货单的送货单状态已审核时，报错
                if(deliveryOrderStatus >= auditedStatus)
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00044, new string[] { deliveryOrderList[i] }));
                }

                // 审核操作的数据库更新
                bool result = deliveryOrderListRepository.Audit(deliveryOrder, uId, systime);
                if (result == false)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }
            return true;
        }

        /// <summary>
        /// 获取送货单详细信息
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <returns>送货单详细情报List</returns>
        public IEnumerable GetDeliveryOrderDetailBySearchById(VM_DeliveryOrderForShow searchConditon)
        {
            return deliveryOrderRepository.GetDeliveryOrderDetailBySearchById(searchConditon);
        }

        /// <summary>
        /// 导入显示的数据
        /// </summary>
        /// <param name="orderNo">外购（外协）计划单号</param>
        /// <returns>外购（外协）信息</returns>
        public List<VM_DeliveryOrderForTableShow> GetImportInfo(string orderNo)
        {
            if (String.IsNullOrEmpty(orderNo))
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00037);
            }
            orderNo = orderNo.Trim();
            if (orderNo.Length != 13)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00035);
            }
            //得到第7、8位，判断是外购还是外协
            string check = orderNo.Substring(6, 2);
            if (check == "WG")
            {
                if (outSourceOrderRepository.GetMCOutSourceOrderById(orderNo) == null)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00036);
                }
            }
            else if (check == "WX")
            {
                if (supplierOrderRepository.GetMCSupplierOrderById(orderNo) == null)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00036);
                }
            }
            else
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00035);
            }
            var info = deliveryOrderRepository.GetImportInfo(orderNo);
            string str;
            List<VM_DeliveryOrderForTableShow> list = new List<VM_DeliveryOrderForTableShow>();
            foreach (var a in info)
            {
                VM_DeliveryOrderForTableShow detail = new VM_DeliveryOrderForTableShow();
                List<String> strInfo = new List<String>();
                str = a.ToString();
                var st = str.Split(',');
                for (int i = 0; i < st.Length; i++)
                {
                    var s = st[i].Split('=');
                    strInfo.Add(s[1].Trim());
                }
                detail.DeliveryCompanyID = strInfo[0];
                detail.MaterielID = strInfo[1];
                detail.Materiel = strInfo[2];
                detail.MaterielName = strInfo[3];
                detail.MaterialsSpec = strInfo[4];
                detail.WarehouseID = strInfo[5];
                detail.Unit = strInfo[6];
                detail.Quantity = strInfo[7];
                detail.UnitID = strInfo[8];
                list.Add(detail);
            }
            return list;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <param name="orderList"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string Add(VM_DeliveryOrderForShow deliveryOrder, Dictionary<string, string>[] orderList,string uID)
        {
            //判断采购计划单号
            if (deliveryOrder.OrderNo != deliveryOrder.OrderNumber)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00065);
            }
            DateTime t = System.DateTime.Now;
            List<MCDeliveryOrderDetail> details = new List<MCDeliveryOrderDetail>();
            for (int i = 0; i < orderList.Length; i++)
            {
                MCDeliveryOrderDetail detail = new MCDeliveryOrderDetail();
                detail.DeliveryOrderID = deliveryOrder.DeliveryOrderID;
                detail.MaterielID = orderList[i]["Materiel"];
                detail.MaterialsSpec = orderList[i]["MaterialsSpec"];
                detail.WarehouseID = orderList[i]["WarehouseID"];
                detail.Unit = orderList[i]["UnitID"];
                detail.Quantity = Convert.ToDecimal(orderList[i]["Quantity"]);
                detail.InnumQuantity = Convert.ToDecimal(orderList[i]["InnumQuantity"]);
                detail.Num = Convert.ToDecimal(orderList[i]["Num"]);
                detail.Scrap = Convert.ToDecimal(orderList[i]["Scrap"]);
                detail.ActualQuantity = Convert.ToDecimal(orderList[i]["ActualQuantity"]);
                detail.Remarks = orderList[i]["Remarks"];
                detail.Unit = "1";//Required字段
                detail.PriceWithTax =Convert.ToDecimal( null);
                detail.CkPriceWithTax = Convert.ToDecimal(null);
                detail.DelFlag = "0";
                detail.EffeFlag = "0";//有效FLG
                detail.CreUsrID = uID;
                detail.CreDt = t;
                detail.UpdUsrID = uID;
                detail.UpdDt = t;
                detail.DelDt = null;
                detail.DelUsrID = null;
                details.Add(detail);
            }
            MCDeliveryOrder order = new MCDeliveryOrder();
            order.DeliveryOrderID = deliveryOrder.DeliveryOrderID;
            order.OrderNo = deliveryOrder.OrderNo;
            //得到第7、8位，判断是外购还是外协
            string check = order.OrderNo.Substring(6, 2);
            if (check == "WG")
            {
                //送货单区分  外购
                order.DeliveryOrderType = "0";
                //生产单元ID  送货单所对应的订单号的部门ID
                order.DepartID = outSourceOrderRepository.GetMCOutSourceOrderById(order.OrderNo).DepartmentID;
                //送货单位
                order.DeliveryCompanyID = outSourceOrderRepository.GetMCOutSourceOrderById(order.OrderNo).OutCompanyID;
                //送货单状态
                order.DeliveryOrderStatus = "0";
            }
            else if (check == "WX")
            {
                //送货单区分  外协
                order.DeliveryOrderType = "1";
                //生产单元ID  送货单所对应的订单号的部门ID
                order.DepartID = supplierOrderRepository.GetMCSupplierOrderById(order.OrderNo).DepartmentID;
                //送货单位
                order.DeliveryCompanyID = supplierOrderRepository.GetMCSupplierOrderById(order.OrderNo).OutCompanyID;
                //送货单状态
                order.DeliveryOrderStatus = "1";
            }
            order.DeliveryUID = deliveryOrder.DeliveryUID;
            order.DeliveryDate = deliveryOrder.DeliveryDate;
            order.TelNo = deliveryOrder.TelNo;
            order.BatchID = deliveryOrder.DeliveryOrderID;//批次号
            order.DeliveryOrderStatus = "0";
            order.VerifyUID = null;
            order.VerifyDate = null;
            order.IspcUID = null;
            order.IspcDate = null;
            order.PrccUID = null;
            order.PrccDate = null;
            order.WhkpUID = null;
            order.WhkpDate = null;
            order.DelFlag = "0";
            order.PrintFlag = "0";//打印区分
            order.EffeFlag = "0";//有效FLG
            order.CreUsrID = uID;
            order.CreDt = t;
            order.UpdUsrID = uID;
            order.UpdDt = t;
            order.DelDt = null;
            order.DelUsrID = null;

            List<DeliveryOrderInfo> list = new List<DeliveryOrderInfo>();
            DeliveryOrderInfo deliveryInfo = new DeliveryOrderInfo();
            deliveryInfo.DelivDetailsList = details;
            deliveryInfo.DelivOrderInfo = order;
            list.Add(deliveryInfo);
            bool result = purchase4InernalService.AddDeliveryOrder(list);
            if (result == false)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00034);
            }
            else
            {
                return SM_Purchase.SMSG_PUR_S00001;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deliveryOrder">筛选条件</param>
        /// <param name="orderList">筛选条件</param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string Update(VM_DeliveryOrderForShow deliveryOrder, Dictionary<string, string>[] orderList,string uID)
        {

            DateTime t = System.DateTime.Now;
            #region 对MCDeliveryOrderDetail表更新
            //每件数量、件数、零头、实收数量 detail表
            for (int i = 0; i < orderList.Length; i++)
            {
                #region 条件判断
                //每件数量
                if (String.IsNullOrEmpty(orderList[i]["InnumQuantity"]))
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00052, new string[] { Convert.ToString(i) }));
                }
                else
                {
                    //验证为数字
                    Match m = Regex.Match(orderList[i]["InnumQuantity"], "^[1-9][0-9]*$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00053, new string[] { Convert.ToString(i) }));
                    }
                    if (orderList[i]["InnumQuantity"].Length > 10)
                    {
                        throw new Exception( ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00054, new string[] { Convert.ToString(i) }));
                    }
                }
                //件数
                if (String.IsNullOrEmpty(orderList[i]["Num"]))
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00055, new string[] { Convert.ToString(i) }));
                }
                else
                {
                    //验证为数字
                    Match m = Regex.Match(orderList[i]["Num"], "^[1-9][0-9]*$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00056, new string[] { Convert.ToString(i) }));
                    }
                    if (orderList[i]["Num"].Length > 10)
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00057, new string[] { Convert.ToString(i) }));
                    }
                }
                //零头
                if (String.IsNullOrEmpty(orderList[i]["Scrap"]))
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00058, new string[] { Convert.ToString(i) }));
                }
                else
                {
                    //验证为数字
                    Match m = Regex.Match(orderList[i]["Scrap"], "^[1-9][0-9]*$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00059, new string[] { Convert.ToString(i) }));
                    }
                    if (orderList[i]["Scrap"].Length > 10)
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00060, new string[] { Convert.ToString(i) }));
                    }
                }
                //实收数量
                if (String.IsNullOrEmpty(orderList[i]["ActualQuantity"]))
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00061, new string[] { Convert.ToString(i) }));
                }
                else
                {
                    //验证为数字
                    Match m = Regex.Match(orderList[i]["ActualQuantity"], "^[1-9][0-9]*$");
                    if (m.Length == 0)//不符合正则表达式
                    {
                        throw new Exception( ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00062, new string[] { Convert.ToString(i) }));
                    }
                    if (orderList[i]["ActualQuantity"].Length > 10)
                    {
                        throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00063, new string[] { Convert.ToString(i) }));
                    }
                }
                //备注
                if (orderList[i]["Remarks"].Length > 512)
                {
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00064, new string[] { Convert.ToString(i) }));
                }
                #endregion
                MCDeliveryOrderDetail detail = new MCDeliveryOrderDetail();
                detail.DeliveryOrderID = deliveryOrder.DeliveryOrderID;
                detail.MaterielID = orderList[i]["MaterielID"];
                detail.InnumQuantity = Convert.ToDecimal(orderList[i]["InnumQuantity"]);
                detail.Num = Convert.ToDecimal(orderList[i]["Num"]);
                detail.Scrap = Convert.ToDecimal(orderList[i]["Scrap"]);
                detail.ActualQuantity = Convert.ToDecimal(orderList[i]["ActualQuantity"]);
                detail.Remarks = orderList[i]["Remarks"];
                detail.UpdUsrID = uID;
                detail.UpdDt = t;
                bool re = deliveryOrderRepository.UpdateDetail(detail);
                if (re == false)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00034);
                }
            }
            #endregion

            #region 更新MCDeliveryOrder表
            MCDeliveryOrder order = new MCDeliveryOrder();
            order.DeliveryOrderID = deliveryOrder.DeliveryOrderID;
            order.DeliveryDate = deliveryOrder.DeliveryDate;
            order.DeliveryCompanyID = deliveryOrder.DeliveryCompanyID;
            order.DeliveryUID = deliveryOrder.DeliveryUID;
            order.TelNo = deliveryOrder.TelNo;
            order.UpdUsrID = uID;
            order.UpdDt = t;
            bool sult = deliveryOrderListRepository.UpdateOrder(order);
            if (sult)
            {
                return SM_Purchase.SMSG_PUR_S00001;
            }
            else
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00034);
            }
            #endregion

        }

        /// <summary>
        /// 得到送货单号对应数据
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <returns></returns>
        public MCDeliveryOrder getDeliveryOrderById(string deliveryOrderID)
        {
            return deliveryOrderListRepository.getDeliveryOrderById(deliveryOrderID);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        public string printInfo(string deliveryOrderID, string uId)
        {
            bool result = deliveryOrderListRepository.printInfo(deliveryOrderID, uId);
            if (result != true)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00034);
            }
            return SM_Purchase.SMSG_PUR_S00001;
        }

    }
}
