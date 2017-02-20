/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ICustomTranslateInfoRepository.cs
// 文件功能描述：客户订单流转卡关系表的Repository接口
//     
// 修改履历：2013/12/07 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface ICustomTranslateInfoRepository : IRepository<CustomTranslateInfo>
    {
        /// <summary>
        /// 根据条件检索相应的客户订单流转卡关系记录
        /// </summary>
        /// <param name="procDelivId">加工流转卡号</param>
        /// <returns>客户订单流转卡关系表实体</returns>
        CustomTranslateInfo GetCustomTranslateInfo(String procDelivId);
    }
}
