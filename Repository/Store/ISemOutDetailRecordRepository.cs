using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface ISemOutDetailRecordRepository : IRepository<SemOutDetailRecord>
    {
        /// <summary>
        /// 半成品库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="semOutDetailRecord">在制品出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        SemOutDetailRecord SelectSemOutDetailRecord(SemOutDetailRecord semOutDetailRecord, string bthID);
    }
}
