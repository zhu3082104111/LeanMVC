/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����MachineSchedule.cs
// �ļ�����������
//          ��������ʵ�����ʵ��Model��
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
    /// DB Table Name: PD_MACHINE_SCHEDULE
    /// DB Table Name(CHS): ��������ʵ����
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_SCHEDULE")]
    public class MachineSchedule : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("MACH_ID", Order = 0)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("RUN_DATE", Order = 1)]
        [Key]
        public DateTime RunDate { get; set; }

        /// <summary>
        /// ��ռ��Сʱ��
        /// </summary>
        [Column("OCC_HOUR")]
        [DecimalPrecision(4, 2)]
        public decimal OccHour { get; set; }

    }
}