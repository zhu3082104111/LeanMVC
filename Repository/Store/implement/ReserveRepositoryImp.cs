// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ReserveRepositoryImp.cs
// 文件功能描述：仓库预约表repository实现类
// 
// 创建标识：
//
// 修改标识：代东泽 20131226
// 修改描述：添加方法
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using Model;
using Model.Store;
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
    /// 仓库预约表repository实现类
    /// 代东泽 201226添加注释
    /// </summary>
    public class ReserveRepositoryImp : AbstractRepository<DB, Reserve>, IReserveRepository
    {
        public bool AddReserve(Reserve reserve) {

            return base.Add(reserve);
        }

        //public bool updateInReserveColumns(Reserve reserve)
        //{
        //    return this.Update(reserve, new string[] { "RECV_QTY" });
        //}

        public bool UpdateInReserveColumns(Reserve reserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE set RECV_QTY=RECV_QTY+{0} where WH_ID={1} and CLN_ODR_ID={2} and CLN_ODR_DTL={3} and PDT_ID={4}", reserve.RecvQty,reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        }

        /// <summary>
        /// 在制品库出库登录更新已出库数量 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns></returns>
        public bool UpdateInReserveCmpQty(Reserve reserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE set CMP_QTY={0},UPD_USR_ID={1},UPD_DT={2} where WH_ID={3} and CLN_ODR_ID={4} and CLN_ODR_DTL={5} and PDT_ID={6}", reserve.CmpQty,reserve.UpdUsrID, reserve.UpdDt, reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        }

        /// <summary>
        /// 成品库入库登录时修改实际在库数量 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns>true</returns>
        public bool UpdateInReserveRecvQuantity(Reserve reserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE set RECV_QTY=RECV_QTY+{0},UPD_USR_ID={1},UPD_DT={2} where WH_ID={3} and CLN_ODR_ID={4} and CLN_ODR_DTL={5} and PDT_ID={6}", reserve.RecvQty, reserve.UpdUsrID, reserve.UpdDt, reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        }

        /// <summary>
        /// 成品库出库登录时更新仓库预约表 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns>true</returns>
        public bool UpdateInReserveFinOut(Reserve reserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE set RECV_QTY=RECV_QTY-{0},PICK_ORDE_QTY={1},CMP_QTY=CMP_QTY+{2},UPD_USR_ID={3},UPD_DT={4} where WH_ID={5} and CLN_ODR_ID={6} and CLN_ODR_DTL={7} and PDT_ID={8} and PDT_SPEC={9}", reserve.RecvQty, reserve.PickOrdeQty, reserve.CmpQty, reserve.UpdUsrID, reserve.UpdDt, reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID, reserve.PdtSpec);
        }

        /// <summary>
        /// 仓库预约表获得预约批次详细单号 陈健
        /// </summary>
        /// <param name="reser">仓库预约数据集合</param>
        /// <returns>数据集合</returns>
        public IEnumerable<Reserve> GetReserveDetailListID(Reserve reser)
        {
            return base.GetList().Where(h => h.WhID.Equals(reser.WhID) && h.ClnOdrID.Equals(reser.ClnOdrID) &&
                h.ClnOdrDtl.Equals(reser.ClnOdrDtl) && h.PdtID.Equals(reser.PdtID) && h.PdtSpec.Equals(reser.PdtSpec) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 仓库预约表获得预约批次详细数据 陈健
        /// </summary>
        /// <param name="reser">仓库预约数据集合</param>
        /// <returns>数据集合</returns>
        public IEnumerable<Reserve> GetReserveDetailList(Reserve reser)
        {
            return base.GetList().Where(h => h.WhID.Equals(reser.WhID) && h.ClnOdrID.Equals(reser.ClnOdrID) &&
                h.ClnOdrDtl.Equals(reser.ClnOdrDtl) && h.PdtID.Equals(reser.PdtID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        //#region IReserveRepository 成员


        //public IEnumerable<Reserve> getReserveForRecvQty(Reserve reserve)
        //{
        //    return base.ExecuteQuery("select a.RECV_QTY as RecvQty, a.WH_ID as WhID,a.CLN_ODR_ID as ClnOdrID,a.CLN_ODR_DTL as ClnOdrDtl,a.PDT_ID as PdtID,a.ORD_PDT_ID as OrdPdtID,a.ORDE_BTH_DTAIL_LIST_ID as OrdeBthDtailListID,a.PDT_SPEC as PdtSpec,a.ORDE_QTY as OrdeQty, a.PICK_ORDE_QTY as PickOrdeQty,a.CMP_QTY as CmpQty from MC_WH_RESERVE as a where WH_ID='{0}' and CLN_ODR_ID='{1}' and CLN_ODR_DTL='{2}' and PDT_ID='{3}'", reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        //}

        //#endregion

        #region IReserveRepository 成员


        public bool DelInReserveColumns(Reserve reserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_RESERVE set RECV_QTY=RECV_QTY-{0} where WH_ID={1} and CLN_ODR_ID={2} and CLN_ODR_DTL={3} and PDT_ID={4}", reserve.RecvQty, reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        }

        #endregion

        #region IReserveRepository 成员


        public Reserve SelectReserve(Reserve reserve)
        {
            return base.First(a => a.WhID == reserve.WhID && a.ClnOdrID == reserve.ClnOdrID && a.ClnOdrDtl == reserve.ClnOdrDtl && a.PdtID == reserve.PdtID && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        #endregion

        #region IReserveRepository 成员


        public bool UpdateReserveForDelRecvColumns(Reserve reserve)
        {
           return base.ExecuteStoreCommand("update MC_WH_RESERVE set RECV_QTY = RECV_QTY-{0} where WH_ID = {1} and CLN_ODR_ID={2} and CLN_ODR_DTL={3} and PDT_ID={4}", reserve.RecvQty, reserve.WhID, reserve.ClnOdrID, reserve.ClnOdrDtl, reserve.PdtID);
        }

        #endregion

        #region 梁龙飞

        /// <summary>
        /// 检测预约信息的数量，{X|X.仓库编号，X.客户订单号，X.客户订单明细，X.产品零件ID，X.预约批次详细单号}
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int ReserveCount(Reserve condition)
        {
            IEnumerable<Reserve> rs = GetAvailableList<Reserve>().Where(a => a.WhID == condition.WhID &&
                a.ClnOdrID == condition.ClnOdrID &&
                a.ClnOdrDtl == condition.ClnOdrDtl &&
                a.PdtID == condition.PdtID &&
                a.OrdeBthDtailListID == condition.OrdeBthDtailListID).ToList();
            if (rs==null || rs.Count()==0)
            {
                return 0;
            }
            return rs.Count();
        }

        /// <summary>
        /// 根据主键查找预约信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<Reserve> GetReserveByKeys(Reserve condition)
        {
            try
            {
                IEnumerable<Reserve> rsL = GetAvailableList<Reserve>().Where(a => a.WhID == condition.WhID &&
              a.ClnOdrID == condition.ClnOdrID &&
              a.ClnOdrDtl == condition.ClnOdrDtl &&
              a.PdtID == condition.PdtID).ToList();
                return rsL;
            }
            catch (Exception)
            {

                return null;
            }
          
        }
        #endregion

        /// <summary>
        /// 根据客户订单，客户订单明细，产品id， 获取仓库预约数据以及详细数据
        /// 代东泽 20131226
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<Model.Store.VM_Reserve> GetReserveDetailsList(Reserve model)
        {
            IQueryable<Reserve> resList=base.GetList();
            IQueryable<ReserveDetail> resdtlList=base.GetList<ReserveDetail>();
            IQueryable<PartInfo> partList=base.GetList<PartInfo>();
            IQueryable<UnitInfo> unitList = base.GetList<UnitInfo>();
            var query = from a in resList
                        where a.ClnOdrID.Equals(model.ClnOdrID) && a.ClnOdrDtl.Equals(model.ClnOdrDtl) && a.OrdPdtID.Equals(model.OrdPdtID)
                        join p in partList on a.PdtID equals p.PartId into _x
                        from x in _x.DefaultIfEmpty()
                        join u in unitList on x.UnitId equals u.UnitId into _y
                        from y in _y.DefaultIfEmpty()
                        join ad in resdtlList on a.OrdeBthDtailListID equals ad.OrdeBthDtailListID into _a
                        from r in _a.DefaultIfEmpty()
                        select new VM_Reserve
                        {
                            BatchID =r.BthID,
                            ProductID = a.PdtID,
                            ProductSpec = a.PdtSpec,
                            AlctQuantity = a.OrdeQty,
                            CurrentQuantity = a.RecvQty,
                            PickiingOrderQuantity = a.PickOrdeQty,
                            AlctQty=r!=null?r.OrderQty:0M,
                            PickiingOrderQty=r!=null?r.PickOrdeQty:0M,
                            OrdeBthDtailListID=r!=null?r.OrdeBthDtailListID:0,
                            PartModel=x!=null?x.PartAbbrevi:"",
                            PartName=x!=null?x.PartName:"",
                            PartUnit=y!=null?y.UnitName:"",
                            PartUnitPrice=x!=null?x.Pricee:0,
                            WhID=a.WhID
                        };
            return query;
                      
        }
    }
}
