// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IPickingListRepository.cs
// 文件功能描述：生产领料单信息repository接口
// 
// 创建标识：代东泽 20131127
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using Model;
using Model.Produce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 
    /// 代东泽 20131127
    /// </summary>
    public interface IPickingListRepository : IRepository<ProduceMaterRequest>
    {
        /// <summary>
        /// 代东泽 20131202为service取得满足条件的 VM_ProduceMaterRequestForTableShow 列表
        /// </summary>
        /// <param name="pickingList">查询条件对象</param>
        /// <param name="paging">分页对象</param>
        /// <returns></returns>
        IEnumerable<VM_ProduceMaterRequestForTableShow> GetPickingListsBySearch(VM_ProduceMaterRequestForSearch pickingList, Extensions.Paging paging);

        
        /// <summary>
        /// //获得生产领料详细对象（yc添加）
        /// 杨灿 20131216
        /// </summary>
        /// <param name="produceMaterDetail"></param>
        /// <returns></returns>
        ProduceMaterDetail SelectProduceMaterDetail(ProduceMaterDetail produceMaterDetail);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProduceMaterDetail> GetDetailListByCondition(Expression<Func<ProduceMaterDetail, bool>> condition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        void AddDetail(ProduceMaterDetail detail);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        VM_ProduceMaterRequestForTableShow GetProduceMaterRequestForDetail(ProduceMaterRequest p);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        IEnumerable<VM_ProduceMaterDetailForDetailShow> GetProduceMaterDetailsForDetail(ProduceMaterRequest p);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void UpdateDetail(ProduceMaterDetail obj);
    }
}
