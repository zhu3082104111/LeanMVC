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
    public class RoleInfo : Entity
    {

        public RoleInfo()
        {;
            Enabled = true;
        }

        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string RId { set; get; }
        [Required, StringLength(20)]
        public string RName { set; get; }
        [StringLength(50)]
        public string RDesc { set; get; }
        public bool Enabled { get; set; }
       /* [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [HiddenInput]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

        public virtual ICollection<RoleChainInfo> RoleChainInfo { get; set; }

        public virtual ICollection<URoleInfo> URoleInfo { get; set; }

      

    }
}
