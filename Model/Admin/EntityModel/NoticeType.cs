using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Model
{
    [Serializable]
    public class NoticeType : Entity
    {

     
        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.None),StringLength(50)]
        public string NoticeTypeID { set; get; }

        public int TypeName { set; get; }
        [StringLength(50)]
        public string Rguid { set; get; }

      

    }
}
