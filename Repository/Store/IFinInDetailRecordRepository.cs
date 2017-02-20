/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IFinInDetailRecordRepository.cs
// 文件功能描述：
//          内部成品库入库履历详细的Repository接口类
//      
// 修改履历：2013/11/24 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    /// <summary>
    /// 内部成品库入库履历详细的Repository接口类
    /// </summary>
    public interface IFinInDetailRecordRepository : IRepository<FinInDetailRecord>
    {
        /// <summary>
        /// 更新入库履历详细表
        /// </summary>
        /// <param name="finInDetailRecord">更新数据集合</param>
        /// <returns>true</returns>
        bool UpdateInFinInDetailRecord(FinInDetailRecord finInDetailRecord);

        /// <summary>
        /// 入库履历详细表添加判断
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <param name="inRecordList">入库履历数据集合</param>
        /// <param name="i">行参数</param>
        /// <returns>入库履历详细添加判断数据集合</returns>
        IQueryable<FinInDetailRecord> GetFinInRecordDetailList(string productWarehouseID,Dictionary<string, string>[] inRecordList, int i);

    }
}
