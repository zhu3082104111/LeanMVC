using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using Extensions;
using Model.Base;
using System.Collections;
namespace BLL
{
    
    public class RoleInfoServiceImp : AbstractService, IRoleInfoService
    {

       //需要使用的Repository类
        private IRoleInfoRepository RoleInfoRepository;
        private IUserInfoRepository UserInfoRepository;
        private IUserInfoLogRepository UserInfoLogRepository;
        private IMenuInfoRepository MenuInfoRepository;
        private IChainInfoRepository ChainInfoRepository;
        private IRoleChainInfoRepository RoleChainInfoRepository;
        private IURoleInfoRepository URoleInfoRepository;
        private IUDepartmentRepository UDepartmentRepository;
        private IDepartmentRepository DepartmentRepository;
             

        //构造方法，必须要，参数为需要注入的属性
        public RoleInfoServiceImp(IRoleInfoRepository RoleInfoRepository, IUserInfoRepository UserInfoRepository, 
                                  IUserInfoLogRepository UserInfoLogRepository, IMenuInfoRepository MenuInfoRepository, 
                                  IChainInfoRepository ChainInfoRepository, IRoleChainInfoRepository RoleChainInfoRepository,
                                  IURoleInfoRepository URoleInfoRepository, IUDepartmentRepository UDepartmentRepository,
                                  IDepartmentRepository DepartmentRepository) 
        {
            this.RoleInfoRepository = RoleInfoRepository;
            this.UserInfoRepository= UserInfoRepository;
            this.UserInfoLogRepository = UserInfoLogRepository;
            this.MenuInfoRepository=MenuInfoRepository;
            this.ChainInfoRepository= ChainInfoRepository;
            this.RoleChainInfoRepository = RoleChainInfoRepository;
            this.URoleInfoRepository = URoleInfoRepository;
            this.UDepartmentRepository = UDepartmentRepository;
            this.DepartmentRepository = DepartmentRepository;
        }
           
        // ============================================================//
        

        //继承自接口

        public IEnumerable<RoleInfo> getAllRoleInfo()
        {
            return RoleInfoRepository.getAllRoleInfo();
        }

        public IEnumerable<RoleInfo> exceptionExample()
        {
            throw new Exception("This is exception");
           
            return null ;
        }


        public IQueryable<RoleInfo> findAllRoleInfo()
        {
           return RoleInfoRepository.getAllRoleInfoAsQueryable();
        }

        public IQueryable<RoleInfo> findAllRoleInfoOrderBy()
        {
            return RoleInfoRepository.getAllRoleInfoAsQueryableOrderBy();
        }

        public RoleInfo getRoleInfoById(string rid)
        {
            return RoleInfoRepository.getRoleInfoById(rid);
        }

        public bool addRoleInfo(RoleInfo r)
        {
            return RoleInfoRepository.addRoleInfo(r);
        }

        public bool updateRoleInfo(RoleInfo r)
        {
            return RoleInfoRepository.updateRoleInfo(r);
        }

        public bool deleteRoleInfo(RoleInfo r)
        {
            return RoleInfoRepository.deleteRoleInfoBySQL(r);
        }

        

        //UserInfo
        


        //构造方法，必须要，参数为需要注入的属性
      

        // ============================================================//
        

        //继承自接口

        public IEnumerable<UserInfo> getAllUserInfo()
        {
            return UserInfoRepository.getAllUserInfo();
        }

        public IQueryable<UserInfo> findAllUserInfo()
        {
           return UserInfoRepository.getAllUserInfoAsQueryable();
        }

        public IQueryable<UserInfo> findAllUserInfoOrderBy()
        {
            return UserInfoRepository.getAllUserInfoAsQueryableOrderBy();
        }

        public IQueryable<UserInfo> findAllUserInfoOrderById(string uid)
        {
            return UserInfoRepository.getAllUserInfoAsQueryableOrderById(uid);
        }

        public UserInfo getUserInfoById(string uid)
        {
            return UserInfoRepository.getUserInfoById(uid);
        }

        
        public bool AddUserInfo(UserInfo u, UDepartment uDept)
        {

            bool result = UserInfoRepository.addUserInfo(u);
            result = UDepartmentRepository.addUDepartment(uDept);
            return result;
        }

        public bool addUserInfo(UserInfo u)
        {
            return UserInfoRepository.addUserInfo(u);
        }

        public bool updateUserInfo(UserInfo u)
        {
            return UserInfoRepository.updateUserInfo(u);
        }

        public bool deleteUserInfo(UserInfo u)
        {
            return UserInfoRepository.deleteUserInfoBySQL(u);
        }

        public UserInfo GetByUserNamePassword(string userName, string password) {
            
            return this.findAllUserInfoOrderBy().FirstOrDefault(a => a.UId.Equals(userName) && a.UPwd.Equals(password));
        
        }

        //MenuInfo

        //继承自接口

        public IEnumerable<MenuInfo> getAllMenuInfo()
        {
            return MenuInfoRepository.getAllMenuInfo();
        }

        public IQueryable<MenuInfo> findAllMenuInfo()
        {
            return MenuInfoRepository.getAllMenuInfoAsQueryable();
        }

        public IQueryable<MenuInfo> findAllMenuInfoOrderBy()
        {
            return MenuInfoRepository.getAllMenuInfoAsQueryableOrderBy();
        }

        public MenuInfo getMenuInfoById(string mid)
        {
            return MenuInfoRepository.getMenuInfoById(mid);
        }

        public bool addMenuInfo(MenuInfo m)
        {
            return MenuInfoRepository.addMenuInfo(m);
        }

        public bool updateMenuInfo(MenuInfo m)
        {
            return MenuInfoRepository.updateMenuInfo(m);
        }

        public bool deleteMenuInfo(MenuInfo m)
        {
            return MenuInfoRepository.deleteMenuInfoBySQL(m);
        }


        //UserInfoLog



        //构造方法，必须要，参数为需要注入的属性


        // ============================================================//


        //继承自接口

        public IEnumerable<UserInfoLog> getAllUserInfoLog()
        {
            return UserInfoLogRepository.getAllUserInfoLog();
        }

        public IQueryable<UserInfoLog> findAllUserInfoLog()
        {
            return UserInfoLogRepository.getAllUserInfoLogAsQueryable();
        }

        public UserInfoLog getUserInfoLogById(string uid)
        {
            return UserInfoLogRepository.getUserInfoLogById(uid);
        }

        public bool addUserInfoLog(UserInfoLog u)
        {
            return UserInfoLogRepository.addUserInfoLog(u);
        }

        public bool updateUserInfoLog(UserInfoLog u)
        {
            return UserInfoLogRepository.updateUserInfoLog(u);
        }

        public bool deleteUserInfoLog(UserInfoLog u)
        {
            return UserInfoLogRepository.deleteUserInfoLogBySQL(u);
        }




        //ChainInfo



        //构造方法，必须要，参数为需要注入的属性


        // ============================================================//


        //继承自接口

        public IEnumerable<ChainInfo> getAllChainInfo()
        {
            return ChainInfoRepository.getAllChainInfo();
        }

        public IQueryable<ChainInfo> findAllChainInfo()
        {
            return ChainInfoRepository.getAllChainInfoAsQueryable();
        }

        public IQueryable<ChainInfo> findAllChainInfoOrderBy()
        {
            return ChainInfoRepository.getAllChainInfoAsQueryableOrderBy();
        }

        public ChainInfo getChainInfoById(string id)
        {
            return ChainInfoRepository.getChainInfoById(id);
        }

        public bool addChainInfo(ChainInfo c)
        {
            return ChainInfoRepository.addChainInfo(c);
        }

        public bool updateChainInfo(ChainInfo c)
        {
            return ChainInfoRepository.updateChainInfo(c);
        }

        public bool deleteChainInfo(ChainInfo c)
        {
            return ChainInfoRepository.deleteChainInfoBySQL(c);
        }




        //RoleChainInfo



        //构造方法，必须要，参数为需要注入的属性


        // ============================================================//


        //继承自接口

        public IEnumerable<RoleChainInfo> getAllRoleChainInfo()
        {
            return RoleChainInfoRepository.getAllRoleChainInfo();
        }

        public IQueryable<RoleChainInfo> findAllRoleChainInfo()
        {
            return RoleChainInfoRepository.getAllRoleChainInfoAsQueryable();
        }

        public IQueryable<RoleChainInfo> findAllRoleChainInfoOrderBy()
        {
            return RoleChainInfoRepository.getAllRoleChainInfoAsQueryableOrderBy();
        }

        public RoleChainInfo getRoleChainInfoById(string id)
        {
            return RoleChainInfoRepository.getRoleChainInfoById(id);
        }

        public RoleChainInfo getRoleChainInfoByRId(string rid)
        {
            return RoleChainInfoRepository.getRoleChainInfoByRId(rid);
        }

        public bool addRoleChainInfo(RoleChainInfo c)
        {
            return RoleChainInfoRepository.addRoleChainInfo(c);
        }

        public bool updateRoleChainInfo(RoleChainInfo c)
        {
            return RoleChainInfoRepository.updateRoleChainInfo(c);
        }

        public bool deleteRoleChainInfo(RoleChainInfo c)
        {
            return RoleChainInfoRepository.deleteRoleChainInfoBySQL(c);
        }

        //URoleInfo
        //继承自接口

        public IEnumerable<URoleInfo> getAllURoleInfo()
        {
            return URoleInfoRepository.getAllURoleInfo();
        }


        public IQueryable<URoleInfo> findAllURoleInfo()
        {
            return URoleInfoRepository.getAllURoleInfoAsQueryable();
        }

        public IQueryable<URoleInfo> findAllURoleInfoOrderBy()
        {
            return URoleInfoRepository.getAllURoleInfoAsQueryableOrderBy();
        }

        public IQueryable<URoleInfo> getURoleInfoByIdOrderBy(string rid)
        {
            return null;
        }

        public URoleInfo getURoleInfoById(string id)
        {
            return URoleInfoRepository.getURoleInfoById(id);
        }

        public bool addURoleInfo(URoleInfo r)
        {
            return URoleInfoRepository.addURoleInfo(r);
        }

        public bool updateURoleInfo(URoleInfo r)
        {
            return URoleInfoRepository.updateURoleInfo(r);
        }

        public bool deleteURoleInfo(URoleInfo r)
        {
            return URoleInfoRepository.deleteURoleInfoBySQL(r);
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


        
        public bool updateNewUser(UserInfo user)
        {
           
            return true;
        }

        public IEnumerable GetUsersBySearchByPage(VM_UserInfoForSearch user, Paging paging, out int total)
        {
            /*IQueryable<UserInfo> list = UserInfoRepository.GetList();
            bool isPaging = true;//按主键查询时(单条记录)，不分页
            total = 1;
            if (!String.IsNullOrEmpty(user.UId))
            {
                list = list.Where(u => u.UId == user.UId);
                isPaging = false;
            }
            else
            {
                if (!String.IsNullOrEmpty(user.RealName))
                {
                    list = list.Where(u => u.RealName.Contains(user.RealName));
                }
                if (!String.IsNullOrEmpty(user.Sex))
                {
                    list = list.Where(u => u.Sex == user.Sex);
                }
               


            }
            /****************/
           /* IQueryable<UserInfo> users = list;
            IQueryable<UDepartment> uDepts = UDepartmentRepository.GetList();
            IQueryable<Department> depts = DepartmentRepository.GetList();
            var query = from u in users
                        join ud in uDepts on u.UId equals ud.UId into uds
                        from _ud in uds.DefaultIfEmpty()
                        join d in depts on _ud.DeptId equals d.DeptId into ds
                        from _d in ds.DefaultIfEmpty()
                        select new
                        {
                            UId = u.UId,
                            UName = u.UName,
                            RealName = u.RealName,
                            Sex = u.Sex,
                            Tel = u.Tel,
                            Email = u.Email,
                            Enabled = u.Enabled,
                            UPwd = u.UPwd,
                            DeptId = _ud.DeptId,
                            DeptName = _d.DeptName,
                            Id = _ud.Id
                        };
            if (isPaging)
            {
                total = query.Count();
                return query.ToPageList("UId asc", paging);
            }
            return query;
            /************/
            total = 1;
            return null;
        }


        [TransactionAop]
        public bool UpdateUserInfo(UserInfo u, UDepartment uDept)
        {
            UserInfoRepository.updateUserInfo(u);
            uDept.UId = u.UId;
            UDepartmentRepository.AddOrUpdate(uDept);
            //UDepartmentRepository.addUDepartment(uDept);
            return true;
        }

        [TransactionAop]
        public bool DeleteUserInfo(UserInfo u, UDepartment uDept)
        {
            UserInfoRepository.deleteUserInfoBySQL(u);
            UDepartmentRepository.deleteUDepartmentBySQL(uDept);
            return true;
        }



        public bool CheckSysUserSysRoleSysControllerSysActions(string Enabled, string userid, string area, string action, string controller)
        {
            //return
            //      findAllRoleInfoOrderBy().Any(
            //        a => a.URoleInfo.Any(b => b.UId.Equals(userid) && b.Enabled.Equals(Enabled))
            //         && a.RoleChainInfo.Any(
            //             b => b.ChainInfo.MenuInfo.ControllerName.Equals(controller) &&
            //             b.ChainInfo.EditInfo.ActionName.Equals(action)));
            return
                  findAllRoleInfoOrderBy().Any(
                    a => a.URoleInfo.Any(b => b.UId.Equals(userid))
                     && a.RoleChainInfo.Any(
                         b => b.ChainInfo.MenuInfo.ControllerName.Equals(controller) &&
                         b.ChainInfo.EditInfo.ActionName.Equals(action)));
             
        }
    }
}
