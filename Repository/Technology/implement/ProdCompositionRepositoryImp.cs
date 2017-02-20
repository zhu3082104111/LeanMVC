/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdCompositionRepositoryImp.cs
// 文件功能描述：成品信息构成表的Repository接口
//     
// 修改履历：2013/11/22 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;
using Repository.database;
using Util;

namespace Repository
{
    class ProdCompositionRepositoryImp: AbstractRepository<DB, ProdComposition>, IProdCompositionRepository
    {
        #region 私有成员，C：梁龙飞
        /// <summary>
        /// 私有方法，递归查找一个零件的工序树
        /// </summary>
        /// <param name="rootMatID"></param>
        /// <returns></returns>
        private IEnumerable<ProdComposition> MatProcessTree(string rootMatID, int matSequenceNo, int ProcessSequenceNo)
        {
            try
            {
                IQueryable<ProdComposition> matList = base.GetAvailableList<ProdComposition>();
                IEnumerable<ProdComposition> query = (from c in matList
                                                      where c.ParItemId == rootMatID
                                                      select c).ToList();
                foreach (var item in query)
                {
                    item.MatSequenceNo = matSequenceNo;
                    item.ProcessSequenceNo = ProcessSequenceNo + 1;
                }

                //寻找父项目为当前列表子项目的物料，并组合它们
                return query.Concat(query.SelectMany(t => MatProcessTree(t.SubItemId, t.MatSequenceNo, t.ProcessSequenceNo)));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 分解功能集 C:梁龙飞     
        /// <summary>
        /// 取出一个产品的结构
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IEnumerable<ProdComposition> DecomposeProduct(string productID)
        {

            int matSequenceNo = 0;
            try
            {
                //初始化为产品
                IEnumerable<ProdComposition> finalData = base.GetAvailableList<ProdComposition>().Where(a => a.ParItemId == productID && a.SubItemId == productID).ToList();
                //如果传入的ID不是产品根
                if (finalData != null && finalData.Count() != 1)
                {
                    throw new Exception("编号为："+productID+"的产品在技术部门数据中不存在！");
                }

                foreach (var item in finalData)
                {
                    item.MatSequenceNo = ++matSequenceNo;
                    item.ProcessSequenceNo = 1;

                }

                //所有一级零件
                IEnumerable<ProdComposition> comMat = base.GetAvailableList<ProdComposition>().Where(a => a.ParItemId == productID && a.SubItemId != productID).ToList().OrderBy(a => a.SubItemId);

                foreach (var item in comMat)
                {
                    item.MatSequenceNo = ++matSequenceNo;
                    item.ProcessSequenceNo = 1;
                }
                finalData = finalData.Concat(comMat);

                //所有二级零件
                foreach (var item in comMat)
                {
                    finalData = finalData.Concat(MatProcessTree(item.SubItemId, item.MatSequenceNo, item.ProcessSequenceNo));
                }

                finalData = finalData.OrderBy(a => a.MatSequenceNo).ThenBy(a => a.ProcessSequenceNo).ToList();

                //格式化数据，形成父子树目录
                int lastID = 0;//前一个ID
                int lastMatSequenceNo = 0;
                int intFormatID = 0;
                foreach (var item in finalData)
                {
                    //MatSequenceNo与上一个MatSequenceNo不相同
                    if (item.MatSequenceNo != lastMatSequenceNo)
                    {
                        item.ProcessSequenceNo = 0;//一级零件，无祖先
                        lastMatSequenceNo = item.MatSequenceNo;//新的MatSequenceNo保存为lastMatSequenceNo
                        item.MatSequenceNo = ++intFormatID;//格式化一级零件ID为从1开始的递增Int

                        lastID = item.MatSequenceNo;//保存当前零件根（最终工序）
                    }
                    else//与上一个MatSequenceNo相等，说明是工序子树
                    {
                        item.MatSequenceNo = ++intFormatID;
                        item.ProcessSequenceNo = lastID;
                    }
                }

                return finalData;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        /// <summary>
        /// 【？】根据订单详细，生成物料分解信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<MaterialDecompose> GeneralMatDecompose(string orderID, string orderDetail, string version)
        {
            try
            {
                MarketOrderDetail od = GetAvailableList<MarketOrderDetail>().First(a => a.ClientOrderID == orderID && a.ClientOrderDetail == orderDetail);
                //订单详细不存在
                if (od == null)
                {
                    return null;
                }

                IEnumerable<ProdComposition> pc = DecomposeProduct(od.ProductID);

                IEnumerable<MaterialDecompose> finalData = (from p in pc
                                                            select new MaterialDecompose
                                                            {
                                                                ClientOrderID = od.ClientOrderID,
                                                                ClientOrderDetail = od.ClientOrderDetail,
                                                                ProductsPartsID = p.SubItemId,//子项目的ID升级为主键3，父项目ID丢弃
                                                                ProductID = od.ProductID,
                                                                MatSequenceNo = p.MatSequenceNo,//序号
                                                                ProcessSequenceNo = p.ProcessSequenceNo,//父序号
                                                                //空缺：原客户订单号
                                                                //DemondQuantity=od.Quantity*p.ConstQty,
                                                                DemondQuantity = p.ConstQty,//用需求数量暂停存构成数量
                                                                ProvideDate = (DateTime)od.DeliveryDate,
                                                                StartDate = DateTime.Now,//【？】
                                                                ProcessID = p.SubProcId,
                                                                //...
                                                                CreDt = DateTime.Now,
                                                                CreUsrID = "201228"
                                                            }).ToList().OrderBy(a => a.MatSequenceNo).ThenBy(a => a.ProcessSequenceNo);
                //重算构成数量
                decimal lastDemondQtt = 0;//上一物料的需求数量
                foreach (var item in finalData)
                {
                    if (item.ProcessSequenceNo == 0)
                    {
                        item.DemondQuantity = item.DemondQuantity * od.Quantity;
                    }
                    else
                    {
                        item.DemondQuantity = item.DemondQuantity * lastDemondQtt;
                    }
                    lastDemondQtt = item.DemondQuantity;

                }
                return finalData;
            }
            catch (Exception e)
            {            
                throw e;
            }
            
        }
        #endregion

        #region
        public IEnumerable<VM_NewBillDataGrid> GetNewBillDataGrid(string id, string clientOrderID, string clientOrderDetail)
        {
            IQueryable<ProdComposition> prodCompositions = base.GetAvailableList<ProdComposition>().Where(t => t.ParItemId == id && t.ParItemId!=t.SubItemId);

            IQueryable<PartInfo> partInfos = base.GetAvailableList<PartInfo>();

            IQueryable<MaterialDecompose> materialDecomposes = base.GetAvailableList<MaterialDecompose>().Where(t=>t.ClientOrderID.Equals(clientOrderID)&&t.ClientOrderDetail.Equals(clientOrderDetail));

            IQueryable<VM_NewBillDataGrid> querys = from pc in prodCompositions
                                                    join pi in partInfos on pc.SubItemId equals pi.PartId
                                                    join md in materialDecomposes on pc.SubItemId equals md.ProductsPartsID into gmd
                                                    from matDec in gmd.DefaultIfEmpty()                     
                                                    select new VM_NewBillDataGrid
                                                    {
                                                        SubItemID = pc.SubItemId,
                                                        PartName = pi.PartName,
                                                        BatchNumber = "",
                                                        ConstQty = pc.ConstQty,
                                                        Remark = pi.Remark,
                                                        Specifica = matDec!=null?matDec.Specifica:""
                                                    };
            return querys.OrderBy(t => t.SubItemID);
            //throw new NotImplementedException();
        }

        public string GetSubProcID(string id)
        {
            IEnumerable<ProdComposition> list = base.GetAvailableList<ProdComposition>().Where(a => a.ParItemId.Equals(id) && a.ParProcId == "" && a.SubProcId != "").AsEnumerable();
            if (list.Any())
            {
                return list.AsEnumerable().ElementAt(0).SubProcId;
            }
            return "";
        }
        #endregion
     
    }
}
