using Extensions;
using Model;
using Repository;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.App_Start
{
    public class TransactionAttribute:ActionFilterAttribute
    {

         protected IUnitOfWork transaction;

         public TransactionAttribute()
         {

         } 


         public override void OnActionExecuting(ActionExecutingContext filterContext) 

         {
             this.transaction = IOCHelper.ResolverClass<IUnitOfWork>();
             transaction.BeginTransaction(); 

         } 

   

         public override void OnActionExecuted(ActionExecutedContext filterContext) 

         {

             if (transaction == null )
             {

                 return;

             }

             if (filterContext.Exception != null)
             {

                 transaction.RollBackTransaction();
             }

             else
             {
                 transaction.Commit();
             }
             transaction = null;

         } 

   
    }
}