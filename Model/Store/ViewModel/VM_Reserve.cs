// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：VM_GiMaterial.cs
// 文件功能描述：仓库预约表VM
// 
// 创建标识：代东泽 20131226
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Store
{
    /// <summary>
    /// 代东泽 20131226
    /// 仓库预约表 领料相关数据model
    /// </summary>
    public class VM_Reserve
    {
        /// <summary>
        /// 预约批次详细单号:主键1
        /// </summary>
        //[StringLength(20)]
        public int OrdeBthDtailListID { set; get; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WhID { set; get; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ClnOdrID { set; get; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string ClnOdrDtl { set; get; }


        /// <summary>
        /// 产品零件ID
        /// </summary>
        public string ProductID { set; get; }

        //
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpec { set; get; }

        //
        /// <summary>
        /// partinfo 表中的 该零件 零件物料型号
        /// </summary>
        public string PartModel { set; get; }

        //
        /// <summary>
        /// partinfo 表中的 该零件 零件物料名称
        /// </summary>
        public string PartName { set; get; }

        //
        /// <summary>
        /// partinfo 表中的 该零件 零件物料单位
        /// </summary>
        public string PartUnit { set; get; }

        //
        /// <summary>
        /// partinfo 表中的 该零件 零件物料单价
        /// </summary>
        public decimal PartUnitPrice { set; get; }
        //
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchID { set; get; }

       
        /// <summary>
        /// 预约总数量
        /// </summary>
        public decimal AlctQuantity { set; get; }

        /// <summary>
        /// 实际在库数量
        /// </summary>
        public decimal CurrentQuantity { set; get; }

        /// <summary>
        /// 领料单开具数量
        /// </summary>
        public decimal PickiingOrderQuantity { set; get; }

        /// <summary>
        /// 预约数量
        /// </summary>
        public decimal AlctQty { set; get; }

        /// <summary>
        /// 本批次领料单开具数量
        /// </summary>
        public decimal PickiingOrderQty { set; get; }

        /// <summary>
        /// 可用在库数量
        /// </summary>
        public decimal UserableQuantity { set; get; }

        /// <summary>
        /// 上次请领数量=》供领料单详细页面check请领数量值用 修改未发领料单时 用新数量-此数量，新增物料单时此值为0
        /// 代东泽 
        /// </summary>
        public decimal LastPickingCount { get; set; }

        /// <summary>
        /// 实领数量=》供领料单详细页面使用
        /// 代东泽 
        /// </summary>
        public decimal RealPickingCount { get; set; }
    }

}
