/*----------------------------------------------------------------
            // Copyright (C) 2013 北京思元软件有限公司
            // 版权所有。 
            //
            // 文件名：ProduceBill.cs
            // 文件功能描述：加工工票实体类
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
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    ///  加工工票表
    ///  代东泽 20131119
    /// </summary>


    [Serializable, Table("PD_PROD_BILL")]
    public  class ProduceBill:Entity
    {
        [Column("BILL_ID")]
        [Key,StringLength(20), DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string BillID {get;set;}

        [Column("EMP_ID")]
        [Required, StringLength(20)]
        public string EmpID { get; set; }

        [Column("DATE")]
        [Required,MaxLength(10)]
        public DateTime? Date { get; set; }

        [Column("CLASS_ID")]
        [StringLength(20)]
        public string ClassID { get; set; }

        [Column("OPTOR_ID")]
        [StringLength(20)]
        public string OptorID { get; set; }

        [Column("DISPCHER_ID")]
        [StringLength(20)]
        public string DispcherID { get; set; }

        [Column("SE_MA_PSN_ID")]
        [StringLength(20)]
        public string SeMaPsnID { get; set; }

        [Column("INC_MA_PSN_ID")]
        [StringLength(20)]
        public string IncMaPsnID { get; set; }

        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnID { get; set; }

        [Column("TEAM_ID")]
        [StringLength(20)]
        public string TeamID { get; set; }

        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }

      


    }
}
