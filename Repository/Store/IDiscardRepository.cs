using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;

namespace Repository
{
    public interface IDiscardRepository : IRepository<Discard>
    {
        //GetDiscardStoreWithPaging
        IEnumerable<VM_StoreDiscardForTableShow> GetDiscardStoreWithPaging(VM_storeDiscardForSearch discardStore, Paging paging);
        //GetStoreDiscardWithPaging
        IEnumerable<VM_StoreDiscardForShow> GetStoreDiscardWithPaging(VM_StoreDiscardForSearch discardStore, Paging paging);


        //GetStoreDiscardWHWithPaging
        IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHWithPaging(VM_StoreDiscardDetailForSearch discardStore, Paging paging,string flg);
        
        
        #region updata用到的一些方法
        bool UpdateBthStockList(BthStockList BthdiscardId);
        IEnumerable<VM_StoreDiscardDetailForTableShow> SearchBthStockList(VM_StoreDiscardDetailForSearch discardStore);
        bool deletDiscard(Discard discard);
        bool UpdateMaterial(Material discard);
        bool UpdateGiMaterial(GiMaterial discard); 

        //报废取消updata discard表
        bool UpdataDiscardRe(Discard discard);

        //报废取消updata bthStock表
        bool UpdateBthStockListRe(BthStockList discard);

        //在库品报废申请详细updata
        bool UpdataDiscardDetail(Discard discard);
        Discard GetDiscardForList(VM_StoreDiscardDetailForTableShow discardRecord, Dictionary<string, string>[] orderList);
        #endregion

        //
        //在库待报废品一览批量查询批量查询
        IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHWithPagingList(string id, string iid, string BthID, string discardId, Paging paging);

        bool GetBthStockListForList(BthStockList bthStockList);

        //
        //弹出页面查询
        IEnumerable<VM_StoreDiscardForShow> GetStoreBthStockListWithPaging(VM_StoreDiscardForSearch discardStore, Paging paging);
        //
        //保存更新批次别库存表
        bool SavaBthStockList(BthStockList discard);
        //
        //详细跳转
        IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardFordetail(VM_StoreDiscardDetailForSearch discardStore, Paging paging, string flg);

    }
}
