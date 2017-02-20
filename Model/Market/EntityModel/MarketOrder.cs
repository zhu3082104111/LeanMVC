/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrder.cs
// 文件功能描述：
//      
// 创建标识：2013/11/14 梁龙飞 新建
// 修改表示：2013/12/3 冯吟夷 修改
// 修改描述：添加 ClientVersion、OrderProgressStatus、OrderStatus 字段
/*****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;//可删

namespace Model
{
    [Serializable, Table("MK_ORDER")] 
    public class MarketOrder:Entity
    {
        /// <summary>
        /// 客户订单号，说明：自加工的场合，首字母【Z】固定
        /// </summary>
        [Column("CLN_ODR_ID",Order=0), Key, StringLength(20)] 
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 版数，说明：数字类型，默认为‘0’。订单变更时可能会更新版数
        /// </summary>
        [Column("CLN_VER", Order = 1), DecimalPrecision(2, 0), Key] 
        public decimal ClientVersion { get; set; }

        /// <summary>
        /// 交货日期
        /// </summary>
        [Column("DELVY_DATE")] 
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [Column("CLN_ID"), StringLength(10)] 
        public string ClientID { get; set; }

        /// <summary>
        /// 包装要求
        /// </summary>
        [Column("PKG_REQ"), StringLength(200)] 
        public string PackageRequire { get; set; }

        /// <summary>
        /// 包装要求图1
        /// </summary>
        [Column("PKG_REQ_IMG1"), StringLength(256)] 
        public string PackageRequireImage1 { get; set; }

        [Column("PKG_REQ_IMG2"), StringLength(256)] //包装要求图2
        public string PackageRequireImage2 { get; set; }

        [Column("PKG_REQ_IMG3"), StringLength(256)] //包装要求图3
        public string PackageRequireImage3 { get; set; }

        [Column("PKG_REQ_IMG4"), StringLength(256)] //包装要求图4
        public string PackageRequireImage4 { get; set; }

        [Column("PKG_REQ_IMG5"), StringLength(256)] //包装要求图5
        public string PackageRequireImage5 { get; set; }

        [Column("OTH_MATTER"), StringLength(200)] //其他注意事项
        public string OtherMatter { get; set; }

        [Column(name: "APPROVAL_FLG", TypeName = "char"), StringLength(1)] //审批FLG
        public string ApprovalFlag { get; set; }

        [Column("APPROVAL_UID"), StringLength(10)] //审批人
        public string ApprovalUserID { get; set; }

        [Column("APPROVAL_DATE")] //审批日期
        public DateTime? ApprovalDate { get; set; }

        [Column("EDIT_UID1"), StringLength(10)] //编制人1
        public string EditUserID1 { get; set; }

        /// <summary>
        /// 编制日期1
        /// </summary>
        [Column("EDIT_DATE1")] 
        public DateTime? EditUserDate1 { get; set; }

        /// <summary>
        /// 编制人2
        /// </summary>
        [Column("EDIT_UID2"), StringLength(10)] 
        public string EditUserID2 { get; set; }

        /// <summary>
        /// 编制日期2
        /// </summary>
        [Column("EDIT_DATE2")] 
        public DateTime? EditUserDate2 { get; set; }

        /// <summary>
        /// 订单进度状态，说明：1：已登录，2：审核中，3：审核通过，4：生产中，5：生产完成，6：送货完成，7：订单完成（已付款）
        /// </summary>
        [Column(name: "ORD_PROG_STA", TypeName = "char"), StringLength(1)] 
        public string OrderProgressStatus { get; set; }

        /// <summary>
        /// 订单状态，说明：1：正常，2：变更，3：退单，4：废止
        /// </summary>
        [Column(name: "ORD_STA", TypeName = "char"), StringLength(1)]
        public string OrderStatus { get; set; }


    }
}
