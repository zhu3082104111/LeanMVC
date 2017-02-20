using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
namespace Repository
{
    public class ChainInfoRepositoryImp : AbstractRepository<DB, ChainInfo>, IChainInfoRepository
    {

        public IEnumerable<Model.ChainInfo> getAllChainInfo()
        {
            return base.GetList().ToList();
        }


        public IQueryable<ChainInfo> getAllChainInfoAsQueryable()
        {
            return base.GetList();
        }
        public IQueryable<ChainInfo> getAllChainInfoAsQueryableOrderBy()
        {
            return base.GetList().OrderBy(a=>a.Id);
            //return base.GetList<string>(a => a.Id);
        }

        public ChainInfo getChainInfoById(string uid)
        {
            return base.GetEntityById(uid);
        }


        public bool addChainInfo(ChainInfo u)
        {
            return base.Add(u);
        }

        public bool updateChainInfo(ChainInfo u)
        {
            return base.Update(u);
        }

        public bool deleteChainInfo(ChainInfo u)
        {
            return base.Delete(u);
        }


        public bool deleteChainInfoBySQL(ChainInfo u)
        {
            return base.ExecuteStoreCommand("delete from BI_ChainInfoLog  where UId={0}", u.Id);
        }
    }
}
