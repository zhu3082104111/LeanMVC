using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_UI.Helper
{
    public class UIConstant
    {
        public static int VIEW_SEARCH = 0;//视图模式  一览
        public static int VIEW_DIALOGUE = 1;//弹出框

        public static int OPERATE_MODEL_CREATE = 0;//操作模式  新增
        public static int OPERATE_MODEL_EDIT = 1;//编辑
        public static int OPERATE_MODEL_DETAIL = 2;//详细查看


        /// <summary>
        /// 代东泽 20131212
        /// 领料单来源
        /// </summary>
        public static class PickingBillComeFrom
        {
            public const string CIRCULATE_CARD = "流转卡领料单";

            public const string ASSEM_DISPATCH = "总装领料单";

            public const string ASSEM_DISPATCH_REMEDY = "总装补料单";

            public const string OUTASSIT_DISPATCH = "外协领料";


        }
        /// <summary>
        /// 代东泽 20131212
        /// 领料单状态
        /// </summary>
        public static class PickingBillState
        {
            public const string PICKING = "已领料";

            public const string NOPICKING = "未领料";


        }

    }
}