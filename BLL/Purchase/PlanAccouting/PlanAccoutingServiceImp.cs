/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PlanAccoutingServiceImp.cs
// 文件功能描述：
//          外协和外购计划台帐的Service接口的实现类
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;
using Repository.Purchase;

namespace BLL
{
    /// <summary>
    /// 外协和外购计划台帐的Service接口的实现类
    /// </summary>
    public class PlanAccoutingServiceImp : AbstractService, IPlanAccoutingService
    {

        // 采购计划台帐的Repository类
        private IPurchaseSupplierAccoutingListRepository PurchaseSupplierAccoutingListRepository;

        /// <summary>
        /// 外协和外购计划台帐的Service的构造函数
        /// </summary>
        /// <param name="SupplierAccoutingListRepository">采购计划台帐的Repository接口对象</param> 
        public PlanAccoutingServiceImp(IPurchaseSupplierAccoutingListRepository PurchaseSupplierAccoutingListRepository)
        {
            this.PurchaseSupplierAccoutingListRepository = PurchaseSupplierAccoutingListRepository;
        }

        /// <summary>
        /// 外购计划台帐一览画面显示数据的查询方法
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>外购计划台帐一览信息</returns>
        public IEnumerable GetPurchaseAccoutingListBySearchByPage(VM_PurchaseAccoutingListForSearch searchCondition, Paging paging)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetPurchaseAccoutingListBySearchByPage(searchCondition, paging);
        }

        /// <summary>
        /// 外购计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns>外购计划台帐详细信息</returns>
        public IEnumerable GetPurchaseAccoutingDetailByNo(string OutOrderNo)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetPurchaseAccoutingDetailByNo(OutOrderNo);
        }

        /// <summary>
        /// 外购单的基本信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns>外购单的基本信息</returns>
        public VM_PurchaseAccoutingDetail SelectOrderInfo(string OutOrderNo)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetOrderByNo(OutOrderNo);
        }

        
        /// <summary>
        /// 外协计划台帐一览画面显示数据的查询方法
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>外协计划台帐一览信息</returns>
        public IEnumerable GetSupplierAccoutingListBySearchByPage(VM_SupplierAccoutingList4Search searchCondition, Paging paging)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetSupplierAccoutingListBySearchByPage(searchCondition, paging);
        }

        /// <summary>
        /// 外协计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协计划台帐详细信息</returns>
        public IEnumerable GetSupplierAccoutingDetailByNo(string supOrderNo)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetSupplierAccoutingDetailByNo(supOrderNo);

        }

        /// <summary>
        /// 外协单的基本信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协单的基本信息</returns>
        public VM_SupplierAccoutingDetailInfo SelectSupplierOrderInfo(string supOrderNo)
        {
            // 返回Repository的查询结果
            return PurchaseSupplierAccoutingListRepository.GetSupplierOrderByNo(supOrderNo);
        }
    }
}
