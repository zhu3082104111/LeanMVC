/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccInDetailRecordRepository.cs
// 文件功能描述：
//            附件库入库履历详细Repository接口
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
    public interface IAccInDetailRecordRepository : IRepository<AccInDetailRecord>
    {
        //获得附件库入库履历对象
        AccInDetailRecord SelectAccInDetailRecord(AccInDetailRecord accInDetailRecord);
        //附件库入库履历对象List
        IEnumerable<AccInDetailRecord> GetAccInDetailRecordForList(AccInDetailRecord accInDetailRecord);
        //附件库入库履历删除
        bool AccInDetailRecordForDel(AccInDetailRecord accInDetailRecord);
        //附件库入库履历修改
        bool AccInRecordForUpdate(AccInDetailRecord accInDetailRecord);

    }
}
