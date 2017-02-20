/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IStoreLockService.cs
// 文件功能描述：产品零件预约锁存服务层接口
// 
// 创建标识：20131215 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;

namespace BLL
{
    /// <summary>
    /// 产品零件预约锁存服务层接口
    /// C:梁龙飞
    /// </summary>
    public interface IStoreLockService
    {
      
        #region 版本：201312 旧版本：废弃 C:梁龙飞
        /// <summary>
        ///// 正常品锁存 新增，检测规格型号来分辨是否是有规格型号的锁存
        ///// </summary>
        ///// <param name="target"></param>
        ///// <returns></returns>
        //[TransactionAop]
        //bool LockNormalBatch(VM_LockReserveShow target);
        
        ///// <summary>
        ///// 正常品锁存 更新，检测规格型号来分辨是否是有规格型号的锁存
        ///// 当锁存数量改为0时，会删除预约信息
        ///// </summary>
        ///// <param name="target"></param>
        ///// <returns></returns>
        //[TransactionAop]
        //bool UpdateNormalBatch(VM_LockReserveShow target);

      
        #endregion


        #region 优化版本1：C：梁龙飞  
    
        /// <summary>
        /// 获取产品排产中 零件或产品 的正常品锁库信息+可锁信息
        /// （只有当 目标有规格要求的时候才有意义）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetNormalMixBatch(VM_MatBtchStockSearch condition);

        /// <summary>
        /// 获取产品排产中 零件或产品 的让步品锁库信息+可锁信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<VM_LockReserveShow> GetAbnormalMixBatch(VM_MatBtchStockSearch condition);

        /// <summary>
        /// 正常无规格品预约
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        bool NormalReserveWithoutSpec(VM_LockReserveShow target);

        /// <summary>
        /// 正常有规格品预约：Add
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        bool AddNormalReserveWithSpec(VM_LockReserveShow target);

        /// <summary>
        /// 正常有规格品预约：{Update,Delete}
        /// 只更新一条预约批次
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateNormalReserveWithSpec(VM_LockReserveShow target);

        /// <summary>
        /// 让步品锁存：Add
        /// C:梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        bool AddAbnormalReserve(VM_LockReserveShow target);

        /// <summary>
        /// 让步品锁存：{ Update,Delete }
        /// C：梁龙飞
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateAbnormalReserve(VM_LockReserveShow target);



        #endregion
    }
}
