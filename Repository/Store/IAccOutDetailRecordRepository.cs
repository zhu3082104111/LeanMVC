/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccOutDetailRecordRepository.cs
// 文件功能描述：
//            附件库出库履历详细Repository接口
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
namespace Repository
{
    public interface IAccOutDetailRecordRepository : IRepository<AccOutDetailRecord>
    {
        //附件库出库履历对象List
        IEnumerable<AccOutDetailRecord> GetAccOutDetailRecordForList(AccOutDetailRecord accOutDetailRecord);
        
        //附件库出库履历删除
        bool AccOutDetailRecordForDel(AccOutDetailRecord accOutDetailRecord);

        /// <summary>
        /// 附件库出库登录插入出库履历详细表查询是否已存在 陈健
        /// </summary>
        /// <param name="accOutDetailRecord">附件库出库履历数据集合</param>
        /// <param name="bthID">批次号</param>
        /// <returns>数据集合</returns>
        AccOutDetailRecord SelectAccOutDetailRecord(AccOutDetailRecord accOutDetailRecord, string bthID);

    }
}
