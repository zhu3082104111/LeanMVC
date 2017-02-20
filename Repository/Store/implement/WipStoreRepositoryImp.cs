using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Collections;

namespace Repository
{
    public class WipStoreRepositoryImp : AbstractRepository<DB, WipStore>, IWipStoreRepository
    {

        public IEnumerable<Model.WipStore> getAllWipStore()
        {
          //  return Db.WipStore.ToList();
            return null;
        }


        public IQueryable<WipStore> getAllWipStoreAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<WipStore> getAllWipStoreAsQueryableOrderById(string id)
        {
            return base.GetList().Where(a => a.Id == id && a.Enabled == true).OrderBy(a => a.Id);
            //return base.GetList<string>(a => a.Id == id && a.Enabled == true, a => a.Id);
        }

        public IQueryable<WipStore> getAllWipStoreAsQueryableOrderBy()
        {
            return base.GetList().Where(a => a.Enabled == true).OrderBy(a => a.Id);
            //return base.GetList<string>(a => a.Enabled == true, a => a.Id);
        }

        public WipStore getWipStoreById(string id)
        {
            return base.GetEntityById(id);
        }


        public bool addWipStore(WipStore w)
        {
            return base.Add(w);
        }

        public bool updateWipStore(WipStore w)
        {
            return this.Update(w);
        }

        public bool updateWipStoreColumns(WipStore w)
        {
            return base.Update(w, new string[] { "DelFlag" });
        }

        public bool deleteWipStore(WipStore w)
        {
            return base.Delete(w);
        }

        public bool updateWipStoreBySQL(WipStore w)
        {
            return base.ExecuteStoreCommand("update BI_WipStore set Enabled='False' where Id={0} ", w.Id);
        }

        public bool deleteWipStoreBySQL(WipStore w)
        {
            return base.ExecuteStoreCommand("delete from BI_WipStore  where Id={0}", w.Id);
        }


        #region IWipStoreRepository 成员


        public IEnumerable GetWipStoreBySearchByPage(WipStore wipstore, Extensions.Paging paging)
        {
            IQueryable<WipStore> list = base.GetList().Where(a => a.Enabled == true && a.DelFlag == "0" && a.WhID == "0002");
            IQueryable<WipStoreDetil> listde = base.GetList<WipStoreDetil>().Where(a => a.Enabled == true);
            bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(wipstore.Id))
            {
                list = list.Where(w => w.Id == wipstore.Id);
                isPaging = false;
            }
            else
            {
                if (!String.IsNullOrEmpty(wipstore.Id))
                {
                    list = list.Where(w => w.Id.Contains(wipstore.Id));
                }
                if (!String.IsNullOrEmpty(wipstore.SupplierName))
                {
                    list = list.Where(w => w.SupplierName.Contains(wipstore.SupplierName));
                }
                if (!String.IsNullOrEmpty(wipstore.MatterName))
                {
                    list = list.Where(w => w.MatterName.Contains(wipstore.MatterName));
                }
                if (!String.IsNullOrEmpty(wipstore.DeliverId))
                {
                    list = list.Where(w => w.DeliverId.Contains(wipstore.DeliverId));
                }
               
            }
            IQueryable<WipStore> WipStore = list;
            var query = from u in WipStore
                        
                        select new
                        {
                            Id = u.Id,
                            SupplierName = u.SupplierName,
                            MatterName = u.MatterName,
                            Paydate = u.Paydate,
                            DeliverId = u.DeliverId,
                            Aduser=u.Aduser,
                            Uduser=u.Uduser,
                            Enabled=u.Enabled,
                            McIsetInListID=u.McIsetInListID
                            

                        };
            //if (isPaging)
            //{
            //    total = query.Count();
            //    return query.OrderBy("Id asc").Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //}      
            paging.total = query.Count();
            return query.ToPageList("Paydate asc", paging);
           
        }

        #endregion

        #region IWipStoreRepository 成员


        public IEnumerable GetWipStoreBySearchById(string Parameter_id)
        {
             IQueryable<WipStore> list = null;
            if (Parameter_id == "")
            {
                list = base.GetList().Where(a => a.Enabled == true);
            }
            else if (Parameter_id == "0") {

                list = base.GetList().Where(a => a.Id == "1000000");
            }
            else
            {
                list = base.GetList().Where(a => a.Id == Parameter_id);
            }
            IQueryable<WipStore> WipStore = list;
            var query = from u in WipStore

                        select new
                        {
                            Id = u.Id,
                            SupplierName = u.SupplierName,
                            MatterName = u.MatterName,
                            Paydate = u.Paydate,
                            Aduser = u.Aduser,
                            Uduser = u.Uduser,
                            Enabled=u.Enabled

                        };
            return query;
        }

        #endregion

        #region IWipStoreRepository 成员


        public IEnumerable SelectWipStore(string pid)
        {
            IQueryable<WipStore> listWipStore = null;
             listWipStore =base.GetList();
             listWipStore = from a in listWipStore where pid.Contains(a.Id) select a;
            
             //.Where(pid.Contains(  n=>n.Id) );
             //IQueryable<WipStore> WipStore = listWipStore;
             var query = from u in listWipStore
                         select new
                         {
                             Id = u.Id,
                             SupplierName = u.SupplierName,
                             MatterName = u.MatterName,
                             Paydate = u.Paydate,
                             Uduser = u.Uduser,
                             Aduser = u.Aduser,
                             Enabled = u.Enabled,
                             DeliverId=u.DeliverId

                         };
             var ff = query.ToList();
             return query;
        }

        #endregion

        #region IWipStoreRepository 成员


        public IEnumerable SelectWipStoreForId(string pid, Paging paging)
        {
            IQueryable<WipStore> listWipStore = null;
            listWipStore = base.GetList();
            listWipStore = from a in listWipStore where pid.Contains(a.DeliverId) select a;

            //.Where(pid.Contains(  n=>n.Id) );
            //IQueryable<WipStore> WipStore = listWipStore;
            var query = from u in listWipStore
                        select new
                        {
                            Id = u.Id,
                            SupplierName = u.SupplierName,
                            MatterName = u.MatterName,
                            Paydate = u.Paydate,
                            Uduser = u.Uduser,
                            Aduser = u.Aduser,
                            Enabled = u.Enabled,
                            DeliverId = u.DeliverId

                        };
            var ff = query.ToList();
            paging.total = query.Count();
            return query;
        }

        #endregion

        #region IWipStoreRepository 成员


        public IEnumerable SelectWipStoreForBthSelect(string qty, string pdtID, Paging paging)
        {
            IQueryable<WipStore> list = base.GetList().Where(a => a.Enabled == true && a.DelFlag == "0"&& a.MatterName==pdtID);
            var query = from u in list
                        select new
                        {
                            Id = u.Id,
                            SupplierName = u.SupplierName,
                            MatterName = u.MatterName,
                            Paydate = u.Paydate,
                            Uduser = u.Uduser,
                            Aduser = qty,
                            Enabled = u.Enabled,
                            DeliverId = u.DeliverId

                        };
            var ff = query.ToList();
            paging.total = query.Count();
            return query;
        }

        #endregion
    }
}
