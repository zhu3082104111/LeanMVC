using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    /// <summary>
    /// 物料分解信息资源库
    /// 20131119 梁龙飞C
    /// </summary>
    public interface IMaterialDecomposeRepository:IRepository<MaterialDecompose>
    {

        #region ProductSheduling C:梁龙飞

        /// <summary>
        /// 从物料分解信息表中取出一个产品的排产计划
        /// 前提产品计划正在排产中
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        IEnumerable<VM_ProductSchedulingShow> GetProductSchedulingList(string orderID, string orderDetail, string version);

        /// <summary>
        /// 订单详细的资料在物料分解表中是否存在
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        bool IsSchedulingInfoExist(string orderID, string orderDetail, string version);

        /// <summary>
        /// 获取有效的目标集
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderDetail"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        IEnumerable<MaterialDecompose> GetAvailableList(string orderID, string orderDetail, string version);

        #endregion


        #region 替代方案：目前不使用：梁龙飞
        /// <summary>
        /// 递归更新物料的需求量
        /// 替代方案：未实现
        /// </summary>
        /// <param name="target"></param>
        /// <param name="offsetDmdQtt">校正需要量</param>
        /// <returns></returns>
        string RecursionUpdate(MaterialDecompose target, decimal offsetDmdQtt);

        /// <summary>
        /// 从目标结点开始，更新到树叶：需求量
        /// 未实现
        /// </summary>
        /// <param name="target"></param>
        /// <param name="offsetDmdQtt"></param>
        /// <returns></returns>
        List<int> RelationUpdate(MaterialDecompose target, decimal offsetDmdQtt);
        #endregion
    }
}
