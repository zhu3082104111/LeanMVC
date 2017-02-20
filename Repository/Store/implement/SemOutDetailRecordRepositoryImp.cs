using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;

namespace Repository
{
    public class SemOutDetailRecordRepositoryImp : AbstractRepository<DB, SemOutDetailRecord>, ISemOutDetailRecordRepository
    {
        /// <summary>
        /// 半成品库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="semOutDetailRecord">在制品出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        public SemOutDetailRecord SelectSemOutDetailRecord(SemOutDetailRecord semOutDetailRecord, string bthID)
        {
            return base.First(a => a.TecnProductOutID == semOutDetailRecord.TecnProductOutID && a.BatchID == bthID && a.ProductID == semOutDetailRecord.ProductID && a.DelFlag == "0" && a.EffeFlag == "0");
        }
    }
}
