/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMakeAutoNoRepository.cs
// 文件功能描述：
//          编码生成表的Repository接口的实现类
//      
// 修改履历：2013/12/5 宋彬磊 新建
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
    public class CodeMakeRepositoryImp : AbstractRepository<DB, CodeMake>, ICodeMakeRepository
    {
        /// <summary>
        /// 根据主Key，获取唯一的编码生成表的数据
        /// </summary>
        /// <param name="codeType">编码区分</param>
        /// <param name="deptId">部门区分</param>
        /// <param name="yyymmmdd">日期</param>
        /// <returns></returns>
        public CodeMake getCodeMakeByKey(string codeType, string deptId, string yyymmmdd)
        {
            return base.Get(a => a.CdCatg == codeType && a.DepartId == deptId && a.Date == yyymmmdd);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool addCodeMake(CodeMake c)
        {
            return base.Add(c);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool updateCodeMake(CodeMake c)
        {
            return base.Update(c);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool deleteCodeMake(CodeMake c)
        {
            return base.Delete(c);
        }
    }
}
