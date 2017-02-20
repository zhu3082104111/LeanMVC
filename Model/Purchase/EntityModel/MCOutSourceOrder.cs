/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCOutSourceOrder.cs
// 文件功能描述：
//          外购单表的实体Model类
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
    /// 对应DB中文表名：外购单表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_OUTSOURCE_ORDER")]
    public class MCOutSourceOrder : Entity
    {
        //外购单号
        [Key, StringLength(20), Column("OUT_ODR_ID")]
        public string OutOrderID { get; set; }

        // 紧急状态
        [Required, StringLength(1), Column("OUT_URGENT_STS", TypeName = "char")]
        public string UrgentStatus { get; set; }

        // 外购部门ID
        [Required, StringLength(20), Column("OUT_DEPART_ID")]
        public string DepartmentID { get; set; }

        // 外购单位ID
        [Required, StringLength(20), Column("OUT_COMP_ID")]
        public string OutCompanyID { get; set; }

        // 外购单状态
        [Required, StringLength(1), Column("OUT_ODR_STS", TypeName = "char")]
        public string OutOrderStatus { get; set; }

        // 批准人
        [StringLength(20), Column("APROV_STF")]
        public string ApproveUID { get; set; }

        // 批准时间
        [Column("APROV_DATE")]
        public DateTime? ApproveDate { get; set; }

        // 审核人
        [StringLength(20), Column("VRF_STF")]
        public string VerifyUID { get; set; }

        // 审核时间
        [Column("VRF_DATE")]
        public DateTime? VerifyDate { get; set; }

        // 编制人
        [StringLength(20), Column("ETB_STF")]
        public string EstablishUID { get; set; }

        // 编制时间
        [Column("ETB_DATE")]
        public DateTime? EstablishDate { get; set; }

        // 签收人
        [StringLength(20), Column("RECR_STF")]
        public string SignUID { get; set; }

        // 签收时间
        [Column("RECR_DATE")]
        public DateTime? SignDate { get; set; }

        // 外购单区分
        [Required, StringLength(1), Column("OUT_ODR_FLG", TypeName = "char")]
        public string OutOrderFlg { get; set; }

        // 备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { get; set; }

        // FAX
        [StringLength(20), Column("FAX_NO")]
        public string FaxNo { get; set; }

        // 打印区分
        [Required, StringLength(1), Column("PRINT_FLG", TypeName = "char")]
        public string PrintFlag { get; set; }
    }
}
