using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;

namespace Repository
{
    public class ProduceScheduRepositoryImp:AbstractRepository<DB,ProduceSchedu>,IProduceScheduRepository
    {

        public override bool Update(ProduceSchedu entity)
        {
            return base.Update(entity, new string[] { "ProductId", "ProcessId", "ScheduStartDt", "ScheduEndDt", "EndProduceTime", "PlanTotalQuanlity", "UpdUsrID", "UpdDt" });
        }


        public IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlan(VM_InProcessingMiddlePlanSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
