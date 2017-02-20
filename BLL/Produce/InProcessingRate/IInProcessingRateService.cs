/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IInProcessingRateService.cs
// 文件功能描述：内部进度查询画面的Service接口
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace BLL
{
    public interface IInProcessingRateService
    {
        /// <summary>
        /// 按条件查询内部加工进度统计
        /// </summary>
        /// <param name="inProcessingRate">输入的查询信息</param>
        /// <param name="paging">分页</param>
        /// <param name="total">满足条件的总数</param>
        /// <returns></returns>
        IEnumerable GetInProcessingRateSearch(VM_InProcessingRateSearch inProcessingRate, Paging paging);
    }
}
