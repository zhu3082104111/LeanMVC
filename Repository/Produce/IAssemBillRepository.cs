// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：WorkticketServiceImp.cs
// 文件功能描述：总装工票数据操作接口
// 
// 创建标识：代东泽 20131126
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
using Model.Produce;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 代东泽 20131126
    /// </summary>
    public interface IAssemBillRepository:IRepository<AssemBill>
    {
        /// <summary>
        /// 查找所有总装大工票
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemBigBillForTableShow> GetAssemBigBillsWithPaging(VM_AssemBigBillForSearch bill,Paging page);

        /// <summary>
        /// 查找总装大工票信息
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        VM_AssemBigBillForDetailShow GetAssemBigBill(AssemBill bill);

        /// <summary>
        /// 查询总装大工票详细信息
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemBigBillPartForDetailShow> GetAssemBigBillDetails(AssemBill bill);

        /// <summary>
        /// 查询大工票对应的所有客户订单
        /// </summary>
        /// <param name="assemBill"></param>
        /// <returns></returns>
        IEnumerable<VM_AssemblyDispatch> GetCustomOrdersByAssemBigBill(AssemBill assemBill);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAssemBillDetail(AssemBillDetail entity);

        bool AddAssemBillDetails();

 

        /// <summary>
        /// 添加总装工票详细
        /// 朱静波 20131203
        /// </summary>
        /// <update>代东泽 20131206</update>
        /// <param name="entity">总装工票实体</param>
        /// <returns>执行结果</returns>
        Boolean AddAssemBigBillDetail(AssemBillDetail entity);

        /// <summary>
        /// 根据总装工票ID取得总装工票详细
        /// 朱静波 20131203
        /// </summary>
        /// <param name="AssemblyTicketID">总装工票ID</param>
        /// <returns>总装工票集</returns>
        IEnumerable<AssemBillDetail> GetAssemBillDetail(String assemblyTicketID);

        /// <summary>
        /// 删除总装工票
        /// 朱静波 20131203
        /// </summary>
        /// <param name="id">总装工票号</param>
        /// <param name="userId">用户ID</param>
        /// <returns>执行结果</returns>
        Boolean DeleteAssemBill(String id, String userId);

        /// <summary>
        /// 删除总装工票详细
        /// 朱静波 20131203
        /// </summary>
        /// <param name="id">总装工票详细号</param>
        /// <param name="userId">用户ID</param>
        /// <returns>执行结果</returns>
        Boolean DeleteAssemBillDetail(String id, String userId);

  
    }
}
