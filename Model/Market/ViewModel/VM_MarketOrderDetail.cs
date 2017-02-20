using System;


namespace Model.Market
{
    #region 修改订单

    public class VM_MarketOrderDetailForMarketOrderDetailTable
    {
        public string ClientOrderID { get; set; } //客户订单号
        public string ClientOrderDetail { get; set; } //客户订单详细
        public string ProductID { get; set; } //产品编号
        public string ProductAbbreviation { get; set; } //产品型号
        public DateTime? MODDeliveryDate { get; set; } //交货日期
        public string ProduceCellID { get; set; } //生产单元区分
        public string ProduceCellName { get; set; } //生产单元名称
        public decimal Quantity { get; set; } //数量
        public string ClientProductID { get; set; } //客户型号
        public decimal PackageQuantity { get; set; } //装箱数
        public string PackageSize { get; set; } //纸箱尺寸
        public string OriginalEquipmentManufacturerID { get; set; } //制造商ID
        public string OriginalEquipmentManufacturerName { get; set; } //制造商名称
        public string ImageName { get; set; } //产品图片
        public string SealColorID { get; set; } //油封颜色ID
        public string SealColorName { get; set; } //油封颜色名称
        public string SealRequire { get; set; } //油封其他特殊要求
        public string SealPicture { get; set; } //油封图片
        public string Urgency { get; set; } //紧急度区分
        public string UrgencyName { get; set; } //紧急度区分名称
        public string Status { get; set; } //订单状态
        public string StatusName { get; set; } //订单状态名称
    }

    #endregion
}
