using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IMenuInfoRepository : IRepository<MenuInfo>
    {
       IEnumerable<MenuInfo> getAllMenuInfo();

       IQueryable<MenuInfo> getAllMenuInfoAsQueryable();

       MenuInfo getMenuInfoById(string mid);

       IQueryable<MenuInfo> getAllMenuInfoAsQueryableOrderBy();

       bool addMenuInfo(MenuInfo m);

       bool updateMenuInfo(MenuInfo m);

       bool deleteMenuInfo(MenuInfo m);

       bool deleteMenuInfoBySQL(MenuInfo m);

    }
}
