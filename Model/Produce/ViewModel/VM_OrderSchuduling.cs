using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 订单排产搜索条件
    /// 20131118 梁龙飞C
    /// </summary>
    public class VM_OrderSchedulingSearch
    {
        public VM_OrderSchedulingSearch()
        {
            DeliveryDateFrom = Constant.CONST_FIELD.INIT_DATETIME;
            DeliveryDateTo =Constant.CONST_FIELD.INIT_DATETIME;
        }

        public string ClientOrderID { get; set; }//订单号
        public string ProductID { get; set; }//产品ID
        public string ProductType { get; set; }//产品型号

        public DateTime DeliveryDateFrom { get; set; }
        public DateTime DeliveryDateTo { get; set; }

        public string SchedulingState { get; set; }  
    }
    /// <summary>
    /// 订单排产搜索结果
    /// 20131118 梁龙飞C
    /// </summary>
    public class VM_OrderSchedulingShow
    {
        public string ClientOrderID { get; set; }//订单号
        public string ClientOrderDetail { get; set; }//客户订单明细

        public string SalePersonName { get; set; }//销售员姓名

        public string strDeliveryDateOfPlan;
        public DateTime? DeliveryDateOfPlan
        {
            get
            {
                if (strDeliveryDateOfPlan=="")
                {
                    return null;
                }
                return Convert.ToDateTime(strDeliveryDateOfPlan); 
            }
            set
            {
                if (value==null)
                {
                    strDeliveryDateOfPlan = "";
                }
                strDeliveryDateOfPlan = ((DateTime)value).ToString("yyyy/MM/dd");
            }
        }//计划交期
        public string ProductID { get; set; }//产品ID
        public string ProductType { get; set; }//产品型号
        public decimal QuantityInPlan { get; set; }//计划数量

        private string schedulingState;
        /// <summary>
        /// 排产状态实际意义
        /// </summary>
        public string strSchedulingState;
        /// <summary>
        /// 排产状态
        /// 注意：排产状态与订单状态不同
        /// </summary>
        public string SchedulingState
        {
            get 
            {
                return schedulingState;
            }
            set
            {
                strSchedulingLink = "进行排产...";
                switch (value)
                {
                    case Constant.GeneralPlanState.ACCEPTED://接收状态
                        strSchedulingState = "未排产";break;
                    case Constant.GeneralPlanState.SCHEDULING:
                        strSchedulingState = "排产确认";break;
                    default:
                        strSchedulingState = "已排产";
                        strSchedulingLink = "";
                        break;
                }
                schedulingState = value;
            }
        }//排产状态

        /// <summary>
        /// 界面排产link字符串，自动生成，不应修改
        /// </summary>
        public string strSchedulingLink { get; set; }
    }
}
