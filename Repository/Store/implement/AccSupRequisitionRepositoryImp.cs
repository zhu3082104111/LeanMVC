/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccSupRequisitionRepositoryImp.cs
// 文件功能描述：
//          附件库外协领料单画面的Repository实现类
//      
// 创建标识：2013/12/31 吴飚 新建
/*****************************************************************************/
using Extensions;
using Model;
using Repository.database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccSupRequisitionRepositoryImp : AbstractRepository<DB, MCSupplierCnsmInfo>, IAccSupRequisitionRepository
    {
        #region 附件库外协领料单查询

        /// <summary>
        /// 通过查询条件显示附件库外协领料单查询结果
        /// </summary>
        /// <param name="searchCondition">查询条件（外协单号）</param>
        /// <param name="paging">分页参数</param>
        /// <param name="requiNum">生成的领料单号</param>
        /// <returns></returns>
        public IEnumerable GetAccSupRequisitionBySearchByPage(String SupOrderID, Paging paging)
        {
            #region 外协领料单表
            IQueryable<MCSupplierCnsmInfo> supplierCnsmInfo = base.GetAvailableList<MCSupplierCnsmInfo>().FilterBySearch(SupOrderID);

            #endregion

            #region 外协加工调度单表,详细表
            //外协加工调度单表
            IQueryable<MCSupplierOrder> supplierOrderInfo = base.GetAvailableList<MCSupplierOrder>().FilterBySearch(SupOrderID);
            
            //外协加工调度单详细表
            IQueryable<MCSupplierOrderDetail> supplierOrderDetailInfo = base.GetAvailableList<MCSupplierOrderDetail>().FilterBySearch(SupOrderID);
            
            //用外协加工调度单详细表做主表分类(产品零件ID，供货商ID)                  
            var supplierOrderList = from s in supplierOrderInfo
                                    join sd in supplierOrderDetailInfo
                                    on s.SupOrderID equals sd.SupOrderID
                                    group s by new { sd.ProductPartID,s.InCompanyID,s.SupOrderID } into ss
                                    select new 
                                    {
                                        ss.Key                                                 
                                    };
                                  
       

            #endregion

            #region 产品零件表
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>().FilterBySearch(SupOrderID);

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>().FilterBySearch(SupOrderID);

            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            #endregion 

            #region 成品构成信息表
            //成品构成信息表
            IQueryable<ProdComposition> pdProdComposition = base.GetAvailableList<ProdComposition>().FilterBySearch(SupOrderID);
            //关联外协加工调度单表的零件产品ID生成新表
            var prodCom = from pdc in pdProdComposition
                          join s in supplierOrderList on pdc.ParItemId equals s.Key.ProductPartID
                          join p in partList on pdc.SubItemId equals p.PartId
                          select new
                          {
                             SubItemId = p.PartId, //子物料ID(子)
                             ParItemId = s.Key.ProductPartID, //零件产品ID(父)
                             ConstQuantity= pdc.ConstQty, //领的料所需子物料数量(子)
                             //RequestQuantity= s.RequestQuantity, //所需领料数量(父)
                             ProductName= p.PartName //零件名称(子)
                          };
            #endregion

            #region 供货商信息表
            // 供货商信息表
            IQueryable<CompInfo> companyList = base.GetAvailableList<CompInfo>().FilterBySearch(SupOrderID);
            #endregion

            #region 仓库预约表
            //仓库预约表
            IQueryable<Reserve> Reserve = base.GetAvailableList<Reserve>().FilterBySearch(SupOrderID);
            //仓库预约详细表
            IQueryable<ReserveDetail> ReserveDetail = base.GetAvailableList<ReserveDetail>().FilterBySearch(SupOrderID);
            //让步仓库预约表
            IQueryable<GiReserve> GiReserve = base.GetAvailableList<GiReserve>().FilterBySearch(SupOrderID);

            
            #endregion

            #region 对查询条件的判断显示出领料单查询结果

                    //根据外协调度单号查询结果，传进的外协单号和生成的外协单表进行匹配
                supplierOrderList = supplierOrderList.Where(s => s.Key.SupOrderID == SupOrderID);

                    //按附件库外协领料单视图类创建查询结果
                    IQueryable<VM_AccSupplierRequisitionForTableShow> query = from so in supplierOrderList
                                                                              join s in supplierOrderDetailInfo on so.Key.SupOrderID equals s.SupOrderID
                                                                              into s1
                                                                              from s in s1.DefaultIfEmpty()
                                                                              join pp in prodAndPartsList on so.Key.ProductPartID equals pp.id
                                                                              into pp1
                                                                              from pp in pp1.DefaultIfEmpty()
                                                                              join com in companyList on so.Key.InCompanyID equals com.CompId
                                                                              into com1
                                                                              from com in com1.DefaultIfEmpty()
                                                                              join pc in prodCom on so.Key.ProductPartID equals pc.ParItemId
                                                                              into pc1
                                                                              from pc in pc1.DefaultIfEmpty()
                                                                              join r in Reserve on s.ProductPartID equals r.PdtID
                                                                              into r1
                                                                              from r in r1.DefaultIfEmpty()
                                                                              select new VM_AccSupplierRequisitionForTableShow
                                                                              {
                                                                                                                   //领料单号
                                                                                  ApplyComName = com.CompName,                                //请领单位
                                                                                  ProdPartName = pp.name,                                     //产品零件名称
                                                                                  MaterielName = pc.ProductName,                              //加工原料名称
                                                                                  RequireAcount = s.RequestQuantity * pc.ConstQuantity,     //需求总量
                                                                                  StorageAcount = r.RecvQty - r.PickOrdeQty,                    //库存可用数量       
                                                                                  PartID = so.Key.ProductPartID
                                                                              };

                    paging.total = query.Count();

                    IEnumerable<VM_AccSupplierRequisitionForTableShow> queryResult = query.ToPageList<VM_AccSupplierRequisitionForTableShow>("RequOrderID asc", paging);

                    return queryResult;      
                              
            #endregion

        }
        #endregion

        #region 插入新的数据到外协领料单信息表
        public IEnumerable InsertInSuppCnsmInfo(String requiNum, String SupOrderID)
        {
            //需要被插入数据的对象获取（领料单实体）
            IQueryable<MCSupplierCnsmInfo> supplierCnsmInfo = base.GetAvailableList<MCSupplierCnsmInfo>().FilterBySearch(SupOrderID);

            base.ExecuteStoreCommand("INSERT INTO MC_SUPPLIER_CNSM_INFO(MATER_REQ_NO,SUP_ODR_ID)VALUES({0},{1})", requiNum, SupOrderID);

            IQueryable<VM_AccSupplierRequisitionForTableShow> AfterInsertList = from s in supplierCnsmInfo
                                                                          select new VM_AccSupplierRequisitionForTableShow 
                                                                          {
                                                                          };
            return AfterInsertList;
        }
        #endregion

        #region 更新外协领料单信息表
        public IEnumerable UpdateInSuppCnsmInfo(String SupOrderID, Paging paging) 
        {
            //需要被更新的数据库里表的对象的获取(用传入的外协单号关联)
            //领料单信息表
            IQueryable<MCSupplierCnsmInfo> supplierCnsmInfo = base.GetAvailableList<MCSupplierCnsmInfo>().FilterBySearch(SupOrderID);
            //用传入的外协单号取得相对应的领料单表里的信息
            supplierCnsmInfo = supplierCnsmInfo.Where(s=>s.SupOrderID==SupOrderID);
            return null;
            //仓库预约表
            //BOM表
            //外协加工调度单表
            //成品构成信息表

            //用取到的数据去更新领料单表的数据·
        }
        #endregion
    }
}
