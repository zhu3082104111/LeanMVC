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
    public class UDepartment : Entity
    {
        public UDepartment()
        {

          
            Enabled = true;
        }

        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Id{ set; get; }
        [Required, StringLength(20)]
        public string UId { set; get; }
        [StringLength(50)]
        public string DeptId { set; get; }
        [StringLength(50)]
        public string Remark { set; get; }
        public bool Enabled { set; get; }
       /* [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

        public virtual Department Department { get; set; }

      

    }
}
