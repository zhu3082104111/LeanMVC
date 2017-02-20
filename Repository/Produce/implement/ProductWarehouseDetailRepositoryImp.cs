/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouseDetailRepositoryImp.cs
// 文件功能描述：成品交仓详细资源库实现类
// 
// 创建标识：20131125 杜兴军
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
using Model;
using Repository.database;
using Extensions;

namespace Repository.Produce.implement
{
    public class ProductWarehouseDetailRepositoryImp:AbstractRepository<DB,ProductWarehouseDetail>,IProductWarehouseDetailRepository
    {

        public VM_ProductWarehouseDetailHeadData GetDetailData(string productWarehouseID)
        {
            IQueryable<ProductWarehouseDetail> warehouseDetailIQ = GetAvailableList<ProductWarehouseDetail>();//成品交仓详细表
            IQueryable<ProductWarehouse> warehouseIQ = GetAvailableList<ProductWarehouse>();//成品交仓表
            IQueryable<UserInfo> userIQ = GetList<UserInfo>();//员工信息
            IQueryable<MasterDefiInfo> masterDefiInfoIQ = GetAvailableList<MasterDefiInfo>();//master信息
            IQueryable<TeamInfo> teamInfoIQ = GetAvailableList<TeamInfo>();//班组信息表
            IQueryable<UnitInfo> unitInfoIQ = GetAvailableList<UnitInfo>();//单位表
            IQueryable<ProdInfo> productInfoIQ = GetAvailableList<ProdInfo>();//成品信息表
 
            warehouseDetailIQ = warehouseDetailIQ.Where(wd => wd.ProductWarehouseID.Equals(productWarehouseID));//获取某一交仓单号的所有详细数据
            userIQ = userIQ.Where(u => u.Enabled=="1");//获取有效的用户信息
            masterDefiInfoIQ = masterDefiInfoIQ.Where(m => m.SectionCd.Equals(Constant.MasterSection.DEPT));//筛选部门(生产单元)所有记录
            
            var query = from warehouse in warehouseIQ
                join warehouseDetail in warehouseDetailIQ on warehouse.ProductWarehouseID equals
                    warehouseDetail.ProductWarehouseID into warehouseDetails //详细表
                join master in masterDefiInfoIQ on warehouse.DepartmentID equals master.AttrCd into masters//master表
                from master in masters.DefaultIfEmpty()
                join user in userIQ on warehouse.WarehousePersonID equals user.UId into warehousePersons//员工表
                from warehousePerson in warehousePersons.DefaultIfEmpty()
                join user in userIQ on warehouse.CheckPersonID equals user.UId into checkPersons//员工表
                from checkPerson in checkPersons.DefaultIfEmpty()
                join user in userIQ on warehouse.DispatherID equals user.UId into dispathers//员工表
                from dispather in dispathers.DefaultIfEmpty()
                where warehouse.ProductWarehouseID == productWarehouseID
                select new VM_ProductWarehouseDetailHeadData()
                {
                    ProductWarehouseID = warehouse.ProductWarehouseID,
                    DepartmentID = master!=null?master.AttrCd:null,
                    DepartmentName = master!=null?master.AttrValue:null,
                    BatchID = warehouse.BatchID,
                    WarehouseDT = warehouse.WarehouseDT,
                    Children = from warehouseDetail in warehouseDetails
                                   join team in teamInfoIQ on warehouseDetail.TeamID equals team.TeamId into teams
                                   join prod in productInfoIQ on warehouseDetail.OrderProductID equals prod.ProductId into prodInfos
                                   from prod in prodInfos.DefaultIfEmpty()
                                   join unitInfo in unitInfoIQ on prod.UnitId equals unitInfo.UnitId into unitInfos
                                   select new VM_ProductWarehouseDetailDetailBodyData()
                                   {
                                      WarehouseLineNO = warehouseDetail.WarehouseLineNO,
                                      ClientOrderID = warehouseDetail.ClientOrderID,
                                      TeamID = warehouseDetail.TeamID,
                                      TeamName = teams.Select(tm=>tm.TeamName).DefaultIfEmpty("").FirstOrDefault(),
                                      OrderProductID = warehouseDetail.OrderProductID,
                                      ProductName = prod!=null?prod.ProdAbbrev:"",
                                      ProductSpecification = warehouseDetail.ProductSpecification,
                                      Unit = unitInfos.Select(unit=>unit.UnitName).DefaultIfEmpty("").FirstOrDefault(),
                                      QualifiedQuantity = warehouseDetail.QualifiedQuantity,
                                      EachBoxQuantity = warehouseDetail.EachBoxQuantity,
                                      BoxQuantity = warehouseDetail.BoxQuantity,
                                      RemianQuantity = warehouseDetail.RemianQuantity,
                                      Remark = warehouseDetail.Remark
                                   },
                    WarehousePersonID = warehousePerson!=null?warehousePerson.UId:null,
                    WarehousePersonName = warehousePerson!=null?warehousePerson.RealName:null,
                    CheckPersonID = checkPerson!=null?checkPerson.UId:null,
                    CheckPersonName = checkPerson!=null?checkPerson.RealName:null,
                    DispatherID = dispather != null ? dispather.UId : null,
                    DispatherName = dispather != null ? dispather.RealName : null,
                    WarehouseState = warehouse.WarehouseState
                };

            VM_ProductWarehouseDetailHeadData data = query.AsEnumerable().FirstOrDefault();
            if (data != null && data.WarehouseDT.Equals(Constant.CONST_FIELD.DB_INIT_DATETIME))
            {
                data.WarehouseDT = null;
            }
            return data;
        }


        public bool AddOrUpdate(ProductWarehouseDetail warehouseDetail)
        {
            if (warehouseDetail.WarehouseLineNO.StartsWith("NULL-")) //没有行号,新增
            {
                warehouseDetail.WarehouseLineNO = warehouseDetail.WarehouseLineNO.Split('-')[1];
                base.Add(warehouseDetail);
            }
            else
            {
                //base.Update(warehouseDetail, new[] { "ClientOrderID", "ClientOrderDetail", "TeamID", "OrderProductID", "ProductSpecification", "QualifiedQuantity", "EachBoxQuantity", "BoxQuantity", "RemianQuantity", "AssemblyDispatchID", "ProductCheckID", "Remark", "DelFlag", "UpdUsrID", "UpdDt" });
                base.Update(warehouseDetail, new[] {"QualifiedQuantity", "EachBoxQuantity", "BoxQuantity", "RemianQuantity", "Remark","UpdUsrID", "UpdDt" });
            }
            return true;
        }


        public override bool Delete(ProductWarehouseDetail warehouseDetail)
        {
            //base.ExecuteStoreCommand("UPDATE PD_PROD_WAREH_DETAIL SET DEL_FLG ={0} ,DEL_USR_ID={1},DEL_DT={2} WHERE PROD_WAREH_ID={3} AND WAREH_LINE_NO={4}",
            //    warehouseDetail.DelFlag, warehouseDetail.DelUsrID, warehouseDetail.DelDt, warehouseDetail.ProductWarehouseID, warehouseDetail.WarehouseLineNO);
            bool result=base.UpdateNotNullColumn(warehouseDetail);
            if (!result)
            {
                throw  new Exception("删除失败");
            }
            //base.Update(warehouseDetail, new string[] { "DelFlag", "DelUsrID", "DelDt" });
            return true;
        }


        public int GetMaxLineNo(string productWarehouseId)
        {
            IQueryable<ProductWarehouseDetail> warehouseDetailIQ = GetList<ProductWarehouseDetail>();
            warehouseDetailIQ = warehouseDetailIQ.Where(p => p.ProductWarehouseID == productWarehouseId);
            string maxNoStr = warehouseDetailIQ.Max(p => p.WarehouseLineNO);
            int maxNo = 0;
            try
            {
                maxNo = int.Parse(maxNoStr);
            }
            catch (Exception)
            {
                maxNo = 0;
            }
            return maxNo;
        }

        /// <summary>
        /// 成品库入库手动登录检索计划单号是否存在 陈健
        /// </summary>
        /// <param name="planID">计划单号</param>
        /// <returns>数据集合</returns>
        public IQueryable<ProductWarehouseDetail> GetFinInRecordPlanID(string planID)
        {

            return base.GetList().Where(h => (h.ClientOrderID+"/"+h.ClientOrderDetail).Equals(planID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 成品库入库手动登录检索检验单号是否存在 陈健
        /// </summary>
        /// <param name="productCheckID">检验单号</param>
        /// <returns>数据集合</returns>
        public IQueryable<ProductWarehouseDetail> GetFinInRecordProductCheckID(string productCheckID)
        {

            return base.GetList().Where(h => h.ProductCheckID.Equals(productCheckID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 成品库入库手动登录检索物料编号是否存在 陈健
        /// </summary>
        /// <param name="orderProductID">物料编号</param>
        /// <returns>数据集合</returns>
        public IQueryable<ProductWarehouseDetail> GetFinInRecordOrProductID(string orderProductID)
        {

            return base.GetList().Where(h => h.OrderProductID.Equals(orderProductID) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }

        /// <summary>
        /// 成品库入库手动登录检索成品交仓单号+批次号+计划单号+检验单号+装配小组+物料编号+规格型号是否存在 陈健
        /// </summary>
        /// <param name="productWarehouseID">交仓单号</param>
        /// <param name="planID">计划单号</param>
        /// <param name="productCheckID">检验单号</param>
        /// <param name="teamID">装配小组</param>
        /// <param name="orderProductID">产品ID</param>
        /// <param name="productSpecification">规格型号</param>
        /// <returns>交仓详细数据集合</returns>
        public IQueryable<ProductWarehouseDetail> GetFinInRecordDetail(string productWarehouseID,string planID,string productCheckID,
            string teamID,string orderProductID,string productSpecification)
        {

            return base.GetList().Where(h => h.ProductWarehouseID.Equals(productWarehouseID) && (h.ClientOrderID+"/"+h.ClientOrderDetail).Equals(planID) && 
                h.ProductCheckID.Equals(productCheckID) && h.TeamID.Equals(teamID) && h.OrderProductID.Equals(orderProductID) && 
                h.ProductSpecification.Equals(productSpecification) && h.EffeFlag.Equals("0") && h.DelFlag.Equals("0"));
        }
       
    }
}
