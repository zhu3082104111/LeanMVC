using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
namespace BLL
{
    public class NoticeServiceImp :AbstractService, INoticeService
    {

       //需要使用的Repository类
        private INoticeRepository NoticeRepository;


        //构造方法，必须要，参数为需要注入的属性
        public NoticeServiceImp(INoticeRepository NoticeRepository) 
        {
            this.NoticeRepository = NoticeRepository;

        
        }

        // ============================================================//
        

        //继承自接口

        public IEnumerable<Notice> getAllNotice()
        {
            return NoticeRepository.getAllNotice();
        }

        public IEnumerable<Notice> exceptionExample()
        {
            throw new Exception("This is exception");
           
            return null ;
        }


        public IQueryable<Notice> findAllNotice()
        {
           return NoticeRepository.getAllNoticeAsQueryable();
        }

        public IQueryable<Notice> findAllNoticeOrderBy()
        {
            return NoticeRepository.getAllNoticeAsQueryableOrderBy();
        }


        public Notice getNoticeById(string Noticeid)
        {
            return NoticeRepository.getNoticeById(Noticeid);
        }

        public bool addNotice(Notice n)
        {
            return NoticeRepository.addNotice(n);
        }

        public bool updateNotice(Notice n)
        {
            return NoticeRepository.updateNotice(n);
        }

        public bool deleteNotice(Notice n)
        {
            return NoticeRepository.deleteNoticeBySQL(n);
        }



    }
}
