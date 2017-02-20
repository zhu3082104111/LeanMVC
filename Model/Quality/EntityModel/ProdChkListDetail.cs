/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdChkListDetail.cs
// 文件功能描述：
//          成品检验单详细表的实体Model类
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
    /// DB Table Name: QU_PROD_CHK_LIST_DETAIL
    /// DB Table Name(CHS): 成品检验单详细表
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PROD_CHK_LIST_DETAIL")]
    public class ProdChkListDetail : Entity
    {

        /// <summary>
        /// 检验单号
        /// </summary>
        [Column("CHK_LIST_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// 检验单序号
        /// </summary>
        [Column("LIST_NUM", Order = 1)]
        [Key, StringLength(2)]
        public string ListNum { get; set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        [Column("CHK_ITEM")]
        [StringLength(200)]
        public string ChkItem { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        [Column("CHK_STAND")]
        [StringLength(200)]
        public string ChkStand { get; set; }

        /// <summary>
        /// 实测结果1
        /// </summary>
        [Column(name: "CHK_RES_1", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes1 { get; set; }

        /// <summary>
        /// 实测结果2
        /// </summary>
        [Column(name: "CHK_RES_2", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes2 { get; set; }

        /// <summary>
        /// 实测结果3
        /// </summary>
        [Column(name: "CHK_RES_3", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes3 { get; set; }

        /// <summary>
        /// 实测结果4
        /// </summary>
        [Column(name: "CHK_RES_4", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes4 { get; set; }

        /// <summary>
        /// 实测结果5
        /// </summary>
        [Column(name: "CHK_RES_5", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes5 { get; set; }

    }
}