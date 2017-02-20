/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWipOutDetailRecordRepository.cs
// 文件功能描述：
//            在制品库出库履历详细Repository接口
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Collections;
namespace Repository
{
    public interface IWipOutDetailRecordRepository : IRepository<WipOutDetailRecord>
    {
        /// <summary>
        /// 在制品库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="wipOutDetailRecord">在制品出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        WipOutDetailRecord SelectWipOutDetailRecord(WipOutDetailRecord wipOutDetailRecord, string bthID);

        //在制品库出库履历详细对象List（yc添加）
        IEnumerable<WipOutDetailRecord> GetWipOutDetailRecordForList(WipOutDetailRecord wipOutDetailRecord);

        //在制品库出库履历删除（yc添加）
        bool WipOutDetailRecordForDel(WipOutDetailRecord WipOutDetailRecord);
    }
}
