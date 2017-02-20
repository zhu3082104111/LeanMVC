/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProductSchedulingService.cs
// 文件功能描述：订单产品排产service接口实现
// 
// 创建标识：201311 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;

namespace BLL
{
    /// <summary>
    /// 订单排产，C：梁龙飞
    /// </summary>
    public class ProductSchedulingServiceImp : AbstractService,IProductSchedulingService
    {
        #region define and construct
        private IMaterialDecomposeRepository materialDecomposeRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IProdCompositionRepository prodCompositionRepository;
        private IProduceGeneralPlanRepository produceGeneralPlanrepository;
        private IReserveRepository reserveRepository;
        private IReserveDetailRepository reserveDetailRepository;
        private IBthStockListRepository bthStockListRepository;
        private IMaterialRepository materialRepository;
        private IGiReserveRepository giReserveRepository;
        private IOrderDetailRepository marketOrderdetailRepository;
        private IPurchaseInstructionRepository purchaseInstructionRepository;
        private IAssistInstructionRepository assistInstructionRepository;

        public ProductSchedulingServiceImp(IMaterialDecomposeRepository materialDecomposeRepository,
            IOrderDetailRepository orderDetailRepository,
            IProdCompositionRepository prodCompositionRepository,
            IProduceGeneralPlanRepository produceGeneralPlanrepository,
            IReserveRepository reserveRepository,
            IReserveDetailRepository reserveDetailRepository,
            IBthStockListRepository bthStockListRepository,
            IMaterialRepository materialRepository,
            IGiReserveRepository giReserveRepository,
            IOrderDetailRepository marketOrderdetailRepository,
            IPurchaseInstructionRepository purchaseInstructionRepository,
            IAssistInstructionRepository assistInstructionRepository)
        {
            this.materialDecomposeRepository = materialDecomposeRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.prodCompositionRepository = prodCompositionRepository;
            this.produceGeneralPlanrepository = produceGeneralPlanrepository;
            this.reserveRepository=reserveRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.materialRepository = materialRepository;
            this.giReserveRepository=giReserveRepository;
            this.marketOrderdetailRepository = marketOrderdetailRepository;
            this.purchaseInstructionRepository = purchaseInstructionRepository;
            this.assistInstructionRepository = assistInstructionRepository;
        }
        #endregion 
        #region 梁龙飞C：测试接口
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="clientOrderID"></param>
        ///// <param name="orderDetail"></param>
        ///// <param name="version"></param>
        ///// <returns></returns>
        //public IEnumerable<MaterialDecompose> GeneralMatDecompose(string clientOrderID, string orderDetail, string version)
        //{
        //    return prodCompositionRepository.GeneralMatDecompose(clientOrderID, orderDetail, version);
        //}
        #endregion
        #region 私有方法
        /// <summary>
        /// 此产品订单物料是否存在正常有规格预约
        /// 因为数据连接等其余问题会抛异常。
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool IsNmlRsvWithSpecExist(string clientOrderID, string orderDetail, string materialID)
        {
            try
            {
                Reserve rs = reserveRepository.FirstOrDefault(a => a.ClnOdrID == clientOrderID &&
                 a.ClnOdrDtl == orderDetail &&
                 a.PdtID == materialID &&
                 a.DelFlag != Constant.CONST_FIELD.DELETED &&
                 a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

                if (rs == null)
                {
                    return false;
                }
                if (rs.OrdeBthDtailListID == 0)
                {
                    return false;
                }
                else
                {
                    if (reserveDetailRepository.GetQuantityByDetailID(rs.OrdeBthDtailListID) == 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        /// <summary>
        /// 此产品订单物料是否存在无规格预约
        /// 因为数据连接等其余问题会抛异常
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool IsNmlRsvWithoutSpecExist(string clientOrderID, string orderDetail, string materialID)
        {
            try
            {
                Reserve rs = reserveRepository.FirstOrDefault(a => a.ClnOdrID == clientOrderID &&
                 a.ClnOdrDtl == orderDetail &&
                 a.PdtID == materialID &&
                 a.DelFlag != Constant.CONST_FIELD.DELETED &&
                 a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

                if (rs == null)
                {
                    return false;
                }
                if (rs.OrdeBthDtailListID == 0)
                {
                    if (rs.OrdeQty <= 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (reserveDetailRepository.GetQuantityByDetailID(rs.OrdeBthDtailListID) == rs.OrdeQty)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        /// <summary>
        /// 此产品订单物料是否存在让步预约
        /// 因为数据连接等其余问题会抛异常
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool IsAbnmlReserveExist(string clientOrderID, string orderDetail, string materialID)
        {
            //让步预约
            decimal lkdNum = giReserveRepository.GetLockedAbnormalNum(new VM_MatBtchStockSearch() { ClientOrderID = clientOrderID, OrderDetail = orderDetail, MaterialID = materialID });
            if (lkdNum <= 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 进行排产，插入到数据库:[与整体方法存在不兼容，主要表现在对早期版本创建的生产计划总表数据状态判断失败]
        /// </summary>
        /// <param name="clientOrderID">客户订单号</param>
        /// <param name="orderDetail">订单明细</param>
        /// <param name="version">版本号(目前不做处理)</param>
        /// <returns></returns>
        public IEnumerable<MaterialDecompose> InsertProDecomInfo(string clientOrderID, string orderDetail, string version)
        {
            try
            {
                //目标生产计划总表
                ProduceGeneralPlan prdGnrlPlan = produceGeneralPlanrepository.FirstOrDefault(a =>
                    a.ClientOrderID == clientOrderID && a.ClientOrderDetail == orderDetail &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED && a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                if (prdGnrlPlan == null)
                {
                    throw new Exception("该生产计划不存在！");
                }
                else
                {
                    if (prdGnrlPlan.Status != Constant.GeneralPlanState.ACCEPTED)
                    {
                        throw new Exception("该生产计划所处的状态不符合要求！");
                    }
                    else {
                        //更改生产计划总表中的状态为已排产
                        prdGnrlPlan.Status = Constant.GeneralPlanState.SCHEDULING;
                        produceGeneralPlanrepository.Update(prdGnrlPlan);
                    }
                }

                IEnumerable<MaterialDecompose> md = prodCompositionRepository.GeneralMatDecompose(clientOrderID, orderDetail, version);
                if (md==null||md.Count()==0)
                {
                    throw new Exception("该计划下的产品："+prdGnrlPlan.ProductID+"经过技术分解后没有返回组成结构，因此无法排产！");
                }
                foreach (var item in md)
                {
                    materialDecomposeRepository.Add(item);
                }
                return md;
            }
            catch (Exception e)
            {
                
                throw e;
            }
           
        }

        #region 订单产品排产功能集 C：梁龙飞
        /// <summary>
        /// 根据主键更新不为空的列
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool UpdateNotNullColumn(MaterialDecompose target)
        {
            return materialDecomposeRepository.UpdateNotNullColumn(target);
        }

        /// <summary>
        /// 获取一条排产信息
        /// </summary>
        /// <param name="traget"></param>
        /// <returns></returns>
        public MaterialDecompose GetSingleDecopose(MaterialDecompose target)
        {
            return materialDecomposeRepository.First(a => a.ClientOrderID == target.ClientOrderID &&
                a.ClientOrderDetail == target.ClientOrderDetail &&
                a.ProductsPartsID == target.ProductsPartsID&&
                a.DelFlag!=Constant.CONST_FIELD.DELETED&&
                a.EffeFlag!=Constant.CONST_FIELD.UN_EFFECTIVE);
        }
        /// <summary>
        /// 更新物料规格
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool UpdateSepcification(MaterialDecompose target)
        {
            MaterialDecompose tempT = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                a.ClientOrderDetail == target.ClientOrderDetail &&
                a.ProductsPartsID == target.ProductsPartsID);

            
            //1.原本是无规格要求
            if (tempT.Specifica == null || tempT.Specifica == "")
            {
                //1.1 切换为有规格要求:清空所有无规格预约，保留有规格预约
                if (target.Specifica!=""&&target.Specifica!=null)
                {
                    if (IsNmlRsvWithoutSpecExist(target.ClientOrderID, target.ClientOrderDetail, target.ProductsPartsID) == true)
                    {
                        throw new Exception("拒绝操作：零件：" + target.ProductsPartsID + " 从无规格要求切换为有规格要求时，发现已存在无规格预约！");
                    }

                    decimal offsetLockQtt = 0;//锁存校正值:清空无规格锁存时造成的锁存量变化
                    Reserve rs = reserveRepository.FirstOrDefault(a => a.ClnOdrID == target.ClientOrderID &&
                        a.ClnOdrDtl == target.ClientOrderDetail &&
                        a.PdtID == target.ProductsPartsID);
                    //没有预约信息
                    if (rs==null)
                    {
                        //只更改物料分解表中的规格信息
                        tempT.Specifica = target.Specifica == null ? "" : target.Specifica;
                        return materialDecomposeRepository.Update(tempT);
                    }

                    //1.1.1如果预约了有规格物料
                    if (rs.OrdeBthDtailListID != 0)
                    {
                        //有规格预约数量
                        decimal qttWithSpec = reserveDetailRepository.GetQuantityByDetailID(rs.OrdeBthDtailListID);
                        //1.1.1.1有规格预约数量==0:删除预约详细和仓库预约
                        if (qttWithSpec <= 0)
                        {
                            reserveDetailRepository.DeleteByDetailID(rs.OrdeBthDtailListID);
                            reserveRepository.Delete(rs);
                        }
                        //1.1.1.2有规格预约数量！=0：删除无规格预约部分
                        else
                        {
                            //无规格预约数
                            offsetLockQtt = rs.OrdeQty - qttWithSpec;
                            //将仓库预约数量修改为有规格预约合计数量
                            rs.OrdeQty = qttWithSpec;
                            reserveRepository.Update(rs);
                        }
                        //IQueryable<ReserveDetail> rsdList = reserveDetailRepository.GetReserveDtlByDtlID(rs.OrdeBthDtailListID);
                        ////1.批次不为空：恢复每个批次的数量
                        //if (rsdList!=null)
                        //{
                        //    foreach (var item in rsdList)
                        //    {
                        //        BthStockList bth = bthStockListRepository.FirstOrDefault(a => a.BthID == item.BthID && a.WhID == rs.WhID && a.PdtID == target.ProductsPartsID);
                        //        bth.OrdeQty += item.OrderQty;
                        //        bthStockListRepository.Update(bth);
                        //    }
                        //}
                        ////2.删除明细
                        //reserveDetailRepository.DeleteByDetailID(rs.OrdeBthDtailListID);
                    }
                    //如果没有预约有规格要求的物料：删除所有预约
                    else
                    {
                        offsetLockQtt = rs.OrdeQty;
                        reserveRepository.Delete(rs);//删除预约
                    }

                    //恢复仓库数量
                    Material mt = materialRepository.FirstOrDefault(a => a.PdtID == target.ProductsPartsID && a.WhID == rs.WhID);
                    mt.AlctQty = mt.AlctQty - offsetLockQtt;
                    mt.RequiteQty = mt.RequiteQty - offsetLockQtt;
                    mt.UseableQty = mt.UseableQty + offsetLockQtt;
                    materialRepository.Update(mt);

                    //物料分解信息保存的正常品锁库数量
                    tempT.NormalLockQuantity = tempT.NormalLockQuantity - offsetLockQtt;
                    ////删除无规格预约
                    //reserveRepository.Delete(rs);
                }
            }
            else {
                //原本是有规格要求：切换成无规格
                if (target.Specifica == null || target.Specifica == "")
                {
                    if (IsNmlRsvWithSpecExist(target.ClientOrderID, target.ClientOrderDetail, target.ProductsPartsID) == true)
                    {
                        throw new Exception("拒绝操作：零件：" + target.ProductsPartsID + " 从有规格要求切换为无规格要求时，发现已存在有规格预约！");
                    }
                    //不做处理
                }
                else //原本是有规格要求：切换成其他规格
                {
                    //不做处理
                }
            }

            tempT.Specifica = target.Specifica==null?"":target.Specifica;         
            return materialDecomposeRepository.Update(tempT);
        }

        /// <summary>
        /// 从物料分解信息表中 返回一个产品的分解计划
        /// </summary>
        /// <param name="orderClientID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProductSchedulingShow> GetProDecomInfo(string orderClientID, string orderDetail, string version)
        {
            return materialDecomposeRepository.GetProductSchedulingList(orderClientID, orderDetail, version);
        }

        public bool UpdateProDecomInfo(List<VM_ProductSchedulingShow> targets)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 订单产品排产(已排产则直接返回计划，未排产则先排产再返回）
        /// </summary>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProductSchedulingShow> SchedulingOnePlan(string clientOrderID, string orderDetail, string version)
        {
            //如果不存在，先插入
            if (materialDecomposeRepository.IsSchedulingInfoExist(clientOrderID,orderDetail,version)==false)
            {
                InsertProDecomInfo(clientOrderID, orderDetail, version);
            }
            //返回           
             return GetProDecomInfo(clientOrderID, orderDetail, version);
        }


        /// <summary>
        /// 获取单独一个物料的排产计划显示
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public VM_ProductSchedulingShow GetSingleMatScheduling(MaterialDecompose target)
        {
            VM_ProductSchedulingShow finalData = SchedulingOnePlan(target.ClientOrderID, target.ClientOrderDetail, "").FirstOrDefault(a => a.MaterialID == target.ProductsPartsID);
            return finalData;
        }

        /// <summary>
        /// 返回格式化后的排产序列
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProductSchedulingShow> GetFormatedSchedulingInf(List<int> ids, string clientOrderID, string orderDetail, string version)
        {
            IEnumerable< VM_ProductSchedulingShow> finalList=GetProDecomInfo(clientOrderID, orderDetail, version).Where(a => ids.Contains(a.id));
            return finalList.OrderBy(a=>a.id);
        }

        /// <summary>
        ///  更新整个产品下所有物料以下排产信息：自产、外协、外购，提供日期、计划天数、启动日期
        /// </summary>
        /// <param name="matDecomList"></param>
        /// <returns></returns>
        public bool UpdateCommission(List<MaterialDecompose> matDecomList)
        {
            foreach (var item in matDecomList)
            {
                //取得对象
                MaterialDecompose orData = GetSingleDecopose(item);

                //1.生产数量
                if (item.ProduceNeedQuantity != orData.ProduceNeedQuantity)
                {
                    orData.ProduceNeedQuantity = item.ProduceNeedQuantity;
                }
                //2.外协数量 
                if (item.AssistNeedQuantity != orData.AssistNeedQuantity)
                {
                    orData.AssistNeedQuantity = item.AssistNeedQuantity;
                }
                //3.采购数量
                if (item.PurchNeedQuantity != orData.PurchNeedQuantity)
                {
                    orData.PurchNeedQuantity = item.PurchNeedQuantity;
                }
                //4.提供日期
                if (item.ProvideDate != orData.ProvideDate)
                {
                    orData.ProvideDate = item.ProvideDate;
                }
                //5.计划天数
                if (item.PreparationPeriod != orData.PreparationPeriod)
                {
                    orData.PreparationPeriod = item.PreparationPeriod;
                }
                //6.启动日期
                if (item.StartDate != orData.StartDate)
                {
                    orData.StartDate = item.StartDate;
                }
                materialDecomposeRepository.Update(orData);

            }
            return true;
        }

#region 订单产品排产的旧版本：C：梁龙飞
        ///// <summary>
        ///// 订单产品排产
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <param name="clientOrderID"></param>
        ///// <param name="orderDetail"></param>
        ///// <returns></returns>
        //public bool ConfirmSchedule(string clientOrderID, string clientOrderDetail, string version)
        //{
        //    //提取目标:因为效率问题，此处不再取格式化后的数据，因此丧失了合法检测的能力
        //    IEnumerable<MaterialDecompose> matdtl = materialDecomposeRepository.GetAvailableList(clientOrderID, clientOrderDetail, version);

        //    if (matdtl.Count() == 0 || matdtl == null)
        //    {
        //        throw new Exception("目标（客户订单号：" + clientOrderID + ",客户订单明细：" + clientOrderDetail + ")不存在！");
        //    }
        //    //1.发布自产指示、外购指示、外协指示【自产目前不做】
        //    foreach (var item in matdtl)
        //    {
        //        //1.1合法有效性检测
        //        if (item.NormalLockQuantity + item.AbnormalLockQuantity + item.ProduceNeedQuantity + item.AssistNeedQuantity + item.PurchNeedQuantity < item.DemondQuantity)
        //        {
        //            throw new Exception("物料：" + item.ProductsPartsID + "（第" + item.MatSequenceNo + "行）的锁存与投产数量和小于满足需求量！");
        //        }

        //        //1.2发布外购指示

        //        //1.3发布外协指示

        //    }
        //    //2.发布生产排期计划

        //    //3.更改此订单产品的状态：已排产
        //    throw new NotImplementedException();
        //}
#endregion

        /// <summary>
        /// 订单产品排产
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public bool ConfirmSchedule(string clientOrderID, string clientOrderDetail, string version)
        {
            try
            {
                IEnumerable<VM_ProductSchedulingShow> prodSchlList = materialDecomposeRepository.GetProductSchedulingList(clientOrderID, clientOrderDetail, version);
                //生产单元号取得
                MarketOrderDetail mkoOne = marketOrderdetailRepository.FirstOrDefault(a =>
                    a.ClientOrderID == clientOrderID &&
                    a.ClientOrderDetail == clientOrderDetail &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);//GetAvailableList<MarketOrderDetail>().First(a => a.ClientOrderID == clientOrderID && a.ClientOrderDetail == clientOrderDetail);
                if (mkoOne == null)
                {
                    throw new Exception("生产单元号不存在！");
                }
                string productCellID = mkoOne.ProduceCellID;

                foreach (var item in prodSchlList)
                {
                  
                    //1.1合法性检测
                    decimal schedulPlanDemand = item.NormalInLock + item.AbnormalInLock + item.ProduceQuantity + item.PurchQuantity + item.AssistQuantity;
                    if (schedulPlanDemand < item.DemondQuantityFloor || schedulPlanDemand > item.DemondQuantityCeiling)
                    {
                        throw new Exception("物料：" + item.MaterialID + "（第" + item.id + "行）的锁存与投产数量和不在需求量范围内！");
                    }
                    //1.2有效性检测
                    if (item.PurchQuantity <= 0 && item.AssistQuantity <= 0&&item.ProduceQuantity<=0)
                    {
                        continue;
                    }

                    //2.发布自产指示、外购指示、外协指示【自产目前不做】
                    //2.1发布外购指示
                    if (item.PurchQuantity>0)
                    {
                        PurchaseInstruction pchsInst = new PurchaseInstruction()
                        {
                            ClientOrderID = item.ClientOrderID,
                            ClientOrderDetail = item.ClientOrderDetail,
                            ProductsPartsID = item.MaterialID,
                            ProductID = item.ProductID,
                            ReceiveFlag = Constant.PurchaseInstState.UN_SCHEDULED,
                            ReceiveDate = Constant.CONST_FIELD.DB_INIT_DATETIME,
                            DepartmentID = productCellID,
                            ProcessID = item.ProcessID
                        };
                        purchaseInstructionRepository.Add(pchsInst);
                    }
                   
                    //2.2发布外协指示
                    if (item.AssistQuantity>0)
                    {
                        AssistInstruction asistinst = new AssistInstruction()
                        {
                            ClientOrderID = item.ClientOrderID,
                            ClientOrderDetail = item.ClientOrderDetail,
                            ProductsPartsID = item.MaterialID,
                            ProductID = item.ProductID,
                            ReceiveFlag = Constant.PurchaseInstState.UN_SCHEDULED,
                            ReceiveDate = Constant.CONST_FIELD.DB_INIT_DATETIME,
                            DepartmentID = productCellID,
                            ProcessID = item.ProcessID
                        };
                        assistInstructionRepository.Add(asistinst);
                    }
                   
                    //3.发布仓库预约：
                    Reserve rs = reserveRepository.FirstOrDefault(a =>
                        a.ClnOdrID == clientOrderID &&
                        a.ClnOdrDtl == clientOrderDetail &&
                        a.DelFlag != Constant.CONST_FIELD.DELETED && 
                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                    if (rs == null)
                    {
                        //不存在预约：没有锁过物料=>新增仓库预约
                        rs = new Reserve
                        {
                            WhID = item.WhID,
                            ClnOdrID = item.ClientOrderID,
                            ClnOdrDtl = item.ClientOrderDetail,
                            OrdPdtID = item.ProductID,
                            PdtID = item.MaterialID,
                            OrdeBthDtailListID = 0,//没有锁存时，定然没有锁到预约详细批次
                            PdtSpec = item.MaterialSpecification,
                            OrdeQty = item.PurchQuantity + item.AssistQuantity + item.ProduceQuantity,
                            RecvQty = 0,
                            PickOrdeQty = 0,
                            CmpQty = 0
                        };
                        reserveRepository.Add(rs);
                    }
                    else 
                    {
                        rs.OrdeQty += item.PurchQuantity + item.AssistQuantity + item.ProduceQuantity;
                        reserveRepository.Update(rs);
                    }
                    //4.更新仓库表
                    Material mat = materialRepository.FirstOrDefault(a =>
                        a.WhID == item.WhID &&
                        a.PdtID == item.MaterialID &&
                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                    if (mat == null)
                    {
                        throw new Exception(item.WhID + "（仓库）中不存在 " + item.MaterialName + " 的登记履历！");
                    }
                    else
                    {
                        //mat.RequiteQty+=item.PurchQuantity + item.AssistQuantity;
                        mat.AlctQty += item.PurchQuantity + item.AssistQuantity + item.ProduceQuantity;
                        materialRepository.Update(mat);
                    }
                }
               
                //5.更改此订单产品的状态：已排产
                ProduceGeneralPlan gnp = produceGeneralPlanrepository.FirstOrDefault(a => a.ClientOrderID == clientOrderID && a.ClientOrderDetail == clientOrderDetail && a.DelFlag != Constant.CONST_FIELD.DELETED && a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                if (gnp.Status == Constant.GeneralPlanState.SCHEDULED)
                {
                    throw new Exception("订单编号：" + clientOrderID + ",详细号：" + clientOrderDetail + "的排产状态为已排产，不可排产！");
                }
                else {
                    gnp.Status = Constant.GeneralPlanState.SCHEDULED;
                }
                produceGeneralPlanrepository.Update(gnp);
                return true;
            }
            catch (Exception e)
            {              
                throw e;
            }
                   
        }
        #endregion
    }
}
