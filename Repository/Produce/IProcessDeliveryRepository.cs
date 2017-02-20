/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProcessDeliveryRepository.cs
// 文件功能描述：加工送货单表的Repository接口
//     
// 修改履历：2013/12/09 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;

namespace Repository
{
    public interface IProcessDeliveryRepository : IRepository<ProcessDelivery>
    {
        /// <summary>
        /// 为加工送货单详细表添加新纪录
        /// </summary>
        /// <param name="entity">加工送货详细表实体</param>
        /// <returns>执行结果</returns>
        bool AddProcessDeliveryDetail(ProcessDeliveryDetail entity);

        /// <summary>
        /// 为加工送货单流转卡对应关系表添加新纪录
        /// </summary>
        /// <param name="entity">加工送货单工票对应关系实体</param>
        /// <returns>执行结果</returns>
        bool AddProcessDelivBill(ProcessDelivBill entity);

        /// <summary>
        /// 根据条件搜索加工产品交仓单一览数据
        /// </summary>
        /// <param name="entity">筛选条件</param>
        /// <param name="paging">分页</param>
        /// <returns>加工产品交仓单一览数据集</returns>
        IEnumerable<VM_ProProductWarehouseView> SearchProProductWarehouse(VM_ProProductWarehouseForSearch entity,
        Paging paging);

        /// <summary>
        /// 根据送货单号取得加工送货单
        /// </summary>
        /// <param name="id">送货单号</param>
        /// <returns>加工送货单实体</returns>
        ProcessDelivery GetProcessDeliveryByID(string id);

        /// <summary>
        /// 根据送货单号取得加工送货单详细
        /// </summary>
        /// <param name="id">送货单号</param>
        /// <returns>加工送货单详细实体</returns>
        ProcessDeliveryDetail GetProcessDeliveryDetailByID(string id);


        /// <summary>
        /// 根据送货单号取得加工送货单工票对应关系表
        /// </summary>
        /// <param name="id">送货单号</param>
        /// <returns>加工送货单工票对应关系表实体</returns>
        ProcessDelivBill GetProcessDelivBillByID(string id);

        /// <summary>
        /// 根据条件删除加工送货单
        /// </summary>
        /// <param name="entity">删除条件</param>
        /// <returns>执行结果</returns>
        bool DeleteProcessDelivery(ProcessDelivery entity);

        /// <summary>
        /// 根据条件删除加工送货单详细
        /// </summary>
        /// <param name="entity">删除条件</param>
        /// <returns>执行结果</returns>
        bool DeleteProcessDeliveryDetail(ProcessDeliveryDetail entity);

        /// <summary>
        /// 根据条件删除加工送货单流转卡对应关系表添加新纪录
        /// </summary>
        /// <param name="entity">删除条件</param>
        /// <returns>执行结果</returns>
        bool DeleteProcessDelivBill(ProcessDelivBill entity);

        /// <summary>
        /// 根据加工送货单号取得加工送货单一览详细部分信息
        /// </summary>
        /// <param name="id">加工送货单号</param>
        /// <returns>执行结果</returns>
        VM_ProcessDelivery GetVMProcessDeliveryByID(string id);

        /// <summary>
        /// 取得加工送货详细表
        /// </summary>
        /// <param name="id">加工送货单号</param>
        /// <returns>加工送货详细数据集</returns>
        IEnumerable<ProcessDeliveryDetail> GetProDelDetViewByID(string id);

        /// <summary>
        /// 更新加工送货单详细表记录
        /// </summary>
        /// <param name="entity">跟新内容</param>
        /// <returns>执行结果</returns>
        bool UpdateProcessDeliveryDetail(ProcessDeliveryDetail entity);

        /// <summary>
        /// 自生产加工品入库分配数量时调用
        /// </summary>
        /// <param name="procDelivId">加工送货单号</param>
        /// <returns>VM_ClnOdrList，包含客户订单号，客户订单明细号，以及交仓数量的List。</returns>
        List<VM_ClnOdrList> GetClnListProcDel(string procDelivId);
    }
}
