/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderDetail.cs
// 文件功能描述：
//      
// 创建标识：2013/11/11 梁龙飞 新建
// 修改表示：2013/11/19 冯吟夷 修改
// 修改：2013/12/4 梁龙飞修改：在类前增加注释。
// 修改描述：更新注释位置，更新主键，更新字段长度，更新 SEAL_COLOR_ID、SEAL_REQU、SEAL_PIC、URGENCY 字段
/*****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; //可删

namespace Model
{
    /// <summary>
    ///  Table("MK_ORDER_DTL")
    ///  订单详细
    /// </summary>
    [Serializable, Table("MK_ORDER_DTL")] 
    public class MarketOrderDetail : Entity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("CLN_ODR_ID",Order=0), Key, StringLength(20)] 
        public string ClientOrderID { get; set; }
        /// <summary>
        /// 客户订单详细，说明：订单中的次序（可用于区别同一产品不同要求之需）
        /// </summary>
        [Column("CLN_ODR_DTL", Order = 1), Key, StringLength(2)] 
        public string ClientOrderDetail { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("PDT_ID"), Required, StringLength(15)] 
        public string ProductID { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        [Column("DELVY_DATE")] 
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 生产单元区分
        /// </summary>
        [Column("PD_CELL_ID"), StringLength(2)] 
        public string ProduceCellID { get; set; }

        /// <summary>
        /// 数量，说明：数量的单位是该【成品信息表】中的【产品单位ID】
        /// </summary>
        [Column("QTY"), DecimalPrecision(10, 2)] 
        public decimal Quantity { get; set; }

        /// <summary>
        /// 客户型号
        /// </summary>
        [Column("CLN_PDT_ID"), StringLength(15)] 
        public string ClientProductID { get; set; }

        /// <summary>
        /// 装箱数
        /// </summary>
        [Column("PKG_QTY"), DecimalPrecision(10, 2)] 
        public decimal PackageQuantity { get; set; }

        /// <summary>
        /// 纸箱尺寸
        /// </summary>
        [Column("PKG_SIZE"), StringLength(50)] 
        public string PackageSize { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        [Column("OEM_ID"), StringLength(10)] 
        public string OriginalEquipmentManufacturerID  { get; set; }

        /// <summary>
        /// 产品图片，说明：路径最大256byte
        /// </summary>
        [Column("IMG_NM"), StringLength(256)] 
        public string ImageName { get; set; }

        /// <summary>
        /// 油封颜色ID
        /// </summary>
        [Column("SEAL_COLOR_ID"), StringLength(5)] 
        public string SealColorID { get; set; }

        /// <summary>
        /// 油封其他特殊要求
        /// </summary>
        [Column("SEAL_REQU"), StringLength(200)] 
        public string SealRequire { get; set; }

        /// <summary>
        /// 油封图片，说明：路径最大256byte
        /// </summary>
        [Column("SEAL_PIC"), StringLength(256)] 
        public string SealPicture { get; set; }

        /// <summary>
        /// 紧急度区分，说明：数值越大约紧急。0：无紧急度要求，1：紧急度1级，2：紧急度2级，3：紧急度3级
        /// </summary>
        [Column(name: "URGENCY", TypeName = "char"), StringLength(1)] 
        public string Urgency { get; set; }

    }
}
