/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IOrderRateService.cs
// 文件功能描述：产品和订单进度查询的Service接口
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System.Collections.Generic;
using Extensions;
using Model;
using Model.Produce;

namespace BLL
{
    public interface IOrderRateService
    {
        /// <summary>
        /// 按条件查询产品和订单信息
        /// </summary>
        /// <param name="inProcessingRate">输入的查询信息</param>
        /// <param name="paging">分页</param>
        /// <param name="headerData">取得标题头数据</param>
        /// <returns>进度查询一览数据集</returns>
        IEnumerable<VM_OrderRateForTableShow> GetOrderRateSearch(VM_OrderRateForSrarch inProcessingRate, Paging paging, VM_HeaderData headerData);

        /// <summary>
        /// 取得产品ID和略称
        /// </summary>
        /// <param name="clientOrderID">客户订单号</param>
        /// <returns>产品型号及相应信息</returns>
        IEnumerable<VM_ProduceType> GetProduceType(string clientOrderID);
    }
}
