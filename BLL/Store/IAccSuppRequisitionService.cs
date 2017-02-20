/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IAccSuppRequisitionService.cs
// 文件功能描述：
//          附件库外协领料单画面的Service接口类
//      
// 创建标识：2013/12/31 吴飚 新建
/*****************************************************************************/
using Extensions;
using Model;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IAccSuppRequisitionService
    {
        //根据查询条件获得附件库外协领料单画面查询结果
        IEnumerable GetAccSupRequisitionBySearchByPage(String SupOrderID, Paging paging);

        //插入新的数据到领料单信息表
        [TransactionAop]
        IEnumerable InsertInSuppCnsmInfo(String requiNum, String SupOrderID, Paging paging);

        //更新领料单信息表里的信息
        [TransactionAop]
        IEnumerable UpdateInSuppCnsmInfo(String SupOrderID, Paging paging);
    }
}
