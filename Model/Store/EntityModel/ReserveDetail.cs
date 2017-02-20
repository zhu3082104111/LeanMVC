// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IGiMaterialRepository.cs
// 文件功能描述：仓库预约详细表
// 
// 创建标识：
//
// 修改标识：代东泽 20131226
// 修改描述：添加注释
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
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
    /// <summary>
    /// M:梁龙飞：仓库预约详细表MC_WH_RESERVE_DETAIL
    /// </summary>
    [Serializable, Table("MC_WH_RESERVE_DETAIL")]
    public class ReserveDetail : Entity
    {
        public ReserveDetail()
        {
            
        }
      

        /// <summary>
        /// 预约批次详细单号:主键1
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("ORDE_BTH_DTAIL_LIST_ID",Order=0)]
        //[StringLength(20)]
        public int OrdeBthDtailListID { set; get; }

        /// <summary>
        /// 批次号，主键2：共有2个主键
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("BTH_ID",Order=1)]
        [StringLength(20)]
        public string BthID { set; get; }

        /// <summary>
        /// 预约数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ORDE_QTY")]
        public decimal OrderQty { set; get; }

        //领料单开具数量
        [DecimalPrecision(10, 2),Column("PICK_ORDE_QTY")]
        public decimal PickOrdeQty { set; get; }

        //已出库数量
        [DecimalPrecision(10, 2),Column("CMP_QTY")]
        public decimal CmpQty { set; get; }

       
    }
}
