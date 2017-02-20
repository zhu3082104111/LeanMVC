/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����CompCalInfo.cs
// �ļ�����������
//          ��˾������Ϣ���ʵ��Model��
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
    /// DB Table Name: BI_COMP_CAL_INFO
    /// DB Table Name(CHS): ��˾������Ϣ��
    /// Edit by WangGang @ 2013-12-02 11:07:32 .
    /// </summary>
    [Serializable, Table("BI_COMP_CAL_INFO")]
    public class CompCalInfo : Entity
    {

        /// <summary>
        /// ���
        /// </summary>
        [Column(name: "YEAR", TypeName = "char", Order = 0)]
        [Key, StringLength(4)]
        public string Year { get; set; }

        /// <summary>
        /// �·�
        /// </summary>
        [Column(name: "MONTH", TypeName = "char", Order = 1)]
        [Key, StringLength(2)]
        public string Month { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        [Column(name: "DAY", TypeName = "char", Order = 2)]
        [Key, StringLength(2)]
        public string Day { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("WEK_NUM")]
        [DecimalPrecision(2, 0)]
        public decimal WekNum { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column(name: "THE_WEEK", TypeName = "char")]
        [StringLength(1)]
        public string TheWeek { get; set; }

        /// <summary>
        /// ��Ϣ������
        /// </summary>
        [Column(name: "REAT_FLG", TypeName = "char")]
        [StringLength(1)]
        public string ReatFlg { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}