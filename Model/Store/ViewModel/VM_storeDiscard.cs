using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 在库品报废申请一览画面查询
    /// </summary>
    public class VM_storeDiscardForSearch
    {
        public string WareHouseID { get; set; }//仓库ID
        public string discardId { set; get; }//报废单号
        public string State { get; set; }//状态
        public DateTime? BeginDate { get; set; }//开始日期
        public DateTime? EndDate { get; set; }//结束日期
    } 
     /// <summary>
    /// 在库品报废申请一览画面显示
    /// </summary>
    public class VM_StoreDiscardForTableShow
        {
            public string BthID { set; get; }//批次号
            public string discardId { get; set; }//报废单号
            public string AplUserID { get; set; }//申请人
            public string AplUser{ get; set; }//申请人
            public string State { get; set; }//状态
            public DateTime? SltDate { get; set; }//报废申请日期
            public string PartDtID { get; set; }//物料编号
        }

    /// <summary>
    /// 在库品报废申请详细
    /// </summary>
    public class VM_StoreDiscardDetailForTableShow
    {
        public string DiscardID { get; set; }//报废单号ID
        public string PartAbbrevi { get; set; }//零件略称
        public string PartDtID { get; set; }//物料编号
        public string BthID { set; get; }//批次号
        public string PartDtName { get;set;}//物料编号
        public string PartDtSpec { get; set; }//规格
        public decimal Quantity { get; set; }//数量
        public decimal QuantityDiscard { get; set; }//数量
        public decimal QuantityTotle{ get; set; }//数量
        public decimal PrchsUp { get; set; }//单价
        public decimal TotalAmt { get; set; }//总价
        public string WareHouseID { get; set; }//仓库ID
        public DateTime? InDate { get; set; }//入库日期
        public string QualityPro { get; set; }//报废原因
        public string SltPlan { get; set; }//处理方案
        public DateTime? SltDate { get; set; }//处理日期
        public string AplUserID { set; get; }//编制人员ID
        public string VrfUserID { set; get; }//审核人员ID
        public string AppovUserID { set; get; }//批准人员ID
        public decimal UseableQty { set; get; }//可用在库数量
        public decimal CurrentQty { set; get; }//实际在库数量
        public decimal TotalAmtTo { set; get; }//总价
        public decimal TotalValuatUp { set; get; }//估价总价
        public string StoreCls { set; get; }//在库区分
        public List<VM_StoreDiscardDetailForTableShow> orderList { get; set; }//tableShow的list
    }


    /// <summary>
    /// 在库品报废申请详细
    /// </summary>
    public class VM_StoreDiscardDetailForSearch
    {
        public string BthID { set; get; }//批次号
        public string DiscardID { get; set; }//报废单号ID
        public string PdtID { get; set; }//零件ID
        public string WhID { set; get; }//仓库ID
        public string State { set; get; }//状态 
    }



    /// <summary>
    /// 在库待报废品一览
    /// </summary>
    public class VM_StoreDiscardForShow
    {
        public string PartDtID { get; set; }//物料编号
        public string ID { get; set; }//物料简称
        public string PartDtName { get; set; }//物料名称
        public string PartDtSpec { get; set; }//规格
        public decimal Quantity { get; set; }//数量
        public string StoreCls { get; set; }//让步区分
        public string WareHouseID { get; set; }//仓库ID
        public DateTime? InDate { get; set; }//入库日期
        public decimal Pricee { get; set; }// 单价
        public string BthID { set; get; }//批次号
        public string PartAbbrevi { get; set; }//零件略称
    }

    /// <summary>
    /// 在库待报废品一览查询
    /// </summary>
    public class VM_StoreDiscardForSearch
    {
        public string WareHouseID { get; set; }//仓库ID
        public string PartDtID { get; set; }//物料编号
        public string PartDtName { get; set; }//物料名称
    }

}
