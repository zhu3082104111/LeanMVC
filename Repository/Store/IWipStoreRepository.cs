using System.Collections;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IWipStoreRepository : IRepository<WipStore>
    {
       IEnumerable<WipStore> getAllWipStore();

       IQueryable<WipStore> getAllWipStoreAsQueryable();

       IQueryable<WipStore> getAllWipStoreAsQueryableOrderBy();

       IQueryable<WipStore> getAllWipStoreAsQueryableOrderById(string id);

       WipStore getWipStoreById(string id);

       IEnumerable GetWipStoreBySearchByPage(WipStore wipstore, Extensions.Paging paging);

       IEnumerable GetWipStoreBySearchById(string Parameter_id);

       bool addWipStore(WipStore w);

       bool updateWipStore(WipStore w);

       bool deleteWipStore(WipStore w);

       bool updateWipStoreColumns(WipStore w);

       bool updateWipStoreBySQL(WipStore w);

       bool deleteWipStoreBySQL(WipStore w);

       IEnumerable SelectWipStore(string pid);

       IEnumerable SelectWipStoreForId(string pid, Extensions.Paging paging);

       IEnumerable SelectWipStoreForBthSelect(string qty, string pdtID, Extensions.Paging paging);


    }
}
