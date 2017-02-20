using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Extensions;

namespace App_UI.Areas.Login.Models
{
    public class LoginModel
    {   
        [Display(Name = "UId")]   
        [Required(ErrorMessage="请输入用户名")]
        public string UId { get; set; }

        [Display(Name = "UPwd")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入密码")]
        public string UPwd { get; set; }

        [Display(Name = "Remember")]
        public bool Remember { get; set; }
    }
}