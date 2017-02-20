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
    public class UDepartmentRepositoryImp : AbstractRepository<DB, UDepartment>, IUDepartmentRepository
    {

        public IEnumerable<Model.UDepartment> getAllUDepartment()
        {
            return base.GetList().ToList();
        }


        public IQueryable<UDepartment> getAllUDepartmentAsQueryable()
        {
            return base.GetList();
        }

        public IQueryable<UDepartment> getAllUDepartmentAsQueryableOrderBy()
        {
            return null;
            //return base.GetList<string>(a => a.DeptId);
        }

        public UDepartment getUDepartmentById(string Deptid)
        {
            return base.GetEntityById(Deptid);
        }


        public bool addUDepartment(UDepartment d)
        {
            return base.Add(d);
        }

        public bool updateUDepartment(UDepartment d)
        {
            return base.Update(d);
        }

        public bool deleteUDepartment(UDepartment d)
        {
            return base.Delete(d);
        }


        public bool deleteUDepartmentBySQL(UDepartment d)
        {
            return base.ExecuteStoreCommand("delete from BI_UDepartment  where UId={0}", d.UId);
        }

        public bool AddOrUpdate(UDepartment ud)
        {
            if (String.IsNullOrEmpty(ud.Id))
            {
                ud.Id = Guid.NewGuid().ToString();
                return base.Add(ud);
            }
            return base.Update(ud);
        }

        public IEnumerable<UDepartment> selectUDepartmentBySQL(string ud)
        {
            return base.ExecuteQuery<UDepartment>("select * from BI_UDepartment where UId={0}", ud);
        }
    }
}
