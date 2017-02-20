/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutStoreServiceImp.cs
// 文件功能描述：
//          内部成品库出库相关画面的Service接口的实现
//
// 修改履历：2013/12/02 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Extensions;
using Model.Store;
using Model;
using BLL.ServerMessage;

namespace BLL.Store
{
    /// <summary>
    /// 内部成品库出库相关画面的Service接口的实现
    /// </summary>
    public class FinOutStoreServiceImp : AbstractService, IFinOutStoreService 
    {
        //引入需要调用的Repository类
        private IFinOutRecordRepository finOutRecordRepository;
        private IFinOutDetailRecordRepository finOutDetailRecordRepository;
        private IProdInfoRepository prodInfoRepository;
        private IReserveRepository reserveRepository;
        private IMaterialRepository materialRepository;
        private IReserveDetailRepository reserveDetailRepository;
        private IBthStockListRepository bthStockListRepository;
        private IPartInfoRepository partInfoRepository;
        /// <summary>
        /// 方法实现，引入调用的Repository
        /// </summary>
        /// <param name="finOutRecordRepository">出库履历</param>
        public FinOutStoreServiceImp(IFinOutRecordRepository finOutRecordRepository, IFinOutDetailRecordRepository finOutDetailRecordRepository, IProdInfoRepository prodInfoRepository
            , IReserveRepository reserveRepository, IMaterialRepository materialRepository, IReserveDetailRepository reserveDetailRepository, IBthStockListRepository bthStockListRepository
            , IPartInfoRepository partInfoRepository) 
        {
            this.finOutRecordRepository = finOutRecordRepository;
            this.finOutDetailRecordRepository = finOutDetailRecordRepository;
            this.prodInfoRepository = prodInfoRepository;
            this.reserveRepository = reserveRepository;
            this.materialRepository = materialRepository;
            this.reserveDetailRepository = reserveDetailRepository;
            this.bthStockListRepository = bthStockListRepository;
            this.partInfoRepository = partInfoRepository;
        }

        /// <summary>
        /// 获得待出库一览画面数据
        /// </summary>
        /// <param name="finOutStore">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutStoreForTableShow> GetFinOutStoreForSearch(VM_storeFinOutStoreForSearch finOutStore, Paging paging)
        {
            return finOutRecordRepository.GetFinOutStoreWithPaging(finOutStore, paging);
        }

        /// <summary>
        /// 获得出库履历一览画面数据
        /// </summary>
        /// <param name="finOutRecord">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutRecordForTableShow> GetFinOutRecordForSearch(VM_storeFinOutRecordForSearch finOutRecord, Paging paging)
        {
            return finOutRecordRepository.GetFinOutRecordWithPaging(finOutRecord, paging);
        }

        /// <summary>
        /// 出库履历一览删除
        /// </summary>
        /// <param name="list">删除的数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <returns>true</returns>
        public bool DeleteFinOutStore(List<string> list, string uId)
        {
            foreach (var Id in list)
            {
                //删除出库履历详细表里数据
                finOutRecordRepository.updateFinOutRecordDetail(new FinOutDetailRecord { InerFinOutID = Id, DelFlag = "1", UpdUsrID = uId, UpdDt = DateTime.Today });
                //删除出库履历表里数据
                finOutRecordRepository.updateFinOutRecord(new FinOutRecord { InerFinOutID = Id, DelFlag = "1", UpdUsrID = uId, UpdDt = DateTime.Today });
            }
            return true;

        }

        /// <summary>
        /// 获得出库履历详细画面（表示）
        /// </summary>
        /// <param name="inerFinOutID">成品送货单号</param>
        /// <param name="uId">登录人员</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutRecordDetailForTableShow> GetFinOutRecordDetailById(string inerFinOutID,string uId, Paging page)
        {
            return finOutRecordRepository.GetFinOutRecordDetailByIdWithPaging(inerFinOutID,uId, page);
        }

        /// <summary>
        /// 出库履历详细画面登录保存（检查，添加，修改）
        /// </summary>
        /// <param name="finOutRecord">更新的数据集合</param>
        /// <param name="pageFlg">页面状态标识</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="outRecordList">更新的数据集合</param>
        /// <returns>提示消息</returns>
        public string FinOutRecordForLogin(VM_storeFinOutRecordDetailForTableShow finOutRecord, string pageFlg, string uId, Dictionary<string, string>[] outRecordList)
        {
            //由零件略称得到其ID
            //for (int i = 0; i < outRecordList.Length; i++)
            //{
            //    var endList = partInfoRepository.GetPartID(outRecordList[i]["OrdPdtID"]).ToList();
            //    outRecordList[i]["OrdPdtID"] = endList[0].PartId;
            //}
            //详细跳转
            if (pageFlg == "1")
            {
                for (int i = 0; i < outRecordList.Length; i++)
                {
                    //入库详细编辑
                    FinOutRecordForELogin(finOutRecord, uId, i, outRecordList);
                }
               
            }
            //新增跳转
            else
            {
                for (int i = 0; i < outRecordList.Length; i++)
                {
                    Reserve reser = new Reserve();
                    reser.WhID = "0302";
                    var clientOrder = outRecordList[i]["ClientOrderID"].Split('/');
                    reser.ClnOdrID = clientOrder[0];
                    reser.ClnOdrDtl = clientOrder[1];
                    reser.PdtID = outRecordList[i]["OrdPdtID"];
                    reser.PdtSpec = outRecordList[i]["ProductSpec"];
                    //获得预约批次详细单号
                    var reserveDetailListID = reserveRepository.GetReserveDetailListID(reser).ToList();
                    if (reserveDetailListID.Count > 0)
                    {
                        //根据预约批次详细单号到仓库预约详细表里检索
                        var reserveDetailList = reserveDetailRepository.GetReserveDetailList(reserveDetailListID[0].OrdeBthDtailListID, outRecordList[i]["BatchID"]).ToList();
                        if (reserveDetailList.Count == 0)
                        {
                            //第【N】行的数据仓库预约详细表里不存在，请重新输入！
                            throw new Exception(ResourceHelper.ConvertMessage(SM_Store.SMSG_STORE_E00001, new string[] { outRecordList[i]["RowIndex"] }));
                        }
                        else
                        {
                            //成品库添加出库履历详细数据
                            FinOutForLoginAddOutRecord(finOutRecord, uId, i, outRecordList);
                            //成品库出库登录修改仓库表
                            FinOutForLoginUpdateMaterial(finOutRecord, uId, i, outRecordList);
                            //成品库出库登录修改仓库预约表及预约详细表
                            FinOutForLoginUpdateReserve(finOutRecord, uId, i, outRecordList);
                            //成品库出库登录修改批次别库存表
                            FinOutForLoginUpdateBthStockList(finOutRecord, uId, i, outRecordList);
                        }

                    }
                    else
                    {
                        throw new Exception(SM_Store.SMSG_STORE_E00001);
                    }

                    //出库履历表添加判断
                    var endList = finOutRecordRepository.GetFinOutRecordLadiID(finOutRecord.LadiID).ToList();

                    if (endList.Count == 0)
                    {
                        //成品库出库履历添加
                        bool aor = finOutRecordRepository.Add(new FinOutRecord
                        {
                            LadiID = finOutRecord.LadiID,
                            InerFinOutID = finOutRecord.InerFinOutID,
                            WareHouseID = "0302",
                            OutMoveCls = "02",
                            WareHouseKpID = finOutRecord.OutWorks,
                            Remarks = finOutRecord.MRemarks,
                            OutDate = DateTime.Today,
                            PrintFlag = "0",
                            EffeFlag = "0",
                            DelFlag = "0",
                            CreUsrID = uId,
                            CreDt = DateTime.Today,
                            UpdUsrID = uId,
                            UpdDt = DateTime.Today
                        });
                        if (aor == false)
                        {
                            throw new Exception("添加失败");
                        }

                    }
                    else
                    {
                        throw new Exception("添加失败");
                    }
                }
               
            }

           return "更新成功";
           
        }

        #region 详细跳转更新出库履历及详细表

        /// <summary>
        /// 对输入的数据进行检查并更新(出库详细编辑更新成品库出库履历表及其详细表)
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        public void FinOutRecordForELogin(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i,Dictionary<string, string>[] outRecordList)
        {
            //修改成品库出库履历详细表(字段赋值)
            FinOutDetailRecord finOutDetailRecord = new FinOutDetailRecord();
            finOutDetailRecord.InerFinOutID = finOutRecord.InerFinOutID;
            var clientOrder = outRecordList[i]["ClientOrderID"].Split('/');
            finOutDetailRecord.ClientOrderID = clientOrder[0];
            finOutDetailRecord.ClientOrderDetail = clientOrder[1];
            finOutDetailRecord.OrdPdtID = outRecordList[i]["OrdPdtID"];
            finOutDetailRecord.BatchID = outRecordList[i]["BatchID"];

            finOutDetailRecord.Quantity = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            finOutDetailRecord.PackPieceQuantity = Convert.ToDecimal(outRecordList[i]["PackPieceQuantity"]);
            finOutDetailRecord.PackPrePieceQuantity = Convert.ToDecimal(outRecordList[i]["PackPrePieceQuantity"]);
            finOutDetailRecord.FracQuantity = Convert.ToDecimal(outRecordList[i]["FracQuantity"]);
            finOutDetailRecord.NotaxAmt = Convert.ToDecimal(outRecordList[i]["Quantity"]) * Convert.ToDecimal(outRecordList[i]["PrchsUp"]);
            finOutDetailRecord.UpdDt = DateTime.Today;
            finOutDetailRecord.UpdUsrID = uId;

            //修改成品库入库履历详细表
            bool uod = finOutDetailRecordRepository.UpdateInFinOutDetailRecord(finOutDetailRecord);
            if (uod == false)
            {
                throw new Exception("修改失败");
            }
        }

        #endregion

        #region 出库履历新添加数据（插入出库履历详细表）

        /// <summary>
        /// 成品库添加出库履历详细数据
        /// </summary>
        /// <param name="finOutRecord">添加数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        public void FinOutForLoginAddOutRecord(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList)
        {
            //出库履历详细表添加判断
            var endDList = finOutDetailRecordRepository.GetFinOutRecordDetailList(finOutRecord.InerFinOutID, outRecordList,i).ToList();

            if (endDList.Count == 0)
            {
                var clientOrder = outRecordList[i]["ClientOrderID"].Split('/');
                //成品库出库履历详细添加
                FinOutDetailRecord detail = new FinOutDetailRecord();
                detail.InerFinOutID = finOutRecord.InerFinOutID;
                detail.ClientOrderID = clientOrder[0];
                detail.ClientOrderDetail = clientOrder[1];
                detail.OrdPdtID = outRecordList[i]["OrdPdtID"];
                detail.ProductSpec = outRecordList[i]["ProductSpec"];
                detail.BatchID = outRecordList[i]["BatchID"];
                detail.GiCls = "000";
                detail.Unit = "kg";
                detail.Quantity = Convert.ToDecimal(outRecordList[i]["Quantity"]);
                detail.PackPieceQuantity = Convert.ToDecimal(outRecordList[i]["PackPieceQuantity"]);
                detail.PackPrePieceQuantity = Convert.ToDecimal(outRecordList[i]["PackPrePieceQuantity"]);
                detail.FracQuantity = Convert.ToDecimal(outRecordList[i]["FracQuantity"]);
                detail.GetQuantity = 100;
                detail.PrchsUp = Convert.ToDecimal(outRecordList[i]["PrchsUp"]);
                detail.NotaxAmt = Convert.ToDecimal(outRecordList[i]["NotaxAmt"]);
                detail.Remarks = outRecordList[i]["Remarks"];
                detail.EffeFlag = "0";
                detail.DelFlag = "0";
                detail.CreUsrID = uId;
                detail.CreDt = DateTime.Today;
                detail.UpdUsrID = uId;
                detail.UpdDt = DateTime.Today;
                bool aodr = finOutDetailRecordRepository.Add(detail);
               
                if (aodr == false)
                {
                    throw new Exception("添加失败");
                }
            }
            else
            {
                throw new Exception("添加失败");
            }
        }

        #endregion

        #region 出库履历详细修改仓库表

        /// <summary>
        /// 成品库出库登录修改仓库表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        public void FinOutForLoginUpdateMaterial(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList)
        {
            //修改仓库表(字段赋值)
            Material material = new Material();
            material.WhID = "0302";
            material.PdtID = outRecordList[i]["OrdPdtID"];

            ProdInfo productInfo = prodInfoRepository.GetProdInfoById(outRecordList[i]["OrdPdtID"]);

            material.CurrentQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            material.TotalAmt = productInfo.Pricee * Convert.ToDecimal(outRecordList[i]["Quantity"]);
            material.TotalValuatUp = productInfo.Evaluate * Convert.ToDecimal(outRecordList[i]["Quantity"]);
            material.UpdDt = DateTime.Today;
            material.UpdUsrID = uId;
            //修改仓库表
            bool um = materialRepository.updateInMaterialFinOut(material);
            if (um == false)
            {
                throw new Exception("更新失败");
            }

        }

        #endregion

        #region 出库履历详细修改仓库预约表及预约详细表

        /// <summary>
        /// 成品库出库登录修改预约及预约详细表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        public void FinOutForLoginUpdateReserve(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList)
        {
            //修改仓库预约表及预约详细表(字段赋值)
            Reserve reser = new Reserve();
            
            reser.WhID = "0302";
            var clientOrder = outRecordList[i]["ClientOrderID"].Split('/');
            reser.ClnOdrID = clientOrder[0];
            reser.ClnOdrDtl = clientOrder[1];
            reser.PdtID = outRecordList[i]["OrdPdtID"];
            reser.PdtSpec = outRecordList[i]["ProductSpec"];
           
            reser.RecvQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            reser.PickOrdeQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            reser.CmpQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            reser.UpdDt = DateTime.Today;
            reser.UpdUsrID = uId;

            ReserveDetail reserDetail = new ReserveDetail();
            reserDetail.BthID = outRecordList[i]["BatchID"];
            reserDetail.PickOrdeQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            reserDetail.CmpQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            reserDetail.UpdDt = DateTime.Today;
            reserDetail.UpdUsrID = uId;

            //获得预约批次详细单号
            var reserveDetailListID = reserveRepository.GetReserveDetailListID(reser).ToList();
            
            //修改仓库预约表
            bool ru = reserveRepository.UpdateInReserveFinOut(reser);
            if (ru == false)
            {
                throw new Exception("更新失败");
            }
            //修改仓库预约详细表
            bool rdu = reserveDetailRepository.UpdateInReserveDetailFinOut(reserveDetailListID[0].OrdeBthDtailListID,reserDetail);
            if (rdu == false)
            {
                throw new Exception("更新失败");
            }

        }

        #endregion

        #region 出库履历详细修改批次别库存表

        /// <summary>
        /// 成品库出库登录修改批次别库存表
        /// </summary>
        /// <param name="finOutRecord">更新数据集合</param>
        /// <param name="uId">登录人员ID</param>
        /// <param name="i">行参数</param>
        /// <param name="outRecordList">更新数据集合</param>
        public void FinOutForLoginUpdateBthStockList(VM_storeFinOutRecordDetailForTableShow finOutRecord, string uId, int i, Dictionary<string, string>[] outRecordList)
        {
            //修改批次别库存表(字段赋值)
            BthStockList bthStockList = new BthStockList();
            bthStockList.WhID = "0302";
            bthStockList.PdtID = outRecordList[i]["OrdPdtID"];
            bthStockList.BthID = outRecordList[i]["BatchID"];
            bthStockList.PdtSpec = outRecordList[i]["ProductSpec"];

            bthStockList.CmpQty = Convert.ToDecimal(outRecordList[i]["Quantity"]);
            bthStockList.UpdUsrID = uId;
            bthStockList.UpdDt = DateTime.Today;
            //修改批次别库存表
            bool bu = bthStockListRepository.updateInBthStockListFinOut(bthStockList);
            if (bu == false)
            {
                throw new Exception("更新失败");
            }
           
        }

        #endregion

        /// <summary>
        /// 获得内部成品送货单画面数据
        /// </summary>
        /// <param name="inerFinOutID">内部成品送货单号</param>
        /// <param name="clientId">客户订单号</param>
        /// <param name="OPdtId">产品ID</param>
        /// <param name="batchId">批次号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutPrintIndexForTableShow> GetFinOutPrintIndexById(string inerFinOutID, string clientId, string OPdtId, string batchId, Paging page)
        {
            return finOutDetailRecordRepository.GetFinOutPrintIndexByIdWithPaging(inerFinOutID,clientId,OPdtId,batchId,page);
        }

        /// <summary>
        /// 获得内部成品送货单打印选择画面数据
        /// </summary>
        /// <param name="finOutPrint">筛选条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>画面数据结果</returns>
        public IEnumerable<VM_storeFinOutPrintForTableShow> GetFinOutPrintForSearch(VM_storeFinOutPrintForSearch finOutPrint, Paging paging)
        {
            return finOutRecordRepository.GetFinOutPrintWithPaging(finOutPrint, paging);
        }

        /// <summary>
        /// 出库详细新增状态，根据输入的零件略称自动生成零件信息
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        public List<PartInfo> GetFinOutRecordPdtInfoById(string partAbbrevi)
        {
            var productInfo = partInfoRepository.GetFinRecordProductInfoById(partAbbrevi);
            return productInfo;
        }

        /// <summary>
        /// 产品零件查询（子查询画面专用）
        /// </summary>
        /// <param name="searchConditon">查询条件</param>
        /// <param name="paging">分页条件</param>
        /// <returns></returns>
        public IEnumerable<VM_ProdAndPartInfo> GetProdAndPartInfo(VM_ProdAndPartInfo searchConditon, Paging paging)
        {
            return finOutRecordRepository.GetProdAndPartInfo(searchConditon, paging);
        }
    }
}
