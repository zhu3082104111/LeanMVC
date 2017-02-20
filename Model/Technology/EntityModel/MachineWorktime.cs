/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����MachineWorktime.cs
// �ļ�����������
//          ������תʱ����ʵ��Model��
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
    /// DB Table Name: PD_MACHINE_WORKTIME
    /// DB Table Name(CHS): ������תʱ���
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_WORKTIME")]
    public class MachineWorktime : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("MACH_ID", Order = 0)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// ���ÿ�ʼʱ��
        /// </summary>
        [Column("ACTIV_STR_DT", Order = 1)]
        [Key]
        public DateTime ActivStrDt { get; set; }

        /// <summary>
        /// ������ֹ����
        /// </summary>
        [Column("ACTIV_END_DT")]
        public DateTime? ActivEndDt { get; set; }

        /// <summary>
        /// ÿ�������תСʱ��
        /// </summary>
        [Column("MAX_WORK_TIME")]
        [DecimalPrecision(4, 2)]
        public decimal MaxWorkTime { get; set; }

    }
}