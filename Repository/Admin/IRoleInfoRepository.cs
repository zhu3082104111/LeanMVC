using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IRoleInfoRepository : IRepository<RoleInfo>
    {
       IEnumerable<RoleInfo> getAllRoleInfo();

       IQueryable<RoleInfo> getAllRoleInfoAsQueryable();

       IQueryable<RoleInfo> getAllRoleInfoAsQueryableOrderBy();

       RoleInfo getRoleInfoById(string rid);


       bool addRoleInfo(RoleInfo r);

       bool updateRoleInfo(RoleInfo r);

       bool deleteRoleInfo(RoleInfo r);

       bool deleteRoleInfoBySQL(RoleInfo r);

    }
}
