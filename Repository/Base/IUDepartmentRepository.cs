using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Repository
{
    public interface IUDepartmentRepository : IRepository<UDepartment>
    {
       IEnumerable<UDepartment> getAllUDepartment();

       IQueryable<UDepartment> getAllUDepartmentAsQueryable();

       IQueryable<UDepartment> getAllUDepartmentAsQueryableOrderBy();

       UDepartment getUDepartmentById(string Deptid);


       bool addUDepartment(UDepartment d);

       bool updateUDepartment(UDepartment d);

       bool deleteUDepartment(UDepartment d);

       bool deleteUDepartmentBySQL(UDepartment d);

       /// <summary>
       /// 添加或更新，当主键为空时添加
       /// </summary>
       /// <param name="ud"></param>
       /// <returns></returns>
  
       bool AddOrUpdate(UDepartment ud);
       IEnumerable<UDepartment> selectUDepartmentBySQL(string ud);
    }
}
