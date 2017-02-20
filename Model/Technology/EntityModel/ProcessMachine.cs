/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProcessMachine.cs
// �ļ�����������
//          ����ͻ�����Ӧ���ʵ��Model��
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
    /// DB Table Name: PD_PROCESS_MACHINE
    /// DB Table Name(CHS): ����ͻ�����Ӧ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PROCESS_MACHINE")]
    public class ProcessMachine : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// ����˳���
        /// </summary>
        [Column("SEQ_NO", Order = 1)]
        [Key, StringLength(5)]
        public string SeqNo { get; set; }

        /// <summary>
        /// ���ID
        /// </summary>
        [Column("PART_ID", Order = 2)]
        [Key, StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("MACH_ID", Order = 3)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// ��λʱ��ӹ�����
        /// </summary>
        [Column("UT_PROD_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal UtProdQty { get; set; }

        /// <summary>
        /// ��λID
        /// </summary>
        [Column("UNIT_ID")]
        [StringLength(20)]
        public string UnitId { get; set; }

    }
}

