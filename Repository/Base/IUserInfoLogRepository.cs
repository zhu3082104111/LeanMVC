using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IUserInfoLogRepository : IRepository<UserInfoLog>
    {
       IEnumerable<UserInfoLog> getAllUserInfoLog();

       IQueryable<UserInfoLog> getAllUserInfoLogAsQueryable();

       IQueryable<UserInfoLog> getAllUserInfoLogAsQueryableOrderBy(string uid);

       UserInfoLog getUserInfoLogById(string uid);



       bool addUserInfoLog(UserInfoLog u);

       bool updateUserInfoLog(UserInfoLog u);

       bool deleteUserInfoLog(UserInfoLog u);

       bool deleteUserInfoLogBySQL(UserInfoLog u);

    }
}
