/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISemInStoreService.cs
// 文件功能描述：
//          半成品库入库相关画面Service接口类
//
// 修改履历：2013/12/07 汪腾飞 新建
 *           2013/12/26 杨灿 修改
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 半成品库入库相关画面Service接口类
    /// </summary>
    public interface ISemStoreService
    {
        /// <summary>
        /// 半成品库待入库一览数据显示
        /// </summary>
        /// <param name="semInPrintForSearch">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        IEnumerable GetSemStoreBySearchByPage(VM_SemInStoreForSearch semInPrintForSearch, Paging paging);

        //半成品入库打印选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semInPrintForSearch"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable GetSemInPrintBySearchByPage(VM_SemInPrintForSearch semInPrintForSearch, Paging paging);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdtID"></param>
        /// <param name="deliveryOrderID"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable SelectSemStore(string pdtID, string deliveryOrderID, Paging paging);
        //半成品库入库履历一览初始化页面
        /// <summary>
        /// 
        /// </summary>
        /// <param name="semInRecordStoreForSearch"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable GetSemInRecordBySearchByPage(VM_SemInRecordStoreForSearch semInRecordStoreForSearch, Paging paging);

        //半成品库出库履历一览初始化页面（yc添加）
        IEnumerable GetSemOutRecordBySearchByPage(VM_SemOutRecordStoreForSearch semOutRecordStoreForSearch, Paging paging);

       //半成品库入库登陆初始化页面
        IEnumerable GetSemInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging);
        
        //半成品库入库登录业务
        [TransactionAop]
        bool SemInForLogin(List<VM_SemInLoginStoreForTableShow> semInLoginStore);

        //半成品库入库登录添加入库履历数据
        void SemInForLoginAddInRecord(VM_SemInLoginStoreForTableShow semInLoginStore);

        //半成品库入库登录修改仓库预约表、修改仓库预约详细表和外协加工调度单详细表
        void SemInForLoginUpdateReserve(VM_SemInLoginStoreForTableShow semInLoginStore, string pdtSpecState);

        //半成品库入库登录修改仓库表
        void SemInForLoginUpdateMaterial(VM_SemInLoginStoreForTableShow semInLoginStore);

        //半成品库入库登录添加批次别库存表
        void SemInForLoginAddBthStockList(VM_SemInLoginStoreForTableShow semInLoginStore);

        //半成品库入库登录添加让步仓库表
        void SemInForLoginAddGiMaterial(VM_SemInLoginStoreForTableShow semInLoginStore);


        //=======================================================
        //半成品库履历删除暂用方法（一期测试）
        string SemInRecordForDelTest(List<VM_SemInRecordStoreForTableShow> semInRecordStore);
        //=======================================================

        //=========================================================
        //半成品库入库登陆保存功能暂用方法（一期测试）
        string SemInStoreForDelTest(List<VM_SemInLoginStoreForTableShow> semInStore);


        #region 待出库一览(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)待出库一览结果集
        /// </summary>
        /// <param name="semOutStoreForSearch">VM_SemOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetSemOutStoreBySearchByPage(VM_SemOutStoreForSearch semOutStoreForSearch, Paging paging);

        #endregion

        #region 出库单打印选择(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)出库单打印选择结果集
        /// </summary>
        /// <param name="semOutPrintForSearch">VM_SemOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetSemOutPrintBySearchByPage(VM_SemOutPrintForSearch semOutPrintForSearch, Paging paging);

        #endregion

        #region 材料领用出库单(半成品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_SemOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        VM_SemOutPrintIndexForInfoShow GetSemOutPrintForInfoShow(string pickListID);

        /// <summary>
        /// 根据 SemOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="semOutDetailRecordList">SemOutDetailRecord 泛型结果集</param>
        /// <returns>VM_SemOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        List<VM_SemOutPrintIndexForTableShow> GetSemOutPrintForTableShow(string pickListID, List<SemOutDetailRecord> semOutDetailRecordList);

        #endregion

        /// <summary>
        /// 半成品库出库登录画面数据表示 陈健
        /// </summary>
        /// <param name="materReqNO">领料单号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        IEnumerable GetSemOutStoreForLoginBySearchByPage(string materReqNO, Paging paging);

        /// <summary>
        /// 半成品库出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        IEnumerable SelectSemStoreForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging);

        /// <summary>
        /// 半成品出库登录保存 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        bool SemOutForLogin(List<VM_SemOutLoginStoreForTableShow> semOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginAddOutRecord(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginUpdateReserve(VM_SemOutLoginStoreForTableShow semOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginUpdateMaterial(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginUpdateGiMaterial(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginUpdateGiReserve(VM_SemOutLoginStoreForTableShow semOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void SemOutForLoginUpdateBthStockList(VM_SemOutLoginStoreForTableShow semOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 半成品库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="semOutLoginStore">出库登录画面数据</param>
        void SemOutForLoginUpdateMaterReq(VM_SemOutLoginStoreForTableShow semOutLoginStore);

    } //end ISemStoreService
}
