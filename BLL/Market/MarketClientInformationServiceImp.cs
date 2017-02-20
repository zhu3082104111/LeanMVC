/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketClientInformationServiceImp.cs
// 文件功能描述：客户信息表Service接口实现类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Repository;
using System.Collections.Generic;

namespace BLL
{
    class MarketClientInformationServiceImp : AbstractService, IMarketClientInformationService
    {
        private IMarketClientInformationRepository iMCIR;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIMCIR">IMarketClientInformationRepository 接口实现类</param>
        public MarketClientInformationServiceImp(IMarketClientInformationRepository paraIMCIR) 
        {
            this.iMCIR = paraIMCIR;
        } //end MarketClientInformationServiceImp

        /// <summary>
        /// 获取表 MK_CLN_INFO 查询记录
        /// </summary>
        /// <param name="paraMOFSTMO">MarketClientInformation 实体类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>MarketClientInformation 实体类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<MarketClientInformation> GetMarketClientInfoListService(MarketClientInformation paraMCI, Paging paraPage)
        {
            return iMCIR.GetMarketClientInfoListRepository(paraMCI, paraPage);
        } //end GetMarketClientInfoListService

    } //end MarketClientInformationServiceImp
}
