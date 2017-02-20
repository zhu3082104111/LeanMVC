/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccOutDetailRecordRepositoryImp.cs
// 文件功能描述：
//            附件库出库履历详细Repository实现
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
    public class AccOutDetailRecordRepositoryImp : AbstractRepository<DB, AccOutDetailRecord>, IAccOutDetailRecordRepository
    {
        #region IAccOutDetailRecordRepository 成员（附件库出库履历详细对象List）

        public IEnumerable<AccOutDetailRecord> GetAccOutDetailRecordForList(AccOutDetailRecord accOutDetailRecord)
        {
            return base.GetList().Where(a => a.SaeetID == accOutDetailRecord.SaeetID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IAccOutDetailRecordRepository 成员（附件库出库履历删除）

        public bool AccOutDetailRecordForDel(AccOutDetailRecord accOutDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_ACC_OUT_DETAIL_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where SAEET_ID={2} and PICK_LIST_DET_NO={3} and BTH_ID={4}", DateTime.Now, accOutDetailRecord.DelUsrID, accOutDetailRecord.SaeetID, accOutDetailRecord.PickListDetNo, accOutDetailRecord.BthID);
        }

        #endregion

        /// <summary>
        /// 附件库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="accOutDetailRecord">附件库出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        public AccOutDetailRecord SelectAccOutDetailRecord(AccOutDetailRecord accOutDetailRecord, string bthID)
        {
            return base.First(a => a.SaeetID == accOutDetailRecord.SaeetID && a.BthID == bthID && a.PdtID == accOutDetailRecord.PdtID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

    }
}
