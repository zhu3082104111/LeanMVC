/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����MachineInfo.cs
// �ļ�����������
//          ������Ϣ���ʵ��Model��
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
    /// DB Table Name: PD_MACHINE_INFO
    /// DB Table Name(CHS): ������Ϣ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_INFO")]
    public class MachineInfo : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("MACH_ID")]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("MACH_NAME")]
        [StringLength(200)]
        public string MachName { get; set; }

        /// <summary>
        /// �����ͺ�
        /// </summary>
        [Column("MACH_MOD")]
        [StringLength(200)]
        public string MachMod { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}