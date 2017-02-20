// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessDelivery.cs
// 文件功能描述：加工送货单实体类
// 
// 创建标识：王瑞 20131121
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
    [Table("PD_PROC_DELIV")]
    [Serializable]
    public class ProcessDelivery : Entity
    {

        [Key, StringLength(20), Column("PROC_DELIV_ID")]
        public string ProcDelivID { get; set; }
      
        [MaxLength(2), Column(name:"DEPART_ID", TypeName = "char")]
        public string DepartID { get; set; }

        [Column("BILL_DT")]
        public DateTime BillDt { get; set; }

        [StringLength(20),Column("WAREH_KPR_ID")]
        public string WarehKprId { get; set; }

        [StringLength(20), Column(name: "BTH_ID")]
        public string BatchID { get; set; }

        [MaxLength(1), Column(name: "WAREH_STA", TypeName = "char")]
        public string WarehouseStatus { get; set; } //交仓单状态

        [StringLength(400),Column("REMARK")]
        public string Remark { get; set; }
    }

}
