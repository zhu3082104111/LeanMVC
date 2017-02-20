using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
namespace Repository
{
    public class NoticeRepositoryImp : AbstractRepository<DB, Notice>, INoticeRepository
    {

        public IEnumerable<Model.Notice> getAllNotice()
        {
            return base.GetList().ToList();
        }


        public IQueryable<Notice> getAllNoticeAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<Notice> getAllNoticeAsQueryableOrderBy()
        {
           // return base.GetList<string>(a => a.Enabled == true, a => a.NoticeID);
            return null;
        }

        public Notice getNoticeById(string Noticeid)
        {
            return base.GetEntityById(Noticeid);
        }


        public bool addNotice(Notice n)
        {
            return base.Add(n);
        }

        public bool updateNotice(Notice n)
        {
            return base.Update(n);
        }

        public bool deleteNotice(Notice n)
        {
            return base.Delete(n);
        }


        public bool deleteNoticeBySQL(Notice n)
        {
            return base.ExecuteStoreCommand("delete from BI_Notice  where NoticeID={0}", n.NoticeID);
        }
    }
}
