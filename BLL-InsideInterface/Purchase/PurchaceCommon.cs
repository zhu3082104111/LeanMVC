/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaceCommon.cs
// 文件功能描述：
//          采购部门的共通的类
//      
// 修改履历：2013/12/17 宋彬磊 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 外购单信息
    /// </summary>
    public class OutSourceOrderInfo
    {
        // 外购单表信息
        public MCOutSourceOrder OutOrderInfo { get; set; }

        // 外购单详细表信息
        public List<MCOutSourceOrderDetail> OutDetailsList { get; set; }
    }

    /// <summary>
    /// 外协单信息
    /// </summary>
    public class SupplierOrderInfo
    {
        // 外协单表信息
        public MCSupplierOrder SupOrderInfo { get; set; }

        // 外协单详细表信息
        public List<MCSupplierOrderDetail> SupDetailsList { get; set; }
    }

    /// <summary>
    /// 送货单信息
    /// </summary>
    public class DeliveryOrderInfo
    {
        // 送货单表信息
        public MCDeliveryOrder DelivOrderInfo { get; set; }

        // 送货单详细表信息
        public List<MCDeliveryOrderDetail> DelivDetailsList { get; set; }
    }
}
