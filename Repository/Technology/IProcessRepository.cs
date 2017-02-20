/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProcessRepository.cs
// 文件功能描述：工序表的Repository接口
//     
// 修改履历：2013/12/09 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface IProcessRepository : IRepository<Process>
    {
        
    }
}
