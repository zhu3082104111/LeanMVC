/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����PurChkListDetail.cs
// �ļ�����������
//          �������鵥��ϸ���ʵ��Model��
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
    /// DB Table Name: QU_PUR_CHK_LIST_DETAIL
    /// DB Table Name(CHS): �������鵥��ϸ��
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PUR_CHK_LIST_DETAIL")]
    public class PurChkListDetail : Entity
    {

        /// <summary>
        /// ���鵥��
        /// </summary>
        [Column("CHK_LIST_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// ���鵥���
        /// </summary>
        [Column("LIST_NUM", Order = 1)]
        [Key, StringLength(2)]
        public string ListNum { get; set; }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        [Column("CHK_ITEM")]
        [StringLength(200)]
        public string ChkItem { get; set; }

        /// <summary>
        /// �����׼
        /// </summary>
        [Column("CHK_STAND")]
        [StringLength(200)]
        public string ChkStand { get; set; }

        /// <summary>
        /// ��Ӧ�̼�������1
        /// </summary>
        [Column("SUP_CONT_1")]
        [StringLength(200)]
        public string SupCont1 { get; set; }

        /// <summary>
        /// LDK��������1
        /// </summary>
        [Column("LDK_CONT_1")]
        [StringLength(200)]
        public string LdkCont1 { get; set; }

        /// <summary>
        /// ��Ӧ�̼�������2
        /// </summary>
        [Column("SUP_CONT_2")]
        [StringLength(200)]
        public string SupCont2 { get; set; }

        /// <summary>
        /// LDK��������2
        /// </summary>
        [Column("LDK_CONT_2")]
        [StringLength(200)]
        public string LdkCont2 { get; set; }

        /// <summary>
        /// ��Ӧ�̼�������3
        /// </summary>
        [Column("SUP_CONT_3")]
        [StringLength(200)]
        public string SupCont3 { get; set; }

        /// <summary>
        /// LDK��������3
        /// </summary>
        [Column("LDK_CONT_3")]
        [StringLength(200)]
        public string LdkCont3 { get; set; }

        /// <summary>
        /// ��Ӧ�̼�������4
        /// </summary>
        [Column("SUP_CONT_4")]
        [StringLength(200)]
        public string SupCont4 { get; set; }

        /// <summary>
        /// LDK��������4
        /// </summary>
        [Column("LDK_CONT_4")]
        [StringLength(200)]
        public string LdkCont4 { get; set; }

        /// <summary>
        /// ��Ӧ�̼�������5
        /// </summary>
        [Column("SUP_CONT_5")]
        [StringLength(200)]
        public string SupCont5 { get; set; }

        /// <summary>
        /// LDK��������5
        /// </summary>
        [Column("LDK_CONT_5")]
        [StringLength(200)]
        public string LdkCont5 { get; set; }

        /// <summary>
        /// ��Ӧ���ж����
        /// </summary>
        [Column(name: "SUP_RES", TypeName = "char")]
        [StringLength(1)]
        public string SupRes { get; set; }

        /// <summary>
        /// LDK�ж����
        /// </summary>
        [Column(name: "LDK_RES", TypeName = "char")]
        [StringLength(1)]
        public string LdkRes { get; set; }

    }
}