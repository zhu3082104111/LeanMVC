/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：purchaseOrderRepositoryImp.cs
// 文件功能描述：外购产品计划单画面的Repository实现
//      
// 修改履历：2013/11/1 刘云 新建
/*****************************************************************************/
using Model;
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
    /// 外购产品计划单画面的Repository实现
    /// </summary>
    public class MCOutSourceOrderRepositoryImp : AbstractRepository<DB, MCOutSourceOrder>, IMCOutSourceOrderRepository
    {
        /// <summary>
        /// 对外购单的删除处理（外购单表的处理）
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="delUID">删除人UserID</param>
        /// <param name="delDate">删除时间</param>
        /// <returns>删除处理结果</returns>
        public bool Delete(string outOrderID, string delUID, DateTime delDate)
        {
            MCOutSourceOrder order = new MCOutSourceOrder();
            // 外购单号
            order.OutOrderID = outOrderID;
            // 删除flg
            order.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
            // 修改人
            order.UpdUsrID = delUID;
            // 修改时间
            order.UpdDt = delDate;
            // 删除人
            order.DelUsrID = delUID;
            // 删除时间
            order.DelDt = delDate;

            // 返回处理结果
            return base.Update(order);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="printUID">打印人UserID</param>
        /// <returns></returns>
        public bool Print(string outOrderID, string printUID)
        {
            MCOutSourceOrder order = new MCOutSourceOrder();
            // 外购单号
            order.OutOrderID = outOrderID;
            // 打印flg
            order.PrintFlag = Constant.GLOBAL_PRINTLAG_ON;
            // 修改人
            order.UpdUsrID = printUID;
            // 修改时间
            order.UpdDt = System.DateTime.Now;

            // 返回处理结果
            return base.Update(order);
        }

        /// <summary>
        /// 对外购单的审核处理
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="verifyUID">审核人UserID</param>
        /// <param name="verifyDate">审核时间</param>
        /// <returns>审核处理结果</returns>
        public bool Audit(string outOrderID, string verifyUID, DateTime verifyDate)
        {
            MCOutSourceOrder order = new MCOutSourceOrder();
            // 外购单号
            order.OutOrderID = outOrderID;
            // 外购单状态
            order.OutOrderStatus = Constant.OutOrderStatus.STATUSO;
            // 审核人
            order.VerifyUID = verifyUID;
            // 审核时间
            order.VerifyDate = verifyDate;
            // 修改人
            order.UpdUsrID = verifyUID;
            // 修改时间
            order.UpdDt = verifyDate;

            // 返回处理结果
            return base.Update(order);
        }

        /// <summary>
        /// 对外购单的批准处理
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="approveUID">批准人UserID</param>
        /// <param name="approveDate">批准时间</param>
        /// <returns>批准处理结果</returns>
        public bool Approve(string outOrderID, string approveUID, DateTime approveDate)
        {
            MCOutSourceOrder order = new MCOutSourceOrder();
            // 外购单号
            order.OutOrderID = outOrderID;
            // 外购单状态
            order.OutOrderStatus = Constant.OutOrderStatus.STATUST;
            // 批准人
            order.ApproveUID = approveUID;
            // 批准时间
            order.ApproveDate = approveDate;
            // 修改人
            order.UpdUsrID = approveUID;
            // 修改时间
            order.UpdDt = approveDate;

            // 返回处理结果
            return base.Update(order);
        }


        /// <summary>
        /// 根据外购单号取得外购单详细信息
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>外购单详细信息</returns>
        public IEnumerable GetPurchaseOrderInfoByID(string outOrderID)
        {
            IQueryable<MCOutSourceOrderDetail> detail = base.GetList<MCOutSourceOrderDetail>();
            //得到POutsourceingPlan表中的所有数据
            IQueryable<MCOutSourceOrder> order = base.GetList();
            //Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();
            //UserInfo表数据
            IQueryable<UserInfo> user = base.GetList<UserInfo>();
            //承接单位
            IQueryable<CompInfo> comp = base.GetList<CompInfo>();
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
            //对外购单号的判断
            if (!String.IsNullOrEmpty(outOrderID))
            {
                detail = detail.Where(d => d.OutOrderID == outOrderID);
            }
            var purchaseOrderDetailObject = from d in detail
                                            group d by new
                                            {
                                                d.OutOrderID,//外购单号
                                                d.ProductPartID,//产品零件
                                                d.MaterialsSpecReq,//规格要求
                                                d.DeliveryDate//交货日期
                                            } into pdo
                                            select new
                                            {
                                                pdo.Key,
                                                //若同一外购单对不同的客户订单外购的产品零件是同一的时候，同时规格要求和交货日期完全相同是，取得是单据要求数量的总和
                                                Quantity = pdo.Sum(de => de.RequestQuantity),
                                                orderIDs=pdo.Select(de=>de.CustomerOrderID),
                                                orderDetails=pdo.Select(de=>de.CustomerOrderDetailID)
                                            };
            
            //用以上对象中的外购单号识别group by后的外购单详细表            
            var query = from p in purchaseOrderDetailObject
                        join d in detail on new { a = p.Key.OutOrderID, b = p.Key.ProductPartID, c = p.Key.MaterialsSpecReq, e = p.Key.DeliveryDate } equals new { a = d.OutOrderID, b = d.ProductPartID, c = d.MaterialsSpecReq, e = d.DeliveryDate } into details
                        select new
                        {
                            PlanNo = details.Select(d => d.PlanNo).DefaultIfEmpty().FirstOrDefault(),
                            ProPartID = (prodAndPartsList.Where(pList => pList.id == p.Key.ProductPartID)).FirstOrDefault().abbrev,
                            PartName = (prodAndPartsList.Where(pList => pList.id == p.Key.ProductPartID)).FirstOrDefault().name,
                            RequestQuantity = p.Quantity,
                            MaterialsSpecReq = p.Key.MaterialsSpecReq,
                            UnitPrice = details.Select(d => d.UnitPrice).DefaultIfEmpty().FirstOrDefault(),
                            Evaluate = details.Select(d => d.Evaluate).DefaultIfEmpty().FirstOrDefault(),
                            DeliveryDate = p.Key.DeliveryDate,
                            VersionNum = details.Select(d => d.VersionNum).DefaultIfEmpty().FirstOrDefault(),
                            Remarks = details.Select(d => d.Remarks).DefaultIfEmpty().FirstOrDefault(),
                            CustomerOrder = p.orderIDs,
                            CustomerOrderDetail = p.orderDetails
                        };
         
            return query;
        }

        /// <summary>
        /// 得到外购单表中符合条件的数据
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        public MCOutSourceOrder getOrderById(string outOrderID)
        {
            MCOutSourceOrder order = base.GetEntityById(outOrderID);
            //Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();
            order.DepartmentID = (master.Where(m => m.AttrCd == order.DepartmentID && m.SectionCd == "00009")).FirstOrDefault().AttrValue;//生产部门名称
            order.UrgentStatus = (master.Where(m => m.AttrCd == order.UrgentStatus && m.SectionCd == "00001")).FirstOrDefault().AttrValue;//紧急状态 关联Master表
            order.OutOrderStatus = (master.Where(m => m.AttrCd == order.OutOrderStatus && m.SectionCd == "00002")).FirstOrDefault().AttrValue;//当前状态
            //承接单位
            IQueryable<CompInfo> comp = base.GetList<CompInfo>();
            order.OutCompanyID = (comp.Where(c => c.CompId == order.OutCompanyID)).FirstOrDefault().CompName;//承接单位
            //UserInfo表数据
            IQueryable<UserInfo> user = base.GetList<UserInfo>();
            if (!String.IsNullOrEmpty(order.ApproveUID))
            {
                order.ApproveUID = (user.Where(u => u.UId == order.ApproveUID)).FirstOrDefault().RealName;
            }
            if (!String.IsNullOrEmpty(order.VerifyUID))
            {
                order.VerifyUID = (user.Where(u => u.UId == order.VerifyUID)).FirstOrDefault().RealName;
            }
            if (!String.IsNullOrEmpty(order.EstablishUID))
            {
                order.EstablishUID = (user.Where(u => u.UId == order.EstablishUID)).FirstOrDefault().RealName;
            }
            if (!String.IsNullOrEmpty(order.SignUID))
            {
                order.SignUID = (user.Where(u => u.UId == order.SignUID)).FirstOrDefault().RealName;
            }
            return order;
        }

        /// <summary>
        /// 更新可以修改的数据
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        public bool UpdateOrder(MCOutSourceOrder purchaseOrder, string uId)
        {
            string outOrderID = purchaseOrder.OutOrderID;//外购单号
            string remarks = purchaseOrder.Remarks;
            string faxNo = purchaseOrder.FaxNo;
            //得到当前时间
            DateTime time = System.DateTime.Now;
            if (!String.IsNullOrEmpty(purchaseOrder.SignUID))
            {
                IQueryable<UserInfo> user = base.GetList<UserInfo>();
                string sUID = user.Where(u => u.RealName == purchaseOrder.SignUID).FirstOrDefault().UId;
                return base.ExecuteStoreCommand("UPDATE MC_OUTSOURCE_ORDER SET RMRS={0},FAX_NO={1},UPD_USR_ID={2},UPD_DT={3},OUT_ODR_STS='3',RECR_STF={4},RECR_DATE={5} WHERE OUT_ODR_ID={6}", remarks, faxNo, uId, time, sUID, time, outOrderID);
            }
            else
            {
                return base.ExecuteStoreCommand("UPDATE MC_OUTSOURCE_ORDER SET RMRS={0},FAX_NO={1},UPD_USR_ID={2},UPD_DT={3} WHERE OUT_ODR_ID={4}", remarks, faxNo, uId, time, outOrderID);
            }
        }



        #region IMCOutSourceOrderRepository 成员


        public MCOutSourceOrder GetMCOutSourceOrderById(string outOrderID)
        {
            return base.GetEntityById(outOrderID);
        }

        #endregion
    }
}
