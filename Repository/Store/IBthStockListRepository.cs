using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Collections;
namespace Repository
{
    /// <summary>
    /// 批次别库存表 资源库
    /// M:梁龙飞:20131210
    /// </summary>
    public interface IBthStockListRepository : IRepository<BthStockList>
    {
        BthStockList SelectBthStockList(BthStockList btockList);

        bool DelBthStockList(BthStockList bthStockList);

        //bool UpdateBthStockList(BthStockList bthStockList);

        bool UpdateBthStockListForStore(BthStockList bthStockList);

       

        #region 基础功能 C：梁龙飞
        /// <summary>
        /// 可锁的总数量
        /// </summary>
        /// <param name="partID">零件ID</param>
        /// <param name="isNormal">正常品与让步品区分：{true:正常品,false:让步品}</param>
        /// <returns></returns>
        decimal CountNotLocked(string partID, bool isNormal);

        /// <summary>
        /// 返回可锁批次
        /// </summary>
        /// <param name="pratID">零件ID</param>
        /// <param name="isNormal">正常品与让步品区分：{true:正常品,false:让步品}</param>
        /// <returns></returns>
        IQueryable<BthStockList> BatchCanLocked(string pratID, bool isNormal);

        #endregion

        #region ProductScheduling业务：主要是检索功能 C:梁龙飞

        /// <summary>
        /// 返回有型号的正常品的锁存信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetNormalLockedBatch(VM_MatBtchStockSearch condition);
        /// <summary>
        /// 返回正常可锁批次
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetNormalUnLockedBatch(VM_MatBtchStockSearch condition);
        /// <summary>
        /// 返回让步品的锁存信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetAbnormalLockedBatch(VM_MatBtchStockSearch condition);
        /// <summary>
        /// 返回让步产品可锁批次
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetAbnormalUnlockedBatch(VM_MatBtchStockSearch condition);
       

        #endregion

        /// <summary>
        /// 成品库出库履历详细修改批次别库存表 陈健
        /// </summary>
        /// <param name="bthStockList">更新数据集合</param>
        /// <returns>true</returns>
        bool updateInBthStockListFinOut(BthStockList bthStockList);

        /// <summary>
        /// 在制品库出库登录修改批次别库存表
        /// </summary>
        /// <param name="bthStockList"></param>
        /// <returns></returns>
        bool UpdateBthStockListForOut(BthStockList bthStockList);
    }
}
