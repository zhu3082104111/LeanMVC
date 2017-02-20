using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Model
{
    [Serializable]
    public class ChainInfo : Entity
    {
         protected ChainInfo()
        {
          
        }

        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        public string MId { set; get; }
        public string EId { set; get; }
        [StringLength(50)]
        public string Remark { set; get; }
        public bool State { set; get; }
        /*[HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

        public virtual EditInfo EditInfo{ get; set; }

        public virtual MenuInfo MenuInfo { get; set; }

        public virtual ICollection<RoleChainInfo> RoleChainInfo { get; set; }

      

    }
}
