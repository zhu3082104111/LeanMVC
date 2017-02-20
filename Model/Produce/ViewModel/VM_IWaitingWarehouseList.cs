/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_IWaitingWarehouseList.cs
// 文件功能描述：加工交仓画面的视图model集
//     
// 修改履历：2013/11/09 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    /// <summary>
    /// 待交仓一览搜索Model
    /// </summary>
    public class VM_IWaitingWarehouseForSearch
    {
        public VM_IWaitingWarehouseForSearch()
        {
            WarehQtyAvailable = Constant.Warehouse.WAREHQTY;
        }
        // <summary>
        /// 加工流转卡号
        /// </summary>
        [EntityProperty("ProcDelivID", PropertyOperator.EQUAL)]
        public string TxtProcDelivID { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        [EntityProperty("ProdAbbrev", PropertyOperator.EQUAL)]
        public string TxtProdAbbrev { get; set; }


        /// <summary>
        /// 可交仓数
        /// </summary>
        [EntityProperty("WarehQtyAvailable", PropertyOperator.GT)]
        public decimal WarehQtyAvailable { get; set; }
    }

    public class VM_ProcessTranslateDetailData
    {
        public string ProcDelivID;
        public int SeqNo;
        public Decimal SumRalOprQty;
    }

    /// <summary>
    /// 加工待交仓一览视图Model
    /// </summary>
    public class VM_IWaitingWarehouseView
    {

        /// <summary>
        /// 加工流转卡号
        /// </summary>
        public string ProcDelivID { get; set; }
 
        /// <summary>
        /// 输出品ID
        /// </summary>
        public string ExportID { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        public string ProdAbbrev { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public string ProcessID { get; set; }

        /// <summary>
        /// 零件单位ID
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        public decimal MaterReqQty { get; set; }

        /// <summary>
        /// 可交仓数
        /// </summary>
        public decimal WarehQtyAvailable { get; set; }

        /// <summary>
        /// 已交仓数--交仓数合计
        /// </summary>
        public decimal WarehQtySubmitted { get; set; }

        /// <summary>
        /// 交仓单号
        /// </summary>
        public string WarehouseNo { get; set; }

        /// <summary>
        /// 是否让步接收
        /// </summary>
        public string ConcessionPart { get; set; }
    }

    /// <summary>
    /// 加工产品交仓单一览
    /// </summary>
    public class VM_ProProductWarehouseView
    {
        /// <summary>
        /// 交仓单号---加工送货单号
        /// </summary>
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 流转卡号
        /// </summary>
        public string ProcessTranID { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string PartID { get; set; } 
        
        /// <summary>
        /// 产品型号
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 领产品型号
        /// </summary>
        public string SubItemID { get; set; }

        /// <summary>
        /// 领产品名称
        /// </summary>
        public string SubItemName { get; set; }

        /// <summary>
        /// 交仓数量
        /// </summary>
        public decimal WarehQtySubmitted { get; set; }

        /// <summary>
        /// 入库状态值
        /// </summary>
        public string WarehouseStatus { get; set; }

        /// <summary>
        /// 入库状态编号
        /// </summary>
        public string WarehouseStatusNo { get; set; }

        /// <summary>
        /// 让步标志
        /// </summary>
        public string ConcessionPart { get; set; }

        /// <summary>
        /// 交仓日期
        /// </summary>
        public DateTime? BillDt { get; set; }

    }

    /// <summary>
    /// 加工产品交仓单一览搜索
    /// </summary>
    public class VM_ProProductWarehouseForSearch
    {
        /// <summary>
        /// 加工送货单号
        /// </summary>
        [EntityProperty("ProcDelivID", PropertyOperator.EQUAL)]
        public string TxtProcDelivID { get; set; }

        /// <summary>
        /// 交仓单号---流转卡号
        /// </summary>
        [EntityProperty("ProcessTranID", PropertyOperator.EQUAL)]
        public string TxtProcessTranID { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [EntityProperty("PartName", PropertyOperator.EQUAL)]
        public string TxtPartName { get; set; }

        /// <summary>
        /// 入库状态
        /// </summary>
        [EntityProperty("WarehouseStatusNo", PropertyOperator.EQUAL)]
        public string TxtWarehouseStatus { get; set; }

        /// <summary>
        /// 交仓日期开始
        /// </summary>
        [EntityProperty("BillDt", PropertyOperator.GE)]
        public DateTime? TxtBillStartDt { get; set; }

        /// <summary>
        /// 交仓日期结束
        /// </summary>
        [EntityProperty("BillDt", PropertyOperator.LE)]
        public DateTime? TxtBillEndDt { get; set; }

    }


    /// <summary>
    /// 加工产品交仓单详细视图
    /// </summary>
    public class VM_ProcessDelivery 
    {
        public string ProcDelivID { get; set; }

        public string DepartID { get; set; }

        public string DepartName{ get; set; }

        public string ConcessionPart { get; set; }//让步分区

        public string BillDate{ get; set; }

        private DateTime _billDateTime;

        public DateTime BillDt
        {
            get { return _billDateTime; }
            set
            {
                _billDateTime = value;
                BillDate = value.ToString("yyyy-MM-dd hh:mm");   
            }
        }

        public string WarehKprId { get; set; }//仓库员ID

        public string WarehKprName { get; set; }//仓库员姓名

        //下两字段暂时先不使用，等待经理指示
        public string QualityInspector { get; set; }//质检员

        public string Managers { get; set; }//经办人

    }

}
