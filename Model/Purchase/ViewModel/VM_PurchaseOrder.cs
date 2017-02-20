/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_purchaseOrderList.cs
// 文件功能描述：产品外购单一览画面的Model
//      
// 修改履历：2013/11/21 刘云 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region 查询结果的视图Model类
    /// <summary>
    /// 查询结果的视图Model类
    /// </summary>
    public class VM_PurchaseOrderForTableShow
    {
        public string RowIndex { get; set; }//存放第几行  行数
        public string PlanNo { get; set; }
        public string ProPartID { get; set; }//产品零件ID  物料编号
        public string PartName { get; set; }//外购件名称
        public decimal RequestQuantity { get; set; }//单据要求数量 数量
        public string MaterialsSpecReq { get; set; }//材料和规格要求
        public decimal UnitPrice { get; set; }//单价
        public decimal Evaluate { get; set; }//估价
        public DateTime DeliveryDate { get; set; }//交货日期
        public string VersionNum { get; set; }//版本号
        public string Remarks { get; set; }//备注
        public List<string> CustomerOrder { get; set; }//客户订单号 隐藏项
        public List<string> CustomerOrderDetail { get; set; }//客户订单详细号 隐藏项
    }
    #endregion

    #region 查询所有的视图Model类
    /// <summary>
    /// 查询所有的视图Model类
    /// </summary>
    public class VM_PurchaseOrderForShow
    { 
        public string OutOrderID { get; set; }//外购单号
        public string UrgentStatus { get; set; }// 紧急状态
        public string CustomerOrder { set; get; }//客户订单号
        public DateTime EstablishDate { get; set; }// 编制时间  下单日期
        public string DeptName { get; set; }//生产部门
        public string DepartmentID { get; set; }//生产部门ID
        public string CompName { get; set; }//承接单位  外购单位ID对应
        public string OutCompanyID { get; set; }//承接单位对应ID
        public string ApproveUID { get; set; }// 批准人
        public string VerifyUID { get; set; }// 审核人
        public string EstablishUID { get; set; }// 编制人
        public string SignUID { get; set; }// 签收人
        public string Remarks2 { get; set; }// 注释
        public string FaxNo { get; set; }// FAX
        public string ProductPartID { get; set; }//产品零件ID  物料编号 判断需要
        public List<VM_PurchaseOrderForTableShow> orderList { get; set; }//tableShow的list
    }
    #endregion
}
