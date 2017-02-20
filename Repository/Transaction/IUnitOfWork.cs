using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork
       : IDisposable
    {

        /// <summary>
        /// 开启事务
        /// </summary>
        ///<remarks>
        /// 
        ///</remarks>
        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 如果当前使用了事务，则提交事务，保存更改
        /// </summary>
        ///<remarks>
        /// 
        ///</remarks>
        void Commit();


        void SaveChanges();


        void SaveChanges(SaveOptions saveOptions);

    
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollBackTransaction();

    }
}
