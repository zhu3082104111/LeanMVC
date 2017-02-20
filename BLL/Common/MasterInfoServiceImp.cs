using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace BLL
{
    class MasterInfoServiceImp:AbstractService,IMasterInfoService
    {
        private IMasterDefiInfoRepository masterInfo;

        public MasterInfoServiceImp(IMasterDefiInfoRepository masterInfo) 
        {
            this.masterInfo = masterInfo;
        }



        public IEnumerable<Model.MasterDefiInfo> GetOneSection(string section)
        {
            return masterInfo.GetListByConditionWithOrderBy(a=>a.SectionCd.Equals(section),n=>n.SNo);
        }




        public Model.MasterDefiInfo GetMasterDefiInfo(string section, string value)
        {
            return masterInfo.Get(n => n.SectionCd.Equals(section)&&n.AttrValue.Equals(value) );
        }
    }
}
