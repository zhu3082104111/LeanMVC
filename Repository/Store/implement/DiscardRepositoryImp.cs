using Extensions;
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DiscardStoreRepositoryImp : AbstractRepository<DB, Discard>, IDiscardRepository
    {
        #region  
        /// <summary>
        /// 弹出页面一览
        /// </summary>
        /// <param name="discardStore">查询用的数据</param>
        /// <returns>查询结果</returns>
        public IEnumerable<VM_StoreDiscardForShow> GetStoreBthStockListWithPaging(VM_StoreDiscardForSearch discardStore, Paging paging)
        {
            DateTime? currentTime = new DateTime();
            currentTime = System.DateTime.Now.AddDays(-365);
            IQueryable<BthStockList> endList = base.GetList<BthStockList>().Where(h => h.DelFlag.Equals("0") &&
                h.EffeFlag.Equals("0") && (h.OrdeQty.Equals(0)) && ((h.Qty - h.CmpQty - h.DisQty) > 0) );

            IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
            if (!String.IsNullOrEmpty(discardStore.PartDtID))
            {
                endList = endList.Where(u => u.WhID.Contains(discardStore.PartDtID));
            }
            if (!String.IsNullOrEmpty(discardStore.PartDtName))
            {
                endList = endList.Where(u => u.WhID.Contains(discardStore.PartDtName));
            }

            IQueryable<VM_StoreDiscardForShow> query = from o in endList
                                                       join ol in firstList on o.PdtID equals ol.PartAbbrevi
                                                       select new VM_StoreDiscardForShow
                                                       {
                                                           ID=ol.PartAbbrevi,
                                                           PartDtID = o.PdtID,
                                                           PartDtName = ol.PartName,//物料名称
                                                           PartDtSpec = o.PdtSpec,//规格
                                                           StoreCls = o.GiCls,//让步区分
                                                           WareHouseID = o.WhID,
                                                           InDate = o.InDate,//入库日期
                                                           Pricee = ol.Pricee,// 单价
                                                           BthID = o.BthID//批次号

                                                       };
            paging.total = query.Count();
            IEnumerable<VM_StoreDiscardForShow> result = query.ToPageList<VM_StoreDiscardForShow>("WareHouseID asc", paging);
            return result;
        }
        #endregion

        #region 在库待报废品一览查询
        /// <summary>
        /// 待库报废品一览查询
        /// </summary>
        /// <param name="discardStore">查询用的数据</param>
        /// <returns>查询结果</returns>
        public IEnumerable<VM_StoreDiscardForShow> GetStoreDiscardWithPaging(VM_StoreDiscardForSearch discardStore, Paging paging)
        {
            DateTime? currentTime=new DateTime();
            currentTime = System.DateTime.Now.AddDays(-365);
            IQueryable<BthStockList> endList = base.GetList<BthStockList>().Where(h => h.DelFlag.Equals("0") &&
                h.EffeFlag.Equals("0") && (h.OrdeQty.Equals(0)) && ((h.Qty - h.CmpQty - h.DisQty) > 0)&&(currentTime > h.InDate));
                
            IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
            if (!String.IsNullOrEmpty(discardStore.WareHouseID))
            {
                endList = endList.Where(u => u.WhID.Contains(discardStore.WareHouseID));
            }

            IQueryable<VM_StoreDiscardForShow> query = from o in endList
                                                       join ol in firstList on o.PdtID equals ol.PartAbbrevi
                                                       select new VM_StoreDiscardForShow
                                                               {
                                                                   PartDtID = o.PdtID,
                                                                   BthID=o.BthID,
                                                                   PartDtName = ol.PartName,
                                                                   PartDtSpec = o.PdtSpec,
                                                                   Quantity = o.Qty,
                                                                   StoreCls = o.GiCls,
                                                                   WareHouseID = o.WhID,
                                                                   InDate = o.InDate,
                                                                   PartAbbrevi=ol.PartAbbrevi

                                                               };
            paging.total = query.Count();
            IEnumerable<VM_StoreDiscardForShow> result = query.ToPageList<VM_StoreDiscardForShow>("WareHouseID asc", paging);
            return result;
        }
        #endregion

        #region  在库品报废申请一览
        static DateTime time = new DateTime();
        /// <summary>
        /// 在库待报废品一览查询
        /// </summary>
        /// <param name="discardStore">查询用的数据</param>
        /// <returns>查询结果</returns>
        public IEnumerable<VM_StoreDiscardForTableShow> GetDiscardStoreWithPaging(VM_storeDiscardForSearch discardStore, Paging paging)
        {
            IQueryable<Discard> endList = base.GetList<Discard>().Where(h => (!h.ApprovalFlg.Equals("2")) && h.DelFlag.Equals("0") && h.EffeFlag.Equals("0"));
            IQueryable<UserInfo> endToList = base.GetList<UserInfo>();
           
                //报废单号
                if ((!String.IsNullOrEmpty(discardStore.discardId)))
                {
                    endList = endList.Where(u => u.DiscardID.Contains(discardStore.discardId));
                }
                //申请状态
                if (!String.IsNullOrEmpty(discardStore.State))
                {
                    if (discardStore.State == "3")
                    { }
                    else
                    {
                        endList = endList.Where(u => u.State.Contains(discardStore.State));
                    }
                    
                }
                //仓库
                if ((!String.IsNullOrEmpty(discardStore.WareHouseID)))
                {
                    if (discardStore.WareHouseID == "4")
                    { }
                    else
                    {
                        endList = endList.Where(u => u.WareHouseID.Contains(discardStore.WareHouseID));
                    }
                }
                //入库日期
                if (discardStore.BeginDate != null)
                {
                    endList = endList.Where(a => a.CreDt >= discardStore.BeginDate);
                }
                if (discardStore.EndDate != null)
                {
                    endList = endList.Where(a => a.CreDt <= discardStore.EndDate);
                }
                IQueryable<VM_StoreDiscardForTableShow> query = (from o in endList
                                                                join ol in endToList on o.AplUserID equals ol.UId
                                                                select new VM_StoreDiscardForTableShow
                                                                   {
                                                                       discardId = o.DiscardID,
                                                                       AplUserID = o.AplUserID,
                                                                       AplUser = ol.UName,
                                                                       State = o.State,
                                                                       SltDate = o.SltDate
                                                                   }).Distinct();
                paging.total = query.Count();
                IEnumerable<VM_StoreDiscardForTableShow> result = query.ToPageList<VM_StoreDiscardForTableShow>("discardId asc", paging);
                return result;
            

        }
        #endregion

        #region 在库品报废申请一览业务逻辑
        //
        //报废updata批次别库存表
        public bool UpdateBthStockList(BthStockList discard)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set DISCARD_FLG={0},QTY=QTY-{1} where PDT_ID={2} and BTH_ID={3} and WH_ID={4}", "1", discard.Qty, discard.PdtID,discard.BthID,discard.WhID);
        }


        //
        //报废废弃物履历表中产生报废
        public bool deletDiscard (Discard discard)
        {
            DateTime? time = new DateTime();
            time = System.DateTime.Now;
            return base.ExecuteStoreCommand("update MC_WH_DISCARD set APPROVAL_FLG={0},STATE={1},SLT_DATE={2} where DISCARD_ID={3} and WH_ID={4}", "1", "2", time, discard.DiscardID,discard.WareHouseID);
        }

        //
        //报废修改仓库表
        public bool UpdateMaterial(Material discard)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set USEABLE_QTY={0},CURRENT_QTY={1},TOTAL_AMT={2},TOTAL_VALUAT_UP={3} where PDT_ID={4}", 
                discard.UseableQty,discard.CurrentQty,discard.TotalAmt,discard.TotalValuatUp,discard.PdtID);
        }

        //
        //报废修改让步仓库表
        public bool UpdateGiMaterial(GiMaterial discard)
        {
            return base.ExecuteStoreCommand("update MC_WH_MATERIAL set USEABLE_QTY={0},CURRENT_QTY={1},TOTAL_AMT={2},TOTAL_VALUAT_UP={3} where PDT_ID={4}",
                discard.UserableQuantity, discard.CurrentQuantity, discard.TotalAmt, discard.TotalValuatUp, discard.ProductID);
        }

        //
        //报废取消updata discard表
        public bool UpdataDiscardRe(Discard discard)
        {
            DateTime? time = new DateTime();
            time = System.DateTime.Now;
            return base.ExecuteStoreCommand("update MC_WH_DISCARD set DEL_FLG={0},SLT_DATE={1} where DISCARD_ID={2} and WH_ID={3} and PDT_ID={4} and BTH_ID={5}", "1", time, discard.DiscardID, discard.WareHouseID, discard.PartDtID,discard.BThId);
        }

        //
        //报废取消updata bthStock表
        public bool UpdateBthStockListRe(BthStockList discard)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set DISCARD_FLG={0} where PDT_ID={1} and WH_ID={2} and PDT_ID={3}", "0", discard.PdtID,discard.WhID,discard.PdtID);
        }


        
        /// <summary>
        /// 取得updata所需数据
        /// </summary>
        /// <param name="discardStore">查询用的数据</param>
        /// <returns>查询结果</returns>
         public IEnumerable<VM_StoreDiscardDetailForTableShow> SearchBthStockList(VM_StoreDiscardDetailForSearch discardStore)
        {
            IQueryable<BthStockList> endList = base.GetList<BthStockList>();
            IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
            IQueryable<Material> firstMaterialList = base.GetList<Material>();
            IQueryable<Discard> endToList = base.GetList<Discard>();
            
                endToList = endToList.Where(u => (u.DiscardID==discardStore.DiscardID)&&(u.PartDtID==discardStore.PdtID)&&(u.WareHouseID==discardStore.WhID)&&(u.BThId==discardStore.BthID));
                IQueryable<VM_StoreDiscardDetailForTableShow> query = from oo in endToList
                                                                      join o in endList on oo.PartDtID equals o.PdtID
                                                                      join ol in firstList on o.PdtID equals ol.PartId
                                                                      join ool in firstMaterialList on o.PdtID equals ool.PdtID
                                                                      select new VM_StoreDiscardDetailForTableShow
                                                                      {
                                                                          DiscardID=oo.DiscardID,
                                                                          PartDtID = oo.PartDtID,
                                                                          PartDtName = ol.PartName,
                                                                          PartDtSpec = o.PdtSpec,
                                                                          Quantity = oo.Quantity,
                                                                          QuantityDiscard=oo.Quantity,
                                                                          QuantityTotle=(o.Qty-oo.Quantity),
                                                                          PrchsUp = ol.Pricee,
                                                                          TotalAmt = (ol.Pricee * oo.Quantity),
                                                                          TotalAmtTo=(ool.TotalAmt-(ol.Pricee*oo.Quantity)),
                                                                          StoreCls = oo.StoreCls,
                                                                          WareHouseID = oo.WareHouseID,
                                                                          InDate = o.InDate,
                                                                          QualityPro = "超过在库期限",
                                                                          SltPlan = "销毁",
                                                                          UseableQty=ool.UseableQty-oo.Quantity,
                                                                          CurrentQty=ool.CurrentQty

                                                                      };
                return query;

           
        }

       #endregion

        #region 在库品报废申请详细updata
         //
        //更新Discard表
         public bool UpdataDiscardDetail(Discard discard)
         {

             return base.ExecuteStoreCommand("update MC_WH_DISCARD set DIS_QTY={0},SLT_PLN={1} where DISCARD_ID={2} and WH_ID={3} and PDT_ID={4}",discard.Quantity,discard.SltPlan,discard.DiscardID,discard.WareHouseID,discard.PartDtID);
         }

         /// <summary>
         /// 由传入主键查找Discard中的数据
         /// </summary>
         /// <param name="discardStore">查询用的数据</param>
         /// <param name="orderList">查询用的数据</param>
         /// <returns>查询结果</returns>
         public Discard GetDiscardForList(VM_StoreDiscardDetailForTableShow discardRecord, Dictionary<string, string>[] orderList)
         {
             Discard discardList = new Discard();//查出的数据
             for (int i = 0; i < orderList.Length; i++)
             {
                 string WareHouseID = "";
                 if (orderList[i]["WareHouseID"] == "分离单元附件库") { WareHouseID = "1"; }
                 else if (orderList[i]["WareHouseID"] == "成品库") { WareHouseID = "2"; }
                 else if (orderList[i]["WareHouseID"] == "半成品库") { WareHouseID = "3"; }
                 else { WareHouseID = orderList[i]["WareHouseID"]; }
                 string PartDtID = orderList[i]["PartDtID"];
                 discardList = base.First(a => a.WareHouseID == WareHouseID && a.DiscardID == discardRecord.DiscardID && a.PartDtID == PartDtID);
             }
             return discardList;

         }

        
         /// <summary>
         /// 判断批次别库存表中的此条数据是否数量足够
         /// </summary>
         /// <param name="discardStore">查询用的数据</param>
         /// <returns>查询结果</returns>
         public bool GetBthStockListForList(BthStockList bthStockList)
         {
             IEnumerable<BthStockList> endList = base.GetList<BthStockList>();
             bool b=false;

             string PartDtID = bthStockList.PdtID; //orderList[i]["PartDtID"];
             string BThId = bthStockList.BthID;//orderList[i]["BThId"];
             string WHId = bthStockList.WhID; //orderList[i]["WareHouseID"];
             decimal qtyb = bthStockList.Qty;
             endList = endList.Where(u => (u.PdtID == PartDtID) && (u.WhID == WHId) && (u.BthID == BThId) && ((u.Qty - u.CmpQty - u.DisQty - qtyb) > 0));
             
             if (endList.Count() == 0)
             {
                 b = true;
                 
             }
             else
             {
                 b = false;
             }
            
             return b; 
 
         }


         #endregion

        #region 保存
         //
         //报废updata批次别库存表
         public bool SavaBthStockList(BthStockList discard)
         {
            DB db=new DB();
            BthStockList t = db.Set<BthStockList>().First((a => a.BthID == discard.BthID && a.WhID == discard.WhID&&a.PdtID==discard.PdtID));
            decimal nowqty =  t.DisQty + discard.DisQty;
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set DISCARD_FLG={0} where PDT_ID={1} and BTH_ID={2} and WH_ID={3}", "1", discard.PdtID, discard.PdtID, discard.WhID);
         }
        #endregion

         #region 从在库待报废品一览跳转到库待报废品详细查询
         /// <summary>
         /// 从在库待报废品一览跳转到库待报废品详细查询
         /// </summary>
         /// <param name="discardStore">查询用的数据</param>
         /// <param name="flg">标识参数</param>
         /// <returns>查询结果</returns>
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHWithPaging(VM_StoreDiscardDetailForSearch discardStore, Paging paging, string flg)
        {
            
            IQueryable<BthStockList> endList = base.GetList<BthStockList>();
            IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
            IQueryable<Discard> endToList = base.GetList<Discard>();
            if (!String.IsNullOrEmpty(discardStore.PdtID))
            //在库一览跳转查询    
            {
                endList = endList.Where(u => u.PdtID.Contains(discardStore.PdtID));
                if (flg == "005") 
                {
                    firstList = firstList.Where(u => u.PartId.Equals(discardStore.PdtID));
                    IQueryable<VM_StoreDiscardDetailForTableShow> query1 = from o in firstList
                                                                          
                                                                          select new VM_StoreDiscardDetailForTableShow
                                                               {
                                                                   PartDtID = o.PartId,
                                                                   PartDtName = o.PartName,
                                                                   
                                                               };
               
                paging.total = query1.Count();
                IEnumerable<VM_StoreDiscardDetailForTableShow> result = query1.ToPageList<VM_StoreDiscardDetailForTableShow>("PartDtID asc", paging);
                return result; 
                }
                else
                {
                    endList = endList.Where(u=>u.WhID.Equals(discardStore.WhID));
                    IQueryable<VM_StoreDiscardDetailForTableShow> query = from o in endList
                                                                          join ol in firstList on o.PdtID equals ol.PartId
                                                                          select new VM_StoreDiscardDetailForTableShow
                                                               {
                                                                   PartDtID = o.PdtID,
                                                                   PartDtName = ol.PartName,
                                                                   PartDtSpec = o.PdtSpec,
                                                                   Quantity = o.Qty,
                                                                   PrchsUp = ol.Pricee,
                                                                   TotalAmt = (ol.Pricee * o.Qty),
                                                                   StoreCls = o.GiCls,
                                                                   WareHouseID = o.WhID,
                                                                   InDate = o.InDate,
                                                                   QualityPro = "超过在库期限",
                                                                   SltPlan = "销毁"
                                                               };
               
                paging.total = query.Count();
                IEnumerable<VM_StoreDiscardDetailForTableShow> result = query.ToPageList<VM_StoreDiscardDetailForTableShow>("WareHouseID asc", paging);
                return result; }

            }
            //参照画面查询
            else if (!String.IsNullOrEmpty(discardStore.DiscardID))
            {
                endToList = endToList.Where(u => u.DiscardID.Contains(discardStore.DiscardID));

                IQueryable<VM_StoreDiscardDetailForTableShow> query = from oo in endToList
                                                                       
                                                                      join ol in firstList on oo.PartDtID equals ol.PartId
                                                                      select new VM_StoreDiscardDetailForTableShow
                                                                      {
                                                                          PartAbbrevi=ol.PartAbbrevi,
                                                                          BthID=oo.BThId,
                                                                          PartDtID = oo.PartDtID,
                                                                          PartDtName = ol.PartName,
                                                                          PartDtSpec = oo.PartDtSpec,
                                                                          Quantity = oo.Quantity,
                                                                          PrchsUp = ol.Pricee,
                                                                          TotalAmt = (ol.Pricee * oo.Quantity),
                                                                          StoreCls = oo.StoreCls,
                                                                          WareHouseID = oo.WareHouseID,
                                                                          InDate =ol.UpdDt,
                                                                          QualityPro = "超过在库期限",
                                                                          SltPlan = "销毁"
                                                                      };
                paging.total = query.Count();
                IEnumerable<VM_StoreDiscardDetailForTableShow> result = query.ToPageList<VM_StoreDiscardDetailForTableShow>("WareHouseID asc", paging);
                return result;

            }
            else
            {
                return null;
            }

       

        }

         /// <summary>
         /// 从在库待报废品一览跳转到库待报废品详细查询
         /// </summary>
         /// <param name="discardStore">查询用的数据</param>
         /// <param name="flg">标识参数</param>
         /// <returns>查询结果</returns>
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardFordetail(VM_StoreDiscardDetailForSearch discardStore, Paging paging, string flg)
         {

             IQueryable<BthStockList> endList = base.GetList<BthStockList>();
             IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
             IQueryable<Discard> endToList = base.GetList<Discard>();

             endToList = endToList.Where(u => (u.DiscardID == discardStore.DiscardID));

                 IQueryable<VM_StoreDiscardDetailForTableShow> query = from oo in endToList

                                                                       join ol in firstList on oo.PartDtID equals ol.PartId
                                                                       select new VM_StoreDiscardDetailForTableShow
                                                                       {
                                                                           PartAbbrevi = ol.PartAbbrevi,
                                                                           BthID = oo.BThId,
                                                                           PartDtID = oo.PartDtID,
                                                                           PartDtName = ol.PartName,
                                                                           PartDtSpec = oo.PartDtSpec,
                                                                           Quantity = oo.Quantity,
                                                                           PrchsUp = ol.Pricee,
                                                                           TotalAmt = (ol.Pricee * oo.Quantity),
                                                                           StoreCls = oo.StoreCls,
                                                                           WareHouseID = oo.WareHouseID,
                                                                           InDate = ol.UpdDt,
                                                                           QualityPro = "超过在库期限",
                                                                           SltPlan = "销毁"
                                                                       };
                 paging.total = query.Count();
                 IEnumerable<VM_StoreDiscardDetailForTableShow> result = query.ToPageList<VM_StoreDiscardDetailForTableShow>("WareHouseID asc", paging);
                 return result;

             


         }
       
         /// <summary>
         /// 在库待报废品一览批量查询批量查询
         /// </summary>
         /// <param name="id">传入参数</param>
         /// <param name="iid">传入参数</param>
         /// <returns>查询结果</returns>
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHWithPagingList(string id, string iid,string BthID,string discardId, Paging paging)
         {
             IQueryable<BthStockList> endList = base.GetList<BthStockList>();
             IQueryable<BthStockList> endListTo = base.GetList<BthStockList>();
             IQueryable<PartInfo> firstList = base.GetList<PartInfo>();
             IQueryable<Discard> endToList = base.GetList<Discard>();
             var idList = id.Split(',');
             var iidList = iid.Split(',');
             var bthIDList = BthID.Split(',');
             if (discardId == "")
             {
                 endList = from a in endList where idList.Contains(a.PdtID) select a;
                 endList = from a in endList where iidList.Contains(a.WhID) select a;
                 endList = from a in endList where bthIDList.Contains(a.BthID) select a;
                 IQueryable<VM_StoreDiscardDetailForTableShow> query = from o in endList
                                                                       join ol in firstList on o.PdtID equals ol.PartId
                                                                       select new VM_StoreDiscardDetailForTableShow
                                                                       {
                                                                           PartDtID = o.PdtID,
                                                                           PartDtName = ol.PartName,
                                                                           PartDtSpec = o.PdtSpec,
                                                                           BthID = o.BthID,
                                                                           PartAbbrevi = ol.PartAbbrevi,
                                                                           Quantity = o.Qty,
                                                                           PrchsUp = ol.Pricee,
                                                                           TotalAmt = (ol.Pricee * o.Qty),
                                                                           StoreCls = o.GiCls,
                                                                           WareHouseID = o.WhID,
                                                                           InDate = o.InDate,
                                                                           QualityPro = "超过在库期限",
                                                                           SltPlan = "销毁"
                                                                       };

                 paging.total = query.Count();
                 IEnumerable<VM_StoreDiscardDetailForTableShow> result = query.ToPageList<VM_StoreDiscardDetailForTableShow>("WareHouseID asc", paging);
                 return result;
             }
             else {
                 //endToList = from a in endToList where idList.Contains(a.PartDtID) select a;
                 //endToList = from a in endToList where iidList.Contains(a.WareHouseID) select a;
                 //endToList = from a in endToList where bthIDList.Contains(a.BThId) select a;
                 endToList = from a in endToList where discardId.Contains(a.DiscardID) select a;
                 IQueryable<VM_StoreDiscardDetailForTableShow> query = from o in endToList
                                                                       join ol in firstList on o.PartDtID equals ol.PartId
                                                                       select new VM_StoreDiscardDetailForTableShow
                                                                       {
                                                                           PartDtID = o.PartDtID,
                                                                           PartDtName = ol.PartName,
                                                                           PartDtSpec = o.PartDtSpec,
                                                                           BthID = o.BThId,
                                                                           PartAbbrevi = ol.PartAbbrevi,
                                                                           Quantity = o.Quantity,
                                                                           PrchsUp = ol.Pricee,
                                                                           TotalAmt = (ol.Pricee * o.Quantity),
                                                                           StoreCls = o.StoreCls,
                                                                           WareHouseID = o.WareHouseID,
                                                                           InDate = o.UpdDt,
                                                                           QualityPro = "超过在库期限",
                                                                           SltPlan = "销毁"
                                                                       };

                 paging.total = query.Count();
                 IEnumerable<VM_StoreDiscardDetailForTableShow> result = query.ToPageList<VM_StoreDiscardDetailForTableShow>("WareHouseID asc", paging);
                 return result;
             }
 
         }

        #endregion
    }
}
