using Extensions;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TransactionAopAttribute : AbstractMethodAopAttribute
    {

        protected IUnitOfWork transaction;


        public override void Before()
        {
            this.transaction = (IUnitOfWork)IOCHelper.ResolverClass<IUnitOfWork>();
            if (transaction != null)
            {
                transaction.BeginTransaction();
            }
        }

        public override void handleException(Exception e)
        {

            if (transaction != null)
            {
                transaction.RollBackTransaction();
            }

        }

        public override void After()
        {
            if (transaction == null)
            {
                return;
            }
            else 
            {
                transaction.Commit();
            }
            transaction = null;
        }
    }
}
