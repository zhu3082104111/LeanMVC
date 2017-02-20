using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 订单指示接收搜索条件视图
    /// 20131114 梁龙飞C
    /// </summary>
    public class VM_OrderAcceptSearch
    {

        public VM_OrderAcceptSearch()
        {
            DeliveryDateBegin = Constant.CONST_FIELD.INIT_DATETIME;
            DeliveryDateEnd = Constant.CONST_FIELD.INIT_DATETIME;
        }

        public string ClientOrderID { get; set; }//客户订单号  
        public string ClientName { set; get; }//客户名称

        public string ProductID { get; set; }//产品ID
        public string ProductType { get; set; }//产品型号

        public string SalePersonName { set; get; }//售员名

        public DateTime DeliveryDateBegin { set; get; }//检索开始时间
        public DateTime DeliveryDateEnd { set; get; }//检索结束时间
        
        public string ProduceCellID { get; set; }//生产单元区分号 
        
    }

    /// <summary>
    /// 订单指示接收显示结果视图
    /// 20131114 梁龙飞C
    /// </summary>
    public class VM_OrderAcceptShow
    {
        public string ClientOrderID { get; set; }//订单号
        public string ClientOrderDetail { get; set; }//订单详细，订单中的次序
        public string ProductID { get; set; }//产品ID
        public string ProductType { get; set; }//产品型号

        public string strDeliveryDate;//DeliveryDate｛“yyyy-MM-dd”｝
        private DateTime datDeliveryDate;
        public DateTime DeliveryDate
        {
            get { return datDeliveryDate; }
            set
            {
                datDeliveryDate = value;
                strDeliveryDate = value.ToString("yyyy/MM/dd");
            }
        }//交货日期
      
        public string ProduceCellID { get; set; }//生产单元区分号
        public decimal Quantity { get; set; }//数量
        public decimal PackageQuantity { get; set; }//装箱数
        public string PackageSize { get; set; }//纸盒尺寸

        public string ClientName { set; get; }//客户名
        public string SalePersonName { set; get; }//售员名
    }


}
