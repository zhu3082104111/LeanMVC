/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderRepositoryImp.cs
// 文件功能描述：订单表Repository接口实现类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Model.Market;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Repository
{
    class MarketOrderRepositoryImp : AbstractRepository<DB, MarketOrder>, IMarketOrderRepository
    {

        public string GetClientName(string clientOrderNO)
        {

            IQueryable<MarketOrder> marketOrder = base.GetAvailableList<MarketOrder>().Where(a => a.ClientOrderID == clientOrderNO);
            IQueryable<MarketClientInformation> ClientInfo = base.GetAvailableList<MarketClientInformation>();

            string result =(from mo in marketOrder
                                        join ci in ClientInfo on mo.ClientID equals ci.ClientID
                                        select ci.ClientName).FirstOrDefault();
            if (result == null)
            {
                return "";
            }
            return result;
        }


        #region 自定义函数

        /// <summary>
        /// 根据用户ID，获取对应实体类
        /// </summary>
        /// <param name="paraUserID">用户ID</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private UserInfo GetUserInfoModel(string paraUserID)
        {
            UserInfo ui = new UserInfo();
            ui.UId = paraUserID;
            return base.Find<UserInfo>(ui);
        }

        /// <summary>
        /// 获取审批名称
        /// </summary>
        /// <param name="paraApprovalFlag">审批Flag</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private string GetMarketApprovalFlagName(string paraApprovalFlag)
        {
            return null;
        }

        /// <summary>
        /// 获取订单进度状态名称
        /// </summary>
        /// <param name="paraOrderProgressStatus">订单进度状态</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private string GetMarketOrderProgressStatusName(string paraOrderProgressStatus)
        {
            return null;
        }

        /// <summary>
        /// 获取订单状态名称
        /// </summary>
        /// <param name="paraOrderStatus">订单状态</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private string GetMarketOrderStatusName(string paraOrderStatus)
        {
            if (paraOrderStatus.Equals("1"))
            {
                return "登录订单";
            }
            else if (paraOrderStatus.Equals("2"))
            {
                return "审核中";
            }
            else if (paraOrderStatus.Equals("3"))
            {
                return "审核通过";
            }
            else if (paraOrderStatus.Equals("4"))
            {
                return "生产中";
            }
            else if (paraOrderStatus.Equals("5"))
            {
                return "生产完成";
            }
            else if (paraOrderStatus.Equals("6"))
            {
                return "送货完成";
            }
            else
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 获取表 MK_ORDER 查询记录
        /// </summary>
        /// <param name="paraMOFSTMO">VM_MarketOrderForSearchTableMarketOrder 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_MarketOrderForShowMarketOrderInfo 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderForShowMarketOrderInfo> GetMarketOrderListRepository(VM_MarketOrderForSearchMarketOrderTable paraMOFSMOT, Paging paraPage)
        {
            IQueryable<MarketClientInformation> marketClientInformationIQ = base.GetList<MarketClientInformation>(); //获取表 MK_CLN_INFO 结果集
            IQueryable<MarketOrder> marketOrderIQ = base.GetList(); //获取表 MK_ORDER 结果集
            IQueryable<MarketOrderDetail> marketOrderDetailIQ = base.GetList<MarketOrderDetail>(); //获取表 MK_ORDER_DTL 结果集
            IQueryable<MasterDefiInfo> mdiOrderStatus = base.GetList<MasterDefiInfo>().Where(mdi => mdi.SectionCd.Equals("00029")); //获取表 BI_MASTER_DEFI_INFO 市场部订单状态结果集
            IQueryable<UserInfo> userInfoIQ = base.GetList<UserInfo>(); //获取表 BI_UserInfo 结果集

            //查询
            if (string.IsNullOrEmpty(paraMOFSMOT.ClientOrderID) == false)
            {
                marketOrderIQ = marketOrderIQ.Where(mo => mo.ClientOrderID.Contains(paraMOFSMOT.ClientOrderID));
            }
            if (string.IsNullOrEmpty(paraMOFSMOT.ProductID) == false)
            {
                marketOrderIQ = from moIQ in marketOrderIQ
                                join modIQ in marketOrderDetailIQ on moIQ.ClientOrderID equals modIQ.ClientOrderID into JoinMOMOD
                                where JoinMOMOD.Any(mod => mod.ProductID.Equals(paraMOFSMOT.ProductID))
                                select moIQ;
                //productInformationIQ = productInformationIQ.Where(pi => pi.ProdAbbrev.Contains(paraMOFSMOT.ProductAbbreviation));
            }
            if (string.IsNullOrEmpty(paraMOFSMOT.ClientID) == false)
            {
                marketOrderIQ = marketOrderIQ.Where(mo => mo.ClientID.Equals(paraMOFSMOT.ClientID));
            }
            if (string.IsNullOrEmpty(paraMOFSMOT.OrderStatus) == false)
            {
                marketOrderIQ = marketOrderIQ.Where(mo => mo.OrderStatus.Equals(paraMOFSMOT.OrderStatus));
            }
            if (string.IsNullOrEmpty(paraMOFSMOT.ProduceCellID) == false) 
            {
                marketOrderIQ = from moIQ in marketOrderIQ
                                join modIQ in marketOrderDetailIQ on moIQ.ClientOrderID equals modIQ.ClientOrderID into JoinMOMOD
                                where JoinMOMOD.Any(mod => mod.ProduceCellID.Equals(paraMOFSMOT.ProduceCellID))
                                select moIQ;
            }
            if (paraMOFSMOT.BeginDeliveryDate != null)
            {
                marketOrderIQ = marketOrderIQ.Where(mo => mo.DeliveryDate >= paraMOFSMOT.BeginDeliveryDate);
            }
            if (paraMOFSMOT.EndDeliveryDate != null)
            {
                marketOrderIQ = marketOrderIQ.Where(mo => mo.DeliveryDate <= paraMOFSMOT.EndDeliveryDate);
            }

            //多表连接
            IQueryable<VM_MarketOrderForShowMarketOrderInfo> resultIQ = from moIQ in marketOrderIQ
                                                                        join mciIQ in marketClientInformationIQ on moIQ.ClientID equals mciIQ.ClientID into JoinMOMOCI
                                                                        from mciIQ in JoinMOMOCI.DefaultIfEmpty()
                                                                        join uiaIQ in userInfoIQ on moIQ.ApprovalUserID equals uiaIQ.UId into JoinMOUIA
                                                                        from uiaIQ in JoinMOUIA.DefaultIfEmpty()
                                                                        join uie1IQ in userInfoIQ on moIQ.EditUserID1 equals uie1IQ.UId into JoinMOUIE1
                                                                        from uie1IQ in JoinMOUIE1.DefaultIfEmpty()
                                                                        join uie2IQ in userInfoIQ on moIQ.EditUserID2 equals uie2IQ.UId into JoinMOUIE2
                                                                        from uie2IQ in JoinMOUIE2.DefaultIfEmpty()
                                                                        join mdiosIQ in mdiOrderStatus on moIQ.OrderStatus equals mdiosIQ.AttrCd into JoinMOMDIOS
                                                                        from mdiosIQ in JoinMOMDIOS.DefaultIfEmpty()
                                                                        select new VM_MarketOrderForShowMarketOrderInfo
                                                                        {
                                                                            ClientOrderID = moIQ.ClientOrderID,
                                                                            ClientVersion = moIQ.ClientVersion,
                                                                            DeliveryDate = moIQ.DeliveryDate,
                                                                            ClientID = moIQ.ClientID,
                                                                            ClientName = mciIQ.ClientName,
                                                                            PackageRequire = moIQ.PackageRequire,
                                                                            PackageRequireImage1 = moIQ.PackageRequireImage1,
                                                                            PackageRequireImage2 = moIQ.PackageRequireImage2,
                                                                            PackageRequireImage3 = moIQ.PackageRequireImage3,
                                                                            PackageRequireImage4 = moIQ.PackageRequireImage4,
                                                                            PackageRequireImage5 = moIQ.PackageRequireImage5,
                                                                            OtherMatter = moIQ.OtherMatter,
                                                                            ApprovalFlag = moIQ.ApprovalFlag,
                                                                            ApprovalFlagName = null, //暂无用到
                                                                            ApprovalUserID = moIQ.ApprovalUserID,
                                                                            ApprovalUserName = uiaIQ.UName,
                                                                            ApprovalDate = moIQ.ApprovalDate,
                                                                            EditUserID1 = moIQ.EditUserID1,
                                                                            EditUserName1 = uie1IQ.UName,
                                                                            EditUserDate1 = moIQ.EditUserDate1,
                                                                            EditUserID2 = moIQ.EditUserID2,
                                                                            EditUserName2 = uie2IQ.UName,
                                                                            EditUserDate2 = moIQ.EditUserDate2,
                                                                            OrderProgressStatus = moIQ.OrderProgressStatus,
                                                                            OrderProgressStatusName = null, //暂无用到
                                                                            OrderStatus = moIQ.OrderStatus,
                                                                            OrderStatusName = mdiosIQ.AttrValue
                                                                        };
            paraPage.total = resultIQ.Count();
            IEnumerable<VM_MarketOrderForShowMarketOrderInfo> iEnumerableMOFSMOI = resultIQ.ToPageList<VM_MarketOrderForShowMarketOrderInfo>("ClientOrderID asc", paraPage);

            return iEnumerableMOFSMOI; //返回结果集

        } //end GetMarketOrderListRepositorys

        /// <summary>
        /// 根据 ClientOrderID、ClientVersion，获取相应对象
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <param name="paraClientVersion">版数</param>
        /// <returns>VM_MarketOrderForShowMarketOrderInfo 页面显示类</returns>
        /// 创建者：冯吟夷
        public VM_MarketOrderForShowMarketOrderInfo GetMarketOrderInfoRepository(string paraClientOrderID, string paraClientVersion)
        {
            /**** MarketOrder ****/
            MarketOrder mo = new MarketOrder(); 
            mo.ClientOrderID = paraClientOrderID; 
            mo.ClientVersion = Convert.ToDecimal(paraClientVersion);
            MarketOrder moResult = base.Find(mo);

            /**** MarketClientInformation ****/
            MarketClientInformation mci = new MarketClientInformation();
            mci.ClientID = moResult.ClientID; 
            MarketClientInformation mciResult = base.Find<MarketClientInformation>(mci);

            VM_MarketOrderForShowMarketOrderInfo mofsmoiResult = new VM_MarketOrderForShowMarketOrderInfo();
            mofsmoiResult.ClientOrderID = moResult.ClientOrderID;
            mofsmoiResult.ClientVersion = moResult.ClientVersion;
            mofsmoiResult.DeliveryDate = moResult.DeliveryDate;
            mofsmoiResult.ClientID = moResult.ClientID;
            mofsmoiResult.ClientName = mciResult.ClientName;
            mofsmoiResult.PackageRequire = moResult.PackageRequire;
            mofsmoiResult.PackageRequireImage1 = moResult.PackageRequireImage1;
            mofsmoiResult.PackageRequireImage2 = moResult.PackageRequireImage2;
            mofsmoiResult.PackageRequireImage3 = moResult.PackageRequireImage3;
            mofsmoiResult.PackageRequireImage4 = moResult.PackageRequireImage4;
            mofsmoiResult.PackageRequireImage5 = moResult.PackageRequireImage5;
            mofsmoiResult.OtherMatter = moResult.OtherMatter;
            mofsmoiResult.ApprovalFlag = moResult.ApprovalFlag;
            mofsmoiResult.ApprovalFlagName = GetMarketApprovalFlagName(moResult.ApprovalFlag); //以后修改 GetMarketApprovalFlagName 函数
            mofsmoiResult.ApprovalUserID = moResult.ApprovalUserID;
            mofsmoiResult.ApprovalUserName = string.IsNullOrEmpty(moResult.ApprovalUserID) ? null : GetUserInfoModel(moResult.ApprovalUserID).UName;
            mofsmoiResult.ApprovalDate = moResult.ApprovalDate;
            mofsmoiResult.EditUserID1 = moResult.EditUserID1;
            mofsmoiResult.EditUserName1 = string.IsNullOrEmpty(moResult.EditUserID1) ? null : GetUserInfoModel(moResult.EditUserID1).UName;
            mofsmoiResult.EditUserDate1 = moResult.EditUserDate1;
            mofsmoiResult.EditUserID2 = moResult.EditUserID2;
            mofsmoiResult.EditUserName2 = string.IsNullOrEmpty(moResult.EditUserID2) ? null : GetUserInfoModel(moResult.EditUserID2).UName;
            mofsmoiResult.EditUserDate2 = moResult.EditUserDate2;
            mofsmoiResult.OrderProgressStatus = moResult.OrderProgressStatus;
            mofsmoiResult.OrderProgressStatusName = GetMarketOrderProgressStatusName(moResult.OrderProgressStatus); //以后修改 GetMarketOrderProgressStatusName 函数
            mofsmoiResult.OrderStatus = moResult.OrderStatus;
            mofsmoiResult.OrderStatusName = GetMarketOrderStatusName(moResult.OrderStatus); //以后修改 GetMarketOrderStatusName 函数

            return mofsmoiResult; //返回结果
        } //end GetMarketOrderModelRepository



    } //end MarketOrderRepositoryImp
}
