// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AssistInstructionImp.cs
// 文件功能描述：
//          外协指示表的Repository接口类的实现
//      
// 修改履历：2014/1/8 廖齐玉 新建
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
    /// <summary>
    /// 外协指示表的Repository接口类的实现
    /// </summary>
    public class AssistInstructionRepositoryImp : AbstractRepository<DB, AssistInstruction>, IAssistInstructionRepository
    {
    }
}
