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
    public class UserInfoLog : Entity
    {
        public UserInfoLog() {
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        [MaxLength(100)]
        [Required]
        public string Url { get; set; }

        public string ChainInfoId { get; set; }

        public virtual ChainInfo ChainInfo { get; set; }

        public string RecordId { get; set; }


        public string SysUserId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        public string EnterpriseId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Ip { get; set; }
        [StringLength(50)]
        public string Remark { set; get; }
/*
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }

        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }*/


    }
}
