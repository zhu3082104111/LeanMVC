/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IStoreLockService.cs
// 文件功能描述：产品零件预约锁存服务层
// 
// 创建标识：20131215 梁龙飞
//----------------------------------------------------------------*/
#define LLF_AUTODATA
#undef LLF_AUTODATA
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;

namespace BLL
{
    public class StoreLockServiceImp :AbstractService,IStoreLockService
    {
        private IBthStockListRepository bthStockListRepository;
        private IReserveRepository reserveRepository;
        private IReserveDetailRepository reserveDetailRepository;
        private IMaterialRepository materialRepository;
        private IMaterialDecomposeRepository materialDecomposeRepository;

        private IGiMaterialRepository giMaterialRepository;
        private IGiReserveRepository giReserveRepository;

        public StoreLockServiceImp(IBthStockListRepository bthStockListRepository,
            IReserveRepository reserveRepository,
            IMaterialRepository materialRepository,
            IReserveDetailRepository reserveDetailRepository,
            IMaterialDecomposeRepository materialDecomposeRepository,
            IGiMaterialRepository giMaterialRepository,
            IGiReserveRepository giReserveRepository)
        {
            this.bthStockListRepository = bthStockListRepository;
            this.reserveRepository = reserveRepository;
            this.materialRepository = materialRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.materialDecomposeRepository = materialDecomposeRepository;

            this.giMaterialRepository = giMaterialRepository;
            this.giReserveRepository = giReserveRepository;
        }

        #region 自动生成测试数据：C:梁龙飞,禁止其余人使用修改删除
        //溯流生成仓库表信息
        private bool LLF_AddTestMaterial(VM_LockReserveShow target)
        {
            try
            {
                int qtt = (new Random().Next(1, 10)) * 3000;
                Material mat = new Material()
                {
                    WhID = target.WhID,
                    PdtID = target.MaterialID,
                    AlctQty = target.OrderQuantity,
                    RequiteQty = 0,//需求总量

                    UseableQty = qtt,
                    CurrentQty = qtt

                };
                materialRepository.Add(mat);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// 逆流生成让步仓库表测试数据
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool LLF_AddGiMat(VM_LockReserveShow target)
        {
            try
            {
                int qtt = (new Random().Next(1, 10)) * 300;
                GiMaterial gimat = new GiMaterial()
                {
                    WareHouseID = target.WhID,
                    ProductID = target.MaterialID,
                    BatchID=target.BthID,
                    ProductSpec=target.Specification,
                    AlctQuantity=target.OrderQuantity,
                    UserableQuantity = qtt,//可用在库数量
                    CurrentQuantity = qtt,//实际在数量
                    DiscardQuantity=0,
                    GiCls = target.GiveinCatID

                };
                giMaterialRepository.Add(gimat);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
 
        }
        //溯流生成批次别库存表信息
        private bool LLF_AddTestBthStock(VM_LockReserveShow target)
        {
            try
            {
                int qtt = (new Random().Next(1, 10)) * 3000;
                BthStockList mat = new BthStockList()
                {
                    BillType="LF",
                    PrhaOdrID="WX_" + DateTime.Now.ToString("yyyyMMddhhmmss").Substring(5),
                    WhID = target.WhID,
                    BthID=target.BthID,
                    PdtID = target.MaterialID,
                    
                    PdtSpec=target.Specification,
                    GiCls = "999",//非让步
                    Qty=qtt,
                    OrdeQty = target.OrderQuantity,
                    CmpQty =0,//需求总量

                    InDate=DateTime.Now,
                    DiscardFlg="0"

                };
                bthStockListRepository.Add(mat);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
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

        #region 锁存功能集，旧版本 ：废弃 C：梁龙飞
     

//        /// <summary>
//        /// 正常锁存 新增,检测规格型号来分辨是否是有规格型号的锁存
//        /// C：梁龙飞【？强检测：内部自检】
//        /// </summary>
//        /// <param name="target"></param>
//        /// <returns></returns>
//        public bool LockNormalBatch(VM_LockReserveShow target)
//        {
//            try
//            {           
//                //1.-----仓库表：更新零件的数量信息
//                Material mat = materialRepository.First(a => a.WhID == target.WhID && a.PdtID == target.MaterialID);
//                if (mat == null)
//                {
//#if LLF_AUTODATA
//                    return LLF_AddTestMaterial(target);//【自动化测试数据逆流生成：仓库表零件的统计信息】
//#endif
//                    throw new Exception("编号为：" + target.MaterialID + " 的零件（产品）在仓库表中没有登记……");
//                }
//                mat.AlctQty = mat.AlctQty + target.OrderQuantity;//被预约数量
//                mat.RequiteQty = mat.RequiteQty + target.TotNeeded;//所有计划单对此物料的需求总量（不包括单配）
//                mat.UseableQty = mat.UseableQty - target.OrderQuantity;//可用在库数量             
//                materialRepository.Update(mat);

//                //2.物料分解信息表：正常锁存品数量
//                MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
//                        a.ClientOrderDetail == target.OrderDetail &&
//                        a.ProductsPartsID == target.MaterialID &&
//                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
//                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
//                mtD.NormalLockQuantity = mtD.NormalLockQuantity + target.OrderQuantity;
//                materialDecomposeRepository.Update(mtD);

//                //3.-----仓库预约表：不存在？插入：跳过
//                Reserve whs = new Reserve()
//                {
//                    WhID = target.WhID,
//                    ClnOdrID = target.ClientOrderID,
//                    ClnOdrDtl = target.OrderDetail,
//                    OrdPdtID = target.ProductID,
//                    PdtID = target.MaterialID,
//                    OrdeBthDtailListID = 0,//无规格锁，没有详细
//                    PdtSpec = target.Specification,//规格要求
//                    OrdeQty = target.OrderQuantity,//预约总数量=客户订单对此零件需求数量合计-让步品锁存数量,初始化为锁存数量
//                    RecvQty = target.OrderQuantity//订单对此产品所需要的总数量中在库的数量部分，初始化为锁存数量
//                    //...
//                };

//                IEnumerable<Reserve> rsList = reserveRepository.GetReserveByKeys(whs);
//                //如果未预约，则预约
//                if (rsList == null || rsList.Count() == 0)
//                {
//                    //无规格：只生成仓库预约：详细号为0
//                    if ((target.Specification == null || target.Specification == "")&&(target.BthID==""||target.BthID==null))
//                    {
//                        reserveRepository.Add(whs);
//                        return true;
//                    }
//                    //有规格：生成新的预约详细单号
//                    whs.OrdeBthDtailListID = reserveDetailRepository.GetMaxBthDetailCode() + 1;
//                    reserveRepository.Add(whs);
//                }
//                else if (rsList.Count() == 1)
//                {
//                    //无规格：退出
//                    if (target.Specification == null || target.Specification == "")
//                    {
//                        return true;
//                    }
//                    //有规格：取得该预约的预约详细单号
//                    whs.OrdeBthDtailListID = rsList.First().OrdeBthDtailListID;
//                }
//                else {
//                    //存在多条预约信息，说明数据异常：禁止锁存，抛出异常
//                    throw new Exception(target.ClientOrderID+" "+target.OrderDetail+" "+target.MaterialID+" 存在多条预约信息！");
//                }
               
//                //4.仓库预约详细表：插入一条新的有规格正常品预约详细
//                ReserveDetail rsd = new ReserveDetail()
//                {
//                    OrdeBthDtailListID = whs.OrdeBthDtailListID,
//                    BthID=target.BthID,
//                    OrderQty=target.OrderQuantity
//                };
//                reserveDetailRepository.Add(rsd);

//                //5.批次别库存表：更新批次别库存中的数量信息
//                BthStockList bsl = bthStockListRepository.First(a => a.BthID == target.BthID && a.WhID == target.WhID && a.PdtID == target.MaterialID);
//                if (bsl == null)
//                {
//                    throw new Exception("编号为：" + target.MaterialID + "的零件（产品）在批次别库存表中没有登记……");
//                }
//                bsl.OrdeQty = bsl.OrdeQty + target.OrderQuantity;//被预约总数增加
//                bthStockListRepository.Update(bsl);  

//                return true;               
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }
            
//        }

//        /// <summary>
//        /// 正常锁存 更新:当锁存数量改为0时，删除相关的锁存信息
//        /// 【？复杂度过高 ，考虑进一步拆分】
//        /// C：梁龙飞【？强检测：内部自检：｛未考虑负值｝】【没有更新仓库表】
//        /// </summary>
//        /// <param name="target"></param>
//        /// <returns></returns>
//        public bool UpdateNormalBatch(VM_LockReserveShow target)
//        {
//            try
//            {
//                Reserve rs = reserveRepository.First(a => a.ClnOdrID == target.ClientOrderID &&
//                    a.ClnOdrDtl == target.OrderDetail &&
//                    a.PdtID==target.MaterialID&&
//                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
//                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
//                if (rs == null)
//                {
//                    throw new Exception(target.MaterialID + "不存在预约");
//                }

//                MaterialDecompose mtD = materialDecomposeRepository.First(a => a.ClientOrderID == target.ClientOrderID &&
//                        a.ClientOrderDetail == target.OrderDetail &&
//                        a.ProductsPartsID == target.MaterialID &&
//                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
//                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

//                //新锁值偏差=新锁值-原锁值
//                decimal offsetLockedNum = target.OrderQuantity - mtD.NormalLockQuantity;


//                //有规格型号的锁存更改
//                if (target.Specification != null && target.Specification != "")
//                {
//                    //1.1更新详细预约表：
//                    ReserveDetail rsd = reserveDetailRepository.First(a => a.OrdeBthDtailListID == rs.OrdeBthDtailListID &&
//                        a.BthID == target.BthID &&
//                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
//                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
//                    if (target.OrderQuantity == 0)
//                    {
//                        reserveDetailRepository.Delete(rsd);
//                    }
//                    else
//                    {
//                        rsd.OrderQty = target.OrderQuantity;
//                        reserveDetailRepository.Update(rsd);
//                    }
//                }

//                //1.2 更新预约表:预约总数量
//                rs.OrdeQty = rs.OrdeQty + offsetLockedNum;
//                if (rs.OrdeQty == 0)
//                {
//                    reserveRepository.Delete(rs);
//                }
//                else { reserveRepository.Update(rs); }


//                //2.更新仓库表:被预约数量、物料需求量、可用在库数量
//                Material mt = materialRepository.First(a => a.WhID == target.WhID &&
//                    a.PdtID == target.MaterialID &&
//                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
//                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
//                mt.AlctQty = mt.AlctQty + offsetLockedNum;
//                mt.RequiteQty = mt.RequiteQty + offsetLockedNum;
//                mt.UseableQty = mt.UseableQty - offsetLockedNum;
//                materialRepository.Update(mt);

//                //3.更新物料分解表：正常品库存数量
//                mtD.NormalLockQuantity = mtD.NormalLockQuantity + offsetLockedNum;
//                materialDecomposeRepository.Update(mtD);
//                return true;
//            }
//            catch (Exception e)
//            {
                
//                throw e;
//            }
           
//        }

       

        #endregion

        #region 优化版本：C：梁龙飞

        /// <summary>
        /// 混合显示有规格正常品可锁、已锁批次信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetNormalMixBatch(VM_MatBtchStockSearch condition)
        {
            try
            {
                //已锁批次
                IEnumerable<VM_LockReserveShow> finalData = bthStockListRepository.GetNormalLockedBatch(condition);

                //可锁批次
                List<string> btIDLocked = finalData.Select(a => a.BthID).ToList();//可锁中剔除目标
                IEnumerable<VM_LockReserveShow> btUnlock = (bthStockListRepository.GetNormalUnLockedBatch(condition)).Where(a => !btIDLocked.Contains(a.BthID));

                finalData = finalData.Concat(btUnlock);

                return finalData;
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        /// <summary>
        /// 混合显示让步品可锁、已锁批次信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<VM_LockReserveShow> GetAbnormalMixBatch(VM_MatBtchStockSearch condition)
        {
            //已锁批次
            IEnumerable<VM_LockReserveShow> finalData = bthStockListRepository.GetAbnormalLockedBatch(condition);
            //可锁批次
            List<string> btIDLocked = finalData.Select(a => a.BthID).ToList();//可锁中剔除目标
            IEnumerable<VM_LockReserveShow> btUnlock=(bthStockListRepository.GetAbnormalUnlockedBatch(condition)).Where(a => !btIDLocked.Contains(a.BthID));
            finalData = finalData.Concat(btUnlock);

            return finalData;
        }

        #region 形象逻辑
        ////1.2.1此正常无规格物料没有预约有规格型号的物料 
        //if (rs.OrdeBthDtailListID == 0)
        //{                 
        //    rs.OrdeQty = rs.OrdeQty + OffsetLockQtt;//预约总数量=预约总数量+校正值（不影响外协外购的预约数量）
        //    rs.RecvQty = rs.RecvQty + OffsetLockQtt;//实际在库数量=实际在库数量+校正值（不影响外协外购的到货数量）
        //    reserveRepository.Update(rs);
        //}
        //    //1.2.2此无规格要求的物料预约了有规格型号的物料
        //else
        //{                                      
        //    decimal lockQttWithoutSpec = reserveDetailRepository.GetQuantityByDetailID(rs.OrdeBthDtailListID);
        //    //1.2.2.1 虽然存在无规格预约详细号，但是实际没有锁存有规格型号的物料：与1.2.1的流程一样
        //    if (lockQttWithoutSpec <= 0)
        //    {
        //        rs.OrdeQty = rs.OrdeQty + OffsetLockQtt;//预约总数量=预约总数量+校正值（不影响外协外购的预约数量）
        //        rs.RecvQty = rs.RecvQty + OffsetLockQtt;//实际在库数量=实际在库数量+校正值（不影响外协外购的到货数量）
        //        reserveRepository.Update(rs);
        //    }
        //    //1.2.2.2 锁存了无规格预约
        //    else
        //    {
        //        //1.2.2.2.1 如果对无规格要求的锁存修改值<此无规格物料的有规格锁存，说明是不合法的
        //        if (lockQttWithoutSpec > rs.OrdeQty + OffsetLockQtt)
        //        {
        //            throw new Exception(target.MaterialID + "：锁存数量下限不能低于其有规格锁存的部分！");
        //        }
        //        rs.OrdeQty = rs.OrdeQty + OffsetLockQtt;//预约总数量=预约总数量+校正值（不影响外协外购的预约数量）
        //        rs.RecvQty = rs.RecvQty + OffsetLockQtt;//实际在库数量=实际在库数量+校正值（不影响外协外购的到货数量）
        //        reserveRepository.Update(rs);
        //    }

        //}
        #endregion
        #region 废弃：无规格品的add update合并为 NormalReserveWithoutSpec
        ///// <summary>
        ///// 正常无规格品预约：新增
        ///// C:梁龙飞
        ///// </summary>
        ///// <param name="target"></param>
        ///// <returns></returns>
        //public bool AddNormalReserveWithoutSpec(VM_LockReserveShow target)
        //{

        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// 正常无规格品预约：修改
        ///// 下列情况无法修改：正常无规格锁存中的有规格要求的部分
        ///// </summary>
        ///// <param name="target"></param>
        ///// <returns></returns>
        //public bool UpdateNormalReserveWithoutSpec(VM_LockReserveShow target)
        //{
        //    Reserve rs=reserveRepository.First(a=>a.ClnOdrID==target.ClientOrderID&&
        //        a.ClnOdrDtl==target.OrderDetail&&
        //        a.DelFlag!=Constant.CONST_FIELD.DELETED&&
        //        a.EffeFlag!=Constant.CONST_FIELD.UN_EFFECTIVE);
        //    if (rs==null)
        //    {
        //        throw new Exception(target.MaterialID+"不存在预约");               
        //    }

        //    MaterialDecompose mtD = materialDecomposeRepository.First(a => a.ClientOrderID == target.ClientOrderID &&
        //            a.ClientOrderDetail == target.OrderDetail &&
        //            a.ProductsPartsID == target.MaterialID &&
        //            a.DelFlag != Constant.CONST_FIELD.DELETED &&
        //        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

        //    //新锁值偏差=新锁值-原锁值
        //    decimal offsetLockedNum = target.OrderQuantity - mtD.NormalLockQuantity;

        //    //1.更新预约表:预约总数量
        //    rs.OrdeQty = rs.OrdeQty + offsetLockedNum;
        //    reserveRepository.Update(rs);

        //    //2.更新仓库表:被预约数量、物料需求量、可用在库数量
        //    Material mt = materialRepository.First(a => a.WhID == target.WhID &&
        //        a.PdtID == target.MaterialID &&
        //        a.DelFlag != Constant.CONST_FIELD.DELETED &&
        //    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
        //    mt.AlctQty = mt.AlctQty + offsetLockedNum;
        //    mt.RequiteQty = mt.RequiteQty + offsetLockedNum;
        //    mt.UseableQty = mt.UseableQty + offsetLockedNum;
        //    materialRepository.Update(mt);

        //    //3.更新物料分解表：正常品库存数量
        //    mtD.NormalLockQuantity = mtD.NormalLockQuantity + offsetLockedNum;
        //    materialDecomposeRepository.Update(mtD);
        //    return true;

        //}
        #endregion

        /// <summary>
        /// 正常无规格品预约
        /// 自动判断是否是新增、修改（不存在删除）
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool NormalReserveWithoutSpec(VM_LockReserveShow target)
        {
            //检测是否有有规格品的存在
            try
            {
                //已存在单配锁
                if (IsAbnmlReserveExist(target.ClientOrderID, target.OrderDetail, target.MaterialID))
                {
                    throw new Exception("禁止操作：试图对" + target.MaterialID + "进行无规格锁存时，发现已锁存了单配品！");
                }
                //可锁数量错误
                if (target.TotAvailable - target.OrderQuantity < 0)
                {
                    throw new Exception("禁止操作：" + target.MaterialID + "，锁存数量>可锁数量。");
                }
                decimal OffsetLockQtt = target.OrderQuantity;//锁存校正值

                Reserve rs = reserveRepository.FirstOrDefault(a => a.ClnOdrID == target.ClientOrderID &&
                  a.ClnOdrDtl == target.OrderDetail &&
                  a.PdtID == target.MaterialID &&
                  a.DelFlag != Constant.CONST_FIELD.DELETED &&
                  a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

                //1.1不存在预约：插入预约
                if (rs == null)
                {
                    rs = new Reserve()
                    {
                        WhID = target.WhID,
                        ClnOdrID = target.ClientOrderID,
                        ClnOdrDtl = target.OrderDetail,
                        OrdPdtID = target.ProductID,
                        PdtID = target.MaterialID,
                        OrdeBthDtailListID = 0,//无规格锁，没有详细
                        PdtSpec = target.Specification,//规格要求
                        OrdeQty = target.OrderQuantity,//预约总数量=客户订单对此零件需求数量合计-让步品锁存数量,初始化为锁存数量
                        RecvQty = target.OrderQuantity//订单对此产品所需要的总数量中在库的数量部分，初始化为锁存数量
                        //...
                    };
                    reserveRepository.Add(rs);
                }
                //1.2存在预约：更新仓库预约信息
                else
                {
                    OffsetLockQtt = target.OrderQuantity - rs.OrdeQty;//锁存校正值

                    //1.2.1此正常无规格要求物料预约了有规格要求的物料
                    if (rs.OrdeBthDtailListID != 0)
                    {
                        decimal lockQttWithoutSpec = reserveDetailRepository.GetQuantityByDetailID(rs.OrdeBthDtailListID);
                        
                        ////1.2.1.1 锁存的有规格要求的物料数量大于0，且修改的锁存量小于这个值[旧版本：对应无规格与有规格锁兼容形式]
                        //if (lockQttWithoutSpec >= 0 && lockQttWithoutSpec > rs.OrdeQty + OffsetLockQtt)
                        //{
                        //    throw new Exception(target.MaterialID + "：合计锁存数量下限不能低于其有规格锁存的部分！");
                        //}
                        //1.2.1.1 锁存的有规格要求的物料数量大于0，且修改的锁存量小于这个值[旧版本：对应无规格与有规格锁兼容形式]
                        if (lockQttWithoutSpec > 0)
                        {
                            throw new Exception("禁止操作：对零件"+target.MaterialID + "进行无规格锁存时发现已锁有有规格型号！");
                        }
                    }
                    rs.OrdeQty = rs.OrdeQty + OffsetLockQtt;//预约总数量=预约总数量+校正值（不影响外协外购的预约数量）
                    rs.RecvQty = rs.RecvQty + OffsetLockQtt;//实际在库数量=实际在库数量+校正值（不影响外协外购的到货数量）
                    reserveRepository.Update(rs);
                }


                //2.-----仓库表：更新零件的数量信息
                Material mat = materialRepository.First(a => a.WhID == target.WhID && a.PdtID == target.MaterialID);
                if (mat == null)
                {
                    throw new Exception("编号为：" + target.MaterialID + " 的零件（产品）在仓库表中没有登记……");
                }
                mat.AlctQty = mat.AlctQty + OffsetLockQtt;//被预约数量
                mat.RequiteQty = mat.RequiteQty + OffsetLockQtt;//所有计划单对此物料的需求总量（不包括单配）
                mat.UseableQty = mat.UseableQty - OffsetLockQtt;//可用在库数量
                materialRepository.Update(mat);

                //3.更新物料分解表中正常锁存数量
                MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                    a.ClientOrderDetail == target.OrderDetail &&
                    a.ProductsPartsID == target.MaterialID &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                mtD.NormalLockQuantity = mtD.NormalLockQuantity + OffsetLockQtt;
                //锁存数量是否超过需求数量
                if (mtD.AbnormalLockQuantity + mtD.NormalLockQuantity > mtD.DemondQuantity)
                {
                    throw new Exception("拒绝服务： 零件：" + target.MaterialID + "锁存的总数量超过需求数量。");
                }
                materialDecomposeRepository.Update(mtD);
                materialDecomposeRepository.RelationUpdate(mtD, target.OrderQuantity);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
       
        /// <summary>
        /// 正常有规格品预约：Add
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool AddNormalReserveWithSpec(VM_LockReserveShow target)
        {
           
            try
            {
                //已存在无规格预约
                if (IsNmlRsvWithoutSpecExist(target.ClientOrderID, target.OrderDetail, target.MaterialID))
                {
                    throw new Exception("禁止操作：试图对" + target.MaterialID + "进行有规格锁存时，发现已锁存了无规格锁存！");
                }
                //已存在单配锁
                if (IsAbnmlReserveExist(target.ClientOrderID, target.OrderDetail, target.MaterialID))
                {
                    throw new Exception("禁止操作：试图对" + target.MaterialID + "进行有规格锁存时，发现已锁存了单配品！");
                }
                //可锁数量错误
                if (target.TotAvailable - target.OrderQuantity < 0)
                {
                    throw new Exception("禁止操作：批次号：" + target.BthID + "，锁存数量>可锁数量。");
                }

                //1.-----仓库表：更新零件的数量信息
                Material mat = materialRepository.First(a => a.WhID == target.WhID && a.PdtID == target.MaterialID);
                if (mat == null)
                {
#if LLF_AUTODATA
                    return LLF_AddTestMaterial(target);//【自动化测试数据逆流生成：仓库表零件的统计信息】
#endif
                    throw new Exception("编号为：" + target.MaterialID + " 的零件（产品）在仓库表中没有登记……");
                }
                mat.AlctQty = mat.AlctQty + target.OrderQuantity;//被预约数量
                mat.RequiteQty = mat.RequiteQty + target.OrderQuantity;//所有计划单对此物料的需求总量（不包括单配）：此时只更新锁存量部分
                mat.UseableQty = mat.UseableQty - target.OrderQuantity;//可用在库数量             
                materialRepository.Update(mat);

                //2.物料分解信息表：Update 正常锁存品数量
                MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                        a.ClientOrderDetail == target.OrderDetail &&
                        a.ProductsPartsID == target.MaterialID &&
                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                mtD.NormalLockQuantity = mtD.NormalLockQuantity + target.OrderQuantity;
                //锁存数量是否超过需求数量
                if (mtD.AbnormalLockQuantity + mtD.NormalLockQuantity > mtD.DemondQuantity)
                {
                    throw new Exception("拒绝服务： 零件：" + target.MaterialID + "锁存的总数量超过需求数量。");
                }
                materialDecomposeRepository.Update(mtD);
                materialDecomposeRepository.RelationUpdate(mtD, target.OrderQuantity);


                //3.-----仓库预约表：不存在？插入：跳过
                Reserve whs = new Reserve()
                {
                    WhID = target.WhID,
                    ClnOdrID = target.ClientOrderID,
                    ClnOdrDtl = target.OrderDetail,
                    OrdPdtID = target.ProductID,
                    PdtID = target.MaterialID,
                    OrdeBthDtailListID = 0,//无规格锁，没有详细
                    PdtSpec = target.Specification,//规格要求
                    OrdeQty = target.OrderQuantity,//预约总数量=客户订单对此零件需求数量合计-让步品锁存数量,初始化为锁存数量
                    RecvQty = target.OrderQuantity//订单对此产品所需要的总数量中在库的数量部分，初始化为锁存数量
                    //...
                };
                IEnumerable<Reserve> rsList = reserveRepository.GetReserveByKeys(whs);
                //如果未预约:预约
                if (rsList == null || rsList.Count() == 0)
                {          
                    //生成新的预约详细单号
                    whs.OrdeBthDtailListID = reserveDetailRepository.GetMaxBthDetailCode() + 1;
                    reserveRepository.Add(whs);
                }
                    //已预约:更新预约数量
                else if (rsList.Count() == 1)
                {
                    
                    whs = rsList.First();
                    whs.OrdeQty = whs.OrdeQty + target.OrderQuantity;
                    whs.RecvQty = whs.RecvQty + target.OrderQuantity;
                    reserveRepository.Update(whs);
                    //whs.OrdeBthDtailListID = rsList.First().OrdeBthDtailListID;
                }
                else
                {
                    //存在多条预约信息，说明数据异常：禁止锁存，抛出异常
                    throw new Exception(target.ClientOrderID + " " + target.OrderDetail + " " + target.MaterialID + " 存在多条预约信息！");
                }

                //4.仓库预约详细表：插入一条新的有规格正常品预约详细
                ReserveDetail rsd = new ReserveDetail()
                {
                    OrdeBthDtailListID = whs.OrdeBthDtailListID,
                    BthID = target.BthID,
                    OrderQty = target.OrderQuantity
                };
                reserveDetailRepository.Add(rsd);

                //5.批次别库存表：更新批次别库存中的数量信息
                BthStockList bsl = bthStockListRepository.First(a => a.BthID == target.BthID && a.WhID == target.WhID && a.PdtID == target.MaterialID);
                if (bsl == null)
                {
                    throw new Exception("编号为：" + target.MaterialID + "的零件（产品）在批次别库存表中没有登记……");
                }
                bsl.OrdeQty = bsl.OrdeQty + target.OrderQuantity;//被预约总数增加
                bthStockListRepository.Update(bsl);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    
        /// <summary>
        /// 正常有规格品预约：｛Update,Delete}
        /// 只更新一条预约批次
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool UpdateNormalReserveWithSpec(VM_LockReserveShow target)
        {
          
            //取得仓库预约信息->预约详细单号
            Reserve rs = reserveRepository.FirstOrDefault(a => a.ClnOdrID == target.ClientOrderID &&
               a.ClnOdrDtl == target.OrderDetail &&
               a.PdtID==target.MaterialID&&
               a.DelFlag != Constant.CONST_FIELD.DELETED &&
               a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
            if (rs == null)
            {
                throw new Exception("试图更新零件："+target.MaterialID + "的仓库预约，但是不存在仓库预约信息。");
            }
           
            //1.更新详细预约表
            ReserveDetail rsd = reserveDetailRepository.FirstOrDefault(a => a.OrdeBthDtailListID == rs.OrdeBthDtailListID &&
                a.BthID==target.BthID&&
                a.DelFlag != Constant.CONST_FIELD.DELETED &&
                a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

            //新锁值偏差=新锁值-原锁值
            decimal offsetLockedNum = target.OrderQuantity - rsd.OrderQty;

            //锁存数量超过可用数量
            if (target.TotAvailable - offsetLockedNum < 0)
            {
                throw new Exception("拒绝服务： 批次号：" + target.BthID + "，锁存数量>可锁数量。");
            }

            if (target.OrderQuantity == 0)
            {
                reserveDetailRepository.Delete(rsd);
            }
            else
            {
                rsd.OrderQty = target.OrderQuantity;
                reserveDetailRepository.Update(rsd);
            }

            //2.更新预约表:预约总数量、实际在库数量
            rs.OrdeQty = rs.OrdeQty + offsetLockedNum;
            rs.RecvQty = rs.RecvQty + offsetLockedNum;
            reserveRepository.Update(rs);

            //3.更新仓库表:被预约数量、物料需求量、可用在库数量
            Material mt = materialRepository.FirstOrDefault(a => a.WhID == target.WhID &&
                a.PdtID == target.MaterialID &&
                a.DelFlag != Constant.CONST_FIELD.DELETED &&
            a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
            mt.AlctQty = mt.AlctQty + offsetLockedNum;
            mt.RequiteQty = mt.RequiteQty + offsetLockedNum;
            mt.UseableQty = mt.UseableQty - offsetLockedNum;
            materialRepository.Update(mt);
            //4.更新批次别库存表
            BthStockList bsl = bthStockListRepository.First(a => a.BthID == target.BthID && a.WhID == target.WhID && a.PdtID == target.MaterialID);
            if (bsl == null)
            {
                throw new Exception("编号为：" + target.MaterialID + "的零件（产品）在批次别库存表中没有登记……");
            }
            bsl.OrdeQty = bsl.OrdeQty + offsetLockedNum;//被预约总数增加
            bthStockListRepository.Update(bsl);
            //5.更新物料分解表：正常品库存数量
            //取得物料分解信息
            MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                    a.ClientOrderDetail == target.OrderDetail &&
                    a.ProductsPartsID == target.MaterialID &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);        
            mtD.NormalLockQuantity = mtD.NormalLockQuantity + offsetLockedNum;
            //锁存数量是否超过需求数量
            if (mtD.AbnormalLockQuantity + mtD.NormalLockQuantity > mtD.DemondQuantity)
            {
                throw new Exception("拒绝服务： 零件：" + target.MaterialID + "锁存的总数量超过需求数量。");
            }
            materialDecomposeRepository.Update(mtD);
            materialDecomposeRepository.RelationUpdate(mtD, offsetLockedNum);
            return true;
        }

        /// <summary>
        /// 让步品锁存：Add
        /// C:梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool AddAbnormalReserve(VM_LockReserveShow target)
        {
            try
            {
                if (target.TotAvailable - target.OrderQuantity < 0)
                {
                    throw new Exception("拒绝服务：批次号：" + target.BthID + "，锁存数量>可锁数量。");
                }
                //已存在正常预约
                if (IsNmlRsvWithoutSpecExist(target.ClientOrderID, target.OrderDetail, target.MaterialID) 
                    || IsNmlRsvWithSpecExist(target.ClientOrderID, target.OrderDetail, target.MaterialID))
                {
                    throw new Exception("禁止操作：试图对" + target.MaterialID + "进行单配锁存时，发现已锁存正常品！");
                }
                //1.-----让步仓库表：更新零件的数量信息
                GiMaterial gimat = giMaterialRepository.FirstOrDefault(a =>
                    a.WareHouseID == target.WhID &&
                    a.ProductID == target.MaterialID &&
                    a.BatchID == target.BthID &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
                    a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                if (gimat == null)
                {
#if LLF_AUTODATA
                    return LLF_AddGiMat(target);//【自动化测试数据逆流生成：让步仓库表零件的统计信息】
#endif
                    throw new Exception("编号为：" + target.MaterialID + " 的零件（产品）在让仓库表中没有登记……");
                }
                gimat.AlctQuantity = gimat.AlctQuantity + target.OrderQuantity;//被预约数量
                gimat.UserableQuantity = gimat.UserableQuantity - target.OrderQuantity;//可用在库数量             
                giMaterialRepository.Update(gimat);

                //2.物料分解信息表：Update：让步锁存数量
                MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                        a.ClientOrderDetail == target.OrderDetail &&
                        a.ProductsPartsID == target.MaterialID &&
                        a.DelFlag != Constant.CONST_FIELD.DELETED &&
                        a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
                mtD.AbnormalLockQuantity = mtD.AbnormalLockQuantity + target.OrderQuantity;
                //锁存数量是否超过需求数量
                if (mtD.AbnormalLockQuantity + mtD.NormalLockQuantity > mtD.DemondQuantity)
                {
                    throw new Exception("拒绝服务： 零件：" + target.MaterialID + "锁存的总数量超过需求数量。");
                }
                materialDecomposeRepository.Update(mtD);
                materialDecomposeRepository.RelationUpdate(mtD, target.OrderQuantity);

                //3.-----让步仓库预约表：Add
                GiReserve whs = new GiReserve()
                {
                    WareHouseID=target.WhID,
                    PrhaOrderID=target.ClientOrderID,
                    ClientOrderDetail=target.OrderDetail,
                    OrdProductID=target.ProductID,
                    ProductID=target.MaterialID,
                    ProductSpec=target.Specification,
                    BatchID=target.BthID,
                    OrderQuantity=target.OrderQuantity,//让步品不存在自产外协外购，一直为锁存量
                    //---

                };
                giReserveRepository.Add(whs);

                //4.批次别库存表：更新批次别库存中的数量信息
                BthStockList bsl = bthStockListRepository.First(a => a.BthID == target.BthID && a.WhID == target.WhID && a.PdtID == target.MaterialID);
                if (bsl == null)
                {
                    throw new Exception("编号为：" + target.MaterialID + "的零件（产品）在批次别库存表中没有登记……");
                }
                bsl.OrdeQty = bsl.OrdeQty + target.OrderQuantity;//被预约总数增加
                bthStockListRepository.Update(bsl);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 让步品锁存：{ Update,Delete }
        /// [没有做无效操作验证]
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool UpdateAbnormalReserve(VM_LockReserveShow target)
        {
            
            //1.更新让步预约表:预约总数量
            GiReserve girs = giReserveRepository.FirstOrDefault(a =>
                a.WareHouseID==target.WhID.Trim()&&
                a.PrhaOrderID == target.ClientOrderID.Trim() &&
                a.ClientOrderDetail == target.OrderDetail.Trim() &&
                a.ProductID == target.MaterialID.Trim() &&
                a.BatchID==target.BthID.Trim()&&
                a.DelFlag != Constant.CONST_FIELD.DELETED &&
                a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
            if (girs == null)
            {
                throw new Exception(target.MaterialID + "不存在预约");
            }

            //偏差值=新锁值-旧锁值
            decimal offsetLockedNum = target.OrderQuantity - girs.OrderQuantity;

            //锁存数量超过可用数量
            if (target.TotAvailable - offsetLockedNum < 0)
            {
                throw new Exception("拒绝服务： 批次号：" + target.BthID + "，锁存数量>可锁数量。");
            }

            if (target.OrderQuantity == 0)
            {
                //删除预约
                giReserveRepository.Delete(girs);
            }
            else
            {
                //更新预约
                girs.OrderQuantity = girs.OrderQuantity + offsetLockedNum;
                giReserveRepository.Update(girs);
            }

            //2.更新让步仓库表:被预约数量、物料需求量、可用在库数量
            GiMaterial gimt = giMaterialRepository.FirstOrDefault(a =>
                a.WareHouseID == target.WhID &&
                a.ProductID == target.MaterialID &&
                a.BatchID == target.BthID &&
                a.DelFlag != Constant.CONST_FIELD.DELETED &&
                a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);
            if (gimt == null)
            {
#if LLF_AUTODATA
                return LLF_AddGiMat(target);//【自动化测试数据逆流生成：让步仓库表零件的统计信息】
#endif
                throw new Exception("编号为：" + target.MaterialID + " 的零件（产品）在让仓库表中没有登记……");
            }
            
            gimt.AlctQuantity = gimt.AlctQuantity + offsetLockedNum;
            gimt.UserableQuantity = gimt.UserableQuantity - offsetLockedNum;
            giMaterialRepository.Update(gimt);
            //3.更新批次别库存表
            BthStockList bsl = bthStockListRepository.First(a => a.BthID == target.BthID && a.WhID == target.WhID && a.PdtID == target.MaterialID);
            if (bsl == null)
            {
                throw new Exception("编号为：" + target.MaterialID + "的零件（产品）在批次别库存表中没有登记……");
            }
            bsl.OrdeQty = bsl.OrdeQty + offsetLockedNum;//被预约总数改变
            bthStockListRepository.Update(bsl);

            //4.更新物料分解表：正常品库存数量
            //取得物料分解信息
            MaterialDecompose mtD = materialDecomposeRepository.FirstOrDefault(a => a.ClientOrderID == target.ClientOrderID &&
                    a.ClientOrderDetail == target.OrderDetail &&
                    a.ProductsPartsID == target.MaterialID &&
                    a.DelFlag != Constant.CONST_FIELD.DELETED &&
                a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE);

            mtD.AbnormalLockQuantity = mtD.AbnormalLockQuantity + offsetLockedNum;
            //锁存数量是否超过需求数量
            if (mtD.AbnormalLockQuantity+mtD.NormalLockQuantity>mtD.DemondQuantity)
            {
                throw new Exception("拒绝服务： 零件：" + target.MaterialID + "锁存的总数量超过需求数量。");
            }
            materialDecomposeRepository.Update(mtD);
            materialDecomposeRepository.RelationUpdate(mtD, offsetLockedNum);
            return true;
        }

        #endregion
    }
}
