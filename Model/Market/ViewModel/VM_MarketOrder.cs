using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Market
{
    #region 客户订单一览

    public class VM_MarketOrderForSearchMarketOrderTable
    {
        public string ClientOrderID { get; set; } //客户订单号
        public string ProductID { get; set; } //产品ID
        public string ClientID { get; set; } //客户ID
        public string OrderStatus { get; set; } //当前状态
        public string ProduceCellID { get; set; } //生产单元
        public DateTime? BeginDeliveryDate { get; set; } //开始交货日期
        public DateTime? EndDeliveryDate { get; set; } //结束交货日期
    }

    public class VM_MarketOrderForTableMarketOrder
    {
        public string ClientOrderID { get; set; } //客户订单号
        public string DeliveryDate;//显示交货日期
        private DateTime? delvyDate;//交货日期
        public DateTime? DelvyDate
        {
            get { return delvyDate; }
            set
            {
                delvyDate = value;
                DeliveryDate = value == null ? "" : DateTime.Parse(value.ToString()).ToString("yyyy/MM/dd");
            }
        }
        public string ClientName { get; set; } //客户名称
        public string EditName { get; set; } //制单人
        public string OtherMatter { get; set; } //备注
        public string StateFlag { get; set; } //当前状态
    }

    #endregion 

    #region 查看订单、修改订单

    public class VM_MarketOrderForShowMarketOrderInfo
    {
        public string ClientOrderID { get; set; } //客户订单号
        public decimal ClientVersion { get; set; } //版数
        public DateTime? DeliveryDate { get; set; } //交货日期
        public string ClientID { get; set; } //客户ID
        public string ClientName { get; set; } //客户名称
        public string PackageRequire { get; set; } //包装要求
        public string PackageRequireImage1 { get; set; } //包装要求图1
        public string PackageRequireImage2 { get; set; } //包装要求图2
        public string PackageRequireImage3 { get; set; } //包装要求图3
        public string PackageRequireImage4 { get; set; } //包装要求图4
        public string PackageRequireImage5 { get; set; } //包装要求图5
        public string OtherMatter { get; set; } //其他注意事项
        public string ApprovalFlag { get; set; } //审批FLG
        public string ApprovalFlagName { get; set; } //审批名称
        public string ApprovalUserID { get; set; } //审批人ID
        public string ApprovalUserName { get; set; } //审批人名称
        public DateTime? ApprovalDate { get; set; } //审批日期
        public string EditUserID1 { get; set; } //编制人1
        public string EditUserName1 { get; set; } //编制人名称1
        public DateTime? EditUserDate1 { get; set; } //编制日期1
        public string EditUserID2 { get; set; } //编制人2
        public string EditUserName2 { get; set; } //编制人名称2
        public DateTime? EditUserDate2 { get; set; } //编制日期2
        public string OrderProgressStatus { get; set; } //订单进度状态
        public string OrderProgressStatusName { get; set; } //订单进度状态名称
        public string OrderStatus { get; set; } //订单状态
        public string OrderStatusName { get; set; } //订单状态名称
    }
   
    #endregion

}