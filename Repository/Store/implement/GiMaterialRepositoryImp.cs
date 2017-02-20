// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：GiMaterialRepositoryImp.cs
// 文件功能描述：让步仓库表repository实现类
// 
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
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
    /// 让步仓库表repository实现类
    /// 代东泽 20131226 添加注释
    /// </summary>
    public class GiMaterialRepositoryImp : AbstractRepository<DB, GiMaterial>, IGiMaterialRepository
    {
        //附件库仓库编码
        public string accWhID = "0002";
        #region IGiMaterialRepository 成员(yc添加)

        public GiMaterial SelectGiMaterial(GiMaterial giMaterial)
        {
            return base.First(a => a.WareHouseID == accWhID && a.ProductID == giMaterial.ProductID && a.BatchID == giMaterial.BatchID && a.EffeFlag=="0" && a.DelFlag=="0");
        }

        #endregion

        #region IGiMaterialRepository 成员（yc添加）

        public bool UpdateGiMaterialForDel(GiMaterial giMaterial)
        {
            return base.ExecuteStoreCommand("update MC_WH_GI_MATERIAL set DEL_FLG='1',DEL_DT={0}, DEL_USR_ID={1} where WH_ID={2} and PDT_ID={3} and BTH_ID={4}",DateTime.Now, giMaterial.DelUsrID, giMaterial.WareHouseID, giMaterial.ProductID, giMaterial.BatchID);
        }

        #endregion



        #region IGiMaterialRepository 成员(yc添加)


        public bool UpdateGiMaterialForStore(GiMaterial giMaterial)
        {
            return base.ExecuteStoreCommand("update MC_WH_GI_MATERIAL set ALCT_QTY={0}, USEABLE_QTY={1}, CURRENT_QTY={2}, TotalAmt={3}, TOTAL_VALUAT_UP={4}, UPD_USR_ID={5}, UPD_DT={6} where WH_ID={7} and PDT_ID={8} and BTH_ID={9} ",  giMaterial.AlctQuantity, giMaterial.UserableQuantity, giMaterial.CurrentQuantity, giMaterial.TotalAmt,giMaterial.TotalValuatUp, giMaterial.UpdUsrID, DateTime.Now, giMaterial.WareHouseID, giMaterial.ProductID, giMaterial.BatchID);
        }

        #endregion

        /// <summary>
        /// 在制品库出库登录修改让步仓库表前查询
        /// </summary>
        /// <param name="giMaterial"></param>
        /// <returns></returns>
        public GiMaterial WipSelectGiMaterial(GiMaterial giMaterial)
        {
            return base.First(a => a.WareHouseID == giMaterial.WareHouseID && a.ProductID == giMaterial.ProductID && a.EffeFlag == "0" && a.DelFlag == "0");
        }


        /// <summary>
        /// 在制品出库登录修改让步仓库表
        /// </summary>
        /// <param name="giMaterial"></param>
        /// <returns></returns>
        public bool UpdateGiMaterialForOut(GiMaterial giMaterial)
        {
            return base.ExecuteStoreCommand("update MC_WH_GI_MATERIAL set ALCT_QTY={0}, CURRENT_QTY={1}, LAST_WHOUT_YMD={2},TOTAL_AMT={3},TOTAL_VALUAT_UP={4}, UPD_USR_ID={5}, UPD_DT={6} where WH_ID={7} and PDT_ID={8}", giMaterial.AlctQuantity, giMaterial.CurrentQuantity, giMaterial.LastWhOutYMD,giMaterial.TotalAmt,giMaterial.TotalValuatUp, giMaterial.UpdUsrID, giMaterial.UpdDt, giMaterial.WareHouseID, giMaterial.ProductID);
        }

    }
}
