using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Store.Controllers
{
    public class AccINVController : Controller
    {
        private IWipStoreService wipStoreService;
        public AccINVController(IWipStoreService wipStoreService)
        {
            this.wipStoreService=wipStoreService;
        }
        //
        // GET: /Store/AccINV/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(Paging paging, WipStore wipStore)
        {
            int total;
            var users = wipStoreService.GetWipStoreBySearchByPage(wipStore, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Edit(string id)
        {
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //return Content("请选择您要删除的数据");
                }
                var deleteID = id.Split(',');
                //定义数组存放需要删除的ID
                List<string> list = new List<string>();
                foreach (var Dsid in deleteID)
                {
                    list.Add(Dsid);
                }
                //然后执行删除的方法删除数据
                wipStoreService.UpdateWipStore(list);
            }
            catch (Exception e)
            {
                result = false;
            }

            return result.ToJson();
        }

        public ActionResult Create(WipStore WipStore)
        {
            if (WipStore.Id != null)
            {
                bool result = true;
                try
                {
                    wipStoreService.AddWipStore(WipStore);
                }
                catch (Exception e)
                {
                    result = false;
                }
                return result.ToJson();
            }
            return View();
        }
        public ActionResult Save(WipStore WipStore)
        {
          
            bool result = true;
            try
            {
                string a = WipStore.DeliverId;
            }
            catch (Exception e)
            {
                result = false;
            }
            return result.ToJson();
        }

    }
}
