/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProdMaterRequ1.cs
// �ļ�����������
//          ��Э���ϵ����ʵ��Model��
//      
// �޸�������2014-01-13 ��� �޸�
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
    /// DB Table Name: PD_PROD_MATER_REQU1
    /// DB Table Name(CHS): ��Э���ϵ���
    /// Edit by WangGang @ 2014-01-13 11:09:01 .
    /// </summary>
    [Serializable, Table("PD_PROD_MATER_REQU1")]
    public class ProdMaterRequ1 : Entity
    {

        /// <summary>
        /// ���ϵ���
        /// </summary>
        [Column("MATER_REQ_NO")]
        [Key, StringLength(20)]
        public string MaterReqNo { get; set; }

        /// <summary>
        /// ���ϵ�����
        /// </summary>
        [Column(name: "MATER_REQ_TYPE", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string MaterReqType { get; set; }

        /// <summary>
        /// ���ò���ID
        /// </summary>
        [Column("DEPA_ID")]
        [StringLength(20)]
        public string DepaId { get; set; }

        /// <summary>
        /// ��;����
        /// </summary>
        [Column("PURPOSE")]
        [StringLength(200)]
        public string Purpose { get; set; }

        /// <summary>
        /// �ֿ�ԱID
        /// </summary>
        [Column("WH_PSN_ID")]
        [StringLength(20)]
        public string WhPsnId { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        [Column(name: "DEPA_AUDIT_FLG", TypeName = "char")]
        [StringLength(1)]
        public string DepaAuditFlg { get; set; }

        /// <summary>
        /// ���������ID
        /// </summary>
        [Column("DEPA_AUDITOR_ID")]
        [StringLength(20)]
        public string DepaAuditorId { get; set; }

        /// <summary>
        /// ������ID
        /// </summary>
        [Column("MATER_HANDLER_ID")]
        [StringLength(20)]
        public string MaterHandlerId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("REQU_DT")]
        public DateTime? RequDt { get; set; }

        /// <summary>
        /// ���ϵ��������ݺ�
        /// </summary>
        [Column("RELA_BIL_ID")]
        [StringLength(20)]
        public string RelaBilId { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}