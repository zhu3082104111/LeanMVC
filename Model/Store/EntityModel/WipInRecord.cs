/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInlRecord.cs
// 文件功能描述：
//          在制品入库履历表的实体Model类
//      
// 修改履历：2013/11/16 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Model
{
    [Serializable, Table("MC_WH_WIP_IN_RECORD")]
    public class WipInRecord : Entity
    {
        public WipInRecord()
        {
           
        }
        /**
         * 在制品库入库履历表 MC_WH_WIP_IN_RECORD								
         * 
         * */

        //计划单号
        [Column("PLAN_ID")]
        [Required, StringLength(20)]
        public string PlanID { set; get; }

        //计划单区分
        [Column("PLAN_CLS")]
        [Required, StringLength(3)]
        public string PlanCls { set; get; }

        //批次号
        [Column("BTH_ID")]
        [Required, StringLength(20)]
        public string BthID { set; get; }

        //送货单号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None),Column("DLVY_LIST_ID")]
        [StringLength(20)]
        public string DlvyListID { set; get; }

        //仓库编号
        [Column("WH_ID")]
        [Required, StringLength(8)]
        public string WhID { set; get; }

        //仓位
        [Column("WH_POSI_ID")]
        [Required, StringLength(3)]
        public string WhPosiID { set; get; }

        //入库移动区分
        [Column("IN_MV_CLS")]
        [Required, StringLength(2)]
        public string InMvCls { set; get; }

        //加工产品入库单据号
        [Column("TECN_PDT_IN_ID")]
        [StringLength(20)]
        public string TecnPdtInID { set; get; }

        //加工单位
        [Column("PROC_UNIT")]
        [StringLength(20)]
        public string ProcUnit { set; get; }


    }
}
