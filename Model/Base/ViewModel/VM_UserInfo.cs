using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Base
{

    public class VM_UserInfoForSearch
    {
        public string UId { get; set; }
        //public string UName { get; set; }
        public string RealName { get; set; }
        public string Sex { get; set; }

        public VM_UserInfoForSearch()
        {
            this.RealName = "";
            this.Sex = "";
        }
    }
    


}
