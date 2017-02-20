/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����UnitInfo.cs
// �ļ�����������
//          ��λ��Ϣ���ʵ��Model��
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
    /// DB Table Name: BI_UNIT_INFO
    /// DB Table Name(CHS): ��λ��Ϣ��
    /// Edit by WangGang @ 2013-12-02 11:07:32 .
    /// </summary>
    [Serializable, Table("BI_UNIT_INFO")]
    public class UnitInfo : Entity
    {

        /// <summary>
        /// ��λID
        /// </summary>
        [Column("UNIT_ID")]
        [Key, StringLength(20)]
        public string UnitId { get; set; }

        /// <summary>
        /// ��λ����
        /// </summary>
        [Column("UNIT_NAME")]
        [Required]
        [StringLength(100)]
        public string UnitName { get; set; }

        /// <summary>
        /// ��λ˵��
        /// </summary>
        [Column("UNIT_EXP")]
        [StringLength(400)]
        public string UnitExp { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}