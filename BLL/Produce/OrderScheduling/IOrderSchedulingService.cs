using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace BLL
{
    public interface IOrderSchedulingService
    {
        /// <summary>
        /// 获取指定订单x,{x|x.status∈{已接受，排产完成，已排产}}
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        IEnumerable<VM_OrderSchedulingShow> GetSchedulOrder(VM_OrderSchedulingSearch searchCondition, Paging pagex);
    }
}
