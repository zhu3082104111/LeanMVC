/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProduceForExternalService.cs.cs
// 文件功能描述：
//          生产部门的外部共通的Service接口类
//      
// 修改履历：2013/12/24 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL_InsideInterface
{
    public interface IProduceForExternalService
    {
        /// <summary>
        /// 更新领料单的仓管员ID和实领数量，出库时调用
        /// </summary>
        /// <param name="ProductMaterID">领料单号</param>
        /// <param name="ProductMaterIDDetail">领料明细号</param>
        /// <param name="WarehouseKeperID">仓库员ID</param>
        /// <param name="ReciveQty">实领数量</param>
        /// <returns>true：成功； false：失败</returns>
        bool UpdateProductMaterByWearHouse(string ProductMaterID, string ProductMaterIDDetail, string WarehouseKeperID, decimal ReciveQty);

        /// <summary>
        /// 更新成品交仓单状态为2 已入库，根据成品交仓单号查询出对应的总装调度单，更新总装调度单入库日期和仓库管理员ID，在仓库入库的时候调用
        /// </summary>
        /// <param name="ProductWearHouseID">成品交仓单号</param>
        /// <param name="WarehouseKeperID">仓库员ID</param>
        /// <param name="WarehouseDate">入库日期</param>
        /// <returns>true：成功； false：失败</returns>
        bool UpdatePDProductWHByWearHouse(string ProductWearHouseID, string WarehouseKeperID, string WarehouseDate);

        /// <summary>
        /// 提供领料单数据
        /// </summary>
        /// <param name="MaterReqID">领料单号</param>
        /// <returns>VM_MaterReqInfo，包含领料单号，产品零件ID，数量，是否提供批次号区分</returns>
        VM_MaterReqInfo GetMaterReqInfo(string MaterReqID);

        /// <summary>
        /// 提供领料单数据
        /// </summary>
        /// <param name="MaterReqID">领料单号</param>
        /// <param name="PdtID">产品零件ID</param>
        /// <returns>VM_MaterReqBthInfo，包含领料单号，产品零件ID，批次号，数量，单配区分</returns>
        VM_MaterReqBthInfo GetMaterReqBthInfo(string MaterReqID, string PdtID);

        /// <summary>
        /// 更新领料单实领数量
        /// </summary>
        /// <param name="MaterReqID">领料单号</param>
        /// <param name="ClnOdrID">客户订单号</param>
        /// <param name="ClnOdrDetID">客户订单明细</param>
        /// <param name="PdtID">产品零件ID</param>
        /// <param name="BthID">批次号</param>
        /// <param name="ReceQty">实领数量</param>
        /// <returns>true：成功； false：失败</returns>
        bool UpdateReceQty(string MaterReqID, string ClnOdrID, string ClnOdrDetID, string PdtID, string BthID,
            string ReceQty);

        /// <summary>
        /// 更新【物料分解信息表】的进度。
        /// 具体是更新字段
        /// 【生产投产数量】
        /// 【外购投产数量】
        /// 【外协投产数量】
        /// </summary>
        /// <param name="ClnOdrID">客户订单号</param>
        /// <param name="ClnOdrDetID">客户订单明细</param>
        /// <param name="PdtID">产品零件ID</param>
        /// <param name="OprQty">投产数量</param>
        /// <param name="OprFlg">投产区分（1：自生产，2：外购，3：外协）</param>
        /// <returns>true：成功； false：失败</returns>
        bool UpdateScheMaterDecom(string ClnOdrID, string ClnOdrDetID, string PdtID, string OprQty, string OprFlg);

        /// <summary>
        /// 自生产加工品入库分配数量时调用
        /// </summary>
        /// <param name="ProcDelivId">加工送货单号</param>
        /// <returns>VM_ClnOdrList，包含客户订单号，客户订单明细号，以及交仓数量的List。</returns>
        List<VM_ClnOdrList> GetClnListProcDel(string ProcDelivId);

        /// <summary>
        /// 对外购指示进行排产后，要更新外购指示表的采购数量。
        /// </summary>
        /// <param name="customerNo">客户订单号</param>
        /// <param name="customerDtlNo">客户订单明细号</param>
        /// <param name="prodPartId">产品零件ID</param>
        /// <param name="quantity">采购数量</param>
        /// <param name="userID">操作人</param>
        /// <param name="updDate">操作时间</param>
        /// <returns>boolean true：成功； false：失败</returns>
        bool UpdPurchInstruc4Add(string customerNo, string customerDtlNo, string prodPartId, Decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 当删除外购单时，要更新外购指示表的采购数量。
        /// </summary>
        /// <param name="customerNo">客户订单号</param>
        /// <param name="customerDtlNo">客户订单明细号</param>
        /// <param name="prodPartId">产品零件ID</param>
        /// <param name="quantity">采购数量</param>
        /// <param name="userID">操作人</param>
        /// <param name="updDate">操作时间</param>
        /// <returns>boolean true：成功； false：失败</returns>
        bool UpdPurchInstruc4Del(string customerNo, string customerDtlNo, string prodPartId, Decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 对外协指示进行排产后，要更新外协指示表的采购数量。
        /// </summary>
        /// <param name="customerNo">客户订单号</param>
        /// <param name="customerDtlNo">客户订单明细号</param>
        /// <param name="prodPartId">产品零件ID</param>
        /// <param name="quantity">采购数量</param>
        /// <param name="userID">操作人</param>
        /// <param name="updDate">操作时间</param>
        /// <returns>boolean true：成功； false：失败</returns>
        bool UpdAssiInstruc4Add(string customerNo, string customerDtlNo, string prodPartId, Decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 当删除外协单时，要更新外协指示表的采购数量。
        /// </summary>
        /// <param name="customerNo">客户订单号</param>
        /// <param name="customerDtlNo">客户订单明细号</param>
        /// <param name="prodPartId">产品零件ID</param>
        /// <param name="quantity">采购数量</param>
        /// <param name="userID">操作人</param>
        /// <param name="updDate">操作时间</param>
        /// <returns>boolean true：成功； false：失败</returns>
        bool UpdAssiInstruc4Del(string customerNo, string customerDtlNo, string prodPartId, Decimal quantity, string userID, DateTime updDate);
    }
}
