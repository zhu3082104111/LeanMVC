/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SemOutStore.cs
// 文件功能描述：
//          半成品库（待出库一览、出库登录、出库履历一览、半成品库出库打印预览）画面Model
//          即半成品库出库相关画面
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

namespace Model
{
    #region 半成品库待出库一览

    public class VM_SemOutStoreForSearch
    {
        /// <summary>
        /// 领料单号
        /// </summary>
        public string MaterReqNO { set; get; }

        /// <summary>
        /// 请领单位ID
        /// </summary>
        public string DeptID { set; get; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterielID { get; set; }

        /// <summary>
        /// 申请领料开始日期
        /// </summary>
        public DateTime? StartRequestDate { get; set; }

        /// <summary>
        /// 申请领料结束日期
        /// </summary>
        public DateTime? EndRequestDate { get; set; }
    } //end VM_SemOutStoreForSearch

    public class VM_SemOutStoreForTableShow
    {
        /// <summary>
        /// 领料单号
        /// </summary>
        public string MaterReqNO { set; get; }

        /// <summary>
        /// 领料单详细号
        /// </summary>
        public string MaterReqDetailNO { set; get; }

        /// <summary>
        /// 请领单位ID
        /// </summary>
        public string DeptID { set; get; }

        /// <summary>
        /// 请领单位名称
        /// </summary>
        public string DeptName { set; get; }

        /// <summary>
        /// 请领数量
        /// </summary>
        public decimal AppoQuantity { set; get; }

        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterielID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterielName { get; set; }

        /// <summary>
        /// 申请领料日期
        /// </summary>
        public DateTime? RequestDate { get; set; }
    } //end VM_SemOutStoreForTableShow

    #endregion


    #region 半成品库出库登录
    public class VM_SemOutLoginStoreForTableShow
    {
        //领料单号
        public string PickListID { set; get; }

        //加工产品出库单
        public string SaeetID { set; get; }

        //请领单位
        public string CallinUnitID { set; get; }

        //物料编号
        public string MaterielID { get; set; }

        //物料名称
        public string MaterielName { get; set; }

        //加工工艺
        public string TecnProcess { set; get; }

        //请领数量
        public decimal CallinQty { set; get; }

        //数量
        public decimal Qty { set; get; }

        //单位
        public string Unit { set; get; }

        //出库单价
        public decimal SellPrc { set; get; }

        //金额
        public decimal NotaxAmt { set; get; }

        //出库日期
        public DateTime? OutDate { set; get; }

        //备注
        public string Rmrs { set; get; }

        ////登录标志
        //public string SemLoginFlg { set; get; }

        //批次号
        public string BthID { set; get; }

        //规格型号
        public string PdtSpec { set; get; }

        //让步区分
        public string GiCls { set; get; }

        //领料单详细号
        public string MaterReqDetailNo { set; get; }

        //外协、外购、自生产区分
        public string OsSupProFlg { set; get; }

        //行索引
        public string RowIndex { set; get; }
    }


    public class VM_SemOutLoginStoreForSearch
    {
        //出库区分
        public string OutMvCls { set; get; }

    }
    #endregion


    #region 半成品库出库批次选择
    public class VM_SemOutBthForTableShow
    {
        //数量
        public decimal Qty { set; get; }

        //批次号
        public string BthID { set; get; }

        //让步区分
        public string GiCls { set; get; }

        //规格型号
        public string PdtSpec { set; get; }

        //可用数量
        public decimal UseableQty { set; get; }

        //单价
        public decimal SellPrc { set; get; }

        //估价
        public decimal ValuatUp { set; get; }

        //使用数量
        public decimal UserQty { set; get; }

        //入库日期
        public DateTime? InDate { set; get; }

        //单价标识
        public string SemLoginPriceFlg { set; get; }

    }
    #endregion


    #region 半成品库出库履历一览
    public class VM_SemOutRecordStoreForTableShow
    {
        //领料单号
        public string PickListID { set; get; }

        //领料单类型
        public string PickListTypeID { set; get; }

        //领料单详细号
        public string PickListDetNo { set; get; }

        //加工产品出库单
        public string SaeetID { set; get; }

        //请领单位
        public string CallinUnitID { set; get; }

        //请领单位
        public string CallinUnitIDs { set; get; }

        //物料ID
        public string MaterielID { get; set; }

        //物料名称
        public string MaterielName { get; set; }

        //批次号
        public string BthID { set; get; }

        //让步区分
        public string GiCls { set; get; }

        //规格型号
        public string PdtSpec { set; get; }

        //加工工艺
        public string TecnProcess { set; get; }

        //数量
        public decimal Qty { set; get; }

        //单位
        public string Unit { set; get; }

        //单价
        public decimal PrchsUp { set; get; }

        //出库价格
        public decimal SellPrc { set; get; }

        //金额
        public decimal NotaxAmt { set; get; }

        //出库日期
        public DateTime? OutDate { set; get; }

        //备注
        public string Rmrs { set; get; }

    }


    public class VM_SemOutRecordStoreForSearch
    {
        //领料单号
        public string PickListID { set; get; }

        //加工产品出库单
        public string SaeetID { set; get; }

        //仓库编码
        public string WhID { set; get; }

        //请领单位
        public string CallinUnitID { set; get; }

        //物料名称
        public string MaterielID { get; set; }

        //批次号
        public string BthID { set; get; }

        //让步区分
        public string GiCls { set; get; }

        //规格型号
        public string PdtSpec { set; get; }

        //出库日期开始
        public DateTime? StartOutDate { get; set; }

        //出库日期结束
        public DateTime? EndOutDate { get; set; }

    }

    #endregion


    #region 半成品库出库打印选择

    public class VM_SemOutPrintForSearch
    {
        /// <summary>
        /// 领料单号
        /// </summary>
        public string PickListID { set; get; }

        /// <summary>
        /// 加工产品出库单
        /// </summary>
        public string TecnProductOutID { set; get; }

        /// <summary>
        /// 请领单位ID
        /// </summary>
        public string DeptID { set; get; }

        /// <summary>
        /// 物料ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 出库日期开始
        /// </summary>
        public DateTime? StartOutDate { get; set; }

        /// <summary>
        /// 出库日期结束
        /// </summary>
        public DateTime? EndOutDate { get; set; }

        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintFlg { set; get; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhID { set; get; }
    } //end VM_SemOutPrintForSearch

    public class VM_SemOutPrintForTableShow
    {
        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintFlg { set; get; }

        /// <summary>
        /// 打印状态名称
        /// </summary>
        public string PrintFlgName { set; get; }

        /// <summary>
        /// 领料单号
        /// </summary>
        public string PickListID { set; get; }

        /// <summary>
        /// 加工产品出库单号
        /// </summary>
        public string TecnProductOutID { set; get; }

        /// <summary>
        /// 领料单详细号
        /// </summary>
        public string PickListDetailNO { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchID { set; get; }

        /// <summary>
        /// 请领单位ID
        /// </summary>
        public string DeptID { set; get; }

        /// <summary>
        /// 请领单位名称
        /// </summary>
        public string DeptName { set; get; }

        /// <summary>
        /// 供货商名称
        /// </summary>
        public string CompName { set; get; }

        /// <summary>
        /// 物料ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 加工工艺ID
        /// </summary>
        public string TecnProcessID { set; get; }

        /// <summary>
        /// 加工工艺名称
        /// </summary>
        public string TecnProcessName { set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { set; get; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitID { set; get; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { set; get; }

        /// <summary>
        /// 出库单价
        /// </summary>
        public decimal PrchsUp { set; get; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal NotaxAmt { set; get; }

        /// <summary>
        /// 出库日期
        /// </summary>
        public DateTime? OutDate { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { set; get; }
    } //end VM_SemOutPrintForTableShow

    #endregion


    #region 半成品库出库打印预览

    public class VM_SemOutPrintIndexForInfoShow
    {
        /// <summary>
        /// 加工产品出库单号
        /// </summary>
        public string TecnProductOutID { set; get; }

        /// <summary>
        /// 调入单位名称
        /// </summary>
        public string CallinUnitName { set; get; }

        /// <summary>
        /// 领料人名称
        /// </summary>
        public string MaterHandlerName { get; set; }

        /// <summary>
        /// 制单人名称
        /// </summary>
        public string WareHouseKpName { get; set; }
    } //end VM_SemOutPrintIndexForInfoShow

    public class VM_SemOutPrintIndexForTableShow
    {
        /// <summary>
        /// 加工产品出库单据号
        /// </summary>
        public string TecnProductOutID { set; get; }

        /// <summary>
        /// 领料单详细号
        /// </summary>
        public string PickListDetailNO { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchID { set; get; }

        /// <summary>
        /// 打印FLAG
        /// </summary>
        public string PrintFlg { set; get; }

        /// <summary>
        /// 打印FLAG名称
        /// </summary>
        public string PrintFlgName { set; get; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string ProductID { set; get; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string ProductName { set; get; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpec { set; get; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitID { set; get; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { set; get; }

        /// <summary>
        /// 请领数量
        /// </summary>
        public decimal AppoQuantity { set; get; }

        /// <summary>
        /// 实领数量
        /// </summary>
        public decimal ReceQuantity { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { set; get; }
    } //end VM_SemOutPrintIndexForInfoShow

    #endregion

} //end Model
