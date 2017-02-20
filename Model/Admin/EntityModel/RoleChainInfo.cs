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
    public class RoleChainInfo : Entity
    {
        public RoleChainInfo()
        {
       
        }

        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        [Required, StringLength(50)]
        public string RId { set; get; }
        [Required, StringLength(20),ForeignKey("ChainInfo")]
        public string CId { set; get; }
        [StringLength(20)]
        public string Remark { set; get; }
        public bool State { set; get; }
      /*  [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }
        */
        public virtual RoleInfo RoleInfo { get; set; }

        public virtual ChainInfo ChainInfo { get; set; }

      

    }
}
