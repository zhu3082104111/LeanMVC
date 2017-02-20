/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderDetailPrint.cs
// 文件功能描述：
//      
// 创建标识：2013/11/07 冯吟夷 新建
// 修改表示：2013/11/19 冯吟夷 修改
// 修改描述：更新注释位置，添加主键
/*****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MK_ORDER_DTL_PRINT")] 
    public class MarketOrderDetailPrint : Entity
    {
        [Column("CLN_ODR_ID",Order=0), Key, StringLength(20)] //客户订单号
        public string ClientOrderID { get; set; }

        [Column("CLN_ODR_DTL", Order = 1), Key, StringLength(2)] //客户订单详细，说明：订单中的次序（可用于区别同一产品不同要求之需）
        public string ClientOrderDetail { get; set; }

        [Column("NO",Order=2), Key, StringLength(2)] //序号
        public string NO { get; set; }

        [Column("PDT_ID"), Required, StringLength(10)] //产品ID
        public string ProductID { get; set; }

        [Column("POS"), StringLength(200)] //打字位置
        public string Position { get; set; }

        [Column("CONT"), StringLength(200)] //打字内容
        public string Content { get; set; }

        [Column("IMG_NM"), StringLength(256)] //图打字片
        public string ImageName { get; set; }


    }
}
