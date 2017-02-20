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
    public class MenuInfoRepositoryImp : AbstractRepository<DB, MenuInfo>, IMenuInfoRepository
    {

        public IEnumerable<Model.MenuInfo> getAllMenuInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<MenuInfo> getAllMenuInfoAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<MenuInfo> getAllMenuInfoAsQueryableOrderBy()
        {
            return base.GetList().Where(a => a.Enabled == true).OrderBy(a => a.SystemId);
            //return base.GetList<string>(a => a.Enabled == true, a => a.SystemId);
        }

        public MenuInfo getMenuInfoById(string mid)
        {
            return base.GetEntityById(mid);
        }


        public bool addMenuInfo(MenuInfo m)
        {
            return base.Add(m);
        }

        public bool updateMenuInfo(MenuInfo m)
        {
            return base.Update(m);
        }

        public bool deleteMenuInfo(MenuInfo m)
        {
            return base.Delete(m);
        }


        public bool deleteMenuInfoBySQL(MenuInfo m)
        {
            return base.ExecuteStoreCommand("delete from BI_MenuInfo  where MId={0}", m.MId);
        }
    }
}
