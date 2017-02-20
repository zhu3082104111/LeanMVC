/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：TeamRepositoryImp.cs
// 文件功能描述：班组表的Repository接口实现类
//     
// 修改履历：2014/1/6 朱静波 新建
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository.database;

namespace Repository.Base.implement
{
    class TeamRepositoryImp : AbstractRepository<DB, TeamInfo>, ITeamRepository
    {
        /// <summary>
        /// 根据检索条件检索（用于生成班组的自动检索）
        /// </summary>
        /// <param name="searcher">检索条件</param>
        /// <returns></returns>
        public override IEnumerable<Searcher> GetAutoSearchData(Searcher searcher)
        {
            var query = from u in base.GetAvailableList<TeamInfo>()
                        where u.TeamId.Contains(searcher.Keyword) || u.TeamName.Contains(searcher.Keyword)
                        select new Searcher()
                        {
                            Id = u.TeamId,
                            Name = u.TeamName
                        };
            return query.ToPageList("Id", new Paging() { page = 1, rows = 10 }).ToList();
        }

        public string GetTeamName(string teamId)
        {
            return base.GetAvailableList<TeamInfo>().Where(t => t.TeamId.Equals(teamId)).Select(t => t.TeamName).FirstOrDefault();
        }
    }
}
