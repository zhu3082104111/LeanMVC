using Model;
using Repository;
using Repository.Base;
/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AutoCreateOddNoServiceImp.cs
// 文件功能描述：
//          各种单据号自动生成的共通接口的实现类
//          该接口中的所有方法，只允许在App_UI层的Controller类里调用。
//      
// 创建标识：2013/11/21 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    /// <summary>
    /// 各种单据号自动生成的共通接口的实现类
    /// </summary>
    public class AutoCreateOddNoServiceImp : AbstractService, IAutoCreateOddNoService
    {
        // 自动开始值
        private const string AUTO_START = "1";

        // 声明编码生成表的Repository接口
        private ICodeMakeRepository makeAutoNoRepository;

        /// <summary>
        /// 各种单据号自动生成的共通类的声明函数
        /// </summary>
        public AutoCreateOddNoServiceImp()
        {
        }

        /// <summary>
        /// 各种单据号自动生成的共通类的声明函数
        /// </summary>
        /// <param name="makeAutoNoRepository">编码生成表的Repository接口</param>
        public AutoCreateOddNoServiceImp(ICodeMakeRepository makeAutoNoRepository)
        {
            this.makeAutoNoRepository = makeAutoNoRepository;
        }

        /// <summary>
        /// 外购单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>外购单号</returns>
        public string getNextOutOrderNo(string userId)
        {
            // 外购单号
            string outOrderNo;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.PURCHASE_ORDER, Constant .ProdUnit.DEFAULT, yymmdd, userId);

            // 外购单号 = 当前日期（YYMMDD）+ 外购标示(WG) +  自动序列号
            outOrderNo = yymmdd + Constant.OrderMarked.PURCHASE_ORDER + autoNo;

            return outOrderNo;
        }

        /// <summary>
        /// 外协加工调度单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>外协加工调度单号</returns>
        public string getNextSuppOrderNo(String userId)
        {
            // 外协加工调度单号
            string suppOrderNo;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.SUPPLIER_ORDER, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 外购加工调度单号 = 当前日期（YYMMDD）+ 外协标示(WX) +  自动序列号
            suppOrderNo = yymmdd + Constant .OrderMarked.SUPPLIER_ORDER + autoNo;

            return suppOrderNo;
        }

        /// <summary>
        /// 送货单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>送货单号</returns>
        public string getNextDeliveryOrderNo(String userId)
        {
            // 送货单号
            string deliveryOrderNo;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.DELIVERY_ORDER, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 送货单号 = 当前日期（YYMMDD）+ 送货单标示(SH) +  自动序列号
            deliveryOrderNo = yymmdd + Constant.OrderMarked.DELIVERY_ORDER + autoNo;

            return deliveryOrderNo;
        }

        /// <summary>
        /// 内部成品送货单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <param name="ProdUnit">生产单元</param>
        /// <returns>内部成品送货单号</returns>
        public string getNextInerFinOutOrderNo(String userId, String ProdUnit)
        {
            // 内部成品送货单号
            string InerFinOutNo;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.INER_FIN_OUT_ORDER, ProdUnit, yymmdd, userId);

            // 送货单号 =  内部成品送货单标示(NS) + 当前日期（YYMMDD）+  自动序列号
            InerFinOutNo = Constant.OrderMarked.INER_FIN_OUT_ORDER + yymmdd + autoNo;

            return InerFinOutNo;
        }

        /// <summary>
        /// 报废单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <param name="ProdUnit">生产单元</param>
        /// <returns>报废单号</returns>
        public string getNextDiscardOrderNo(String userId, String ProdUnit)
        {
            // 报废单号
            string DiscardNo;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.DISCARD_ORDER, ProdUnit, yymmdd, userId);

            // 报废单号 =  报废单号(BF) + 当前日期（YYMMDD）+  自动序列号
            DiscardNo = Constant.OrderMarked.DISCARD_ORDER + yymmdd + autoNo;

            return DiscardNo;
        }

        /// <summary>
        /// 获取自动生成的No
        /// </summary>
        /// <param name="codetype">编码区分</param>
        /// <param name="deparId">部门区分</param>
        /// <param name="yymmdd">年月日</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        private string getAutoNo(string codetype, string deparId, string yymmdd, string userId)
        {
            try
            {
                // 自动生产序号的位数
                int padLength = 5;

                // 编码区分的判断
                switch(codetype){
                    // TODO：各种单据根据自己的情况来确定自动生产序号的位数
                    //case Constant.CodeType.PURCHASE_ORDER:
                    //    padLength = 5;
                    //    break;
                    // 默认值
                    default:
                        padLength = 5;
                        break;
                }

                // 根据主Key，取得数据库中当前值
                CodeMake maxCode = makeAutoNoRepository.getCodeMakeByKey(codetype, deparId, yymmdd);

                // 数据库中无数据时
                if (maxCode == null)
                {
                    // 实例化CodeMake类
                    maxCode = new CodeMake();

                    // 插入数据的设定
                    // 编码区分
                    maxCode.CdCatg = codetype;
                    // 部门区分
                    maxCode.DepartId = deparId;
                    // 日期
                    maxCode.Date = yymmdd;
                    // 序号
                    maxCode.OdNum = AUTO_START.PadLeft(padLength, '0');
                    // 创建人
                    maxCode.CreUsrID = userId;
                    // 修改人
                    maxCode.UpdUsrID = userId;
                    // 修改日期
                    maxCode.UpdDt = DateTime.Now;

                    // 插入数据
                    bool ret = makeAutoNoRepository.Add(maxCode);
                    if (ret == false) {
                        throw new Exception("自动序号取得时，发生错误。");
                    }

                    // 返回初始值
                    return maxCode.OdNum;

                }
                // 数据库中有数据时。
                else
                {
                    // 获取当前序号，自动加1
                    int maxNo = Int32.Parse(maxCode.OdNum);
                    maxNo = maxNo + 1;

                    // 更新数据的设定
                    // 序号
                    maxCode.OdNum = maxNo.ToString().PadLeft(padLength, '0');
                    // 创建人
                    maxCode.CreUsrID = userId;
                    // 修改人
                    maxCode.UpdUsrID = userId;
                    // 修改日期
                    maxCode.UpdDt = DateTime.Now;

                    // 更新数据
                    bool ret = makeAutoNoRepository.Update(maxCode);
                    if (ret == false)
                    {
                        throw new Exception("自动序号取得时，发生错误。");
                    }

                    // 返回当前值
                    return maxCode.OdNum;
                }
            }
            catch
            {
                throw new Exception("自动序号取得时，发生错误。");
            }
        }

        /// <summary>
        /// 自动生成5位序号
        /// 临时方法，最终要删除
        /// </summary>
        /// <returns></returns>
        private string getAutoNoOf5Median()
        {
            int no = 0;
            if (no <= 99999)
            {
                no++;
            }
            return  string.Format("{0:00000}", no);
        }

        /// <summary>
        /// 自动生成5位序号
        /// 临时方法，最终要删除
        /// </summary>
        /// <returns></returns>
        private string getAutoNoOf4Median()
        {
            int no = 0;
            if (no <= 9999)
            {
                no++;
            }
            return string.Format("{0:0000}", no);
        }

        /// <summary>
        /// 根据公司ID取得公司简称
        /// </summary>
        /// <param name="compID">公司ID</param>
        /// <returns>公司简称</returns>
        private string getAbbreviationByCompID(String compID)
        {
            return "LDK";
        }

        /// <summary>
        /// 获取当前时间（YYMMDD形式）
        /// </summary>
        /// <returns>返回当前时间</returns>
        private string getSystemDateYYMMDD()
        {
            return DateTime.Now.ToString("yyMMdd");
        }


        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string getSystemDate(int length)
        {
            string systemDate = "";
            DateTime sysDate = DateTime.Now;
            if (length == 4)
            {
                systemDate = sysDate.ToString("YYMM");
            }
            else if (length == 6)
            {
                systemDate = sysDate.ToString("YYMMdd");
            }

            return systemDate;
        }

        #region 朱静波()
        /// <summary>
        /// 总装调度单号自动生成函数
        /// </summary>
        /// <returns>总装调度单号</returns>
        public string GetAssemblyDispatchID(String userId)
        {
            // 总装调度单号
            string dispatchId;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.ASSEMBLY_DISPATCH_ID, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 总装调度单号 = 当前日期（YYMMDD）6+ 票据识别码(ZD)2 +  自动序列号5
            dispatchId = yymmdd + Constant.OrderMarked.ASSEMBLY_DISPATCH_ID + autoNo;

            return dispatchId;
        }

        /// <summary>
        /// 总装工票号自动生成函数
        /// </summary>
        /// <returns>总装工票号</returns>
        public string GetAssembBillID(String userId)
        {
            // 总装工票号
            string assembBillID;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.ASSEM_BILL_ID, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 总装工票号 = 当前日期（YYMMDD）6+ 票据识别码(ZD)2 +  自动序列号5
            assembBillID = yymmdd + Constant.OrderMarked.ASSEM_BILL_ID + autoNo;

            return assembBillID;
        }

        /// <summary>
        /// 加工送货单号自动生成函数
        /// </summary>
        /// <returns>加工送货单号</returns>
        public string GetProcDelivID(String userId)
        {
            // 加工送货单
            string procDelivID;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.PROCDELIV_ID, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 加工送货单  = 当前日期（YYMMDD）6+ 票据识别码(JS)2 +  自动序列号5
            procDelivID = yymmdd + Constant.OrderMarked.PROCDELIV_ID + autoNo;

            return procDelivID;
        }

        #endregion

        #region 20131212 杜兴军 

        /// <summary>
        /// 加工流转卡号自动生成函数
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public string GetProcessTranslateCardId(string userId)
        {
            // 加工流转卡号
            string translateCardId=null;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.TRANSLATE_CARD_ID, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 加工流转卡号 = 当前日期（YYMMDD）6+ 票据识别码(JL)2 +  自动序列号5
            translateCardId = yymmdd + Constant.OrderMarked.TRANSLATE_CARD_ID + autoNo;

            return translateCardId;
        }

        /// <summary>
        /// 成品交仓单号自动生成函数
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public string GetProdWarehouseId(string userId)
        {
            // 成品交仓单号
            string prodWarehosueId=null;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.PROD_WAREHOUSE_ID, Constant.ProdUnit.DEFAULT, yymmdd, userId);

            // 成品交仓单号 = 当前日期（YYMMDD）6+ 票据识别码(NS)2 +  自动序列号5
            prodWarehosueId = yymmdd + Constant.OrderMarked.PROD_WAREHOUSE_ID + autoNo;

            return prodWarehosueId;

        }

        #endregion


        /// <summary>
        /// 生成生产领料单号
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetPickingMateriaRequestId(string UserId)
        {
            // 加工送货单
            string id;

            // 当前日期
            string yymmdd = getSystemDateYYMMDD();

            // 自动序列号
            string autoNo = getAutoNo(Constant.CodeType.PICKING_MATERIAREQUEST, Constant.ProdUnit.DEFAULT, yymmdd, UserId);

            // 加工送货单  = 当前日期（YYMMDD）6+ 票据识别码(JS)2 +  自动序列号5
            id = yymmdd + Constant.OrderMarked.PROCDELIV_ID + autoNo;

            return id;
        }
    }
}
