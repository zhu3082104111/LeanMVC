// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemBill.cs
// 文件功能描述：总装工票实体类
// 
// 创建标识：代东泽 20131119
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using System.Data.Entity;
using System.Threading.Tasks;
namespace Model
{

    /// <summary>
    ///  总装工票实体类
    ///  代东泽 20131119
    /// </summary>
    [Table("PD_ASSEM_BILL")]
    [Serializable]
    public class AssemBill:Entity
    {
        [Key, StringLength(20), Column("ASS_BILL_ID")]
        public string AssemBillID { get; set; }//总装工票ID

        [Required, StringLength(20), Column("PRODUCT_ID")]
        public string ProductID { get; set; }//产品ID

        [Required, StringLength(20), Column("ASS_PROC_ID")]
        public string AssemProcessID { get; set; }//总装工序ID

        [StringLength(1), Column(name: "END_FLG", TypeName = "char")]
        public string EndFlag { get; set; }//是否完结区分

        [StringLength(1), Column(name: "CHK_RESU", TypeName = "char")]
        public string CheckResult { get; set; }//成品检验结果

        [StringLength(20), Column("DISPCHER_ID")]
        public string DispatcherID { get; set; }//调度员ID

        [StringLength(20), Column("CHK_PSN_ID")]
        public string CheckerID { get; set; }//检验员ID

        [StringLength(20), Column("TEAM_LEADR_ID")]
        public string TeamLeaderID { get; set; }//组长ID


        //备注
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }

    }
}
