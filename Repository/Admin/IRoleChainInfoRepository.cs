using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoleChainInfoRepository : IRepository<RoleChainInfo>
    {
       IEnumerable<RoleChainInfo> getAllRoleChainInfo();

       IQueryable<RoleChainInfo> getAllRoleChainInfoAsQueryable();

       IQueryable<RoleChainInfo> getAllRoleChainInfoAsQueryableOrderBy();

       RoleChainInfo getRoleChainInfoById(string id);

       RoleChainInfo getRoleChainInfoByRId(string rid);


       bool addRoleChainInfo(RoleChainInfo r);

       bool updateRoleChainInfo(RoleChainInfo r);

       bool deleteRoleChainInfo(RoleChainInfo r);

       bool deleteRoleChainInfoBySQL(RoleChainInfo r);

    }
}
