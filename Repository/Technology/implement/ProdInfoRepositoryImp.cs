using Extensions;
using Model;
using Model.Technology;
using Repository.database;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    class ProdInfoRepositoryImp : AbstractRepository<DB, ProdInfo>, IProdInfoRepository
    {
        public string GetProduceId(string prodAbbrev)
        {
            try
            {
                ProdInfo entity = base.GetList(a => a.ProdAbbrev == prodAbbrev).First();
                return entity.ProductId;
            }
            catch (System.NullReferenceException)
            {
                return "###";
            }
        }

        public string GetProdAbbrev(string produceId)
        {
            try
            {
                ProdInfo entity = base.GetEntityById(produceId);
                return entity.ProdAbbrev;
            }
            catch (System.NullReferenceException)
            {
                return "###";
            }
        }

        public ProdInfo GetProdInfoById(string productID)
        {
            return base.GetEntityById(productID);
        }


        /// <summary>
        /// 根据部门ID、产品类别ID，从结果集中查询出相应产品类别名称
        /// </summary>
        /// <param name="paraDepartmentID">部门ID</param>
        /// <param name="ProductCategoryID">产品类别ID</param>
        /// <param name="paraMasterDefiInfoIE">MasterDefiInfo 结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        private string GetProductCategoryName(string paraDepartmentID, string paraProductCategoryID, IEnumerable<MasterDefiInfo> paraMasterDefiInfoIE)
        {
            if (paraDepartmentID.Equals("01"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00010") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo!=null){
                    return masterDefiInfo.AttrValue;
                }else
                {
                    return "";
                }
            }
            else if (paraDepartmentID.Equals("02"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00011") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo != null)
                {
                    return masterDefiInfo.AttrValue;
                }
                else
                {
                    return "";
                }
            }
            else if (paraDepartmentID.Equals("03"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00012") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo != null)
                {
                    return masterDefiInfo.AttrValue;
                }
                else
                {
                    return "";
                }
            }
            else if (paraDepartmentID.Equals("04"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00013") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo != null)
                {
                    return masterDefiInfo.AttrValue;
                }
                else
                {
                    return "";
                }
            }
            else if (paraDepartmentID.Equals("05"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00014") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo != null)
                {
                    return masterDefiInfo.AttrValue;
                }
                else
                {
                    return "";
                }
            }
            else if (paraDepartmentID.Equals("06"))
            {
                MasterDefiInfo masterDefiInfo = paraMasterDefiInfoIE.FirstOrDefault(mdiIE => mdiIE.SectionCd.Equals("00015") && mdiIE.AttrCd.Equals(paraProductCategoryID));
                //朱静波-暂时修改
                if (masterDefiInfo != null)
                {
                    return masterDefiInfo.AttrValue;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return null;
            }
        } //end GetProductCategoryName

        /// <summary>
        /// 获取表 PD_PROD_INFO 查询记录
        /// </summary>
        /// <param name="paraPIFSTPI">VM_ProdInfoForSearchTableProdInfo 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_ProdInfoForTableProdInfo 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_ProdInfoForTableProdInfo> GetProdInfoListRepository(VM_ProdInfoForSearchTableProdInfo paraPIFSTPI, Paging paraPage)
        {
            IQueryable<ProdInfo> prodInfoIQ = base.GetList();
            IQueryable<MasterDefiInfo> masterDefiInfoIQ = base.GetList<MasterDefiInfo>().Where(mdi => mdi.SectionCd.Equals("00009"));
            //获取单位表数据

            //查询
            if (string.IsNullOrEmpty(paraPIFSTPI.DepartmentID) == false && paraPIFSTPI.DepartmentID.Equals("00") == false)
            {
                prodInfoIQ = prodInfoIQ.Where(pi => pi.DepartId.Equals(paraPIFSTPI.DepartmentID));
            }
            if (string.IsNullOrEmpty(paraPIFSTPI.ProductAbbreviation) == false)
            {
                prodInfoIQ = prodInfoIQ.Where(pi => pi.ProdAbbrev.Contains(paraPIFSTPI.ProductAbbreviation));
            }
            if (string.IsNullOrEmpty(paraPIFSTPI.ProductCategoryID) == false)
            {
                prodInfoIQ = prodInfoIQ.Where(pi => pi.ProdCatg.Equals(paraPIFSTPI.ProductCategoryID));
            }
            if (string.IsNullOrEmpty(paraPIFSTPI.ProductName) == false)
            {
                prodInfoIQ = prodInfoIQ.Where(pi => pi.ProdName.Contains(paraPIFSTPI.ProductName));
            }

            //多表连接
            IQueryable<VM_ProdInfoForTableProdInfo> resultIQ = from piIQ in prodInfoIQ
                                                               join mdiIQ in masterDefiInfoIQ on piIQ.DepartId equals mdiIQ.AttrCd
                                                               select new VM_ProdInfoForTableProdInfo
                                                               {
                                                                   
                                                                   DepartmentID = piIQ.DepartId,
                                                                   DepartmentName = mdiIQ.AttrValue,
                                                                   OldModelID = piIQ.OldModelId,
                                                                   ProductAbbreviation = piIQ.ProdAbbrev,
                                                                   ProductCategoryID = piIQ.ProdCatg,
                                                                   ProductCategoryName = null,
                                                                   ProductID = piIQ.ProductId,
                                                                   ProductName = piIQ.ProdName,
                                                                   Specifica = piIQ.Specifica,
                                                                   UnitName = piIQ.UnitId //获取单位名称
                                                               };

            IEnumerable<MasterDefiInfo> masterDefiInfoIE = base.GetList<MasterDefiInfo>().ToList().AsEnumerable(); //获取表 BI_MASTER_DEFI_INFO 所有数据至内存，可以加快搜索速度
            IEnumerable<VM_ProdInfoForTableProdInfo> resultIE = resultIQ.ToList().AsEnumerable(); //执行操作，获取查询数据至内存，IQueryable型不能设置属性 ProductCategoryName 值

            //遍历查询结果集
            foreach (var piftpi in resultIE)
            {
                piftpi.ProductCategoryName = GetProductCategoryName(piftpi.DepartmentID, piftpi.ProductCategoryID, masterDefiInfoIE); //设置属性 ProductCategoryName 值
            }

            masterDefiInfoIE = null; //释放内存？
            paraPage.total = resultIE.AsQueryable().Count();
            IEnumerable<VM_ProdInfoForTableProdInfo> prodInfoForTableProdInfoIE = resultIE.AsQueryable().ToPageList<VM_ProdInfoForTableProdInfo>("ProductID asc", paraPage);

            
            return prodInfoForTableProdInfoIE; //返回结果集
        } //end GetProdInfoListRepository


    } //end ProdInfoRepositoryImp
}
