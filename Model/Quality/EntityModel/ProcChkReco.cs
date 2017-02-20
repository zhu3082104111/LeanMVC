/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcChkReco.cs
// 文件功能描述：
//          过程检验记录单表的实体Model类
//      
// 修改履历：2014-01-09 汪罡 修改
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
    /// DB Table Name: QU_PROC_CHK_RECO
    /// DB Table Name(CHS): 过程检验记录单表
    /// Edit by WangGang @ 2014-01-09 16:50:03 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_RECO")]
    public class ProcChkReco : Entity
    {

        /// <summary>
        /// 记录单号
        /// </summary>
        [Column("CHK_RECO_ID")]
        [Key, StringLength(20)]
        public string ChkRecoId { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        [Column("PART_ID")]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [Column("PART_NAME")]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// 零件型号
        /// </summary>
        [Column("PART_MOD")]
        [StringLength(200)]
        public string PartMod { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [Column("EQUIP_NAME")]
        [StringLength(200)]
        public string EquipName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Column("EQUIP_NO")]
        [StringLength(20)]
        public string EquipNo { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("PROC_NAME")]
        [StringLength(200)]
        public string ProcName { get; set; }

        /// <summary>
        /// 工序顺序号
        /// </summary>
        [Column("PROC_SEQU_NO")]
        [StringLength(5)]
        public string ProcSequNo { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [Column("SUB_PROC_NAME")]
        [StringLength(200)]
        public string SubProcName { get; set; }

        /// <summary>
        /// 定额编号
        /// </summary>
        [Column("QUOT_NUM")]
        [StringLength(5)]
        public string QuotNum { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        [Column("OPERA_ID")]
        [StringLength(20)]
        public string OperaId { get; set; }

        /// <summary>
        /// 班组
        /// </summary>
        [Column("TEAM_ID")]
        [StringLength(20)]
        public string TeamId { get; set; }

        /// <summary>
        /// 检验日期
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// 自检结果时间
        /// </summary>
        [Column("SEL_CHK_TIME")]
        [StringLength(6)]
        public string SelChkTime { get; set; }

        /// <summary>
        /// 自检结果签名
        /// </summary>
        [Column("SEL_CHK_SIGN")]
        [StringLength(50)]
        public string SelChkSign { get; set; }

        /// <summary>
        /// 首检结果时间
        /// </summary>
        [Column("FIRST_CHK_TIME")]
        [StringLength(6)]
        public string FirstChkTime { get; set; }

        /// <summary>
        /// 首检结果签名
        /// </summary>
        [Column("FIRST_CHK_SIGN")]
        [StringLength(50)]
        public string FirstChkSign { get; set; }

        /// <summary>
        /// 第一次巡检时间
        /// </summary>
        [Column("1ST_CHK_TIME")]
        [StringLength(6)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _1stChkTime { get; set; }
        public string FirstCheckTime { get; set; }

        /// <summary>
        /// 第一次巡检签名
        /// </summary>
        [Column("1ST_CHK_SIGN")]
        [StringLength(50)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _1stChkSign { get; set; }
        public string FirstCheckSign { get; set; }

        /// <summary>
        /// 第二次巡检时间
        /// </summary>
        [Column("2ND_CHK_TIME")]
        [StringLength(6)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _2ndChkTime { get; set; }
        public string SecondCheckTime { get; set; }

        /// <summary>
        /// 第二次巡检签名
        /// </summary>
        [Column("2ND_CHK_SIGN")]
        [StringLength(50)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _2ndChkSign { get; set; }
        public string SecondCheckSign { get; set; }

        /// <summary>
        /// 第三次巡检时间
        /// </summary>
        [Column("3RD_CHK_TIME")]
        [StringLength(6)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _3rdChkTime { get; set; }
        public string ThirdCheckTime { get; set; }

        /// <summary>
        /// 第三次巡检签名
        /// </summary>
        [Column("3RD_CHK_SIGN")]
        [StringLength(50)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _3rdChkSign { get; set; }
        public string ThirdCheckSign { get; set; }

        /// <summary>
        /// 第四次巡检时间
        /// </summary>
        [Column("4TH_CHK_TIME")]
        [StringLength(6)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _4thChkTime { get; set; }
        public string FourthCheckTime { get; set; }

        /// <summary>
        /// 第四次巡检签名
        /// </summary>
        [Column("4TH_CHK_SIGN")]
        [StringLength(50)]
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：命名存在不规范的情况，可能导致不稳定，故作修改。
        //public string _4thChkSign { get; set; }
        public string FourthCheckSign { get; set; }

        /// <summary>
        /// 末检时间
        /// </summary>
        [Column("LAST_CHK_TIME")]
        [StringLength(6)]
        public string LastChkTime { get; set; }

        /// <summary>
        /// 末检签名
        /// </summary>
        [Column("LAST_CHK_SIGN")]
        [StringLength(50)]
        public string LastChkSign { get; set; }

        /// <summary>
        /// 处理意见建议
        /// </summary>
        [Column("OPINION")]
        [StringLength(200)]
        public string Opinion { get; set; }

        /// <summary>
        /// 加工工票ID
        /// </summary>
        [Column("BILL_ID")]
        [StringLength(20)]
        public string BillId { get; set; }

        /// <summary>
        /// 加工工票行号
        /// </summary>
        [Column("BI_LINE_NO")]
        [StringLength(2)]
        public string BiLineNo { get; set; }

        /// <summary>
        /// 质检员ID
        /// </summary>
        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnId { get; set; }

        /// <summary>
        /// 检验单号
        /// </summary>
        [Column("CHK_LIST_ID")]
        [StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}