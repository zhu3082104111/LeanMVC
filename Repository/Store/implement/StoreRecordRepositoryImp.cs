/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：StoreRecordRepositoryImp.cs
// 文件功能描述：在库品一览画面的Repository实现
//      
// 修改履历：2014/1/8 刘云 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Data;

namespace Repository
{
    /// <summary>
    /// 在库品一览画面的Repository实现
    /// </summary>
    public class StoreRecordRepositoryImp : AbstractRepository<DB, Material>, IStoreRecordRepository
    {
        /// <summary>
        /// 得到将要在页面上显示的数据
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        public IEnumerable GetStoreRecordBySearchByPage(VM_StoreRecordForSearch searchConditon, Extensions.Paging paging)
        {
            IQueryable<Material> material = base.GetList();
            IQueryable<GiMaterial> gmaterial = base.GetList<GiMaterial>();            
            if (!String.IsNullOrEmpty(searchConditon.Wh))
            {
                material = material.Where(m => m.WhID.Substring(0, 2) == searchConditon.Wh);
                gmaterial = gmaterial.Where(g => g.WareHouseID.Substring(0, 2) == searchConditon.Wh);
            }
            if (!String.IsNullOrEmpty(searchConditon.DepartmentID))
            {
                material = material.Where(m => m.WhID.Substring(2, 2) == searchConditon.DepartmentID);
                gmaterial = gmaterial.Where(g => g.WareHouseID.Substring(2, 2) == searchConditon.DepartmentID);
            }
            //成品信息表
            IQueryable<ProdInfo> prodInfo = base.GetList<ProdInfo>();
            //零件信息表
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>();
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodInfo
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName,
                                        type = prod.ProdCatg
                                    }
                                ).Union
                               (
                                   from part in partInfo
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName,
                                       type = part.PartCatg
                                   }
                               );


            if (searchConditon.PartType == "0")//正常品
            {
                IQueryable<VM_StoreRecordForTableShow> query = from m in material
                                                               join p in prodAndPartsList on m.PdtID equals p.id
                                                               select new VM_StoreRecordForTableShow
                                                            {
                                                                PdtID = p.abbrev,
                                                                PdtName = p.name,
                                                                AlctQty = m.AlctQty,
                                                                RequiteQty = m.RequiteQty,
                                                                OrderQty = m.OrderQty,
                                                                CnsmQty = m.CnsmQty,
                                                                ArrveQty = m.ArrveQty,
                                                                IspcQty = m.IspcQty,
                                                                UseableQty = m.UseableQty,
                                                                CurrentQty = m.CurrentQty,
                                                                //Price = p.price,
                                                                TotalAmt = m.TotalAmt
                                                            };
                paging.total = query.Count();
                IEnumerable<VM_StoreRecordForTableShow> result = query.ToPageList<VM_StoreRecordForTableShow>("PdtID asc", paging);
                return result;
            }
            else if (searchConditon.PartType == "1")//让步品
            {
                IQueryable<VM_StoreRecordForTableShow> query = from g in gmaterial
                                                               join p in prodAndPartsList on g.ProductID equals p.id
                                                               select new VM_StoreRecordForTableShow
                                                               {
                                                                   PdtID = p.abbrev,
                                                                   PdtName = p.name,
                                                                   AlctQty = g.AlctQuantity,
                                                                   UseableQty = g.UserableQuantity,
                                                                   CurrentQty = g.CurrentQuantity,
                                                                   TotalAmt = g.TotalAmt,
                                                                   RequiteQty = -1,
                                                                   OrderQty = -1,
                                                                   CnsmQty =-1,
                                                                   ArrveQty = -1,
                                                                   IspcQty = -1
                                                               };
                paging.total = query.Count();
                IEnumerable<VM_StoreRecordForTableShow> result = query.ToPageList<VM_StoreRecordForTableShow>("PdtID asc", paging);
                return result;
            }
            else
            {
                return null;
            }
        }


    }
}
