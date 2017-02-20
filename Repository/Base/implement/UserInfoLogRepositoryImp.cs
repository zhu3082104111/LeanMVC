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
    public class UserInfoLogRepositoryImp : AbstractRepository<DB, UserInfoLog>, IUserInfoLogRepository
    {

        public IEnumerable<Model.UserInfoLog> getAllUserInfoLog()
        {
            return base.GetList().ToList();
        }


        public IQueryable<UserInfoLog> getAllUserInfoLogAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<UserInfoLog> getAllUserInfoLogAsQueryableOrderBy(string id) {

            //return base.GetList<string>(a=>a.Id==id,a=>a.Id);
            return null;
        }

        public UserInfoLog getUserInfoLogById(string uid)
        {
            return base.GetEntityById(uid);
        }


        public bool addUserInfoLog(UserInfoLog u)
        {
            return base.Add(u);
        }

        public bool updateUserInfoLog(UserInfoLog u)
        {
            return base.Update(u);
        }

        public bool deleteUserInfoLog(UserInfoLog u)
        {
            return base.Delete(u);
        }

        

        public bool deleteUserInfoLogBySQL(UserInfoLog u)
        {
            return base.ExecuteStoreCommand("delete from BI_UserInfoLog  where Id={0}", u.Id);
        }
    }
}
