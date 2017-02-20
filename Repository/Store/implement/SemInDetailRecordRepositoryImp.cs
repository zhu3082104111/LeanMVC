using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;

namespace Repository
{
    public class SemInDetailRecordRepositoryImp : AbstractRepository<DB, SemInDetailRecord>, ISemInDetailRecordRepository
    {

        #region ISemInDetailRecordRepository 成员（获取半成品库入库履历详细对象）

        public SemInDetailRecord SelectSemInDetailRecord(SemInDetailRecord semInDetailRecord)
        {
            return base.First(a => a.TecnPdtInId == semInDetailRecord.TecnPdtInId && a.PdtId == semInDetailRecord.PdtId && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region ISemInDetailRecordRepository 成员（入库履历删除功能）


        public bool SemInDetailRecordForDel(SemInDetailRecord semInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_SEM_IN_DETAIL_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where TECN_PDT_IN_ID={2} and PDT_ID={3} ", DateTime.Now, semInDetailRecord.DelUsrID, semInDetailRecord.TecnPdtInId, semInDetailRecord.PdtId);
        }

        #endregion

        #region ISemInDetailRecordRepository 成员（获得半成品库入库履历详细List数据）


        public IEnumerable<SemInDetailRecord> GetSemInDetailRecordForList(SemInDetailRecord semInDetailRecord)
        {
            return base.GetList().Where(a => a.TecnPdtInId == semInDetailRecord.TecnPdtInId && a.DelFlag == "0" && a.EffeFlag == "0");
        }
        #endregion
    }
}
