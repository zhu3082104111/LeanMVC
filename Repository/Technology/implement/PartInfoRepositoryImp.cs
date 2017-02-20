/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PartInfoRepositoryImp.cs
// 文件功能描述：
//          零件信息的Repository接口的实现
//      
// 修改履历：2013/12/12 陈健 新建
// 修改履历：2013/12/16 宋彬磊  添加（产品零件查询（子查询画面专用））
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Extensions;

namespace Repository.Technology.implement
{
    /// <summary>
    /// 零件信息的Repository接口的实现
    /// </summary>
    public class PartInfoRepositoryImp : AbstractRepository<DB, PartInfo>, IPartInfoRepository
    {
        /// <summary>
        /// 成品库入库履历详细手动登录根据产品ID生成产品名称 陈健
        /// </summary>
        /// <param name="partAbbrevi">产品略称</param>
        /// <returns>产品名称</returns>
        public List<PartInfo> GetFinRecordProductInfoById(string partAbbrevi)
        {
            var prodList = base.GetAvailableList<PartInfo>().Where(p => p.PartAbbrevi.Equals(partAbbrevi)).ToList();
            if (prodList.Count == 0)
            {
                return null;
            }
            else
            {
                return prodList;
            }
        }

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        public IEnumerable<VM_ProdAndPartInfo4Sel> GetProdAndPartInfo4Sel(VM_ProdAndPartInfo4Sel searchConditon, Paging paging)
        {
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();

            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );

            // 物料编号（产品零件略称）
            if (!String.IsNullOrEmpty(searchConditon.Abbrev))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.abbrev.Contains(searchConditon.Abbrev));
            }

            // 物料名称（产品零件名称）
            if (!String.IsNullOrEmpty(searchConditon.Name))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.name.Contains(searchConditon.Name));
            }

            IQueryable<VM_ProdAndPartInfo4Sel> query = from pp in prodAndPartsList
                                                       select new VM_ProdAndPartInfo4Sel
                                                       {
                                                           Id = pp.id,
                                                           Abbrev = pp.abbrev,
                                                           Name = pp.name
                                                       };

            paging.total = query.Count();
            IEnumerable<VM_ProdAndPartInfo4Sel> result = query.ToPageList<VM_ProdAndPartInfo4Sel>("Abbrev asc", paging);
            return result;
        }

        /// <summary>
        /// 成品库出库履历详细画面 根据零件略称获得其ID 陈健
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>数据集合</returns>
        public IEnumerable<PartInfo> GetPartID(string partAbbrevi)
        {

            return base.GetList().Where(h => h.PartAbbrevi.Equals(partAbbrevi) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }
    }
}
