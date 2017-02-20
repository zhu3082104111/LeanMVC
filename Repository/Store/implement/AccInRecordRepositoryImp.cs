/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInRecordRepositoryImp.cs
// 文件功能描述：
//            附件库入库履历及入库相关业务Repository实现
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Collections;

namespace Repository
{
    public class AccInRecordRepositoryImp : AbstractRepository<DB, AccInRecord>, IAccInRecordRepository
    {
        //附件库仓库编码
        public String accWhID = "0002";
        //当前用户
        public string LoginUserID = "201228";
        //让步区分正常品
        public string norMalGiCls = "999";

        #region IAccInRecordRepository 成员（附件库待入库一览页面数据初始化页面（yc添加））
        //=========================
        //待入库一览相关函数
        //=========================
        public IEnumerable GetAccInStoreBySearchByPage(VM_AccInStoreForSearch accInStoreForSearch, Paging paging)
        {
            IQueryable<PurChkList> purChkList = null;
            IQueryable<MCDeliveryOrderDetail> mCDeliveryOrderDetailList = null;

            //取得满足条件的进货检验单表数据((注：GetList可替换为GetAvailableList))
            purChkList = base.GetList<PurChkList>().Where(p => p.OsSupFlg == "1" && p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag=="0");
            //取得满足条件的送货单详细表数据
            mCDeliveryOrderDetailList = base.GetList<MCDeliveryOrderDetail>().Where(m => m.DelFlag == "0" && m.WarehouseID == accWhID && m.EffeFlag == "0");

           // bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(accInStoreForSearch.IsetRepID))
            {
                purChkList = purChkList.Where(c => c.ChkListId == accInStoreForSearch.IsetRepID);
               // isPaging = false;
            }
            if (!String.IsNullOrEmpty(accInStoreForSearch.DeliveryOrderID))
            {
                purChkList = purChkList.Where(c => c.DlyOdrId.Contains(accInStoreForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(accInStoreForSearch.DeliveryCompanyID))
            {
                purChkList = purChkList.Where(c => c.CompName.Contains(accInStoreForSearch.DeliveryCompanyID));
            }
            if (!String.IsNullOrEmpty(accInStoreForSearch.MaterielID))
            {
                purChkList = purChkList.Where(c => c.PartId.Contains(accInStoreForSearch.MaterielID));
            }
            if (!String.IsNullOrEmpty(accInStoreForSearch.MaterielName))
            {
                purChkList = purChkList.Where(c => c.PartName.Contains(accInStoreForSearch.MaterielName));
            }
            //质检日期
            if (accInStoreForSearch.StartDt != null)
            {
                purChkList = purChkList.Where(c => c.ChkDt >= accInStoreForSearch.StartDt);
            }
            if (accInStoreForSearch.EndDt != null)
            {
                purChkList = purChkList.Where(d => d.ChkDt <= accInStoreForSearch.EndDt);
            }

            else
            {
            }
        
            IQueryable<VM_AccInStoreForTableShow> query = from p in purChkList
                                                          join m in mCDeliveryOrderDetailList
                                                          on p.DlyOdrId equals m.DeliveryOrderID where p.PartId == m.MaterielID //方法1
                                                          //on new { p.DlyOdrId, p.PartId } equals new { m.DeliveryOrderID, m.MaterielID }//方法2

                        select new VM_AccInStoreForTableShow
                        {
                           //送货单号
                            DeliveryOrderID=p.DlyOdrId, 
                            //检验报告单号
                            IsetRepID =p.ChkListId,
                            //供货商名称
                            DeliveryCompanyID=p.CompName, 
                            //物料名称
                            MaterielID =p.PartName,
                            //质检日期
                            ChkDt=p.ChkDt
                        };
            //if (isPaging)
            //{
            //     paging.total = query.Count();
            //     IEnumerable<VM_AccInStoreForTableShow> resultForFirst = query.ToPageList<VM_AccInStoreForTableShow>("ChkDt asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //     return resultForFirst;
            //}      
            paging.total = query.Count();
            IEnumerable<VM_AccInStoreForTableShow> result = query.ToPageList<VM_AccInStoreForTableShow>("ChkDt asc", paging);
            return result;
        }

        #endregion

        #region IAccInRecordRepository 成员（附件库入库登录初始化页面 yc（添加））


        public IEnumerable GetAccInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {

            IQueryable<PurChkList> purChkList = null;
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList=null;
            IQueryable<MCDeliveryOrderDetail> mCDeliveryOrderDetailList = null;
            IQueryable<AccInRecord> accInRecordList = null;
            IQueryable<AccInDetailRecord> accInDetailRecordList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList=null;                                        

            //取得满足条件的进货检验单表数据
            purChkList = base.GetList<PurChkList>().Where(p => p.OsSupFlg == "1" && p.StoStat == "0" && p.DelFlag == "0"&& p.EffeFlag=="0");
             //取得满足条件的送货单表数据
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(m => m.DelFlag == "0" && m.EffeFlag == "0");
            //取得满足条件的送货单详细表数据
            mCDeliveryOrderDetailList = base.GetList<MCDeliveryOrderDetail>().Where(m => m.DelFlag == "0" && m.WarehouseID == accWhID && m.EffeFlag == "0");
            //取得满足条件的入库履历表数据
            accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList=base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");


            //purChkList = from p in purChkList where mcIsetInListID.Contains(p.DlyOdrId) && isetRepID.Contains(p.ChkListId) select p;

            //入库与入库履历中的数据
            IQueryable<VM_AccInLoginStoreForTableShow> accInRecordListQuery = from a in accInRecordList
                                                                              join b in accInDetailRecordList on a.McIsetInListID equals b.McIsetInListID
                                                                              join c in partInfoList on b.PdtID equals c.PartId
                                                                              join d in unitInfoList on c.UnitId equals d.UnitId
                                                                              where (deliveryOrderID.Contains(a.DlvyListID))
                                                                              select new VM_AccInLoginStoreForTableShow
                                                                              {
                                                                                  //采购订单号
                                                                                  PrhaOdrID = a.PrhaOdrID,
                                                                                  //送货单号
                                                                                  DeliveryOrderID = a.DlvyListID,
                                                                                  //物资验收入库单号
                                                                                  McIsetInListID = a.McIsetInListID,
                                                                                  //批次号
                                                                                  BthID = a.BthID,
                                                                                  //检验报告单号
                                                                                  IsetRepID = b.IsetRepID,
                                                                                  //让步区分
                                                                                  GiCls = b.GiCls,
                                                                                  //物资ID
                                                                                  PdtID = b.PdtID,
                                                                                  //物资名称
                                                                                  PdtName = b.PdtName,
                                                                                  //规格型号
                                                                                  PdtSpec = b.PdtSpec,
                                                                                  //数量
                                                                                  Qty = b.Qty,
                                                                                  //单位
                                                                                  Unit = d.UnitName,
                                                                                  //单价
                                                                                  PrchsUp = b.PrchsUp,
                                                                                  //估价
                                                                                  ValuatUp=b.ValuatUp,
                                                                                  //金额
                                                                                  NotaxAmt = b.NotaxAmt,
                                                                                  //入库日期
                                                                                  InDate = b.InDate,
                                                                                  //备注
                                                                                  Rmrs = b.Rmrs,
                                                                                  //标识
                                                                                  AccLoginFlg = "ForLogin",
                                                                                  //单价标识
                                                                                  AccLoginPriceFlg="1",
                                                                                  //供货商ID
                                                                                  CompID=""

                                                                              };
            //单价取得方式
            var accInRecordListQuerys = accInRecordListQuery.ToList();
            for (int i = 0; i < accInRecordListQuerys.Count;i++ )
            {
                var PrchsUp = accInRecordListQuerys[i].PrchsUp;
                var ValuatUp = accInRecordListQuerys[i].ValuatUp;
                if (PrchsUp == '0' && ValuatUp != '0')
                {
                    //用估价
                    PrchsUp = ValuatUp;                
                    accInRecordListQuerys[i].AccLoginPriceFlg = "0";
                }
                else {
                           
                }

            }

            IQueryable<VM_AccInLoginStoreForTableShow> purChkListQuery = from p in purChkList
                                                                         join m in mCDeliveryOrderDetailList on p.DlyOdrId equals m.DeliveryOrderID
                                                                         join n in mCDeliveryOrderList on m.DeliveryOrderID equals n.DeliveryOrderID
                                                                         join c in partInfoList on p.PartId equals c.PartId
                                                                         join d in unitInfoList on c.UnitId equals d.UnitId
                                                                         //join a incordList on  p.DlyOdrId equals a.DlvyListID
                                                                         where (p.PartId == m.MaterielID && isetRepID.Contains(p.ChkListId))
                                                                         select new VM_AccInLoginStoreForTableShow
                                                                         {
                                                                             //采购订单号
                                                                             PrhaOdrID = m.DeliveryOrderID,
                                                                             //送货单号
                                                                             DeliveryOrderID = m.DeliveryOrderID,
                                                                             //物资验收入库单号
                                                                             McIsetInListID = m.DeliveryOrderID + accWhID,
                                                                             //批次号
                                                                             BthID = n.BatchID,
                                                                             //检验报告单号
                                                                             IsetRepID = p.ChkListId,
                                                                             //让步区分
                                                                             GiCls = p.GiCls,
                                                                             //物资ID
                                                                             PdtID = p.PartId,
                                                                             //物资名称
                                                                             PdtName = p.PartName,
                                                                             //规格型号
                                                                             PdtSpec = p.PdtSpec,
                                                                             //数量
                                                                             Qty = p.StoQty,
                                                                             //单位
                                                                             Unit = d.UnitName,
                                                                             //单价
                                                                             PrchsUp = m.PriceWithTax,
                                                                             //估价
                                                                             ValuatUp = m.CkPriceWithTax,
                                                                             //金额
                                                                             NotaxAmt = p.StoQty * m.PriceWithTax,
                                                                             //入库日期
                                                                             InDate = DateTime.Now,
                                                                             //备注
                                                                             Rmrs = p.Remark,
                                                                             //标识
                                                                             AccLoginFlg = "",
                                                                             //单价标识
                                                                             AccLoginPriceFlg="1",
                                                                             //供货商ID
                                                                             CompID=p.CompId
                                                                         };
            //单价取得方式
            var purChkListQuerys = purChkListQuery.ToList();
            for (int i = 0; i < purChkListQuerys.Count; i++)
            {
                var compID = purChkListQuerys[i].CompID;
                var pdtID = purChkListQuerys[i].PdtID;
                var inDate = purChkListQuerys[i].InDate;
                CompMaterialInfo compMaterialInfo = base.First<CompMaterialInfo>(a => a.DelFlag == "0" && a.EffeFlag == "0" && a.CompId == compID && a.PdtId == pdtID && a.CompType == "1" && a.ActivStrDt <= inDate && a.ActivEndDt >= inDate);
                //CompMaterialInfo中无数据暂且注释
                //if (compMaterialInfo.UnitPrice == '0' && compMaterialInfo.Evaluate != '0' || compMaterialInfo.UnitPrice==null)
                //{
                //    //用估价
                //    purChkListQuerys[i].ValuatUp = compMaterialInfo.Evaluate;
                //    accInRecordListQuerys[i].AccLoginPriceFlg = "0";
                //}
                //else
                //{

                //}               

                //送货单详细表中应该保存单价和估价？？
            }


            var query = accInRecordListQuerys.Union(purChkListQuerys); 
            paging.total = query.Count();
            IEnumerable<VM_AccInLoginStoreForTableShow> result = query.AsQueryable().ToPageList<VM_AccInLoginStoreForTableShow>("InDate desc", paging);
            return result;
        }

        #endregion



        #region IAccInRecordRepository 成员(附件库入库履历一览（yc添加）)


        public IEnumerable GetAccInRecordBySearchByPage(VM_AccInRecordStoreForSearch accInRecordStoreForSearch, Paging paging)
        {
            IQueryable<AccInRecord> accInRecordList = null;
            IQueryable<AccInDetailRecord> accInDetailRecordList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;

            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的入库履历表数据
            accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            // bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.IsetRepID))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.IsetRepID.Contains(accInRecordStoreForSearch.IsetRepID));
                //isPaging = false;
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.PrhaOdrID))
            {
                accInRecordList = accInRecordList.Where(a => a.PrhaOdrID.Contains(accInRecordStoreForSearch.PrhaOdrID));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.DeliveryOrderID))
            {
                accInRecordList = accInRecordList.Where(a => a.DlvyListID.Contains(accInRecordStoreForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.BthID))
            {
                accInRecordList = accInRecordList.Where(a => a.BthID.Contains(accInRecordStoreForSearch.BthID));
            }
            //入库日期
            if (accInRecordStoreForSearch.StartInDate != null)
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.InDate >= accInRecordStoreForSearch.StartInDate);
            }        
            if (accInRecordStoreForSearch.EndInDate != null)
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.InDate <= accInRecordStoreForSearch.EndInDate);
            }      
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.PdtName))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.PdtName.Contains(accInRecordStoreForSearch.PdtName));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.McIsetInListID))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.McIsetInListID.Contains(accInRecordStoreForSearch.McIsetInListID));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.GiCls))
            {
                if (accInRecordStoreForSearch.GiCls == norMalGiCls)
                {
                    accInDetailRecordList = accInDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (accInRecordStoreForSearch.GiCls != norMalGiCls)
                {
                    accInDetailRecordList = accInDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.PdtSpec))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.PdtSpec.Contains(accInRecordStoreForSearch.PdtSpec));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.InMvCls))
            {
                accInRecordList = accInRecordList.Where(a => a.InMvCls.Contains(accInRecordStoreForSearch.InMvCls));
            }
            if (!String.IsNullOrEmpty(accInRecordStoreForSearch.InMvCls))
            {
                accInRecordList = accInRecordList.Where(a => a.InMvCls.Contains(accInRecordStoreForSearch.InMvCls));
            }

            //入库与入库履历中的数据
            IQueryable<VM_AccInRecordStoreForTableShow> accInRecordListQuery = from a in accInRecordList
                                                                               join b in accInDetailRecordList on a.McIsetInListID equals b.McIsetInListID
                                                                               join c in partInfoList on b.PdtID equals c.PartId
                                                                               join d in unitInfoList on c.UnitId equals d.UnitId
                                                                               select new VM_AccInRecordStoreForTableShow
                                                                               {
                                                                                   //采购订单号
                                                                                   PrhaOdrID = a.PrhaOdrID,
                                                                                   //送货单号
                                                                                   DeliveryOrderID = a.DlvyListID,
                                                                                   //批次号
                                                                                   BthID = a.BthID,
                                                                                   //物资验收入库单号
                                                                                   McIsetInListID = a.McIsetInListID,
                                                                                   //检验报告单号
                                                                                   IsetRepID = b.IsetRepID,
                                                                                   //让步区分
                                                                                   GiCls = b.GiCls,
                                                                                   //物资ID
                                                                                   PdtID = b.PdtID,
                                                                                   //物资名称
                                                                                   PdtName = b.PdtName,
                                                                                   //规格型号
                                                                                   PdtSpec = b.PdtSpec,
                                                                                   //数量
                                                                                   Qty = b.Qty,
                                                                                   //单位
                                                                                   Unit = d.UnitName,
                                                                                   //单价
                                                                                   PrchsUp = b.PrchsUp,
                                                                                   //估价
                                                                                   ValuatUp = b.ValuatUp,
                                                                                   //金额
                                                                                   NotaxAmt = b.NotaxAmt,
                                                                                   //入库日期
                                                                                   InDate = b.InDate,
                                                                                   //备注
                                                                                   Rmrs = b.Rmrs,
                                                                                   //单价标识
                                                                                   AccInRecordPriceFlg = "1"

                                                                               };


            //if (isPaging)
            //{
            //    paging.total = accInRecordListQuerys.Count();
            //    IEnumerable<VM_AccInRecordStoreForTableShow> resultForFirst = accInRecordListQuerys.AsQueryable().ToPageList<VM_AccInRecordStoreForTableShow>("InDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}
            paging.total = accInRecordListQuery.Count();
            IEnumerable<VM_AccInRecordStoreForTableShow> result = accInRecordListQuery.ToPageList<VM_AccInRecordStoreForTableShow>("InDate desc", paging);
            //单价取得方式
            var accInRecordListQuerys = accInRecordListQuery.ToList();
            for (int i = 0; i < accInRecordListQuerys.Count; i++)
            {
                var PrchsUp = accInRecordListQuerys[i].PrchsUp;
                var ValuatUp = accInRecordListQuerys[i].ValuatUp;
                if (PrchsUp == '0' && ValuatUp != '0')
                {
                    //用估价
                    PrchsUp = ValuatUp;
                    accInRecordListQuerys[i].AccInRecordPriceFlg = "0";
                }
                else
                {

                }
            }
            return accInRecordListQuerys;
        }

        #endregion
       

        #region IAccInRecordRepository 成员(获取附件库履历数据对象（yc添加）)


        public AccInRecord SelectAccInRecord(AccInRecord accInRecord)
        {
            return base.First(a => a.DlvyListID == accInRecord.DlvyListID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IAccInRecordRepository 成员（附件库删除入库履历数据（yc添加））


        public bool AccInRecordForDel(AccInRecord accInRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_ACC_IN_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where DLVY_LIST_ID={2} ",DateTime.Now, accInRecord.DelUsrID, accInRecord.DlvyListID);
        }

        #endregion



        #region IAccInRecordRepository 成员(入库单打印选择画面显示（yc添加）)


        public IEnumerable GetAccInPrintBySearchByPage(VM_AccInPrintForSearch accInPrintForSearch, Paging paging)
        {
            IQueryable<AccInRecord> accInRecordList = null;
            IQueryable<AccInDetailRecord> accInDetailRecordList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList = null;

            //取得满足条件的送货单表数据
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(m => m.DelFlag == "0" && m.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的入库履历表数据
            accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.WhID == accWhID && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            //bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(accInPrintForSearch.IsetRepID))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.IsetRepID == accInPrintForSearch.IsetRepID);
                //isPaging = false;
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.PrhaOdrID))
            {
                accInRecordList = accInRecordList.Where(a => a.PrhaOdrID.Contains(accInPrintForSearch.PrhaOdrID));
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.DeliveryOrderID))
            {
                accInRecordList = accInRecordList.Where(a => a.DlvyListID.Contains(accInPrintForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.BthID))
            {
                accInRecordList = accInRecordList.Where(a => a.BthID.Contains(accInPrintForSearch.BthID));
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.GiCls))
            {
                if (accInPrintForSearch.GiCls == norMalGiCls)
                {
                    accInDetailRecordList = accInDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (accInPrintForSearch.GiCls != norMalGiCls)
                {
                    accInDetailRecordList = accInDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.PdtID))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.PdtID.Contains(accInPrintForSearch.PdtID));
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.PrintFlg))
            {
                accInDetailRecordList = accInDetailRecordList.Where(a => a.PrintFlg.Contains(accInPrintForSearch.PrintFlg));
            }
            //送货日期
            if (accInPrintForSearch.StartDeliveryDate != null)
            {
                mCDeliveryOrderList = mCDeliveryOrderList.Where(a => a.DeliveryDate >= accInPrintForSearch.StartDeliveryDate);
            }
            if (accInPrintForSearch.StartDeliveryDate != accInPrintForSearch.EndDeliveryDate)
            {
                mCDeliveryOrderList = mCDeliveryOrderList.Where(a => a.DeliveryDate <= accInPrintForSearch.EndDeliveryDate);
            }
            if (!String.IsNullOrEmpty(accInPrintForSearch.WhID))
            {
                accInRecordList = accInRecordList.Where(a => a.WhID.Contains(accInPrintForSearch.WhID));
            }

            //履历中的数据
            IQueryable<VM_AccInPrintForTableShow> accInPrintListQuery = from a in accInRecordList
                                                                        join b in accInDetailRecordList on a.McIsetInListID equals b.McIsetInListID
                                                                        join m in mCDeliveryOrderList on a.DlvyListID equals m.DeliveryOrderID
                                                                        join c in partInfoList on b.PdtID equals c.PartId
                                                                        join d in unitInfoList on c.UnitId equals d.UnitId
                                                                        select new VM_AccInPrintForTableShow
                                                                        {
                                                                            //打印状态
                                                                            PrintFlg = b.PrintFlg,
                                                                            //采购订单号
                                                                            PrhaOdrID = a.PrhaOdrID,
                                                                            //送货单号
                                                                            DeliveryOrderID = a.DlvyListID,
                                                                            //批次号
                                                                            BthID = a.BthID,
                                                                            //物资验收入库单号
                                                                            McIsetInListID = a.McIsetInListID,
                                                                            //检验报告单号
                                                                            IsetRepID = b.IsetRepID,
                                                                            //让步区分
                                                                            GiCls = b.GiCls,
                                                                            //物资ID
                                                                            PdtID = b.PdtID,
                                                                            //物资名称
                                                                            PdtName = b.PdtName,
                                                                            //规格型号
                                                                            PdtSpec = b.PdtSpec,
                                                                            //数量
                                                                            Qty = b.Qty,
                                                                            //单位
                                                                            Unit = d.UnitName,
                                                                            //单价
                                                                            PrchsUp = b.PrchsUp,
                                                                            //金额
                                                                            NotaxAmt = b.NotaxAmt,
                                                                            //入库日期
                                                                            InDate = b.InDate,
                                                                            //备注
                                                                            Rmrs = b.Rmrs

                                                                        };
            //if (isPaging)
            //{
            //    paging.total = accInPrintListQuery.Count();
            //    IEnumerable<VM_AccInPrintForTableShow> resultForFirst = accInPrintListQuery.ToPageList<VM_AccInPrintForTableShow>("InDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}

            paging.total = accInPrintListQuery.Count();
            IEnumerable<VM_AccInPrintForTableShow> result = accInPrintListQuery.ToPageList<VM_AccInPrintForTableShow>("InDate desc", paging);
            return result;
        }

        #endregion

        #region IAccInRecordRepository 成员（物资验收入库单显示（yc添加））


        public IEnumerable SelectForAccInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            IQueryable<AccInRecord> accInRecordList = null;
            IQueryable<AccInDetailRecord> accInDetailRecordList = null;
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList = null;
            IQueryable<MCOutSourceOrder> mCOutSourceOrder = null;
            IQueryable<PurChkList> purChkList = null;
            //部门表
            IQueryable<Department> departmentList = null;
            //供货商信息表
            IQueryable<CompInfo> compInfoList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;

            //外协外购（加工单位）取得供货商信息表
            compInfoList = base.GetList<CompInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //自生产（加工单位）取得部门信息表
            departmentList = base.GetList<Department>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //进货检验单表
            purChkList = base.GetList<PurChkList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //外购单表
            mCOutSourceOrder = base.GetList<MCOutSourceOrder>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //送货单表
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的出库履历表数据
            accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.WhID == accWhID && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //履历中的数据
            IQueryable<VM_AccInPrintIndexForTableShow> accInPrintListQuery = from a in accInRecordList
                                                                              join b in accInDetailRecordList on a.McIsetInListID equals b.McIsetInListID
                                                                              join c in partInfoList on b.PdtID equals c.PartId
                                                                              join d in unitInfoList on c.UnitId equals d.UnitId
                                                                              // join f in mCDeliveryOrderList on a.DlvyListID equals f.DeliveryOrderID
                                                                              //join g in mCOutSourceOrder  on a.PrhaOdrID equals g.OutOrderID
                                                                              //join h in purChkList on b.IsetRepID equals h.ChkListId
                                                                              where (deliveryOrderID.Contains(a.DlvyListID) && pdtID.Contains(b.PdtID) && (deliveryOrderID).Contains(b.McIsetInListID.Substring(0, b.McIsetInListID.Length - 4)))
                                                                              select new VM_AccInPrintIndexForTableShow
                                                                              {
                                                                                  //单据日期
                                                                                  Date = DateTime.Now,
                                                                                  //单据编号
                                                                                  PrhaOdrID = a.McIsetInListID,
                                                                                  //打印状态
                                                                                  PrintFlg = b.PrintFlg,
                                                                                  //送货单位
                                                                                  DeliveryCompanyID = (purChkList.Where(f => b.IsetRepID == f.ChkListId)).FirstOrDefault().CompName,//(部门ID这块需确认)？？？？？
                                                                                  //采购部门
                                                                                  DepartID = "物控部",//(mCOutSourceOrder.Where(m=>a.PrhaOdrID == m.OutOrderID)).FirstOrDefault().DepartmentID
                                                                                  //所属部门
                                                                                  DepartName ="分离单元",//(mCOutSourceOrder.Where(m => a.PrhaOdrID == m.OutOrderID)).FirstOrDefault().DepartmentID
                                                                                  //物资ID
                                                                                  PdtID = b.PdtID,
                                                                                  //物料名称
                                                                                  PdtName = b.PdtName,
                                                                                  //规格型号
                                                                                  PdtSpec = b.PdtSpec,
                                                                                  //数量
                                                                                  Qty = b.Qty,
                                                                                  //单位
                                                                                  Unit = d.UnitName,
                                                                                  //单价
                                                                                  PrchsUp = b.PrchsUp,
                                                                                  //金额
                                                                                  NotaxAmt = b.NotaxAmt,
                                                                                  //备注
                                                                                  Rmrs = b.Rmrs,
                                                                                  //质检
                                                                                  UID = (purChkList.Where(f => b.IsetRepID == f.ChkListId)).FirstOrDefault().ChkPsnId,
                                                                                  //采购
                                                                                  UID1 = (mCOutSourceOrder.Where(m => a.PrhaOdrID == m.OutOrderID)).FirstOrDefault().OutCompanyID,
                                                                                  //仓管
                                                                                  WhkpID = b.WhkpID,
                                                                                  //制单人
                                                                                  ProID = LoginUserID,
                                                                                  //入库日期
                                                                                  InDate = b.InDate

                                                                              };
            //for (int i = 0; i < accInPrintListQuery.Count(); i++)
            //{
            //    var xx = accInPrintListQuery.ElementAt(i).DepartName;
            //}
          
            paging.total = accInPrintListQuery.Count();
            IEnumerable<VM_AccInPrintIndexForTableShow> result = accInPrintListQuery.ToPageList<VM_AccInPrintIndexForTableShow>("InDate desc", paging);
          
            var accInPrintListQuerys = accInPrintListQuery.ToList();
            for (int i = 0; i < accInPrintListQuerys.Count; i++)
            {
                string WhkpID1 = accInPrintListQuerys[0].WhkpID;
                string WhkpID = accInPrintListQuerys[i].WhkpID;
                if (!WhkpID.Equals(WhkpID1))
                {
                    for (int j = 0; j < accInPrintListQuerys.Count; j++)
                    {
                        accInPrintListQuerys[j].WhkpID = WhkpID1 + "等";
                    }
                    break;
                }
                else
                {
                    //accInPrintListQueryList[i].WhkpID = WhkpID1;
                }
            }
            return result;

        }

        #endregion

        #region IAccInRecordRepository 成员


        public IEnumerable GetWipStoreBySearchByPageTest(string mcIsetInListID, string isetRepID, Paging paging)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAccInRecordRepository 成员（修改进货检验单表入库状态（一期测试）（yc添加））


        public bool UpdatePurChkListForStoStat(string IsetRepID, string stoStat)
        {
            return base.ExecuteStoreCommand("update QU_PUR_CHK_LIST set STO_STAT={0} where CHK_LIST_ID={1} ",stoStat, IsetRepID);
        }

        #endregion

        #region IAccInRecordRepository 成员（修改过程检验单表入库状态（一期测试）（yc添加））


        public bool UpdateProcChkListForStoStat(string IsetRepID, string stoStat)
        {
            return base.ExecuteStoreCommand("update QU_PROC_CHK_LIST set STO_STAT={0} where CHK_LIST_ID={1} ", stoStat, IsetRepID);
        }

        #endregion
    }
}
