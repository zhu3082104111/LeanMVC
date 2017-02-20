// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：WorkticketRepositoryImp.cs
// 文件功能描述：加工工票数据操作类
// 
// 创建标识：代东泽 20131126
//
// 修改标识：代东泽 20131126
// 修改描述：后期开发
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model.Produce;
namespace Repository
{
    class WorkticketRepositoryImp : AbstractRepository<DB, ProduceBill>, IWorkticketRepository
    {

        public IEnumerable<Model.Produce.VM_ProduceBillForTableShow> GetWorkticketsWithPaging(Model.Produce.VM_ProduceBillForSrarch workticket, Extensions.Paging paging)
        {


            return null;
        }

        public Model.Produce.VM_ProduceBillForDetailShow GetWorkticket(Model.Produce.VM_ProduceBillForTableShow workticket)
        {
            throw new NotImplementedException();
        }



        public Model.Produce.VM_ProduceBillForDetailShow GetWorkticket(string billId)
        {
            throw new NotImplementedException();
        }
    }
}
