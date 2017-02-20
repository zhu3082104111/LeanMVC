/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：StoreExternalServiceImp.cs
// 文件功能描述：
//          仓库部门的外部共通的Service接口的实现类
//      
// 修改履历：2013/12/17 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using Util;
using System.Collections;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 仓库部门的外部共通的Service接口的实现类
    /// </summary>
    public class StoreExternalServiceImp : IStoreExternalService
    {
        private ICompMaterialInfoRepository compMaterialInfoRepository;
        private IBthStockListRepository bthStockListRepository;

        public StoreExternalServiceImp(ICompMaterialInfoRepository compMaterialInfoRepository, IBthStockListRepository bthStockListRepository)
        {
            this.compMaterialInfoRepository = compMaterialInfoRepository;
            this.bthStockListRepository = bthStockListRepository;
        }

        //需要使用的Repository类
        #region IStoreExternalService 成员（根据“产品或零件ID”返回相应的“单位ID”和“单位名称”）

        public VM_MaterUnitInfo GetMaterUnitId(string PdtID)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IStoreExternalService 成员（根据“产品零件ID”和“供应商ID”取得“单价”及“估价”信息）

        public VM_MaterPriceInfo GetMaterPrice(string PdtID, string CompID)
        {
            throw new NotImplementedException();
        }

        #endregion


        /// <summary>
        /// 批次别库存表的添加方法 陈健
        /// </summary>
        /// <param name="userId">登录人员ID</param>
        /// <param name="bthStock">批次别信息</param>
        /// <returns></returns>
        public bool WhInInsertBthStockList(string userId,List<BthStockList> bthStock)
        {
            //批次别信息不为空
            if (bthStock != null && bthStock.Count > 0)
            { 
            // 遍历批次别信息List
                foreach (BthStockList bthStockInfo in bthStock)
                {
                    BthStockList detail = new BthStockList();
                    detail.BillType = bthStockInfo.BillType;
                    detail.PrhaOdrID = bthStockInfo.PrhaOdrID;
                    detail.BthID = bthStockInfo.BthID;
                    detail.WhID = bthStockInfo.WhID;
                    detail.PdtID = bthStockInfo.PdtID;
                    detail.PdtSpec = bthStockInfo.PdtSpec;
                    detail.GiCls = bthStockInfo.GiCls;
                    detail.Qty = bthStockInfo.Qty;
                    detail.OrdeQty = bthStockInfo.OrdeQty;
                    detail.CmpQty = 0;
                    detail.DisQty = 0;
                    detail.InDate = DateTime.Today;
                    detail.DiscardFlg = "0";
                    detail.EffeFlag = "0";
                    detail.DelFlag = "0";
                    detail.CreUsrID = userId;
                    detail.CreDt = DateTime.Today;
                    detail.UpdUsrID = userId;
                    detail.UpdDt = DateTime.Today;

                    // 向批次别库存表里插入数据
                    bool ret = bthStockListRepository.Add(detail);

                    // 插入数据失败时，返回false；
                    if (ret == false)
                    {
                        return false;
                    }
                }

            }

            return true;
    
        }
    }
}
