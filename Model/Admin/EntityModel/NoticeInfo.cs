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
    public class NoticeInfo : Entity
    {
        protected NoticeInfo()
        {
        }
     
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string ID{ set; get; }       
        [StringLength(20)]
        public string NoticeName { set; get; }

        public int State { set; get; }
       /* [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; } */

      

    }
}
