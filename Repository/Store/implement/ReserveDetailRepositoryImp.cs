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
    /// <summary>
    /// 仓库预约详细表
    /// M：梁龙飞
    /// </summary>
    public class ReserveDetailRepositoryImp : AbstractRepository<DB, ReserveDetail>, IReserveDetailRepository
    {

        #region IReserveDetailRepository 成员

        public bool UpdateReserveDetailForDel(ReserveDetail reserveDetail)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE_DETAIL set DEL_FLG='1', DEL_USR_ID='{0}', DEL_DT={1} where ORDE_BTH_DTAIL_LIST_ID={2} and BTH_ID={3}",reserveDetail.DelUsrID, DateTime.Now, reserveDetail.OrdeBthDtailListID, reserveDetail.BthID );
        }

        #endregion

        #region IReserveDetailRepository 成员


        public bool UpdateReserveDetailForUpdate(ReserveDetail reserveDetail)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE_DETAIL set OrderQty= {0}, UPD_USR_ID={1},UPD_DT={2} where ORDE_BTH_DTAIL_LIST_ID={3} and BTH_ID={4}", reserveDetail.OrderQty, reserveDetail.UpdUsrID, DateTime.Now, reserveDetail.OrdeBthDtailListID, reserveDetail.BthID);
        }

        #endregion

        /// <summary>
        /// 成品库出库登录更新领料单开具数量及已出库数量 陈健
        /// </summary>
        /// <param name="ordeBthDtailListID">预约批次详细单号</param>
        /// <param name="reserveDetail">更新数据集合</param>
        /// <returns>true</returns>
        public bool UpdateInReserveDetailFinOut(int ordeBthDtailListID,ReserveDetail reserveDetail)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE_DETAIL set PICK_ORDE_QTY={0},CMP_QTY=CMP_QTY+{1},UPD_USR_ID={2},UPD_DT={3} where ORDE_BTH_DTAIL_LIST_ID={4} and BTH_ID={5}", reserveDetail.PickOrdeQty, reserveDetail.CmpQty, reserveDetail.UpdUsrID, reserveDetail.UpdDt, ordeBthDtailListID,reserveDetail.BthID);
        }

        /// <summary>
        /// 根据预约批次详细单号到仓库预约详细表里检索 陈健
        /// </summary>
        /// <param name="ordeBthDtailListID">预约批次详细单号</param>
        /// <param name="batchID">批次号</param>
        /// <returns>仓库预约详细表检索数据集合</returns>
        public IQueryable<ReserveDetail> GetReserveDetailList(int ordeBthDtailListID, string batchID)
        {
            return base.GetList().Where(h => h.OrdeBthDtailListID.Equals(ordeBthDtailListID) && h.BthID.Equals(batchID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 在制品库出库登录修改领料单开具数量及已出库数量 陈健
        /// </summary>
        /// <param name="reserveDetail">更新数据集合</param>
        /// <returns></returns>
        public bool UpdateInReserveDetailQty(ReserveDetail reserveDetail)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE_DETAIL set PICK_ORDE_QTY=PICK_ORDE_QTY-{0},CMP_QTY=CMP_QTY+{1},UPD_USR_ID={2},UPD_DT={3} where ORDE_BTH_DTAIL_LIST_ID={4} and BTH_ID={5}", reserveDetail.PickOrdeQty, reserveDetail.CmpQty, reserveDetail.UpdUsrID, reserveDetail.UpdDt, reserveDetail.OrdeBthDtailListID, reserveDetail.BthID);
        }
        

        #region 基础功能：C:梁龙飞

        /// <summary>
        /// 根据预约详细单号检索仓库预约详细信息
        /// 没有值时返回空
        /// </summary>
        /// <param name="orderBthDtailListID"></param>
        /// <returns></returns>
        public IQueryable<ReserveDetail> GetReserveDtlByDtlID(int orderBthDtailListID)
        {
            try
            {
                IQueryable<ReserveDetail> finalData=GetAvailableList<ReserveDetail>().Where(a => a.OrdeBthDtailListID == orderBthDtailListID);
                return finalData;
            }
            catch (Exception)
            {
                return null;
            }
           
        }
        /// <summary>
        /// 获取最大的 【预约批次详细单号】
        /// </summary>
        /// <returns></returns>
        public int GetMaxBthDetailCode()
        {
            try
            {
                IQueryable<int> intL = from rd in GetAvailableList<ReserveDetail>()
                                       select rd.OrdeBthDtailListID;
                if (intL == null || intL.Count() == 0)
                {
                    return 0;
                }
                int maxID = intL.Max();
                return maxID;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public bool DeleteByDetailID(int batchDetailID)
        {
            return base.ExecuteStoreCommand("delete from MC_WH_RESERVE_DETAIL where ORDE_BTH_DTAIL_LIST_ID={0}", batchDetailID);
        }

        /// <summary>
        /// 获取某一订单详细锁定的物料数量和
        /// </summary>
        /// <param name="batchDetailID"></param>
        /// <returns></returns>
        public decimal GetQuantityByDetailID(int batchDetailID)
        {
            try
            {
                IQueryable<decimal> decQtt = from rd in GetAvailableList<ReserveDetail>().Where(a => a.OrdeBthDtailListID == batchDetailID)
                                             select rd.OrderQty;
                if (decQtt==null||decQtt.Count()==0)
                {
                    return 0;
                }
                decimal totQtt = decQtt.Sum();
                return totQtt;
            }
            catch (Exception)
            {
                return 0;
            }
            

        }
        #endregion

        #region IReserveDetailRepository 成员


        public ReserveDetail SelectReserveDetail(ReserveDetail reserveDetail)
        {
            return base.First(a => a.OrdeBthDtailListID == reserveDetail.OrdeBthDtailListID && a.BthID == reserveDetail.BthID && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        #endregion

    }
}
