/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcChkRecoDetail.cs
// 文件功能描述：
//          过程检验记录单详细表的实体Model类
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
    /// DB Table Name: QU_PROC_CHK_RECO_DETAIL
    /// DB Table Name(CHS): 过程检验记录单详细表
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_RECO_DETAIL")]
    public class ProcChkRecoDetail : Entity
    {

        /// <summary>
        /// 记录单号
        /// </summary>
        [Column("CHK_RECO_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ChkRecoId { get; set; }

        /// <summary>
        /// 记录单序号
        /// </summary>
        [Column("RECO_NUM", Order = 1)]
        [Key, StringLength(2)]
        public string RecoNum { get; set; }

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
        /// 自检结果
        /// </summary>
        [Column(name: "SEL_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string SelChkRes { get; set; }

        /// <summary>
        /// 首检结果
        /// </summary>
        [Column(name: "FIRST_CHK_", TypeName = "char")]
        [StringLength(1)]
        public string FirstChk { get; set; }

        /// <summary>
        /// 第一次巡检结果
        /// </summary>
        [Column(name: "1ST_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string FirstChkRes { get; set; }

        /// <summary>
        /// 第二次巡检结果
        /// </summary>
        [Column(name: "2ND_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string SecondChkRes { get; set; }

        /// <summary>
        /// 第三次巡检结果
        /// </summary>
        [Column(name: "3RD_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string ThirdChkRes { get; set; }

        /// <summary>
        /// 第四次巡检结果
        /// </summary>
        [Column(name: "4TH_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string FourthChkRes { get; set; }

        /// <summary>
        /// 末检结果
        /// </summary>
        [Column(name: "LAST_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string LastChkRes { get; set; }

    }
}