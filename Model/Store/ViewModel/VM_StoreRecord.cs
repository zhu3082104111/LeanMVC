/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_StoreRecord.cs
// 文件功能描述：在库品一览画面的ViewModel
//      
// 修改履历：2014/1/8 刘云 新建
/*****************************************************************************/
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region 查询条件的视图Model类
    /// <summary>
    /// 查询条件的视图Model类
    /// </summary>
    public class VM_StoreRecordForSearch
    {
        public string Wh { set; get; }//仓库编号

        public string DepartmentID { set; get; }//部门

        public string PartType { set; get; }//类别
    }
    #endregion
    
    #region 查询结果的视图Model类
    /// <summary>
    /// 查询结果的视图Model类
    /// </summary>
    public class VM_StoreRecordForTableShow
    {
        public string PdtID { set; get; }//物料ID 

        public string PdtName { set; get; }//物料名称

        public decimal AlctQty { set; get; }//被预约数量

        public decimal RequiteQty { set; get; }//物料需求量

        public decimal OrderQty { set; get; }//下单数量

        public decimal CnsmQty { set; get; }//外协取料数量

        public decimal ArrveQty { set; get; }//到货数量

        public decimal IspcQty { set; get; }//检收数量

        public decimal UseableQty { set; get; }//可用在库数量

        public decimal CurrentQty { set; get; }//实际在库数量

        public decimal TotalAmt { set; get; }//总价
    }
    #endregion
}
