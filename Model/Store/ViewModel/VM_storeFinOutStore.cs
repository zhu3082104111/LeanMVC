using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model.Store
{

    public class VM_storeFinOutStoreForSearch
    {
        public string ladiID { get; set; }  //提货单编号
    }

    public class VM_storeFinOutStoreForTableShow
    {
        public string ladiID { get; set; }  //提货单编号
    }

    public class VM_storeFinOutRecordForSearch
    {
        [EntityProperty("InerFinOutID", PropertyOperator.CONTAINS)]
        public string InerFinOutID { get; set; }  //送货单号
        [EntityProperty("LadiID", PropertyOperator.CONTAINS)]
        public string LadiID { get; set; }  //提货单号
        
        public string ClientOrderID { get; set; }  //订单号
        [EntityProperty("PartAbbrevi", PropertyOperator.CONTAINS)]
        public string PartAbbrevi { get; set; }  //产品ID
        [EntityProperty("OutDate", PropertyOperator.GE)]
        public DateTime? OutDate1 { get; set; }  //出库日期
        [EntityProperty("OutDate", PropertyOperator.LE)]
        public DateTime? OutDate2 { get; set; }  //出库日期
    }

    public class VM_storeFinOutRecordForTableShow
    {
        public string InerFinOutID { get; set; }  //内部成品送货单号
        public DateTime OutDate { get; set; }  //出库日期
        public string Remarks { set; get; }  //备注

        public string LadiID { get; set; }  //提货单号
        public string WhkpID { get; set; }  //出库人
    }

    public class VM_storeFinOutRecordDetailForSearch
    {
        
        public string LadiID { get; set; }  //提货单号
        public string InerFinOutID { get; set; }  //内部成品送货单号

    }

    public class VM_storeFinOutRecordDetailForTableShow
    {
        public string ClientOrderID { get; set; }  //订单号
        public string OrdPdtID { get; set; }  //物料编号
        public string ProductName { get; set; }  //物料名称
        public string BatchID { get; set; }  //批次号

        public string ProductSpec { get; set; } //规格型号
        //public string Unit { get; set; }  //单位
        public decimal Quantity { get; set; }  //数量
        public decimal PackPrePieceQuantity { get; set; }  //每件数量
        public decimal PackPieceQuantity { get; set; }  //件数
        public decimal FracQuantity { get; set; }  //零头
        public decimal PrchsUp { get; set; }  //单价
        public decimal NotaxAmt { get; set; }  //金额

        public string Remarks { get; set; }//备注
        public string OutWorks { get; set; }//出库人
        public string MRemarks { get; set; }//备注

        public string LadiID { get; set; }//提货单号
        public string InerFinOutID { get; set; }  //内部成品送货单号

    }

    public class VM_storeFinOutPrintIndexForTableShow
    {
        public string ClientOrderID { get; set; }  //订单号
        public string OrdPdtID { get; set; }  //物料编号
        public string ProductName { get; set; }  //物料名称
        public decimal Quantity { get; set; }  //数量
        public decimal PackPrePieceQuantity { get; set; }  //每件数量
        public decimal PackPieceQuantity { get; set; }  //件数
        public decimal FracQuantity { get; set; }  //零头
        public decimal GetQuantity { get; set; }  //实收数量
        public string Remarks { get; set; }//备注
        
    }

    public class VM_storeFinOutPrintForSearch
    {
        [EntityProperty("InerFinOutID", PropertyOperator.CONTAINS)]
        public string InerFinOutID { get; set; }  //内部成品送货单号
      
        //public string ClientOrderID { get; set; } //订单号
        [EntityProperty("OutDate", PropertyOperator.EQUAL)]
        public DateTime? OutDate { get; set; }  //出库日期
        [EntityProperty("PrintFlag", PropertyOperator.EQUAL)]
        public string PrintFlag { get; set; }//印刷标志
    }

    public class VM_storeFinOutPrintForTableShow
    {
        public string InerFinOutID { get; set; }  //内部成品送货单号
        public string ClientOrderID { get; set; }  //客户订单号
        public DateTime OutDate { get; set; }  //出库日期
        public string WareHouseKpID { get; set; }  //仓管员
        public string Remarks { get; set; }//备注

        public string OrdPdtID { get; set; }  //产品ID
        public string BatchID { get; set; }  //批次号
        public string ClientOrderDetail { get; set; }  //客户订单明细
    }

    /// <summary>
    /// 产品零件信息的视图Model（出库登录子查询画面专用）
    /// </summary>
    public class VM_ProdAndPartInfo
    {
        // 产品零件ID
        public string Id { get; set; }

        // 产品零件略称
        public string Abbrev { get; set; }

        // 产品零件名称
        public string Name { get; set; }

        //产品零件单价
        public decimal Pricee { get; set; }
    }

}
