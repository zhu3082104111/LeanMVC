/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccStoreService.cs
// 文件功能描述：
//          仓库部门附件库Service接口类
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
    public interface IAccStoreService
    {

        //AccStore
        //接口方法

        /// <summary>
        /// 按条件查询用户并分页
        /// </summary>
        /// <param name="user">待查找的用户的信息</param>
        /// <param name="paging">分页</param>
        /// <param name="total">满足条件的总数</param>
        /// <returns></returns>
        /// 
        //附件库待入库一览初始化页面
        IEnumerable GetAccInStoreBySearchByPage(VM_AccInStoreForSearch accInStoreForSearch, Paging paging);

        //附件库入库登录画面数据表示
        IEnumerable GetAccInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging);

        //附件库入库履历一览初始化页面
        IEnumerable GetAccInRecordBySearchByPage(VM_AccInRecordStoreForSearch accInRecordStoreForSearch, Paging paging);

        IEnumerable GetWipStoreBySearchByPageTest(string mcIsetInListID, string isetRepID, Paging paging);

        bool UpdateWipStore(List<string> list);

        bool UpdateWipStore(WipStore wipStore);
        //=======================================================
        //附件库入库登录暂用方法（一期测试）
        bool AccInForLoginTest(List<VM_AccInLoginStoreForTableShow> accInLoginStore);

        //附件库履历删除暂用方法（一期测试）
        string AccInRecordForDelTest(List<VM_AccInRecordStoreForTableShow> accInRecordStore);

        string AccOutRecordForDelTest(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore);
        //=======================================================
        bool AddAccStore(WipStoreDetil wipStoreDetil);

        //附件库入库登录业务
        [TransactionAop]
        bool AccInForLogin(List<VM_AccInLoginStoreForTableShow> accInLoginStore);

        //附件库入库登录添加入库履历数据
        void AccInForLoginAddInRecord(VM_AccInLoginStoreForTableShow accInLoginStore);

        //附件库入库登录修改仓库预约表、修改仓库预约详细表和外购单明细表
        void AccInForLoginUpdateReserve(VM_AccInLoginStoreForTableShow accInLoginStore, string pdtSpecState);

        //附件库入库登录修改外购单明细表
        void AccInForLoginUpdateMCOutSourceOrderDetail(VM_AccInLoginStoreForTableShow accInLoginStore, string pdtSpecState);

        //附件库入库登录修改仓库表
        void AccInForLoginUpdateMaterial(VM_AccInLoginStoreForTableShow accInLoginStore);

        //附件库入库登录添加批次别库存表
        void AccInForLoginAddBthStockList(VM_AccInLoginStoreForTableShow accInLoginStore);

        //附件库入库登录添加让步仓库表
        void AccInForLoginAddGiMaterial(VM_AccInLoginStoreForTableShow accInLoginStore);

        //附件库入库履历一览删除功能
        [TransactionAop]
        string AccInRecordForDel(List<VM_AccInRecordStoreForTableShow> accInRecordStore);

        //附件库入库履历一览(删除功能)修改履历表
        void AccInRecordForDelInRecord(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览(删除功能)修改仓库预约表、仓库预约详细表和外购单明细表
        void AccInRecordForDelReserve(VM_AccInRecordStoreForTableShow accInRecordStore,string pdtSpecState);

        //附件库入库履历一览（删除功能）修改仓库表
        void AccInRecordForDelMaterial(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览（删除功能）删除让步仓库表
        void AccInRecordForDelGiMaterial(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览修改功能
        [TransactionAop]
        bool AccInRecordForUpdate(List<VM_AccInRecordStoreForTableShow> accInRecordStore);       

        //附件库入库履历一览(修改功能)修改履历表
        void AccInRecordForUpdateInRecord(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外购单明细表
        void AccInRecordForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore, string pdtSpecState);

        //附件库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外购单明细表（添加）
        void AccInRecordAddForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore, string pdtSpecState, decimal addQty);

        //附件库入库履历一览(修改功能)修改仓库预约表、仓库预约详细表和外购单明细表（减去）
        void AccInRecordDelForUpdateReserve(VM_AccInRecordStoreForTableShow accInRecordStore, string pdtSpecState, decimal delQty);

        //附件库入库履历一览（修改功能）修改仓库表
        void AccInRecordForUpdateMaterial(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览（修改功能）修改批次别库存表
        void AccInRecordForUpdateBthStockList(VM_AccInRecordStoreForTableShow accInRecordStore);

        //附件库入库履历一览（修改功能）修改让步仓库表
        void AccInRecordForUpdateGiMaterial(VM_AccInRecordStoreForTableShow accInRecordStore);

        #region 待出库一览(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)待出库一览结果集
        /// </summary>
        /// <param name="accOutStoreForSearch">VM_AccOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetAccOutStoreBySearchByPage(VM_AccOutStoreForSearch accOutStoreForSearch, Paging paging);

        #endregion

        #region 出库单打印选择(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)出库单打印选择结果集
        /// </summary>
        /// <param name="accOutPrintForSearch">VM_AccOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        IEnumerable GetAccOutPrintBySearchByPage(VM_AccOutPrintForSearch accOutPrintForSearch, Paging paging);

        #endregion

        #region 材料领用出库单(附件库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_AccOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        VM_AccOutPrintIndexForInfoShow GetAccOutPrintForInfoShow(string pickListID);

        /// <summary>
        /// 根据 AccOutDetailRecord 泛型结果集，获取相关信息
        /// </summary>
        /// <param name="accOutDetailRecordList">AccOutDetailRecord 泛型结果集</param>
        /// <returns>VM_AccOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        List<VM_AccOutPrintIndexForTableShow> GetAccOutPrintForTableShow(List<AccOutDetailRecord> accOutDetailRecordList);

        #endregion

        /// <summary>
        /// 附件库出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        IEnumerable GetAccOutStoreForLoginBySearchByPage(string pickListID, string materielID,string pickListDetNo, Paging paging);

        //附件库出库履历一览初始化页面
        IEnumerable GetAccOutRecordBySearchByPage(VM_AccOutRecordStoreForSearch accOutRecordStoreForSearch, Paging paging);

        /// <summary>
        /// 附件库出库登录保存 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        [TransactionAop]
        bool AccOutForLogin(List<VM_AccOutLoginStoreForTableShow> accOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录修改仓库预约表及仓库预约详细表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginUpdateReserve(VM_AccOutLoginStoreForTableShow accOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录添加出库履历数据 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginAddOutRecord(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录修改仓库表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginUpdateMaterial(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录修改让步仓库表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginUpdateGiMaterial(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录修改让步仓库预约表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginUpdateGiReserve(VM_AccOutLoginStoreForTableShow accOutLoginStore, Dictionary<string, string>[] selectOrderList);

        /// <summary>
        /// 附件库出库登录修改批次别库存表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        /// <param name="pdtSpecState">记录合格、规格品、让步品状态</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        void AccOutForLoginUpdateBthStockList(VM_AccOutLoginStoreForTableShow accOutLoginStore, string pdtSpecState, Dictionary<string, string>[] selectOrderList);


        //附件库出库履历一览删除功能
        [TransactionAop]
        string AccOutRecordForDel(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore);

        //附件库出库履历一览(删除功能)修改履历表
        void AccOutRecordForDelInRecord(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览(删除功能)修改仓库预约表、仓库预约详细表
        void AccOutRecordForDelReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore, string pdtSpecState);

        //附件库出库库履历一览（删除功能）修改仓库表
        void AccOutRecordForDelMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（删除功能）删除让步仓库表
        void AccOutRecordForDelGiMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（删除功能）删除让步仓库预约表
        void AccOutRecordForDelGiReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（删除功能）删除生产领料单详细表
        void AccOutRecordForDelProduceMaterDetail(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（删除功能）删除外协领料单信息表
        void AccOutRecordForDelMCSupplierCnsmInfo(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（删除功能）删除批次别库存表
        //void AccOutRecordForDelBthStockList(VM_AccOutRecordStoreForTableShow accOutRecordStore, string pdtSpecFlg);

        //附件库出库履历一览修改功能
        [TransactionAop]
        bool AccOutRecordForUpdate(List<VM_AccOutRecordStoreForTableShow> accOutRecordStore);

        //附件库出库履历一览(修改功能)修改履历表
        void AccOutRecordForUpdateInRecord(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览(修改功能)修改仓库预约表、仓库预约详细表
        void AccOutRecordForUpdateReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore, string pdtSpecState);

        //附件库出库库履历一览（修改功能）修改仓库表
        void AccOutRecordForUpdateMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（修改功能）修改让步仓库表
        void AccOutRecordForUpdateGiMaterial(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（修改功能）修改让步仓库预约表
        void AccOutRecordForUpdateGiReserve(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（修改功能）修改生产领料单详细表
        void AccOutRecordForUpdateProduceMaterDetail(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //附件库出库履历一览（修改功能）修改外协领料单信息表
        void AccOutRecordForUpdateMCSupplierCnsmInfo(VM_AccOutRecordStoreForTableShow accOutRecordStore);

        //查询产品单价
        decimal SelectCompMaterialInfoForPrice(VM_AccInLoginStoreForTableShow accInLoginStore);

        /// <summary>
        /// 附件库出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        IEnumerable SelectAccOutStoreForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging);

        //入库单打印选择画面显示
        IEnumerable GetAccInPrintBySearchByPage(VM_AccInPrintForSearch accInPrintForSearch, Paging paging);

        //物资验收入库单显示
        IEnumerable SelectForAccInPrintPreview(string pdtID, string deliveryOrderID, Paging paging);

        /// <summary>
        /// 附件库出库登录修改生产领料单及外协领料单表 陈健
        /// </summary>
        /// <param name="accOutLoginStore">出库登录画面数据</param>
        void AccOutForLoginUpdateMaterReq(VM_AccOutLoginStoreForTableShow accOutLoginStore);
    } //end IAccStoreService
}
