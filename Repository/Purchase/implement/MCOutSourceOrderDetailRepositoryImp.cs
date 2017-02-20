/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：purchaseOrderListRepositoryImp.cs
// 文件功能描述：产品外购单一览画面的Repository实现
//      
// 修改履历：2013/10/28 刘云 新建
/*****************************************************************************/
using Model;
using Extensions;
using Repository.database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 产品外购单一览画面的Repository实现
    /// </summary>
    public class MCOutSourceOrderDetailRepositoryImp : AbstractRepository<DB, MCOutSourceOrderDetail>, IMCOutSourceOrderDetailRepository
    {
        /// <summary>
        /// 取得产品外购单一览画面的显示信息
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns>产品外购单信息List</returns>
        public IEnumerable GetPurchaseOrderListInfoByPage(VM_PurchaseOrderListForSearch searchConditon, Paging paging)
        {
            #region 数据源
            // 得到外购单表数据  根据条件自动过滤结果
            IQueryable<MCOutSourceOrder> order = base.GetList<MCOutSourceOrder>().FilterBySearch(searchConditon);
            
            // 得到外购单详细表数据  根据条件自动过滤结果
            IQueryable<MCOutSourceOrderDetail> detail = base.GetList().FilterBySearch(searchConditon);
            
            // 供货商 根据条件自动过滤结果 CompName     PD_COMP_INFO--CompId=OutCompanyID
            IQueryable<CompInfo> compInfo = base.GetList<CompInfo>().FilterBySearch(searchConditon);
            
            // 用户表
            IQueryable<UserInfo> user = base.GetList<UserInfo>().FilterBySearch(searchConditon);
            
            // Master数据管理表(紧急状态)
            IQueryable<MasterDefiInfo> urgentStatusM = base.GetList<MasterDefiInfo>().Where(m => m.SectionCd == Constant.MasterSection.URGENT_STATE);

            // Master数据管理表(外购单状态)
            IQueryable<MasterDefiInfo> statusM = base.GetList<MasterDefiInfo>().Where(m => m.SectionCd == Constant.MasterSection.ODR_STATE);

            // Master数据管理表(生产单元)
            IQueryable<MasterDefiInfo> departM = base.GetList<MasterDefiInfo>().Where(m => m.SectionCd == Constant.MasterSection.DEPT);
            #endregion

            #region 查询条件过滤
            //对客户订单号的判断  外购单详细表的客户订单号 + "-" +客户订单明细号
            if (!String.IsNullOrEmpty(searchConditon.CustomerOrder))
            {
                // 查询条件.客户订单号
                detail = detail.Where(d => (d.CustomerOrderID + Constant.SPLICE_MARK + d.CustomerOrderDetailID).Contains(searchConditon.CustomerOrder.Trim()));

                // 外购单表与外购单详细表的关联
                order = (from o in order
                         from d in detail
                         where d.OutOrderID.Contains(o.OutOrderID)
                         select o
                        ).Distinct();
            }
            #endregion

            // 抽取有效数据
            IQueryable<VM_PurchaseOrderListForTableShow> query = from o in order
                                                                 // 关联紧急状态Master表
                                                                 join urgent in urgentStatusM on o.UrgentStatus equals urgent.AttrCd
                                                                 // 关联生产单元Master表
                                                                 join d in departM on o.DepartmentID equals d.AttrCd
                                                                 // 关联当前状态Master表
                                                                 join sts in statusM on o.OutOrderStatus equals sts.AttrCd
                                                                 // 关联供货商信息表
                                                                 join comp in compInfo on o.OutCompanyID equals comp.CompId
                                                                 // 关联用户表
                                                                 join u in user on o.EstablishUID equals u.UId
                                                                 // 取得结果
                                                                 select new VM_PurchaseOrderListForTableShow
                                                                 {
                                                                     OutOrderID = o.OutOrderID,         // 外购单号
                                                                     UrgentStatus = urgent.AttrValue,   // 紧急状态名称
                                                                     DeptName = d.AttrValue,            // 生产部门名称
                                                                     CompName = comp.CompName,          // 供货商名称
                                                                     OutOrderStatus = sts.AttrValue,    // 当前状态名称
                                                                     OrderStatus = o.OutOrderStatus,    // 当前状态Code
                                                                     EstablishUName = u.RealName,       // 编制人的名字
                                                                     EstablishDate = o.EstablishDate,   // 编制日期
                                                                     ApproveDate = o.ApproveDate,       // 批准日期
                                                                     Remarks = o.Remarks                // 备注
                                                                 };
            // 统计结果数量  
            paging.total = query.Count();

            // 将结果返回给视图Model，默认按外购单号升序
            IEnumerable<VM_PurchaseOrderListForTableShow> result = query.ToPageList<VM_PurchaseOrderListForTableShow>("OutOrderID asc", paging);
            
            // 返回查询结果
            return result;
        }

        /// <summary>
        /// 对外购单的删除处理（外购单详细表的处理）
        /// </summary>
        /// <param name="detail">外购详细实体</param>
        /// <param name="delUID">删除人UserID</param>
        /// <param name="delDate">删除时间</param>
        /// <returns>删除处理结果</returns>
        public bool Delete(MCOutSourceOrderDetail detail, string delUID, DateTime delDate)
        {
            // 删除flg
            detail.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
            // 修改人
            detail.UpdUsrID = delUID;
            // 修改时间
            detail.UpdDt = delDate;
            // 删除人
            detail.DelUsrID = delUID;
            // 删除时间
            detail.DelDt = delDate;

            // 返回处理结果
            return base.Update(detail);
        }

        /// <summary>
        /// 更新可以修改的数据
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public bool UpdateDetail(MCOutSourceOrderDetail purchaseOrder,string uId)
        {
            //成品信息表
            IQueryable<ProdInfo> prodInfo = base.GetList<ProdInfo>();
            //零件信息表
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>();
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodInfo
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partInfo
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            string outOrderID = purchaseOrder.OutOrderID;//外购单号
            string customerOrderID = purchaseOrder.CustomerOrderID;
            string customerOrderDetailID = purchaseOrder.CustomerOrderDetailID;
            string productPartID = prodAndPartsList.Where(p => p.abbrev == purchaseOrder.ProductPartID).FirstOrDefault().id;
            decimal unitPrice = purchaseOrder.UnitPrice;//单价
            decimal evaluate = purchaseOrder.Evaluate;
            DateTime deliveryDate = purchaseOrder.DeliveryDate;//交货日期
            string planNo = purchaseOrder.PlanNo;
            string versionNum = purchaseOrder.VersionNum;//版本号
            string remarks = purchaseOrder.Remarks;//备注
            DateTime time = System.DateTime.Now;
            return base.ExecuteStoreCommand("UPDATE MC_OUTSOURCE_ORDER_DTL SET PRCHS_UP={0},EVALUATE={1},DLY_DATE={2},VER={3},RMRS={4},PLAN_NO={5},UPD_USR_ID={6},UPD_DT={7} WHERE OUT_ODR_ID={8} AND CLN_ODR_ID={9} AND CLN_ODR_DTL_ID={10} AND PROD_PART_ID={11}", unitPrice, evaluate, deliveryDate, versionNum, remarks, planNo, uId, time, outOrderID,customerOrderID,customerOrderDetailID, productPartID);
        }

        

        #region IMCOutSourceOrderDetailRepository 成员


        public IEnumerable<MCOutSourceOrderDetail> GetMCOutSourceOrderDetailForListAsc(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            return base.GetList().Where(a => a.EffeFlag == "0" && a.DelFlag == "0" && a.OutOrderID == mcOutSourceOrderDetail.OutOrderID && a.ProductPartID == mcOutSourceOrderDetail.ProductPartID).OrderBy(n => n.CreDt);
        }


        public IEnumerable<MCOutSourceOrderDetail> GetMCOutSourceOrderDetailForListDesc(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            return base.GetList().Where(a => a.EffeFlag == "0" && a.DelFlag == "0" && a.OutOrderID == mcOutSourceOrderDetail.OutOrderID && a.ProductPartID == mcOutSourceOrderDetail.ProductPartID).OrderByDescending(n => n.CreDt);
        }
        #endregion

        #region IMCOutSourceOrderDetailRepository 成员


        public bool UpdateMCOutSourceOrderDetailActColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            return base.ExecuteStoreCommand("update MC_OUTSOURCE_ORDER_DTL set ACT_QTY=ACT_QTY+{0} where OUT_ODR_ID={1},CLN_ODR_ID={2} and CLN_ODR_DTL_ID={3} and PROD_PART_ID={4} ", mcOutSourceOrderDetail.ActualQuantity, mcOutSourceOrderDetail.OutOrderID, mcOutSourceOrderDetail.CustomerOrderID, mcOutSourceOrderDetail.CustomerOrderDetailID);
        }

        #endregion

        #region IMCOutSourceOrderDetailRepository 成员


        public bool UpdateMCOutSourceOrderDetailForDelActColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            return base.ExecuteStoreCommand("update MC_OUTSOURCE_ORDER_DTL set ACT_QTY=ACT_QTY - {0},UPD_USR_ID={1},UPD_DT={2} where OUT_ODR_ID={3},CLN_ODR_ID={4} and CLN_ODR_DTL_ID={5} and PROD_PART_ID={6} ", mcOutSourceOrderDetail.ActualQuantity, mcOutSourceOrderDetail.UpdUsrID, DateTime.Now, mcOutSourceOrderDetail.OutOrderID, mcOutSourceOrderDetail.CustomerOrderID, mcOutSourceOrderDetail.CustomerOrderDetailID, mcOutSourceOrderDetail.ProductPartID);
        }

        #endregion

        #region IMCOutSourceOrderDetailRepository 成员


        public MCOutSourceOrderDetail SelectMCOutSourceOrderDetail(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            return base.First(a => a.OutOrderID == mcOutSourceOrderDetail.OutOrderID && a.CustomerOrderID == mcOutSourceOrderDetail.CustomerOrderID && a.CustomerOrderDetailID == mcOutSourceOrderDetail.CustomerOrderDetailID && a.ProductPartID == mcOutSourceOrderDetail.ProductPartID && a.EffeFlag=="0" &&a.DelFlag=="0");
        }

        #endregion

        #region IMCOutSourceOrderDetailRepository 成员


        public bool UpdateMCOutSourceOrderDetailColumns(MCOutSourceOrderDetail mcOutSourceOrderDetail)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
