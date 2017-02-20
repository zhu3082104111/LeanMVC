using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;

namespace BLL
{
    public interface IDiscardService
    {
        //Discard
        //接口方法

        /// <summary>
        /// 按条件查询用户并分页
        /// </summary>
        /// <param name="user">待查找的用户的信息</param>
        /// <param name="paging">分页</param>
        /// <param name="total">满足条件的总数</param>
        /// <returns></returns>
        //IQueryable GetDiscardBySearchByPage(Discard Discard, Paging paging, out int total);

        IEnumerable<VM_StoreDiscardForTableShow> GetdiscardStoreForSearch(VM_storeDiscardForSearch discardStore, Paging paging);

        IEnumerable<VM_StoreDiscardForShow> GetStoreDiscardForSearch(VM_StoreDiscardForSearch discardStore, Paging paging);

        IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHForSearch(VM_StoreDiscardDetailForSearch discardStore, Paging paging, string flg);

        //string FinInRecordForLogin(VM_StoreDiscardDetailForTableShow finInRecord, Dictionary<string, string>[] orderList, string id);

        //
        //在库品报废申请一览的报废与取消
        string DiscardForScrappedOrCancel(VM_StoreDiscardDetailForSearch discardList, string flg);
        
        //
        //在库品报废申请详细updata和保存
        string DiscardSaveUpdata(VM_StoreDiscardDetailForTableShow discardRecordList, Dictionary<string, string>[] orderList, string id);
        //
        //在库待报废品一览批量查询批量查询
        IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHForSearchList(string id, string iid, string BthID, string discardId, Paging paging);
        //
        //在库品报废申请一览详细跳转
        IEnumerable<VM_StoreDiscardDetailForTableShow> GetDiscarorSearchList(VM_StoreDiscardDetailForSearch search, Paging paging);

        //
        //弹出页面一览
        IEnumerable<VM_StoreDiscardForShow> GetStoreBthStockListForSearch(VM_StoreDiscardForSearch discardStore, Paging paging);
    }
}
