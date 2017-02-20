/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInDetailRecordRepositoryImp.cs
// 文件功能描述：
//            在制品库入库履历详细Repository实现
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WipInDetailRecordRepositoryImp : AbstractRepository<DB, WipInDetailRecord>, IWipInDetailRecordRepository
    {


        #region IWipInDetailRecordRepository 成员（获取在制品库入库履历详细对象）

        public WipInDetailRecord SelectWipInDetailRecord(WipInDetailRecord wipInDetailRecord)
        {
            return base.First(a => a.TecnPdtInID == wipInDetailRecord.TecnPdtInID && a.PdtID == wipInDetailRecord.PdtID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IWipInDetailRecordRepository 成员（删除功能）


        public bool WipInDetailRecordForDel(WipInDetailRecord wipInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_WIP_IN_DETAIL_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where TECN_PDT_IN_ID={2} and PDT_ID={3} ", DateTime.Now, wipInDetailRecord.DelUsrID, wipInDetailRecord.TecnPdtInID, wipInDetailRecord.PdtID);
        }

        #endregion

        #region IWipInDetailRecordRepository 成员（获得在制品库入库履历详细List数据）


        public IEnumerable<WipInDetailRecord> GetWipInDetailRecordForList(WipInDetailRecord wipInDetailRecord)
        {
            return base.GetList().Where(a => a.TecnPdtInID == wipInDetailRecord.TecnPdtInID && a.DelFlag == "0" && a.EffeFlag=="0");
        }
        #endregion

        #region IWipInDetailRecordRepository 成员（在制品库入库履历修改功能）


        public bool WipInRecordForUpdate(WipInDetailRecord wipInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_WIP_IN_DETAIL_RECORD set QTY={0},NOTAX_AMT={1},UPD_USR_ID={2},UPD_DT={3}, where TECN_PDT_IN_ID={4} and PDT_ID={5} ", wipInDetailRecord.Qty, wipInDetailRecord.NotaxAmt, wipInDetailRecord.UpdUsrID, wipInDetailRecord.UpdDt, wipInDetailRecord.TecnPdtInID, wipInDetailRecord.PdtID);
        }

        #endregion
    }
}
