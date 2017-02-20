/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierInstructionListController.cs
// 文件功能描述：
//          外协计划一览画面的Controller
//      
// 修改履历：2013/11/13 宋彬磊 新建
/*****************************************************************************/
using System.Web.Mvc;
using Extensions;
using Model;
using BLL;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外协计划一览画面的Controller
    /// </summary>
    public class SupplierInstructionListController : BaseController
    {
        // 外购指示和外协指示的Service
        private IInstructionsService instructionsService;

        /// <summary>
        /// 外协计划一览画面的构造函数
        /// </summary>
        /// <param name="instructionsService">外购指示和外协指示的Service</param>
        public SupplierInstructionListController(IInstructionsService instructionsService)
        {
            this.instructionsService = instructionsService;
        }

        /// <summary>
        /// 显示外协计划（外协指示）一览画面
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierInstructionList()
        {
            // 返回视图
            return View();
        }

        /// <summary>
        /// 查询并取得外协计划一览画面的详细信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns>查询结果</returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Query(Paging paging, VM_SupplierInstructionList4Search searchCondition)
        {
            // 查询外协指示List信息
            var suppInstructionList = instructionsService.GetSupplierInstructionList(searchCondition, paging);
            
            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return suppInstructionList.ToJson(paging.total);
        }
    }
}
