/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdAndPartInfoController.cs
// 文件功能描述：
//          产品零件信息相关操作的Controller
//      
// 创建标识：2013/12/16 宋彬磊 新建
/*****************************************************************************/
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Technology.Controllers
{
    /// <summary>
    /// 产品零件信息相关操作的Controller类
    /// </summary>
    public class ProdAndPartInfoController : BaseController
    {
        // 产品零件Service
        private IProdAndPartInfoService ppService;

        /// <summary>
        /// 实例化函数
        /// </summary>
        /// <param name="ppService"></param>
        public ProdAndPartInfoController(IProdAndPartInfoService ppService)
        {
            this.ppService = ppService;
        }

        public JsonResult Get(string id)
        {
            return null;
        }

        /// <summary>
        /// 显示产品零件信息（子查询画面）
        /// </summary>
        /// <param name="abbrev">物料编号（产品零件略称）</param>
        /// <param name="name">物料名称（产品零件名称）</param>
        /// <returns></returns>
        public ActionResult ShowProdAndPartInfo4Sel(string abbrev, string name)
        {
            ViewBag.abbrev = abbrev;
            ViewBag.name = name;

            return View();
        }

        /// <summary>
        /// 获取产品零件信息（子查询画面）
        /// </summary>
        /// <param name="search"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProdAndPartInfo4Sel(VM_ProdAndPartInfo4Sel search, Paging paging)
        {
            var ppInfoList = ppService.GetProdAndPartInfo4Sel(search, paging);
            return ppInfoList.ToJson(paging.total);
        }
    }
}
