using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IURoleInfoRepository : IRepository<URoleInfo>
    {
       IEnumerable<URoleInfo> getAllURoleInfo();

       IQueryable<URoleInfo> getAllURoleInfoAsQueryable();

       IQueryable<URoleInfo> getAllURoleInfoAsQueryableOrderBy();

       URoleInfo getURoleInfoById(string rid);


       bool addURoleInfo(URoleInfo r);

       bool updateURoleInfo(URoleInfo r);

       bool deleteURoleInfo(URoleInfo r);

       bool deleteURoleInfoBySQL(URoleInfo r);

    }
}
