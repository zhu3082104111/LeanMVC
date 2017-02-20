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
    public class DepartType : Entity
    {
   
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string ID{ set; get; }
        [StringLength(50)]
        public string DepartID { set; get; }
        [StringLength(50)]
        public string DGuid { set; get; }


      

    }
}
