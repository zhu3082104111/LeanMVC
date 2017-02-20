// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：GiMaterial.cs
// 文件功能描述：让步仓库表实体类
// 
// 创建标识：
//
// 修改标识：代东泽 20131226
// 修改描述：添加注释
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// 让步仓库实体
    /// 代东泽 20131226 添加注释
    /// </summary>
    [Serializable, Table("MC_WH_GI_MATERIAL")]
    public class GiMaterial : Entity
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key, Required, StringLength(8), Column("WH_ID",Order = 0)]
        public string WareHouseID { set; get; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, Required, StringLength(20), Column("PDT_ID", Order = 1)]
        public string ProductID { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Key, StringLength(20), Column("BTH_ID", Order = 2)]
        public string BatchID { set; get; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpec { set; get; }

        

        /// <summary>
        /// 被预约数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ALCT_QTY")]
        public decimal AlctQuantity { set; get; }

        /// <summary>
        /// 可用在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("USEABLE_QTY")]
        public decimal UserableQuantity { set; get; }

        /// <summary>
        /// 实际在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("CURRENT_QTY")]
        public decimal CurrentQuantity { set; get; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("DIS_QTY")]
        public decimal DiscardQuantity { set; get; }

        /// <summary>
        /// 总价
        /// </summary>
        [DecimalPrecision(10, 2), Column("TOTAL_AMT")]
        public decimal TotalAmt { set; get; }

        /// <summary>
        /// 估价总价
        /// </summary>
        [DecimalPrecision(10, 2), Column("TOTAL_VALUAT_UP")]
        public decimal TotalValuatUp { set; get; }

        //最终出库日
        [Column("LAST_WHOUT_YMD")]
        public DateTime? LastWhOutYMD { set; get; }

        //最终入库日
        [Column("LAST_WHIN_YMD")]
        public DateTime? LastWhInYMD { set; get; }

        /// <summary>
        /// 让步区分
        /// </summary>
        [Required, StringLength(3), Column("GI_CLS")]
        public string GiCls { set; get; }

    }
}
