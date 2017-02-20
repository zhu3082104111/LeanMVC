using Repository;
/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAutoCreateOddNoService.cs
// 文件功能描述：
//          各种单据号自动生成的共通接口
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
    /// 各种单据号自动生成的共通接口
    /// </summary>
    public interface IAutoCreateOddNoService
    {
        /// <summary>
        /// 外购单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>外购单号</returns>
        [TransactionAop]
        string getNextOutOrderNo(string userId);

        /// <summary>
        /// 外协加工调度单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>外协加工调度单号</returns>
        [TransactionAop]
        string getNextSuppOrderNo(String userId);

        /// <summary>
        /// 送货单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <returns>送货单号</returns>
        [TransactionAop]
        string getNextDeliveryOrderNo(String userId);

        /// <summary>
        /// 总装调度单号自动生成函数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>总装调度单号</returns>
        /// 朱静波 创建
        [TransactionAop]
        string GetAssemblyDispatchID(String userId);

        /// <summary>
        /// 总装工票号自动生成函数
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>总装工票号</returns>
        /// 朱静波 创建
        [TransactionAop]
        string GetAssembBillID(String userId);

        /// <summary>
        /// 加工送货单号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>加工送货单号</returns>
        /// 朱静波 创建
        [TransactionAop]
        string GetProcDelivID(String userId);


        /// <summary>
        /// 内部成品送货单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <param name="ProdUnit">生产单元</param>
        /// <returns>内部成品送货单号</returns>
        string getNextInerFinOutOrderNo(String userId, String ProdUnit);
        
         /// <summary>
        /// 报废单号自动生成函数
        /// </summary>
        /// <param name="userId">登录用户ID</param>
        /// <param name="ProdUnit">生产单元</param>
        /// <returns>报废单号</returns>
        string getNextDiscardOrderNo(String userId, String ProdUnit);

        /// <summary>
        /// 加工流转卡号自动生成函数
        /// 20131230 杜兴军
        /// </summary>
        /// <param name="userId">登陆用户ID</param>
        /// <returns></returns>
        [TransactionAop]
        string GetProcessTranslateCardId(string userId);


        /// <summary>
        /// 生产领料单号自动生成函数
        /// 代东泽 20131230
        /// </summary>
        /// <returns></returns>
        [TransactionAop]
        string GetPickingMateriaRequestId(string UserId);

        /// <summary>
        /// 成品交仓单号自动生成函数
        /// 20131230 杜兴军
        /// </summary>
        /// <param name="userId">登陆用户ID</param>
        /// <returns></returns>
        [TransactionAop]
        string GetProdWarehouseId(string userId);
    }
}
