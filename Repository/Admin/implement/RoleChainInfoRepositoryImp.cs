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
    public class RoleChainInfoRepositoryImp : AbstractRepository<DB, RoleChainInfo>, IRoleChainInfoRepository
    {

        public IEnumerable<Model.RoleChainInfo> getAllRoleChainInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<RoleChainInfo> getAllRoleChainInfoAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<RoleChainInfo> getAllRoleChainInfoAsQueryableOrderBy()
        {
            return base.GetList().OrderBy(a => a.Id);
            //return base.GetList<string>(a => a.Id);
        }

        public RoleChainInfo getRoleChainInfoById(string id)
        {
            return base.GetEntityById(id);
        }

        public RoleChainInfo getRoleChainInfoByRId(string rid)
        {
            return base.Get(a => a.RId==rid);
        }

        public bool addRoleChainInfo(RoleChainInfo r)
        {
            return base.Add(r);
        }

        public bool updateRoleChainInfo(RoleChainInfo r)
        {
            return base.Update(r);
        }

        public bool deleteRoleChainInfo(RoleChainInfo r)
        {
            return base.Delete(r);
        }


        public bool deleteRoleChainInfoBySQL(RoleChainInfo r)
        {
            return base.ExecuteStoreCommand("delete from BI_RoleChainInfo where RId={0}", r.RId);
        }
    }
}
