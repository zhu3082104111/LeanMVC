/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IStoreExternalService.cs
// 文件功能描述：
//          仓库部门的外部共通的Service接口类
//      
// 修改履历：2013/12/17 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using System.Collections;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 仓库部门的外部共通的Service接口类
    /// </summary>
    public interface IStoreExternalService
    {
        //根据“产品或零件ID”返回相应的“单位ID”和“单位名称”（模块间接口，序号20）
        VM_MaterUnitInfo GetMaterUnitId(string PdtID);

        //根据“产品零件ID”和“供应商ID”取得“单价”及“估价”信息 （模块间接口，序号21）
        VM_MaterPriceInfo GetMaterPrice(string PdtID, string CompID);

        /// <summary>
        /// 批次别库存表的添加方法 陈健
        /// </summary>
        /// <param name="userId">登录人员ID</param>
        /// <param name="bthStock">批次别信息</param>
        /// <returns></returns>
        bool WhInInsertBthStockList(string userId, List<BthStockList> bthStock);

    }
}
