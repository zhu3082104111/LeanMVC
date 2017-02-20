/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAssemblyDispatchRepository.cs
// 文件功能描述：总装调度单表的Repository接口
//     
// 修改履历：2013/11/21 朱静波 新建
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

namespace Repository
{
    public interface IAssemblyDispatchRepository : IRepository<AssemblyDispatch>
    {
        /// <summary>
        /// 根据条件取得总装调度单表中取得所有的数据
        /// </summary>
        /// <param name="inProcessingRate">搜索条件</param>
        /// <param name="paging">分页</param>
        /// <returns>总装调度单数据集</returns>
        IEnumerable<VM_AssemblyDispatch> GetAllAssemblyScheduleBill(VM_FAScheduleBillForSearch inProcessingRate, Paging paging);

        /// <summary>
        /// 根据提供的id删除相应的总装调度单
        /// </summary>
        /// <param name="id">总装调度单号</param>
        /// <param name="userId">用户ID</param>
        /// <returns>执行结果</returns>
        Boolean DeleteAssemblyDispatchById(string id, string userId);

        /// <summary>
        /// 取得单个总装调度单实体
        /// </summary>
        /// <param name="id">总装调度单号</param>
        /// <returns>总装调度单实体</returns>
        AssemblyDispatch GetAssemblyScheduleBillById(string id);

        /// <summary>
        /// 更新总装调度单
        /// </summary>
        /// <param name="entity">总装调度单实体</param>
        /// <returns>执行结果</returns>
        Boolean UpdateAssemblyScheduleBill(AssemblyDispatch entity);

        /// <summary>
        /// 添加新的总装调度单记录
        /// </summary>
        /// <param name="entity">总装调度单实体</param>
        /// <returns>执行结果</returns>
        Boolean AddAssemblyScheduleBill(AssemblyDispatch entity);

        /// <summary>
        /// 查找指定总装工票ID的总装调度单记录个数
        /// </summary>
        /// <param name="assemblyTicketID">查找指定总装工票ID</param>
        /// <returns>记录个数</returns>
        int GetAssemblyTicketIdQuantity(string assemblyTicketID);
    }
}
