/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProdChkListDetail.cs
// �ļ�����������
//          ��Ʒ���鵥��ϸ���ʵ��Model��
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
    /// DB Table Name: QU_PROD_CHK_LIST_DETAIL
    /// DB Table Name(CHS): ��Ʒ���鵥��ϸ��
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PROD_CHK_LIST_DETAIL")]
    public class ProdChkListDetail : Entity
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
        /// ʵ����1
        /// </summary>
        [Column(name: "CHK_RES_1", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes1 { get; set; }

        /// <summary>
        /// ʵ����2
        /// </summary>
        [Column(name: "CHK_RES_2", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes2 { get; set; }

        /// <summary>
        /// ʵ����3
        /// </summary>
        [Column(name: "CHK_RES_3", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes3 { get; set; }

        /// <summary>
        /// ʵ����4
        /// </summary>
        [Column(name: "CHK_RES_4", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes4 { get; set; }

        /// <summary>
        /// ʵ����5
        /// </summary>
        [Column(name: "CHK_RES_5", TypeName = "char")]
        [StringLength(1)]
        public string ChkRes5 { get; set; }

    }
}