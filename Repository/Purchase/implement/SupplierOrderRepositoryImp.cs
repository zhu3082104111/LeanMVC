/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierOrderRepositoryImp.cs
// 文件功能描述：
//           外协调度单的Repository的实现
//      
// 创建标识：2013/10/31 廖齐玉 新建
//
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Extensions;

namespace Repository.Purchase.implement
{
    /// <summary>
    ///  外协调度单的Repository的实现
    /// </summary>
    public class SupplierOrderRepositoryImp : AbstractRepository<DB, MCSupplierOrder>, ISupplierOrderRepository 
    {
        /// <summary>
        /// 获取外协调度单一览画面数据方法
        /// </summary>
        /// <param name="searchCondition">获取条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>调度单List</returns>
        public IEnumerable<VM_SupplierOrderList> GetSupplierOrderListForSearch(VM_SupplierOrderListForSearch searchCondition, Extensions.Paging page)
        {
            // 数据表数据的获取

            // 调度单表
            IQueryable<MCSupplierOrder> supplierOrder = base.GetAvailableList<MCSupplierOrder>().FilterBySearch(searchCondition);
            // 调度单明细表
            IQueryable<MCSupplierOrderDetail> supplierOrderDetail = base.GetAvailableList<MCSupplierOrderDetail>();
            //供应商表
            IQueryable<CompInfo> companyName = base.GetAvailableList<CompInfo>();
            //员工信息表
            IQueryable<UserInfo> userInfo = base.GetAvailableList<UserInfo>();
            IQueryable<UserInfo> userInfoMark = userInfo;
            IQueryable<UserInfo> userInfoProdu = userInfo;
            
            //部门表
            IQueryable<MasterDefiInfo> department = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.DEPT));
            //Code表-种类
            IQueryable<MasterDefiInfo> masterOrder = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.SUP_ODR_TYPE));
            //Code表-紧急  
            IQueryable<MasterDefiInfo> masterUrgent = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.URGENT_STATE));
            //Code表-状态
            IQueryable<MasterDefiInfo> masterStatus = base.GetAvailableList<MasterDefiInfo>().Where(a => a.SectionCd.Equals(Constant.MasterSection.SUP_ODR_STATUS));

            //bool isPaging = true;//按主键查询时(单条记录)，不分页

            // ----------------------- 查询条件判断 --------------------------
            #region 查询条件判断

            //客户订单号
            if (!String.IsNullOrEmpty(searchCondition.CustomerOrderID))
            {
                #region 测试方法1
                //string customerOrderNo = "";
                //string customerOrderDetailNo = "";

                //var customerList = searchCondition.CustomerOrderID.Split('-');
                ////输入样式 1. 9 2. 9- 3. -9 4. -
                ////1. length:1 detail:null
                ////2. length:2 detail:""
                ////3. length:2 order:""
                ////4. length:2 all:""

                //customerOrderNo = customerList[0];
                //if (customerList.Length != 1)
                //{
                //    customerOrderDetailNo = customerList[1];
                //    if (customerOrderNo == "")
                //    {
                //        supplierOrderDetail = supplierOrderDetail.Where(t =>  t.CustomerOrderDetailID.Contains(customerOrderDetailNo));
                //    }
                //    else if (customerOrderDetailNo == "")
                //    {
                //        supplierOrderDetail = supplierOrderDetail.Where(t => t.CustomerOrderID.Contains(customerOrderNo));
                //    }
                //    else
                //    {
                //        supplierOrderDetail = supplierOrderDetail.Where(t => t.CustomerOrderID.Contains(customerOrderNo) && t.CustomerOrderDetailID.Contains(customerOrderDetailNo));
                //    }
                //}
                //else 
                //{
                //    supplierOrderDetail = supplierOrderDetail.Where(t => (t.CustomerOrderID + t.CustomerOrderDetailID).Contains(searchCondition.CustomerOrderID));
                //}
                #endregion

                #region 测试方法2
                //// 检索条件的客户订单号的长度
                //int custOrderLength = searchCondition.CustomerOrderID.Length;
                //if (custOrderLength >= 2)
                //{
                //    // 客户订单号
                //    string customerOrderNo = searchCondition.CustomerOrderID.Substring(0, custOrderLength - 2);
                //    // 客户订单详细号
                //    string customerOrderDetailNo = searchCondition.CustomerOrderID.Substring(custOrderLength - 2);
                //    // 明细表
                //    supplierOrderDetail = supplierOrderDetail.Where(t => t.CustomerOrderID.Contains(customerOrderNo) || t.CustomerOrderDetailID.Contains(customerOrderDetailNo));
                //}
                //else
                //{
                //    // 明细表
                //    supplierOrderDetail = supplierOrderDetail.Where(t => t.CustomerOrderID.Contains(searchCondition.CustomerOrderID) || t.CustomerOrderDetailID.Contains(searchCondition.CustomerOrderID));
                //} 
                #endregion

                //搜索基本样式 0-0 或者 0- 或者 -0
                supplierOrderDetail = supplierOrderDetail.Where(sd => (sd.CustomerOrderID + sd.CustomerOrderDetailID).Contains(searchCondition.CustomerOrderID));
                supplierOrder = from s in supplierOrder
                                join sd in supplierOrderDetail on s.SupOrderID equals sd.SupOrderID
                                select s;
            }

            //调度种类
            if (!String.IsNullOrEmpty(searchCondition.OrderType))
            {
                supplierOrder = supplierOrder.Where(s => s.OrderType == searchCondition.OrderType);
            }

            //调度单号
            if (!String.IsNullOrEmpty(searchCondition.SupOrderID))
            {
                supplierOrder = supplierOrder.Where(s => s.SupOrderID.Contains(searchCondition.SupOrderID));
            }

            //紧急状态
            if (!String.IsNullOrEmpty(searchCondition.UrgentStatus))
            {
                supplierOrder = supplierOrder.Where(s => s.UrgentStatus == searchCondition.UrgentStatus);
            }

            //当前状态
            if (!String.IsNullOrEmpty(searchCondition.SupOrderStatus))
            {
                supplierOrder = supplierOrder.Where(s => s.SupOrderStatus == searchCondition.SupOrderStatus);
            }

            //制单人
            if (!String.IsNullOrEmpty(searchCondition.MarkName))
            {
                userInfoMark = userInfo.Where(u => u.RealName.Contains(searchCondition.MarkName));
                supplierOrder =from s in supplierOrder
                               join u in userInfoMark on s.MarkUID equals u.UId
                               select s;
            }

            //调入单位
            if (!String.IsNullOrEmpty(searchCondition.InCompanyName))
            {
                companyName = companyName.Where(c => c.CompName.Contains(searchCondition.InCompanyName));
                supplierOrder = from s in supplierOrder
                                join cn in companyName on s.InCompanyID equals cn.CompId
                                select s;
            }

            //生产主管
            if (!String.IsNullOrEmpty(searchCondition.PrdMngrName))
            {
                userInfoProdu = userInfo.Where(u => u.RealName.Contains(searchCondition.PrdMngrName));
                supplierOrder = from s in supplierOrder
                                join u in userInfoProdu on s.PrdMngrUID equals u.UId
                                select s;
            }

            //生产部门
            if (!String.IsNullOrEmpty(searchCondition.DepartmentName))
            {
                department = department.Where(d => d.AttrCd == searchCondition.DepartmentName);
            }
   
            #endregion
            //--------------------------------------------------------------------------

            //------------------------------------界面显示------------------------------

            var query = (from o in supplierOrder
                         //join ol in supplierOrderDetail on o.SupOrderID equals ol.SupOrderID//取出符合客户订单号的调度单

                         //左连接，查询避免数据相关人员未填（生产主管，制单人或是经办人）而显示不出数据
                         //join u0 in userInfo on o.PrdMngrUID equals u0.UId into pname//取出生产主管的Name
                         //from u0 in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null 
                         //join u1 in userInfo on o.OptrUID equals u1.UId into oname //取出经办人的Name
                         //from u1 in oname.DefaultIfEmpty()
                         //join u2 in userInfo on o.MarkUID equals u2.UId into mname//取出制单人的Name
                         //from u2 in mname.DefaultIfEmpty()

                         join d in department on o.DepartmentID equals d.AttrCd //取出部门
                         //join c in companyName on o.InCompanyID equals c.CompId  //取出调入单位
                         join mo in masterOrder on o.OrderType equals mo.AttrCd //调度单种类
                         join mu in masterUrgent on o.UrgentStatus equals mu.AttrCd//紧急状态
                         join ms in masterStatus on o.SupOrderStatus equals ms.AttrCd// 调度单状态

                         select new VM_SupplierOrderList
                         {
                             SupOrderID = o.SupOrderID,
                             OrderType = mo.AttrValue,
                             UrgentStatus = mu.AttrValue,
                             SupOrderStatusCd = o.SupOrderStatus,
                             SupOrderStatus = ms.AttrValue,
                             DepartmentName = d.AttrValue,
                             InCompanyName = (companyName.Where(c=>c.CompId==o.InCompanyID)).FirstOrDefault().CompName,
                             MarkName = (userInfoMark.Where(u => u.UId == o.MarkUID)).FirstOrDefault().RealName,
                             PrdMngrName = (userInfoProdu.Where(u => u.UId == o.PrdMngrUID)).FirstOrDefault().RealName,
                             OptrName = (userInfo.Where(u => u.UId == o.OptrUID)).FirstOrDefault().RealName,
                             MarkDate = o.MarkSignDate,
                         }).Distinct();

            page.total = query.Count();
            IEnumerable<VM_SupplierOrderList> result = query.ToPageList<VM_SupplierOrderList>("SupOrderID asc", page);
            return result;
            
        }

 
        /// <summary>
        /// 删除,修改外协调度单表的Delete标志
        /// </summary>
        /// <param name="s">外协调度单实体</param>
        /// <returns>执行结果</returns>
        [TransactionAop]
        public bool DeleteSupplierOrder(MCSupplierOrder s)
        {
            return base.Update(s);
            //return base.ExecuteStoreCommand("update MC_SUPPLIER_ORDER set DEL_FLG={0},DEL_USR_ID={1},DEL_DT={2} where SUP_ODR_ID={3} ", s.DelFlag, s.DelUsrID, DateTime.Now, s.SupOrderID);
        }

        /// <summary>
        /// 审核,外协单的审核
        /// </summary>
        /// <param name="s">外协调度单实体</param>
        /// <returns>执行结果</returns>
        public bool AuditSupplierOrder(MCSupplierOrder s)
        {
            return base.Update(s);
        }

        #region  根据外协加工调度单号得到外协加工调度单表对应数据
        /// <summary>
        /// 根据外协加工调度单号得到外协加工调度单表对应数据
        /// </summary>
        /// <param name="supOrderID">外协加工调度单号</param>
        /// <returns></returns>
        public MCSupplierOrder GetMCSupplierOrderById(string supOrderID)
        {
            return base.GetEntityById(supOrderID);
        }
        #endregion

    }
}
