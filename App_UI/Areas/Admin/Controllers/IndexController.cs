using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Web.Security;
using App_UI.Areas.Controllers;


namespace App_UI.Areas.Admin.Controllers
{
    public class IndexController : BaseController
    {
        private IRoleInfoService roleInfoService;
        public IndexController(IRoleInfoService roleInfoService)
        {

            this.roleInfoService = roleInfoService;

        }


        //
        // GET: /Admin/Index/
        [HandleErrorAttribute]
        public ActionResult Index()
        {
            if(Session["UserID"]!=null)
            {
                string UserID = Session["UserID"].ToString();
                ViewBag.UserName = Session["UserID"].ToString();
                
            }
           
            return View();
        }

        public ActionResult Welcome() 
        {

            return View();
        }

        public JsonResult GetMenuInfo()
        {
            string UserID = Session["UserID"].ToString();
            ViewBag.UserName = Session["UserID"].ToString();
            //var model = roleInfoService.getAllMenuInfo();
            var model = roleInfoService.findAllMenuInfoOrderBy()
                                   .Where(
                                       a =>
                                       a.Display).ToList();
            //a.ChainInfo.Any(
            //    b =>
            //    b.RoleChainInfo.Any(
            //        c =>
            //        c.RoleInfo.URoleInfo.Any(
            //            d => d.UId.Equals(UserID))))).ToList();

            //===========
            string arr = "[";
            bool flag = true;
            foreach (var item in model.Where(a => a.SystemId.Length == 3))
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    arr += ",";
                }

                arr += "{'menuid':'" + item.MId + "','icon':'icon-sys','menuname':'" + item.MName + "'";
                bool k = true;
                foreach (var item1 in model.Where(a => a.SystemId.Length == 6 && a.SystemId.StartsWith(item.SystemId)))
                {
                    if (k)
                    {
                        arr += ",'menus':[";
                        k = false;
                    }
                    else
                    {
                        arr += ",";
                    }
                    // arr += "{'menuid':'" + item1.MId + "','icon':'icon-sys','menuname':'" + item1.MName + "','url':'"+item1.AreaName+"/" + item1.ControllerName + "/" + item1.ActionName + "'}";
                    //arr += "{'menuid':'" + item1.MId + "','icon':'icon-sys','menuname':'" + item1.MName +"'";
                    if (item1.ControllerName.Equals(""))
                    {
                        arr += "{'menuid':'" + item1.MId + "','icon':'icon-sys','menuname':'" + item1.MName + "'";
                    }
                    else
                    {
                        arr += "{'menuid':'" + item1.MId + "','icon':'icon-sys','menuname':'" + item1.MName + "','url':'" + item1.AreaName + "/" + item1.ControllerName + "/" + item1.ActionName + "/" + item1 .Parameter+ "'";
                    }

                    bool k2 = true;
                    foreach (var item2 in model.Where(a => a.SystemId.Length == 9 && a.SystemId.StartsWith(item1.SystemId)))
                    {

                        if (k2)
                        {
                            arr += ",'menus':[";
                            k2 = false;
                        }
                        else
                        {
                            arr += ",";
                        }
                        arr += "{'menuid':'" + item2.MId + "','icon':'icon-sys','menuname':'" + item2.MName + "','url':'" + item2.AreaName + "/" + item2.ControllerName + "/" + item2.ActionName + "/" + item1.Parameter + "'}";
                    }
                    if (k2)
                    {
                        arr += "}";
                    }
                    else
                    {
                        arr += "]}";
                    }

                }
                if (k)
                {
                    arr += "}";
                }
                else
                {
                    arr += "]}";
                }
                // arr += "]}";
            }
            arr += "]";
            JsonResult json = new JsonResult();
            json.Data = arr;
            return json;
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Admin/Login");
        }


    }
}
