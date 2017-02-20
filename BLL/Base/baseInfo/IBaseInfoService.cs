using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace BLL
{
    public interface IBaseInfoService
    {
        //Department
        //接口方法
        IEnumerable<Department> getAllDepartment();

        //接口方法
        IEnumerable<Department> exceptionExample();

        IQueryable<Department> findAllDepartment();

        IQueryable<Department> findAllDepartmentOrderBy();

        IQueryable<Department> findAllDepartmentOrderById(string DeptId);

        Department getDepartmentById(string Deptid);

        bool addDepartment(Department d);

        bool updateDepartment(Department d);

        bool deleteDepartment(Department d);


        //UDepartment
        //接口方法
        IEnumerable<UDepartment> getAllUDepartment();

        IQueryable<UDepartment> findAllUDepartment();

        IQueryable<UDepartment> findAllUDepartmentOrderBy();

        UDepartment getUDepartmentById(string Deptid);

        bool addUDepartment(UDepartment d);

        bool updateUDepartment(UDepartment d);

        bool deleteUDepartment(UDepartment d);

        string SelectDeptName(string uid);

        /// <summary>
        /// 员工部门获取
        /// </summary>
        /// <returns></returns>
        IEnumerable<Department> GetDepartment();
    }
}
