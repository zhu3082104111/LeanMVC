using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
       IEnumerable<UserInfo> getAllUserInfo();

       IQueryable<UserInfo> getAllUserInfoAsQueryableOrderBy();

       IQueryable<UserInfo> getAllUserInfoAsQueryableOrderById(string uid);

       UserInfo getUserInfoById(string uid);


       bool addUserInfo(UserInfo u);

       bool updateUserInfo(UserInfo u);

       bool deleteUserInfo(UserInfo u);

       bool deleteUserInfoBySQL(UserInfo u);


       IQueryable<UserInfo> getAllUserInfoAsQueryable();

    }
}
