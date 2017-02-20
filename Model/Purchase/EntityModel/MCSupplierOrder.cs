/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCSupplierOrder.cs
// 文件功能描述：
//          外协加工调度单表的实体Model类
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
    /// 对应DB中文表名：外协加工调度单表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_SUPPLIER_ORDER")]
    public class MCSupplierOrder : Entity
    {
        //外协加工调度单号
        [Key, StringLength(20), Column("SUP_ODR_ID")]
        public string SupOrderID{set;get;}

        //ORDER_FLG 种类
        [Required, StringLength(1), Column("ORDER_TYPE", TypeName = "char")]
        public string OrderType { set; get; }

        //紧急状态
        [Required, StringLength(1), Column("SUP_URGENT_STS", TypeName = "char")]
        public string UrgentStatus { set; get; }
        
        //外协单状态
        [Required, StringLength(1), Column("SUP_STS", TypeName = "char")]
        public string SupOrderStatus { set; get; }

        //外协单所属部门
        [Required, StringLength(20), Column("SUP_DEPART_ID")]
        public string DepartmentID { set; get; }

        //调入部门
        [StringLength(20), Column("IN_COMP_ID")]
        public string InCompanyID { set; get; }

        //调出部门
        [StringLength(20), Column("OUT_COMP_ID")]
        public string OutCompanyID { set; get; }

        //生产主管
        [StringLength(20), Column("PD_MNGR_ID")]
        public string PrdMngrUID { set; get; }

        //生产主管签字时间
        [Column("PD_MNGR_SIGN_DATE")]
        public DateTime? PrdMngrSignDate { set; get; }

        //制单人
        [StringLength(20), Column("MKR_ID")]
        public string MarkUID { set; get; }

        //制单人签字时间
        [Column("MKR_SIGN_DATE")]
        public DateTime?  MarkSignDate { set; get; }

        //经办人
        [StringLength(20), Column("OPTR_ID")]
        public string OptrUID { set; get; }

        //经办人签字时间
        [Column("OPTR_SIGN_DATE")]
        public DateTime? OptrSignDate { set; get; }

        // 打印区分
        [Required, StringLength(1), Column("PRINT_FLG", TypeName = "char")]
        public string PrintFlag { get; set; }
    }
}
