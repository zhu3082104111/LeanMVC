// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：PickingMaterialServiceImp.cs
// 文件功能描述：领料单service实现类
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
using Repository;
using Model;
using Model.Produce;
using Extensions;
using Model.Store;
namespace BLL
{
    class PickingMaterialServiceImp:AbstractService,IPickingMaterialService
    {
        /// <summary>
        /// 
        /// </summary>
        private IPickingListRepository pickingListRepository;
        /// <summary>
        /// 
        /// </summary>
        private IProcessTranslateCardRepository translateCardRepository;

        /// <summary>
        /// 
        /// </summary>
        private IProdCompositionRepository prodCompositionRepository;
        /// <summary>
        /// 
        /// </summary>
        private IReserveRepository reserveRepository;
        /// <summary>
        /// 
        /// </summary>
        private IReserveDetailRepository reserveDetailRepository;
        /// <summary>
        /// 
        /// </summary>
        private IAssemBillRepository assemBillRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingListRepository"></param>
        public PickingMaterialServiceImp(IAssemBillRepository assemBillRepository,IReserveDetailRepository reserveDetailRepository,IReserveRepository reserveRepository,IProdCompositionRepository prodCompositionRepository,IProcessTranslateCardRepository translateCardRepository,IPickingListRepository pickingListRepository) 
        {
            this.pickingListRepository = pickingListRepository;
            this.translateCardRepository = translateCardRepository;
            this.prodCompositionRepository = prodCompositionRepository;
            this.reserveRepository = reserveRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.assemBillRepository = assemBillRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingList"></param>
        /// <param name="paging"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<ProduceMaterRequest> GetMaterials(VM_ProduceMaterRequestForSearch pickingList, Extensions.Paging paging, out int total)
        {
            total = 1;
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingList"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProduceMaterRequestForTableShow> GetMaterialsForSearch(VM_ProduceMaterRequestForSearch pickingList, Paging paging)
        {

            return pickingListRepository.GetPickingListsBySearch(pickingList, paging);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingList"></param>
        /// <returns></returns>
        public bool CreatePickingList(ProduceMaterRequest pickingList)
        {
           return pickingListRepository.Add(pickingList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingList"></param>
        /// <returns></returns>
        public bool UpdatePickingList(ProduceMaterRequest pickingList)
        {
            return pickingListRepository.Update(pickingList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pickingList"></param>
        /// <returns></returns>
        public bool DeletePickingList(ProduceMaterRequest pickingList)
        {
            return pickingListRepository.Delete(pickingList);
        }


        /// <summary>
        /// 代东泽 20131225
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetPickingBillDataByTranslateCard(ProcessTranslateCard card, IList<Model.Store.VM_Reserve> list)
        {
            //找出流转卡
            ProcessTranslateCard translateCard = translateCardRepository.Find(card);
            card = translateCard;
            //找出流转卡对应的所有客户订单
            IList<VM_CustomTranslateInfoForDetaiShow> adList=translateCardRepository.GetCustomOrdersForTranslateCard(card).ToList();


            //取得该产品对应的所有零件信息，构成数量,一般只有一个
            var prodPartList = prodCompositionRepository.GetListByCondition(n => n.ParItemId.Equals(translateCard.ExportID));
            prodPartList = prodPartList.ToList();

            //找出该流转卡是否已经领过料
            IEnumerable<ProduceMaterDetail> details = pickingListRepository.GetDetailListByCondition(n => n.ProcDelivID.Equals(card.ProcDelivID));
            if (details.Count() > 0) //如果已经领过料了
            {

            }
            else//还没有领过料，需要找出改流转卡需要领哪些料 
            {
            
            }
            //如果领过，判断数量是否已经领够，取出来每个零件请领数量和。这是总共已经领数量
            var query = from a in details
                        group a by new { a.MaterialID,a.CustomerOrderNum,a.CustomerOrderDetails,a.BthID,a.AppoQty } into _a
                        select new { _a.Key.AppoQty,_a.Key.MaterialID, _a.Key.CustomerOrderNum, _a.Key.CustomerOrderDetails, _a.Key.BthID, count = _a.Sum(n => n.AppoQty) };
            query = query.ToList();

            //循环计算出  每个  零件最多  还可以领的数量   无论是否有批次号
           /* foreach (var o in prodPartList)//一般只有一个
            {
                o.ConstQty = o.ConstQty * translateCard.NedProcQty;//最大领料数,最大领料数不是看这个，是看锁库锁的数量
                //判断零件料是否有批次，如果没有批次，则去物料分解表中查询该零件锁的数量，如果有批次，则不用修改，因为预约详细表中批次零件的预约数量就是最大能领数量 
                var queryPartList = query.Where(n => n.MaterialID.Equals(o.SubItemId));//无批次号的零件，
                decimal thisPartRealCount=queryPartList.Sum(n=>n.count);
                foreach (var part in queryPartList) 
                {
                    
                }
                decimal lostCount = o.ConstQty - thisPartRealCount;//剩余可以领料数   该零件此种批次最大可以请领总数 
                o.ConstQty=lostCount;//此时  把此种零件此种批次剩余可以领的最大料数 存入 到构成数量中

                //如果领过料，而且是有批次的，那么要去 领料单详细中找到该批次的料，然后用 该中料的预约数量减去 领料单详细表中该批次料的 请领数量/实领数量，得出这次最大能领数量
                
                //如果是没有领过料，则 计算出 该订单，该零件 的全部数量，因为 有可能仓库发的是有批次号的。
            }*/
            //如果每个零件还可以领的数量都是0，那么意味着，这个卡不能在继续领料了

            //取出 加工流转卡对应的所有客户订单号中的  每个零件的所有批次的信息

            decimal allCount = 0M;//统计标识该流转卡是否还能领料
            foreach(var order in adList){//adlist  是一个加工流转卡 对应的所有订单号
                Reserve r = new Reserve();
                r.ClnOdrID = order.CustomerOrderNum;
                r.ClnOdrDtl = order.CustomerOrderDetails;
                r.OrdPdtID = translateCard.ExportID;

                var partBthList = reserveRepository.GetReserveDetailsList(r);//取出来 这个订单详细所对应的零件 仓库预约详细信息表，无批次的只有一个，有批次的可能有多个
                partBthList = partBthList.ToList();
                //循环当前客户订单ad 下的 这个产品的每个零件
                foreach (var pl in prodPartList) //该产品对应的所有零件信息，构成数量 一般  只有一个零件（料）。SubItemId 是 零件构成表中的 构成 子零件id ，等于预约详细表中的零件productid
                {

                    // 仓库预约详细 ，pl 零件  对应的的   批次零件 列表
                    var currentPartList = partBthList.Where(n=>n.ProductID.Equals(pl.SubItemId));//找出同一零件的 所有批次 记录. 。如果有批次那就是可能多条，如果每批次数据只有一条
                    if (currentPartList.Count() > 0) //判断 有没有 该零件-批次信息
                    {
                        currentPartList = currentPartList.ToList();
                        if (currentPartList.Count() == 1 && currentPartList.First().OrdeBthDtailListID == 0)//无批次的 
                        {//是无批次的
                            var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails));//无批次号的零件，
                            decimal thisPartRealCount = queryPartList.Sum(n => n.count);//此时此种存的是 仓库实际发货的 该订单此种零件的所有数量

                            VM_Reserve firstVM = currentPartList.ElementAt(0);
                            //firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity - thisPartRealCount;//没有批次的 领料单开具数量
                            firstVM.LastPickingCount = 0M;
                            firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity;//不用减去仓库实际发的数量
                           /* if (firstVM.AlctQty <= 0M) 
                            {//如果仓库发完了，则表示不能再领了
                                return false;
                            }*/
                            allCount += firstVM.AlctQty;
                            firstVM.ClnOdrID=order.CustomerOrderNum;
                            firstVM.ClnOdrDtl = order.CustomerOrderDetails;
                            list.Add(firstVM);
                        }
                        else 
                        { //锁的是有批次的零件
                            decimal pthCountSize = currentPartList.Sum(n => n.AlctQty);//批次零件的预约数量 ，所有批次零件预约数量的和
                            VM_Reserve firstVM = currentPartList.ElementAt(0);
                            decimal countSize = firstVM.CurrentQuantity;//总的该零件实际在库数量  

                            decimal rst = countSize - pthCountSize;//这是剩下的没有批次的零件的预约数量
                            if (rst > 0M)//如果有批次的的总数量不够,此种情况一般不会出现，因为锁料，三种情况是互斥的
                            {
                                VM_Reserve vm = new VM_Reserve();
                                vm.CurrentQuantity = countSize;//实际在库数量 -
                                /*--
                                 *--
                                 */
                                vm.AlctQty = rst;// rst 没有批次的记录   的   预约数量，就是本个没有批次的零件最大可以领用的数量
                                vm.ClnOdrDtl = firstVM.ClnOdrDtl;
                                vm.ClnOdrID = firstVM.ClnOdrID;
                                //vm.PickiingOrderQty = firstVM.PickiingOrderQuantity - currentPartList.Sum(n => n.PickiingOrderQty);//没有批次的 领料单开具数量
                                vm.ProductID = firstVM.ProductID;
                                vm.ProductSpec = firstVM.ProductSpec;
                                vm.WhID = vm.WhID;
                                vm.BatchID = "";
                                list.Add(vm);
                            }

                            
                            foreach (var o in currentPartList) //循环该种零件的所有批次
                            {
                                //var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails) && n.BthID.Equals(o.BatchID));//该次号的零件
                                //decimal bthPartNextCount=queryPartList.Sum(n=>n.count);
                                //此中要计算出 该 订单的 该批次 的 最大可以请领的数量、领料单开具数量，无批次的最大可以请领数量、领料单开具的数量
                                
                                
                                o.AlctQty = o.AlctQty - o.PickiingOrderQty;//此种批次 零件剩余可以领的最大数量
                                o.LastPickingCount = 0M;
                                allCount += firstVM.AlctQty;
                                //o.AlctQty = bthPartNextCount;//此种批次 零件可以领的最大数量
                                o.ClnOdrID = order.CustomerOrderNum;
                                o.ClnOdrDtl = order.CustomerOrderDetails;
                                list.Add(o);
                            }
                        
                        }

                       /* //VM_ProduceMaterRequestForDetailShow obj=new VM_ProduceMaterRequestForDetailShow();
                        decimal pthCountSize = currentPartList.Sum(n => n.AlctQty);//批次零件的预约数量 ，所有批次零件预约数量的和
                        VM_Reserve firstVM = currentPartList.ElementAt(0);
                        decimal countSize = firstVM.CurrentQuantity;//总的该零件实际在库数量  

                        decimal rst = countSize-pthCountSize ;//这是剩下的没有批次的零件的预约数量
                        if (rst > 0M)//如果有批次的的总数量不够
                        {
                            VM_Reserve vm = new VM_Reserve();
                            vm.CurrentQuantity = countSize;//实际在库数量 -
                            vm.AlctQty = rst;// rst 没有批次的记录   的   预约数量，就是本个没有批次的零件最大可以领用的数量
                            vm.ClnOdrDtl = firstVM.ClnOdrDtl;
                            vm.ClnOdrID = firstVM.ClnOdrID;
                            //vm.PickiingOrderQty = firstVM.PickiingOrderQuantity - currentPartList.Sum(n => n.PickiingOrderQty);//没有批次的 领料单开具数量
                            vm.ProductID = firstVM.ProductID;
                            vm.ProductSpec = firstVM.ProductSpec;
                            vm.WhID = vm.WhID;
                            vm.BatchID = "";
                            vm.MaxPickingCount = pl.ConstQty;
                            list.Add(vm);
                        }
                        foreach (var o in currentPartList) //循环该种零件的所有批次
                        {
                            //此中要计算出 该 订单的 该批次 的 最大可以请领的数量、领料单开具数量，无批次的最大可以请领数量、领料单开具的数量

                            //IEnumerable<ProdComposition> pcList = prodPartList.Where(n => n.SubItemId.Equals(o.ProductID));
                            //if (pcList.Count() == 0)
                            //{
                            //没有找到这个零件
                            // }
                            //  else
                            //  {
                            // ProdComposition pc = pcList.ElementAt(0);
                            o.MaxPickingCount = pl.ConstQty;//此种批次 零件可以领的最大数量
                            //o.MaxPickingCount = pc.ConstQty;//把最大本种类型的 零件  能领料数量赋值   （这种类型的总的能领数量数量）
                            //   }

                            list.Add(o);
                        }*/
                    }

                }
                
            }
            if (allCount <= 0M)
            {
                return false;
            }
            else 
            {
                return true;
            }
                //如果没有领够，则计算出每个零件还可以领的数量
                //如果领够，则不允许领料，return false
           // return false;
            //如果没有领过，则查询出相关数据放入list中，可以直接领料
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmq"></param>
        /// <param name="pmdList"></param>
        public void SavePickingMaterial(ProduceMaterRequest pmq, IList<ProduceMaterDetail> pmdList, IList<Reserve> reserveList, IList<ReserveDetail> reserveDetailList)
        {
            pickingListRepository.Add(pmq);
            foreach (var obj in pmdList)
            {
                pickingListRepository.AddDetail(obj);
            }
            foreach (var obj in reserveList) 
            {
                Reserve temp=reserveRepository.Find(obj);
                obj.PickOrdeQty = obj.PickOrdeQty + temp.PickOrdeQty;//此处逻辑判断  有待确定
                reserveRepository.UpdateNotNullColumn(obj);

            }
            foreach (var obj in reserveDetailList)
            {
                ReserveDetail temp = reserveDetailRepository.Find(obj);
                obj.PickOrdeQty = obj.PickOrdeQty + temp.PickOrdeQty;//此处逻辑判断  有待确定  到底是加上，还是减去
                reserveDetailRepository.UpdateNotNullColumn(obj);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsCanCreatePickingBill(string id, string type)
        {
            decimal allCount = 0M;//统计标识该卡是否还能领料
            if (Constant.PickingBillComeFrom.CIRCULATE_CARD.Equals(type)) {
                ProcessTranslateCard card = new ProcessTranslateCard();
                card.ProcDelivID = id;
                //找出流转卡
                ProcessTranslateCard translateCard = translateCardRepository.Find(card);
                card = translateCard;
                //找出流转卡对应的所有客户订单
                IList<VM_CustomTranslateInfoForDetaiShow> adList = translateCardRepository.GetCustomOrdersForTranslateCard(card).ToList();
                //取得该产品对应的所有零件信息，构成数量,一般只有一个
                var prodPartList = prodCompositionRepository.GetListByCondition(n => n.ParItemId.Equals(translateCard.ExportID));
                prodPartList = prodPartList.ToList();
                //找出该流转卡是否已经领过料
                IEnumerable<ProduceMaterDetail> details = pickingListRepository.GetDetailListByCondition(n => n.ProcDelivID.Equals(card.ProcDelivID));
          
                //如果领过，判断数量是否已经领够，取出来每个零件请领数量和。这是总共已经领数量
                var query = from a in details
                            group a by new { a.MaterialID, a.CustomerOrderNum, a.CustomerOrderDetails, a.BthID } into _a
                            select new { _a.Key.MaterialID, _a.Key.CustomerOrderNum, _a.Key.CustomerOrderDetails, _a.Key.BthID, count = _a.Sum(n => n.AppoQty) };
                query = query.ToList();
                //如果每个零件还可以领的数量都是0，那么意味着，这个卡不能在继续领料了

                //取出 加工流转卡对应的所有客户订单号中的  每个零件的所有批次的信息
                foreach (var order in adList)
                {//adlist  是一个加工流转卡 对应的所有订单号
                    Reserve r = new Reserve();
                    r.ClnOdrID = order.CustomerOrderNum;
                    r.ClnOdrDtl = order.CustomerOrderDetails;
                    r.OrdPdtID = translateCard.ExportID;

                    var partBthList = reserveRepository.GetReserveDetailsList(r);//取出来 这个订单详细所对应的零件 仓库预约详细信息表，无批次的只有一个，有批次的可能有多个
                    partBthList = partBthList.ToList();
                    //循环当前客户订单ad 下的 这个产品的每个零件
                    foreach (var pl in prodPartList) //该产品对应的所有零件信息，构成数量 一般  只有一个零件（料）。SubItemId 是 零件构成表中的 构成 子零件id ，等于预约详细表中的零件productid
                    {
                        // 仓库预约详细 ，pl 零件  对应的的   批次零件 列表
                        var currentPartList = partBthList.Where(n => n.ProductID.Equals(pl.SubItemId));//找出同一零件的 所有批次 记录. 。如果有批次那就是可能多条，如果每批次数据只有一条
                        if (currentPartList.Count() > 0) //判断 有没有 该零件-批次信息
                        {
                            currentPartList = currentPartList.ToList();
                            if (currentPartList.Count() == 1 && currentPartList.First().OrdeBthDtailListID == 0)//无批次的 
                            {//是无批次的
                                VM_Reserve firstVM = currentPartList.ElementAt(0);
                                //firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity - thisPartRealCount;//没有批次的 领料单开具数量
                                firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity;//不用减去仓库实际发的数量
                                /* if (firstVM.AlctQty <= 0M) 
                                 {//如果仓库发完了，则表示不能再领了
                                     return false;
                                 }*/
                                allCount += firstVM.AlctQty;
                            }
                            else
                            { //锁的是有批次的零件
                                foreach (var o in currentPartList) //循环该种零件的所有批次
                                {
                                    //var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails) && n.BthID.Equals(o.BatchID));//该次号的零件
                                    //decimal bthPartNextCount=queryPartList.Sum(n=>n.count);
                                    //此中要计算出 该 订单的 该批次 的 最大可以请领的数量、领料单开具数量，无批次的最大可以请领数量、领料单开具的数量
                                    o.AlctQty = o.AlctQty - o.PickiingOrderQty;//此种批次 零件剩余可以领的最大数量
                                    //allCount += firstVM.AlctQty;
                                    allCount += o.AlctQty;
                                    //o.AlctQty = bthPartNextCount;//此种批次 零件可以领的最大数量
                                 
                                }
                            }
                        }
                    }
                }
            }
            else if (Constant.PickingBillComeFrom.ASSEM_DISPATCH.Equals(type))
            {
                AssemBill card = new AssemBill();
                card.AssemBillID = id;
                //找出流转卡
                AssemBill assemBill =assemBillRepository.Find(card);
                card = assemBill;
                //找出流转卡对应的所有客户订单
                IList<VM_AssemblyDispatch> adList = assemBillRepository.GetCustomOrdersByAssemBigBill(card).ToList();

                //取得该产品对应的所有零件信息，构成数量,一般只有一个
                var prodPartList = prodCompositionRepository.GetListByCondition(n => n.ParItemId.Equals(assemBill.ProductID));
                prodPartList = prodPartList.ToList();
                //找出该流转卡是否已经领过料
                IEnumerable<ProduceMaterDetail> details = pickingListRepository.GetDetailListByCondition(n => n.AssBillID.Equals(card.AssemBillID));

                //如果领过，判断数量是否已经领够，取出来每个零件请领数量和。这是总共已经领数量
                var query = from a in details
                            group a by new { a.MaterialID, a.CustomerOrderNum, a.CustomerOrderDetails, a.BthID } into _a
                            select new { _a.Key.MaterialID, _a.Key.CustomerOrderNum, _a.Key.CustomerOrderDetails, _a.Key.BthID, count = _a.Sum(n => n.AppoQty) };
                query = query.ToList();
                //如果每个零件还可以领的数量都是0，那么意味着，这个卡不能在继续领料了

                //取出 加工流转卡对应的所有客户订单号中的  每个零件的所有批次的信息
                foreach (var order in adList)
                {//adlist  是一个加工流转卡 对应的所有订单号
                    Reserve r = new Reserve();
                    r.ClnOdrID = order.CustomerOrderNum;
                    r.ClnOdrDtl = order.CustomerOrderDetails;
                    r.OrdPdtID = assemBill.ProductID;

                    var partBthList = reserveRepository.GetReserveDetailsList(r);//取出来 这个订单详细所对应的零件 仓库预约详细信息表，无批次的只有一个，有批次的可能有多个
                    partBthList = partBthList.ToList();
                    //循环当前客户订单ad 下的 这个产品的每个零件
                    foreach (var pl in prodPartList) //该产品对应的所有零件信息，构成数量 一般  只有一个零件（料）。SubItemId 是 零件构成表中的 构成 子零件id ，等于预约详细表中的零件productid
                    {
                        // 仓库预约详细 ，pl 零件  对应的的   批次零件 列表
                        var currentPartList = partBthList.Where(n => n.ProductID.Equals(pl.SubItemId));//找出同一零件的 所有批次 记录. 。如果有批次那就是可能多条，如果每批次数据只有一条
                        if (currentPartList.Count() > 0) //判断 有没有 该零件-批次信息
                        {
                            currentPartList = currentPartList.ToList();
                            if (currentPartList.Count() == 1 && currentPartList.First().OrdeBthDtailListID == 0)//无批次的 
                            {//是无批次的
                                VM_Reserve firstVM = currentPartList.ElementAt(0);
                                //firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity - thisPartRealCount;//没有批次的 领料单开具数量
                                firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity;//不用减去仓库实际发的数量
                                /* if (firstVM.AlctQty <= 0M) 
                                 {//如果仓库发完了，则表示不能再领了
                                     return false;
                                 }*/
                                allCount += firstVM.AlctQty;
                            }
                            else
                            { //锁的是有批次的零件
                                foreach (var o in currentPartList) //循环该种零件的所有批次
                                {
                                    //var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails) && n.BthID.Equals(o.BatchID));//该次号的零件
                                    //decimal bthPartNextCount=queryPartList.Sum(n=>n.count);
                                    //此中要计算出 该 订单的 该批次 的 最大可以请领的数量、领料单开具数量，无批次的最大可以请领数量、领料单开具的数量
                                    o.AlctQty = o.AlctQty - o.PickiingOrderQty;//此种批次 零件剩余可以领的最大数量
                                    //allCount += firstVM.AlctQty;
                                    allCount += o.AlctQty;
                                    //o.AlctQty = bthPartNextCount;//此种批次 零件可以领的最大数量

                                }
                            }
                        }
                    }
                }
            }
            else if (Constant.PickingBillComeFrom.OUTASSIT_DISPATCH.Equals(type)) 
            {
                return false;
            }
            else if (Constant.PickingBillComeFrom.ASSEM_DISPATCH_REMEDY.Equals(type))
            {
                return false;
            }
            else {
                throw new Exception("");
            }


            if (allCount <= 0M)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public VM_ProduceMaterRequestForTableShow GetProduceMaterRequest(ProduceMaterRequest p)
        {
            return pickingListRepository.GetProduceMaterRequestForDetail(p);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProduceMaterDetailForDetailShow> GetProduceMaterDetailsForEdit(ProduceMaterRequest p)
        {
            return pickingListRepository.GetProduceMaterDetailsForDetail(p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmq"></param>
        /// <param name="pmdList"></param>
        /// <param name="reserveList"></param>
        /// <param name="reserveDetailList"></param>
        public void UpdatePickingList(ProduceMaterRequest pmq, IList<ProduceMaterDetail> pmdList, IList<Reserve> reserveList, IList<ReserveDetail> reserveDetailList)
        {

            //pickingListRepository.UpdaterNotNullColumn(pmq);
            foreach (var obj in pmdList)
            {
                pickingListRepository.UpdateDetail(obj);
            }
            foreach (var obj in reserveList)
            {
                Reserve temp = reserveRepository.Find(obj);
                obj.PickOrdeQty = obj.PickOrdeQty + temp.PickOrdeQty;//
                reserveRepository.UpdateNotNullColumn(obj);

            }
            foreach (var obj in reserveDetailList)
            {
                ReserveDetail temp = reserveDetailRepository.Find(obj);
                obj.PickOrdeQty = obj.PickOrdeQty + temp.PickOrdeQty;//  加上  新的与旧数据的差值
                reserveDetailRepository.UpdateNotNullColumn(obj);

            }
        }

        /// <summary>
        /// 总装大工票的开具领料单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetPickingBillDataByAssemBigBill(AssemBill bill, IList<VM_Reserve> list)
        {
          
            //找出大工票
            AssemBill assemBill = assemBillRepository.Find(bill);
            bill = assemBill;
            //找出大工票对应的所有客户订单
            IList<VM_AssemblyDispatch> adList = assemBillRepository.GetCustomOrdersByAssemBigBill(bill).ToList();

            //取得该产品对应的所有零件信息，构成数量,一般只有一个
            var prodPartList = prodCompositionRepository.GetListByCondition(n => n.ParItemId.Equals(assemBill.ProductID));
            prodPartList = prodPartList.ToList();
            //找出该大工票是否已经领过料
            IEnumerable<ProduceMaterDetail> details = pickingListRepository.GetDetailListByCondition(n => n.AssBillID.Equals(bill.AssemBillID));

            //如果领过，判断数量是否已经领够，取出来每个零件请领数量和。这是总共已经领数量
            var query = from a in details
                        group a by new { a.MaterialID, a.CustomerOrderNum, a.CustomerOrderDetails, a.BthID } into _a
                        select new { _a.Key.MaterialID, _a.Key.CustomerOrderNum, _a.Key.CustomerOrderDetails, _a.Key.BthID, count = _a.Sum(n => n.AppoQty) };
            query = query.ToList();
            //如果每个零件还可以领的数量都是0，那么意味着，这个卡不能在继续领料了
            decimal allCount = 0M;//统计标识该流转卡是否还能领料
            foreach (var order in adList)
            {//adlist  是一个加工流转卡 对应的所有订单号
                Reserve r = new Reserve();
                r.ClnOdrID = order.CustomerOrderNum;
                r.ClnOdrDtl = order.CustomerOrderDetails;
                r.OrdPdtID = assemBill.ProductID;

                var partBthList = reserveRepository.GetReserveDetailsList(r);//取出来 这个订单详细所对应的零件 仓库预约详细信息表，无批次的只有一个，有批次的可能有多个
                partBthList = partBthList.ToList();
                //循环当前客户订单ad 下的 这个产品的每个零件
                foreach (var pl in prodPartList) //该产品对应的所有零件信息，构成数量 一般  只有一个零件（料）。SubItemId 是 零件构成表中的 构成 子零件id ，等于预约详细表中的零件productid
                {
                    // 仓库预约详细 ，pl 零件  对应的的   批次零件 列表
                    var currentPartList = partBthList.Where(n => n.ProductID.Equals(pl.SubItemId));//找出同一零件的 所有批次 记录. 。如果有批次那就是可能多条，如果每批次数据只有一条
                    if (currentPartList.Count() > 0) //判断 有没有 该零件-批次信息
                    {
                        currentPartList = currentPartList.ToList();
                        if (currentPartList.Count() == 1 && currentPartList.First().OrdeBthDtailListID == 0)//无批次的 
                        {//是无批次的
                            var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails));//无批次号的零件，
                            decimal thisPartRealCount = queryPartList.Sum(n => n.count);//此时此种存的是 仓库实际发货的 该订单此种零件的所有数量

                            VM_Reserve firstVM = currentPartList.ElementAt(0);
                            //firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity - thisPartRealCount;//没有批次的 领料单开具数量
                            firstVM.LastPickingCount = 0M;
                            firstVM.AlctQty = firstVM.CurrentQuantity - firstVM.PickiingOrderQuantity;//不用减去仓库实际发的数量
                            /* if (firstVM.AlctQty <= 0M) 
                             {//如果仓库发完了，则表示不能再领了
                                 return false;
                             }*/
                            allCount += firstVM.AlctQty;
                            firstVM.ClnOdrID = order.CustomerOrderNum;
                            firstVM.ClnOdrDtl = order.CustomerOrderDetails;
                            list.Add(firstVM);
                        }
                        else
                        { //锁的是有批次的零件
                            decimal pthCountSize = currentPartList.Sum(n => n.AlctQty);//批次零件的预约数量 ，所有批次零件预约数量的和
                            VM_Reserve firstVM = currentPartList.ElementAt(0);
                            decimal countSize = firstVM.CurrentQuantity;//总的该零件实际在库数量  

                            decimal rst = countSize - pthCountSize;//这是剩下的没有批次的零件的预约数量
                            if (rst > 0M)//如果有批次的的总数量不够,此种情况一般不会出现，因为锁料，三种情况是互斥的
                            {
                                VM_Reserve vm = new VM_Reserve();
                                vm.CurrentQuantity = countSize;//实际在库数量 -
                                /*--
                                 *--
                                 */
                                vm.AlctQty = rst;// rst 没有批次的记录   的   预约数量，就是本个没有批次的零件最大可以领用的数量
                                vm.ClnOdrDtl = firstVM.ClnOdrDtl;
                                vm.ClnOdrID = firstVM.ClnOdrID;
                                //vm.PickiingOrderQty = firstVM.PickiingOrderQuantity - currentPartList.Sum(n => n.PickiingOrderQty);//没有批次的 领料单开具数量
                                vm.ProductID = firstVM.ProductID;
                                vm.ProductSpec = firstVM.ProductSpec;
                                vm.WhID = vm.WhID;
                                vm.BatchID = "";
                                list.Add(vm);
                            }
                            foreach (var o in currentPartList) //循环该种零件的所有批次
                            {
                                //var queryPartList = query.Where(n => n.MaterialID.Equals(pl.SubItemId) && n.CustomerOrderNum.Equals(order.CustomerOrderNum) && n.CustomerOrderDetails.Equals(order.CustomerOrderDetails) && n.BthID.Equals(o.BatchID));//该次号的零件
                                //decimal bthPartNextCount=queryPartList.Sum(n=>n.count);
                                //此中要计算出 该 订单的 该批次 的 最大可以请领的数量、领料单开具数量，无批次的最大可以请领数量、领料单开具的数量

                                o.AlctQty = o.AlctQty - o.PickiingOrderQty;//此种批次 零件剩余可以领的最大数量
                                o.LastPickingCount = 0M;
                                allCount += firstVM.AlctQty;
                                //o.AlctQty = bthPartNextCount;//此种批次 零件可以领的最大数量
                                o.ClnOdrID = order.CustomerOrderNum;
                                o.ClnOdrDtl = order.CustomerOrderDetails;
                                list.Add(o);
                            }
                        }
                    }
                }
            }
            if (allCount <= 0M)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
