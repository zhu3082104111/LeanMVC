using App_UI.Areas.Controllers;
/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProduceBill.cs
// 文件功能描述：masterInfo控制器
// 
// 创建标识：代东泽 20131122
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using System.Collections;
using System.Text;
using Model;
using Model.Base;
namespace App_UI.Areas.Common.Controllers
{
    public class MasterInfoController : BaseController
    {
        private IMasterInfoService masterInfoService;
        private IBaseInfoService baseInfoService;

        public MasterInfoController(IMasterInfoService masterInfo, IBaseInfoService baseInfoService) 
        {
            this.masterInfoService = masterInfo;
            this.baseInfoService = baseInfoService;
        }

      
        public JsonResult Get(string id) 
        {
            JsonResult js=new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data=masterInfoService.GetOneSection(id);
           
            return js; 
        }

        public JsonResult GetForSearch(string id)
        {
            IList<VM_MasterInfoForSelect> ls = new List<VM_MasterInfoForSelect>() { new VM_MasterInfoForSelect { AttrCd = "", AttrValue = "全部" ,selected=true} };
            IEnumerable<MasterDefiInfo> list = masterInfoService.GetOneSection(id);
            var res = from a in list select new VM_MasterInfoForSelect { AttrCd = a.AttrCd, AttrValue = a.AttrValue };
            foreach (var s in res) 
            {
               ls.Add(s);               
            }
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = ls;
            return js;
        }

        public JsonResult GetSelectData(string id)
        {
           
            IEnumerable<MasterDefiInfo> list = masterInfoService.GetOneSection(id);
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = list;
            return js;
        }

        /// <summary>
        /// 获取所有部门（员工）
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDepartMentData()
        {
            //新建一个id和name相关的下拉列表
            IList<VM_MasterInfoForSelect> ls = new List<VM_MasterInfoForSelect>() { new VM_MasterInfoForSelect { AttrCd = "", AttrValue = "全部", selected = true } };
            //取出部门信息
            IEnumerable<Department> deptlist = baseInfoService.GetDepartment();
            //选取需要字段（ID号和部门Name）
            var res = from a in deptlist select new VM_MasterInfoForSelect { AttrCd = a.DeptId , AttrValue = a.DeptName  };
            foreach (var s in res)
            {
                //形成下拉列表值
                ls.Add(s);
            }
            //返回值初始化
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;            
            //生成结果赋给返回值
            js.Data = ls;
            return js;
        }
    }
}
