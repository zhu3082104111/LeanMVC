/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFAScheduleBillService.cs
// 文件功能描述：加工交仓的Service接口
//     
// 修改履历：2013/12/06 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository;

namespace BLL
{
    public interface IIWaitingWarehouseListService
    {
        /// <summary>
        /// 交仓
        /// </summary>
        /// <param name="entity">需要交仓的数据</param>
        /// <param name="userId">用户ID</param>
        /// <param name="procDelivID">新加工送货单号</param>
        /// <returns>执行结果</returns>
        [TransactionAop]
        Boolean DeliveryWarehouse(VM_IWaitingWarehouseView entity, string userId, string procDelivID);

        /// <summary>
        /// 根据条件取得可交仓数量大于0的数据
        /// </summary>
        /// <param name="entity">筛选条件</param>
        /// <param name="paging">分页</param>
        /// <returns>加工待交仓一览数据集</returns>
        IEnumerable<VM_IWaitingWarehouseView> SearchTranslateCard(VM_IWaitingWarehouseForSearch entity, Paging paging);

        /// <summary>
        /// 根据条件搜索加工产品交仓单一览数据
        /// </summary>
        /// <param name="entity">筛选条件</param>
        /// <param name="paging">分页</param>
        /// <returns>加工产品交仓单一览</returns>
        IEnumerable<VM_ProProductWarehouseView> SearchProProductWarehouse(VM_ProProductWarehouseForSearch entity,
            Paging paging);

        /// <summary>
        /// 删除加工产品交仓单
        /// </summary>
        /// <param name="deleList">产品交仓单号数据集</param>
        /// <returns>执行结果</returns>
        //[TransactionAop]
        string DeleteWarehouse(List<string> deleList, string userId);

        /// <summary>
        /// 根据主键取得加工送货单实体
        /// </summary>
        /// <param name="id">加工送货单号</param>
        /// <returns>加工送货单实体</returns>
        ProcessDelivery GetProcessDeliveryByID(string id);

        /// <summary>
        /// 根据加工送货单号取得加工送货单一览详细部分信息
        /// </summary>
        /// <param name="id">加工送货单号</param>
        /// <returns>加工送货单一览详细部分信息</returns>
        VM_ProcessDelivery GetVMProcessDelivery(string id);

        /// <summary>
        /// 取得加工产品交仓单一览详细视图
        /// </summary>
        /// <param name="id">加工送货单号</param>
        /// <returns>加工送货详细数据集</returns>
        IEnumerable<ProcessDeliveryDetail> GetProDelDetViewByID(string id);

        /// <summary>
        /// 保存待交仓一览详细对让步标志的修改
        /// </summary>
        /// <param name="procDelivID">加工送货单号</param>
        /// <param name="concessionPart">让步标志</param>
        /// <returns>执行结果</returns>
        string SaveConcession(string procDelivID, string concessionPart);
    }
}