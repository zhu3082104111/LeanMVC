/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouseServiceImp.cs
// 文件功能描述：成品交仓业务层实现类
// 
// 创建标识：20131125 杜兴军
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    public class ProductWarehouseServiceImp:IProductWarehouseService
    {
        private IProductWarehouseRepository productWarehouseRepository;
        private IProductWarehouseDetailRepository productWarehouseDetailRepository;


        public ProductWarehouseServiceImp(IProductWarehouseRepository productWarehouseRepository, IProductWarehouseDetailRepository productWarehouseDetailRepository)
        {
            this.productWarehouseRepository = productWarehouseRepository;
            this.productWarehouseDetailRepository = productWarehouseDetailRepository;
        }

        
        public IEnumerable<VM_ProductWarehouseShow> GetWarehouseByPage(VM_ProductWarehouseSearch search, Paging paging)
        {
            return productWarehouseRepository.GetWarehouseShowByPage(search, paging);
        }
        
        #region 成品待交仓单一览画面
            /// <summary>
            /// 数据方法的调用  潘军
            /// </summary>
            /// <param name="productWarehouseID"></param>
            /// <returns></returns>
        public IEnumerable GetPWaitingWarehouseListByPage(VM_PWaitingWarehouseListForSearch search, Paging paging)
            {
                return productWarehouseRepository.GetPWaitingWarehouseListBySearchByPage(search, paging); 
            }
        #endregion
        
        public VM_ProductWarehouseDetailHeadData GetWarehouseDetail(string productWarehouseID)
        {
            return productWarehouseDetailRepository.GetDetailData(productWarehouseID);
        }


        public bool UpdateWarehouse(ProductWarehouse warehouse, List<ProductWarehouseDetail> warehouseDetailList)
        {
            productWarehouseRepository.AddOrUpdateWarehouse(warehouse);//更新或添加交仓单信息，无交仓单号则为添加
            if (!warehouseDetailList.Any())
            {
                return true;
            }
            foreach (ProductWarehouseDetail producWarehouseDetail in warehouseDetailList)
            {
                productWarehouseDetailRepository.AddOrUpdate(producWarehouseDetail);
            }
            
            return true;
        }


        public bool DeleteWarehouseDetail(ProductWarehouseDetail warehouseDetail)
        {
            return productWarehouseDetailRepository.Delete(warehouseDetail);
        }



        public int GetMaxLineNo(string productWarehouseId)
        {
            return productWarehouseDetailRepository.GetMaxLineNo(productWarehouseId);
        }


        public bool DeleteWarehouse(string warehouseIds,string userId)
        {
            /*string[] warehouseIdArr = warehouseIds.Split(',');//分割成品交仓单号
            List<ProductWarehouse> warehouseList=new List<ProductWarehouse>();//成品交仓
            List<ProductWarehouseDetail> warehouseDetailList=new List<ProductWarehouseDetail>();//成品交仓详细
            DateTime now = DateTime.Now;//当前时间
            for (int i = 0; i < warehouseIdArr.Length; i++)
            {
                ProductWarehouse warehouse=new ProductWarehouse()
                {
                    ProductWarehouseID = warehouseIdArr[i],
                    DelFlag = Constant.GLOBAL_DELFLAG_OFF,
                    DelUsrID = userId,
                    DelDt = now
                };
                ProductWarehouseDetail warehouseDetail=new ProductWarehouseDetail()
                {
                    ProductWarehouseID = warehouseIdArr[i],
                    DelFlag = Constant.GLOBAL_DELFLAG_OFF,
                    DelUsrID = userId,
                    DelDt = now
                };
                warehouseList.Add(warehouse);
                warehouseDetailList.Add(warehouseDetail);
            }
            productWarehouseRepository.DeleteWarehouse(warehouseList, warehouseDetailList);*/
            return productWarehouseRepository.DeleteWarehouse(warehouseIds,userId);
        }


        public bool AddProductWarehouse(ProductWarehouse warehouse, List<ProductWarehouseDetail> warehouseDetails)
        {
            productWarehouseRepository.Add(warehouse);
            for (int i = 0, len = warehouseDetails.Count(); i < len; i++)
            {
                productWarehouseDetailRepository.Add(warehouseDetails[i]);
            }
            return true;
        }


        public bool UpdateWarehouseState(ProductWarehouse warehouse)
        {
            productWarehouseRepository.Update(warehouse, new string[] { "WarehouseState", "UpdDt", "UpdUsrID" });
            return true;
        }
    }
}
