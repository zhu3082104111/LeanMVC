/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketClientInformationRepositoryImp.cs
// 文件功能描述：客户信息表Repository接口实现类
//     
// 修改履历：2013/11/26 朱静波 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
    class MarketClientInformationRepositoryImp : AbstractRepository<DB, MarketClientInformation>, IMarketClientInformationRepository
    {
        /// <summary>
        /// 获取表 MK_CLN_INFO 查询记录
        /// </summary>
        /// <param name="paraMOFSTMO">MarketClientInformation 实体类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>MarketClientInformation 实体类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<MarketClientInformation> GetMarketClientInfoListRepository(MarketClientInformation paraMCI, Extensions.Paging paraPage)
        {
            IQueryable<MarketClientInformation> marketClientInformationIQ = base.GetList(); //获取全部数据

            //查询
            if (String.IsNullOrEmpty(paraMCI.ClientName) == false)
            {
                marketClientInformationIQ = marketClientInformationIQ.Where(mci => mci.ClientName.Contains(paraMCI.ClientName));
            }
            if (String.IsNullOrEmpty(paraMCI.ClientNO) == false)
            {
                marketClientInformationIQ = marketClientInformationIQ.Where(mci => mci.ClientNO.Contains(paraMCI.ClientNO));
            }

            paraPage.total = marketClientInformationIQ.Count(); //
            IEnumerable<MarketClientInformation> marketClientInformationIE = marketClientInformationIQ.ToPageList<MarketClientInformation>("ClientID asc", paraPage); //

            return marketClientInformationIE; //返回结果集
        } //end GetMarketClientInfoListRepository

    } //end MarketClientInformationRepositoryImp
}
