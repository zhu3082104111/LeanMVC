using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    /// <summary>
    /// 仓库预约详细表
    /// M:梁龙飞 20131217
    /// </summary>
    public interface IReserveDetailRepository : IRepository<ReserveDetail>
    {

        //附件库入库履历删除操作（yc添加）
        bool UpdateReserveDetailForDel(ReserveDetail reserveDetail);

        //附件库入库履历修改操作（yc添加）
        bool UpdateReserveDetailForUpdate(ReserveDetail reserveDetail);

        //查询ReserveDetail对象（yc添加）
        ReserveDetail SelectReserveDetail(ReserveDetail reserveDetail);

        /// <summary>
        /// 成品库出库登录更新领料单开具数量及已出库数量 陈健
        /// </summary>
        /// <param name="ordeBthDtailListID">预约批次详细单号</param>
        /// <param name="reserveDetail">更新数据集合</param>
        /// <returns>true</returns>
        bool UpdateInReserveDetailFinOut(int ordeBthDtailListID, ReserveDetail reserveDetail);

        /// <summary>
        /// 根据预约批次详细单号到仓库预约详细表里检索 陈健
        /// </summary>
        /// <param name="ordeBthDtailListID">预约批次详细单号</param>
        /// <param name="batchID">批次号</param>
        /// <returns>仓库预约详细表检索数据集合</returns>
        IQueryable<ReserveDetail> GetReserveDetailList(int ordeBthDtailListID, string batchID);


        /// <summary>
        /// 根据预约详细单号检索仓库预约详细信息
        /// 没有值时返回空
        /// C：梁龙飞
        /// </summary>
        /// <param name="orderBthDtailListID"></param>
        /// <returns></returns>
        IQueryable<ReserveDetail> GetReserveDtlByDtlID(int orderBthDtailListID);

        /// <summary>
        /// 获取最大的仓库预约详细编号
        /// C：梁龙飞
        /// </summary>
        /// <returns></returns>
        int GetMaxBthDetailCode();

        /// <summary>
        /// 根据详细单号删除所有
        /// C:梁龙飞
        /// </summary>
        /// <param name="batchDetailID"></param>
        /// <returns></returns>
        bool DeleteByDetailID(int batchDetailID);

        /// <summary>
        /// 获取某一订单详细编号下的预约数量合计
        /// C：梁龙飞
        /// </summary>
        /// <param name="batchDetailID"></param>
        /// <returns></returns>
        decimal GetQuantityByDetailID(int batchDetailID);

        /// <summary>
        /// 在制品库出库登录修改领料单开具数量及已出库数量 陈健
        /// </summary>
        /// <param name="reserveDetail">更新数据集合</param>
        /// <returns></returns>
        bool UpdateInReserveDetailQty(ReserveDetail reserveDetail);



    }
}
