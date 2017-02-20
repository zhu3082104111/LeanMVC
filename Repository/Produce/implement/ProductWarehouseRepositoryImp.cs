/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouseController.cs
// 文件功能描述：成品交仓资源库实现类
// 
// 创建标识：20131123 杜兴军
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository.database;
using Model.Store;
using System.Collections;

namespace Repository
{
    public class ProductWarehouseRepositoryImp:AbstractRepository<DB,ProductWarehouse>,IProductWarehouseRepository
    {

        public IEnumerable<VM_ProductWarehouseShow> GetWarehouseShowByPage(VM_ProductWarehouseSearch search,Paging paging)
        {
            IQueryable<ProductWarehouse> warehouseIQ = GetAvailableList<ProductWarehouse>() ;//成品交仓表
            IQueryable<ProductWarehouseDetail> warehouseDetailIQ = GetAvailableList<ProductWarehouseDetail>();//成品交仓详细表
            IQueryable<UserInfo> userInfoIQ = GetList<UserInfo>();//用户信息表
            IQueryable<MasterDefiInfo> masterDefiInfoIQ = GetAvailableList<MasterDefiInfo>();//master信息表

            masterDefiInfoIQ = masterDefiInfoIQ.Where(m => m.SectionCd.Equals(Constant.MasterSection.DEPT));//筛选所有部门记录

            warehouseIQ = warehouseIQ.FilterBySearch(search);//按条件筛选交仓单信息
            warehouseDetailIQ = warehouseDetailIQ.FilterBySearch(search);//按条件筛选交仓单详细信息
            userInfoIQ = userInfoIQ.Where(u => u.Enabled == "1");//筛选有效的用户记录

            //详细表信息，若详细表的检索条件存在，则应该与详细表连接查询，否则与详细表无关
            if (!String.IsNullOrEmpty(search.ClientOrderID) || !String.IsNullOrEmpty(search.OrderProductID) || !String.IsNullOrEmpty(search.TeamID))//和成品交仓详细表联接
            {
                warehouseIQ = from warehouse in warehouseIQ
                    join warehouseDetail in warehouseDetailIQ on warehouse.ProductWarehouseID equals
                        warehouseDetail.ProductWarehouseID
                    select warehouse;
            }

            var query = from warehouse in warehouseIQ
                join master in masterDefiInfoIQ on warehouse.DepartmentID equals master.AttrCd into masters
                from master in masters.DefaultIfEmpty()
                join user in userInfoIQ on warehouse.WarehousePersonID equals user.UId into warehousePersons
                from warehousePerson in warehousePersons.DefaultIfEmpty()
                join user in userInfoIQ on warehouse.CheckPersonID equals user.UId into checkPersons
                from checkPerson in checkPersons.DefaultIfEmpty()
                join user in userInfoIQ on warehouse.DispatherID equals user.UId into dispathers
                from dispather in dispathers.DefaultIfEmpty()
                select new VM_ProductWarehouseShow()
                {
                    ProductWarehouseID = warehouse.ProductWarehouseID,
                    DepartmentName = master!=null?master.AttrValue:null,
                    WarehouseDT = warehouse.WarehouseDT,
                    WarehousePersonName = warehousePerson!=null? warehousePerson.RealName:null,//userInfoIQ.Where(u=>u.UId==warehouse.WarehousePersonID).FirstOrDefault().UName,
                    CheckPersonName = checkPerson!=null? checkPerson.RealName:null,
                    DispatherPersonName = dispather!=null? dispather.RealName:null,
                    BatherID = warehouse.BatchID,
                    WarehouseState = warehouse.WarehouseState
                };
            paging.total = query.Count();
            return query.ToPageList("ProductWarehouseID", paging);
        }


        public bool AddOrUpdateWarehouse(ProductWarehouse warehouse)
        {
            if (warehouse.ProductWarehouseID != null)
            {
                //base.UpdateNotNullColumn(warehouse);
                base.Update(warehouse, new[] {/* "BatchID", "WarehouseState",*/ "DepartmentID", "WarehouseDT", "WarehousePersonID", "CheckPersonID", "DispatherID", "Remark", "UpdUsrID", "UpdDt" });
            }
            else
            {
                base.Add(warehouse);
            }
            return true;
        }

        /// <summary>
        /// 成品库入库登录修改成品交仓单表交仓状态 陈健
        /// </summary>
        /// <param name="productWarehouse">成品交仓单号</param>
        /// <returns></returns>
        public bool updateInProductWarehouse(ProductWarehouse productWarehouse)
        {
            return base.ExecuteStoreCommand("update PD_PROD_WAREH set WAREH_STA={0},UPD_USR_ID={1},UPD_DT={2} where PROD_WAREH_ID={3}", productWarehouse.WarehouseState,productWarehouse.UpdUsrID,productWarehouse.UpdDt, productWarehouse.ProductWarehouseID);
        }


        public bool DeleteWarehouse(string warehouseIds, string userId)
        {
            bool result1 = false;
            bool result2 = false;
            DateTime now = DateTime.Now;
            string delFlag = Constant.GLOBAL_DELFLAG_OFF;
            string updateWarehouseSql = "UPDATE PD_PROD_WAREH SET DEL_FLG ={0} ,DEL_USR_ID={1} ,DEL_DT={2} WHERE EFFE_FLG={3} AND DEL_FLG={4} AND PROD_WAREH_ID IN (";
            string updateWarehouseDetailSql = "UPDATE PD_PROD_WAREH_DETAIL SET DEL_FLG={0} ,DEL_USR_ID={1} ,DEL_DT={2} WHERE EFFE_FLG={3} AND DEL_FLG={4} AND PROD_WAREH_ID IN (";
            string[] ids = warehouseIds.Split(',');
            foreach (string id in ids)
            {
                updateWarehouseSql += id + ",";
                updateWarehouseDetailSql += id + ",";
            }
            updateWarehouseSql= updateWarehouseSql.Substring(0, updateWarehouseSql.Length-1);
            updateWarehouseSql += ")";
            updateWarehouseDetailSql = updateWarehouseDetailSql.Substring(0, updateWarehouseDetailSql.Length - 1);
            updateWarehouseDetailSql += ")";
            result1=base.ExecuteStoreCommand(updateWarehouseSql, delFlag, userId, now,Constant.GLOBAL_EffEFLAG_ON,Constant.GLOBAL_DELFLAG_ON);
            result2=base.ExecuteStoreCommand(updateWarehouseDetailSql, delFlag, userId, now,Constant.GLOBAL_EffEFLAG_ON,Constant.GLOBAL_DELFLAG_ON);
            if (!(result1 && result2))
            {
                throw new Exception("记录在操作前已被删除！");
            }
            return true;
            //return true;
        }

        /// <summary>
        /// 成品库入库手动登录检索成品交仓单号是否存在 陈健
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>数据集合</returns>
        public IQueryable<ProductWarehouse> GetFinInRecordPdtWhID(string productWarehouseID)
        {

            return base.GetList().Where(h => h.ProductWarehouseID.Equals(productWarehouseID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 成品库入库手动登录检索批次号是否存在 陈健
        /// </summary>
        /// <param name="batchI">批次号</param>
        /// <returns>数据集合</returns>
        public IQueryable<ProductWarehouse> GetFinInRecordBtchID(string batchID)
        {

            return base.GetList().Where(h => h.BatchID.Equals(batchID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 成品库入库详细登录根据交仓单号查询 陈健
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <returns>数据集合</returns>
        VM_StoreFinInRecordForDetailShow IProductWarehouseRepository.GetFinInRecordInfosById(string productWarehouseID)
        {

            IQueryable<ProductWarehouse> pdtWHList = base.GetList().Where(h => h.ProductWarehouseID.Equals(productWarehouseID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));

            IQueryable<VM_StoreFinInRecordForDetailShow> query = from o in pdtWHList

                                                                 select new VM_StoreFinInRecordForDetailShow
                                                                {
                                                                    ProductWarehouseID = o.ProductWarehouseID,  //成品交仓单号
                                                                    BatchID = o.BatchID,  //批次号
                                                                    WareHouseID = "03"+o.DepartmentID,  //仓库编号
                                                                    InMoveCls = "00",  //入库移动区分
                                                                    InDate = DateTime.Today,  //入库日期
                                                                    MRemarks = o.Remark  //备注

                                                                };

            IEnumerable<VM_StoreFinInRecordForDetailShow> result = query.AsEnumerable();
            return result.First();

        }

        /// <summary>
        /// 暂时无用
        /// </summary>
        /// <param name="warehouseList"></param>
        /// <param name="warehouseDetailList"></param>
        /// <returns></returns>
        public bool DeleteWarehouse(List<ProductWarehouse> warehouseList, List<ProductWarehouseDetail> warehouseDetailList)
        {
            for (int i = 0, len = warehouseList.Count(); i < len; i++)
            {
                base.UpdateNotNullColumn(warehouseList[i]);
                //base.Update(warehouseList[i], new[] { "DelFlag", "DelUsrID", "DelDt" });
            }
            for (int i = 0, len = warehouseDetailList.Count(); i < len; i++)
            {
                base.UpdateNotNullColumn(warehouseDetailList[i]);
                //base.Update(warehouseDetailList[i], new[] { "DelFlag", "DelUsrID", "DelDt" });
            }
            return true;
        }
        
        
        #region 成品待交仓一览画面
        /// <summary>
        /// 成品待交仓一览画面读取数据(查询函数)  潘军
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public IEnumerable GetPWaitingWarehouseListBySearchByPage(VM_PWaitingWarehouseListForSearch search, Paging paging)
        {
            //Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();
            //成品交仓单详细表 根据条件自动过滤结果
            IQueryable<ProductWarehouseDetail> ProWarehouseDetail = base.GetAvailableList<ProductWarehouseDetail>().FilterBySearch(search);          
            //总装调度单表
            IQueryable<AssemblyDispatch> Assembly = base.GetAvailableList<AssemblyDispatch>().FilterBySearch(search);
            //成品信息表
            IQueryable<ProdInfo> PInfo = base.GetAvailableList<ProdInfo>().FilterBySearch(search);
            //物料分解信息表
            IQueryable<MaterialDecompose> Material = base.GetAvailableList<MaterialDecompose>().FilterBySearch(search);
            //单位表
            IQueryable<UnitInfo> UnitInfo = base.GetAvailableList<UnitInfo>().FilterBySearch(search);
           
            IQueryable<VM_PWaitingWarehouseListForTableShow> query = from As in Assembly//总装调度单表                                                                     
                                                                     join PI in PInfo on As.ProductID equals PI.ProductId into gpi//'成品信息表'和‘总装调度单表’关联
                                                                     from pInfo in gpi.DefaultIfEmpty()
                                                                     join UN in UnitInfo on new { b = pInfo.UnitId } equals new { b = UN.UnitId }//'成品信息表'和'单位表'
                                                                     join MA in Material on new { a = As.CustomerOrderNum, b = As.CustomerOrderDetails, c = As.ProductID } equals new { a = MA.ClientOrderID, b = MA.ClientOrderDetail, c = MA.ProductsPartsID }//'物料分解信息表'和'总装调度单表'关联
                                                                     join ProW in ProWarehouseDetail on As.AssemblyDispatchID equals ProW.AssemblyDispatchID into warehouseDetails//‘总装调度单表’和‘成品交仓单详细表’链接的两个相等字段（‘总装调度单号’）
                                                                     let wQty=As.ActualAssemblyNum - warehouseDetails.Select(wh=>wh.QualifiedQuantity).DefaultIfEmpty(0).Sum()//可交仓数量
                                                                     where wQty > 0
                                                                     
                                                                     
                                                                     select new VM_PWaitingWarehouseListForTableShow
                                                                     {
                                                                        AssemblyOrderNo = As.AssemblyDispatchID,//总装调度单号
                                                                        CustomerOrderID = As.CustomerOrderNum,//客户订单号
                                                                        ProductType =(pInfo!=null? pInfo.ProdAbbrev:""),//产品型号
                                                                        WarehouseQuantityAvailable =wQty,//可交仓数量
                                                                        Specification=MA.Specifica,//材料及规格要求
                                                                        Unit=UN.UnitName,//单位名称
                                                                        Packing = As.WareHouseBoxCer,//每箱数量
                                                                        //Cartons=As.WareHouseBoxQuantity,//箱数
                                                                        Cartons = As.WareHouseBoxCer == 0 ? 0 : (((int)(wQty / As.WareHouseBoxCer) == (wQty / As.WareHouseBoxCer)) ? (int)(wQty / As.WareHouseBoxCer) : (int)(wQty / As.WareHouseBoxCer) + 1), //箱数
                                                                        //Odd=As.WareHouseOdd,//零头
                                                                        Odd = As.WareHouseBoxCer == 0 ? 0:wQty % As.WareHouseBoxCer,//零头
                                                                        ClientOrderDetail=As.CustomerOrderDetails,//客户订单明细
                                                                        TeamIDs = As.Team,//班组                                                                       
                                                                        //DepartmentID = pInfo.DepartId//生产部门
                                                                        DepartmentID = (master.Where(m => m.AttrCd == pInfo.DepartId && m.SectionCd == "00009")).FirstOrDefault().AttrValue,//生产部门名称 关联Master表 
                                                                     };
            paging.total = query.Count();
            IEnumerable<VM_PWaitingWarehouseListForTableShow> result = query.ToPageList<VM_PWaitingWarehouseListForTableShow>("AssemblyOrderNo asc", paging);
            return result;
            //return query.ToList();

        }
        
        #endregion
    }
}
