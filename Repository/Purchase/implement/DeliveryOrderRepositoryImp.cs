/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：DeliveryOrderRepositoryImp.cs
// 文件功能描述：
//            送货单表的Repository接口的实现类
//      
// 修改履历：2013/11/13 姬思楠 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository.database;

namespace Repository
{
    /// <summary>
    /// 送货单表的Repository接口的实现类
    /// </summary>
    public class DeliveryOrderRepositoryImp : AbstractRepository<DB, MCDeliveryOrder>, IDeliveryOrderRepository
    {
        /// <summary>
        /// 取得送货单一览信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>送货单信息List</returns>
        public IEnumerable GetDeliveryOrderListBySearchByPage(VM_DeliveryOrderListForSearch searchCondition, Paging paging)
        {
            // 送货单表  根据条件自动过滤结果
            IQueryable<MCDeliveryOrder> deliveryOrderListInfo = base.GetAvailableList<MCDeliveryOrder>().FilterBySearch(searchCondition);

            // 送货单区分（Code定义 section_cd = "00008"）
            IQueryable<MasterDefiInfo> deliveryOrderType = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.DEL_ODR_TYPE));

            //供货商名称  根据条件自动过滤结果
            IQueryable<CompInfo> deliveryCompanyName = base.GetAvailableList<CompInfo>().FilterBySearch(searchCondition);

            //送货单状态（Code定义 section_cd = "00038"）
            IQueryable<MasterDefiInfo> deliveryOrderStatus = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.DEL_ODR_STATE));

            // 审核人信息表  根据条件自动过滤结果
            IQueryable<UserInfo> verifyUserInfo = base.GetAvailableList<UserInfo>().FilterBySearch(searchCondition);

            // 检查员信息表  根据条件自动过滤结果
            IQueryable<UserInfo> ispcUserInfo = base.GetAvailableList<UserInfo>().FilterBySearch(searchCondition);

            // 核价员信息表  根据条件自动过滤结果
            IQueryable<UserInfo> prccUserInfo = base.GetAvailableList<UserInfo>().FilterBySearch(searchCondition);

            // 仓管员信息表  根据条件自动过滤结果
            IQueryable<UserInfo> whkpUserInfo = base.GetAvailableList<UserInfo>().FilterBySearch(searchCondition);

            // 查询
            IQueryable<VM_DeliveryOrderListForTableShow> query = from d in deliveryOrderListInfo
                                                                 // 关联送货单种类情报
                                                                 join t in deliveryOrderType on d.DeliveryOrderType equals t.AttrCd
                                                                 // 关联送货单位情报
                                                                 join c in deliveryCompanyName on d.DeliveryCompanyID equals c.CompId
                                                                 // 关联送货单状态情报
                                                                 join g in deliveryOrderStatus on d.DeliveryOrderStatus equals g.AttrCd
                                                                 // 关联审核人情报
                                                                 join v in verifyUserInfo on d.VerifyUID equals v.UId into vusers
                                                                 from v in vusers.DefaultIfEmpty()
                                                                 // 关联检查员情报
                                                                 join i in ispcUserInfo on d.IspcUID equals i.UId into ispers
                                                                 from i in ispers.DefaultIfEmpty()
                                                                 // 关联核价员情报
                                                                 join p in prccUserInfo on d.PrccUID equals p.UId into prcers
                                                                 from p in prcers.DefaultIfEmpty()
                                                                 // 关联仓管员情报
                                                                 join w in whkpUserInfo on d.WhkpUID equals w.UId into whkers
                                                                 from w in whkers.DefaultIfEmpty()

                                                                 select new VM_DeliveryOrderListForTableShow
                                                                 {
                                                                    DeliveryOrderID = d.DeliveryOrderID,
                                                                    DeliveryOrderType = t.AttrValue,
                                                                    OrderNo = d.OrderNo,
                                                                    DeliveryCompanyID = c.CompName,
                                                                    DeliveryUID = d.DeliveryUID,
                                                                    VerifyUID = v.RealName,
                                                                    IspcUID = i.RealName,
                                                                    PrccUID = p.RealName,
                                                                    WhkpUID = w.RealName,
                                                                    DeliveryDate = d.DeliveryDate,
                                                                    VerifyDate = d.VerifyDate,
                                                                    BatchID=g.AttrValue
                                                                 };
            paging.total = query.Count();
            IEnumerable<VM_DeliveryOrderListForTableShow> result = query.ToPageList<VM_DeliveryOrderListForTableShow>("DeliveryOrderID desc", paging);
            return result;
        }

        /// <summary>
        /// 删除送货单（更新送货单表的信息）
        /// </summary>
        /// <param name="deliveryOrder">送货单实体</param>
        /// <param name="uId">用户</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        public bool Delete(MCDeliveryOrder deliveryOrder, string uId, DateTime systime)
        {
            // 送货单存在时
            if (deliveryOrder != null)
            {
                // 删除Flg
                deliveryOrder.DelFlag = Constant.CONST_FIELD.DELETED;
                // 修改人
                deliveryOrder.UpdUsrID = uId;
                // 修改时间
                deliveryOrder.UpdDt = systime;
                // 删除人
                deliveryOrder.DelUsrID = uId;
                // 删除时间
                deliveryOrder.DelDt = systime;

                // 返回更新结果
                return base.Update(deliveryOrder);
            }

            return false;
        }

        /// <summary>
        /// 审核送货单
        /// </summary>
        /// <param name="deliveryOrder">送货单实体</param>
        /// <param name="uId">用户</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        public bool Audit(MCDeliveryOrder deliveryOrder, string uId, DateTime systime)
        {
            // 送货单存在时
            if (deliveryOrder != null)
            {
                // 送货单状态（1：已审核）
                deliveryOrder.DeliveryOrderStatus = Constant.DeliveryOrderStatus.AUDITED;
                // 审核人
                deliveryOrder.VerifyUID = uId;
                // 审核时间
                deliveryOrder.VerifyDate = systime;
                // 修改人
                deliveryOrder.UpdUsrID = uId;
                // 修改时间
                deliveryOrder.UpdDt = systime;

                // 返回更新结果
                return base.Update(deliveryOrder);
            }

            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <returns></returns>
        public bool UpdateOrder(MCDeliveryOrder deliveryOrder)
        {
            string deliveryOrderID = deliveryOrder.DeliveryOrderID;
            DateTime deliveryDate = deliveryOrder.DeliveryDate;
            IQueryable<CompInfo> comp = base.GetList<CompInfo>();
            string deliveryCompanyID = comp.Where(c => c.CompName == deliveryOrder.DeliveryCompanyID).FirstOrDefault().CompId;//deliveryOrder.DeliveryCompanyID;
            IQueryable<UserInfo> user = base.GetList<UserInfo>();
            string deliveryUID = deliveryOrder.DeliveryUID;
            string telNo = deliveryOrder.TelNo;
            return base.ExecuteStoreCommand("UPDATE MC_DELIVERY_ORDER SET DLY_DATE={0},DLY_COMP_ID={1},DLY_STF={2},TEL={3},UPD_USR_ID={4},UPD_DT={5} WHERE DLY_ODR_ID={6}", deliveryDate, deliveryCompanyID, deliveryUID, telNo, deliveryOrder.UpdUsrID, deliveryOrder.UpdDt, deliveryOrderID);
        }
        
        /// <summary>
        /// 通过送货单号得到对应送货单表数据
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <returns></returns>
        public MCDeliveryOrder getDeliveryOrderById(string deliveryOrderID)
        {
            MCDeliveryOrder order = base.GetEntityById(deliveryOrderID);
            //供货商名称
            IQueryable<CompInfo> comp = base.GetList<CompInfo>();
            order.DeliveryCompanyID = (comp.Where(c => c.CompId == order.DeliveryCompanyID)).FirstOrDefault().CompName;
            order.DeliveryUID = order.DeliveryUID;
            return order;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        public bool printInfo(string deliveryOrderID, string uId)
        {
            DateTime time = System.DateTime.Now;
            return base.ExecuteStoreCommand("UPDATE MC_DELIVERY_ORDER SET PRINT_FLG='1',UPD_USR_ID={0},UPD_DT={1} WHERE DLY_ODR_ID={2}", uId, time, deliveryOrderID);
        }

    }
} 
