/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����Process.cs
// �ļ�����������
//          ������Ϣ���ʵ��Model��
//      
// �޸�������2014-01-06 ��� �޸�
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
    /// DB Table Name: PD_PROCESS
    /// DB Table Name(CHS): ������Ϣ��
    /// Edit by WangGang @ 2014-01-06 11:26:01 .
    /// </summary>
    [Serializable, Table("PD_PROCESS")]
    public class Process : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID")]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// ��λʱ��ӹ��������Լӹ���
        /// </summary>
        [Column("SE_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal SeUtProdQty { get; set; }

        /// <summary>
        /// ��λʱ��ӹ���������Э��
        /// </summary>
        [Column("AS_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal AsUtProdQty { get; set; }

        /// <summary>
        /// ��λʱ��ӹ��������⹺��
        /// </summary>
        [Column("PU_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal PuUtProdQty { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("PROC_NAME")]
        [Required]
        [StringLength(200)]
        public string ProcName { get; set; }

        /// <summary>
        /// �Լӹ��ɷ�����
        /// </summary>
        [Column(name: "SE_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string SeFlg { get; set; }

        /// <summary>
        /// ��Э�ɷ�����
        /// </summary>
        [Column(name: "AS_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string AsFlg { get; set; }

        /// <summary>
        /// �⹺�ɷ�����
        /// </summary>
        [Column(name: "PU_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string PuFlg { get; set; }

        /// <summary>
        /// Ĭ�Ϲ������
        /// </summary>
        [Column(name: "DEF_PROC_CATG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string DefProcCatg { get; set; }

        /// <summary>
        /// ��λID���Լӹ���
        /// </summary>
        [Column("SE_UNIT_ID")]
        [StringLength(20)]
        public string SeUnitId { get; set; }

        /// <summary>
        /// ��λID����Э��
        /// </summary>
        [Column("AS_UNIT_ID")]
        [StringLength(20)]
        public string AsUnitId { get; set; }

        /// <summary>
        /// ��λID���⹺��
        /// </summary>
        [Column("PU_UNIT_ID")]
        [StringLength(20)]
        public string PuUnitId { get; set; }

        /// <summary>
        /// �Լӹ�����
        /// </summary>
        [Column("SE_UNIT_PRICE")]
        [DecimalPrecision(16, 6)]
        public decimal SeUnitPrice { get; set; }

        /// <summary>
        /// ���۵�λID
        /// </summary>
        [Column("PRI_UNIT_ID")]
        [StringLength(20)]
        public string PriUnitId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("DESCRIPTION")]
        [StringLength(400)]
        public string Description { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2014-01-06
        //�޸�ԭ�򣺰������µ�DB��������ӡ�ע�����֡��ֶΡ�
        /// <summary>
        /// ע������
        /// </summary>
        [Column(name: "IM_FLG", TypeName = "char")]
        [StringLength(1)]
        public string ImFlg { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Column("ATTRI_RATE")]
        [DecimalPrecision(12, 2)]
        public decimal AttriRate { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2014-01-06
        //�޸�ԭ�򣺰������µ�DB��������ӡ���Ҫ׼���������Լӹ������ֶΡ�
        /// <summary>
        /// ��Ҫ׼���������Լӹ���
        /// </summary>
        [Column("SE_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal SeNpDays { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2014-01-06
        //�޸�ԭ�򣺰������µ�DB��������ӡ���Ҫ׼����������Э�����ֶΡ�
        /// <summary>
        /// ��Ҫ׼����������Э��
        /// </summary>
        [Column("AS_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal AsNpDays { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2014-01-06
        //�޸�ԭ�򣺰������µ�DB��������ӡ���Ҫ׼���������⹺�����ֶΡ�
        /// <summary>
        /// ��Ҫ׼���������⹺��
        /// </summary>
        [Column("PU_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal PuNpDays { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}