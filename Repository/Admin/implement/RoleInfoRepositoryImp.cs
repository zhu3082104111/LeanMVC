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
    public class RoleInfoRepositoryImp : AbstractRepository<DB, RoleInfo>, IRoleInfoRepository
    {

        public IEnumerable<Model.RoleInfo> getAllRoleInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<RoleInfo> getAllRoleInfoAsQueryable()
        {
            return base.GetList() ;
        }


        public IQueryable<RoleInfo> getAllRoleInfoAsQueryableOrderBy()
        {
            return base.GetList().Where(a => a.Enabled == true).OrderBy(a => a.RId);
            //return base.GetList<string>(a => a.Enabled == true, a => a.RId);
        }

        public IQueryable<RoleInfo> getRoleInfoByIdAsQueryableOrderBy(string rid)
        {
            return (IQueryable<RoleInfo>)base.GetEntityById(rid);
        }

        public RoleInfo getRoleInfoById(string rid)
        {
            return base.GetEntityById(rid);
        }


        public bool addRoleInfo(RoleInfo r)
        {
            return base.Add(r);
        }

        public bool updateRoleInfo(RoleInfo r)
        {
            return base.Update(r);
        }

        public bool deleteRoleInfo(RoleInfo r)
        {
            return base.Delete(r);
        }
        //多表查询
        public IQueryable<RoleInfo> selectTest(RoleInfo r)
        {

            //var qury=from a in  Db.URInfo
            //join b in Db.RoleInfo
            //on a.RId equals b.RId
            //join c in Db.URInfo
            //on a.UId equals c.UId  where r.RId==c.RId        
            // select new{b.RName, b.RId};


            //return (IQueryable<RoleInfo>)qury;
            return null;
        }

        public bool deleteRoleInfoBySQL(RoleInfo r)
        {
            return base.ExecuteStoreCommand("delete from BI_RoleInfo  where RId={0}", r.RId);
        }
    }
}
