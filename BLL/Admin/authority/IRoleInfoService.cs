using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Extensions;
using Model.Base;
using System.Collections;

namespace BLL
{
    public interface IRoleInfoService
    {
        //接口方法
        IEnumerable<RoleInfo> getAllRoleInfo();

        //接口方法
        IEnumerable<RoleInfo> exceptionExample();

        IQueryable<RoleInfo> findAllRoleInfo();

        IQueryable<RoleInfo> findAllRoleInfoOrderBy();

        RoleInfo getRoleInfoById(string rid);

        bool addRoleInfo(RoleInfo r);

        bool updateRoleInfo(RoleInfo r);

        bool deleteRoleInfo(RoleInfo r);

        bool CheckSysUserSysRoleSysControllerSysActions(string Enabled, string userid, string area, string action, string controller);


        //UserInfo
        //接口方法
        IEnumerable<UserInfo> getAllUserInfo();

        /// <summary>
        /// 按条件查询用户并分页
        /// </summary>
        /// <param name="user">待查找的用户的信息</param>
        /// <param name="paging">分页</param>
        /// <param name="total">满足条件的总数</param>
        /// <returns></returns>
        IEnumerable GetUsersBySearchByPage(VM_UserInfoForSearch user, Paging paging, out int total);

        //接口方法
       

        IQueryable<UserInfo> findAllUserInfo();

        IQueryable<UserInfo> findAllUserInfoOrderBy();

        IQueryable<UserInfo> findAllUserInfoOrderById(string uid);

        UserInfo getUserInfoById(string uid);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="u">用户信息</param>
        /// <param name="uDept">关联部门</param>
        /// <returns></returns>
        [TransactionAop]
        bool AddUserInfo(UserInfo u, UDepartment uDept);

        bool addUserInfo(UserInfo u);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="u">待更新的用户信息</param>
        /// <param name="uDept">待更新的部门信息</param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateUserInfo(UserInfo u, UDepartment uDept);

        bool updateUserInfo(UserInfo u);

        bool deleteUserInfo(UserInfo u);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="u">待删除的用户信息</param>
        /// <param name="uDept">待删除的部门信息</param>
        /// <returns></returns>
        [TransactionAop]
        bool DeleteUserInfo(UserInfo u, UDepartment uDept);

        UserInfo GetByUserNamePassword(string userName, string password);


        //MenuInfo
        //接口方法
        IEnumerable<MenuInfo> getAllMenuInfo();

        //接口方法
       
        IQueryable<MenuInfo> findAllMenuInfo();

        IQueryable<MenuInfo> findAllMenuInfoOrderBy();

        MenuInfo getMenuInfoById(string mid);

        bool addMenuInfo(MenuInfo m);

        bool updateMenuInfo(MenuInfo m);

        bool deleteMenuInfo(MenuInfo m);


        //UserInfoLog
        //接口方法
        IEnumerable<UserInfoLog> getAllUserInfoLog();

        //接口方法


        IQueryable<UserInfoLog> findAllUserInfoLog();

        UserInfoLog getUserInfoLogById(string uid);

        bool addUserInfoLog(UserInfoLog u);

        bool updateUserInfoLog(UserInfoLog u);

        bool deleteUserInfoLog(UserInfoLog u);



        //ChainInfo
        //接口方法
        IEnumerable<ChainInfo> getAllChainInfo();

        //接口方法


        IQueryable<ChainInfo> findAllChainInfo();

        IQueryable<ChainInfo> findAllChainInfoOrderBy();

        ChainInfo getChainInfoById(string id);

        bool addChainInfo(ChainInfo c);

        bool updateChainInfo(ChainInfo c);

        bool deleteChainInfo(ChainInfo c);

        //RoleChainInfo
        //接口方法
        IEnumerable<RoleChainInfo> getAllRoleChainInfo();

        //接口方法


        IQueryable<RoleChainInfo> findAllRoleChainInfo();

        IQueryable<RoleChainInfo> findAllRoleChainInfoOrderBy();

        RoleChainInfo getRoleChainInfoById(string id);

        RoleChainInfo getRoleChainInfoByRId(string rid);

        bool addRoleChainInfo(RoleChainInfo r);

        bool updateRoleChainInfo(RoleChainInfo r);

        bool deleteRoleChainInfo(RoleChainInfo r);


        //URoleInfo
        //接口方法
        IEnumerable<URoleInfo> getAllURoleInfo();

        //接口方法

        IQueryable<URoleInfo> findAllURoleInfo();

        IQueryable<URoleInfo> findAllURoleInfoOrderBy();

        IQueryable<URoleInfo> getURoleInfoByIdOrderBy(string id);

        URoleInfo getURoleInfoById(string id);

        bool addURoleInfo(URoleInfo r);

        bool updateURoleInfo(URoleInfo r);

        bool deleteURoleInfo(URoleInfo r);

        //UDepartment
        //接口方法
        IEnumerable<UDepartment> getAllUDepartment();

        IQueryable<UDepartment> findAllUDepartment();

        IQueryable<UDepartment> findAllUDepartmentOrderBy();

        UDepartment getUDepartmentById(string Deptid);

        bool addUDepartment(UDepartment d);

        bool updateUDepartment(UDepartment d);

        bool deleteUDepartment(UDepartment d);

        [TransactionAop]
        bool updateNewUser(UserInfo user);


    }
}
