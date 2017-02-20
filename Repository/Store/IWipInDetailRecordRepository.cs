/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWipInDetailRecordRepository.cs
// 文件功能描述：
//            在制品库入库履历详细Repository接口
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
    public interface IWipInDetailRecordRepository : IRepository<WipInDetailRecord>
    {
        //获取在制品库入库履历详细对象
        WipInDetailRecord SelectWipInDetailRecord(WipInDetailRecord wipInDetailRecord);
        //在制品库入库履历删除功能
        bool WipInDetailRecordForDel(WipInDetailRecord wipInDetailRecord);
        //获得在制品库入库履历详细List数据
        IEnumerable<WipInDetailRecord> GetWipInDetailRecordForList(WipInDetailRecord WipInDetailRecord);
        //在制品库入库履历修改功能
        bool WipInRecordForUpdate(WipInDetailRecord wipInDetailRecord);

    }
}
