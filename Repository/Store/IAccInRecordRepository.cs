/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccInRecordRepository.cs
// 文件功能描述：
//            附件库入库履历及入库相关业务Repository接口
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
using Extensions;
namespace Repository
{
    public interface IAccInRecordRepository : IRepository<AccInRecord>
    {
        //附件库待入库一览画面数据表示
        IEnumerable GetAccInStoreBySearchByPage(VM_AccInStoreForSearch accInStoreForSearch, Extensions.Paging paging);

        //附件库入库登录画面数据表示
        IEnumerable GetAccInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Extensions.Paging paging);

         //附件库待入库履历一览初始化页面
        IEnumerable GetAccInRecordBySearchByPage(VM_AccInRecordStoreForSearch accInRecordStoreForSearch, Extensions.Paging paging);
    
        //测试方法
        IEnumerable GetWipStoreBySearchByPageTest(string mcIsetInListID, string isetRepID, Extensions.Paging paging);

        //获取履历数据对象
        AccInRecord SelectAccInRecord(AccInRecord accInRecord);

        //履历数据删除方法
        bool AccInRecordForDel(AccInRecord accInRecord);

        //入库单打印选择画面显示
        IEnumerable GetAccInPrintBySearchByPage(VM_AccInPrintForSearch accInPrintForSearch, Paging paging);

        //物资验收入库单显示
        IEnumerable SelectForAccInPrintPreview(string pdtID, string deliveryOrderID, Paging paging);

        //修改进货检验单表入库状态（一期测试）
        bool UpdatePurChkListForStoStat(string IsetRepID, string stoStat);

        //修改过程检验单表入库状态（一期测试）
        bool UpdateProcChkListForStoStat(string IsetRepID, string stoStat);

    }
}
