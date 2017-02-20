using BLL.ServerMessage;
using Extensions;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DiscardServiceImp : AbstractService, IDiscardService 
    {
         private IDiscardRepository discardRepository;
        //构造函数
         public DiscardServiceImp(IDiscardRepository discardRepository) 
        {
            this.discardRepository = discardRepository;
        }



        
         /// <summary>
         /// 在库品报废申请一览的报废与取消
         /// </summary>
         /// <param name="discardList">零件号</param>
         /// <param name="flg">标识字符</param>
         /// <returns>massage信息</returns>
         public string DiscardForScrappedOrCancel(VM_StoreDiscardDetailForSearch discardList, string flg) 
         {
             string list="";
             if (flg == "0")//报废
             {
                 list= DiscardForLoginUpdateReserve(discardList);
             }
             else if (flg == "1")//报废取消
             {
                 list = DiscardForLoginUpdate(discardList);
             }
             return list;

         }

        
         /// <summary>
         /// 在库品报废申请一览详细跳转
         /// </summary>
         /// <param name="search">查询数据</param>
         /// <returns>查询结果</returns
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetDiscarorSearchList(VM_StoreDiscardDetailForSearch search, Paging paging)
         {
             string flg = "";
             return discardRepository.GetStoreDiscardFordetail(search, paging, flg);
         }

         #region 报废方法

         /// <summary>
         /// 报废
         /// </summary>
         /// <param name="discardList">查询数据</param>
         /// <returns>无</returns>
         public string DiscardForLoginUpdateReserve(VM_StoreDiscardDetailForSearch discardList)
         {
             string list ="报废成功";
             var listTo = discardRepository.SearchBthStockList(discardList).ToList();
             BthStockList bthStockList = new BthStockList();
             Discard discard = new Discard();
             Material material = new Material();
             GiMaterial giMaterial = new GiMaterial();
             if (discardList.State == "00")//根据让步字段判断更新表
             {
                 //
                 //修改仓库表
                 material.CurrentQty = listTo[0].CurrentQty;
                 material.UseableQty = listTo[0].UseableQty;
                 material.TotalAmt = listTo[0].TotalAmtTo;
                 material.TotalValuatUp = listTo[0].TotalValuatUp;
                 material.PdtID = discardList.PdtID;
                 discardRepository.UpdateMaterial(material);
             }
             else
             {
                 //
                 //修改让步仓库表
                 giMaterial.UserableQuantity = listTo[0].UseableQty;
                 giMaterial.CurrentQuantity = listTo[0].CurrentQty;
                 giMaterial.TotalAmt = listTo[0].TotalAmtTo;
                 giMaterial.ProductID = discardList.PdtID;
                 giMaterial.BatchID = discardList.BthID;
                 discardRepository.UpdateGiMaterial(giMaterial);
 
             }

             //
             //更新废弃物履历表
             discard.DiscardID = discardList.DiscardID;
             discard.WareHouseID = discardList.WhID;
             discard.BThId = discardList.BthID;
             discard.PartDtID = discardList.PdtID;
             discardRepository.deletDiscard(discard);
             //
             //修改批次别库存表
             bthStockList.PdtID= discardList.PdtID;
             bthStockList.BthID = discardList.BthID;
             bthStockList.WhID = discardList.WhID;
             bthStockList.Qty = listTo[0].Quantity;
             discardRepository.UpdateBthStockList(bthStockList);
             return list;
         }
        #endregion

         #region 报废取消方法
         /// <summary>
         /// 报废取消方法
         /// </summary>
         /// <param name="discardList">查询数据</param>
         /// <returns>无</returns>
         public string DiscardForLoginUpdate(VM_StoreDiscardDetailForSearch discardList) 
         {
             string list="保费取消已成功";
             var listTo = discardRepository.SearchBthStockList(discardList).ToList();
              Discard discard = new Discard();
              BthStockList bthStockList = new BthStockList();
              //
              //updata bthStockList表
              bthStockList.BthID = discardList.BthID;
              bthStockList.PdtID = discardList.PdtID;
              bthStockList.WhID = discardList.WhID;
              discardRepository.UpdateBthStockListRe(bthStockList);
             //
             //updata Disacard 表
              discard.DiscardID = discardList.DiscardID;
              discard.WareHouseID= discardList.WhID;
              discard.BThId = discardList.BthID;
              discard.PartDtID = discardList.PdtID;
              discardRepository.UpdataDiscardRe(discard);
              return list;

         }
        #endregion

         #region 通用的方法
         //
         //在库品报废申请详细
         public IEnumerable<VM_StoreDiscardForTableShow> GetdiscardStoreForSearch(VM_storeDiscardForSearch discardStore, Paging paging)
         {
             return discardRepository.GetDiscardStoreWithPaging(discardStore,paging);
                 
         }

        //
        //在库待报废品一览
         public IEnumerable<VM_StoreDiscardForShow> GetStoreDiscardForSearch(VM_StoreDiscardForSearch discardStore, Paging paging)
         {
             return discardRepository.GetStoreDiscardWithPaging(discardStore, paging);
         }

        //
        //弹出页面一览
         public IEnumerable<VM_StoreDiscardForShow> GetStoreBthStockListForSearch(VM_StoreDiscardForSearch discardStore, Paging paging)
         {
             return discardRepository.GetStoreBthStockListWithPaging(discardStore, paging);
         }

        //
        //在库报废品申请详细
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHForSearch(VM_StoreDiscardDetailForSearch discardStore, Paging paging, string flg)
         {
             return discardRepository.GetStoreDiscardWHWithPaging(discardStore, paging, flg);
         }

         //
         //在库待报废品一览批量查询批量查询
         public IEnumerable<VM_StoreDiscardDetailForTableShow> GetStoreDiscardWHForSearchList(string id, string iid, string BthID, string discardId, Paging paging)
         {
             return discardRepository.GetStoreDiscardWHWithPagingList(id,iid, BthID,discardId,paging);
         }

        #endregion

         #region 在库品报废申请详细的保存和更新
         /// <summary>
         /// 保存和更新
         /// </summary>
         /// <param name="finInRecord">零件号</param>
         /// <param name="orderList">查询数据</param>
         /// <param name="id">标识字符</param>
         /// <returns>massage信息</returns>
         public string DiscardSaveUpdata(VM_StoreDiscardDetailForTableShow discardRecordList, Dictionary<string, string>[] orderList, string id)
        {
            string list;
            if (discardRepository.GetDiscardForList(discardRecordList, orderList) == null)//判断表中是否有词条数据
            {
                list = discardSave(discardRecordList, orderList, id);//保存 
            }
            else
            {
                list = DiscardUpdata(discardRecordList, orderList);//更新
            }
            return list;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="finInRecord">零件号</param>
        /// <param name="orderList">查询数据</param>
        /// <param name="id">标识字符</param>
        /// <returns>massage信息</returns>
         public string discardSave(VM_StoreDiscardDetailForTableShow discardRecordList, Dictionary<string, string>[] orderList, string id)
        {
            VM_StoreDiscardDetailForSearch discard = new VM_StoreDiscardDetailForSearch();
           
            //入库履历添加
            string eList = "保存成功";
            for (int i = 0; i < orderList.Length; i++)
            {
                discard.PdtID = orderList[i]["PartDtID"];
                //查询到一些穿不过来又需要的数据
                var listTo = discardRepository.SearchBthStockList(discard).ToList();
                // 判断批次别库存表中的此条数据是否数量足够
                BthStockList bthStockList = new BthStockList();
                bthStockList.PdtID = orderList[i]["PartDtID"];
                bthStockList.BthID = orderList[i]["BThId"];
                bthStockList.DisQty = decimal.Parse(orderList[i]["Quantity"]);
                bthStockList.WhID = orderList[i]["WareHouseID"];
                bthStockList.Qty = decimal.Parse(orderList[i]["Quantity"]);
                if (discardRepository.GetBthStockListForList(bthStockList))
                {
                    throw new Exception(SM_Store.SMSG_STORE_E10001);
                }
                
                //更新批次别库存表
                discardRepository.SavaBthStockList(bthStockList);

                //保存在废弃物履历表中    
                    discardRepository.Add(new Discard
                 {
                     DiscardID = discardRecordList.DiscardID,
                     WareHouseID = orderList[i]["WareHouseID"],
                     PartDtID = orderList[i]["PartDtID"],
                     BThId = orderList[i]["BThId"],
                     PartDtName = orderList[i]["PartDtName"],
                     PartDtSpec = orderList[i]["PartDtSpec"],
                     Quantity = decimal.Parse(orderList[i]["Quantity"]),
                     PrchsUp = decimal.Parse(orderList[i]["PrchsUp"]),
                     TotalAmt = decimal.Parse(orderList[i]["Quantity"]) * decimal.Parse(orderList[i]["PrchsUp"]),
                     QualityPro = orderList[i]["QualityPro"],
                     SltPlan = orderList[i]["SltPlan"],
                     //SltDate =DateTime.Parse(orderList[i]["InDate"]),
                 });
                }
                
            
           return eList;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="finInRecord">零件号</param>
        /// <param name="orderList">查询数据</param>
        /// <returns>massage信息</returns>
         public string DiscardUpdata(VM_StoreDiscardDetailForTableShow discardRecordList, Dictionary<string, string>[] orderList)
        {
            string eList = "更新成功";
            for (int i = 0; i < orderList.Length; i++)
            {
                Discard discard = new Discard();
                discard.PartDtID = orderList[i]["PartDtID"];
                discard.Quantity =decimal.Parse(orderList[i]["Quantity"]);
                discard.SltPlan = orderList[i]["SltPlan"];
                discard.DiscardID = discardRecordList.DiscardID;
                discard.WareHouseID = orderList[i]["WareHouseID"];
                //更新discard数据库
                discardRepository.UpdataDiscardDetail(discard);
            }
            return eList;
        }

        #endregion






    }
}
