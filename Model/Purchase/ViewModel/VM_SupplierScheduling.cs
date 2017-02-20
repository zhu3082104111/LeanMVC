/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SupplierScheduling.cs
// 文件功能描述：外协排产画面的视图Model
//      
// 修改履历：2013/12/02 陈阵 新建
// 修改履历：2014/01/03 陈阵 添加字段：采购数量(Bug管理No:LDK-PT-PUR-00111)
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    /// <summary>
    /// 外协排产画面的视图Model类
    /// </summary>
    public class VM_SupplierScheduling
    {
        // 外协加工调度单号
        public string SupOrderID { get; set; }

        // 紧急状态
        public string UrgencyStatus { get; set; }

        // 生产部门ID
        public string DeptID { get; set; }

        // 客户订单号
        public string CustOrderNo { get; set; }

        // 产品零件ID
        public string ProductPartID { get; set; }

        // 产品ID
        public string ProductID { get; set; }

        // 产品型号
        public string PrdtType { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 材料规格和要求
        public string MaterialsSpecReq { get; set; }

        // 本道工序名
        public string PdProcID { get; set; }

        // 计划数量
        public decimal PlanQuantity { get; set; }

        // 开始日
        public DateTime? PlanDateS { set; get; }

        // 结束日
        public DateTime? PlanDateE { set; get; }

        // 待购数量
        public decimal WaitingQuantity { get; set; }

        // 采购数量
        public decimal RequestQuantity { get; set; }

        // 单价
        public decimal UnitPrice { get; set; }

        // 估价
        public decimal EstiPrice { get; set; }

        // 交货日期
        public DateTime? DeliveryDate { set; get; }

    }

}
