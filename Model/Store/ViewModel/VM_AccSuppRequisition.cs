/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_AccSuppRequisition.cs
// 文件功能描述：
//          附件库外协领料画面查询视图MODEL
//
// 创建标识：2013/12/31 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region 附件库外协领料单画面查询结果视图类
    public class VM_AccSupplierRequisitionForTableShow
    {
        //领料单号
        public string RequOrderID { get; set; }

        //请领单位
        public string ApplyComName { set; get; }

        //产品零件名称
        public string ProdPartName { get; set; }

        //加工原料名称
        public string MaterielName { get; set; }

        //需求总量
        public decimal? RequireAcount { get; set; }

        //库存数量
        public decimal? StorageAcount { get; set; }

        //零件ID
        public string PartID { get; set; }
    }

     #endregion

    #region 附件库外协领料单画面查询条件视图类
    public class VM_AccSupplierRequisitionForSearch
    {
        //外协调度单号
        public string SupOrderID { get; set; }
    }
    #endregion
}
