using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
namespace BLL
   
{
    public class BaseInfoServiceImp :AbstractService, IBaseInfoService
    {

       //需要使用的Repository类
        private IDepartmentRepository DepartmentRepository;
        private IUDepartmentRepository UDepartmentRepository;


        //构造方法，必须要，参数为需要注入的属性
        public BaseInfoServiceImp(IDepartmentRepository DepartmentRepository, IUDepartmentRepository UDepartmentRepository) 
        {
            this.DepartmentRepository = DepartmentRepository;
            this.UDepartmentRepository = UDepartmentRepository;

        
        }

        // ============================================================//

        //Department
        //继承自接口

        public IEnumerable<Department> getAllDepartment()
        {
            return DepartmentRepository.getAllDepartment();
        }

        public IEnumerable<Department> exceptionExample()
        {
            throw new Exception("This is exception");
           
            return null ;
        }


        public IQueryable<Department> findAllDepartment()
        {
           return DepartmentRepository.getAllDepartmentAsQueryable();
        }

        public IQueryable<Department> findAllDepartmentOrderBy()
        {
            return DepartmentRepository.getAllDepartmentAsQueryableOrderBy();
        }

        public IQueryable<Department> findAllDepartmentOrderById(string uid)
        {
            return DepartmentRepository.getAllDepartmentAsQueryableOrderById(uid);
        }

        public Department getDepartmentById(string Deptid)
        {
            return DepartmentRepository.getDepartmentById(Deptid);
        }

        public bool addDepartment(Department d)
        {
            return DepartmentRepository.addDepartment(d);
        }

        public bool updateDepartment(Department d)
        {
            return DepartmentRepository.updateDepartment(d);
        }

        public bool deleteDepartment(Department d)
        {
            return DepartmentRepository.deleteDepartmentBySQL(d);
        }


        //UDepartment
        //继承自接口

        public IEnumerable<UDepartment> getAllUDepartment()
        {
            return UDepartmentRepository.getAllUDepartment();
        }


        public IQueryable<UDepartment> findAllUDepartment()
        {
            return UDepartmentRepository.getAllUDepartmentAsQueryable();
        }

        public IQueryable<UDepartment> findAllUDepartmentOrderBy()
        {
            return UDepartmentRepository.getAllUDepartmentAsQueryableOrderBy();
        }

        public UDepartment getUDepartmentById(string Deptid)
        {
            return UDepartmentRepository.getUDepartmentById(Deptid);
        }

        public bool addUDepartment(UDepartment d)
        {
            return UDepartmentRepository.addUDepartment(d);
        }

        public bool updateUDepartment(UDepartment d)
        {
            return UDepartmentRepository.updateUDepartment(d);
        }

        public bool deleteUDepartment(UDepartment d)
        {
            return UDepartmentRepository.deleteUDepartmentBySQL(d);
        }

        //得到当前用户所在部门
        public string SelectDeptName(string uid)
        {
            string DeptName = "";
            var UDepartment = UDepartmentRepository.selectUDepartmentBySQL(uid);
            foreach (Object obj in UDepartment)
            {
                if (obj is UDepartment)//这个是类型的判断，这里Department是一个类或结构
                {
                    UDepartment UDep = (UDepartment)obj;
                    var Department = DepartmentRepository.selectDepartmentBySQL(UDep.DeptId);
                    foreach (Object o in Department)
                    {
                        if (o is Department)
                        {
                            Department Dep = (Department)o;
                            DeptName = Dep.DeptName;
                        }
                    }
                }
            }
            return DeptName;
        }

        /// <summary>
        /// 员工部门获取
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> GetDepartment()
        {
            return DepartmentRepository.GetListByConditionWithOrderBy(d => d.DeptId.Contains(""), s => s.DeptName);
            //throw new NotImplementedException();
        }
    }
}
