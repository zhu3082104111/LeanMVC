/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessDetailRepositoryImp.cs
// 文件功能描述：工序详细信息表的Repository接口实现
//     
// 修改履历：2013/11/29 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;

namespace Repository
{
    class ProcessDetailRepositoryImp : AbstractRepository<DB, ProcessDetail>,IProcessDetailRepository
    {
        public IEnumerable<int> GetSeqNo(string procId)
        {
            return base.GetAvailableList<ProcessDetail>().Where(a => a.ProcessId.Equals(procId)).Select(a => a.SeqNo);
        }
    }
}
