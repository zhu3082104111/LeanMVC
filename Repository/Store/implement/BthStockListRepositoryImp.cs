using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Repository
{
    public class BthStockListRepositoryImp : AbstractRepository<DB, BthStockList>, IBthStockListRepository
    {
        #region 自动生成测试数据 C:梁龙飞-----------只有作者可删除

        /// <summary>
        /// 逆流生成单配批次
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool createAbnormalBth(VM_MatBtchStockSearch condition)
        {
            string[] gicls={"001","002","003","004","005","006","007"};
            //取仓库编号          
            Random rnd = new Random();
            BthStockList testD = new BthStockList()
            {
                BillType = "19",
                PrhaOdrID = "LLF" + DateTime.Now.ToString("yyyyMMddhhmmss").Substring(7),
                BthID = "WX_" + DateTime.Now.ToString("yyyyMMddhhmmss").Substring(5),
                WhID = condition.WarehouseID,
                PdtID = condition.MaterialID,
                PdtSpec = "",
                GiCls = gicls[rnd.Next(0,6)],//999为非让步
                Qty = rnd.Next(1, 10) * 200,
                OrdeQty = 0,
                CmpQty = 0,
                DisQty = 0,
                InDate = DateTime.Now,
                CreDt = DateTime.Now,
                CreUsrID = "201228",
                DiscardFlg = "0"
            };
            return Add(testD);
        }

        /// <summary>
        /// 自动生成正常品批次别库存信息：测试使用：梁龙飞专用
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool addTestData(VM_MatBtchStockSearch condition)
        {
            //取仓库编号          
            Random rnd=new Random();
            BthStockList testD = new BthStockList()
            {
                BillType = "19",
                PrhaOdrID = "LLF" + DateTime.Now.ToString("yyyyMMddhhmmss").Substring(7),
                BthID = "WX_" + DateTime.Now.ToString("yyyyMMddhhmmss").Substring(5),
                WhID = condition.WarehouseID,
                PdtID = condition.MaterialID,
                PdtSpec = condition.Specification,
                GiCls = "999",
                Qty =rnd.Next(1,10)* 200,
                OrdeQty = 0,
                CmpQty = 0,
                DisQty = 0,
                InDate = DateTime.Now,
                CreDt = DateTime.Now,
                CreUsrID = "201228",
                DiscardFlg = "0"
            };
            if (condition.Specification!="" &&condition.Specification!=null)
            {
                return Add(testD);
            }
            return false;
        }
        #endregion

        #region 基础功能 C:梁龙飞

        public decimal CountNotLocked(string partID, bool isNormal)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BthStockList> BatchCanLocked(string pratID, bool isNormal)
        {
            throw new NotImplementedException();
        }
       
        #endregion

        #region C:梁龙飞:订单排产界面锁存功能集：主要是检索功能
        /// <summary>
        /// 检索有型号的正常品的已锁存信息
        /// C：梁龙飞 20131210
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetNormalLockedBatch(VM_MatBtchStockSearch condition)
        {
            //条件检测---...
            //预约信息,一个订单的零件只有一条【？是否检测数据库异常：一个订单的零件有多个预约】
            IQueryable<Reserve> storeReserve = GetAvailableList<Reserve>().
                Where(a => a.ClnOdrID == condition.ClientOrderID.Trim() && 
                    a.ClnOdrDtl == condition.OrderDetail.Trim()&&a.PdtID==condition.MaterialID.Trim());
            //仓库预约详细表:此预约详细号下的详细记录
            IQueryable<ReserveDetail> storeReserveDetail = GetAvailableList<ReserveDetail>();
            //批次别库存表
            IQueryable<BthStockList> bthstore = GetAvailableList<BthStockList>();

            IEnumerable<VM_LockReserveShow> finalData = from sR in storeReserve
                                                            join sRD in storeReserveDetail
                                                            on sR.OrdeBthDtailListID equals sRD.OrdeBthDtailListID
                                                            join bS in bthstore
                                                            on new { sR.WhID, sR.PdtID, sRD.BthID } equals new { bS.WhID, bS.PdtID, bS.BthID }
                                                            //where sRD.BthID == bS.BthID
                                                            select new VM_LockReserveShow()
                                                            {
                                                                OriginFlag = BatchOrigin.Locked,//已经预约
                                                                WhID = sR.WhID,//仓库编号
                                                                ClientOrderID = sR.ClnOdrID,//客户订单号
                                                                OrderDetail = sR.ClnOdrDtl,//客户订单详细
                                                                ProductID = sR.OrdPdtID,//产品ID
                                                                MaterialID = sR.PdtID,

                                                                BthID = sRD.BthID,
                                                                OrderQuantity = sRD.OrderQty,
                                                                Specification = bS.PdtSpec,

                                                                TotAvailable = bS.Qty - bS.OrdeQty//可预约数量                                                             
                                                            };            
            return finalData;
        }

        /// <summary>
        /// 获得带有型号规格的正常可锁：必需有型号规格要求【？】
        /// C：梁龙飞 20131210
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetNormalUnLockedBatch(VM_MatBtchStockSearch condition)
        {
            ////测试数据自动生成
            //addTestData(condition);           
            IQueryable<BthStockList> bSL = GetAvailableList<BthStockList>().Where(a=>a.PdtID==condition.MaterialID
                &&a.WhID==condition.WarehouseID
                && (a.Qty - a.OrdeQty > 0) &&
                a.GiCls=="999");
            IEnumerable<VM_LockReserveShow> finalData = bSL.Select(
                a=>
                new VM_LockReserveShow()
                {
                    OriginFlag=BatchOrigin.Unlocked,

                    MaterialID=a.PdtID,

                    WhID=a.WhID,//仓库号

                    BthID=a.BthID,

                    Specification=a.PdtSpec,

                    TotAvailable=a.Qty-a.OrdeQty

                });
            //暂时返回固定行数（要修改成可锁数量和不小于需求量的形势）
            return  finalData.Take(condition.Rows);;
        }

        /// <summary>
        /// 订单下产品的让步锁存信息
        /// C：梁龙飞 20131210
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetAbnormalLockedBatch(VM_MatBtchStockSearch condition)
        {

            //让步预约
            IQueryable<GiReserve> giRsv = GetAvailableList<GiReserve>().Where(
                a => a.PrhaOrderID == condition.ClientOrderID.Trim() &&
                    a.ClientOrderDetail == condition.OrderDetail.Trim() &&
                    a.ProductID==condition.MaterialID.Trim());
            //批次别库存表
            IQueryable<BthStockList> bthSL = GetAvailableList<BthStockList>().Where(a =>
                a.WhID == condition.WarehouseID&&
                a.PdtID==condition.MaterialID&&
                a.GiCls!="999");

            //让步品 未锁信息
            IEnumerable<VM_LockReserveShow> finalData = from gR in giRsv
                                                            join bSL in bthSL
                                                            on new { BthID = gR.BatchID, WhID = gR.WareHouseID, PdtID = gR.ProductID } equals new { bSL.BthID, bSL.WhID, bSL.PdtID }
                                                            select new VM_LockReserveShow()
                                                            {
                                                                OriginFlag = BatchOrigin.Locked,//已经预约
                                                                WhID = gR.WareHouseID,
                                                                ClientOrderID =gR.PrhaOrderID ,//客户订单号
                                                                OrderDetail = gR.ClientOrderDetail,//客户订单详细
                                                                ProductID = gR.OrdProductID,//产品ID
                                                                MaterialID = gR.ProductID,

                                                                BthID =bSL.BthID,
                                                                OrderQuantity =gR.OrderQuantity ,
                                                                Specification =bSL.GiCls ,//让步区分
                                                                GiveinCatID=bSL.GiCls,
                                                                TotAvailable = bSL.Qty - bSL.OrdeQty//可预约数量  
                                                            };


            return finalData;
        }

        
        /// <summary>
        /// 让步可锁批次显示
        /// C：梁龙飞 20131210
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetAbnormalUnlockedBatch(VM_MatBtchStockSearch condition)
        {
            //createAbnormalBth(condition);//测试数据逆流生成

            IQueryable<BthStockList> bSL = GetAvailableList<BthStockList>().Where(
                a => a.PdtID == condition.MaterialID &&
                    (a.Qty - a.OrdeQty > 0)&&
                    a.GiCls!="999");///暂用999代替，【！999】表示 让步产品
            IEnumerable<VM_LockReserveShow> finalData = bSL.Select(
                a =>
                new VM_LockReserveShow()
                {
                    OriginFlag = BatchOrigin.Unlocked,
                    WhID=a.WhID,
                    MaterialID = a.PdtID,
                    BthID = a.BthID,
                    Specification = a.GiCls,//让步区分，暂不处理
                    GiveinCatID = a.GiCls,
                    TotAvailable = a.Qty - a.OrdeQty
                });
            //暂时返回固定行数（要修改成可锁数量和不小于需求量的形势）
            return finalData.Take(condition.Rows);
        }
        #endregion
         
        #region IBthStockListRepository 成员


        public BthStockList SelectBthStockList(BthStockList btockList)
        {
            return base.First(a => a.BillType == btockList.BillType && a.BthID==btockList.BthID && a.WhID==btockList.WhID && a.PdtID==btockList.PdtID &&a.DelFlag=="0");

        }

        #endregion

        #region IBthStockListRepository 成员（主键删除该条数据，yc添加）


        public bool DelBthStockList(BthStockList bthStockList)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set DEL_FLG='1', DEL_USR_ID={0}, DEL_DT={1} where BILL_TYPE={1} and BTH_ID={2} and WH_ID={3} and PDT_ID={4} ", bthStockList.DelUsrID, bthStockList.DelDt, bthStockList.BillType, bthStockList.BthID, bthStockList.WhID, bthStockList.PdtID);
        }

        #endregion

        //#region IBthStockListRepository 成员(修改批次别数量，yc添加)


        //public bool UpdateBthStockList(BthStockList bthStockList)
        //{
        //    return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set QTY={0}, ORDE_QTY={1} where BILL_TYPE={2} and BTH_ID={3} and WH_ID={4} and PDT_ID={5} ", bthStockList.Qty, bthStockList.Qty, bthStockList.BillType, bthStockList.BthID, bthStockList.WhID, bthStockList.PdtID);
        //}
        //#endregion  



        /// <summary>
        /// 成品库出库履历详细修改批次别库存表 陈健
        /// </summary>
        /// <param name="bthStockList">更新数据集合</param>
        /// <returns>true</returns>
        public bool updateInBthStockListFinOut(BthStockList bthStockList)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set CMP_QTY=CMP_QTY+{0},UPD_USR_ID={1},UPD_DT={2} where BTH_ID={3} and WH_ID={4} and PDT_ID={5} and PDT_SPEC={6}", bthStockList.CmpQty, bthStockList.UpdUsrID, bthStockList.UpdDt, bthStockList.BthID, bthStockList.WhID, bthStockList.PdtID, bthStockList.PdtSpec);
        }



        #region IBthStockListRepository 成员(yc添加)


        public bool UpdateBthStockListForStore(BthStockList bthStockList)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set QTY={0}, ORDE_QTY={1},CMP_QTY={2}, UPD_USR_ID={3}, UPD_DT={4} where BTH_ID={5} and WH_ID={6} and PDT_ID={7} and PDT_SPEC={8}", bthStockList.Qty, bthStockList.OrdeQty, bthStockList.CmpQty, bthStockList.UpdUsrID, bthStockList.UpdDt, bthStockList.BthID, bthStockList.WhID, bthStockList.PdtID, bthStockList.PdtSpec);
        }

        #endregion

        /// <summary>
        /// 在制品库出库登录修改批次别库存表
        /// </summary>
        /// <param name="bthStockList"></param>
        /// <returns></returns>
        public bool UpdateBthStockListForOut(BthStockList bthStockList)
        {
            return base.ExecuteStoreCommand("update MC_WH_BTH_STOCK_LIST set ORDE_QTY={0},CMP_QTY={1},UPD_USR_ID={2},UPD_DT={3} where BILL_TYPE={4} and BTH_ID={5} and WH_ID={6} and PDT_ID={7}", bthStockList.OrdeQty, bthStockList.CmpQty, bthStockList.UpdUsrID, bthStockList.UpdDt, bthStockList.BillType,bthStockList.BthID, bthStockList.WhID, bthStockList.PdtID);
        }
    }
}
