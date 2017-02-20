/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccSuppRequisitionServiceImp.cs
// 文件功能描述：
//          附件库外协领料单画面的Service实现类
//      
// 创建标识：2013/12/31 吴飚 新建
/*****************************************************************************/
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Store;
using System.Collections;
using Repository;

namespace BLL
{
    public class AccSuppRequisitionServiceImp : AbstractService, IAccSuppRequisitionService
    {
        //调用外协领料单信息表的Repository
        private IAccSupRequisitionRepository AccSupRequisitionRepository;

        /// <summary>
        /// 附件库外协领料的构造器
        /// </summary>
        /// <param name="AccSupRequisitionRepository">Repository层实现方法的接口对象</param>
        public AccSuppRequisitionServiceImp(IAccSupRequisitionRepository AccSupRequisitionRepository) 
        {
            this.AccSupRequisitionRepository = AccSupRequisitionRepository;
        }
        
        /// <summary>
        /// 通过查询条件获得在附件库外协领料上显示结果
        /// </summary>
        /// <param name="searchCondition">查询内容</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable GetAccSupRequisitionBySearchByPage(String SupOrderID, Paging paging) 
        {
            return AccSupRequisitionRepository.GetAccSupRequisitionBySearchByPage(SupOrderID, paging);
        }
        
        /// <summary>
        /// 插入新的数据到领料单信息表并查询新的
        /// </summary>
        /// <param name="requiNum">新生成的领料单号</param>
        /// <param name="SupOrderID">传入的外协单号</param>
        /// <returns></returns>
        public IEnumerable InsertInSuppCnsmInfo(String requiNum, String SupOrderID,Paging paging) 
        {
            //准备插入的新的数据
            //调用Repository里插入的方法
            var InsertInSuppCnsmList = AccSupRequisitionRepository.InsertInSuppCnsmInfo( requiNum, SupOrderID);
            //再查询出待显示的数据
            return AccSupRequisitionRepository.GetAccSupRequisitionBySearchByPage(SupOrderID,paging);
        }

        /// <summary>
        /// 更新领料单信息表里的数据
        /// </summary>
        /// <param name="SupOrderID">传入的外协单号</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable UpdateInSuppCnsmInfo(String SupOrderID, Paging paging) 
        {
            //调用Repository里的更新方法
            var UpdateInSuppCnsmList = AccSupRequisitionRepository.UpdateInSuppCnsmInfo(SupOrderID,paging);
            //再查询出待显示的数据
            return AccSupRequisitionRepository.GetAccSupRequisitionBySearchByPage(SupOrderID, paging);
        }
    }
}
