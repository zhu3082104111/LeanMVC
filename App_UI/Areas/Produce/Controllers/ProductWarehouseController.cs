/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouseController.cs
// 文件功能描述：成品交仓控制器
// 
// 创建标识：20131123 杜兴军
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using BLL.Common;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class ProductWarehouseController : BaseController
    {
        //
        // GET: /Produce/ProductWarehouse/

        private IProductWarehouseService productWarehouseService;
        private IAutoCreateOddNoService autoCreateOddNoService;

        /// <summary>
        /// 
        /// </summary>
        public ProductWarehouseController(IProductWarehouseService productWarehouseService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.productWarehouseService = productWarehouseService;
            this.autoCreateOddNoService = autoCreateOddNoService;

        }

        #region 视图
        /// <summary>
        /// 主视图（一览）
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 详细视图
        /// </summary>
        /// <param name="productWarehouseId">成品交仓单号</param>
        /// <param name="detailOrUpdate">0为查看详细(默认)；1为编辑</param>
        /// <returns></returns>
        public ActionResult Details(string productWarehouseId,string detailOrUpdate="0")
        {
            ViewBag.productWarehouseID = productWarehouseId;
            //ViewBag.detailOrUpdate = detailOrUpdate;
            return View();
        }

        /// <summary>
        /// 编辑详细视图
        /// </summary>
        /// <param name="productWarehouseId">成品交仓单号</param>
        /// <returns></returns>
        public ActionResult UpdateDetail(string productWarehouseId)
        {
            ViewBag.productWarehouseID = productWarehouseId;
            return View();
        }

        #endregion


        #region 列表
        /// <summary>
        /// 成品交仓单   一览
        /// </summary>
        /// <param name="search">检索条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public JsonResult Get(VM_ProductWarehouseSearch search,Paging paging)
        {
            return productWarehouseService.GetWarehouseByPage(search, paging).ToJson(paging.total);
        }

        /// <summary>
        /// 成品交仓详细
        /// </summary>
        /// <param name="productWarehouseId">成品交仓单号</param>
        /// <returns></returns>
        public JsonResult GetDetail(string productWarehouseId)
        {
            return productWarehouseService.GetWarehouseDetail(productWarehouseId).ToJson(0);
        }

        #endregion


        #region 数据操作
        /// <summary>
        /// 新增成品交仓单
        /// </summary>
        /// <param name="formCollection">表单数据</param>
        /// <returns></returns>
        public JsonResult Add(FormCollection formCollection)
        {
            bool result = true;
            string message = "操作成功！";
            string backId = null;//待返回的成品交仓单号
            try
            {
                ProductWarehouse warehouseData = new ProductWarehouse();//交仓单
                List<ProductWarehouseDetail> warehouseDetailList = new List<ProductWarehouseDetail>();//交仓单详细
                string userId = Session["UserID"].ToString(); //当前用户ID
                DateTime now = DateTime.Now; //当前时间
                warehouseData.ProductWarehouseID = autoCreateOddNoService.GetProdWarehouseId(userId);//交仓单号
                warehouseData.DepartmentID = formCollection["DepartmentID"]; //交仓部门
                warehouseData.BatchID = ""; //批次号
                warehouseData.WarehouseDT = new DateTime(now.Year,now.Month,now.Day); //交仓日期
                warehouseData.WarehousePersonID = userId; //交仓人
                warehouseData.CheckPersonID = null; //检验员
                warehouseData.DispatherID = null; //调度员
                warehouseData.WarehouseState = Constant.ProductWarehouseState.UNSUBMIT; //未提交
                warehouseData.CreDt = now; //创建日期
                warehouseData.CreUsrID = userId; //创建人
                warehouseData.EffeFlag = Constant.GLOBAL_EffEFLAG_ON; //有效
                warehouseData.DelFlag = Constant.GLOBAL_DELFLAG_ON; //未删除
                backId = warehouseData.ProductWarehouseID;

                int lineNo = 1;
                string[] clientOrderIdArr = formCollection["ClientOrderIDs"].Split(','); //客户订单号
                string[] clientOrderDetailArr = formCollection["ClientOrderDetails"].Split(',');//客户订单明细
                string[] teamIdArr = formCollection["TeamIDs"].Split(','); //装配小组
                string[] orderProductIdArr = formCollection["OrderProductIDs"].Split(','); //产品型号(零件)
                string[] productSpecificationArr = formCollection["ProductSpecifications"].Split(','); //规格
                string[] qualifiedQuantityArr = formCollection["QualifiedQuantitys"].Split(','); //合格数量
                string[] eachBoxQuantityArr = formCollection["EachBoxQuantitys"].Split(','); //每箱数量
                string[] boxQuantityArr = formCollection["BoxQuantitys"].Split(','); //箱数 没有就自己算
                string[] remianQuantityArr = formCollection["RemianQuantitys"].Split(','); //零头 没有就自己算
                string[] assembleDispatchIdArr = formCollection["AssembleDispatchIDs"].Split(',');//总装调度单号

                for (int i = 0, len = clientOrderIdArr.Length; i < len; i++)
                {
                    ProductWarehouseDetail warehouseDetail = new ProductWarehouseDetail();
                    warehouseDetail.ProductWarehouseID = warehouseData.ProductWarehouseID;
                    warehouseDetail.WarehouseLineNO = lineNo + "";
                    warehouseDetail.ClientOrderID = clientOrderIdArr[i];
                    warehouseDetail.ClientOrderDetail = clientOrderDetailArr[i];
                    warehouseDetail.TeamID = teamIdArr[i];
                    warehouseDetail.OrderProductID = orderProductIdArr[i];
                    warehouseDetail.ProductSpecification = productSpecificationArr[i];
                    warehouseDetail.QualifiedQuantity = decimal.Parse(qualifiedQuantityArr[i]);
                    warehouseDetail.EachBoxQuantity = decimal.Parse(eachBoxQuantityArr[i]);
                    warehouseDetail.BoxQuantity = decimal.Parse(boxQuantityArr[i]);
                    warehouseDetail.RemianQuantity = decimal.Parse(remianQuantityArr[i]);
                    warehouseDetail.AssemblyDispatchID = assembleDispatchIdArr[i];
                    warehouseDetail.ProductCheckID = null;
                    warehouseDetail.Remark = null;
                    warehouseDetail.EffeFlag = Constant.GLOBAL_EffEFLAG_ON;
                    warehouseDetail.DelFlag = Constant.GLOBAL_DELFLAG_ON;
                    warehouseDetail.CreDt = now;
                    warehouseDetail.CreUsrID = userId;
                    warehouseDetailList.Add(warehouseDetail);
                    lineNo++;
                }
                productWarehouseService.AddProductWarehouse(warehouseData, warehouseDetailList);//添加成品交仓单信息
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
                backId = null;
            }
            finally
            { }
            return new { result = result, message = message, backId=backId }.ToJson();
        }

        /// <summary>
        /// 修改成品交仓单
        /// </summary>
        /// <param name="formCollection">表单数据</param>
        /// <returns></returns>
        public JsonResult Update(FormCollection formCollection)
        {
            bool result = true;
            string message = "操作成功！";
            try
            {
                ProductWarehouse warehouseData = new ProductWarehouse();//交仓单
                List<ProductWarehouseDetail> warehouseDetailList = new List<ProductWarehouseDetail>();//交仓单详细
                string userId = Session["UserID"].ToString(); //当前用户ID
                DateTime now = DateTime.Now; //时间
                warehouseData.ProductWarehouseID = formCollection["ProductWarehouseID"]; //交仓单号
                warehouseData.DepartmentID = formCollection["DepartmentID"]; //交仓部门
                //warehouseData.BatchID = formCollection["BatchID"]; //批次号
                DateTime warehouseDt=Constant.CONST_FIELD.INIT_DATETIME;
                bool isParseDTOk = false;//交仓日期是否解析成功
                isParseDTOk= DateTime.TryParse(formCollection["WarehouseDT"], out warehouseDt);
                warehouseData.WarehouseDT = isParseDTOk ? warehouseDt : Constant.CONST_FIELD.DB_INIT_DATETIME;//交仓日期
                warehouseData.WarehousePersonID = formCollection["WarehousePersonID"]; //交仓人
                warehouseData.CheckPersonID = formCollection["CheckPersonID"]; //检验员
                warehouseData.DispatherID = formCollection["DispatherID"]; //调度员
                //warehouseData.WarehouseState = Constant.ProductWarehouseState.UNSUBMIT; //未提交
                warehouseData.UpdDt = now; //修改日期
                warehouseData.UpdUsrID = userId; //修改人
                
                if (formCollection["QualifiedQuantity"] != null) //有详细信息
                {
                    string[] modelTypeArr = formCollection["ModelType"].Split(','); //标识数据是否改变(0未改变)
                    string[] warehouseLineNoArr = formCollection["WarehouseLineNO"].Split(','); //行号
                    string[] qualifiedQuantityArr = formCollection["QualifiedQuantity"].Split(','); //合格数量
                    string[] eachBoxQuantityArr = formCollection["EachBoxQuantity"].Split(','); //每箱数量
                    string[] boxQuantityArr = formCollection["BoxQuantity"].Split(','); //箱数
                    string[] remianQuantityArr = formCollection["RemianQuantity"].Split(','); //零头
                    string[] remarkArr = formCollection["Remark"].Split(','); //备注

                    for (int i = 0, len = modelTypeArr.Length; i < len; i++)
                    {
                        if (modelTypeArr[i].Equals("0")) //数据未变化
                        {
                            continue;
                        }
                        ProductWarehouseDetail warehouseDetail = new ProductWarehouseDetail();
                        warehouseDetail.ProductWarehouseID = warehouseData.ProductWarehouseID;
                        warehouseDetail.WarehouseLineNO = warehouseLineNoArr[i];
                        warehouseDetail.QualifiedQuantity = int.Parse(qualifiedQuantityArr[i]);
                        warehouseDetail.EachBoxQuantity = int.Parse(eachBoxQuantityArr[i]);
                        warehouseDetail.BoxQuantity = int.Parse(boxQuantityArr[i]);
                        warehouseDetail.RemianQuantity = int.Parse(remianQuantityArr[i]);
                        warehouseDetail.Remark = remarkArr[i];
                        warehouseDetail.UpdDt = now;
                        warehouseDetail.UpdUsrID = userId;
                        warehouseDetailList.Add(warehouseDetail);
                    }
                    productWarehouseService.UpdateWarehouse(warehouseData, warehouseDetailList);//更新成品交仓单信息
                }
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
            }
            finally
            {}
            return new {result = result, message = message}.ToJson();
        }

        /// <summary>
        /// 删除成品交仓单详细某条记录
        /// </summary>
        /// <param name="warehouseDetail">交仓单详细信息(单号和行号)</param>
        /// <returns></returns>
        public JsonResult DeleteDetail(ProductWarehouseDetail warehouseDetail)
        {
            bool result = true;
            string message = "";
            try
            {
                warehouseDetail.DelUsrID = Session["UserID"].ToString();
                warehouseDetail.DelFlag = Constant.GLOBAL_DELFLAG_OFF;
                warehouseDetail.DelDt = DateTime.Now;
                productWarehouseService.DeleteWarehouseDetail(warehouseDetail);//执行删除
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
            }
            return new {result = result, message = message}.ToJson();
        }

        /// <summary>
        /// 删除成品交仓单记录(多条)
        /// </summary>
        /// <param name="warehouseIds">交仓单号集</param>
        /// <returns></returns>
        public JsonResult Delete(string warehouseIds)
        {
            bool result = true;
            string message = "";
            try
            {
                string userId = Session["UserID"].ToString();
                productWarehouseService.DeleteWarehouse(warehouseIds,userId);//执行删除
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
            }
            
            return new{result=result,message=message}.ToJson();
        }

        /// <summary>
        /// 修改交仓单状态
        /// </summary>
        /// <param name="productWarehouseId"></param>
        /// <returns></returns>
        public JsonResult UpdateWarehouseState(string productWarehouseId)
        {
            bool result = true;
            string message = null;
            try
            {
                string userId = Session["UserID"].ToString();
                ProductWarehouse warehouse=new ProductWarehouse()
                {
                    ProductWarehouseID = productWarehouseId,
                    WarehouseState = Constant.ProductWarehouseState.SUBMITED,
                    UpdDt = DateTime.Now,
                    UpdUsrID = userId
                };
                productWarehouseService.UpdateWarehouseState(warehouse);
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
            }
            return new {result = result, message = message}.ToJson();
        }

        #endregion
    }
}
