/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompInfoRepositoryImp.cs
// 文件功能描述：
//          供货商信息的Repository接口的实现类
//      
// 修改履历：2013/12/9 宋彬磊 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Repository.Technology.implement
{
    /// <summary>
    /// 供货商信息的Repository接口的实现类
    /// </summary>
    public class CompInfoRepositoryImp : AbstractRepository<DB, CompInfo>, ICompInfoRepository
    {
        /// <summary>
        /// 根据条件获取供货商信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>供货商信息</returns>
        public IEnumerable<VM_CompanyInfo4Sel> GetCompanyInfo4Sel(VM_CompSearchCondition searchConditon, Paging paging)
        {
            // 供货商信息表
            IQueryable<CompInfo> compInfo = base.GetAvailableList<CompInfo>();

            // 供货商供货信息表
            IQueryable<CompMaterialInfo> compMaterialInfo = base.GetAvailableList<CompMaterialInfo>();

            // 供货商种类的设定
            if (!String.IsNullOrEmpty(searchConditon.CompType))
            {
                // 外购供货商
                if (Constant.CompanyType.OUT_SOURCING.Equals(searchConditon.CompType))
                {
                    compInfo = compInfo.Where(c => c.CompType == Constant.CompanyType.OUT_SOURCING || c.CompType == Constant.CompanyType.BOTH);
                }
                // 外协供货商
                else if (Constant.CompanyType.SUPPLIER.Equals(searchConditon.CompType))
                {
                    compInfo = compInfo.Where(c => c.CompType == Constant.CompanyType.SUPPLIER || c.CompType == Constant.CompanyType.BOTH);
                }
                // 外购、外协供货商
                else
                {
                    compInfo = compInfo.Where(c => c.CompType != Constant.CompanyType.UNKONWN);
                }
            }

            // 供货商ID
            if (!String.IsNullOrEmpty(searchConditon.CompID))
            {
                compInfo = compInfo.Where(c => c.CompId.Contains(searchConditon.CompID));
            }

            // 供货商名称
            if (!String.IsNullOrEmpty(searchConditon.CompName))
            {
                compInfo = compInfo.Where(c => c.CompName.Contains(searchConditon.CompName));
            }

            // 供货商可提供的产品零件ID的设定
            if (!String.IsNullOrEmpty(searchConditon.PdtID))
            {
                compMaterialInfo = compMaterialInfo.Where(cm => cm.PdtId == searchConditon.PdtID);
                compInfo = (from c in compInfo
                            from cm in compMaterialInfo
                            where c.CompId == cm.CompId
                            select c).Distinct();
            }

            IQueryable<VM_CompanyInfo4Sel> query = from c in compInfo
                                                   select new VM_CompanyInfo4Sel
                                                   {
                                                       CompID = c.CompId,
                                                       CompName = c.CompName
                                                   };
            paging.total = query.Count();
            IEnumerable<VM_CompanyInfo4Sel> result = query.ToPageList<VM_CompanyInfo4Sel>("CompID asc", paging);
            return result;
        }
    }
}
