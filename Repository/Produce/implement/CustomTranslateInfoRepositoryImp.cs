/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ICustomTranslateInfoRepository.cs
// 文件功能描述：客户订单流转卡关系表的Repository接口的实现
//     
// 修改履历：2013/12/07 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;

namespace Repository
{
    class CustomTranslateInfoRepositoryImp : AbstractRepository<DB, CustomTranslateInfo>, ICustomTranslateInfoRepository
    {
        public CustomTranslateInfo GetCustomTranslateInfo(string procDelivId)
        {
            CustomTranslateInfo customTranslateInfos =
                base.GetAvailableList<CustomTranslateInfo>().FirstOrDefault(a => a.ProcDelivID.Equals(procDelivId));
            return customTranslateInfos;
        }
    }
}
