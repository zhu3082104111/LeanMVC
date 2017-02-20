using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    [Serializable]
    public class Entity
    {
        public Entity()
        {

            CreDt = DateTime.Now;
            EffeFlag = Constant.GLOBAL_DELFLAG_ON;
            DelFlag = Constant.GLOBAL_DELFLAG_ON;

        }

        #region Edit By Wang Gang @ 20140109：为数据库每张表添加自增长字段。
        //修 改 人：汪罡
        //修改日期：2014-01-09
        //修改原因：为数据库每张表添加自增长字段。
        /// <summary>
        /// 自增字段
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "AUTO_NO", TypeName = "bigint")]
        [Required]
        public long AutoNo { get; set; }
        #endregion

        [StringLength(1), Column(name: "EFFE_FLG", TypeName = "char")]
        public string EffeFlag { get; set; }

        [StringLength(1), Column(name: "DEL_FLG", TypeName = "char")]
        public string DelFlag { get; set; }

        [Column("CRE_USR_ID")]
        [StringLength(20)]
        public string CreUsrID { get; set; }

        [Column("CRE_DT")]
        public DateTime CreDt { get; set; }

        [Column("UPD_USR_ID")]
        [StringLength(20)]
        public string UpdUsrID { get; set; }

        [Column("UPD_DT")]
        public DateTime? UpdDt { get; set; }

        [Column("DEL_USR_ID")]
        [StringLength(20)]
        public string DelUsrID { get; set; }

        [Column("DEL_DT")]
        public DateTime? DelDt { get; set; }
    }
}
