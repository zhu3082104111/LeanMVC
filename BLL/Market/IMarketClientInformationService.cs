/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMarketClientInformationService.cs
// 文件功能描述：客户信息表Service接口类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public interface IMarketClientInformationService
    {
        /// <summary>
        /// 获取表 MK_CLN_INFO 查询记录
        /// </summary>
        /// <param name="paraMOFSTMO">MarketClientInformation 实体类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>MarketClientInformation 实体类</returns>
        /// 创建者：冯吟夷
        IEnumerable<MarketClientInformation> GetMarketClientInfoListService(MarketClientInformation paraMCI, Paging paraPage);

    }// end IMarketClientInformationService
}
