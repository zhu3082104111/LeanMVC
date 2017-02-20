/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccOutRecord.cs
// 文件功能描述：
//          附件库出库履历表的实体Model类
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
    [Serializable, Table("MC_WH_ACC_OUT_RECORD")]
    public class AccOutRecord : Entity
    {
        public AccOutRecord()
        {
          
        }
        /**
         * 附件库出库履历表 MC_WH_ACC_OUT_RECORD								
         * 
         * */

        //领料单号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PICK_LIST_ID")]
        [Required, StringLength(20)]
        public string PickListID { set; get; }

        //领料单类型
        [Column("PICK_LIST_TYPE_ID")]
        [Required, StringLength(20)]
        public string PickListTypeID { set; get; }

        //仓库编号
        [Column("WH_ID")]
        [Required, StringLength(8)]
        public string WhID { set; get; }

        //出库移动区分
        [Column("OUT_MV_CLS")]
        [Required, StringLength(2)]
        public string OutMvCls { set; get; }

        //加工产品出库单号
        [Column("SAEET_ID")]
        [Required, StringLength(20)]
        public string SaeetID { set; get; }

        //调入单位ID
        [Column("CALLIN_UNIT_ID")]
        [Required, StringLength(20)]
        public string CallinUnitID { set; get; }
        
       
    }
}
