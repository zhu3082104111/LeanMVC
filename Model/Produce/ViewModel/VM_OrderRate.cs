/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_OrderRate.cs
// 文件功能描述：产品订单进度页面的视图model集
//     
// 修改履历：2013/11/09 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 产品和订单进度查询Model
    /// </summary>
    public class VM_OrderRateForSrarch
    {
        public string TxtClientOrderID { get; set; }//客户订单号
        public string ClientOrderDetail { get; set; }//客户订单详细
        public string ProductID { get; set; }//产品ID

        public string TxtProductID
        {
            get { return ProductID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var vals = value.Split(',');
                    ProductID = vals[0];
                    ClientOrderDetail = vals[1];
                }
            }
        }//产品型号和客户订单详细
        
    }

    /// <summary>
    /// 产品型号下拉框数据Model
    /// </summary>
    public class VM_ProduceType
    {
        public VM_ProduceType() 
        {
            selected = false;
        }

        public string ProductIdCOD { get; set; }//产品ID+客户订单详细
        public string ProdAbbrev { get; set; }//产品略称

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool selected{ get; set; }
    }

    /// <summary>
    /// 产品和订单进度查询头数据Model
    /// </summary>
    public class VM_HeaderData
    {
        public string ClientOrderID { get; set; }//客户订单号
        public Decimal PlanQuantity { get; set; }//计划数量
        public DateTime DeliveryDate { get; set; }//计划交期
        public decimal AchieveRate { get; set; }//达成率
    }

    /// <summary>
    /// 产品和订单进度查询一览Model
    /// </summary>
    public class VM_OrderRateForTableShow
    {
        /// <summary>
        /// 供界面显示用，唯一识别key
        /// </summary>
        public int id { get; set; }
        public string strID
        {
            get { return id.ToString(); }
            set
            {
                id = Convert.ToInt32(value);
            }
        }
        /// <summary>
        /// 供界面显示用，唯一识别key父项目ID
        /// </summary>
        public int _parentId { get; set; }
        public string strParentID
        {
            get { return _parentId.ToString(); }
            set
            {
                _parentId = Convert.ToInt32(value);
            }
        }

        public string ProductID { get; set; }//产品ID
        public string ProductType { get; set; }// 产品型号
        public string ProductsPartsID { get; set; }//产品零件ID
        public string MaterialName { get; set; }//物料名称
        public Decimal PlanNeedNumber { get; set; }//计划需求数量
        public Decimal TotalLockNumber { get; set; }//锁库数量合计
        public Decimal PurchaseQuantity { get; set; }//购买数量
        public DateTime StartDate { get; set; }//开始
        public DateTime EndDate { get; set; }//结束
        public Decimal STP_Num { get; set; }//自加工需求数量
        public Decimal STP_AchieveRate { get; set; }//自加工达成率
        public Decimal OutSource_Num { get; set; }//外购需求数量
        public Decimal OutSource_AchieveRate { get; set; }//外购达成率
        public Decimal Association_Num { get; set; }//外协需求数量
        public Decimal Association_AchieveRate { get; set; }//外协达成率
        public Decimal MaterialAchieveRate { get; set; }//物料达成率
        public decimal PreparationPeriod { get; set; }//备料工期（计划）

        public decimal ProduceProductionQuantity { get; set; }// 生产投产数量
        public decimal PurchProductionQuantity { get; set; }// 外购投产数量
        public decimal AssistProductionQuantity { get; set; }// 外协投产数量
    }

}
