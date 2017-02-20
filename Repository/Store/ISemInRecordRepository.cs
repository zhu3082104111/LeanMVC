/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISemInRecordRepository.cs
// 文件功能描述：
//          半成品库入库履历表的Repository
//      
// 修改履历：2013/12/07 汪腾飞 新建
/*****************************************************************************/
using System.Collections;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 半成品库入库履历表的Repository
    /// </summary>
    public interface ISemInRecordRepository : IRepository<SemInRecord>
    {
        /// <summary>
        /// 半成品库入库履历信息获取
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        IEnumerable GetSemStoreBySearchByPage(VM_SemInStoreForSearch searchCondition, Extensions.Paging paging);

        IEnumerable GetSemInPrintBySearchByPage(VM_SemInPrintForSearch seminprint, Extensions.Paging paging);

        IEnumerable SelectSemStore(string pdtID, string deliveryOrderID,Extensions.Paging paging);

        IEnumerable GetSemInRecordBySearchByPage(VM_SemInRecordStoreForSearch semInRecordStoreForSearch,Extensions.Paging paging);

        IEnumerable GetSemInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Extensions.Paging paging);

        //获取履历数据对象
        SemInRecord SelectSemInRecord(SemInRecord semInRecord);

        //履历数据删除方法
        bool SemInRecordForDel(SemInRecord semInRecord);

        //半成品库入库登陆保存功能
        bool SemInStoreForDel(string IsetRepID, string StoStat);
        bool SemInStoreForDelProc(string IsetRepID, string StoStat);


    }

}
