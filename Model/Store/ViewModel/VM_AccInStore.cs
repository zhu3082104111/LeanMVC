/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_AccInStore.cs
// 文件功能描述：
//          附件库（待入库一览、入库登录、入库履历一览、附件库入库打印预览）画面Model
//          即附件库入库相关画面
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
    #region 附件库待入库一览
    public class VM_AccInStoreForTableShow
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


      public class VM_AccInStoreForSearch
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


     #region 附件库入库登录
     public class VM_AccInLoginStoreForTableShow
     {      
            //采购订单号
            public string PrhaOdrID { set; get; }

            //送货单号
            public string DeliveryOrderID { get; set; }

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

            //数量
            public decimal Qty { set; get; }

            //单位
            public string Unit { set; get; }

            //单价
            public decimal PrchsUp { set; get; }

            //估价
            public decimal ValuatUp { set; get; }

            //金额
            public decimal NotaxAmt { set; get; }

            //入库日期
            public DateTime? InDate { set; get; }

            //备注
            public string Rmrs { set; get; }

            //区分登录与未登录数据
            public string AccLoginFlg { set; get; }

           //区分单价、估价
            public string AccLoginPriceFlg { set; get; }
        
         
        }


     public class VM_AccInLoginStoreForSearch
     {
         //入库区分
         public string InMvCls { set; get; }

     }
     #endregion


      #region 附件库入库履历一览
     public class VM_AccInRecordStoreForTableShow
     {
         //采购订单号
         public string PrhaOdrID { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

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

         //数量
         public decimal Qty { set; get; }

         //单位
         public string Unit { set; get; }

         //单价
         public decimal PrchsUp { set; get; }

         //估价
         public decimal ValuatUp { set; get; }

         //金额
         public decimal NotaxAmt { set; get; }

         //入库日期
         public DateTime? InDate { set; get; }

         //备注
         public string Rmrs { set; get; }

         //区分单价、估价
         public string AccInRecordPriceFlg { set; get; }
     
     }


     public class VM_AccInRecordStoreForSearch
     {
         //采购订单号
         public string PrhaOdrID { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

         //物资验收入库单号
         public string McIsetInListID { set; get; }

         //检验报告单号
         public string IsetRepID { set; get; }

         //批次号
         public string BthID { set; get; }

         //让步区分
         public string GiCls { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //规格型号
         public string PdtSpec { set; get; }

         //入库移动区分
         public string InMvCls { set; get; }

         //入库日期开始
         public DateTime? StartInDate { get; set; }

         //入库日期结束
         public DateTime? EndInDate { get; set; }



     }

      #endregion


     #region 附件库入库打印选择
     public class VM_AccInPrintForTableShow
     {
         //打印状态
         public string PrintFlg { set; get; }

         //采购订单号
         public string PrhaOdrID { set; get; }

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

         //物资ID
         public string PdtID { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //规格型号
         public string PdtSpec { set; get; }

         //数量
         public decimal Qty { set; get; }

         //单位
         public string Unit { set; get; }

         //单价
         public decimal PrchsUp { set; get; }

         //金额
         public decimal NotaxAmt { set; get; }

         //入库日期
         public DateTime? InDate { set; get; }

         //备注
         public string Rmrs { set; get; }

     }


     public class VM_AccInPrintForSearch
     {
         //采购订单号
         public string PrhaOdrID { set; get; }

         //批次号
         public string BthID { set; get; }

         //检验报告单号
         public string IsetRepID { set; get; }

         //物资ID
         public string PdtID { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //让步区分
         public string GiCls { set; get; }

         //送货单号
         public string DeliveryOrderID { get; set; }

         //送货开始日期
         public DateTime? StartDeliveryDate { get; set; }

         //送货结束日期
         public DateTime? EndDeliveryDate { get; set; }

         //打印状态
         public string PrintFlg { set; get; }

         //仓库编码
         public string WhID { set; get; }


     }

     #endregion

    #region 附件库入库打印预览
     public class VM_AccInPrintIndexForTableShow
     {
         //单据日期
         public DateTime? Date { set; get; } 

         //单据编号
         public string PrhaOdrID { set; get; }

         //打印状态
         public string PrintFlg { set; get; }

         //送货单位
         public string DeliveryCompanyID { get; set; }

         //采购部门
         public string DepartID { set; get; }

         //所属部门
         public string DepartName { set; get; }

         //物资ID
         public string PdtID { set; get; }

         //物资名称
         public string PdtName { set; get; }

         //规格型号
         public string PdtSpec { set; get; }

         //数量
         public decimal Qty { set; get; }

         //单位
         public string Unit { set; get; }

         //单价
         public decimal PrchsUp { set; get; }

         //金额
         public decimal NotaxAmt { set; get; }

         //备注
         public string Rmrs { set; get; }

         //质检
         public string UID { set; get; }

         //采购
         public string UID1 { set; get; }

         //仓管
         public string WhkpID { set; get; }

         //制单人
         public string ProID { set; get; }

         //入库日期
         public DateTime? InDate { set; get; } 

     }

    #endregion
}
