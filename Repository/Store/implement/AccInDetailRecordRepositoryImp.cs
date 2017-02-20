/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInDetailRecordRepositoryImp.cs
// 文件功能描述：
//            附件库入库履历详细Repository实现
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
    public class AccInDetailRecordRepositoryImp : AbstractRepository<DB, AccInDetailRecord>, IAccInDetailRecordRepository
    {
        #region IAccInDetailRecordRepository 成员（获取附件库入库履历详细对象）


        public AccInDetailRecord SelectAccInDetailRecord(AccInDetailRecord accInDetailRecord)
        {
            return base.First(a => a.McIsetInListID == accInDetailRecord.McIsetInListID && a.PdtID == accInDetailRecord.PdtID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IAccInDetailRecordRepository 成员(获得附件库入库履历详细List对象（yc添加））


        public IEnumerable<AccInDetailRecord> GetAccInDetailRecordForList(AccInDetailRecord accInDetailRecord)
        {
            return base.GetList().Where(a => a.McIsetInListID == accInDetailRecord.McIsetInListID && a.DelFlag == "0" && a.EffeFlag=="0");
        }

        #endregion

        #region IAccInDetailRecordRepository 成员（删除附件库入库履历详细数据（yc添加））


        public bool AccInDetailRecordForDel(AccInDetailRecord accInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_ACC_IN_DETAIL_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where MC_ISET_IN_LIST_ID={2} and PDT_ID={3} ",DateTime.Now, accInDetailRecord.DelUsrID, accInDetailRecord.McIsetInListID, accInDetailRecord.PdtID);
        }

        #endregion

        #region IAccInDetailRecordRepository 成员（附件库入库修改履历详细数据（yc添加））


        public bool AccInRecordForUpdate(AccInDetailRecord accInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_ACC_IN_DETAIL_RECORD set QTY={0},NOTAX_AMT={1},UPD_USR_ID={2},UPD_DT={3}, where MC_ISET_IN_LIST_ID={4} and PDT_ID={5} ", accInDetailRecord.Qty, accInDetailRecord.NotaxAmt, accInDetailRecord.UpdUsrID,DateTime.Now, accInDetailRecord.McIsetInListID, accInDetailRecord.PdtID);
        }

        #endregion
    }
}
