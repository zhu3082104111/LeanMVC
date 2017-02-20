/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_ProducePlan.cs
// 文件功能描述：生产计划视图Model
//     
// 修改履历：2013/12/21 朱静波 新建
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
    /// 生产计划总表搜索Model
    /// </summary>
    public class VM_ProducePlanForSearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [EntityProperty("ClientOrderID", PropertyOperator.EQUAL)]
        public string TxtClientOrderID { get; set; }

        /// <summary>
        /// 销售员ID
        /// </summary>
        [EntityProperty("SaleID", PropertyOperator.EQUAL)]
        public string TxtSaleId { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [EntityProperty("ProdAbbrev", PropertyOperator.EQUAL)]
        public string TxtProductType { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
        [EntityProperty("StateValue", PropertyOperator.EQUAL)]
        public string TxtProduceState { get; set; }

        /// <summary>
        /// 计划交期开始
        /// </summary>
        [EntityProperty("DeliveryDate", PropertyOperator.GE)]
        public DateTime? TxtDelvyStartDate { get; set; }

        /// <summary>
        /// 计划交期结束
        /// </summary>
        [EntityProperty("DeliveryDate", PropertyOperator.LE)]
        public DateTime? TxtDelvyEndDate { get; set; }
    }

    /// <summary>
    /// 生产计划总表数据视图
    /// </summary>
    public class VM_ProducePlanShow
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 客户订单详细
        /// </summary>
        public string ClientOrderDetail { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public string SaleID { get; set; }

        /// <summary>
        /// 销售员名字
        /// </summary>
        public string SaleName { get; set; }

        /// <summary>
        /// 计划交期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        public string ProdAbbrev { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID{ get; set; }

        /// <summary>
        /// 计划数
        /// </summary>
        public Decimal PlanQTY { get; set; }

        /// <summary>
        /// 完成数量
        /// </summary>
        public Decimal QTYCompletion { get; set; }

        /// <summary>
        /// 未完成数量
        /// </summary>
        public Decimal QTYUncompletion{ get; set; }

        /// <summary>
        /// 达成率
        /// </summary>
        public Decimal AchievementRate{ get; set; }

        /// <summary>
        /// 生产状态值
        /// </summary>
        public string StateValue { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
        public string ProduceState { get; set; }
    }
}
