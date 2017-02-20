using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface INoticeRepository : IRepository<Notice>
    {
       IEnumerable<Notice> getAllNotice();

       IQueryable<Notice> getAllNoticeAsQueryable();

       IQueryable<Notice> getAllNoticeAsQueryableOrderBy();

       Notice getNoticeById(string Noticeid);


       bool addNotice(Notice n);

       bool updateNotice(Notice n);

       bool deleteNotice(Notice n);

       bool deleteNoticeBySQL(Notice n);

    }
}
