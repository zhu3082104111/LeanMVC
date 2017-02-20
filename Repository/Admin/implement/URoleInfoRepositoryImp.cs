using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
namespace Repository
{
    public class URoleInfoRepositoryImp : AbstractRepository<DB, URoleInfo>, IURoleInfoRepository
    {

        public IEnumerable<Model.URoleInfo> getAllURoleInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<URoleInfo> getAllURoleInfoAsQueryable()
        {
            return base.GetList();
        }


        public IQueryable<URoleInfo> getAllURoleInfoAsQueryableOrderBy()
        {
            return base.GetList().OrderBy(a => a.RId);
            //return base.GetList<string>(a=>a.RId);
        }

        public IQueryable<URoleInfo> getURoleInfoByIdAsQueryableOrderBy(string rid)
        {
            return (IQueryable<URoleInfo>)base.GetEntityById(rid);
        }

        public URoleInfo getURoleInfoById(string rid)
        {
            return base.GetEntityById(rid);
        }


        public bool addURoleInfo(URoleInfo r)
        {
            return base.Add(r);
        }

        public bool updateURoleInfo(URoleInfo r)
        {
            return base.Update(r);
        }

        public bool deleteURoleInfo(URoleInfo r)
        {
            return base.Delete(r);
        }
        //多表查询
        public IQueryable<URoleInfo> selectTest(URoleInfo r)
        {

            //var qury=from a in  Db.URInfo
            //join b in Db.URoleInfo
            //on a.RId equals b.RId
            //join c in Db.URInfo
            //on a.UId equals c.UId  where r.RId==c.RId        
            // select new{b.RName, b.RId};


            //return (IQueryable<URoleInfo>)qury;
            return null;
        }

        public bool deleteURoleInfoBySQL(URoleInfo r)
        {
            return base.ExecuteStoreCommand("delete from BI_URoleInfo  where UId={0}", r.UId);
        }
    }
}
