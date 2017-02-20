using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 订单产品排产显示视图
    /// 20131120 梁龙飞C
    /// </summary>
    public class VM_ProductSchedulingShow
    {
        /// <summary>
        /// 供界面显示用，唯一识别key
        /// </summary>
        public int id { get; set; }

        //public string strID
        //{
        //    get { return id.ToString(); }
        //    set
        //    {
        //        id = Convert.ToInt32(value);
        //    }
        //}

        /// <summary>
        /// 供界面显示用，唯一识别key
        /// </summary>
        public int _parentId { get; set; }

        //public string strParentID
        //{
        //    get { return _parentId.ToString(); }
        //    set
        //    {
        //        _parentId = Convert.ToInt32(value);
        //    }
        //}

        /// <summary>
        /// 客户订单号 ，一级目录   
        /// </summary>
        public string ClientOrderID { get; set; }
        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string ClientOrderDetail { get; set; }


        /// <summary>
        /// 产品ID,一级目录
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 【隐藏】产品或零件ID，二级目录
        /// </summary>
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }//零件名

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WhID { get; set; }
        /// <summary>
        /// 材料及规格说明
        /// </summary>
        public string MaterialSpecification { get; set; }
        /// <summary>
        /// 单件产品需求物料数量
        /// </summary>
        public decimal ConstituteQuantity { get; set; }
        /// <summary>
        /// 【隐藏】物料损耗率  
        /// </summary>
        public decimal LossRate { get; set; }   

        /// <summary>
        /// 计划需求数量下限
        /// </summary>
        public decimal DemondQuantityFloor { get; set; }
        /// <summary>
        /// 计划需求数量上限
        /// </summary>
        public decimal DemondQuantityCeiling { get; set; }
        /// <summary>
        /// 正常库存数量
        /// </summary>
        public decimal NormalInStore { get; set; }
        /// <summary>
        /// 单配库存数量
        /// </summary>
        public decimal AbnormalInStore { get; set; }
        /// <summary>
        /// 正常锁存数量
        /// </summary>
        public decimal NormalInLock { get; set; }
        /// <summary>
        /// 单配锁存数量
        /// </summary>
        public decimal AbnormalInLock { get; set; }//

        /// <summary>
        /// 投料小计
        /// </summary>
        public decimal TotalDemondQuantity { get; set; }
  
        /// <summary>
        /// 计划生产数量
        /// </summary>
        public decimal ProduceQuantity { get; set; }
        /// <summary>
        /// 计划外购数量
        /// </summary>
        public decimal PurchQuantity { get; set; }
        /// <summary>
        /// 计划外协数量
        /// </summary>
        public decimal AssistQuantity { get; set; }

        public string strProvideDate="";
        public DateTime ProvideDate//提供日期
        {
            get { return Convert.ToDateTime(strProvideDate); }

            set
            {
                strProvideDate = value.ToString("yyyy/MM/dd");
            }
        }

        public string strStartDate="";
        /// <summary>
        /// 启动日期
        /// </summary>
        public DateTime StartDate 
        {
            get { return Convert.ToDateTime( strStartDate);}
            set
            {
                strStartDate = value.ToString("yyyy/MM/dd");
            }               
        }
        /// <summary>
        /// 标准备料工期，根据投产数量和工序生产率计算出标准耗时
        /// </summary>
        public decimal StandPreparationPeriod { get; set; }
        /// <summary>
        /// 备料工期（计划） ，计划耗时
        /// </summary>
        public decimal PreparationPeriod { get; set; }

        /// <summary>
        /// 工序ID 
        /// </summary>
        public string ProcessID { get; set; }
        /// <summary>
        /// 工序名
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 单位时间自产量
        /// </summary>
        public decimal SeUtProdQty { get; set; }
        /// <summary>
        /// 单位时间外协产量
        /// </summary>
        public decimal AsUtProdQty { get; set; }
        /// <summary>
        /// 单位时间外购产量
        /// </summary>
        public decimal PuUtProdQty { get; set; }


        public string Remark { get; set; }//备注

        /// <summary>
        /// 是否为非树叶的结点
        /// </summary>
        public bool IsHasChild { get; set; }
    }
}
