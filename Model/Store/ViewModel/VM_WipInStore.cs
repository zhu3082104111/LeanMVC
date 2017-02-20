/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_WipInStore.cs
// 文件功能描述：
//          在制品库（待入库一览、入库登录、入库履历一览、在制品库入库打印预览）画面Model
//          即在制品库入库相关画面
//
// 创建标识：2013/11/18 杨灿 新建
//
// 修改表示：2013/11/21 杨灿 修改
// 修改描述：添加注释和字段初始化赋值
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region 在制品库待入库一览
    public class VM_WipInStoreForTableShow
    {
        //送货单号
        public string DeliveryOrderID { get; set; }

        //检验报告单号
        public string IsetRepID { set; get; }

        //供货商名称
        public string DeliveryCompanyID { get; set; }

        //物料名称
        public string MaterielID { get; set; }

        //质检日期
        public DateTime? ChkDt { get; set; }
    }


      public class VM_WipInStoreForSearch
      {
        //检验报告单号
        public string IsetRepID { set; get; }

        //送货单号
        public string DeliveryOrderID { get; set; }

        //供货商名称
        public string DeliveryCompanyID { get; set; }

        //物料ID
        public string MaterielID { get; set; }

        //物料名称
        public string MaterielName { get; set; }

        //质检开始日期
        public DateTime? StartDt { get; set; }

        //质检结束日期
        public DateTime? EndDt { get; set; }
        }
     #endregion


     #region 在制品库入库登录
     public class VM_WipInLoginStoreForTableShow
     {
            //计划单号
            public string PlanID { set; get; }

            //送货单号
            public string DeliveryOrderID { get; set; }

            //供货商ID
            public string CompID { get; set; }

            //加工单位(来自外协外购)
            public string ProcUnit { set; get; }

            //加工单位(来自生产)
            public string ProcUnits { set; get; }

            //批次号
            public string BthID { set; get; }

            //物资验收入库单号
            public string McIsetInListID { set; get; }

            //检验报告单号
            public string IsetRepID { set; get; }

            //让步区分
            public string GiCls { set; get; }

            //物资名称
            public string PdtName { set; get; }

            //物资ID
            public string PdtID { set; get; }

            //规格型号
            public string PdtSpec { set; get; }

            //加工工艺
            public string TecnProcess { set; get; }
         
            //合格
            public decimal Qty { set; get; }

            //报废
            public decimal ProScrapQty { set; get; }

            //料废
            public decimal ProMaterscrapQty { set; get; }

            //余料
            public decimal ProOverQty { set; get; }
         
            //缺料
            public decimal ProLackQty { set; get; }

            //合计
            public decimal ProTotalQty { set; get; }

            //单位
            public string Unit { set; get; }

            //加工单价
            public decimal PrchsUp { set; get; }

            //估价
            public decimal ValuatUp { set; get; }

            //金额
            public decimal NotaxAmt { set; get; }

            //备注
            public string Rmrs { set; get; }

            //入库日期
            public DateTime? InDate { set; get; }

           //登录标识
            public string WipLoginFlg { set; get; }

           //单价标识
            public string WipLoginPriceFlg { set; get; }

           //外协、外购、自生产区分
            public string OsSupProFlg { set; get; }

        }


     public class VM_WipInLoginStoreForSearch
     {
         //入库区分
         public string InMvCls { set; get; }

     }
     #endregion


      #region 在制品库入库履历一览
     public class VM_WipInRecordStoreForTableShow
     {
            //计划单号
            public string PlanID { set; get; }

            //送货单号
            public string DeliveryOrderID { get; set; }

            //加工单位
            public string ProcUnit { set; get; }

            //加工单位
            public string ProcUnits { set; get; }
         
            //供货商ID
            public string CompID { get; set; }

            //批次号
            public string BthID { set; get; }

            //物资验收入库单号
            public string McIsetInListID { set; get; }

            //检验报告单号
            public string IsetRepID { set; get; }

            //让步区分
            public string GiCls { set; get; }

            //物资ID
            public string PdtID { set; get; }

            //物资名称
            public string PdtName { set; get; }

            //规格型号
            public string PdtSpec { set; get; }

            //加工工艺
            public string TecnProcess { set; get; }
         
            //合格
            public decimal Qty { set; get; }

            //报废
            public decimal ProScrapQty { set; get; }

            //料废
            public decimal ProMaterscrapQty { set; get; }

            //余料
            public decimal ProOverQty { set; get; }
         
            //缺料
            public decimal ProLackQty { set; get; }

            //合计
            public decimal ProTotalQty { set; get; }

            //单位
            public string Unit { set; get; }

            //加工单价
            public decimal PrchsUp { set; get; }

            //金额
            public decimal NotaxAmt { set; get; }

            //备注
            public string Rmrs { set; get; }

            //入库日期
            public DateTime? InDate { set; get; }

            //单价标识
            public string WipInRecordPriceFlg { set; get; }

            //外协、外购、自生产区分
            public string OsSupProFlg { set; get; }
     
     }


     public class VM_WipInRecordStoreForSearch
     {
         //计划单号
         public string PlanID { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

         //批次号
         public string BthID { set; get; }

         //物资验收入库单号
         public string McIsetInListID { set; get; }

         //检验报告单号
         public string IsetRepID { set; get; }

         //让步区分
         public string GiCls { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //仓库编码
         public string WhID { set; get; }

         //规格型号
         public string PdtSpec { set; get; }

         //入库日期开始
         public DateTime? StartInDate { get; set; }

         //入库日期结束
         public DateTime? EndInDate { get; set; }

     }

      #endregion


     #region 在制品库入库打印选择
     public class VM_WipInPrintForTableShow
     {
         //打印状态
         public string PrintFlg { set; get; }

         //计划单号
         public string PlanID { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

         //加工单位
         public string ProcUnit { set; get; }

         //加工单位
         public string ProcUnits { set; get; }

         //批次号
         public string BthID { set; get; }

         //物资验收入库单号
         public string McIsetInListID { set; get; }

         //检验报告单号
         public string IsetRepID { set; get; }

         //让步区分
         public string GiCls { set; get; }

         //物资ID
         public string PdtID { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //入库日期
         public DateTime? InDate { get; set; }

         //规格型号
         public string PdtSpec { set; get; }

         //加工工艺
         public string TecnProcess { set; get; }

         //合格
         public decimal Qty { set; get; }

         //报废
         public decimal ProScrapQty { set; get; }

         //料废
         public decimal ProMaterscrapQty { set; get; }

         //余料
         public decimal ProOverQty { set; get; }

         //缺料
         public decimal ProLackQty { set; get; }

         //合计
         public decimal ProTotalQty { set; get; }

         //单位
         public string Unit { set; get; }

         //加工单价
         public decimal PrchsUp { set; get; }

         //金额
         public decimal NotaxAmt { set; get; }

         //备注
         public string Rmrs { set; get; }


     }


     public class VM_WipInPrintForSearch
     {
         //打印状态
         public string PrintFlg { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

         //批次号
         public string BthID { set; get; }

         //检验报告单号
         public string IsetRepID { set; get; }

         //让步区分
         public string GiCls { set; get; }

         //物资ID
         public string PdtID { set; get; }

         //送货日期开始
         public DateTime? StartInDate { get; set; }

         //送货日期结束
         public DateTime? EndInDate { get; set; }

         //仓库编码
         public string WhID { set; get; }

     }

     #endregion

    #region 在制品库入库打印预览
     public class VM_WipInPrintIndexForTableShow
     {
         //打印状态
         public string PrintFlg { set; get; }

         //开单日期
         public DateTime? Date { set; get; }

         //入库日期
         public DateTime? InDate { set; get; } 

         //加工单位
         public string ProcUnit { get; set; }

         //加工单位
         public string ProcUnits { get; set; }

         //物资ID
         public string PdtID { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //加工工艺
         public string TecnProcess { set; get; }

         //单位
         public string Unit { set; get; }

         //合格
         public decimal Qty { set; get; }

         //报废
         public decimal ProScrapQty { set; get; }

         //料废
         public decimal ProMaterscrapQty { set; get; }

         //余料
         public decimal ProOverQty { set; get; }

         //缺料
         public decimal ProLackQty { set; get; }

         //合计
         public decimal ProTotalQty { set; get; }

         //备注
         public string Rmrs { set; get; }

         //质检
         public string UID { set; get; }

         //质检
         public string UIDs { set; get; }

         //经办人
         public string UID1 { set; get; }

         //仓管
         public string WhkpID { set; get; }

     }

    #endregion
}
