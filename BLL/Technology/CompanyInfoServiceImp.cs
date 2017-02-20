/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompanyInfoServiceImp.cs
// 文件功能描述：
//          供货商信息的Service接口的实现类
//      
// 修改履历：2013/12/9 宋彬磊 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 供货商信息的Service接口的实现类
    /// </summary>
    public class CompanyInfoServiceImp : AbstractService, ICompanyInfoService
    {
        // 供货商信息的Repository
        private ICompInfoRepository compRepository;

        /// <summary>
        /// 实例化函数
        /// </summary>
        /// <param name="compRepository"></param>
        public CompanyInfoServiceImp(ICompInfoRepository compRepository)
        {
            this.compRepository = compRepository;
        }

        /// <summary>
        /// 根据条件获取供货商信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>供货商信息</returns>
        public IEnumerable<VM_CompanyInfo4Sel> GetCompanyInfo4Sel(VM_CompSearchCondition searchConditon, Paging paging)
        {
            return compRepository.GetCompanyInfo4Sel(searchConditon, paging);
        }
    }
}
