/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPurchase4InernalService.cs
// 文件功能描述：
//          采购部门的内部共通的Service接口类
//      
// 修改履历：2013/12/17 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 采购部门的内部共通的Service接口的实现类
    /// </summary>
    public interface IPurchase4InernalService
    {
        /// <summary>
        /// 外购单的添加函数
        /// </summary>
        /// <param name="outSourceOrder">外购单信息</param>
        /// <returns></returns>
        bool AddOutSourceOrder(List<OutSourceOrderInfo> outSourceOrderList);

        /// <summary>
        /// 外协单的添加函数
        /// </summary>
        /// <param name="supplierOrderList">外协单信息</param>
        /// <returns></returns>
        bool AddSupplierOrder(List<SupplierOrderInfo> supplierOrderList);

        /// <summary>
        /// 送货单的添加函数（）
        /// </summary>
        /// <param name="deliveryOrderList">送货单信息</param>
        /// <returns></returns>
        bool AddDeliveryOrder(List<DeliveryOrderInfo> deliveryOrderList);
    }
}
