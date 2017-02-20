/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISemInDetailRecordRepository.cs
// 文件功能描述：
//            半成品库入库履历详细Repository接口
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using System.Collections;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;


namespace Repository
{
    public interface ISemInDetailRecordRepository : IRepository<SemInDetailRecord>
    {
        //获取半成品库入库履历详细对象
        SemInDetailRecord SelectSemInDetailRecord(SemInDetailRecord semInDetailRecord);
        
        //半成品库入库履历删除功能
        bool SemInDetailRecordForDel(SemInDetailRecord semInDetailRecord);

        //获得半成品库入库履历详细List数据
        IEnumerable<SemInDetailRecord> GetSemInDetailRecordForList(SemInDetailRecord SemInDetailRecord);


    }
}
