/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����TeamMemberInfo.cs
// �ļ�����������
//          �����Ա���ñ��ʵ��Model��
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
    /// DB Table Name: BI_TEAM_MEMBER_INFO
    /// DB Table Name(CHS): �����Ա���ñ�
    /// Edit by WangGang @ 2013-12-19 10:27:55 .
    /// </summary>
    [Serializable, Table("BI_TEAM_MEMBER_INFO")]
    public class TeamMemberInfo : Entity
    {

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("TEAM_ID", Order = 0)]
        [Key, StringLength(20)]
        public string TeamId { get; set; }

        /// <summary>
        /// ��ԱID
        /// </summary>
        [Column("MEM_ID", Order = 1)]
        [Key, StringLength(20)]
        public string MemId { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰������µ�DB����飬�˱��ѱ���ֳ������֡�
        /*
        /// <summary>
        /// ��������
        /// </summary>
        [Column("TEAM_NAME")]
        [Required]
        [StringLength(200)]
        public string TeamName { get; set; }

        /// <summary>
        /// ��������������Ԫ
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string DepartId { get; set; }
        */

        /// <summary>
        /// ���鳤����
        /// </summary>
        [Column(name: "TEAM_LEAD_FLG", TypeName = "char")]
        [StringLength(1)]
        public string TeamLeadFlg { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}

