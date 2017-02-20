/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProdInfo.cs
// �ļ�����������
//          ��Ʒ��Ϣ���ʵ��Model��
//      
// �޸�������2013-12-09 ��� �޸�
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// DB Table Name: PD_PROD_INFO
    /// DB Table Name(CHS): ��Ʒ��Ϣ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PROD_INFO")]
    public class ProdInfo : Entity
    {

        /// <summary>
        /// ��ƷID
        /// </summary>
        [Column("PRODUCT_ID")]
        [Key, StringLength(20)]
        public string ProductId { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        [Column(name: "PROD_CATG", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string ProdCatg { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [Column("PROD_NAME")]
        [Required]
        [StringLength(200)]
        public string ProdName { get; set; }

        /// <summary>
        /// ��Ʒ�Գ�
        /// </summary>
        [Column("PROD_ABBREV")]
        [StringLength(200)]
        public string ProdAbbrev { get; set; }

        /// <summary>
        /// Ԥ�����
        /// </summary>
        [Column(name: "PREP_CATG", TypeName = "char")]
        [StringLength(2)]
        public string PrepCatg { get; set; }

        /// <summary>
        /// �ɲ�Ʒ����
        /// </summary>
        [Column("OLD_MODEL_ID")]
        [StringLength(200)]
        public string OldModelId { get; set; }

        /// <summary>
        /// ��Ʒ��λID
        /// </summary>
        [Column("UNIT_ID")]
        [StringLength(20)]
        public string UnitId { get; set; }

        /// <summary>
        /// ���ϼ�����ͺ�
        /// </summary>
        [Column("SPECIFICA")]
        [StringLength(400)]
        public string Specifica { get; set; }

        /// <summary>
        /// ��Ʒ����1
        /// </summary>
        [Column(name: "ATTRIBUTE1", TypeName = "char")]
        [StringLength(5)]
        public string Attribute1 { get; set; }

        /// <summary>
        /// ��Ʒ����2
        /// </summary>
        [Column(name: "ATTRIBUTE2", TypeName = "char")]
        [StringLength(5)]
        public string Attribute2 { get; set; }

        /// <summary>
        /// ��Ʒ����3
        /// </summary>
        [Column(name: "ATTRIBUTE3", TypeName = "char")]
        [StringLength(5)]
        public string Attribute3 { get; set; }

        /// <summary>
        /// ��Ʒ����4
        /// </summary>
        [Column(name: "ATTRIBUTE4", TypeName = "char")]
        [StringLength(5)]
        public string Attribute4 { get; set; }

        /// <summary>
        /// ��Ʒ����5
        /// </summary>
        [Column(name: "ATTRIBUTE5", TypeName = "char")]
        [StringLength(5)]
        public string Attribute5 { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("PRICEE")]
        [DecimalPrecision(16, 6)]
        public decimal Pricee { get; set; }

        /// <summary>
        /// ���۵�λID
        /// </summary>
        [Column("PRI_UNIT_ID")]
        [StringLength(20)]
        public string PriUnitId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("EVALUATE")]
        [DecimalPrecision(16, 6)]
        public decimal Evaluate { get; set; }

        /// <summary>
        /// ���۵�λID
        /// </summary>
        [Column("EVA_UNIT_ID")]
        [StringLength(20)]
        public string EvaUnitId { get; set; }

        /// <summary>
        /// ��Ʒͼ��
        /// </summary>
        [Column("PROD_SCHE")]
        [StringLength(100)]
        public string ProdSche { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}