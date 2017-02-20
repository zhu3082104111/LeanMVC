/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProdAndPartInfoService.cs
// 文件功能描述：
//          产品零件信息的Service接口
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

namespace BLL
{
    /// <summary>
    /// 产品零件信息的Service接口
    /// </summary>
    public interface IProdAndPartInfoService
    {
        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        IEnumerable<VM_ProdAndPartInfo4Sel> GetProdAndPartInfo4Sel(VM_ProdAndPartInfo4Sel searchConditon, Paging paging);
    }
}
