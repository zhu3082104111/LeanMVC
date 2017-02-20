using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace BLL
{
    public interface INoticeService
    {
        //接口方法
        IEnumerable<Notice> getAllNotice();

        //接口方法
        IEnumerable<Notice> exceptionExample();

        IQueryable<Notice> findAllNotice();

        IQueryable<Notice> findAllNoticeOrderBy();

        Notice getNoticeById(string Noticeid);

        bool addNotice(Notice n);

        bool updateNotice(Notice n);

        bool deleteNotice(Notice n);




    }
}
