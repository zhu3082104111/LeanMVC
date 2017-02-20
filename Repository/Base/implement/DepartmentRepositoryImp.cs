using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
namespace Repository
{
    public class DepartmentRepositoryImp : AbstractRepository<DB, Department>, IDepartmentRepository
    {

        public IEnumerable<Model.Department> getAllDepartment()
        {
            return base.GetList().ToList();
        }


        public IQueryable<Department> getAllDepartmentAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<Department> getAllDepartmentAsQueryableOrderBy()
        {
            return null;
            //return base.GetList<string>(a => a.Enabled == true, a => a.DeptId);
        }

        public IQueryable<Department> getAllDepartmentAsQueryableOrderById(string uid)
        {
            return null;
            //return base.GetList<string>(a => a.DeptId == uid, a => a.DeptId);
        }


        public Department getDepartmentById(string Deptid)
        {
            return base.GetEntityById(Deptid);
        }


        public bool addDepartment(Department d)
        {
            return base.Add(d);
        }

        public bool updateDepartment(Department d)
        {
            return base.Update(d);
        }

        public bool deleteDepartment(Department d)
        {
            return base.Delete(d);
        }


        public bool deleteDepartmentBySQL(Department d)
        {
            return base.ExecuteStoreCommand("delete from BI_Department  where DeptId={0}", d.DeptId);
        }

        public IEnumerable<Department> selectDepartmentBySQL(string d)
        {
            return base.ExecuteQuery<Department>("select * from BI_Department where DeptId={0}", d);
        }
    }
}
