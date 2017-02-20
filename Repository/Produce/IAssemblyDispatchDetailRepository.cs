/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAssemblyDispatchDetailRepository.cs
// 文件功能描述：总装调度详细表的Repository接口
//     
// 修改履历：2013/11/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;

namespace Repository
{
    public interface IAssemblyDispatchDetailRepository
    {
        /// <summary>
        /// 取得总装调度单详细表
        /// </summary>
        /// <param name="id">总装调度单ID</param>
        /// <returns>总装调度单数据集</returns>
        IEnumerable<AssemblyDispatchDetail> GetAssemblyDispatchDetailById(string id);

        /// <summary>
        /// 保存总装调度单详细记录
        /// </summary>
        /// <param name="entity">总装调度单实体</param>
        /// <returns>执行结果</returns>
        Boolean SaveAssemblyDispatchDetail(AssemblyDispatchDetail entity);

        /// <summary>
        /// 根据总装调度单ID删除总装调度单详细
        /// </summary>
        /// <param name="id">总装调度单号</param>
        /// <param name="userId">用户ID</param>
        /// <returns>执行结果</returns>
        Boolean DeleteAssemblyDispatchDetailById(String id, string userId);

        /// <summary>
        /// 从总装调度单详细表中捞取数据
        /// </summary>
        /// <param name="id">总装调度单号</param>
        /// <returns>总装调度单详细集</returns>
        IEnumerable<VM_NewBillDataGrid> GetEditBillDataGrid(string id);
    }

}
