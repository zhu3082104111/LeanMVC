/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompanyInfoController.cs
// 文件功能描述：
//          供货商信息相关操作的Controller
//      
// 创建标识：2013/12/9 宋彬磊 新建
/*****************************************************************************/
using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Technology.Controllers
{
    /// <summary>
    /// 供货商信息相关操作的Controller类
    /// </summary>
    public class CompanyInfoController : BaseController
    {
        // 供货商信息的service
        private ICompanyInfoService compInfoService;

        /// <summary>
        /// 实例化函数
        /// </summary>
        /// <param name="compInfoService"></param>
        public CompanyInfoController(ICompanyInfoService compInfoService)
        {
            this.compInfoService = compInfoService;
        }

        public JsonResult Get(string id)
        {
            return null;
        }

        /// <summary>
        /// 显示供货商信息（子查询画面）
        /// </summary>
        /// <param name="compType"></param>
        /// <param name="compID"></param>
        /// <param name="compName"></param>
        /// <param name="pdtID"></param>
        /// <param name="retId"></param>
        /// <param name="retName"></param>
        /// <returns></returns>
        public ActionResult ShowCompInfo4Sel(string compType, string compID, string compName, string pdtID, string retId, string retName)
        {
            ViewBag.compType = compType;
            ViewBag.compID = compID;
            ViewBag.compName = compName;
            ViewBag.pdtID = pdtID;
            ViewBag.retId = retId;
            ViewBag.retName = retName;

            return View();
        }

        /// <summary>
        /// 获取供货商信息（子查询画面）
        /// </summary>
        /// <param name="compType">供货商种类（1：外购，  2：外协）</param>
        /// <param name="compID">供货商ID</param>
        /// <param name="compName">供货商名称</param>
        /// <param name="pdtID">供货商可提供的产品零件ID</param>
        /// <returns>供货商信息</returns>
        [HttpPost]
        public JsonResult GetCompanyInfo4Sel(VM_CompSearchCondition search, Paging paging)
        {
            var compInfoList = compInfoService.GetCompanyInfo4Sel(search, paging);
            return compInfoList.ToJson(paging.total);
        }
    }
}
