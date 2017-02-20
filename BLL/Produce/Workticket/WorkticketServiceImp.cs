// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：WorkticketServiceImp.cs
// 文件功能描述：工票service实现类
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
using Repository;
using BLL.ServerMessage;
using Model.Produce;
using Model;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 代东泽 20131126
    /// </summary>
    public class WorkticketServiceImp:AbstractService,IWorkticketService
    {
        private IAssemblyDispatchRepository assemDispatchRepository;

        private IWorkticketRepository workticketRepository;

        private IAssemBillRepository assemBillRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="assemDispatchRepository"></param>
        /// <param name="workticketRepository"></param>
        /// <param name="assemBillRepository"></param>
        public WorkticketServiceImp(IAssemblyDispatchRepository assemDispatchRepository,IWorkticketRepository workticketRepository, IAssemBillRepository assemBillRepository)
        {
            this.assemDispatchRepository = assemDispatchRepository;
            this.workticketRepository = workticketRepository;
            this.assemBillRepository = assemBillRepository;
        }

        #region 总装大工票

        public IEnumerable<Model.Produce.VM_AssemBigBillForTableShow> GetAssemBigBillsForSearch(Model.Produce.VM_AssemBigBillForSearch bill, Extensions.Paging paging)
        {
            return assemBillRepository.GetAssemBigBillsWithPaging(bill, paging); 
        }

        public Model.Produce.VM_AssemBigBillForDetailShow GetAssemBigBillInfo(AssemBill workticket)
        {
            return assemBillRepository.GetAssemBigBill(workticket);
        }

        public IEnumerable<Model.Produce.VM_AssemBigBillPartForDetailShow> GetAssemBigBillsDetailInfo(AssemBill workticket)
        {
            return assemBillRepository.GetAssemBigBillDetails(workticket);
        }

        void IWorkticketService.SaveAssemBigBill(AssemBill bill, IEnumerable<AssemBillDetail> bds, IEnumerable<AssemblyDispatch> ads, IList<string> flags)
        {
            assemBillRepository.UpdateNotNullColumn(bill);
            int k = 0;
            foreach(var a in bds) 
            {

                if (CommonConstant.OLD.Equals(flags.ElementAt(k)))//update
                {
                    assemBillRepository.UpdateAssemBillDetail(a);
                }
                else //add
                {
                    assemBillRepository.AddAssemBigBillDetail(a);
                }
                k++;
               
            }
            foreach (var a in ads) 
            {
                assemDispatchRepository.UpdateNotNullColumn(a);
            }
            //return assemBillRepository.AddAssemBillDetails();
           //throw new NotImplementedException(SM_Produce.SaveWorkticketFailed);
        }

        public IEnumerable<VM_AssemblyDispatch> GetCustomOrdersForAssemBigBill(AssemBill assemBill)
        {
            return assemBillRepository.GetCustomOrdersByAssemBigBill(assemBill);
        }

        #endregion
    }
}
