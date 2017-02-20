/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProcChkList.cs
// �ļ�����������
//          ���̼��鵥���ʵ��Model��
//      
// �޸�������2013-12-19 ��� �޸�
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
    /// DB Table Name: QU_PROC_CHK_LIST
    /// DB Table Name(CHS): ���̼��鵥��
    /// Edit by WangGang @ 2013-12-19 11:30:06 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_LIST")]
    public class ProcChkList : Entity
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
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

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
        /// ���״̬
        /// </summary>
        [Column(name: "STO_STAT", TypeName = "char")]
        [StringLength(1)]
        public string StoStat { get; set; }

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

        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺸���DB����飬���ӡ������ۺ��ж����ֶΡ�
        /// <summary>
        /// �����ۺ��ж�
        /// </summary>
        [Column(name: "CHK_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string ChkComRes { get; set; }

        /// <summary>
        /// �ӹ��ͻ�����
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