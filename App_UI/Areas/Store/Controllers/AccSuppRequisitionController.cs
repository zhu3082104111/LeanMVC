/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccSuppRequisitionController.cs
// 文件功能描述：
//          附件库外协领料单画面的Controller
//      
// 创建标识：2013/12/31 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;
using BLL.Common;

namespace App_UI.Areas.Store.Controllers
{
    public class AccSuppRequisitionController : Controller
    {

        // 附件库领料外协单Service接口
        private IAccSuppRequisitionService AccSupRequisionService;

        // 自动生成单据号的Service
        private IAutoCreateOddNoService AutoCreateOddNoService;

        /// <summary>
        /// 启动附件库外协领料单画面函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Init()
        {
            return View();
        }

        /// <summary>
        /// 附件库领料外协单构造器
        /// </summary>
        /// <param name="AccSupRequisionService">附件库领料外协单Service接口对象</param>
        public AccSuppRequisitionController(IAccSuppRequisitionService AccSupRequisionService, IAutoCreateOddNoService AutoCreateOddNoService) 
        {
            this.AccSupRequisionService = AccSupRequisionService;
            this.AutoCreateOddNoService = AutoCreateOddNoService;
        }

        /// <summary>
        /// 显示附件库外协领料单画面
        /// </summary>
        /// <param name="searchCondition">查询条件的视图类</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public JsonResult Query(String SupOrderID,Paging paging) 
        {
            int total;
            
            // 用传入的外协单号匹配领料单信息表中的外协单号
            // 生成外协领料单对象
            List<MCSupplierCnsmInfo> SupplierCnsmInfo = new List<MCSupplierCnsmInfo>();
            // 用外协单号查询外协领料单信息表里的信息（方法未写）
            
            
                // 如果领料单中没有任何记录则插入新的领料单
                if (SupplierCnsmInfo.Count() == 0)
                {
                    // 插入新领料单信息并查询
                    // 通过查询的外协单号生成领料单号
                    var requiNum = AutoCreateOddNoService.GetPickingMateriaRequestId(SupOrderID);
                    // 调用Service里的插入方法
                    var AccSupReInsertInfo = AccSupRequisionService.InsertInSuppCnsmInfo(requiNum, SupOrderID, paging);
                    return AccSupReInsertInfo.ToJson(paging.total);
                }
                // 如果领料单中有记录
                else 
                {
                    for (int i = 0; i < SupplierCnsmInfo.Count(); i++)
                    {
                        //如果领料单中有相匹配的信息，且该领料单为有效单，则更新字段，并查询
                        if (SupplierCnsmInfo.ElementAt(i).SupOrderID.Equals(SupOrderID) && SupplierCnsmInfo.ElementAt(i).EffeFlag.Equals("0"))
                        {
                            //更新领料单信息并查询
                            //通过匹配到的外协单号调用Service里的更新方法
                            var AccSupReUpdateInfo = AccSupRequisionService.UpdateInSuppCnsmInfo(SupOrderID, paging);
                            return AccSupReUpdateInfo.ToJson(paging.total);
                        }
                        //如果为无效单，则重新生成一张新的领料单并查询
                        else
                        {
                            // 插入新领料单信息并查询
                            // 通过查询的外协单号生成领料单号
                            var requiNum = AutoCreateOddNoService.GetPickingMateriaRequestId(SupOrderID);
                            // 调用Service里的插入方法
                            var AccSupReInsertInfo = AccSupRequisionService.InsertInSuppCnsmInfo(requiNum, SupOrderID, paging);
                            return AccSupReInsertInfo.ToJson(paging.total);
                        }
                    }
                }
            
           
           
            
            

            //得到将要在页面上显示的数据
            var AccSupplierRequisitionInfo = AccSupRequisionService.GetAccSupRequisitionBySearchByPage(SupOrderID, paging);
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return AccSupplierRequisitionInfo.ToJson(paging.total);
        }

    }
}
