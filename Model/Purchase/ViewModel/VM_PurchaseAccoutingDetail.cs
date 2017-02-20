/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_PurchaseAccoutingDetail.cs
// 文件功能描述：
//          外购计划台帐详细画面的Model类
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 外购计划台帐详细画面的基本信息
    /// </summary>
    public class VM_PurchaseAccoutingDetail
    {
        // 外购单号
        public string OutOrderId { get; set; }

        // 紧急状态
        public string UrgentStatus { get; set; }

        // 下单日期(编制时间)
        public DateTime? EstablishDate { get; set; }

        // 生产部门
        public string DeptName { get; set; }

        // 供货商
        public string CompName { get; set; }
    }
      
    /// <summary>
    /// 外购计划台帐详细画面的详细信息
    /// </summary>
    public class VM_PurchaseAccoutingDetail4Table
    {

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 材料规格及要求
        public string MaterialsSpecReq { get; set; }

        // 订货数量
        public decimal OrderQuantity { get; set; }

        // 交货日期
        public DateTime? OrderDate { get; set; }

        // 送货单号
        public string DeliveryOrderNo { get; set; }

        // 送货日期
        public DateTime? DeliveryDate { get; set; }
        
        // 送货数量
        public decimal? DeliveryQuantity { get; set; }

        // 质检单号
        public string QCOrderNo { get; set; }

        // 质检日期
        public DateTime? QCDate { get; set; }

        // 质检结果
        public string QCResult { get; set; }

        // 入库单号
        public string StorageOrderNo { get; set; }

        // 入库日期
        public DateTime? StorageDate { get; set; }

        // 入库数量
        public decimal? StorageQuantity { get; set; }
    }
}
