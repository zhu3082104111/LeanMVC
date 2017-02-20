/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����CodeMake.cs
// �ļ�����������
//          �������ɱ��ʵ��Model��
//      
// �޸�������2013-12-18 ��� �޸�
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
    /// DB Table Name: BI_CODE_MAKE
    /// DB Table Name(CHS): �������ɱ�
    /// Edit by WangGang @ 2013-12-18 11:50:32 .
    /// </summary>
    [Serializable, Table("BI_CODE_MAKE")]
    public class CodeMake : Entity
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Column(name: "CD_CATG", TypeName = "char", Order = 0)]
        [Key, StringLength(5)]
        public string CdCatg { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char", Order = 1)]
        [Key, StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("DATE", Order = 2)]
        //[Key, StringLength(400)]
        [Key, StringLength(8)]
        public string Date { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [Column("OD_NUM")]
        [StringLength(7)]
        public string OdNum { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}