/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：SupplierOrderServiceImp.cs
// 文件功能描述：
//              调度单画面Service接口类的实现
// 
// 创建标识：2013/11/14  廖齐玉 创建
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;
using BLL.ServerMessage;
using BLL_InsideInterface;

namespace BLL
{
    /// <summary>
    /// 调度单画面Service接口类的实现
    /// </summary>
    public class SupplierOrderServiceImp:AbstractService,ISupplierOrderService 
    {
        // 引入需要调用的Repository类
        // 调度单详细表的Repository类
        private ISupplierOrderDetailRepository supplierOrderDetailRepository;
        // 调度单表的Repository类
        private ISupplierOrderRepository supplierOrderRepository;
        // 采购部共通Repository类
        private IPurchase4InernalService purchase4InernalService;

        /// <summary>
        /// repository类的实现
        /// </summary>
        /// <param name="supplierOrderDetailRepository">调度单详细表的Repository类</param>
        /// <param name="supplierOrderRepository">调度单表的Repository类</param>
        /// <param name="purchase4InernalService">采购部共通Repository类</param>
        public SupplierOrderServiceImp(ISupplierOrderDetailRepository supplierOrderDetailRepository, ISupplierOrderRepository supplierOrderRepository, IPurchase4InernalService purchase4InernalService
           )
        {
            this.supplierOrderDetailRepository = supplierOrderDetailRepository;
            this.supplierOrderRepository = supplierOrderRepository;
            this.purchase4InernalService = purchase4InernalService;
        }

      
        /// <summary>
        /// 调度单明细表里的简要信息显示到界面上
        /// </summary>
        /// <param name="supplierOrder">外协单详细 数据ViewModel</param>
        /// <returns></returns>
        public IEnumerable<VM_SupplierOrder> GetSupplierOrderByIdForSearch(VM_SupplierOrder supplierOrder,Paging page)
        {
            return supplierOrderDetailRepository.GetSupplierOrderDetailByIdForSearch(supplierOrder.SupOrderID ,page);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="uId">用户ID</param>
        /// <param name="s">数据视图</param>
        /// <param name="orderDetailData">Table表数据集合</param>
        /// <returns></returns>     
        [TransactionAop]
        public bool UpdateSupplierOrderDetail(string uId, VM_SupplierOrderInfor s, Dictionary<string, string>[] orderDetailData)
        {
            //执行结果
            bool result = false;
            string errorList = "";
            // detail表值得存放
            string value = "";
    
            for (int i = 0; i < orderDetailData.Length; i++)
            {

                #region 对单价、估价、交货日期、备注进行验证

                // 单价输入不正确未做
                // 给变量value 赋值当前价格
                value = orderDetailData[i]["PrchsUp"];
                if (value == "")
                {
                    //第【N】行的单价未输入，请您输入正确的单价
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { orderDetailData[i]["RowIndex"] });
                }
                else {
                    // 转换value值的输出值
                    int a = 0;
                    if (int.TryParse(value, out a) == false) {
                        //第【N】行的单价为无效数值，请您输入正确的单价
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00009, new string[] { orderDetailData[i]["RowIndex"] });
                    }
                    int len = value.Length;
                    if (len > 10) {
                        //第【N】行的单价长度超过10，请您输入正确的单价
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00008, new string[] { orderDetailData[i]["RowIndex"] });
                    }
                }

                // 估价未做正确输入
                // 给变量value 赋值当前估价
                value = orderDetailData[i]["Evaluate"];
                if (value == "")
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00026, new string[] { orderDetailData[i]["RowIndex"] });
                }
                else {
                    // 转换value值的输出值
                    int a = 0;
                    if (int.TryParse(value, out a) == false)
                    {
                        //第【N】行的估价为无效数值，请您输入正确的单价
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00026, new string[] { orderDetailData[i]["RowIndex"] });
                    }
                    int len = value.Length;
                    if (len > 10)
                    {
                        //第【N】行的单价长度超过10，请您输入正确的单价
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00046, new string[] { orderDetailData[i]["RowIndex"] });
                    }
                }

                // 判断输入的交货日期为无效日期 
                // 给 value 赋值需要操作的日期
                value = orderDetailData[i]["DlyDate"];
                if (value == "")
                {
                    //第【N】行的交货日期未输入，请您输入正确的交货日期
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { orderDetailData[i]["RowIndex"] });
                }
                else {
                    // 比较输出的日期，初始化
                    DateTime a=DateTime.Now;
                     if (DateTime.TryParse(value, out a)==false){
                         //第【N】行的交货日期为无效日期，请您输入正确的交货日期
                         errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00011, new string[] { orderDetailData[i]["RowIndex"] });
                     };
                }

                // 备注
                //  value 赋值 = remarks
                value = orderDetailData[i]["Remarks"];
                if (value != "")
                {
                    // 获得字符串的总的字节数
                    if (Encoding.Default.GetBytes(value).Length > 512)
                    {
                        //第【N】行输入的备注的字节数超过？？，请重新输入
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00013, new string[] { orderDetailData[i]["RowIndex"] });
                    }
                }
                if (errorList != "")
                {
                    throw new Exception(errorList);
                }
                #endregion

                // 明细表数据添加
                // 初始化
                MCSupplierOrderDetail supplierOrderDetail = new MCSupplierOrderDetail();
                // 赋值
                supplierOrderDetail.SupOrderID = s.SupOdrId;
                supplierOrderDetail.ProductPartID = orderDetailData[i]["ProductPartID"];

                supplierOrderDetail.UnitPrice = Convert.ToDecimal(orderDetailData[i]["PrchsUp"]);
                supplierOrderDetail.Evaluate = Convert.ToDecimal(orderDetailData[i]["Evaluate"]);
                supplierOrderDetail.DeliveryDate = Convert.ToDateTime(orderDetailData[i]["DlyDate"]);
                supplierOrderDetail.Remarks = orderDetailData[i]["Remarks"];
                supplierOrderDetail.UpdUsrID =uId;
                supplierOrderDetail.UpdDt =System.DateTime.Now;

                result = supplierOrderDetailRepository.UpdateSupplierOrderDetail(supplierOrderDetail);
                if (result == false)
                {
                    throw new Exception("修改失败！");
                }
            }
            
            // 修改调度单表
            MCSupplierOrder so = new MCSupplierOrder();
            // 获取原ID对应的实体
            so = supplierOrderRepository.GetEntityById(s.SupOdrId);
            // 待修改的属性重新赋值
            // 制单人,实体中制单人为空，当制单人有值时传的值也是空
            if (string.IsNullOrEmpty(so.MarkUID))       
            {
                if (string.IsNullOrEmpty(s.MarkName))
                {
                    // 制单人为空时，制单人为修改者
                    so.MarkUID = uId;
                }
                else
                {
                    so.MarkUID = s.MarkName;
                }
                so.MarkSignDate = System.DateTime.Now;       // 制单人签字时间
            }


            // 生产主管修改
            if (string.IsNullOrEmpty(so.PrdMngrUID))
            {
                if (string.IsNullOrEmpty(s.PrdMngrName)==false)
                {
                    so.PrdMngrUID = s.PrdMngrName;            // 生产主管
                    so.PrdMngrSignDate = System.DateTime.Now; // 生产主管签字时间
                }
            }

            // 经办人修改
            if (string.IsNullOrEmpty(so.OptrUID))
            {
                // 界面上经办人有值
                if (string.IsNullOrEmpty(s.OptrName) == false)
                {
                    if (string.IsNullOrEmpty(so.PrdMngrUID))
                    {
                        throw new Exception("生产主管未签字！");
                    }
                    // 调度单状态不为【未处理】
                    if (so.SupOrderStatus != Constant.SupplierOrderStatus.UNTREATED)
                    {
                        so.SupOrderStatus = Constant.SupplierOrderStatus.DONE;//状态：【已经办】
                        so.OptrUID = s.OptrName;                  // 经办人
                        so.OptrSignDate = System.DateTime.Now;    // 经办人签字时间
                    }
                    else {
                        throw new Exception("调度单未审核，经办人不能签字！");
                    }
                }
            }
            
            so.UpdUsrID = uId;                      // 修改人
            so.UpdDt = System.DateTime.Now;         // 系统的当前时间
            // 更新调度单表
            if (supplierOrderRepository.Update(so) == false) 
            {
                throw new Exception("修改失败！");
            };

            return true;
        }

        /// <summary>
        ///获取调度单信息 
        /// </summary>
        /// <param name="supplierOrderId">调度单号Id</param>
        /// <returns></returns>
        VM_SupplierOrderInfor ISupplierOrderService.GetDetailInformation(string supplierOrderId)
        {
            return supplierOrderDetailRepository.GetSupplierOrderDetailInforById(supplierOrderId);
        }

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <param name="supOdrId">外协单号</param>
        /// <param name="uId">用户ID</param>
        /// <param name="s">数据视图</param>
        /// <param name="orderDetailData">Table表数据集合</param>
        /// <returns></returns>
        [TransactionAop]
        public bool Add(string supOdrId, string uId, VM_SupplierOrderInfor s, Dictionary<string, string>[] orderDetailData)
        {
            //出错信息
            string errorList="";
            //执行结果

            #region 调度单信息监测

            //客户订单号不为空
            if (s.ClientOrderID == null)//客户订单号未输入
            {
                //您输入的客户定单号不存在，请输入正确的客户订单号！
                throw new Exception(SM_Purchase.SMSG_PUR_E00001);
            }
            //调入单位不为空
            if (s.IncompId == null)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00029);
            }
            //调度单所在单位不为空
            if (s.Department == null)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00032);
            }
            //调度单的紧急状态不为空
            if (s.UrgentStatus == null)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00031);
            }
            //调度单的类型不为空
            if (s.OrderType == null)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00030);
            } 
            #endregion


            //拆分客户订单号
            int cusLength = s.ClientOrderID.Length;
            //客户订单号
            string customerOrderID = s.ClientOrderID.Substring(0, cusLength - 2);
            //客户订单明细号
            string customerOrderDetailID = s.ClientOrderID.Substring(cusLength - 2);

            if (orderDetailData == null)
            {
                throw new Exception(SM_Purchase.SMSG_PUR_E00033);
            }
            //明细表添加
            List<MCSupplierOrderDetail> orderDetailList = new List<MCSupplierOrderDetail>();
            for (int listCount = 0; listCount < orderDetailData.Length; listCount++)
            {
                #region 调度单明细表判断

                //零件ID号判断
                if (orderDetailData[listCount]["ProductPartID"] == "")
                {
                    //第【N】行的零件ID号未输入，请您输入您所需的零件ID号   
                    throw new Exception(ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00005, new string[] { orderDetailData[listCount]["RowIndex"] }));
                }
                //加工工艺判断
                if (orderDetailData[listCount]["PdProcDtID"] == "")
                {
                    //第【N】行输入的加工工艺为无效数据，请您输入正确的加工工艺
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00028, new string[] { orderDetailData[listCount]["RowIndex"] });
                }
                //数量判断
                if (orderDetailData[listCount]["Qty"] == "")
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00006, new string[] { orderDetailData[listCount]["RowIndex"] });
                }
                //单价判断
                if (orderDetailData[listCount]["PrchsUp"] == "")
                {
                    //第【N】行的单价未输入，请您输入正确的单价
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00008, new string[] { orderDetailData[listCount]["RowIndex"] });
                }

                //判断输入的交货日期为无效日期 未做
                if (orderDetailData[listCount]["DlyDate"] == "" || Convert.ToDateTime(orderDetailData[listCount]["DlyDate"]) == null)
                {
                    //第【N】行的交货日期未输入，请您输入正确的交货日期
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00010, new string[] { orderDetailData[listCount]["RowIndex"] });
                }
                //第【N】行的估价未输入
                if (orderDetailData[listCount]["Evaluate"] == "")
                {
                    errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00026, new string[] { orderDetailData[listCount]["RowIndex"] });
                }
                if (orderDetailData[listCount]["Remarks"] != "")//备注
                {
                    if (orderDetailData[listCount]["Remarks"].Length > 512)
                    {
                        //第【N】行输入的备注的字节数超过？？，请重新输入
                        errorList += ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00013, new string[] { orderDetailData[listCount]["RowIndex"] });
                    }
                }
                if (errorList != "")
                {
                    throw new Exception(errorList);
                } 
                #endregion
                
                //明细表添加
                MCSupplierOrderDetail md = new MCSupplierOrderDetail();
                md.SupOrderID = supOdrId;
                md.CustomerOrderID = customerOrderID;
                md.CustomerOrderDetailID = customerOrderDetailID;
                md.ProductPartID = orderDetailData[listCount]["ProductPartID"];
                md.ProductID = "";//数据来源不清
                md.PdProcID = orderDetailData[listCount]["PdProcDtID"];  
                md.RequestQuantity = Convert.ToDecimal(orderDetailData[listCount]["Qty"]);
                md.UnitPrice = Convert.ToDecimal(orderDetailData[listCount]["PrchsUp"]);
                md.Evaluate = Convert.ToDecimal(orderDetailData[listCount]["Evaluate"]);
                md.DeliveryDate = Convert.ToDateTime(orderDetailData[listCount]["DlyDate"]);
                md.Remarks = orderDetailData[listCount]["Remarks"];
                md.CreUsrID = uId;
                md.WarehouseID = "0102";//没有数据来源
                md.CompleteStatus = "0";

                //将明细表添加到list中
                orderDetailList.Add(md);
            }


            //调度单表的添加
            MCSupplierOrder mo = new MCSupplierOrder();
            mo.SupOrderID = supOdrId;
            mo.DepartmentID = s.Department;
            mo.InCompanyID = s.IncompId;
            if (s.MarkName == null)
            {
                s.MarkName = "";
                mo.MarkSignDate = Constant.CONST_FIELD.DB_INIT_DATETIME;//初始值
            }
            else
            {
                mo.MarkSignDate = System.DateTime.Now;//当前时间
            }
            mo.MarkUID = s.MarkName;
            if (s.OptrName == null)
            {
                s.OptrName = "";
                mo.OptrSignDate = Constant.CONST_FIELD.DB_INIT_DATETIME;//初始值
            }
            else
            {
                mo.MarkSignDate = System.DateTime.Now;//当前时间
            }
            mo.OptrUID = s.OptrName;
            if (s.PrdMngrName == null)
            {
                s.PrdMngrName = "";
                mo.PrdMngrSignDate = Constant.CONST_FIELD.DB_INIT_DATETIME;//初始值
            }
            else
            {
                mo.PrdMngrSignDate = System.DateTime.Now;//当前时间
            }
            mo.PrdMngrUID = s.PrdMngrName;
            mo.OrderType = s.OrderType;
            mo.UrgentStatus = s.UrgentStatus;
            mo.PrintFlag = "0";//初始值
            mo.SupOrderStatus = Constant.SupplierOrderStatus.UNTREATED;//初始值,未处理
            mo.CreUsrID = uId;

            //判断返回结果

            BLL_InsideInterface.SupplierOrderInfo soi = new BLL_InsideInterface.SupplierOrderInfo();
            soi.SupOrderInfo = mo;
            soi.SupDetailsList = orderDetailList;
            List<BLL_InsideInterface.SupplierOrderInfo> list = new List<BLL_InsideInterface.SupplierOrderInfo>();
            list.Add(soi);

            if (purchase4InernalService.AddSupplierOrder(list) == false) 
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 判断是否存在调度单实体
        /// </summary>
        /// <param name="supOrderId"></param>
        /// <returns></returns>
        public bool GetSupplierOrderById(string supOrderId)
        {
            //是否查到相应实体结果，无实体 返回False
            if (supplierOrderRepository.GetEntityById(supOrderId) == null)
            {
                return false;
            };
            return true;
        }

        /// <summary>
        /// 获取外协调度单一览画面数据方法
        /// </summary>
        /// <param name="searchCondition">获取条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>调度单List</returns>
        public IEnumerable<VM_SupplierOrderList> GetSupplierOrderListBySearchByPage(VM_SupplierOrderListForSearch searchCondition, Paging paging)
        {
            return supplierOrderRepository.GetSupplierOrderListForSearch(searchCondition, paging);
        }

        /// <summary>
        /// 删除外协进度单  单表删除 
        /// </summary>
        /// <param name="list">待删除Id的集合</param>
        /// <param name="uId">删除者的Id</param>
        /// <returns>执行结果</returns>
        public bool DeleteSupplierOrder(List<string> list, string uId)
        {
            //存放已经办的supplierOrderId
            string doneOrderList = "";
            foreach (var id in list)
            {
                //无数据时，返回没有删除实体信息
                if (string.IsNullOrEmpty(id)==true)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00021);
                }
                //获取Id的实体
                MCSupplierOrder ms =supplierOrderRepository.GetEntityById(id);
                //判断该实体是否是已经办的
                if (ms.SupOrderStatus ==Constant.SupplierOrderStatus.DONE ) 
                {
                    doneOrderList += id;
                    doneOrderList += ",";
                }
                // 删除调度单表数据
                MCSupplierOrder s = new MCSupplierOrder();
                s = supplierOrderRepository.GetEntityById(id);
                s.DelFlag = Constant.CONST_FIELD.DELETED;
                s.DelUsrID = uId;
                s.DelDt = DateTime.Now;
                s.UpdUsrID = uId;
                s.UpdDt = DateTime.Now;
                supplierOrderRepository.Update(s);


                //删除明细表数据
                supplierOrderDetailRepository.DeleteSupplierOrderDetail(
                    new MCSupplierOrderDetail { 
                        DelFlag = Constant.CONST_FIELD.DELETED, 
                        DelUsrID = uId,
                        DelDt = DateTime.Now,
                        UpdUsrID= uId,
                        UpdDt= DateTime.Now,
                        SupOrderID = id 
                    });

                //外协指示表数量减少
                //待编写

            }
            if (string.IsNullOrEmpty(doneOrderList) == false)
            {
                //去掉最后的“,”号
                string orderList = doneOrderList.Substring(0,doneOrderList.Length-1);
                //错误信息  “已经办”的数据不能删除
                string errorMessage = ResourceHelper.ConvertMessage(SM_Purchase.SMSG_PUR_E00024, new string[] { orderList });
                throw new Exception(errorMessage);
            }
            return true;
        }

        /// <summary>
        /// 审核数据
        /// </summary>
        /// <param name="list">待审核的Id集合</param>
        /// <param name="uId">审核者的Id</param>
        /// <returns>执行结果</returns>
        public bool AuditSupplierOrder(List<string> list, string uId)
        {
            foreach (var Id in list)
            {
                // 无数据时，返回未选择审核对象
                if ( string.IsNullOrEmpty(Id)==true)
                {
                    throw new Exception(SM_Purchase.SMSG_PUR_E00023);
                }
                // 获取Id号对应的实体
                MCSupplierOrder s = supplierOrderRepository.GetEntityById(Id);
                // 添加为保存时，实体不存在
                if (s == null)
                {
                    throw new Exception("不存在该调度单！请先保存或是检查数据是否正常");
                }
                // 已审核
                else if (s.SupOrderStatus != Constant.SupplierOrderStatus.UNTREATED)
                {
                    throw new Exception("该调度单已审核！");
                }
                // 未审核，进行审核操作
                // 初始化
                MCSupplierOrder so = new MCSupplierOrder();
                so = supplierOrderRepository.GetEntityById(Id);
                // 赋值
                so.SupOrderStatus = Constant.SupplierOrderStatus.AUDITED;
                so.UpdUsrID = uId;
                so.UpdDt = DateTime.Now;
                // 审核，更新数据库
                supplierOrderRepository.Update(so);
            }
            return true;
        }

        /// <summary>
        /// 外协调度单的打印
        /// </summary>
        /// <param name="uId">登陆者的ID</param>
        /// <param name="supplierOrderId">需要打印的调度单ID</param>
        /// <returns></returns>
        public bool PrintSupplierOrder(string uId, string supplierOrderId)
        {
            // 执行结果
            bool result = false;
            // 该调度单的状态
            string supplierOrderStatus = "";
            // 获取实体
            MCSupplierOrder s = supplierOrderRepository.GetEntityById(supplierOrderId);
            // 获取实体的状态
            supplierOrderStatus = s.SupOrderStatus;
            // 判断调度单是否为已经办
            if (supplierOrderStatus == Constant.SupplierOrderStatus.DONE)
            {
                s.PrintFlag = Constant.CONST_FIELD.EFFECTIVE;
                s.UpdUsrID = uId;
                s.UpdDt =System.DateTime.Now;
                // 修改打印标志和修改人及时间
                result = supplierOrderRepository.Update(s) == true;
            }
            else {
                // 不是【已经办】状态的订单的警示信息。
                throw new Exception("该打印的调度单未经办，请先确定调度单为已经办，才可打印！");
            }
            return result;
        }
    }
}
