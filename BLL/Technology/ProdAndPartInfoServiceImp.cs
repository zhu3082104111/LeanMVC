/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompanyInfoServiceImp.cs
// 文件功能描述：
//          产品零件信息的Service接口的实现类
//      
// 修改履历：2013/12/16 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;
using Repository;

namespace BLL
{
    /// <summary>
    /// 产品零件信息的Service接口的实现类
    /// </summary>
    public class ProdAndPartInfoServiceImp : AbstractService, IProdAndPartInfoService
    {
        // 供货商信息的Repository
        private IPartInfoRepository prodAndPartRepository;

        /// <summary>
        /// 实例化函数
        /// </summary>
        /// <param name="prodAndPartRepository"></param>
        public ProdAndPartInfoServiceImp(IPartInfoRepository prodAndPartRepository)
        {
            this.prodAndPartRepository = prodAndPartRepository;
        }

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        public IEnumerable<VM_ProdAndPartInfo4Sel> GetProdAndPartInfo4Sel(VM_ProdAndPartInfo4Sel searchConditon, Paging paging)
        {
            return prodAndPartRepository.GetProdAndPartInfo4Sel(searchConditon, paging);
        }
    }
}
