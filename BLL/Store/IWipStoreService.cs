/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWipStoreService.cs
// 文件功能描述：
//          仓库部门在制品库Service接口类
//      
// 修改履历：2013/11/15 杨灿 新建
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
    public interface IWipStoreService
    {

        //WipStore
        //接口方法

        /// <summary>
        /// 按条件查询用户并分页
        /// </summary>
        /// <param name="user">待查找的用户的信息</param>
        /// <param name="paging">分页</param>
        /// <param name="total">满足条件的总数</param>
        /// <returns></returns>
        /// 
        //在制品库待入库一览初始化页面
        IEnumerable GetWipInStoreBySearchByPage(VM_WipInStoreForSearch wipInStoreForSearch, Paging paging);

        //在制品库入库登录画面数据表示
        IEnumerable GetWipInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging);

        //在制品库入库履历一览初始化页面
        IEnumerable GetWipInRecordBySearchByPage(VM_WipInRecordStoreForSearch wipInRecordStoreForSearch, Paging paging);

        //在制品库入库登录业务
        [TransactionAop]
        bool WipInForLogin(List<VM_WipInLoginStoreForTableShow> wipInLoginStore);

        //在制品库入库登录添加入库履历数据
        void WipInForLoginAddInRecord(VM_WipInLoginStoreForTableShow wipInLoginStore);

        //在制品库入库登录修改仓库预约表、修改仓库预约详细表、外购单明细表和外协加工调度单详细表
        void WipInForLoginUpdateReserve(VM_WipInLoginStoreForTableShow wipInLoginStore, string pdtSpecState);

        //在制品库入库登录修改仓库表
        void WipInForLoginUpdateMaterial(VM_WipInLoginStoreForTableShow wipInLoginStore);

        //在制品库入库登录添加批次别库存表
        void WipInForLoginAddBthStockList(VM_WipInLoginStoreForTableShow wipInLoginStore);

        //在制品库入库登录添加让步仓库表
        void WipInForLoginAddGiMaterial(VM_WipInLoginStoreForTableShow wipInLoginStore);

        //在制品库入库履历一览删除功能
        [TransactionAop]
        string WipInRecordForDel(List<VM_WipInRecordStoreForTableShow> wipInRecordStore);

        //在制品库入库履历一览(删除功能)修改履历表
        void WipInRecordForDelInRecord(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品库入库履历一览(删除功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表
        void WipInRecordForDelReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState);

        //在制品库入库履历一览（删除功能）修改仓库表
        void WipInRecordForDelMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品库入库履历一览（删除功能）删除让步仓库表
        void WipInRecordForDelGiMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品库入库履历一览修改功能
        [TransactionAop]
        bool WipInRecordForUpdate(List<VM_WipInRecordStoreForTableShow> wipInRecordStore);

        //在制品入库履历一览(修改功能)修改履历表
        void WipInRecordForUpdateInRecord(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表
        void WipInRecordForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState);

        //在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表（添加）
        void WipInRecordAddForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState, decimal addQty );

        //在制品库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外协加工调度单详细表（减去）
        void WipInRecordDDelForUpdateReserve(VM_WipInRecordStoreForTableShow wipInRecordStore, string pdtSpecState, decimal delQty);

        //在制品入库履历一览（修改功能）修改仓库表
        void WipInRecordForUpdateMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品入库履历一览（修改功能）修改批次别库存表
        void WipInRecordForUpdateBthStockList(VM_WipInRecordStoreForTableShow wipInRecordStore);

        //在制品库入库履历一览（修改功能）修改让步仓库表
        void WipInRecordForUpdateGiMaterial(VM_WipInRecordStoreForTableShow wipInRecordStore);

        #region 待出库一览(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)待出库一览结果集
        /// </summary>
        /// <param name="wipOutStoreForSearch">VM_WipOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetWipOutStoreBySearchByPage(VM_WipOutStoreForSearch wipOutStoreForSearch, Paging paging);

        #endregion

        #region 出库单打印选择(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipOutPrintForSearch">VM_WipOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetWipOutPrintBySearchByPage(VM_WipOutPrintForSearch wipOutPrintForSearch, Paging paging);

        #endregion

        #region 材料领用出库单(在制品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_WipOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        VM_WipOutPrintIndexForInfoShow GetWipOutPrintForInfoShow(string pickListID);

        /// <summary>
        /// 根据 WipOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="wipOutDetailRecordList">WipOutDetailRecord 泛型结果集</param>
        /// <returns>VM_WipOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        List<VM_WipOutPrintIndexForTableShow> GetWipOutPrintForTableShow(List<WipOutDetailRecord> wipOutDetailRecordList);

        #endregion

        /// <summary>
        /// 在制品出库登录画面数据表示 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>在制品库出库登录画面数据集合</returns>
        IEnumerable GetWipOutStoreForLoginBySearchByPage(string pickListID, string materielID,string materReqDetailNo, Paging paging);

        /// <summary>
        /// 在制品出库登录保存 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        [TransactionAop]
        bool WipOutForLogin(List<VM_WipOutLoginStoreForTableShow> wipOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginAddOutRecord(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList);

        //在制品出库履历一览初始化页面
        IEnumerable GetWipOutRecordBySearchByPage(VM_WipOutRecordStoreForSearch wipOutRecordStoreForSearch, Paging paging);

        //在制品库出库履历一览删除功能
        [TransactionAop]
        bool WipOutRecordForDel(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore);

        //在制品库出库履历一览修改功能
        [TransactionAop]
        bool WipOutRecordForUpdate(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore);

        IEnumerable GetWipStoreBySearchByPage(WipStore WipStore, Paging paging);

        IEnumerable GetWipStoreBySearchById(string id);

        bool UpdateWipStore(List<string> list);

        //=======================================================
        //在制品库入库登录暂用方法（一期测试）
        bool WipInForLoginTest(List<VM_WipInLoginStoreForTableShow> wipInLoginStore);

        //在制品库履历删除暂用方法（一期测试）
        string WipInRecordForDelTest(List<VM_WipInRecordStoreForTableShow> wipInRecordStore);

        string WipOutRecordForDelTest(List<VM_WipOutRecordStoreForTableShow> wipOutRecordStore);
        //=======================================================

        bool UpdateWipStore(WipStore WipStore);

        IEnumerable SelectWipStore(string pid);

        bool AddWipStore(WipStore WipStore);

        IEnumerable SelectWipStoreForId(string pid, Extensions.Paging paging);

        /// <summary>
        /// 出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        IEnumerable SelectWipStoreForBthSelect(decimal qty, string pdtID, string pickListID,string materReqDetailNo,string osSupProFlg,Paging paging);

        //入库单打印选择画面显示
        IEnumerable GetWipInPrintBySearchByPage(VM_WipInPrintForSearch wipInPrintForSearch, Paging paging);

        //加工产品入库单显示
        IEnumerable SelectForWipInPrintPreview(string pdtID, string deliveryOrderID, Paging paging);

        /// <summary>
        /// 在制品库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginUpdateReserve(VM_WipOutLoginStoreForTableShow wipOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginUpdateMaterial(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginUpdateGiMaterial(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginUpdateGiReserve(VM_WipOutLoginStoreForTableShow wipOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void WipOutForLoginUpdateBthStockList(VM_WipOutLoginStoreForTableShow wipOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 在制品库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="wipOutLoginStore">出库登录画面数据</param>
        void WipOutForLoginUpdateMaterReq(VM_WipOutLoginStoreForTableShow wipOutLoginStore);
    } //end IWipStoreService
}
