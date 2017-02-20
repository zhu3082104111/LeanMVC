/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWipInRecordRepository.cs
// 文件功能描述：
//            在制品库入库履历及入库相关业务Repository接口
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
using Extensions;
using System.Collections;
namespace Repository
{
    public interface IWipInRecordRepository : IRepository<WipInRecord>
    {
        //在制品库带入库一览画面初始化
        IEnumerable GetWipInStoreBySearchByPage(VM_WipInStoreForSearch wipInStoreForSearch, Paging paging);

        //在制品库入库登录画面数据表示
        IEnumerable GetWipInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging);

        //在制品库入库履历一览初始化页面
        IEnumerable GetWipInRecordBySearchByPage(VM_WipInRecordStoreForSearch wipInRecordStoreForSearch, Paging paging);

        //获取履历数据对象
        WipInRecord SelectWipInRecord(WipInRecord wipInRecord);

        //履历数据删除方法
        bool WipInRecordForDel(WipInRecord wipInRecord);

        //入库单打印选择画面显示
        IEnumerable GetWipInPrintBySearchByPage(VM_WipInPrintForSearch wipInPrintForSearch, Paging paging);

        //加工产品出库单显示
        IEnumerable SelectForWipInPrintPreview(string pdtID, string deliveryOrderID, Paging paging);



    }
}
