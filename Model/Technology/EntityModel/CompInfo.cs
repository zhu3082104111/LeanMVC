/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����CompInfo.cs
// �ļ�����������
//          ��������Ϣ���ʵ��Model��
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
    /// DB Table Name: PD_COMP_INFO
    /// DB Table Name(CHS): ��������Ϣ��
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_COMP_INFO")]
    public class CompInfo : Entity
    {

        /// <summary>
        /// ������ID
        /// </summary>
        [Column("COMP_ID")]
        [Key, StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// ��λ����
        /// </summary>
        [Column(name: "COMP_TYPE", TypeName = "char")]
        [StringLength(1)]
        public string CompType { get; set; }

        /// <summary>
        /// ��˾����
        /// </summary>
        [Column("COMP_NAME")]
        [StringLength(200)]
        public string CompName { get; set; }

        /// <summary>
        /// ��˾��ַ
        /// </summary>
        [Column("COMP_ADD")]
        [StringLength(300)]
        public string CompAdd { get; set; }

        /// <summary>
        /// �ʱ�
        /// </summary>
        [Column("COMP_ZC")]
        [StringLength(10)]
        public string CompZc { get; set; }

        /// <summary>
        /// �����ȼ�
        /// </summary>
        [Column(name: "CRE_LEVEL", TypeName = "char")]
        [StringLength(1)]
        public string CreLevel { get; set; }

        /// <summary>
        /// �绰1
        /// </summary>
        [Column("TEL_NO_1")]
        [StringLength(16)]
        public string TelNo1 { get; set; }

        /// <summary>
        /// �绰2
        /// </summary>
        [Column("TEL_NO_2")]
        [StringLength(16)]
        public string TelNo2 { get; set; }

        /// <summary>
        /// ����1
        /// </summary>
        [Column("FAX_1")]
        [StringLength(16)]
        public string Fax1 { get; set; }

        /// <summary>
        /// ����2
        /// </summary>
        [Column("FAX_2")]
        [StringLength(16)]
        public string Fax2 { get; set; }

        /// <summary>
        /// ��ϵ������
        /// </summary>
        [Column("CONTACTS_NAME")]
        [StringLength(32)]
        public string ContactsName { get; set; }

    }
}