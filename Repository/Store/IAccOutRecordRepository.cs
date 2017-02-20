/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccOutRecordRepository.cs
// 文件功能描述：
//            附件库出库履历及出库相关业务Repository接口
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
using  Extensions;
namespace Repository
{
    public interface IAccOutRecordRepository : IRepository<AccOutRecord>
    {
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
        /// 根据 AccOutDetailRecord 类，获取相关信息
        /// </summary>
        /// <param name="accOutRecord">AccOutDetailRecord 实体类</param>
        /// <returns>VM_AccOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        VM_AccOutPrintIndexForTableShow GetAccOutPrintForTableShow(AccOutDetailRecord accOutDetailRecord);

        #endregion

        /// <summary>
        /// 附件库出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        IEnumerable GetAccOutStoreForLoginBySearchByPage(string pickListID, string materielID, string pickListDetNo, Paging paging);

        //附件库出库履历一览初始化页面
        IEnumerable GetAccOutRecordBySearchByPage(VM_AccOutRecordStoreForSearch accOutRecordStoreForSearch, Paging paging);

        //出履历数据删除方法
        bool AccOutRecordForDel(AccOutRecord accOutRecord);

        /// <summary>
        /// 附件库出库登录插入出库履历表查询是否已存在 陈健
        /// </summary>
        /// <param name="accOutRecord">附件库出库登录数据集合</param>
        /// <returns>数据集合</returns>
        AccOutRecord SelectAccOutRecord(AccOutRecord accOutRecord);

        /// <summary>
        /// Desc取得生产领料单详细表List 陈健
        /// </summary>
        /// <param name="produceMaterDetail">生产领料单数据</param>
        /// <returns>生产领料单数据集合</returns>
        IEnumerable<ProduceMaterDetail> GetProduceMaterDetailForListDesc(ProduceMaterDetail produceMaterDetail);

        #region 附件出库批次选择已出库（已作废）
        ///// <summary>
        ///// 附件库出库批次选择画面数据（已出库）
        ///// </summary>
        ///// <param name="qty"></param>
        ///// <param name="pickListID"></param>
        ///// <param name="paging"></param>
        ///// <returns></returns>
        //IEnumerable SelectAccOutRecordForBthSelect(decimal qty, string pickListID, Paging paging); 
        #endregion

        /// <summary>
        /// 生产查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <returns>生产领料数据集合</returns>
        ProduceMaterDetail AccOutRecordInfo(string pickListID, string pickListDetNo);

        /// <summary>
        /// 附件库出库批次选择画面数据（生产未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产未指定批次选择画面数据集合</returns>
        IEnumerable SelectAccOutRecordProNForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging);

        /// <summary>
        /// 附件库出库批次选择画面数据（生产指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产指定批次选择画面数据集合</returns>
        IEnumerable SelectAccOutRecordProForBthSelect(decimal qty, string pickListID, string pickListDetNo, Paging paging);

        /// <summary>
        /// 外协查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <returns>外协领料数据集合</returns>
        MCSupplierCnsmInfo AccOutRecordSInfo(string pickListID, string pickListDetNo);

        /// <summary>
        /// 附件库出库批次选择画面数据（外协未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协未指定批次选择画面数据集合</returns>
        IEnumerable SelectAccOutRecordSupNForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging);

        /// <summary>
        /// 附件库出库批次选择画面数据（外协指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协指定批次选择画面数据集合</returns>
        IEnumerable SelectAccOutRecordSupForBthSelect(decimal qty, string pickListID, string pickListDetNo, Paging paging);

        /// <summary>
        /// 附件库出库登录修改外协领料单信息表 陈健
        /// </summary>
        /// <param name="mcSupplierCnsmInfo">更新外协领料单数据集合</param>
        /// <returns>更新结果</returns>
        bool UpdateSupplierCnsmInfoForOut(MCSupplierCnsmInfo mcSupplierCnsmInfo);

        /// <summary>
        /// 附件库出库登录修改生产领料单详细表 陈健
        /// </summary>
        /// <param name="produceMaterDetail">更新生产领料单数据集合</param>
        /// <returns>更新结果</returns>
        bool UpdateProduceMaterDetailForOut(ProduceMaterDetail produceMaterDetail);

    } //end IAccOutRecordRepository
}
