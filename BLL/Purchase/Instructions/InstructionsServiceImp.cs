/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：InstructionsServiceImp.cs
// 文件功能描述：
//          外购、外协计划一览画面的Service接口的实现类
//      
// 修改履历：2013/12/06 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 外购、外协计划一览画面的Service接口的实现类
    /// </summary>
    public class InstructionsServiceImp : AbstractService, IInstructionsService
    {
        // 外购指示的Repository类
        private IPurchaseInstructionRepository purInstructRepos;
        // 外协指示的Repository类
        private ISupplierInstructionRepository suppInstructRepos;

        /// <summary>
        /// 外购、外协计划一览画面的Service接口的实现类的构造方法
        /// </summary>
        /// <param name="purchaseRepository"></param>
        /// <param name="supplierRepository"></param>
        public InstructionsServiceImp(IPurchaseInstructionRepository purInstructRepos, ISupplierInstructionRepository suppInstructRepos)
        {
            this.purInstructRepos = purInstructRepos;
            this.suppInstructRepos = suppInstructRepos;
        }

        /// <summary>
        /// 获取外购指示List的方法
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外购指示List</returns>
        public IEnumerable<VM_PurchaseInstructionList4Table> GetPurchaseInstructionList(VM_PurchaseInstructionList4Search searchItem, Paging paging)
        {
            return purInstructRepos.GetPurchaseInstructionListWithPaging(searchItem, paging);
        }

        /// <summary>
        /// 获取外协指示List的方法
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外协指示List</returns>
        public IEnumerable<VM_SupplierInstructionList4Table> GetSupplierInstructionList(VM_SupplierInstructionList4Search searchItem, Paging paging)
        {
            return suppInstructRepos.GetSupplierInstructionListWithPaging(searchItem, paging);
        }
    }
}
