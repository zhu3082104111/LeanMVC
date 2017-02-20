//系统公用枚举和字段
//C： 20131119 梁龙飞
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{ 

    public class Constant{
        private const string ZERO = "0";
        private const string ONE = "1";
        /// <summary>
        /// 有效
        /// </summary>
        public const string GLOBAL_EffEFLAG_ON = ZERO;//有效
        
        /// <summary>
        /// 无效
        /// </summary>
        public const string GLOBAL_EffEFLAG_OFF = ONE;//无效

        /// <summary>
        /// 未删除
        /// </summary>
        public const string GLOBAL_DELFLAG_ON = ZERO;//未删除
        
        /// <summary>
        /// 已删除
        /// </summary>
        public const string GLOBAL_DELFLAG_OFF = ONE;//已删除

        /// <summary>
        /// 未打印
        /// </summary>
        public const string GLOBAL_PRINTLAG_OFF = "0";//未打印

        /// <summary>
        /// 已打印
        /// </summary>
        public const string GLOBAL_PRINTLAG_ON = "1";//已打印

        /// <summary>
        /// 连接符
        /// </summary>
        public const string SPLICE_MARK = "-";

        ///// <summary>
        ///// 客户订单状态
        ///// 未接收1
        ///// </summary>
        //public  class GeneralPlanState
        //{
        //    /// <summary>
        //    /// 未接收1
        //    /// </summary>
        //    public  const  string NOT_ACCEPT = "1";
        //    /// <summary>
        //    /// 已接收2
        //    /// </summary>
        //    public const string ACCEPTED = "2";
        //    /// <summary>
        //    /// 排产中3
        //    /// </summary>
        //    public const string PURCHASE = "3";
        //    /// <summary>
        //    /// 排产完成4
        //    /// </summary>
        //    public const string PRODUCING = "4";
        //    /// <summary>
        //    /// 装配中5
        //    /// </summary>
        //    public const string ASSEMBLING = "5";
        //    /// <summary>
        //    /// 生产完成6
        //    /// </summary>
        //    public const string PROCESSED = "6";
        //}

        ///// <summary>
        ///// 生产计划总表：产品排产状态：废弃
        ///// C：梁龙飞
        ///// </summary>
        //public  class ProductScheduling {
        //    /// <summary>
        //    /// 全部0
        //    /// </summary>
        //    public const string All = "0";
        //    /// <summary>
        //    /// 未排产1
        //    /// </summary>
        //    public const string NOT_SHCEDULE = "1";
        //    /// <summary>
        //    /// 排产中2
        //    /// </summary>
        //    public const string SCHEDULING = "2";
        //    /// <summary>
        //    /// 已排产3（除未排产和排产中的所有状态）
        //    /// </summary>
        //    public const string SCHEDULED = "3";
        //}

        /// <summary>
        /// 生产计划总表：产品排产状态
        /// 1：未接收，2：已接收(未排产)，3：排产中，4：已排产, 5：采购中，6：生产中，7：总装中，8：生产完成
        /// </summary>
        public class GeneralPlanState
        {
            /// <summary>
            /// 0：默认状态
            /// </summary>
            public const string ALL_STATE = "0";
            /// <summary>
            /// 1：未接受
            /// </summary>
            public const string UN_ACC = "1";
            /// <summary>
            /// 2：已接收（未排产）
            /// </summary>
            public const string ACCEPTED = "2";
            /// <summary>
            /// 3：排产中
            /// </summary>
            public const string SCHEDULING = "3";
            /// <summary>
            /// 4：排产完成
            /// </summary>
            public const string SCHEDULED = "4";
            /// <summary>
            /// 5：采购中
            /// </summary>
            public const string PURCHASING = "5";
            /// <summary>
            /// 6：生产中
            /// </summary>
            public const string PRODUCING = "6";
            /// <summary>
            /// 7：总装中
            /// </summary>
            public const string ASSEMBLING = "7";
            /// <summary>
            /// 8：生产完成
            /// </summary>
            public const string PRODUCED = "8";
        }
        /// <summary>
        /// 外购指示表，处理状态
        /// </summary>
        public class PurchaseInstState
        {
            /// <summary>
            /// 外购指示：未排产
            /// </summary>
            public const string UN_SCHEDULED = "1";
            /// <summary>
            /// 外购指示：排产中
            /// </summary>
            public const string SCHEDULING = "2";
            /// <summary>
            /// 外购指示：已排产
            /// </summary>
            public const string SCHEDULED = "3";
        }
        /// <summary>
        /// 常量
        /// </summary>
        public static class CONST_FIELD 
        {
            /// <summary>
            /// 时间字段初始化值（0001/1/1 00:00:00），只读
            /// </summary>
            public static readonly DateTime INIT_DATETIME = Convert.ToDateTime("0001/1/1 00:00:00");

            /// <summary>
            /// 数据库时间初始值
            /// </summary>
            public static readonly DateTime DB_INIT_DATETIME = Convert.ToDateTime("1990/1/1 00:00:00");//new DateTime(1900,1,1,0,0,0);
            /// <summary>
            /// 删除区分 - 已删除
            /// </summary>
            public   const string DELETED = "1";
            /// <summary>
            /// 删除区分 - 未删除
            /// </summary>
            public   const string UN_DELETED = "0";
            /// <summary>
            /// 有效
            /// </summary>
            public   const string EFFECTIVE = "0";
            /// <summary>
            /// 无效
            /// </summary>
            public   const string UN_EFFECTIVE = "1";


        }

        /// <summary>
        /// 产品外购单当前状态
        /// </summary>
        public class OutOrderStatus
        {
            /// <summary>
            /// 未处理
            /// </summary>
            public const string STATUSZ = "0";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string STATUSO = "1";
            /// <summary>
            /// 已批准
            /// </summary>
            public const string STATUST = "2";
            /// <summary>
            /// 已签收
            /// </summary>
            public const string STATUSS = "3";
        }

        /// <summary>
        /// 产品外购单区分
        /// </summary>
        public class OutOrderType
        {
            /// <summary>
            /// 生产
            /// </summary>
            public const string PRODUCT = "0";

            /// <summary>
            /// 仓库
            /// </summary>
            public const string STORE = "1";
        }

        /// <summary>
        /// masterinfo表中的section常量
        /// 代东泽 2013-11-27
        /// </summary>
        public static class MasterSection 
        {
            /// <summary>
            /// "00005"-打印区分
            /// </summary>
            public const string PRINT = "00005";//打印区分
            /// <summary>
            /// "00009"-(生产单元的)部门
            /// </summary>
            public const string DEPT = "00009";//(生产单元的)部门
            /// <summary>
            /// "00039"-物料领用状态
            /// </summary>
            public const string PICKING_STATE = "00039";//物料领用状态
            /// <summary>
            /// "00040"-领料类型
            /// </summary>
            public const string PICKING_TYPE = "00040";//领料类型
            /// <summary>
            /// "00037"-外购外协排产状态
            /// </summary>
            public const string SCHEDULE_STATUS = "00037";//外购外协排产状态
            /// <summary>
            /// "00054"-完成状态
            /// </summary>
            public const string COMPLETE_STATUS = "00054";//完成状态

            /// <summary>
            /// "00001";//紧急状态
            /// </summary>
            public const string URGENT_STATE = "00001";//紧急状态
            /// <summary>
            /// "00002";//外购单状态
            /// </summary>
            public const string ODR_STATE = "00002";//外购单状态
            /// <summary>
            /// "00008";//外购外协送货单区分
            /// </summary>
            public const string DEL_ODR_TYPE = "00008";//外购外协送货单区分
            /// <summary>
            /// "00038";//送货单状态
            /// </summary>
            public const string DEL_ODR_STATE = "00038";//送货单状态
            /// <summary>
            /// "00036";//仓库种别区分
            /// </summary>
            public const string WH_TYPE_ID = "00036";//仓库种别区分


            /// <summary>
            /// "00007" 外协单种类 M表 编码代号
            /// </summary>
            public const string SUP_ODR_TYPE = "00007";
            /// <summary>
            /// "00034" 外协单状态 M表 编码代号
            /// </summary>
            public const string SUP_ODR_STATUS = "00034";

            public const string PRODUCE_PLAN_STATE = "00041";//生产计划状态,20131223 朱静波

            /// <summary>
            /// "00043"-成品交仓单状态,20131203 杜兴军
            /// </summary>
            public const string PROD_WAREHOUSE_STATE = "00043";//成品交仓单状态,20131203 杜兴军
            /// <summary>
            /// "00044"-内部加工计划一览进度状态,20131203 杜兴军
            /// </summary>
            public const string PROD_SCHEDU_SHOW_STATE = "00044";//内部加工计划一览进度状态,20131203 杜兴军
            /// <summary>
            ///  "00045" -内部加工中计划排产状态,20131203 杜兴军
            /// </summary>
            public const string PROD_SCHEDU_MIDDLE_STATE = "00045";//内部加工中计划排产状态,20131203 杜兴军

            // 进货检验单雷迪克检验结果
            public const string LDK_PUR_CHK_RES = "00055";

        }

        /// <summary>
        /// 单据编码区分
        /// 未完，待追加
        /// </summary>
        public static class CodeType
        {
            /// <summary>
            /// 加工送货单分区
            /// </summary>
            public const string PROCDELIV_ID = "00005";

            /// <summary>
            /// 总装调度单分区
            /// </summary>
            public const string ASSEMBLY_DISPATCH_ID = "00006";

            /// <summary>
            /// 总装大工票分区
            /// </summary>
            public const string ASSEM_BILL_ID = "00007";

            // 外协单号区分
            public const string SUPPLIER_ORDER = "00011";

            // 外购单号区分
            public const string PURCHASE_ORDER = "00012";

            // 送货单号区分
            public const string DELIVERY_ORDER = "00013";

            // 内部成品送货单号区分
            public const string INER_FIN_OUT_ORDER = "00014";

            // 报废单号区分
            public const string DISCARD_ORDER = "00015";
            /// <summary>
            /// 领料单号区分
            /// 代东泽 20131230
            /// </summary>
            public const string PICKING_MATERIAREQUEST = "00004";

            /// <summary>
            /// 加工流转卡号区分
            /// </summary>
            public const string TRANSLATE_CARD_ID = "00003";

            /// <summary>
            /// 成品交仓单号区分
            /// </summary>
            public const string PROD_WAREHOUSE_ID = "00009";


        }

        /// <summary>
        /// 单据标示
        /// 未完，待追加
        /// </summary>
        public static class OrderMarked
        {
            // 外购单标示
            public const string PURCHASE_ORDER = "WG";

            // 外协单标示
            public const string SUPPLIER_ORDER = "WX";

            // 送货单标示
            public const string DELIVERY_ORDER = "SH";

            // 内部成品送货单号区分
            public const string INER_FIN_OUT_ORDER = "NS";

            // 报废单号区分
            public const string DISCARD_ORDER = "BF";

            /// <summary>
            /// 总装调度单区分
            /// </summary>
            public const string ASSEMBLY_DISPATCH_ID = "ZD";

            /// <summary>
            /// 总装大工票号区分
            /// </summary>
            public const string ASSEM_BILL_ID = "ZG";

            /// <summary>
            /// 加工送货单号区分
            /// </summary>
            public const string PROCDELIV_ID = "JS";

            /// <summary>
            /// 领料单号标识
            /// 代东泽 20131230
            /// </summary>
            public const string PICKING_MATERIAREQUEST = "LL";


            /// <summary>
            /// 加工流转卡号区分
            /// </summary>
            public const string TRANSLATE_CARD_ID = "JL";

            /// <summary>
            /// 成品交仓单号区分
            /// </summary>
            public const string PROD_WAREHOUSE_ID = "NS";

        }

        /// <summary>
        /// 生产单元
        /// </summary>
        public static class ProdUnit
        {

            // 默认（无生产单元区分）
            public const string DEFAULT = "00";

            // 驰航
            public const string CHIHANG = "01";

            // 分离
            public const string FENGLI = "02";

            // 涨紧轮
            public const string ZHANGJINLUN = "03";

            // 三叉
            public const string SANCHA = "04";

            // 捷拓
            public const string JIETUO = "05";

            // 沃众
            public const string WOZHONG = "06";
        }

         /// <summary>
        /// 仓库
        /// </summary>
        public static class Store
        {

            // 附件库
            public const string WH_ACC = "00";

            // 在制品库
            public const string WH_WIP = "01";

            // 半成品库
            public const string WH_SEM = "02";

            // 成品库
            public const string WH_FIN = "03";

            // 五金库
            public const string WH_HAR = "04";
        }

        /// <summary>
        /// 自动检索的类型，20131204 杜兴军
        /// </summary>
        public static class AutoSearchType
        {
            public const string USER = "user";

            public const string TEAM = "team";

        }


        /// <summary>
        /// 加工排产(中计划)状态
        /// 20131206 杜兴军
        /// </summary>
        public static class ProcessingMiddlePlanState
        {
            /// <summary>
            /// 未排产
            /// </summary>
            public const string NOTSCHEDU = "1";
            
            /// <summary>
            /// 已排产
            /// </summary>
            public const string HAVESCHEDUED = "2";

            /// <summary>
            /// 已投料(已生产)
            /// </summary>
            public const string HAVEPRODUCE = "3";

            /// <summary>
            /// 已完成
            /// </summary>
            public const string HAVEFINISHED = "4";
        }

        /// <summary>
        /// 加工排产(一览)状态
        /// 20131206 杜兴军
        /// </summary>
        public static class ProcessingPlanState
        {
            /// <summary>
            /// 未开始
            /// </summary>
            public const string NOTSTART = "1";

            /// <summary>
            /// 生产中
            /// </summary>
            public const string PRODUCING = "2";

            /// <summary>
            /// 完成
            /// </summary>
            public const string FINISHED = "3";

            /// <summary>
            /// 延迟(计划值>实绩值)
            /// </summary>
            public const string DELAY = "4";

            /// <summary>
            /// 延误(完成日>提供日)
            /// </summary>
            public const string FAULT = "5";

            /// <summary>
            /// 延迟(计划值>实绩值)
            /// </summary>
            public const string TXT_DELAY = "延迟";

            /// <summary>
            /// 延误(完成日>提供日)
            /// </summary>
            public const string TXT_FAULT = "延误";
        }

        /// <summary>
        /// 供货商种类
        /// </summary>
        public static class CompanyType
        {
            // 未指定
            public const string UNKONWN = "0";

            // 外购单位
            public const string OUT_SOURCING = "1";

            // 外协单位
            public const string SUPPLIER = "2";

            // 外购外协均可
            public const string BOTH = "3";
        }

        /// <summary>
        /// 下拉列表的【全部】栏是否显示
        /// </summary>
        public static class SelAllDispFlg
        {
            // 不显示
            public const string UNDISP = "0";

            // 显示
            public const string DISP = "1";
        }

        /// <summary>
        /// 加工交仓
        /// </summary>
        /// 20131211 朱静波
        public static class Warehouse
        {
            /// <summary>
            /// 待交仓一览画面可交仓数比较值
            /// </summary>
            public const Decimal WAREHQTY = 0;

            /// <summary>
            /// 加工送货单表交仓单状态的默认值
            /// </summary>
            public const string  WAREH_STA = "1";

            /// <summary>
            /// 加工产品交仓单一览入库状态对应的编码
            /// </summary>
            public const string WAREH_STA_SECTION_CD = "00043";

            /// <summary>
            /// 加工产品交仓单一览让步状态对应的编码
            /// </summary>
            public const string CONCESS_PARTS_SECTION_CD = "00048";

            /// <summary>
            /// 部门ID对应编码
            /// </summary>
            public const string PRODUCE_UNIT_SECTION_CD = "00009";

            /// <summary>
            /// 是否让步接收标志0
            /// </summary>
            public const string CONCESSIONPART_OFF = "0";

            /// <summary>
            /// 是否让步接收标志1
            /// </summary>
            public const string CONCESSIONPART_ON = "1";
        }

        /// <summary>
        /// 总装调度单
        /// </summary>
        /// 20131211 朱静波
        public static class FAScheduleBill
        {
            /// <summary>
            /// 新增总装调度单详细的默认项目序号值
            /// </summary>
            public const string PROJECT_NO = "1";

            /// <summary>
            /// (生产单元的)部门
            /// </summary>
            public const string DEPT = "00009";

            /// <summary>
            /// 总装一览的当前状态
            /// </summary>
            public const string CURRENT_STATE = "00042";

            /// <summary>
            /// 新增总装调度单的默认状态(未生成工票)
            /// </summary>
            public const string DISPATCH_STATUS = "1";

            /// <summary>
            /// 总装调度单状态(未领料)
            /// </summary>
            public const string DISP_STA_Not_Pick = "2";

            /// <summary>
            /// 打字位置内容分隔符
            /// </summary>
            public const string TYPE_STYLE_ONE = " ";

            /// <summary>
            /// 打字分隔符
            /// </summary>
            public const string TYPE_STYLE_SECOND = ";";
        }

        /// <summary>
        /// 生产类型
        /// 20131210 杜兴军
        /// </summary>
        public static class ProduceType
        {
            /// <summary>
            /// 自加工
            /// </summary>
            public const string SELFPROD = "1";    

            /// <summary>
            /// 外协加工
            /// </summary>
            public const string ASSISTPROD = "2";

            /// <summary>
            /// 外购
            /// </summary>
            public const string PURCHASE = "3";

            /// <summary>
            /// 外协注塑
            /// </summary>
            public const string ASSISTZHUSU = "4";

            /// <summary>
            /// 总装
            /// </summary>
            public const string ASSEM = "5";
        }


        /// <summary>
        /// 代东泽 20131212
        /// 领料单来源
        /// </summary>
        public static class PickingBillComeFrom 
        {
            /// <summary>
            /// "流转卡领料单"
            /// Value="1"
            /// </summary>
            public const string CIRCULATE_CARD = "1";

            /// <summary>
            /// "总装领料单"
            /// Value="2"
            /// </summary>
            public const string ASSEM_DISPATCH = "2";
            /// <summary>
            /// "总装补料单"
            /// Value="3"
            /// </summary>
            public const string ASSEM_DISPATCH_REMEDY = "3";
            /// <summary>
            /// "外协领料"
            /// Value="4"
            /// </summary>
            public const string OUTASSIT_DISPATCH = "4";
            /// <summary>
            /// "流转卡领料单"
            /// </summary>
            public const string Txt_CIRCULATE_CARD = "流转卡领料单";
            /// <summary>
            /// "总装领料单"
            /// </summary>
            public const string Txt_ASSEM_DISPATCH = "总装领料单";
            /// <summary>
            /// "总装补料单"
            /// </summary>
            public const string Txt_ASSEM_DISPATCH_REMEDY = "总装补料单";
            /// <summary>
            /// "外协领料"
            /// </summary>
            public const string Txt_OUTASSIT_DISPATCH = "外协领料";
        }


        /// <summary>
        /// 外协调度单 状态类型
        /// 廖齐玉
        /// 2013-12-12
        /// </summary>
        public static class SupplierOrderStatus 
        {
            /// <summary>
            /// 种类：加工调度单 [value = 1]
            /// </summary>
            public const string ORDERTYPE_PROCESS = "1";
            /// <summary>
            /// 种类：注塑调度单 [value = 2]
            /// </summary>
            public const string ORDERTYPE_INJECT = "2";
            
            /// <summary>
            /// 调度单处理状态：未处理 [value = 0]
            /// </summary>
            public const string UNTREATED = "0";
            /// <summary>
            /// 调度单处理状态：已审核 [value = 1]
            /// </summary>
            public const string AUDITED = "1";
            /// <summary>
            /// 调度单处理状态：已经办 [value = 2]
            /// </summary>
            public const string DONE = "2";

            //US:UrgentStatus 
            /// <summary>
            /// UrgentStatus：普通 [value = 0]
            /// </summary>
            public const string US_NORMAL = "0";
            /// <summary>
            /// UrgentStatus：紧急 [value = 1]
            /// </summary>
            public const string US_EMRFENCY= "1";
            /// <summary>
            /// UrgentStatus：加急 [value = 2]
            /// </summary>
            public const string US_URGENT = "2";
        }

        /// <summary>
        /// 日历休息区分
        /// 20131217 杜兴军
        /// </summary>
        public static class CalendarRetFlg
        {
            /// <summary>
            ///工作日
            /// </summary>
            public const string WORK = "0";

            /// <summary>
            /// 休息日
            /// </summary>
            public const string UNWORK = "1";
        }
        /// <summary>
        /// 代东泽 20131224
        /// 加工流转卡状态
        /// </summary>
        public static class TranslateCardState 
        {

            /// <summary>
            ///已完结
            /// </summary>
            public const string OVER = "1";

            /// <summary>
            /// 未完结
            /// </summary>
            public const string NOTOVER = "0";
        }

        /// <summary>
        /// 送货单状态
        /// </summary>
        public static class DeliveryOrderStatus
        {
            // 未处理
            public const string UNTREATED = "0";

            // 已审核
            public const string AUDITED = "1";

            // 已入库
            public const string STORAGED = "2";
        }

        /// <summary>
        /// 成品交仓单状态
        /// 2014.1.3 杜兴军
        /// </summary>
        public static class ProductWarehouseState
        {
            /// <summary>
            /// 未提交
            /// </summary>
            public const string UNSUBMIT = "1";

            /// <summary>
            /// 已提交
            /// </summary>
            public const string SUBMITED = "2";

            /// <summary>
            /// 已入库
            /// </summary>
            public const string WAREHOUSED = "3";
        }

        /// <summary>
        /// 单据完成状态
        /// 2014-1-6 lqy
        /// </summary>
        public static class PurchaseCompleteStatus
        {
            /// <summary>
            /// 未完成
            /// </summary>
            public const string UNCOMPLETED = "0";
            
            /// <summary>
            /// 已完成
            /// </summary>
            public const string COMPLETED = "1";
        }
    }
}
