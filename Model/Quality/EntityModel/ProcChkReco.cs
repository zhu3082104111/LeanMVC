/*****************************************************************************/
// Copyright (C) 2013 ����˼Ԫ������޹�˾
// ��Ȩ���С�
//
// �ļ�����ProcChkReco.cs
// �ļ�����������
//          ���̼����¼�����ʵ��Model��
//      
// �޸�������2014-01-09 ��� �޸�
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
    /// DB Table Name: QU_PROC_CHK_RECO
    /// DB Table Name(CHS): ���̼����¼����
    /// Edit by WangGang @ 2014-01-09 16:50:03 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_RECO")]
    public class ProcChkReco : Entity
    {

        /// <summary>
        /// ��¼����
        /// </summary>
        [Column("CHK_RECO_ID")]
        [Key, StringLength(20)]
        public string ChkRecoId { get; set; }

        /// <summary>
        /// ���ID
        /// </summary>
        [Column("PART_ID")]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [Column("PART_NAME")]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// ����ͺ�
        /// </summary>
        [Column("PART_MOD")]
        [StringLength(200)]
        public string PartMod { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// �豸����
        /// </summary>
        [Column("EQUIP_NAME")]
        [StringLength(200)]
        public string EquipName { get; set; }

        /// <summary>
        /// �豸���
        /// </summary>
        [Column("EQUIP_NO")]
        [StringLength(20)]
        public string EquipNo { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("PROC_NAME")]
        [StringLength(200)]
        public string ProcName { get; set; }

        /// <summary>
        /// ����˳���
        /// </summary>
        [Column("PROC_SEQU_NO")]
        [StringLength(5)]
        public string ProcSequNo { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("SUB_PROC_NAME")]
        [StringLength(200)]
        public string SubProcName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Column("QUOT_NUM")]
        [StringLength(5)]
        public string QuotNum { get; set; }

        /// <summary>
        /// ����ԱID
        /// </summary>
        [Column("OPERA_ID")]
        [StringLength(20)]
        public string OperaId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Column("TEAM_ID")]
        [StringLength(20)]
        public string TeamId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// �Լ���ʱ��
        /// </summary>
        [Column("SEL_CHK_TIME")]
        [StringLength(6)]
        public string SelChkTime { get; set; }

        /// <summary>
        /// �Լ���ǩ��
        /// </summary>
        [Column("SEL_CHK_SIGN")]
        [StringLength(50)]
        public string SelChkSign { get; set; }

        /// <summary>
        /// �׼���ʱ��
        /// </summary>
        [Column("FIRST_CHK_TIME")]
        [StringLength(6)]
        public string FirstChkTime { get; set; }

        /// <summary>
        /// �׼���ǩ��
        /// </summary>
        [Column("FIRST_CHK_SIGN")]
        [StringLength(50)]
        public string FirstChkSign { get; set; }

        /// <summary>
        /// ��һ��Ѳ��ʱ��
        /// </summary>
        [Column("1ST_CHK_TIME")]
        [StringLength(6)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _1stChkTime { get; set; }
        public string FirstCheckTime { get; set; }

        /// <summary>
        /// ��һ��Ѳ��ǩ��
        /// </summary>
        [Column("1ST_CHK_SIGN")]
        [StringLength(50)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _1stChkSign { get; set; }
        public string FirstCheckSign { get; set; }

        /// <summary>
        /// �ڶ���Ѳ��ʱ��
        /// </summary>
        [Column("2ND_CHK_TIME")]
        [StringLength(6)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _2ndChkTime { get; set; }
        public string SecondCheckTime { get; set; }

        /// <summary>
        /// �ڶ���Ѳ��ǩ��
        /// </summary>
        [Column("2ND_CHK_SIGN")]
        [StringLength(50)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _2ndChkSign { get; set; }
        public string SecondCheckSign { get; set; }

        /// <summary>
        /// ������Ѳ��ʱ��
        /// </summary>
        [Column("3RD_CHK_TIME")]
        [StringLength(6)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _3rdChkTime { get; set; }
        public string ThirdCheckTime { get; set; }

        /// <summary>
        /// ������Ѳ��ǩ��
        /// </summary>
        [Column("3RD_CHK_SIGN")]
        [StringLength(50)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _3rdChkSign { get; set; }
        public string ThirdCheckSign { get; set; }

        /// <summary>
        /// ���Ĵ�Ѳ��ʱ��
        /// </summary>
        [Column("4TH_CHK_TIME")]
        [StringLength(6)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _4thChkTime { get; set; }
        public string FourthCheckTime { get; set; }

        /// <summary>
        /// ���Ĵ�Ѳ��ǩ��
        /// </summary>
        [Column("4TH_CHK_SIGN")]
        [StringLength(50)]
        //�� �� �ˣ����
        //�޸����ڣ�2014-01-09
        //�޸�ԭ���������ڲ��淶����������ܵ��²��ȶ��������޸ġ�
        //public string _4thChkSign { get; set; }
        public string FourthCheckSign { get; set; }

        /// <summary>
        /// ĩ��ʱ��
        /// </summary>
        [Column("LAST_CHK_TIME")]
        [StringLength(6)]
        public string LastChkTime { get; set; }

        /// <summary>
        /// ĩ��ǩ��
        /// </summary>
        [Column("LAST_CHK_SIGN")]
        [StringLength(50)]
        public string LastChkSign { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        [Column("OPINION")]
        [StringLength(200)]
        public string Opinion { get; set; }

        /// <summary>
        /// �ӹ���ƱID
        /// </summary>
        [Column("BILL_ID")]
        [StringLength(20)]
        public string BillId { get; set; }

        /// <summary>
        /// �ӹ���Ʊ�к�
        /// </summary>
        [Column("BI_LINE_NO")]
        [StringLength(2)]
        public string BiLineNo { get; set; }

        /// <summary>
        /// �ʼ�ԱID
        /// </summary>
        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnId { get; set; }

        /// <summary>
        /// ���鵥��
        /// </summary>
        [Column("CHK_LIST_ID")]
        [StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}