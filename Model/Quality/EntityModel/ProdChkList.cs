/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProdChkList.cs
// �ļ�����������
//          ��Ʒ���鵥���ʵ��Model��
//      
// �޸�������2013-12-26 ��� �޸�
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
    /// DB Table Name: QU_PROD_CHK_LIST
    /// DB Table Name(CHS): ��Ʒ���鵥��
    /// Edit by WangGang @ 2013-12-26 16:58:05 .
    /// </summary>
    [Serializable, Table("QU_PROD_CHK_LIST")]
    public class ProdChkList : Entity
    {

        /// <summary>
        /// ���鵥��
        /// </summary>
        [Column("CHK_LIST_ID")]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// �ͻ�������
        /// </summary>
        [Column("CLN_ODR_ID")]
        [StringLength(20)]
        public string ClnOdrId { get; set; }

        /// <summary>
        /// �ͻ�������ϸ
        /// </summary>
        [Column("CLN_ODR_DTL")]
        [StringLength(2)]
        public string ClnOdrDtl { get; set; }

        /// <summary>
        /// ��ƷID
        /// </summary>
        [Column("PRODUCT_ID")]
        [StringLength(20)]
        public string ProductId { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [Column("PROD_NAME")]
        [StringLength(200)]
        public string ProdName { get; set; }

        /// <summary>
        /// ��Ʒ�ͺ�
        /// </summary>
        [Column("PROD_ABBREV")]
        [StringLength(20)]
        public string ProdAbbrev { get; set; }

        /// <summary>
        /// ��Ʒͼ��
        /// </summary>
        [Column("PROD_SCHE")]
        [StringLength(100)]
        public string ProdSche { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("STO_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal StoQty { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("CHK_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal ChkQty { get; set; }

        /// <summary>
        ///// ������
        /// �������
        /// </summary>
        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺸������µ�DB���������Ӧ��
        //[Column(name: "CHK_RES", TypeName = "char")]
        //[StringLength(1)]
        [Column("CHK_RES")]
        [StringLength(400)]
        public string ChkRes { get; set; }

        /// <summary>
        /// ������Ԫ�鳤ID
        /// </summary>
        [Column("TEAM_LEAD_ID")]
        [StringLength(20)]
        public string TeamLeadId { get; set; }

        /// <summary>
        /// Ʒ����������ID
        /// </summary>
        [Column("QUA_MAG_ID")]
        [StringLength(20)]
        public string QuaMagId { get; set; }

        /// <summary>
        /// ����ԱID
        /// </summary>
        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnId { get; set; }

        /// <summary>
        /// ���״̬
        /// </summary>
        [Column(name: "STO_STAT", TypeName = "char")]
        [StringLength(1)]
        public string StoStat { get; set; }

        /// <summary>
        /// ���ϼ����Ҫ��
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { get; set; }

        /// <summary>
        /// ��װ���ȵ�ID
        /// </summary>
        [Column("ASS_DISP_ID")]
        [StringLength(20)]
        public string AssDispId { get; set; }

        /// <summary>
        /// ��Ʒ���ֵ���
        /// </summary>
        [Column("PROD_WAREH_ID")]
        [StringLength(20)]
        public string ProdWarehId { get; set; }

        //�� �� �ˣ����
        //�޸����ڣ�2013-12-19
        //�޸�ԭ�򣺰������µ�DB���������µ��ֶΡ�
        /// <summary>
        /// �ò�Ʒʹ������
        /// </summary>
        [Column(name: "USD_GI_FLG", TypeName = "char")]
        [StringLength(1)]
        public string UsdGiFlg { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}