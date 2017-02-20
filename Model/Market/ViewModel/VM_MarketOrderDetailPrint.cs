

namespace Model.Market
{
    #region 修改订单

    public class VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable
    {
        public string ClientOrderID { get; set; } //客户订单号
        public string ClientOrderDetailPrint { get; set; } //客户订单详细
        public string NO { get; set; } //序号
        public string MODPProductID { get; set; } //产品ID
        public string ProductAbbreviation { get; set; } //产品型号
        public string Position { get; set; } //打字位置
        public string Content { get; set; } //打字内容
        public string ImageName { get; set; } //图打字片
        public string Status { get; set; } //订单状态
        public string StatusName { get; set; } //订单状态名称
    }

    #endregion
}
