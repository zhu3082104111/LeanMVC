/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMakeAutoNoRepository.cs
// 文件功能描述：
//          编码生成表的Repository接口
//      
// 修改履历：2013/12/5 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Base
{
    /// <summary>
    /// 编码生成表的Repository接口
    /// </summary>
    public interface ICodeMakeRepository : IRepository<CodeMake>
    {
        /// <summary>
        /// 根据主Key，获取唯一的编码生成表的数据
        /// </summary>
        /// <param name="codeType">编码区分</param>
        /// <param name="deptId">部门区分</param>
        /// <param name="yyymmmdd">日期</param>
        /// <returns></returns>
        CodeMake getCodeMakeByKey(string codeType, string deptId, string yyymmmdd);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool addCodeMake(CodeMake c);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool updateCodeMake(CodeMake c);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool deleteCodeMake(CodeMake c);
    }
}
