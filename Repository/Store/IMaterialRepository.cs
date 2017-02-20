using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    /// <summary>
    /// 仓库资源库
    /// M：梁龙飞
    /// </summary>
    public interface IMaterialRepository : IRepository<Material>
    {
        //入库登录时修改被预约数量实际在库数量及总价
        bool updateMaterialForStoreLogin(Material material);

        Material selectMaterial(Material material);

        /// <summary>
        /// 成品库入库履历详细修改仓库表
        /// </summary>
        /// <param name="material">更新数据集合</param>
        /// <returns>true</returns>
        bool updateInMaterialFinIn(Material material);

        /// <summary>
        /// 成品库出库履历详细修改仓库表
        /// </summary>
        /// <param name="material">更新数据集合</param>
        /// <returns>true</returns>
        bool updateInMaterialFinOut(Material material);

        /// <summary>
        /// 获取产品在仓库中的可锁库存量
        /// 产品不存在时返回为0
        /// C:梁龙飞
        /// </summary>
        /// <param name="whID">仓库ID</param>
        /// <param name="matID">物料ID</param>
        /// <returns></returns>
        decimal GetUnlockedNum( string whID, string matID);

        /// <summary>
        /// 在制品库出库登录修改仓库表
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        bool updateMaterialForOutLogin(Material material);

    }
}
