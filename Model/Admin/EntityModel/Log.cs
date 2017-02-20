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
    public class Log
    {
        protected Log()
        {

            Id = Guid.NewGuid().ToString();

        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        [MaxLength(100)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Remark { set; get; }
        [HiddenInput]
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        [ScaffoldColumn(false)]
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }
    }
}
