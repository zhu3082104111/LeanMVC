/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ICompInfoRepository.cs
// 文件功能描述：
//          供货商信息的Repository接口
//      
// 修改履历：2013/12/9 宋彬磊 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 供货商信息的Repository接口类
    /// </summary>
    public interface ICompInfoRepository : IRepository<CompInfo>
    {
        /// <summary>
        /// 根据条件获取供货商信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>供货商信息</returns>
        IEnumerable<VM_CompanyInfo4Sel> GetCompanyInfo4Sel(VM_CompSearchCondition searchConditon, Paging paging);
    }
}
