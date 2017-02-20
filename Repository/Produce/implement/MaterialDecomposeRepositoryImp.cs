using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;

namespace Repository
{
    /// <summary>
    /// C：梁龙飞
    /// 物料分解资源库
    /// </summary>
    public class MaterialDecomposeRepositoryImp : AbstractRepository<DB, MaterialDecompose>, IMaterialDecomposeRepository
    {
        #region 私有方法：梁龙飞
        /// <summary>
        /// 获取正常产品在仓库中的可锁库存量
        /// 产品不存在时返回为0
        /// C:梁龙飞
        /// </summary>
        /// <param name="whID"></param>
        /// <param name="matID"></param>
        /// <returns></returns>
        private decimal GetUnlockedNormalNum(string whID, string matID)
        {
            Material temp = GetAvailableList<Material>().FirstOrDefault(a => a.WhID == whID && a.PdtID == matID);
            if (temp != null)
            {
                return temp.UseableQty;
            }
            return 0;
        }

        /// <summary>
        /// 获取让步产品在仓库中的可锁库存量
        /// 产品不存在时返回为0
        /// C:梁龙飞
        /// </summary>
        /// <param name="whID"></param>
        /// <param name="matID"></param>
        /// <returns></returns>
        private decimal GetUnlockedAbnormalNum(string whID, string matID)
        {
            try
            {
                decimal totAvaiQtt = GetAvailableList<GiMaterial>().Where(a => a.WareHouseID == whID && a.ProductID == matID).Sum(a => a.UserableQuantity);
                return totAvaiQtt;
            }
            catch (Exception)
            {
                return 0;
                //throw;
            }


        }

        #endregion

        #region ProductScheduling C:梁龙飞

        /// <summary>
        /// 查出一个订单明细的排产计划
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProductSchedulingShow> GetProductSchedulingList(string orderID, string orderDetail, string version)
        {
            //生产单元号取得
            MarketOrderDetail mkoOne = GetAvailableList<MarketOrderDetail>().First(a => a.ClientOrderID == orderID && a.ClientOrderDetail == orderDetail);
            string productCellID = mkoOne.ProduceCellID;
            //产品信息取得
            ProdInfo prdOne = GetAvailableList<ProdInfo>().First(a => a.ProductId == mkoOne.ProductID);

            //1.物料分解信息
            IQueryable<MaterialDecompose> matdtl = GetAvailableList<MaterialDecompose>().
                Where(a => a.ClientOrderID == orderID && a.ClientOrderDetail == orderDetail);
            if (matdtl==null||matdtl.Count()==0)
            {
                throw new Exception("订单号为"+orderID+"订单明细为"+orderDetail+"的订单排产信息不存在！");
            }
            matdtl = matdtl.OrderBy(a => a.MatSequenceNo).ThenBy(a => a.ProcessSequenceNo);
            //2.零件信息
            IQueryable<PartInfo> partif = GetAvailableList<PartInfo>();

            //3.工序信息
            IQueryable<Process> procif = GetAvailableList<Process>();

            //4.产品信息
            IQueryable<ProdInfo> prodinf = GetAvailableList<ProdInfo>();

            //5.成品构成信息【取构成数】
            IQueryable<ProdComposition> prodcomp=GetAvailableList<ProdComposition>();

            //6.物料正常库存信息
            IQueryable<Material> nomarlMat = GetAvailableList<Material>();

            //7.物料让步库存信息
            IQueryable<GiMaterial> giveInMat = GetAvailableList<GiMaterial>();

            //排产视图生成
            IEnumerable<VM_ProductSchedulingShow> finalData = (from mat in matdtl
                                                              join prdcmp in prodcomp//构成数量
                                                              on mat.ProductsPartsID equals prdcmp.SubItemId//目前如此理解：工序是链结构，不能存在网状和树结构

                                                               join pati in partif//left join 零件信息
                                                               on mat.ProductsPartsID equals pati.PartId into dataWithPartName
                                                               from patiTemp in dataWithPartName.DefaultIfEmpty()

                                                               join prci in procif//left join 工序
                                                               on mat.ProcessID equals prci.ProcessId into dataWithProc
                                                               from procTemp in dataWithProc.DefaultIfEmpty()                                                             

                                                               select new VM_ProductSchedulingShow
                                                               {
                                                                   id = mat.MatSequenceNo,
                                                                   _parentId = mat.ProcessSequenceNo,
                                                                   ClientOrderID = mat.ClientOrderID,
                                                                   ClientOrderDetail = mat.ClientOrderDetail,
                                                                   ProductID = mat.ProductID,
                                                                   ProductType = prdOne.ProdAbbrev,//产品类型：产品缩写
                                                                   WhID = patiTemp != null ? (patiTemp.PartCatg + mkoOne.ProduceCellID) : "",//仓库编号=种类+生产单元区分
                                                                   MaterialID = mat.ProductsPartsID,
                                                                   MaterialName =patiTemp!=null? patiTemp.PartName:"",
                                                                   MaterialSpecification = mat.Specifica == null ? "" : mat.Specifica,
                                                                   ConstituteQuantity = prdcmp.ConstQty,
                                                                   LossRate =procTemp!=null? procTemp.AttriRate/10000:0,//损耗率,计算成实际值
                                                                   DemondQuantityFloor = mat.DemondQuantity,//需求下限
                                                                   DemondQuantityCeiling = mat.DemondQuantity * (1 + (procTemp!=null? procTemp.AttriRate/10000:0)),//需要数量上限
                                                                   NormalInStore = 0,//库存量，【？需要仓库提供接口】
                                                                   AbnormalInStore = 0,//''
                                                                   NormalInLock = mat.NormalLockQuantity,//正常锁库数量
                                                                   AbnormalInLock = mat.AbnormalLockQuantity,//【？修改名称：应为special】
                                                                   TotalDemondQuantity = mat.DemondQuantity - mat.NormalLockQuantity - mat.AbnormalLockQuantity,//投料小计

                                                                   ProduceQuantity = mat.ProduceNeedQuantity,//【？此三列数据库中存在混乱数据段，待定】
                                                                   PurchQuantity = mat.PurchNeedQuantity,
                                                                   AssistQuantity = mat.AssistNeedQuantity,

                                                                   SeUtProdQty=procTemp!=null?procTemp.SeUtProdQty:0,
                                                                   AsUtProdQty=procTemp!=null?procTemp.AsUtProdQty:0,
                                                                   PuUtProdQty=procTemp!=null?procTemp.PuUtProdQty:0,


                                                                   ProvideDate = mat.ProvideDate,
                                                                   StartDate = mat.StartDate,

                                                                   StandPreparationPeriod = 0,//标准备料工期
                                                                   PreparationPeriod = mat.PreparationPeriod,//计划备料工期

                                                                   ProcessID=procTemp!=null?procTemp.ProcessId:"",
                                                                   ProcessName = procTemp!=null?procTemp.ProcName:""
                                                               }).ToList().OrderBy(a => a.id).ThenBy(a => a._parentId);

            //第一行产品的仓库ID生成
            finalData.First(a => a.id == 1).WhID = prdOne.ProdCatg + mkoOne.ProduceCellID;
            //第一行的物料是产品本身
            finalData.First(a => a.id == 1).MaterialName = finalData.First().ProductType;
            //格式化数据
            foreach (var item in finalData)
            {
                //A.计算备料工期
                if (item.SeUtProdQty!=0)
                {
                    item.StandPreparationPeriod = item.ProduceQuantity / item.SeUtProdQty;
                }
                if (item.SeUtProdQty != 0)
                {
                    item.StandPreparationPeriod = item.StandPreparationPeriod > (item.AssistQuantity / item.AsUtProdQty) ? item.StandPreparationPeriod : (item.AssistQuantity / item.AsUtProdQty);
                }
                if (item.SeUtProdQty != 0)
                {
                    item.StandPreparationPeriod = item.StandPreparationPeriod > (item.PurchQuantity / item.PuUtProdQty) ? item.StandPreparationPeriod : (item.PurchQuantity / item.PuUtProdQty);
                }
                item.StandPreparationPeriod = Math.Ceiling(item.StandPreparationPeriod/24);
                item.PreparationPeriod = item.PreparationPeriod == 0 ? item.StandPreparationPeriod : item.PreparationPeriod;
               
                //B.统计正常、让步品在库信息
                item.NormalInStore = GetUnlockedNormalNum(item.WhID, item.MaterialID);
                item.AbnormalInStore=GetUnlockedAbnormalNum(item.WhID, item.MaterialID);

                //C.重新计算需求量上限、下限
                VM_ProductSchedulingShow parNode =finalData.First(a=>a.id==1);//直系父结点为产品
                if (item._parentId != 0)
                {
                    parNode = finalData.First(a => a.id == item.id - 1);//直系父结点为物料
                }
                if (item.id==1)
                {
                    item.StartDate = (item.ProvideDate).AddDays(-(double)(item.PreparationPeriod));
                    continue;
                }
                //C1.需求量下限：（父结点的投料小计 - 外购） * 组成单位父亲的数量
                item.DemondQuantityFloor = (parNode.TotalDemondQuantity - parNode.PurchQuantity) * item.ConstituteQuantity;
                //C2.需求量上限:下限加上损耗值
                item.DemondQuantityCeiling = Math.Ceiling(item.DemondQuantityFloor * item.LossRate) + item.DemondQuantityFloor;
                //D.投料小计
                item.TotalDemondQuantity = item.DemondQuantityFloor-item.NormalInLock-item.AbnormalInLock;
                //E.提供日期、启动日期计算
                item.ProvideDate =parNode.StartDate ;
                item.StartDate = (item.ProvideDate).AddDays(-(double)(item.PreparationPeriod));


            }
           
            return finalData;
        }

        /// <summary>
        /// 排产信息是否存在
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool IsSchedulingInfoExist(string orderID, string orderDetail, string version)
        {
            IQueryable<MaterialDecompose> data = base.GetAvailableList<MaterialDecompose>().Where(a => a.ClientOrderID == orderID && a.ClientOrderDetail == orderDetail);
            if (data != null && data.Count() > 0)
            {
                return true;
            }
            return false;
           
        }

        /// <summary>
        /// 获取有效的查询结果集
        /// 没有结果时返回null
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<MaterialDecompose> GetAvailableList(string orderID, string orderDetail, string version)
        {
            IEnumerable<MaterialDecompose> finalDatas =
                GetAvailableList<MaterialDecompose>().Where(a => a.ClientOrderID == orderID &&
                a.ClientOrderDetail == orderDetail);
            if (finalDatas.Count()==0||finalDatas ==null)
            {
                return null;
            }
            return finalDatas.OrderBy(a => a.MatSequenceNo).
                ThenBy(a => a.ProcessSequenceNo);

        }
        #endregion

        #region 替代方案：梁龙飞
        /// <summary>
        /// [废弃：此功能在检索排产信息时算出，不再直接更新到数据库]
        /// 递归更新物料的需求量
        /// C：梁龙飞，慎用
        /// </summary>
        /// <param name="target"></param>
        /// <param name="offsetDmdQtt">校正需要量</param>
        /// <returns></returns>
        public string RecursionUpdate(MaterialDecompose target, decimal offsetDmdQtt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [废弃：此功能在检索排产信息时算出，不再直接更新到数据库]
        /// 从目标结点开始(不更新目标结点)，更新到树叶：需求量
        /// 产品的深度为0[考虑从界面上取，后台的工作量太大]
        /// </summary>
        /// <param name="target"></param>
        /// <param name="offsetDmdQtt"></param>
        /// <returns></returns>
        public List<int> RelationUpdate(MaterialDecompose target, decimal offsetDmdQtt)
        {
            //try
            //{
            //    List<int> finalData = new List<int>();
            //    IEnumerable<MaterialDecompose> listWillUpdate = GetAvailableList<MaterialDecompose>().Where(
            //        a => a.ClientOrderID == target.ClientOrderID &&
            //            a.ClientOrderDetail == target.ClientOrderDetail &&
            //            a.MatSequenceNo > target.MatSequenceNo).OrderBy(a => a.MatSequenceNo);
            //    //1.深度为0:更新所有子结点
            //    if (target.MatSequenceNo == 1)
            //    {
            //        listWillUpdate = listWillUpdate.ToList();
            //        foreach (var item in listWillUpdate)
            //        {
            //            //查找每个物料的组成
            //            ProdComposition prod = GetAvailableList<ProdComposition>().FirstOrDefault(a => a.ParItemId == target.ProductsPartsID &&
            //                    a.SubItemId == item.ProductsPartsID);
            //            item.DemondQuantity = item.DemondQuantity - offsetDmdQtt*prod.ConstQty;
            //            Update<MaterialDecompose>(item);
            //            finalData.Add(item.MatSequenceNo);
            //        }
            //        return finalData;
            //    }
            //    else
            //    {
            //        //2.存在于树中，且深度至少为2：
            //        if (target.ProcessSequenceNo != 0)
            //        {
            //            listWillUpdate = listWillUpdate.Where(a=>a.ProcessSequenceNo == target.ProcessSequenceNo).ToList();
            //        }
            //            //3.深度为1：
            //        else 
            //        {
            //            listWillUpdate = listWillUpdate.Where(a => a.ProcessSequenceNo == target.MatSequenceNo).ToList();              
            //            if (listWillUpdate == null)
            //            {
            //                return finalData;
            //            }
            //        }

            //        MaterialDecompose lastMatDecom = target;//上一个物料信息(小心引用)
            //        decimal lastOffsetDmdQtt = offsetDmdQtt;//上一个物料的锁存的增加值
            //        foreach (var item in listWillUpdate)
            //        {
            //            //查找构成数量
            //            ProdComposition prod = GetAvailableList<ProdComposition>().FirstOrDefault(a => a.ParItemId == lastMatDecom.ProductsPartsID &&
            //                    a.SubItemId == item.ProductsPartsID);
            //            if (prod == null)
            //            {
            //                throw new Exception("技术构成信息不存在！");
            //                //prod = new ProdComposition() { ConstQty = 1 };
            //            }
            //            lastOffsetDmdQtt = lastOffsetDmdQtt * prod.ConstQty;
            //            item.DemondQuantity = item.DemondQuantity - lastOffsetDmdQtt;

            //            Update<MaterialDecompose>(item);
            //            finalData.Add(item.MatSequenceNo);

            //            lastMatDecom = item;
            //        }
            //        return finalData;
            //    }
            //}
            //catch (Exception e)
            //{

            //    throw e;
            //}
            return null;

        }

        #endregion
    }
}
