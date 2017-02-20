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
    [Serializable, Table("BI_WipStore")]
    public class WipStore : Entity
    {
        public WipStore()
        {
            Addate = DateTime.Now;
            Uddate = DateTime.Now;
            Paydate = DateTime.Now;
            Enabled = true;
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        [StringLength(50)]
        public string DeliverId { set; get; }
        [StringLength(50)]
        public string McIsetInListID { set; get; }
        [StringLength(50)]
        public string SupplierName { set; get; }
        [StringLength(50)]
        public string MatterName { set; get; }
        [StringLength(50)]
        public string WhID { set; get; }//仓库ID
        [StringLength(50)]
        public string WhPosiID { set; get; }//仓位
        [StringLength(50)]
        public string InMvCls { set; get; }//入库移动区分
        [StringLength(50)]
        public string WhkpID { set; get; }//仓管员ID
        public DateTime? Paydate { set; get; }
        public bool Enabled { get; set; }
        public DateTime? Addate { set; get; }
        [StringLength(20)]
        public string Aduser { set; get; }
        public DateTime? Uddate { set; get; }
        [StringLength(20)]
        public string Uduser { set; get; }
         //质检开始日期
        public DateTime? StartDt { get; set; }
         //质检结束日期
         public DateTime? EndDt { get; set; }
        


    }
}
