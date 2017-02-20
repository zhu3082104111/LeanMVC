/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IStoreRecordService.cs
// 文件功能描述：在库品一览画面的Service接口
//      
// 修改履历：2014/1/8 刘云 新建
/*****************************************************************************/
using Extensions;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 在库品一览画面的Service接口
    /// </summary>
    public interface IStoreRecordService
    {
        /// <summary>
        /// 在库品一览画面的显示数据的取得函数
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页情报</param>
        /// <returns>在库品一览情报List</returns>
        IEnumerable GetStoreRecordBySearchByPage(VM_StoreRecordForSearch searchCondition, Paging paging);
    }
}
