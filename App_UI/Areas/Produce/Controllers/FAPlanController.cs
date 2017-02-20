/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：FAPlanController.cs
// 文件功能描述：总装管理控制器
// 
// 创建标识：
//
// 修改标识：20131217 杜兴军
// 修改描述：接手此功能
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class FAPlanController : BaseController
    {
        #region Service接口

        private IAssemblePlanService assemblePlanService;//总装计划Service接口

        #endregion

        public FAPlanController(IAssemblePlanService assemblePlanService)
        {
            this.assemblePlanService = assemblePlanService;
        }

        //
        // GET: /Produce/FAPlan/

        #region 视图
        /// <summary>
        /// 总装计划一览
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 总装排产(中计划)
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneralSchedu()
        {
            return View();
        }

        #endregion


        #region 查询/列表

        /// <summary>
        /// 获取总装计划一览数据
        /// </summary>
        /// <param name="search">查询参数</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssemblePlan(VM_AssemblePlanSearch search, Paging paging)
        {
            return assemblePlanService.GetAssemblePlan(search, paging).ToJson(paging.total);
        }

        /// <summary>
        /// 获取总装中计划数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssembleMiddlePlan(VM_AssembleMiddlePlanSearch search, Paging paging)
        {
            return assemblePlanService.GetAssembleMiddlePlan(search, paging).ToJson(paging.total);
        }

        /// <summary>
        /// 获取指定的原料数量
        /// </summary>
        /// <param name="search">匹配条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMatetialQuanlity(VM_MaterialQuanlitySearch search)
        {
            IEnumerable<VM_MaterialQuanlityShow> materialQuanlityList = assemblePlanService.GetMaterialQuanlity(search);
            List<Object> footer=new List<object>();
            footer.Add(new {ExportAbbrev = "最大转配数量", RealInQuanlity = materialQuanlityList.Select(m=>m.MaxQuanlity).DefaultIfEmpty(0).Min()});
            var data =
                new
                {
                    rows = materialQuanlityList.DefaultIfEmpty(new VM_MaterialQuanlityShow(){ExportAbbrev = "无对应零件",RealInQuanlity = 0}),
                    footer =footer
                };
            return data.ToJson();
        }
        
        #endregion


        #region 数据操作


        #endregion

    }
}
