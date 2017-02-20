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
    public class MaterialRepositoryImp : AbstractRepository<DB, Material>, IMaterialRepository
    {
        public bool updateMaterialForStoreLogin(Material material)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set ALCT_QTY={0},USEABLE_QTY={1},CURRENT_QTY={2},CNSM_QTY={3},TOTAL_AMT={4},TOTAL_VALUAT_UP={5},LAST_WHIN_YMD={6},UPD_DT={7},UPD_USR_ID={8} where WH_ID={9} and PDT_ID={10}'", material.AlctQty, material.UseableQty, material.CurrentQty, material.CnsmQty, material.TotalAmt, material.TotalValuatUp, material.LastWhINYmd, material.UpdDt, material.UpdUsrID, material.WhID, material.PdtID);
        }

        /// <summary>
        /// 成品库入库履历详细修改仓库表
        /// </summary>
        /// <param name="material">更新数据集合</param>
        /// <returns>true</returns>
        public bool updateInMaterialFinIn(Material material)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set CURRENT_QTY=CURRENT_QTY+{0},TOTAL_AMT=TOTAL_AMT+{1},TOTAL_VALUAT_UP=TOTAL_VALUAT_UP+{2},UPD_USR_ID={3},UPD_DT={4} where WH_ID='{5}' and PDT_ID='{6}'", material.CurrentQty, material.TotalAmt, material.TotalValuatUp, material.UpdUsrID, material.UpdDt, material.WhID, material.PdtID);
        }

        /// <summary>
        /// 成品库出库履历详细修改仓库表
        /// </summary>
        /// <param name="material">更新数据集合</param>
        /// <returns>true</returns>
        public bool updateInMaterialFinOut(Material material)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set CURRENT_QTY=CURRENT_QTY-{0},TOTAL_AMT=TOTAL_AMT-{1},TOTAL_VALUAT_UP=TOTAL_VALUAT_UP-{2},UPD_USR_ID={3},UPD_DT={4} where WH_ID={5} and PDT_ID={6}", material.CurrentQty, material.TotalAmt, material.TotalValuatUp, material.UpdUsrID, material.UpdDt, material.WhID, material.PdtID);
        }

        /// <summary>
        /// 在制品库出库登录修改仓库表
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public bool updateMaterialForOutLogin(Material material)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set ALCT_QTY={0},CURRENT_QTY={1},CNSM_QTY={2},LAST_WHOUT_YMD={3},TOTAL_AMT={4},TOTAL_VALUAT_UP={5},UPD_DT={6},UPD_USR_ID={7} where WH_ID={8} and PDT_ID={9}", material.AlctQty, material.CurrentQty, material.CnsmQty, material.LastWhoutYmd,material.TotalAmt,material.TotalValuatUp, material.UpdDt, material.UpdUsrID, material.WhID, material.PdtID);
        }


        #region IMaterialRepository 成员


        public Material selectMaterial(Material material)
        {
          return  base.First(a => a.WhID == material.WhID && a.PdtID == material.PdtID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IMaterialRepository 成员


        public bool DelInRecordMaterialColumns(Material material)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set ALCT_QTY={0}, USEABLE_QTY={1},CURRENT_QTY={2},TOTAL_AMT={3},TOTAL_VALUAT_UP={4},LAST_WHIN_YMD={5} where WH_ID={6} and PDT_ID={7}", material.AlctQty, material.UseableQty,material.CurrentQty,material.TotalAmt,material.TotalValuatUp, null, material.WhID, material.PdtID);
        }

        #endregion


        #region C：梁龙飞

        /// <summary>
        /// 获取产品在仓库中的可锁库存量
        /// 产品不存在时返回为0
        /// C:梁龙飞
        /// </summary>
        /// <param name="whID">仓库ID</param>
        /// <param name="matID">物料ID</param>
        /// <returns></returns>
        public decimal GetUnlockedNum(string whID, string matID)
        {
            Material temp= GetAvailableList<Material>().First(a => a.WhID == whID && a.PdtID == matID);
            if (temp!=null)
            {
                return temp.UseableQty;
            }
            return 0;
        }

        #endregion
    }
}
