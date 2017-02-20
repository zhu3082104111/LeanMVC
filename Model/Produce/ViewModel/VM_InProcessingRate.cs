/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_InProcessingRate.cs
// 文件功能描述：内部进度页面的视图model集
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 内部加工进度统计-搜索
    /// </summary>
    public class VM_InProcessingRateSearch
    {

        public string TxtClientOrderID { get; set; }//客户订单号
        public string TxtProductID { get; set; }//产品型号
        public DateTime? TxtStartTime { get; set; }//开始时间起始
        public DateTime? TxtEndTime { get; set; }//开始时间截止
        public string txtMaterialType { get; set; }//物料型号
        public string txtProcess { get; set; }//工序
        public string txtProductionUnits { get; set; }//生产单元
        public DateTime? txtPlanDeliveryFrom { get; set; }//计划交期起始
        public DateTime? txtPlanDeliveryTo { get; set; }//计划交期截止

    }

    public class VM_InProcessingRateForTableShow
    {
        public string ClientOrderID { get; set; }//客户订单号
        public string ProduceID { get; set; }//产品型号
        public Decimal PlanNumber { get; set; }//计划数量
        public DateTime? PlannedDelivery { get; set; }//计划交期
        public string MaterialName { get; set; }//物料名称
        public string ProcessName { get; set; }//工序名称
        public DateTime? StartTime { get; set; }//计划日期开始
        public DateTime? EndTime { get; set; }//计划日期结束
        public Decimal AssemblyNum { get; set; }//数量需求
        public Decimal ActualNum { get; set; }//数量完成
        public Decimal AchievementRate { get; set; }//达成率
    }
}
