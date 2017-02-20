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
    public class UserInfo : Entity
    {

        public UserInfo()
        {
 
            Enabled = "1";
            URoleInfo = new List<URoleInfo>();
            UDepartment = new List<UDepartment>();
            Department = new List<Department>();
            UserInfoLog =new List<UserInfoLog>();
            

        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None),Required(ErrorMessage = "请输入用户名")]
        public string UId { set; get; }
        [Required(ErrorMessage = "请输入用户名"), StringLength(50)]
        public string UName { set; get; }
        [Required(ErrorMessage = "请输入密码"), StringLength(70)]
        [DataType(DataType.Password)]
        public string UPwd { set; get; }
        [Required, StringLength(50)]
        public string RealName { set; get; }
        [StringLength(5),ScaffoldColumn(false)]
        public string Sex { set; get; }
        [Display(Name = "Sex")]
        [DataType("DropDownList")]
        [HiddenInput]
        public List<string> Sexs { get; set; }    
        [StringLength(50)]
        public string Character { set; get; }
        [StringLength(20)]
        public string Tel { set; get; }
        [StringLength(20)]
        public string Email { set; get; }
        [StringLength(20)]
        public string QQ { set; get; }
        [StringLength(20)]
        public string WangWang { set; get; }
        [StringLength(50)]
        public string CompanyName { set; get; }
        [StringLength(500)]
        public string CompanyInfo { set; get; }
        [StringLength(30)]
        public string Bankname { set; get; }
        [StringLength(50)]
        public string BankAccount { set; get; }
        [StringLength(200)]
        public string Address { set; get; }
        [Column(TypeName = "char"), StringLength(1)]
        public string Enabled { get; set; }
        [StringLength(200)]
        public string Description { set; get; }
       /* [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/

        [Display(Name = "Department")]
        [DataType("MultiSelectList")]
        public List<string> DeptIds { get; set; }

        public virtual ICollection<UDepartment> UDepartment { get; set; }


        //角色
        [Display(Name = "Role")]
        [DataType("MultiSelectList")]
        public List<string> RIds { get; set; }

        public virtual ICollection<URoleInfo> URoleInfo { get; set; }

        //用户操作记录
        public virtual ICollection<UserInfoLog> UserInfoLog { get; set; }

        public virtual ICollection<Department> Department { get; set; }

        

    }

    
}
