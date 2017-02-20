/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ConcInfo.cs
// �ļ�����������
//          �ò���Ϣ���ʵ��Model��
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
    /// DB Table Name: QU_CONC_INFO
    /// DB Table Name(CHS): �ò���Ϣ��
    /// Edit by WangGang @ 2013-12-19 10:57:05 .
    /// </summary>
    [Serializable, Table("QU_CONC_INFO")]
    public class ConcInfo : Entity
    {

        /// <summary>
        /// ���
        /// </summary>
        [Column("SELF_INC_ID")]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰���DB�����ı䳤�ȡ�
        //[Key, DecimalPrecision(10, 0)]
        [Key, DecimalPrecision(20, 0)]
        public decimal SelfIncId { get; set; }

        /// <summary>
        /// ���ID
        /// </summary>
        [Column("PART_ID")]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰���DB��������ӷǿա�
        [Required]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// �ò�����
        /// </summary>
        [Column("GI_CLS")]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰���DB��������ӷǿա�
        [Required]
        [StringLength(3)]
        public string GiCls { get; set; }

        /// <summary>
        /// �ò�����
        /// </summary>
        [Column("GI_CLS_DESC")]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰���DB��������ӷǿա�
        [Required]
        [StringLength(400)]
        public string GiClsDesc { get; set; }

        /// <summary>
        /// ���κ�
        /// </summary>
        [Column("BTH_ID")]
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰���DB��������ӷǿա�
        [Required]
        [StringLength(20)]
        public string BthId { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}