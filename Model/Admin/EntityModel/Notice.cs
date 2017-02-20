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
    public class Notice : Entity
    {
        public Notice()
        {

        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string NoticeID { set; get; }

        public int TypeID { set; get; }
        [MaxLength(100)]
        [AdditionalMetadata("Size", 50)]
        [Required]
        public string Title { set; get; }
        [MaxLength]
        [DataType(DataType.Html)]
        [Required]
        [AllowHtml]
        public string Info { set; get; }
        public bool Enabled { get; set; }
      /*  [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/


    }
}
