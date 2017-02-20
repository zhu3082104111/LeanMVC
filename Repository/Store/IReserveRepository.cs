// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IReserveRepository.cs
// 文件功能描述：仓库预约表repository接口
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model.Store;
namespace Repository
{
    /// <summary>
    /// 仓库预约表资源库
    /// M：梁龙飞
    /// </summary>
    public interface IReserveRepository : IRepository<Reserve>
    {
        bool AddReserve(Reserve reserve);

        //入库登录时修改实际在库数量
        bool UpdateInReserveColumns(Reserve reserve);

        //履历删除时修改实际在库数量
        bool UpdateReserveForDelRecvColumns(Reserve reserve);

        /// <summary>
        /// 成品库入库登录时修改实际在库数量 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns>true</returns>
        bool UpdateInReserveRecvQuantity(Reserve reserve);

        //入库登录时修改实际在库数量
        Reserve SelectReserve(Reserve reserve);

        /// <summary>
        /// 成品库出库登录时更新仓库预约表 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns>true</returns>
        bool UpdateInReserveFinOut(Reserve reserve);

        /// <summary>
        /// 仓库预约表获得预约批次详细单号 陈健
        /// </summary>
        /// <param name="reser">仓库预约数据集合</param>
        /// <returns>数据集合</returns>
        IEnumerable<Reserve> GetReserveDetailListID(Reserve reser);

        /// <summary>
        /// 仓库预约表获得预约批次详细数据 陈健
        /// </summary>
        /// <param name="reser">仓库预约数据集合</param>
        /// <returns>数据集合</returns>
        IEnumerable<Reserve> GetReserveDetailList(Reserve reser);
        
        /// <summary>
        /// 检测预约信息的数量，{X|X.仓库编号，X.客户订单号，X.客户订单明细，X.产品零件ID，X.预约批次详细单号}
        /// C：梁龙飞
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        int ReserveCount(Reserve condition);

        /// <summary>
        /// 根据主键查找预约信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<Reserve> GetReserveByKeys(Reserve condition);

        /// <summary>
        /// 在制品库出库登录更新已出库数量 陈健
        /// </summary>
        /// <param name="reserve">更新数据集合</param>
        /// <returns></returns>
        bool UpdateInReserveCmpQty(Reserve reserve);
        /// <summary>
        /// 根据客户订单，客户订单明细，产品id， 获取仓库预约数据以及详细数据
        /// 代东泽 20131226
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<VM_Reserve> GetReserveDetailsList(Reserve model);
    }
}
