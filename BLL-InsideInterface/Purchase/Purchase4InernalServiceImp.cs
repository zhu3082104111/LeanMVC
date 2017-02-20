/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：Purchase4ExternalServiceImp.cs
// 文件功能描述：
//          采购部门的内部共通的Service接口的实现类
//      
// 修改履历：2013/12/17 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 采购部门的内部共通的Service接口的实现类
    /// </summary>
    public class Purchase4InernalServiceImp : IPurchase4InernalService
    {
        // 返回结果遍历
        bool ret;
        
        // 外购单表的Repository类
        private IMCOutSourceOrderRepository outOrderRepos;

        // 外购单详细表Repository类
        private IMCOutSourceOrderDetailRepository outOrderDetailRepos;

        // 外协单表的Repository类
        private ISupplierOrderRepository supOrderRepos;

        // 外协单详细表的Repository类
        private ISupplierOrderDetailRepository supOrderDetailRepos;

        //送货单表的Repository类
        private IDeliveryOrderDetailRepository delOrderRepos;

        //送货单详细表的Repository类
        private IDeliveryOrderRepository delOrderDetailRepos;


        public Purchase4InernalServiceImp(IMCOutSourceOrderRepository outOrderRepos, IMCOutSourceOrderDetailRepository outOrderDetailRepos,
            ISupplierOrderRepository supOrderRepos, ISupplierOrderDetailRepository supOrderDetailRepos,
            IDeliveryOrderDetailRepository delOrderRepos, IDeliveryOrderRepository delOrderDetailRepos)
        {
            this.outOrderRepos = outOrderRepos;
            this.outOrderDetailRepos = outOrderDetailRepos;
            this.supOrderRepos = supOrderRepos;
            this.supOrderDetailRepos = supOrderDetailRepos;
            this.delOrderRepos = delOrderRepos;
            this.delOrderDetailRepos = delOrderDetailRepos;
        }

        /// <summary>
        /// 外购单的添加函数
        /// </summary>
        /// <param name="outSourceOrder">外购单信息</param>
        /// <returns></returns>
        public bool AddOutSourceOrder(List<OutSourceOrderInfo> outSourceOrderList)
        {
            // 外购单信息List不为空时
            if (outSourceOrderList != null && outSourceOrderList.Count > 0)
            {
                // 遍历外购单信息List
                foreach(OutSourceOrderInfo outSourceOrderInfo in outSourceOrderList)
                {
                    // 外购单表信息
                    MCOutSourceOrder outOrderInfo = outSourceOrderInfo.OutOrderInfo;
                    
                    // 向外购单表里插入数据
                    ret = outOrderRepos.Add(outOrderInfo);
                    
                    // 如果插入失败时，返回false。
                    if(ret == false)
                    {
                        return false;
                    }

                    // 外购单详细表信息
                    List<MCOutSourceOrderDetail> detailsList = outSourceOrderInfo.OutDetailsList;
                    
                    // 外购单详细信息List不为空时
                    if(detailsList != null && detailsList.Count > 0)
                    {
                        // 遍历外购单详细信息List
                        foreach(MCOutSourceOrderDetail outDetail in detailsList)
                        {
                            // 向外购单详细表里插入数据
                            ret = outOrderDetailRepos.Add(outDetail);

                            // 如果插入失败时，返回false。
                            if(ret == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// 外协单的添加函数
        /// </summary>
        /// <param name="supplierOrderList">外协单信息</param>
        /// <returns></returns>
        public bool AddSupplierOrder(List<SupplierOrderInfo> supplierOrderList)
        {
            // 外协单信息不为空时
            if (supplierOrderList != null && supplierOrderList.Count > 0)
            {
                // 遍历外协单信息List
                foreach (SupplierOrderInfo supplierOrderInfo in supplierOrderList)
                {
                    // 外协单表信息
                    MCSupplierOrder supOrder = supplierOrderInfo.SupOrderInfo;
                    
                    // 向外协单表里插入数据
                    ret = supOrderRepos.Add(supOrder);

                    // 插入失败时，返回false；
                    if (ret == false)
                    {
                        return false;
                    }

                    // 外协单详细表信息List
                    List<MCSupplierOrderDetail> detailsList = supplierOrderInfo.SupDetailsList;

                    // 当外协单详细表信息List不为空时
                    if (detailsList != null && detailsList.Count > 0)
                    {
                        // 遍历外协单详细表信息List
                        foreach (MCSupplierOrderDetail supDetail in detailsList)
                        {
                            // 想外协单详细表里插入数据
                            ret = supOrderDetailRepos.Add(supDetail);

                            // 插入数据失败时，返回false；
                            if (ret == false)
                            {
                                return false;
                            }

                            //TODO:外协领料单表的更新
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 送货单的添加函数（）
        /// </summary>
        /// <param name="deliveryOrderList">送货单信息</param>
        /// <returns></returns>
        public bool AddDeliveryOrder(List<DeliveryOrderInfo> deliveryOrderList)
        {
            // 外购单信息List不为空时
            if (deliveryOrderList != null && deliveryOrderList.Count > 0)
            {
                // 遍历送货单信息List
                foreach (DeliveryOrderInfo deliveryOrderInfo in deliveryOrderList)
                {
                    // 送货单表信息
                    MCDeliveryOrder delOrderInfo = deliveryOrderInfo.DelivOrderInfo;

                    // 向送货单表里插入数据
                    ret = delOrderDetailRepos.Add(delOrderInfo);

                    // 如果插入失败时，返回false。
                    if (ret == false)
                    {
                        return false;
                    }

                    // 送货单详细表信息
                    List<MCDeliveryOrderDetail> detailsList = deliveryOrderInfo.DelivDetailsList;

                    // 送货单详细信息List不为空时
                    if (detailsList != null && detailsList.Count > 0)
                    {
                        // 遍历送货单详细信息List
                        foreach (MCDeliveryOrderDetail delDetail in detailsList)
                        {
                            // 向送货单详细表里插入数据
                            ret = delOrderRepos.Add(delDetail);

                            // 如果插入失败时，返回false。
                            if (ret == false)
                            {
                                return false;
                            }
                        }
                    }

                }
            }

            return true;
        }
    }
}
