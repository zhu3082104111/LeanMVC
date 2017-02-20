/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProductSchedulingService.cs
// 文件功能描述：订单产品排产service接口
// 
// 创建标识：201311 梁龙飞
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
    /// <summary>
    /// 订单产品排产接口
    /// 梁龙飞
    /// </summary>
    public interface IProductSchedulingService
    {
        //#region 测试接口，C：梁龙飞
        //IEnumerable<MaterialDecompose> GeneralMatDecompose(string orderClientID, string orderDetail, string version);
        //#endregion

        /// <summary>
        /// 主键俱全下，更新不为空的列
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        bool UpdateNotNullColumn(MaterialDecompose target);

        /// <summary>
        /// 根据主键返回一条排产信息
        /// </summary>
        /// <param name="traget"></param>
        /// <returns></returns>
        MaterialDecompose GetSingleDecopose(MaterialDecompose traget);

        /// <summary>
        /// 根据订单号，订单详细，物料名 更新材料规格
        /// </summary>
        /// <param name="traget"></param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateSepcification(MaterialDecompose traget);

        /// <summary>
        /// 获取一个产品的排产信息,物料分解信息库中不存在时返回null
        /// </summary>
        /// <param name="orderClientID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        IEnumerable<VM_ProductSchedulingShow> GetProDecomInfo(string orderClientID, string orderDetail, string version);

        /// <summary>
        /// 排产并插入到数据库,返回排产结果（在插入前对计划状态确认，T.status!=未排产，抛出异常）
        /// </summary>
        /// <param name="clientOrderID">客户订单号</param>
        /// <param name="orderDetail">订单明细</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        [TransactionAop]
        IEnumerable<MaterialDecompose> InsertProDecomInfo(string clientOrderID, string orderDetail, string version);

        /// <summary>
        /// 订单产品排产
        /// </summary>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [TransactionAop]
        IEnumerable<VM_ProductSchedulingShow> SchedulingOnePlan(string clientOrderID, string orderDetail, string version);

        #region 多方案功能集 C:梁龙飞（所有重复功能需要提供版本号）
        /// <summary>
        /// 版本号：1.1
        /// 获取单独一个物料的排产计划显示
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        VM_ProductSchedulingShow GetSingleMatScheduling(MaterialDecompose target);

        /// <summary>
        /// 版本号：1.2
        /// 获取格式化后的排产信息：某些信息并没有保存在物料分解表中，而是经过上下文计算而出
        /// </summary>
        /// <param name="ids">需要返回的行号：对应MatSequence</param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        IEnumerable<VM_ProductSchedulingShow> GetFormatedSchedulingInf(List<int> ids, string clientOrderID, string orderDetail,string version);

        #endregion
        /// <summary>
        /// 排产更新，只是更新计划分解信息，不更新计划的状态
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        bool UpdateProDecomInfo(List<VM_ProductSchedulingShow> targets);

        /// <summary>
        /// 更新整个产品下所有物料以下排产信息：自产、外协、外购，提供日期、计划天数、启动日期
        /// 事务性操作
        /// 并不下发投料任务
        /// </summary>
        /// <param name="matDecomList"></param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateCommission(List<MaterialDecompose> matDecomList);

        /// <summary>
        /// 确认排产：从数据库中取数据进行排产，在排产前会对数据进行检测
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        [TransactionAop]
        bool ConfirmSchedule(string clientOrderID, string clientOrderDetail, string version);


    }
}
