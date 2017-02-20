/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AssemblyDispatchDetailRepositoryImp.cs
// 文件功能描述：总装调度详细表的Repository接口的实现
//     
// 修改履历：2013/11/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;
using Repository.database;

namespace Repository
{
    class AssemblyDispatchDetailRepositoryImp : AbstractRepository<DB, AssemblyDispatchDetail>, IAssemblyDispatchDetailRepository
    {
        
        public IEnumerable<AssemblyDispatchDetail> GetAssemblyDispatchDetailById(string id)
        {
            throw new NotImplementedException();
        }

        public bool SaveAssemblyDispatchDetail(AssemblyDispatchDetail entity)
        {
            return base.Add(entity);
        }

        public bool DeleteAssemblyDispatchDetailById(string id, string userId)
        {
            return base.ExecuteStoreCommand("update PD_ASSEM_DISPATCH_DETAIL set DEL_FLG={0},DEL_DT={1},DEL_USR_ID='{2}' where ASS_DISP_ID={3} ", Constant.GLOBAL_DELFLAG_OFF, DateTime.Now, userId, id);
        }

        public IEnumerable<VM_NewBillDataGrid> GetEditBillDataGrid(string id)
        {
            IQueryable<AssemblyDispatchDetail> assemblyDispatchDetails = base.GetAvailableList<AssemblyDispatchDetail>().Where(t => t.AssemblyDispatchID.Equals(id));
            IQueryable<VM_NewBillDataGrid> querys = from assdisdet in assemblyDispatchDetails
                                                    select new VM_NewBillDataGrid
                                                    {
                                                        SubItemID = assdisdet.PartID,
                                                        PartName = assdisdet.PartName,
                                                        BatchNumber = assdisdet.BatchNum,
                                                        ConstQty = assdisdet.UnitQuantity,
                                                        Remark = assdisdet.Remark,
                                                        Specifica = assdisdet.Specifica
                                                    };
            return querys.OrderBy(t => t.SubItemID);
        }
    }
}
