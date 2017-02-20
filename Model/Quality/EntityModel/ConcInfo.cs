/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ConcInfo.cs
// 文件功能描述：
//          让步信息表的实体Model类
//      
// 修改履历：2013-12-19 汪罡 修改
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
    /// DB Table Name: QU_CONC_INFO
    /// DB Table Name(CHS): 让步信息表
    /// Edit by WangGang @ 2013-12-19 10:57:05 .
    /// </summary>
    [Serializable, Table("QU_CONC_INFO")]
    public class ConcInfo : Entity
    {

        /// <summary>
        /// 序号
        /// </summary>
        [Column("SELF_INC_ID")]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照DB设计书改变长度。
        //[Key, DecimalPrecision(10, 0)]
        [Key, DecimalPrecision(20, 0)]
        public decimal SelfIncId { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        [Column("PART_ID")]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照DB设计书增加非空。
        [Required]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// 让步区分
        /// </summary>
        [Column("GI_CLS")]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照DB设计书增加非空。
        [Required]
        [StringLength(3)]
        public string GiCls { get; set; }

        /// <summary>
        /// 让步描述
        /// </summary>
        [Column("GI_CLS_DESC")]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照DB设计书增加非空。
        [Required]
        [StringLength(400)]
        public string GiClsDesc { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Column("BTH_ID")]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照DB设计书增加非空。
        [Required]
        [StringLength(20)]
        public string BthId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}