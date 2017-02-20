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
    public class MenuInfo : Entity
    {
        protected MenuInfo()
        {
            ControllerName = "Index";
            ActionName = "Index";
            Enabled = true;
            Display = true;

      
        }
   
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string MId { set; get; }
        [Required, StringLength(20)]
        public string SystemId { set; get; }
        [Required, StringLength(50)]
        public string MName { set; get; }

        [Required, StringLength(50)]
        public string AreaName { set; get; }

        [StringLength(50)]
        public string ControllerName{ set; get; }
        [StringLength(50)]
        public string ActionName { set; get; }
        [StringLength(50)]
        public string Parameter { set; get; }
        public bool Display { get; set; }
        public bool Enabled { get; set; }
       /* [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

        public virtual ICollection<ChainInfo> ChainInfo { get; set; }
      

    }
}
