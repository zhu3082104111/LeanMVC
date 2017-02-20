using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;
using Model.Market;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace App_UI.Areas.Market.Controllers
{
    public class OrderBillController : BaseController
    {
        private IMarketOrderService iMOS; //定义 IMarketOrderService 接口
        private IMarketOrderDetailService iMODS; //定义 IMarketOrderDetailService 接口
        private IMarketOrderDetailPrintService iMODPS; //定义 IMarketOrderDetailPrintService 接口
        private IOrderAcceptService iOAS; //定义 IOrderAcceptService 接口


        #region 自定义函数

        /// <summary>
        /// 删除相关客户订单号文件夹
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// 创建者：冯吟夷
        private void DeleteFile(string paraClientOrderID)
        {
            string strInitPath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachFile"].ToString(); //获取初始路径
            string strClientOrderPath = strInitPath + @"\" + paraClientOrderID; //获取客户订单号文件夹路径
            if (System.IO.Directory.Exists(strClientOrderPath))
            {
                System.IO.Directory.Delete(strInitPath + @"\" + paraClientOrderID, true); //用于删除里面有子文件夹、文件的文件夹
            }
        } //end DeleteFile

        /// <summary>
        /// 图片操作
        /// </summary>
        /// <param name="paraHttpPostedFileBaseArr">HttpPostedFileBase[] 上传文件数组上传文件数组</param>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// 创建者：冯吟夷
        private void SaveImg(HttpPostedFileBase[] paraHttpPostedFileBaseArr, string paraClientOrderID)
        {
            string strInitPath = Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["attachFile"].ToString()); //获取初始路径

            //遍历上传文件
            for (int i = 0; i < paraHttpPostedFileBaseArr.Length; i++)
            {
                string strSavePath = strInitPath + @"\" + paraClientOrderID + @"\图片" + Convert.ToString(i + 1); //设置要存储图片的路径

                if (paraHttpPostedFileBaseArr[i] != null && paraHttpPostedFileBaseArr[i].ContentLength > 0)
                {
                    if (System.IO.Directory.Exists(strSavePath) == true) //判断文件夹是否存在，如果不存在，就创建。否则，则先删除，再创建。类似事务回滚，保证中途出错的情况下，重头再来。  
                    {
                        System.IO.Directory.Delete(strSavePath, true); //用于删除里面有子文件夹、文件的文件夹
                    }
                    System.IO.Directory.CreateDirectory(strSavePath); //新建文件夹  
                    paraHttpPostedFileBaseArr[i].SaveAs(strSavePath + @"\" + System.IO.Path.GetFileName(paraHttpPostedFileBaseArr[i].FileName)); //存储图片
                } //end if   
            } //end for
        } //end SaveImg

        /// <summary>
        /// 更新 MarketOrderDetail 属性值，并返回泛型结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单ID</param>
        /// <param name="paraClientOrderDetail">客户订单明细</param>
        /// <param name="paraProductID">产品ID</param>
        /// <param name="paraDeliveryDate">交货日期</param>
        /// <param name="paraProduceCellID">生产单元ID</param>
        /// <param name="paraQuantity">数量</param>
        /// <param name="paraClientProductID">客户型号</param>
        /// <param name="paraPackageQuantity">装箱数</param>
        /// <param name="paraPackageSize">纸箱尺寸</param>
        /// <param name="paraSealColorID">油封颜色ID</param>
        /// <param name="paraSealRequire">油封其他特殊要求</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private List<MarketOrderDetail> UpdateMarketOrderDetailList(string paraClientOrderID, string paraClientOrderDetail, string paraProductID, string paraDeliveryDate, string paraProduceCellID, string paraQuantity, string paraClientProductID, string paraPackageQuantity, string paraPackageSize, string paraSealColorID, string paraSealRequire)
        {
            if (string.IsNullOrEmpty(paraClientOrderDetail) == true) //判断是否有值
            {
                return null;
            }

            DateTime? nullDateTime = null; //设置空日期
            int index = 0; //设置循环变量
            List<MarketOrderDetail> marketOrderDetailList = new List<MarketOrderDetail>(); //定义 MarketOrderDetail 泛型
            string[] clientOrderDetailArr = paraClientOrderDetail.Split(',');
            string[] productIDArr = paraProductID.Split(',');
            string[] deliveryDateArr = paraDeliveryDate.Split(',');
            string[] produceCellIDArr = paraProduceCellID.Split(',');
            string[] quantityArr = paraQuantity.Split(',');
            string[] clientProductIDArr = paraClientProductID.Split(',');
            string[] packageQuantityArr = paraPackageQuantity.Split(',');
            string[] packageSizeArr = paraPackageSize.Split(',');
            string[] sealColorIDArr = paraSealColorID.Split(',');
            string[] sealRequireArr = paraSealRequire.Split(',');

            for (index = 0; index < clientOrderDetailArr.Length; index++) //循环分别为每个 MarketOrderDeatil 对象赋值
            {
                MarketOrderDetail marketOrderDetail = new MarketOrderDetail(); //新增 MarketOrderDeatil 对象
                marketOrderDetail.ClientOrderID = paraClientOrderID;
                marketOrderDetail.ClientOrderDetail = clientOrderDetailArr[index];
                marketOrderDetail.ProductID = string.IsNullOrEmpty(productIDArr[index]) ? null : productIDArr[index];
                marketOrderDetail.DeliveryDate = string.IsNullOrEmpty(deliveryDateArr[index]) ? nullDateTime : Convert.ToDateTime(deliveryDateArr[index]); //设置交货日期。不设置空日期，三目运算符报错。
                marketOrderDetail.ProduceCellID = string.IsNullOrEmpty(produceCellIDArr[index]) ? null : produceCellIDArr[index];
                marketOrderDetail.Quantity = Convert.ToDecimal(quantityArr[index]);
                marketOrderDetail.ClientProductID = string.IsNullOrEmpty(clientProductIDArr[index]) ? null : clientProductIDArr[index];
                marketOrderDetail.PackageQuantity = Convert.ToDecimal(packageQuantityArr[index]);
                marketOrderDetail.PackageSize = string.IsNullOrEmpty(packageSizeArr[index]) ? null : packageSizeArr[index];
                marketOrderDetail.OriginalEquipmentManufacturerID = null;
                marketOrderDetail.ImageName = null;
                marketOrderDetail.SealColorID = string.IsNullOrEmpty(sealColorIDArr[index]) ? null : sealColorIDArr[index];
                marketOrderDetail.SealRequire = string.IsNullOrEmpty(sealRequireArr[index]) ? null : sealRequireArr[index];
                marketOrderDetail.SealPicture = null;
                marketOrderDetail.Urgency = "0";

                marketOrderDetailList.Add(marketOrderDetail); //添加 MarketOrderDetail 对象
            }

            return marketOrderDetailList; //返回 MarketOrderDetail 泛型结果集
        } //end UpdateMarketOrderDetailList

        /// <summary>
        /// 更新 MarketOrder 属性值，并返回对象
        /// </summary>
        /// <param name="paraClientOrderID">客户订单ID</param>
        /// <param name="paraDeliveryDate">交货日期</param>
        /// <param name="paraClientID">客户ID</param>
        /// <param name="paraPackageRequire">包装要求</param>
        /// <param name="paraPackageRequireImage1">包装要求图1</param>
        /// <param name="paraPackageRequireImage2">包装要求图2</param>
        /// <param name="paraPackageRequireImage3">包装要求图3</param>
        /// <param name="paraOtherMatter">其他注意事项</param>
        /// <param name="paraEditUserID1">编制人1</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private MarketOrder UpdateMarketOrderModel(string paraClientOrderID, string paraDeliveryDate, string paraClientID, string paraPackageRequire, string paraPackageRequireImage1, string paraPackageRequireImage2, string paraPackageRequireImage3, string paraOtherMatter, string paraEditUserID1)
        {
            DateTime? nullDateTime = null; //设置空日期
            MarketOrder marketOrder = new MarketOrder(); //创建对象
            string strInitPath = System.Web.Configuration.WebConfigurationManager.AppSettings["attachFile"].ToString(); //获取初始路径
            //设置对象属性
            marketOrder.ClientOrderID = paraClientOrderID;
            marketOrder.ClientVersion = 0;
            marketOrder.DeliveryDate = String.IsNullOrEmpty(paraDeliveryDate) ? nullDateTime : Convert.ToDateTime(paraDeliveryDate); //设置交货日期。不设置空日期，三目运算符报错。
            marketOrder.ClientID = String.IsNullOrEmpty(paraClientID) ? null : paraClientID;
            marketOrder.PackageRequire = paraPackageRequire;
            marketOrder.PackageRequireImage1 = string.IsNullOrEmpty(paraPackageRequireImage1) ? null : strInitPath + @"\" + paraClientOrderID + @"\图片1\" + paraPackageRequireImage1;
            marketOrder.PackageRequireImage2 = string.IsNullOrEmpty(paraPackageRequireImage2) ? null : strInitPath + @"\" + paraClientOrderID + @"\图片2\" + paraPackageRequireImage2;
            marketOrder.PackageRequireImage3 = string.IsNullOrEmpty(paraPackageRequireImage3) ? null : strInitPath + @"\" + paraClientOrderID + @"\图片3\" + paraPackageRequireImage3;
            marketOrder.PackageRequireImage4 = null;
            marketOrder.PackageRequireImage5 = null;
            marketOrder.OtherMatter = string.IsNullOrEmpty(paraOtherMatter) ? null : paraOtherMatter;
            marketOrder.ApprovalFlag = "0";
            marketOrder.ApprovalUserID = null;
            marketOrder.ApprovalDate = null;
            marketOrder.EditUserID1 = string.IsNullOrEmpty(paraEditUserID1) ? null : paraEditUserID1;
            marketOrder.EditUserDate1 = DateTime.Now;
            marketOrder.EditUserID2 = null;
            marketOrder.EditUserDate2 = null;
            marketOrder.OrderProgressStatus = "1";
            marketOrder.OrderStatus = "1";

            return marketOrder; //返回结果
        } //end UpdateMarketOrderModel

        /// <summary>
        /// 更新 MarketOrderDetailPrint 属性值，并返回泛型结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单ID</param>
        /// <param name="paraClientOrderDetail">客户订单明细</param>
        /// <param name="paraProductID">产品ID</param>
        /// <param name="paraDeliveryDate">交货日期</param>
        /// <param name="paraProduceCellID">生产单元ID</param>
        /// <param name="paraQuantity">数量</param>
        /// <param name="paraClientProductID">客户型号</param>
        /// <param name="paraPackageQuantity">装箱数</param>
        /// <param name="paraPackageSize">纸箱尺寸</param>
        /// <param name="paraSealColorID">油封颜色ID</param>
        /// <param name="paraSealRequire">油封其他特殊要求</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private List<MarketOrderDetailPrint> UpdateMarketOrderDetailPrintList(string paraClientOrderID, string paraClientOrderDetail, string paraProductID, string paraPosition, string paraContent)
        {
            if (string.IsNullOrEmpty(paraClientOrderDetail) == true) //判断是否有值
            {
                return null;
            }

            int index = 0; //设置循环变量
            List<MarketOrderDetailPrint> marketOrderDetailPrintList = new List<MarketOrderDetailPrint>(); //定义 MarketOrderDetailPrint 泛型
            string[] clientOrderDetailArr = paraClientOrderDetail.Split(',');
            string[] productIDArr = paraProductID.Split(',');
            string[] positionArr = paraPosition.Split(',');
            string[] contentArr = paraContent.Split(',');

            for (index = 0; index < clientOrderDetailArr.Length; index++) //循环分别为每个 MarketOrderDetailPrint 对象赋值
            {
                MarketOrderDetailPrint marketOrderDetailPrint = new MarketOrderDetailPrint(); //新增 MarketOrderDetailPrint 对象
                marketOrderDetailPrint.ClientOrderID = paraClientOrderID;
                marketOrderDetailPrint.ClientOrderDetail = clientOrderDetailArr[index];
                marketOrderDetailPrint.NO = Convert.ToString(index + 1);
                marketOrderDetailPrint.ProductID = string.IsNullOrEmpty(productIDArr[index]) ? null : productIDArr[index];
                marketOrderDetailPrint.Position = string.IsNullOrEmpty(positionArr[index]) ? null : positionArr[index];
                marketOrderDetailPrint.Content = string.IsNullOrEmpty(contentArr[index]) ? null : contentArr[index];
                marketOrderDetailPrint.ImageName = null;

                marketOrderDetailPrintList.Add(marketOrderDetailPrint); //添加 MarketOrderDetailPrint 对象
            }

            return marketOrderDetailPrintList; //返回 MarketOrderDetailPrint 泛型结果集
        } //end UpdateMarketOrderDetailPrintList

        /// <summary>
        /// 更新 ProduceGeneralPlan 属性值，并返回泛型结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单ID</param>
        /// <param name="paraClientOrderDetail">客户订单明细</param>
        /// <param name="paraProductID">产品ID</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private List<ProduceGeneralPlan> UpdateProduceGeneralPlanList(string paraClientOrderID, string paraClientOrderDetail, string paraProductID) 
        {
            if (string.IsNullOrEmpty(paraClientOrderDetail) == true) //判断是否有值
            {
                return null;
            }

            int index = 0; //设置循环变量
            List<ProduceGeneralPlan> produceGeneralPlanList = new List<ProduceGeneralPlan>(); //定义 MarketOrderDetail 泛型
            string[] clientOrderDetailArr = paraClientOrderDetail.Split(',');
            string[] productIDArr = paraProductID.Split(',');

            for (index = 0; index < clientOrderDetailArr.Length; index++) //循环分别为每个 ProduceGeneralPlan 对象赋值
            {
                ProduceGeneralPlan produceGeneralPlan = new ProduceGeneralPlan(); //新增 ProduceGeneralPlan 对象
                produceGeneralPlan.ClientOrderID = paraClientOrderID;
                produceGeneralPlan.ClientOrderDetail = clientOrderDetailArr[index];
                produceGeneralPlan.ProductID = productIDArr[index];
                produceGeneralPlan.Status = "1";

                produceGeneralPlanList.Add(produceGeneralPlan); //添加 ProduceGeneralPlan 对象
            }

            return produceGeneralPlanList;
        } //end UpdateProduceGeneralPlanList

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIMOS">IMarketOrderService 接口实现类</param>
        /// 创建者：冯吟夷
        public OrderBillController(IMarketOrderService paraIMOS, IMarketOrderDetailService paraIMODS,IMarketOrderDetailPrintService paraIMODPS, IOrderAcceptService paraIOAS) 
        {
            this.iMOS = paraIMOS;
            this.iMODS = paraIMODS;
            this.iMODPS = paraIMODPS;
            this.iOAS = paraIOAS;
        } //end OrderBillController

        #region 客户订单一览

        /// <summary>
        /// 获取表 MK_ORDER 查询数据
        /// </summary>
        /// <param name="paraMOFSTMO">VM_MarketOrderForSearchTableMarketOrder 表单查询类</param>
        /// <param name="paraPage">Paging 分页属性类</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMarketOrderList(VM_MarketOrderForSearchMarketOrderTable paraMOFSMOT, Paging paraPage)
        {
            IEnumerable<VM_MarketOrderForShowMarketOrderInfo> iEnumerableMOFSMOI = iMOS.GetMarketOrderListService(paraMOFSMOT, paraPage);
            int total = 0;
            total = paraPage.total;
            //
            return iEnumerableMOFSMOI.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end GetMarketOrderList

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public ActionResult Index()
        {
            return View();
        } //end Index

        /// <summary>
        /// 客户订单查询子页面
        /// 2014-1-10 杜兴军
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowOrderBillDialog()
        {
            return View();
        }//end ShowOrderBillDialog

        #endregion

        #region 新增订单

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public ActionResult AddOrderBill()
        {
            return View();
        } //end AddOrderBill

        /// <summary>
        /// 添加表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraFormCollection">FormCollection 表单提交类</param>
        /// <param name="PackageRequireImg">HttpPostedFileBase[] 上传文件数组</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public string AddOrderBill(FormCollection paraFormCollection, HttpPostedFileBase[] PackageRequireImg)
        {
            try
            {
                /**** 图片操作 ****/
                SaveImg(PackageRequireImg, paraFormCollection["ClientOrderID"]);

                /**** MarketOrder ****/
                string strPackageRequireImg1 = PackageRequireImg[0] != null && PackageRequireImg[0].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[0].FileName) : null; //判断 PackageRequireImg1 是否为空，避免调用 FileName 属性的时候异常
                string strPackageRequireImg2 = PackageRequireImg[1] != null && PackageRequireImg[1].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[1].FileName) : null; //判断 PackageRequireImg2 是否为空，避免调用 FileName 属性的时候异常
                string strPackageRequireImg3 = PackageRequireImg[2] != null && PackageRequireImg[2].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[2].FileName) : null; //判断 PackageRequireImg3 是否为空，避免调用 FileName 属性的时候异常
                MarketOrder marketOrder = UpdateMarketOrderModel(paraFormCollection["ClientOrderID"], paraFormCollection["DeliveryDate"], paraFormCollection["ClientID"], paraFormCollection["PackageRequire"], strPackageRequireImg1, strPackageRequireImg2, strPackageRequireImg3, paraFormCollection["OtherMatter"], paraFormCollection["EditUserID1"]); //获取 MarketOrder 对象

                /**** MarketOrderDetail ****/
                List<MarketOrderDetail> marketOrderDetailList = UpdateMarketOrderDetailList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetail"], paraFormCollection["ProductID"], paraFormCollection["MODDeliveryDate"], paraFormCollection["ProduceCellID"], paraFormCollection["Quantity"], paraFormCollection["ClientProductID"], paraFormCollection["PackageQuantity"], paraFormCollection["PackageSize"], paraFormCollection["SealColorID"], paraFormCollection["SealRequire"]); //获取 MarketOrderDetail 泛型结果集

                /**** MarketOrderDetailPrint ****/
                List<MarketOrderDetailPrint> marketOrderDetailPrintList = UpdateMarketOrderDetailPrintList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetailPrint"], paraFormCollection["MODPProductID"], paraFormCollection["Position"], paraFormCollection["Content"]); //获取 MarketOrderDetailPrint 泛型结果集

                /**** ProduceGeneralPlan ****/
                List<ProduceGeneralPlan> produceGeneralPlanList = UpdateProduceGeneralPlanList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetail"], paraFormCollection["ProductID"]); //获取 ProduceGeneralPlan 泛型结果集

                return Convert.ToString(iMOS.AddMarketOrderManagementService(marketOrder, marketOrderDetailList, marketOrderDetailPrintList, produceGeneralPlanList)); //返回结果
            }
            catch (Exception exception)
            {
                DeleteFile(paraFormCollection["ClientOrderID"]); //删除相关客户订单号文件夹
                return Convert.ToString(exception); //返回结果
            }
        } //end AddOrderBill

        #endregion 

        #region 修改订单

        /// <summary>
        /// 根据客户订单号，获取查询结果集
        /// </summary>
        /// <param name="id">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public JsonResult GetMarketOrderDetailList(string id)
        {
            IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> iEnumerableMODFMODT = iMODS.GetMarketOrderDetailListService(id);

            return iEnumerableMODFMODT.ToJson(0);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end GetMarketOrderDetailList

        /// <summary>
        /// 根据客户订单号，获取查询结果集
        /// </summary>
        /// <param name="id">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public JsonResult GetMarketOrderDetailPrintList(string id)
        {
            IEnumerable<VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable> iEnumerableMODPFMODPT = iMODPS.GetMarketOrderDetailPrintListService(id);

            return iEnumerableMODPFMODPT.ToJson(0);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器

        } //end GetMarketOrderDetailPrintList

        /// <summary>
        /// 更新页面
        /// </summary>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public ActionResult UpdateOrderBill(string ClientOrderID,string ClientVersion)
        {
            VM_MarketOrderForShowMarketOrderInfo mofsmoiModel = iMOS.GetMarketOrderInfoService(ClientOrderID, ClientVersion);

            return View(mofsmoiModel);
        } //end UpdateOrderBill

        /// <summary>
        /// 更新表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraFormCollection">FormCollection 表单提交类</param>
        /// <param name="PackageRequireImg">HttpPostedFileBase[] 上传文件数组</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public string UpdateOrderBill(FormCollection paraFormCollection, HttpPostedFileBase[] PackageRequireImg)
        {
            try
            {
                /**** 图片操作 ****/
                SaveImg(PackageRequireImg, paraFormCollection["ClientOrderID"]);

                /**** MarketOrder ****/
                string strPackageRequireImg1 = PackageRequireImg[0] != null && PackageRequireImg[0].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[0].FileName) : null; //判断 PackageRequireImg1 是否为空，避免调用 FileName 属性的时候异常
                string strPackageRequireImg2 = PackageRequireImg[1] != null && PackageRequireImg[1].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[1].FileName) : null; //判断 PackageRequireImg2 是否为空，避免调用 FileName 属性的时候异常
                string strPackageRequireImg3 = PackageRequireImg[2] != null && PackageRequireImg[2].ContentLength > 0 ? Path.GetFileName(PackageRequireImg[2].FileName) : null; //判断 PackageRequireImg3 是否为空，避免调用 FileName 属性的时候异常
                MarketOrder marketOrder = UpdateMarketOrderModel(paraFormCollection["ClientOrderID"], paraFormCollection["DeliveryDate"], paraFormCollection["ClientID"], paraFormCollection["PackageRequire"], strPackageRequireImg1, strPackageRequireImg2, strPackageRequireImg3, paraFormCollection["OtherMatter"], paraFormCollection["EditUserID1"]); //获取 MarketOrder 对象

                /**** MarketOrderDetail ****/
                List<MarketOrderDetail> marketOrderDetailList = UpdateMarketOrderDetailList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetail"], paraFormCollection["ProductID"], paraFormCollection["MODDeliveryDate"], paraFormCollection["ProduceCellID"], paraFormCollection["Quantity"], paraFormCollection["ClientProductID"], paraFormCollection["PackageQuantity"], paraFormCollection["PackageSize"], paraFormCollection["SealColorID"], paraFormCollection["SealRequire"]); //获取 MarketOrderDetail 泛型结果集

                /**** MarketOrderDetailPrint ****/
                List<MarketOrderDetailPrint> marketOrderDetailPrintList = UpdateMarketOrderDetailPrintList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetailPrint"], paraFormCollection["MODPProductID"], paraFormCollection["Position"], paraFormCollection["Content"]); //获取 MarketOrderDetailPrint 泛型结果集

                /**** ProduceGeneralPlan ****/
                List<ProduceGeneralPlan> produceGeneralPlanList = UpdateProduceGeneralPlanList(paraFormCollection["ClientOrderID"], paraFormCollection["ClientOrderDetail"], paraFormCollection["ProductID"]); //获取 ProduceGeneralPlan 泛型结果集

                return iMOS.UpdateMarketOrderManagementService(marketOrder, marketOrderDetailList, marketOrderDetailPrintList, produceGeneralPlanList).ToString(); //返回结果
            }
            catch (Exception ex)
            {
                DeleteFile(paraFormCollection["ClientOrderID"]); //删除相关客户订单号文件夹
                return ex.ToString(); //返回结果
            }
        } //end UpdateOrderBill

        #endregion


    } //end OrderBillController
}
