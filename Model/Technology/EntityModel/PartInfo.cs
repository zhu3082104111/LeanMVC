/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����PartInfo.cs
// �ļ�����������
//          �����Ϣ���ʵ��Model��
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
    /// DB Table Name: PD_PART_INFO
    /// DB Table Name(CHS): �����Ϣ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PART_INFO")]
    public class PartInfo : Entity
    {

        /// <summary>
        /// ���ID
        /// </summary>
        [Column("PART_ID")]
        [Key, StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Column(name: "PART_CATG", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string PartCatg { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("PART_SUB_CATG")]
        [Required]
        [StringLength(3)]
        public string PartSubCatg { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Column("PART_NAME")]
        [Required]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// ����Գ�
        /// </summary>
        [Column("PART_ABBREVI")]
        [StringLength(200)]
        public string PartAbbrevi { get; set; }

        /// <summary>
        /// Ԥ�����
        /// </summary>
        [Column(name: "PREP_CATG", TypeName = "char")]
        [StringLength(2)]
        public string PrepCatg { get; set; }

        /// <summary>
        /// ���������
        /// </summary>
        [Column("OLD_MODEL_ID")]
        [StringLength(200)]
        public string OldModelId { get; set; }

        /// <summary>
        /// �����λID
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
        /// �������1
        /// </summary>
        [Column(name: "ATTRIBUTE_1", TypeName = "char")]
        [StringLength(5)]
        public string Attribute1 { get; set; }

        /// <summary>
        /// �������2
        /// </summary>
        [Column(name: "ATTRIBUTE_2", TypeName = "char")]
        [StringLength(5)]
        public string Attribute2 { get; set; }

        /// <summary>
        /// �������3
        /// </summary>
        [Column(name: "ATTRIBUTE_3", TypeName = "char")]
        [StringLength(5)]
        public string Attribute3 { get; set; }

        /// <summary>
        /// �������4
        /// </summary>
        [Column(name: "ATTRIBUTE_4", TypeName = "char")]
        [StringLength(5)]
        public string Attribute4 { get; set; }

        /// <summary>
        /// �������5
        /// </summary>
        [Column(name: "ATTRIBUTE_5", TypeName = "char")]
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
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}