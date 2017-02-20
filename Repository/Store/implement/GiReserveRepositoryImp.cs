using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;

namespace Repository
{
    public class GiReserveRepositoryImp : AbstractRepository<DB, GiReserve>, IGiReserveRepository
    {

        #region IGiReserveRepository 成员

        public GiReserve SelectGiReserve(GiReserve giReserve)
        {
            return base.First(a => a.WareHouseID == giReserve.WareHouseID && a.PrhaOrderID == giReserve.PrhaOrderID && a.ClientOrderDetail == giReserve.ClientOrderDetail && a.ProductID == giReserve.ProductID && a.BatchID == giReserve.BatchID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IGiReserveRepository 成员


        public bool UpdateGiReserveForOutStoreQty(GiReserve giReserve)
        {
            return base.ExecuteStoreCommand("update MC_WH_GI_RESERVE set PICK_ORDE_QTY={0},CMP_QTY={1},UPD_USR_ID='{2}', UPD_DT={3}, where WH_ID={4} and PRHA_ODR_ID={5} and CLN_ODR_DTL={6} and PDT_ID={7} and BTH_ID=[8}", giReserve.PickOrderQuantity, giReserve.CmpQuantity, giReserve.UpdUsrID, DateTime.Now, giReserve.WareHouseID, giReserve.PrhaOrderID, giReserve.ClientOrderDetail, giReserve.ProductID, giReserve.BatchID);
        }

        #endregion


        #region IGiReserveRepository 梁龙飞

        public decimal GetLockedAbnormalNum(VM_MatBtchStockSearch condition)
        {
            try
            {
                IEnumerable<decimal> list = GetAvailableList<GiReserve>().Where(a => a.PrhaOrderID == condition.ClientOrderID &&
                    a.ClientOrderDetail == condition.OrderDetail && a.ProductID == condition.MaterialID &&
                    a.WareHouseID == condition.WarehouseID).Select(a => a.OrderQuantity);
                if (list == null || list.Count() <= 0)
                {
                    return 0;
                }
                else 
                {
                    return list.Sum();
                }
                //decimal lkdNum = GetAvailableList<GiReserve>().Where(a => a.PrhaOrderID == condition.ClientOrderID &&
                //    a.ClientOrderDetail == condition.OrderDetail && a.ProductID == condition.MaterialID &&
                //    a.WareHouseID == condition.WarehouseID).Select(a => a.OrderQuantity).Sum();
                //return lkdNum;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
