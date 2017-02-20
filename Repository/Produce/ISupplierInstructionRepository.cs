/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISupplierInstructionRepository.cs
// 文件功能描述：
//          外协指示表的Repository接口类
//      
// 修改履历：2014/1/8 陈阵 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 外协指示表的Repository接口类
    /// </summary>
    public interface ISupplierInstructionRepository : IRepository<AssistInstruction>
    {
        /// <summary>
        /// 获取外协指示List的查询方法(采购部门的外协计划一览画面用)
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外协指示List信息</returns>
        IEnumerable<VM_SupplierInstructionList4Table> GetSupplierInstructionListWithPaging(VM_SupplierInstructionList4Search searchItem, Paging paging);

        /// <summary>
        /// 取得外协排产画面的显示信息
        /// </summary>
        /// <param name="insturctions">外协指示List</param>
        /// <returns>外协排产画面的显示信息</returns>
        IEnumerable<VM_SupplierScheduling> GetSupplierSchedulingInfo(string[] insturctions);
    }
}
