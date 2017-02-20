/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����CompMaterialInfo.cs
// �ļ�����������
//          �����̹�����Ϣ���ʵ��Model��
//      
// �޸�������2013-12-16 ��� �޸�
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
    /// DB Table Name: PD_COMP_MATERIAL_INFO
    /// DB Table Name(CHS): �����̹�����Ϣ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_COMP_MATERIAL_INFO")]
    public class CompMaterialInfo : Entity
    {

        /// <summary>
        /// ������ID
        /// </summary>
        [Column("COMP_ID", Order = 0)]
        [Key, StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// ��Ʒ���ID
        /// </summary>
        [Column("PDT_ID", Order = 1)]
        [Key, StringLength(20)]
        public string PdtId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column(name: "COMP_TYPE", TypeName = "char", Order = 2)]
        [Key, StringLength(1)]
        public string CompType { get; set; }

        /// <summary>
        /// ���ÿ�ʼ����
        /// </summary>
        [Column("ACTIV_STR_DT", Order = 3)]
        [Key]
        public DateTime ActivStrDt { get; set; }

        /// <summary>
        /// ������ֹ����
        /// </summary>
        [Column("ACTIV_END_DT")]
        [Required]
        public DateTime ActivEndDt { get; set; }

        /// <summary>
        /// ��Ӧ���ṩ�ͺ�
        /// </summary>
        [Column("VEN_MODEL_ID")]
        [StringLength(200)]
        public string VenModelId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("UNIT_PRICE")]
        [DecimalPrecision(16, 6)]
        public decimal UnitPrice { get; set; }

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
        /// ��Ӧ�̲���
        /// </summary>
        [Column("SUP_CAPA")]
        [DecimalPrecision(16, 6)]
        public decimal SupCapa { get; set; }

        /// <summary>
        /// ��Ӧ�̲��ܵ�λID
        /// </summary>
        [Column("SUPY_UNIT_ID")]
        [StringLength(20)]
        public string SupyUnitId { get; set; }

        /// <summary>
        /// ����������������
        /// </summary>
        [Column("SUP_CYC_DAY")]
        [DecimalPrecision(12, 2)]
        public decimal SupCycDay { get; set; }

        /// <summary>
        /// ��Ӧ���������ȼ�
        /// </summary>
        [Column(name: "QLT_LEVEL", TypeName = "char")]
        [StringLength(1)]
        public string QltLevel { get; set; }

        /// <summary>
        /// �����Ӧ����
        /// </summary>
        [Column("MOTH_MAX_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal MothMaxQty { get; set; }

        /// <summary>
        /// ��С��������
        /// </summary>
        [Column("MIN_ODR_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal MinOdrQty { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}