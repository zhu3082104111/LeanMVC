// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IGiMaterialRepository.cs
// 文件功能描述：让步仓库表repository接口
// 
// 创建标识：
//
// 修改标识：代东泽 20131226
// 修改描述：添加注释
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    /// <summary>
    /// 让步仓库表资源库
    /// M:梁龙飞
    /// </summary>
    public interface IGiMaterialRepository : IRepository<GiMaterial>
    {
        GiMaterial SelectGiMaterial(GiMaterial giMaterial);
        //让步仓库删除操作（yc添加）
        bool UpdateGiMaterialForDel(GiMaterial giMaterial);
        //附件库入库履历修改
        bool UpdateGiMaterialForStore(GiMaterial giMaterial);

        /// <summary>
        /// 在制品库出库登录修改让步仓库表前查询
        /// </summary>
        /// <param name="giMaterial"></param>
        /// <returns></returns>
        GiMaterial WipSelectGiMaterial(GiMaterial giMaterial);

        /// <summary>
        /// 在制品出库登录修改让步仓库表
        /// </summary>
        /// <param name="giMaterial"></param>
        /// <returns></returns>
        bool UpdateGiMaterialForOut(GiMaterial giMaterial);
        
    }
}
