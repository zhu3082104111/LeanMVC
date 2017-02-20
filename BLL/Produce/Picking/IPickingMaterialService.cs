// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IPickingMaterialService.cs
// 文件功能描述：生产领料单信息service接口
// 
// 创建标识：代东泽 20131127
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
using Model.Produce;
using Model;
using Extensions;
using System.Collections;
using Repository;
using Model.Store;
namespace BLL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPickingMaterialService
    {
        IEnumerable<ProduceMaterRequest> GetMaterials(VM_ProduceMaterRequestForSearch search,Paging paging, out int total);

        /// <summary>
        /// 根据检索框条件获取所有满足条件领料单信息
        /// </summary>
        /// <param name="pickingList">搜索表单</param>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        IEnumerable<VM_ProduceMaterRequestForTableShow> GetMaterialsForSearch(VM_ProduceMaterRequestForSearch pickingList, Extensions.Paging paging);


        bool CreatePickingList(ProduceMaterRequest pickingList);


        bool DeletePickingList(ProduceMaterRequest pickingList);


        /// <summary>
        /// 代东泽 20131225
        /// </summary>
        /// <param name="card"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        bool GetPickingBillDataByTranslateCard(ProcessTranslateCard card, IList<Model.Store.VM_Reserve> list);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        bool GetPickingBillDataByAssemBigBill(AssemBill bill,IList<VM_Reserve> list);

        /// <summary>
        /// 代东泽 20140102
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsCanCreatePickingBill(string id,string type);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmq"></param>
        /// <param name="pmdList"></param>
        /// <param name="reserveList"></param>
        /// <param name="reserveDetailList"></param>
        [TransactionAop]
        void SavePickingMaterial(ProduceMaterRequest pmq, IList<ProduceMaterDetail> pmdList, IList<Reserve> reserveList, IList<ReserveDetail> reserveDetailList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        VM_ProduceMaterRequestForTableShow GetProduceMaterRequest(ProduceMaterRequest p);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        IEnumerable<VM_ProduceMaterDetailForDetailShow> GetProduceMaterDetailsForEdit(ProduceMaterRequest p);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="pmq"></param>
         /// <param name="pmdList"></param>
         /// <param name="reserveList"></param>
         /// <param name="reserveDetailList"></param>
        void UpdatePickingList(ProduceMaterRequest pmq, IList<ProduceMaterDetail> pmdList, IList<Reserve> reserveList, IList<ReserveDetail> reserveDetailList);
    }


  
}
