/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SemInStore.cs
// 文件功能描述：
//          半成品库（待入库一览、入库登录、入库履历一览、半成品库入库打印预览）画面Model
//          即半成品库入库相关画面
//
// 创建标识：2013/11/26 汪 新建
//
// 修改描述：添加注释和字段初始化赋值
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    #region 半成品库待入库一览
    public class VM_SemInStoreForTableShow
    {
        //送货单号
        public string DeliveryOrderID { get; set; }

        //检验报告单号
        public string IsetRepID { set; get; }

        //物料名称
        public string PName { get; set; }

        //质检日期
        public DateTime? QCDate { get; set; }
    }


    public class VM_SemInStoreForSearch
    {
        //检验报告单号
        public string IsetRepID { set; get; }

        //送货单号
        public string DeliveryOrderID { get; set; }

        //物料名称
        public string PName { get; set; }

        //质检开始日期
        [EntityProperty("ChkDt", PropertyOperator.GT)]
        public DateTime? StartDt { get; set; }

        //质检结束日期
        [EntityProperty("ChkDt", PropertyOperator.LE)]
        public DateTime? EndDt { get; set; }
    }
    #endregion


    #region 半成品库入库登录
    public class VM_SemInLoginStoreForTableShow
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

        //入库日期
        public DateTime? InDate { set; get; }

        //备注
        public string Rmrs { set; get; }

        //区分登录与未登录数据
        public string SemLoginFlg { set; get; }

        //区分单价、估价
        public string AccLoginPriceFlg { set; get; }

        //供货商ID
        public string CompID { get; set; }

        //物资ID
        public string PdtID { set; get; }

        //单价区分
        public string SemLoginPriceFlg { set; get; }

        //外协、自生产区分
        public string OsSupProFlg { set; get; }
    }



    public class VM_SemInLoginStoreForSearch
    {
        //入库区分
        public string InMvCls { set; get; }

    }
    #endregion


    #region 半成品库入库履历一览
    public class VM_SemInRecordStoreForTableShow
    {

        //仓库编码
        public string WhID { set; get; }
        
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
        public string SemInRecordPriceFlg { set; get; }

        //外协、外购、自生产区分
        public string OsSupProFlg { set; get; }
     

    }


    public class VM_SemInRecordStoreForSearch
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

        //入库日期开始
        public DateTime? StartInDate { get; set; }

        //入库日期结束
        public DateTime? EndInDate { get; set; }

        //仓库编码
        public string WhID { set; get; }

        //规格型号
        public string PdtSpec { set; get; }

    }

    #endregion


    #region 半成品库入库打印选择
    public class VM_SemInPrintForTableShow
    {
        //打印状态
        public string PrintFlg { set; get; }

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

        //入库日期
        public DateTime? InDate { get; set; }


        //仓库编码
        public string WhID { set; get; }

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

        //物资ID
        public string PdtID { set; get; }

    }


    public class VM_SemInPrintForSearch
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

        //物资名称
        public string PdtName { set; get; }

        //送货日期开始
        public DateTime? StartInDate { get; set; }

        //送货日期结束
        public DateTime? EndInDate { get; set; }

        //仓库编码
        public string WhID { set; get; }

    }

    #endregion

    #region 半成品库入库打印预览
    public class VM_SemInPrintIndexForTableShow
    {
        //打印状态
        public string PrintFlg { set; get; }

        //开单日期
        public DateTime? Date { set; get; }

        //入库日期
        public DateTime? InDate { set; get; } 

        //加工单位
        public string DeliveryCompanyID { get; set; }

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

        //经办人
        public string UID1 { set; get; }

        //仓管
        public string WhkpID { set; get; }

    }

    #endregion
}
