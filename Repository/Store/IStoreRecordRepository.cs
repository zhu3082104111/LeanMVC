/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IStoreRecordRepository.cs
// 文件功能描述：在库品一览画面的Repository接口
//      
// 修改履历：2014/1/8 刘云 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 在库品一览画面的Repository接口
    /// </summary>
    public interface IStoreRecordRepository : IRepository<Material>
    {
        /// <summary>
        /// 得到将要在页面上显示的数据
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        IEnumerable GetStoreRecordBySearchByPage(VM_StoreRecordForSearch searchConditon, Extensions.Paging paging);
    }
}
