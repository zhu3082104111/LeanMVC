/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ITeamRepository.cs
// 文件功能描述：班组表的Repository接口
//     
// 修改履历：2014/1/6 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Base
{
    public interface ITeamRepository : IRepository<TeamInfo>
    {
        /// <summary>
        /// 根据班组ID取得班组名称
        /// </summary>
        /// <param name="teamId">班组ID</param>
        /// <returns>班组名称</returns>
        String GetTeamName(string teamId);
    }
}
