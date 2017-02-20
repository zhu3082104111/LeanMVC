using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    /// <summary>
    /// 让步仓库预约表资源库
    /// M:梁龙飞
    /// </summary>
    public interface IGiReserveRepository : IRepository<GiReserve>
    {
        /// <summary>
        /// 获得订单产品物料的让步品锁存和
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        decimal GetLockedAbnormalNum(VM_MatBtchStockSearch condition);

        //取得让步仓库预约对象数据（yc添加）
        GiReserve SelectGiReserve(GiReserve giReserve);

        bool UpdateGiReserveForOutStoreQty(GiReserve giReserve);

    }
}
