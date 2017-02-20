/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：UnitInfoRepositoryImp.cs
// 文件功能描述：
//          编码生成表的Repository接口的实现类
//      
// 修改履历：2013/12/17 杨灿 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base.implement
{
    /// <summary>
    /// 编码生成表的Repository接口的实现类
    /// </summary>
    public class UnitInfoRepositoryImp : AbstractRepository<DB, UnitInfo>, IUnitInfoRepository
    {

    }
}
