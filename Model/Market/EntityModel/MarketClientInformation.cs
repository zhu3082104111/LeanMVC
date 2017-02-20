/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketClientInformation.cs
// 文件功能描述：
//      
// 创建标识：2013/11/15 冯吟夷 新建
// 修改表示：2013/11/19 冯吟夷 修改
// 修改描述：更新注释位置，添加主键
/*****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MK_CLN_INFO")]
    public class MarketClientInformation : Entity
    {
        [Column("CLN_ID"), Key, StringLength(10)] //客户ID，说明：客户ID，客户的唯一识别号
        public string ClientID { get; set; }

        [Column(name: "COMP_TYPE", TypeName = "char"), StringLength(1)] //单位区分，说明：0：无特别指定； 1：外购单位； 2：外协单位；
        public string CompanyType { get; set; }

        [Column("CLN_NAME"), StringLength(200)] //公司名称
        public string ClientName { get; set; }

        [Column("CLN_NO"), StringLength(200)] //公司简称，说明：雷迪克对客户的简称
        public string ClientNO { get; set; }

        [Column("CLN_ADD"), StringLength(300)] //公司地址
        public string ClientAddress { get; set; }

        [Column("CLN_ZC"), StringLength(10)] //邮编
        public string ClientZipCode { get; set; }

        [Column(name: "CLN_LEVEL", TypeName = "char"), StringLength(1)] //客户等级，说明：1：普通客户，2：大客户，3：重要客户
        public string ClientLevel { get; set; }

        [Column(name: "CRE_LEVEL", TypeName = "char"), StringLength(1)] //信誉等级，说明：0：无信誉等级，1：信誉差，2：较差，3：中等，4：较好，5：好
        public string CreditLevel { get; set; }

        [Column("TEL_NO_1"), StringLength(16)] //电话1
        public string TelephoneNO1 { get; set; }

        [Column("TEL_NO_2"), StringLength(16)] //电话2
        public string TelephoneNO2 { get; set; }

        [Column("FAX_1"), StringLength(16)] //传真1
        public string Fax1 { get; set; }

        [Column("FAX_2"), StringLength(16)] //传真2
        public string Fax2 { get; set; }

        [Column("CONTACTS_NAME"), StringLength(32)] //联系人姓名
        public string ContactsName { get; set; }


    }
}
