// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IWorkticketRepository.cs
// 文件功能描述：加工工票数据操作接口
// 
// 创建标识：代东泽 20131126
//
// 修改标识：代东泽 20131126
// 修改描述：后期开发
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;
using Extensions;
namespace Repository
{
    public interface IWorkticketRepository : IRepository<ProduceBill>
    {
       IEnumerable<VM_ProduceBillForTableShow> GetWorkticketsWithPaging(VM_ProduceBillForSrarch workticket, Paging paging);

       VM_ProduceBillForDetailShow GetWorkticket(VM_ProduceBillForTableShow workticket);

       VM_ProduceBillForDetailShow GetWorkticket(string billId);
    }
}
