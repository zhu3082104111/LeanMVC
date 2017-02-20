/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCSupplierOrderDetail.cs
// 文件功能描述：
//          外协加工调度单详细表的实体Model类
//      
// 修改履历：2013/11/19 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 对应DB中文表名：外协加工调度单详细表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_SUPPLIER_ORDER_DTL")]
    public class MCSupplierOrderDetail : Entity
    {
        //外协加工调度单
        [Key, StringLength(20), Column("SUP_ODR_ID", Order = 0)]
        public string SupOrderID { set; get; }

        //客户订单号
        [Key, StringLength(20), Column("CLN_ODR_ID", Order = 1)]
        public string CustomerOrderID { set; get; }

        //客户订单明细号
        [Key, StringLength(2), Column("CLN_ODR_DTL_ID", Order = 2)]
        public string CustomerOrderDetailID { set; get; }

        //产品零件ID
        [Key, StringLength(20), Column("PROD_PART_ID", Order = 3)]
        public string ProductPartID { set; get; }

        //产品ID
        [StringLength(15), Column("PROD_ID")]
        public string ProductID { set; get; }

        //材料和规格要求
        [StringLength(200), Column("MATL_SPEC_REQ")]
        public string MaterialsSpecReq { get; set; }

       //加工工艺
        [Required, StringLength(20), Column("PD_PROC_ID")]
        public string PdProcID{set;get;}

        //仓库编码
        [Required, StringLength(8), Column("WH_ID")]
        public string WarehouseID { set; get; }

        //单价
        [Required, DecimalPrecision(10, 2), Column("PRCHS_UP")]
        public decimal UnitPrice { set; get; }

        //估价
        [Required, DecimalPrecision(10, 2), Column("EVALUATE")]
        public decimal Evaluate { set; get; }

        //单据要求数量
        [Required, DecimalPrecision(10, 2), Column("REQ_QTY")]
        public decimal RequestQuantity { set; get; }

        //实际入库数量
        [Required, DecimalPrecision(10, 2), Column("ACT_QTY")]
        public decimal ActualQuantity { set; get; }

        //其他数量
        [Required, DecimalPrecision(10, 2), Column("OTHER_QTY")]
        public decimal OtherQuantity { get; set; }

        //完成状态
        [Required, StringLength(1), Column("COMPLETE_STS", TypeName = "char")]
        public string CompleteStatus { get; set; }

        //交货日期
        [Required, Column("DLY_DATE")]
        public DateTime DeliveryDate { set; get; }

        //RMRS备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { set; get; }
    }
}
