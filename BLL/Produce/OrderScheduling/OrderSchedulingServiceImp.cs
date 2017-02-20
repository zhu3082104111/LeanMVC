/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：OrderSchedulingServiceImp.cs
// 文件功能描述：订单产品service接口
// 
// 创建标识：201311 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;
using Repository;

namespace BLL
{
    public class OrderSchedulingServiceImp : AbstractService,IOrderSchedulingService
    {
        private IProduceGeneralPlanRepository ProduceGeneralPlanRepository;
        public OrderSchedulingServiceImp(IProduceGeneralPlanRepository ProduceGeneralPlanrepository)
        {
            this.ProduceGeneralPlanRepository = ProduceGeneralPlanrepository;
        }


        #region 订单排产界面：C：梁龙飞
        /// <summary>
        /// 获取指定订单x,{x|x.status∈{已接受，排产完成，已排产}}
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        public IEnumerable<VM_OrderSchedulingShow> GetSchedulOrder(VM_OrderSchedulingSearch searchCondition, Paging pagex)
        {
            return ProduceGeneralPlanRepository.GetPlanAccepted(searchCondition,pagex);
        }

        #endregion
    }
}
