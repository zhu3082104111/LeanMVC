/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProcessDetail.cs
// �ļ�����������
//          ������ϸ��Ϣ���ʵ��Model��
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
    /// DB Table Name: PD_PROCESS_DETAIL
    /// DB Table Name(CHS): ������ϸ��Ϣ��
    /// Edit by WangGang @ 2013-12-19 14:00:46 .
    /// </summary>
    [Serializable, Table("PD_PROCESS_DETAIL")]
    public class ProcessDetail : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// ˳���
        /// </summary>
        [Column("SEQ_NO", Order = 1)]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰������µ�DB����鼰ָժ����Ҫ�ĳ�Integer���͡�
        //[Key, StringLength(5)]
        //public string SeqNo { get; set; }
        [Key]
        public int SeqNo { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("SUB_PROC_NAME")]
        [Required]
        [StringLength(50)]
        public string SubProcName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Column("QUOT_NUM")]
        [StringLength(5)]
        public string QuotNum { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("OPERA_RAT")]
        [DecimalPrecision(7, 2)]
        public decimal OperaRat { get; set; }

        /// <summary>
        /// ��λʱ��ӹ�����
        /// </summary>
        [Column("SE_UT_PROD_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal SeUtProdQty { get; set; }

        /// <summary>
        /// ��λID���Լӹ���
        /// </summary>
        [Column("SE_UNIT_ID")]
        [StringLength(20)]
        public string SeUnitId { get; set; }

        /// <summary>
        /// �Լӹ�����
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
        /// ��������
        /// </summary>
        [Column("DESCRIPTION")]
        [StringLength(400)]
        public string Description { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}