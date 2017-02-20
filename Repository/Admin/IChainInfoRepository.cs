using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IChainInfoRepository : IRepository<ChainInfo>
    {
       IEnumerable<ChainInfo> getAllChainInfo();

       IQueryable<ChainInfo> getAllChainInfoAsQueryable();

       IQueryable<ChainInfo> getAllChainInfoAsQueryableOrderBy();

       ChainInfo getChainInfoById(string uid);


       bool addChainInfo(ChainInfo u);

       bool updateChainInfo(ChainInfo u);

       bool deleteChainInfo(ChainInfo u);

       bool deleteChainInfoBySQL(ChainInfo u);

    }
}
