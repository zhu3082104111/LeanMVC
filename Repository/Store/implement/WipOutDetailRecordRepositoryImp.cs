/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipOutDetailRecordRepositoryImp.cs
// 文件功能描述：
//            在制品库出库履历详细Repository实现
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
using System.Collections;

namespace Repository
{
    public class WipOutDetailRecordRepositoryImp : AbstractRepository<DB, WipOutDetailRecord>, IWipOutDetailRecordRepository
    {
        /// <summary>
        /// 在制品库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="wipOutDetailRecord">在制品出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        public WipOutDetailRecord SelectWipOutDetailRecord(WipOutDetailRecord wipOutDetailRecord,string bthID)
        {
            return base.First(a => a.TecnPdtOutID == wipOutDetailRecord.TecnPdtOutID && a.BthID == bthID && a.PdtID == wipOutDetailRecord.PdtID && a.DelFlag == "0" && a.EffeFlag == "0");
        }


        #region IWipOutDetailRecordRepository 成员（附件库出库履历详细对象List）


        public IEnumerable<WipOutDetailRecord> GetWipOutDetailRecordForList(WipOutDetailRecord wipOutDetailRecord)
        {
            return base.GetList().Where(a => a.TecnPdtOutID == wipOutDetailRecord.TecnPdtOutID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IWipOutDetailRecordRepository 成员（在制品库出库履历删除（yc添加））


        public bool WipOutDetailRecordForDel(WipOutDetailRecord wipOutDetailRecord)
        {
           return base.ExecuteStoreCommand("update MC_WH_WIP_OUT_DETAIL_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where TECN_PDT_OUT_ID={2} and PICK_LIST_DET_NO={3} and BTH_ID={4}", DateTime.Now, wipOutDetailRecord.DelUsrID, wipOutDetailRecord.TecnPdtOutID, wipOutDetailRecord.PickListDetNo, wipOutDetailRecord.BthID);
        }

        #endregion
    }
}
