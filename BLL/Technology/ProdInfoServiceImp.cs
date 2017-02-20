using Extensions;
using Model.Technology;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Model;


namespace BLL
{
    class ProdInfoServiceImp : AbstractService, IProdInfoService
    {
        private IProdInfoRepository iProdInfoRepository;

        public ProdInfoServiceImp(IProdInfoRepository paraIProdInfoRepository)
        {
            this.iProdInfoRepository = paraIProdInfoRepository;
        }

        public IEnumerable<VM_ProdInfoForTableProdInfo> GetProdInfoListService(VM_ProdInfoForSearchTableProdInfo paraPIFSTPI, Paging paraPage)
        {
            return iProdInfoRepository.GetProdInfoListRepository(paraPIFSTPI, paraPage);
        }



    }
}
