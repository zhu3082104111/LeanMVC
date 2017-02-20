/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：OrderRateServiceImp.cs
// 文件功能描述：总装调度单的Service接口实现
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository;
using Util;

namespace BLL
{
    class OrderRateServiceImp : AbstractService, IOrderRateService
    {
        private IProduceGeneralPlanRepository produceGeneralPlanRepository;
        private IOrderDetailRepository orderDetailRepository;

        public OrderRateServiceImp(IProduceGeneralPlanRepository produceGeneralPlanRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.produceGeneralPlanRepository = produceGeneralPlanRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public IEnumerable<VM_OrderRateForTableShow> GetOrderRateSearch(VM_OrderRateForSrarch inProcessingRate, Paging paging, VM_HeaderData headerData)
        {
            IList<VM_OrderRateForTableShow> result = produceGeneralPlanRepository.GetOrderRateForSrarch(inProcessingRate, paging, headerData).ToList();

            Decimal totalDay = 0;
            Decimal totalSum = 0;

            foreach (var orderRate in result)
            {
                //物料达成率：（生产投产数量+外购投产数量+外协投产数量+正常品锁库数量+单配品锁库数量）×100%/(自加工数量+外协数量+外购数量+锁库数量合）
                if (orderRate.STP_Num + orderRate.OutSource_Num + orderRate.Association_Num + orderRate.TotalLockNumber > 0)
                {
                    orderRate.MaterialAchieveRate = Decimal.Round(((orderRate.ProduceProductionQuantity +
                                                     orderRate.PurchProductionQuantity +
                                                     orderRate.AssistProductionQuantity + orderRate.TotalLockNumber)
                                                     / (orderRate.STP_Num + orderRate.OutSource_Num + orderRate.Association_Num + orderRate.TotalLockNumber)), 3) * 100;
                    orderRate.STP_AchieveRate = orderRate.STP_AchieveRate * 100;
                    orderRate.OutSource_AchieveRate = orderRate.OutSource_AchieveRate * 100;
                    orderRate.Association_AchieveRate = orderRate.Association_AchieveRate * 100;
                }

                
                if (orderRate.MaterialAchieveRate > 0)
                {
                    totalSum = orderRate.PreparationPeriod % orderRate.MaterialAchieveRate + totalSum;
                }
                totalDay = orderRate.PreparationPeriod + totalDay;

            }
            //订单对应产品的达成率
            if (totalDay > 0)
            {
                headerData.AchieveRate = Decimal.Round(Decimal.Parse((totalSum / totalDay).ToString()), 3) * 100;
            }
            return result;
        }

        public IEnumerable<VM_ProduceType> GetProduceType(string clientOrderID)
        {
            return orderDetailRepository.GetProduceType(clientOrderID);
        }
    }
}
