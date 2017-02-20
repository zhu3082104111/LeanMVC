/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_BllInsideInterface.cs
// 文件功能描述：
//          生产部门的外部共通的Service接口使用视图Model类
//      
// 修改履历：2013/12/24 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //具体视图属性由担当者进行添加
    //创建 朱静波 20131224
    /// <summary>
    /// VM_MaterReqInfo，包含领料单号，产品零件ID，数量，是否提供批次号区分
    /// </summary>
    public class VM_MaterReqInfo
    {
    }

    /// <summary>
    /// VM_MaterReqBthInfo，包含领料单号，产品零件ID，批次号，数量，单配区分
    /// </summary>
    public class VM_MaterReqBthInfo
    {
    }

    /// <summary>
    /// VM_ClnOdrList，包含客户订单号，客户订单明细号，以及交仓数量的List。
    /// </summary>
    public class VM_ClnOdrList
    {
        public string ClnOrderID;//--客户订单号
        public string ClnOrderDetail;//--订单明细号
        public decimal WarehQty;//--交仓数量
    }
}
