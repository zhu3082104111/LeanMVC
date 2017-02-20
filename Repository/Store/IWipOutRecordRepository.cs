/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWipOutRecordRepository.cs
// 文件功能描述：
//            在制品库出库履历及出库相关业务Repository接口
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Collections;
using Extensions;
namespace Repository
{
    public interface IWipOutRecordRepository : IRepository<WipOutRecord>
    {
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
        /// 根据 WipOutDetailRecord 类，获取相关信息
        /// </summary>
        /// <param name="wipOutRecord">WipOutDetailRecord 实体类</param>
        /// <returns>VM_WipOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        VM_WipOutPrintIndexForTableShow GetWipOutPrintForTableShow(WipOutDetailRecord wipOutDetailRecord);

        #endregion

        /// <summary>
        /// 在制品出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>在制品库出库登录画面数据集合</returns>
        IEnumerable GetWipOutStoreForLoginBySearchByPage(string pickListID, string materielID,string materReqDetailNo, Paging paging);

        //在制品出库履历一览初始化页面
        IEnumerable GetWipOutRecordBySearchByPage(VM_WipOutRecordStoreForSearch wipOutRecordStoreForSearch, Paging paging);

        /// <summary>
        /// 在制品库出库登录插入出库履历表查询是否已存在 陈健
        /// </summary>
        /// <param name="wipOutRecord">在制品出库登录数据集合</param>
        /// <returns>数据集合</returns>
        WipOutRecord SelectWipOutRecord(WipOutRecord wipOutRecord);

        #region 批次选择画面已出库数据（废弃）
        /// <summary>
        /// 在制品库出库批次选择画面数据（已出库）
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="pickListID"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        //IEnumerable SelectWipOutRecordForBthSelect(decimal qty, string pickListID, Paging paging); 
        #endregion

        /// <summary>
        /// 生产查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>生产领料数据集合</returns>
        ProduceMaterDetail WipOutRecordInfo(string pickListID, string materReqDetailNo);

        /// <summary>
        /// 在制品库出库批次选择画面数据（生产未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产未指定批次选择画面数据集合</returns>
        IEnumerable SelectWipOutRecordProNForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging);

        /// <summary>
        /// 在制品库出库批次选择画面数据（生产指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产指定批次选择画面数据集合</returns>
        IEnumerable SelectWipOutRecordProForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging);

        /// <summary>
        /// 外协查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>外协领料数据集合</returns>
        MCSupplierCnsmInfo WipOutRecordSInfo(string pickListID, string materReqDetailNo);

        /// <summary>
        /// 在制品库出库批次选择画面数据（外协未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协未指定批次选择画面数据集合</returns>
        IEnumerable SelectWipOutRecordSupNForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging);

        /// <summary>
        /// 在制品库出库批次选择画面数据（外协指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协指定批次选择画面数据集合</returns>
        IEnumerable SelectWipOutRecordSupForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging);

        /// <summary>
        /// Desc取得生产领料单详细表List 陈健
        /// </summary>
        /// <param name="produceMaterDetail">生产领料单数据</param>
        /// <returns>生产领料单数据集合</returns>
        IEnumerable<ProduceMaterDetail> GetProduceMaterDetailForListDesc(ProduceMaterDetail produceMaterDetail);

        //出履历数据删除方法(yc添加)
        bool WipOutRecordForDel(WipOutRecord wipOutRecord);

        /// <summary>
        /// 在制品库出库登录修改外协领料单信息表 陈健
        /// </summary>
        /// <param name="mcSupplierCnsmInfo">更新外协领料单数据集合</param>
        /// <returns>更新结果</returns>
        bool UpdateSupplierCnsmInfoForOut(MCSupplierCnsmInfo mcSupplierCnsmInfo);

        /// <summary>
        /// 在制品库出库登录修改生产领料单详细表 陈健
        /// </summary>
        /// <param name="produceMaterDetail">更新生产领料单数据集合</param>
        /// <returns>更新结果</returns>
        bool UpdateProduceMaterDetailForOut(ProduceMaterDetail produceMaterDetail);

    } //end IWipOutRecordRepository
}
