/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierOrderDetailRepositoryImp.cs
// 文件功能描述：
//           外协调度单明细表的Repository的实现
//      
// 创建标识：2013/11/22 廖齐玉 新建
//
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Repository;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 外协调度单明细表的Repository接口类的实现
    /// </summary>
    public class SupplierOrderDetailRepositoryImp:AbstractRepository<DB,MCSupplierOrderDetail>,ISupplierOrderDetailRepository
    {
        /// <summary>
        /// 获取明细表信息
        /// </summary>
        /// <param name="supplierOrderId">调度单号</param>
        /// <param name="page">分页排序</param>
        /// <returns></returns>
        public IEnumerable<VM_SupplierOrder> GetSupplierOrderDetailByIdForSearch(string supplierOrderId,Paging page)
        {
            #region 产品名称
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();

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

            #region 加工工艺
            //加工工艺
            IQueryable<Process> process = base.GetAvailableList<Process>(); 
            #endregion

            #region 明细表数据
            //明细表
            IQueryable<MCSupplierOrderDetail> supplierOrderDetail = base.GetAvailableList<MCSupplierOrderDetail>().Where(a => a.SupOrderID == supplierOrderId);
            // 合并外协单详细表的相同物料求总数（按照产品零件ID、规格型号及要求、加工工艺和交货日期 group by）
            var supplierOrderDetailObject = from s in supplierOrderDetail
                                            group s by new
                                            {
                                                s.SupOrderID,         //调度单号
                                                s.ProductPartID,      //零件ID  
                                                s.MaterialsSpecReq,   //规格型号及要求
                                                s.PdProcID,           //加工工艺
                                                s.DeliveryDate,       //交货日期
                                                s.UnitPrice,          //单价
                                                s.Evaluate,           //估价
                                                s.Remarks             //备注
                                            } into sdo
                                            select new
                                            {
                                                sdo.Key,
                                                //调度单的需对该件产品的总数，（有可能不同的客户对同一件商品的需要）
                                                TotalQuantity = sdo.Sum(de => de.RequestQuantity)
                                            };  
            #endregion

            IQueryable<VM_SupplierOrder> query = from o in supplierOrderDetailObject
                                                 select new VM_SupplierOrder
                                                 {
                                                     // 调度单号
                                                     SupOrderID = o.Key.SupOrderID,
                                                     // 产品零件Id
                                                     ProductPartID =  o.Key.ProductPartID,
                                                     // 产品略称
                                                     ProductAbbrev =(prodAndPartsList.Where(p => p.id == o.Key.ProductPartID)).FirstOrDefault().abbrev,
                                                     // 产品名称
                                                     ProductName = (prodAndPartsList.Where(p=>p.id ==o.Key.ProductPartID )).FirstOrDefault().name,
                                                     // 材料规格及型号
                                                     MaterialsSpecReq = o.Key.MaterialsSpecReq,
                                                     // 加工工艺
                                                     PdProcDtID =(process.Where(p=>p.ProcessId == o.Key.PdProcID)).FirstOrDefault().ProcName,
                                                     // 数量
                                                     ReqQty = o.TotalQuantity,
                                                     // 价格
                                                     PriceUp = o.Key.UnitPrice,
                                                     // 估价
                                                     Evaluate = o.Key.Evaluate,
                                                     // 交货日期
                                                     DlyDate = o.Key.DeliveryDate,
                                                     // 备注
                                                     Remarks = o.Key.Remarks,
                                                 };
            page.total = query.Count();
            IEnumerable<VM_SupplierOrder> result = query.ToPageList<VM_SupplierOrder>("ProductPartID asc", page);
            return result;
           
        }

        /// <summary>
        /// 获取ID相对应的数据
        /// </summary>
        /// <param name="supplierOrderId">调度单号</param>
        /// <returns></returns>
        VM_SupplierOrderInfor ISupplierOrderDetailRepository.GetSupplierOrderDetailInforById(string supplierOrderId)
        {
            // 外协单
            IQueryable<MCSupplierOrder> supplierOrder = base.GetAvailableList<MCSupplierOrder>().Where(s =>s.SupOrderID ==supplierOrderId);
            // 生产部门表
            IQueryable<MasterDefiInfo> department = base.GetAvailableList<MasterDefiInfo>().Where(o => o.SectionCd == Constant.MasterSection.DEPT);
            // 员工信息表
            IQueryable<UserInfo > userInfo =base.GetAvailableList<UserInfo>();
            // 调入单位表
            IQueryable<CompInfo> companyName = base.GetAvailableList<CompInfo>();
            // 调度单状态 Master表
            IQueryable<MasterDefiInfo> orderStatus = base.GetAvailableList<MasterDefiInfo>().Where(o => o.SectionCd == Constant.MasterSection.SUP_ODR_STATUS);
            // 调度单紧急 Master表
            IQueryable<MasterDefiInfo> urgentStatus = base.GetAvailableList<MasterDefiInfo>().Where(o => o.SectionCd == Constant.MasterSection.URGENT_STATE);

            IQueryable<VM_SupplierOrderInfor> query = from o in supplierOrder 
                                                      // 部门信息 
                                                      join d in department on o.DepartmentID equals d.AttrCd
                                                      // 调入单位
                                                      join c in companyName on o.InCompanyID equals c.CompId
                                                      // 制单人
                                                      join um in userInfo on o.MarkUID equals um.UId into markName
                                                      from um in markName.DefaultIfEmpty()
                                                      // 经办人
                                                      join uo in userInfo on o.OptrUID equals uo.UId into opeName
                                                      from uo in opeName.DefaultIfEmpty()
                                                      // 生产主管
                                                      join up in userInfo on o.PrdMngrUID equals up.UId into pName
                                                      from up in pName.DefaultIfEmpty()
                                                      // 当前状态
                                                      join os in orderStatus on o.SupOrderStatus equals os.AttrCd
                                                      // 紧急状态
                                                      join us in urgentStatus on o.UrgentStatus equals us.AttrCd
                                                      select new VM_SupplierOrderInfor
                                                     {
                                                         OrderStatus = os.AttrValue,//调度单当前状态
                                                         UrgentStatus = us.AttrValue,//紧急状态
                                                         Department = d.AttrValue,//部门
                                                         OrderType = o.OrderType,//调度单种类
                                                         IncompId = c.CompName,//调入单位
                                                         PrdMngrName = up.RealName,//生产主管
                                                         PrdMngrSignDate = o.PrdMngrSignDate,//主管审批时间
                                                         MarkName = um.RealName,//制单人
                                                         MarkSignDate = o.MarkSignDate,//制单时间
                                                         OptrName = uo.RealName,//经办人
                                                         OptrSignDate = o.OptrSignDate//经办时间
                                                     };

            IEnumerable<VM_SupplierOrderInfor> result = query.AsEnumerable();
            return result.First();
      
        }

        /// <summary>
        ///  修改保存数据
        /// </summary>
        /// <param name="supplierOrder">对应实体</param>
        /// <returns></returns>
        public bool UpdateSupplierOrderDetail(MCSupplierOrderDetail supplierOrder)
        {
            return base.ExecuteStoreCommand(
                //更新语句
                "UPDATE MC_SUPPLIER_ORDER_DTL SET " +
                " PRCHS_UP={0}, EVALUATE ={1}, DLY_DATE ={2}, RMRS={3} " +
                " WHERE SUP_ODR_ID={4}  AND PROD_PART_ID ={5}",
                //待修改的值
                supplierOrder.UnitPrice, supplierOrder.Evaluate, supplierOrder.DeliveryDate, supplierOrder.Remarks,
                //主键
                supplierOrder.SupOrderID, supplierOrder.ProductPartID);
        }

        /// <summary>
        /// 根据其中之一的主键获取与改主键相等的所有实体集合
        /// </summary>
        /// <param name="supplierOrderId">多主键中的一主键</param>
        /// <returns></returns>
        public IQueryable<MCSupplierOrderDetail> GetSupplierOrderDetailList(string supplierOrderId)
        {
            //throw new NotImplementedException();
            return base.GetList().Where(a => a.SupOrderID == supplierOrderId);
        }

        /// <summary>
        /// 删除调度单明细表里的数据
        /// </summary>
        /// <param name="s">将要删除的实体</param>
        /// <returns></returns>
        public bool DeleteSupplierOrderDetail(MCSupplierOrderDetail s)
        {
            //throw new NotImplementedException();
            return base.ExecuteStoreCommand("update MC_SUPPLIER_ORDER_DTL set DEL_FLG={0},DEL_USR_ID={1},DEL_DT={2},UDP_USR_ID={3},UPT_DT={4} where SUP_ODR_ID={5} ", s.DelFlag, s.DelUsrID, s.DelDt,s.UpdUsrID,s.UpdDt,s.SupOrderID);
        }


        #region ISupplierOrderDetailRepository 成员（yc添加）


        public IEnumerable<MCSupplierOrderDetail> GetMCSupplierOrderDetailForListAsc(MCSupplierOrderDetail mcSupplierOrderDetail)
        {
            return base.GetList().Where(a => a.EffeFlag=="0" && a.DelFlag=="0" && a.SupOrderID == mcSupplierOrderDetail.SupOrderID && a.ProductPartID == mcSupplierOrderDetail.ProductPartID).OrderBy(n => n.CreDt);
        }

        public IEnumerable<MCSupplierOrderDetail> GetMCSupplierOrderDetailForListDesc(MCSupplierOrderDetail mcSupplierOrderDetail)
        {
            return base.GetList().Where(a => a.EffeFlag == "0" && a.DelFlag == "0" && a.SupOrderID == mcSupplierOrderDetail.SupOrderID && a.ProductPartID == mcSupplierOrderDetail.ProductPartID).OrderByDescending(n => n.CreDt);
        }

        public bool UpdateMCSupplierOrderDetailActColumns(MCSupplierOrderDetail mcSupplierOrderDetail)
        {
            return base.ExecuteStoreCommand("update MC_SUPPLIER_ORDER_DTL set ACT_QTY=ACT_QTY+{0} where SUP_ODR_ID={1},CLN_ODR_ID={2} and CLN_ODR_DTL_ID={3} and PROD_PART_ID={4} ", mcSupplierOrderDetail.ActualQuantity, mcSupplierOrderDetail.SupOrderID, mcSupplierOrderDetail.CustomerOrderID, mcSupplierOrderDetail.CustomerOrderDetailID);
        }

        public bool UpdateMCSupplierOrderDetailForDelActColumns(MCSupplierOrderDetail mcSupplierOrderDetail)
        {
            return base.ExecuteStoreCommand("update MC_SUPPLIER_ORDER_DTL set ACT_QTY=ACT_QTY-{0} where SUP_ODR_ID={1},CLN_ODR_ID={2} and CLN_ODR_DTL_ID={3} and PROD_PART_ID={4} ", mcSupplierOrderDetail.ActualQuantity, mcSupplierOrderDetail.SupOrderID, mcSupplierOrderDetail.CustomerOrderID, mcSupplierOrderDetail.CustomerOrderDetailID);
        }

        #endregion
    }
}
