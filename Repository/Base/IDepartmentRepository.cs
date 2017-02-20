using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
       IEnumerable<Department> getAllDepartment();

       IQueryable<Department> getAllDepartmentAsQueryable();

       IQueryable<Department> getAllDepartmentAsQueryableOrderBy();

       IQueryable<Department> getAllDepartmentAsQueryableOrderById(string uid);

       Department getDepartmentById(string Deptid);


       bool addDepartment(Department d);

       bool updateDepartment(Department d);

       bool deleteDepartment(Department d);

       bool deleteDepartmentBySQL(Department d);
       IEnumerable<Department> selectDepartmentBySQL(string d);
    }
}
