/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCDeliveryOrder.cs
// 文件功能描述：
//          送货单表的实体Model类
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
    /// 对应DB中文表名：送货单表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_DELIVERY_ORDER")]
    public class MCDeliveryOrder : Entity
    {
        // 送货单号
        [Key, StringLength(20), Column("DLY_ODR_ID")]
        public string DeliveryOrderID { get; set; }

        // 送货单区分
        [Required, StringLength(1), Column("DLY_ODR_FLG", TypeName="char")]
        public string DeliveryOrderType { get; set; }

        // 订单号
        [Required, StringLength(20), Column("ODR_ID")]
        public string OrderNo { get; set; }

        // 生产单元ID
        [Required, StringLength(20), Column("DEPART_ID")]
        public string DepartID { get; set; }

        // 送货单位
        [Required, StringLength(20), Column("DLY_COMP_ID")]
        public string DeliveryCompanyID { get; set; }

        // 批次号
        [Required, StringLength(20), Column("BTH_ID")]
        public string BatchID { get; set; }

        // 送货人
        [Required, StringLength(20), Column("DLY_STF")]
        public string DeliveryUID { get; set; }

        // 送货日期
        [Required, StringLength(20), Column("DLY_DATE")]
        public DateTime DeliveryDate { get; set; }

        // 联系电话
        [StringLength(20), Column("TEL")]
        public string TelNo { get; set; }

        // 送货单状态
        [Required, StringLength(1), Column("DLY_ODR_STS")]
        public string DeliveryOrderStatus { get; set; }

        // 审核人员
        [StringLength(20), Column("VRF_STF")]
        public string VerifyUID { get; set; }

        // 审核时间
        [Column("VRF_DATE")]
        public DateTime? VerifyDate { get; set; }

        // 收货单位检验员
        [StringLength(20), Column("ISPC_ID")]
        public string IspcUID { get; set; }

        // 收货单位检验时间
        [Column("ISPC_DATE")]
        public DateTime? IspcDate { get; set; }

        // 收货单位核价员
        [StringLength(20), Column("PRCC_ID")]
        public string PrccUID { get; set; }

        // 收货单位核价时间
        [Column("PRCC_DATE")]
        public DateTime? PrccDate { get; set; }

        // 收货单位仓管员
        [StringLength(20), Column("WHKP_ID")]
        public string WhkpUID { get; set; }

        // 收货单位仓管时间
        [Column("WHKP_DATE")]
        public DateTime? WhkpDate { get; set; }

        // 打印区分
        [Required, StringLength(1), Column("PRINT_FLG", TypeName = "char")]
        public string PrintFlag { get; set; }
    }
}
