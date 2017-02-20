/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProduceGeneralPlan.cs
// 文件功能描述：生产计划总表
// 
// 创建标识：朱静波 20131112
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
    /// 生产计划总表[PD_GENERAL_PLAN]
    /// 20131112 朱静波 建立
    /// 20131120 梁龙飞 M
    /// </summary>
    [Serializable]
    [Table("PD_GENERAL_PLAN")]
    public class ProduceGeneralPlan : Entity
    {
        [Column("CLN_ODR_ID",Order = 0)]
        [Key, StringLength(20)]
        public string ClientOrderID { get; set; }//客户订单号

        [Key, StringLength(2), Column("CLN_ODR_DTL",Order = 1)]
        public string ClientOrderDetail { get; set; }//客户订单明细，区分相同型号的产品

        [Required, StringLength(15), Column("PRODUCT_ID")]
        public string ProductID { get; set; }//产品ID

        [StringLength(1), Column(name: "STATUS", TypeName = "char")]
        public string Status { get; set; }//订单生产状态（1：未接收，2：已接收，3：采购中，4：生产中，5：总装中，6：生产完成）


    }
}
