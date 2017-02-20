/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AssemblyDispatchRepositoryImp.cs.cs
// 文件功能描述：总装调度单表的Repository接口实现
//     
// 修改履历：2013/11/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository.database;

namespace Repository
{
    class AssemblyDispatchRepositoryImp : AbstractRepository<DB, AssemblyDispatch>, IAssemblyDispatchRepository
    {
        public IEnumerable<VM_AssemblyDispatch> GetAllAssemblyScheduleBill(VM_FAScheduleBillForSearch inProcessingRate, Paging paging)
        {
            IQueryable<AssemblyDispatch> adlist = base.GetAvailableList<AssemblyDispatch>();
            //成品信息表
            IQueryable<ProdInfo> pilist = base.GetAvailableList<ProdInfo>();
            if (inProcessingRate.TxtProductionUnits != null)
            {
                string[] departIdArray = inProcessingRate.TxtProductionUnits.Split(',');
                pilist = pilist.Where(t => departIdArray.Contains(t.DepartId));
            }
            else
            {
                //return masterInfo.GetListByConditionWithOrderBy<string>(a => a.SectionCd.Equals(section), n => n.SNo);
            }
            //状态表
            IQueryable<MasterDefiInfo> masDefInf = base.GetAvailableList<MasterDefiInfo>().Where(t => t.SectionCd.Equals(Constant.FAScheduleBill.CURRENT_STATE));

            //班组
            IQueryable<TeamInfo> teamInfos = base.GetAvailableList<TeamInfo>();

            IQueryable<VM_AssemblyDispatch> newAdlist = from ad in adlist
                                                        join pi in pilist on ad.ProductID equals pi.ProductId
                                                        join mdf in masDefInf on ad.DispatchStatus equals mdf.AttrCd
                                                        join ti in teamInfos on ad.Team equals ti.TeamId into gti
                                                        from teamInfo in gti.DefaultIfEmpty()
                                                        select new VM_AssemblyDispatch
                                                     {
                                                         ActualAssemblyNum = ad.ActualAssemblyNum,
                                                         AssemblyDispatchID = ad.AssemblyDispatchID,
                                                         AssemblyPlanNum = ad.AssemblyPlanNum,
                                                         AssemblyTicketID = ad.AssemblyTicketID,
                                                         CustomerName = ad.CustomerName,
                                                         CustomerOrderDetails = ad.CustomerOrderDetails,
                                                         CustomerOrderNum = ad.CustomerOrderNum,
                                                         PlanDeliveryDate = ad.PlanDeliveryDate,
                                                         ProdAbbrev = pi.ProdAbbrev,
                                                         ProductID = ad.ProductID,
                                                         Remarks = ad.Remarks,
                                                         SchedulingOrderDate = ad.SchedulingOrderDate,
                                                         Team = teamInfo.TeamName,
                                                         WareHouseDate = ad.WareHouseDate,
                                                         DispatchStatus = mdf.AttrValue,
                                                         AttrCd = mdf.AttrCd
                                                     };
            IQueryable<VM_AssemblyDispatch> resultData = newAdlist.FilterBySearch(inProcessingRate);

            paging.total = resultData.Count();
            return resultData.ToPageList("AssemblyDispatchID asc", paging);

        }

        public bool DeleteAssemblyDispatchById(string id, string userId)
        {
            return base.ExecuteStoreCommand("update PD_ASSEM_DISPATCH set DEL_FLG={0},DEL_DT={1},DEL_USR_ID={2} where ASS_DISP_ID={3} ", Constant.GLOBAL_DELFLAG_OFF, DateTime.Now, userId, id);
        }

        public AssemblyDispatch GetAssemblyScheduleBillById(string id)
        {
            IQueryable<AssemblyDispatch> assemblyDispatches =
                base.GetAvailableList<AssemblyDispatch>().Where(t => t.AssemblyDispatchID.Equals(id));
            return assemblyDispatches.FirstOrDefault();
        }

        public bool UpdateAssemblyScheduleBill(AssemblyDispatch entity)
        {
            return base.Update(entity);
        }

        public bool AddAssemblyScheduleBill(AssemblyDispatch entity)
        {
            return base.Add(entity);
        }

        public int GetAssemblyTicketIdQuantity(string assemblyTicketID)
        {
            IQueryable<AssemblyDispatch> assDislist = base.GetAvailableList<AssemblyDispatch>().Where(t => t.AssemblyTicketID.Equals(assemblyTicketID));
            if (assDislist.Any())
            {
                return assDislist.Count();
            }
            return 0;
        }
    }
}
