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
    public class EditInfo : Entity
    {
        protected EditInfo()
        {

        }
   
        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string EId { set; get; }
        [Required, StringLength(20)]
        public string EName { set; get; }
        [StringLength(20)]
        public string ActionName { set; get; }
        [StringLength(20)]
        public string SystemId { set; get; }
        [StringLength(50)]
        public string Remark { set; get; }
     /*   [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

      

    }
}
