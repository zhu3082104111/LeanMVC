using Extensions;
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

namespace Repository
{
    public class UserInfoRepositoryImp : AbstractRepository<DB, UserInfo>, IUserInfoRepository
    {

        public IEnumerable<Model.UserInfo> getAllUserInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<UserInfo> getAllUserInfoAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<UserInfo> getAllUserInfoAsQueryableOrderById(string uid)
        {
            return base.GetList().Where(a => a.UId == uid && a.Enabled.Equals("1")).OrderBy(a => a.UId);
            //return base.GetList<string>(a => a.UId == uid && a.Enabled.Equals("1"), a => a.UId);
        }
        
        public IQueryable<UserInfo> getAllUserInfoAsQueryableOrderBy()
        {
            return base.GetList().Where(a => a.Enabled.Equals("1")).OrderBy(a => a.UId);
            //return base.GetList<string>(a => a.Enabled.Equals("1"), a => a.UId);
        }

        public UserInfo getUserInfoById(string uid)
        {
            return base.GetEntityById(uid);
        }


        public bool addUserInfo(UserInfo u)
        {
            return base.Add(u);
        }

        public bool updateUserInfo(UserInfo u)
        {
            return this.Update(u);
        }

        public bool deleteUserInfo(UserInfo u)
        {
            return base.Delete(u);
        }

        public override bool Update(UserInfo userInfo)
        {

            /*try
            {
                ObservableCollection<UserInfo> tt = GetSet().Local;
                IEnumerable<UserInfo> cc = tt.AsEnumerable<UserInfo>();

                for (int i = 0; i < cc.Count(); i++)
                {
                    Db.Entry<UserInfo>(cc.ElementAt(i)).State = EntityState.Detached;
                }

                GetSet().Attach(userInfo);

                if (userInfo != (UserInfo)null)
                {
                    Db.SetModified<UserInfo>(userInfo);
                }
                //db.Entry<T>(entity).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return false;
            }*/
            return false;
        }

        public bool deleteUserInfoBySQL(UserInfo u)
        {
            return base.ExecuteStoreCommand("delete from BI_UserInfo  where UId={0}", u.UId);
        }

        /// <summary>
        /// 根据检索条件检索
        /// </summary>
        /// <param name="searcher">检索条件</param>
        /// <returns></returns>
        public override IEnumerable<Searcher> GetAutoSearchData(Searcher searcher)
        {
            var query = from u in GetList()
                where u.UId.Contains(searcher.Keyword) || u.RealName.Contains(searcher.Keyword)
                select new Searcher()
                {
                    Id = u.UId,Name = u.RealName
                };
            return query.ToPageList("Id",new Paging(){page = 1,rows = 10}).ToList();
        }
    }
}
