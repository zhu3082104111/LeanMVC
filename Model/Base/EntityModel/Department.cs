using System;
using System.Web;
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
    public class Department : Entity
    {
        public Department()
        {
            DeptId = Guid.NewGuid().ToString();
           
        }
     
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [HiddenInput]
        public string DeptId { set; get; }
        [StringLength(20)]
        public string Department1 { set; get; }
        [StringLength(50)]
        public string DeptName { set; get; }
        [StringLength(20),ScaffoldColumn(false)]
        public string DeptParentId { set; get; }
        [StringLength(20)]
        [Display(Name = "ManagerId")]
        [DataType("DropDownList")]
        [Required]
        [ForeignKey("UserInfo")]
        public string ManagerIds { get; set; }
        [ScaffoldColumn(false)]
        public virtual UserInfo UserInfo { get; set; }
        [StringLength(20)]
        public string Tel { set; get; }
        public bool Enabled { get; set; }
        /*[HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }
        */
        [Display(Name = "DeptParentId")]
        [DataType("SelectList")]
        public List<string> DeptIds { get; set; }


      

    }
}
