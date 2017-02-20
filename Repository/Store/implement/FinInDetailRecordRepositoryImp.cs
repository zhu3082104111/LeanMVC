/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinInDetailRecordRepositoryImp.cs
// 文件功能描述：
//          内部成品库入库履历详细的Repository接口的实现
//      
// 修改履历：2013/11/24 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;

namespace Repository
{
    /// <summary>
    /// 内部成品库入库履历详细的Repository接口的实现
    /// </summary>
    public class FinInDetailRecordRepositoryImp : AbstractRepository<DB, FinInDetailRecord>, IFinInDetailRecordRepository
    {
        /// <summary>
        /// 更新入库履历详细表
        /// </summary>
        /// <param name="finInDetailRecord">更新数据集合</param>
        /// <returns>true</returns>
        public bool UpdateInFinInDetailRecord(FinInDetailRecord finInDetailRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_FIN_IN_DETAIL_RECORD set QTY={0},PRO_SCRAP_QTY={1},PRO_MATERSCRAP_QTY={2},PRO_OVER_QTY={3},RMRS={4},UPD_USR_ID={5},UPD_DT={6} where FS_IN_ID={7} and ISET_REP_ID={8} and PDT_ID={9} and TECN_PROCESS={10} and CLN_ODR_DTL={11}", finInDetailRecord.Quantity
                , finInDetailRecord.ProScrapQuantity, finInDetailRecord.ProMaterscrapQuantity, finInDetailRecord.ProOverQuantity, finInDetailRecord.Remarks,
                finInDetailRecord.UpdUsrID,finInDetailRecord.UpdDt,
                finInDetailRecord.FsInID, finInDetailRecord.IsetRepID, finInDetailRecord.ProductID, finInDetailRecord.TecnProcess, finInDetailRecord.ClientOrderDetail);
        }

       /// <summary>
       /// 入库履历详细表添加判断
       /// </summary>
       /// <param name="productWarehouseID">成品交仓单号</param>
       /// <param name="inRecordList">入库履历数据集合</param>
       /// <param name="i">行参数</param>
       /// <returns>入库履历详细添加判断数据集合</returns>
        public IQueryable<FinInDetailRecord> GetFinInRecordDetailList(string productWarehouseID,Dictionary<string, string>[] inRecordList, int i)
        {
            var clientOrder = inRecordList[i]["PlanID"].Split('/');
            var a = clientOrder[0];
            var b = clientOrder[1];
            var c = inRecordList[i]["ProductCheckID"];
            var d = inRecordList[i]["OrderProductID"];

            return base.GetList().Where(h => h.FsInID.Equals(productWarehouseID) && h.IsetRepID.Equals(c) &&
                h.ProductID.Equals(d) && h.TecnProcess.Equals(a) &&
                h.ClientOrderDetail.Equals(b) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

    }
}
