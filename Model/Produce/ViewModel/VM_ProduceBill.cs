using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Produce
{

   public class VM_ProduceBillSuper {

        public string BillId { get; set; }

        public string PartModel { get; set; }

        public string ProduceUnit { get; set; }//部门

        public string Operator { get; set; }

        public string State { get; set; }//状态

        public string Team { get; set; }

    }

    public class VM_ProduceBillForTableShow : VM_ProduceBillSuper
    {

        public DateTime? Date { get; set; }
    }

    public class VM_ProduceBillForSrarch : VM_ProduceBillSuper
    {


        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class VM_ProduceBillForDetailShow { 
    
    
    }
}
