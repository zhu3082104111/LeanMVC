/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProcChkRecoDetail.cs
// �ļ�����������
//          ���̼����¼����ϸ���ʵ��Model��
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
    /// DB Table Name: QU_PROC_CHK_RECO_DETAIL
    /// DB Table Name(CHS): ���̼����¼����ϸ��
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_RECO_DETAIL")]
    public class ProcChkRecoDetail : Entity
    {

        /// <summary>
        /// ��¼����
        /// </summary>
        [Column("CHK_RECO_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ChkRecoId { get; set; }

        /// <summary>
        /// ��¼�����
        /// </summary>
        [Column("RECO_NUM", Order = 1)]
        [Key, StringLength(2)]
        public string RecoNum { get; set; }

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
        /// �Լ���
        /// </summary>
        [Column(name: "SEL_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string SelChkRes { get; set; }

        /// <summary>
        /// �׼���
        /// </summary>
        [Column(name: "FIRST_CHK_", TypeName = "char")]
        [StringLength(1)]
        public string FirstChk { get; set; }

        /// <summary>
        /// ��һ��Ѳ����
        /// </summary>
        [Column(name: "1ST_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string FirstChkRes { get; set; }

        /// <summary>
        /// �ڶ���Ѳ����
        /// </summary>
        [Column(name: "2ND_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string SecondChkRes { get; set; }

        /// <summary>
        /// ������Ѳ����
        /// </summary>
        [Column(name: "3RD_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string ThirdChkRes { get; set; }

        /// <summary>
        /// ���Ĵ�Ѳ����
        /// </summary>
        [Column(name: "4TH_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string FourthChkRes { get; set; }

        /// <summary>
        /// ĩ����
        /// </summary>
        [Column(name: "LAST_CHK_RES", TypeName = "char")]
        [StringLength(1)]
        public string LastChkRes { get; set; }

    }
}