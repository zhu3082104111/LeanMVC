/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompCalInfo.cs
// 文件功能描述：
//          公司日历信息表的实体Model类
//      
// 修改履历：2013-12-02 汪罡 修改
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
    /// DB Table Name: BI_COMP_CAL_INFO
    /// DB Table Name(CHS): 公司日历信息表
    /// Edit by WangGang @ 2013-12-02 11:07:32 .
    /// </summary>
    [Serializable, Table("BI_COMP_CAL_INFO")]
    public class CompCalInfo : Entity
    {

        /// <summary>
        /// 年份
        /// </summary>
        [Column(name: "YEAR", TypeName = "char", Order = 0)]
        [Key, StringLength(4)]
        public string Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        [Column(name: "MONTH", TypeName = "char", Order = 1)]
        [Key, StringLength(2)]
        public string Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [Column(name: "DAY", TypeName = "char", Order = 2)]
        [Key, StringLength(2)]
        public string Day { get; set; }

        /// <summary>
        /// 周数
        /// </summary>
        [Column("WEK_NUM")]
        [DecimalPrecision(2, 0)]
        public decimal WekNum { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        [Column(name: "THE_WEEK", TypeName = "char")]
        [StringLength(1)]
        public string TheWeek { get; set; }

        /// <summary>
        /// 休息日区分
        /// </summary>
        [Column(name: "REAT_FLG", TypeName = "char")]
        [StringLength(1)]
        public string ReatFlg { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}