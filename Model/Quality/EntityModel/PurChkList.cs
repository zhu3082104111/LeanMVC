/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����PurChkList.cs
// �ļ�����������
//          �������鵥���ʵ��Model��
//      
// �޸�������2013-12-02 ��� �޸�
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
    /// DB Table Name: QU_PUR_CHK_LIST
    /// DB Table Name(CHS): �������鵥��
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PUR_CHK_LIST")]
    public class PurChkList : Entity
    {

        /// <summary>
        /// ���鵥��
        /// </summary>
        [Column("CHK_LIST_ID")]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// �ʼ�����
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// ���ID
        /// </summary>
        [Column("PART_ID")]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Column("PART_NAME")]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// ����ͺ�
        /// </summary>
        [Column("PART_MOD")]
        [StringLength(200)]
        public string PartMod { get; set; }

        /// <summary>
        /// ��Ӧ��ID
        /// </summary>
        [Column("COMP_ID")]
        [StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// ��Ӧ������
        /// </summary>
        [Column("COMP_NAME")]
        [StringLength(200)]
        public string CompName { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("PUR_DT")]
        public DateTime? PurDt { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("STO_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal StoQty { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("CHK_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal ChkQty { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        [Column(name: "CHK_CNT_APR", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntApr { get; set; }

        /// <summary>
        /// ��������ֵ��
        /// </summary>
        [Column(name: "CHK_CNT_VAL", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntVal { get; set; }

        /// <summary>
        /// �������ݲ���
        /// </summary>
        [Column(name: "CHK_CNT_MAT", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntMat { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        [Column(name: "CHK_CNT_PER", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntPer { get; set; }

        /// <summary>
        /// ����״̬
        /// </summary>
        [Column("SUP_STAT")]
        [StringLength(200)]
        public string SupStat { get; set; }

        /// <summary>
        /// �������ۺ��ж�
        /// </summary>
        [Column(name: "SUP_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string SupComRes { get; set; }

        /// <summary>
        /// LDK�ۺ��ж�
        /// </summary>
        [Column(name: "LDK_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string LdkComRes { get; set; }

        /// <summary>
        /// Ʒ����������ID
        /// </summary>
        [Column("QUA_MAG_ID")]
        [StringLength(20)]
        public string QuaMagId { get; set; }

        /// <summary>
        /// ����ԱID
        /// </summary>
        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnId { get; set; }

        /// <summary>
        /// ���״̬
        /// </summary>
        [Column(name: "STO_STAT", TypeName = "char")]
        [StringLength(1)]
        public string StoStat { get; set; }

        /// <summary>
        /// �⹺��Э����
        /// </summary>
        [Column(name: "OS_SUP_FLG", TypeName = "char")]
        [StringLength(1)]
        public string OsSupFlg { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// �ò�����
        /// </summary>
        [Column("GI_CLS")]
        [StringLength(3)]
        public string GiCls { get; set; }

        /// <summary>
        /// ���ϼ����Ҫ��
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { get; set; }

        /// <summary>
        /// �ͻ�����
        /// </summary>
        [Column("DLY_ODR_ID")]
        [StringLength(20)]
        public string DlyOdrId { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}