/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����TeamInfo.cs
// �ļ�����������
//          ������Ϣ���ʵ��Model��
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
    /// DB Table Name: BI_TEAM_INFO
    /// DB Table Name(CHS): ������Ϣ��
    /// Edit by WangGang @ 2013-12-19 10:27:55 .
    /// </summary>
    [Serializable, Table("BI_TEAM_INFO")]
    public class TeamInfo : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("TEAM_ID")]
        [Key, StringLength(20)]
        public string TeamId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("TEAM_NAME")]
        [Required]
        [StringLength(200)]
        public string TeamName { get; set; }

        /// <summary>
        /// ��������������ԪID
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}