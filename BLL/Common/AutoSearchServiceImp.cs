using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;
using Repository.Base;

namespace BLL.Common
{
    public class AutoSearchServiceImp:IAutoSearchService
    {

        private IUserInfoRepository userInfoRepository;
        private ITeamRepository iTeamRepository;

        public AutoSearchServiceImp(IUserInfoRepository userInfoRepository, ITeamRepository iTeamRepository)
        {
            this.userInfoRepository = userInfoRepository;
            this.iTeamRepository = iTeamRepository;
        }


        /// <summary>
        /// 按条件检索
        /// </summary>
        /// <param name="searcher">检索条件</param>
        /// <returns></returns>
        public List<Searcher> GetBySearcher(Searcher searcher)
        {
            var list=new List<Searcher>();
            switch (searcher.Type)
            {
                case Constant.AutoSearchType.USER:
                    list = userInfoRepository.GetAutoSearchData(searcher).ToList();break;
                case Constant.AutoSearchType.TEAM:
                    list = iTeamRepository.GetAutoSearchData(searcher).ToList();break;
                default:
                    break;
            }
            return list;
        }
    }
}
