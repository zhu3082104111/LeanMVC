using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;

namespace Repository
{
    public class ProduceScheduDetailRepositoryImp:AbstractRepository<DB,ProduceScheduDetail>,IProduceScheduDetailRepository
    {
        public override bool Add(ProduceScheduDetail schedu)
        {
            
            throw new NotImplementedException();
        }

        public bool Delete(VM_InProcessingLittlePlanShow condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VM_InProcessingLittlePlanShow> GetLittlePlan(VM_InProcessingLittlePlanSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
