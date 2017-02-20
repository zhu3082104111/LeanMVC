/*
Navicat SQL Server Data Transfer

Source Server         : LDK
Source Server Version : 90000
Source Host           : BCL_WH_DDZ-PC\SQLEXPRESS:1433
Source Database       : LDK
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 90000
File Encoding         : 65001

Date: 2013-09-23 11:51:55
*/




-- ----------------------------
-- Table structure for [dbo].[BI_Department]
-- ----------------------------
DROP TABLE [dbo].[BI_Department]
GO
CREATE TABLE [dbo].[BI_Department] (
[DeptId] nvarchar(128) NOT NULL ,
[Department1] nvarchar(20) NULL ,
[DeptName] nvarchar(50) NULL ,
[DeptParentId] nvarchar(20) NULL ,
[ManagerIds] nvarchar(128) NOT NULL ,
[Tel] nvarchar(20) NULL ,
[Enabled] bit NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)
GO

-- ----------------------------
-- Records of BI_Department
-- ----------------------------
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1000', N'公司', N'公司', N'1000', N'4234234', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1001', N'第一开发部', N'第一开发部', N'1000', N'5966103', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1002', N'第二开发部', N'第二开发部', N'1000', N'4345435', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1003', N'第三开发部', N'第三开发部', N'1002', N'4233434', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'haha');
GO
-- ----------------------------
-- Table structure for [dbo].[BI_DepartType]
-- ----------------------------
DROP TABLE [dbo].[BI_DepartType]
GO
CREATE TABLE [dbo].[BI_DepartType] (
[ID] nvarchar(128) NOT NULL ,
[DepartID] nvarchar(50) NULL ,
[DGuid] nvarchar(50) NULL 
)


GO

-- ----------------------------
-- Records of BI_DepartType
-- ----------------------------

-- ----------------------------
-- Table structure for [dbo].[BI_EditInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_EditInfo]
GO
CREATE TABLE [dbo].[BI_EditInfo] (
[EId] nvarchar(128) NOT NULL ,
[EName] nvarchar(20) NOT NULL ,
[ActionName] nvarchar(20) NULL ,
[SystemId] nvarchar(20) NULL ,
[Remark] nvarchar(50) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_EditInfo
-- ----------------------------
INSERT INTO [dbo].[BI_EditInfo] ([EId], [EName], [ActionName], [SystemId], [Remark], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'1', N'列表', N'Index', N'001', null, N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228');
GO
INSERT INTO [dbo].[BI_EditInfo] ([EId], [EName], [ActionName], [SystemId], [Remark], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'2', N'删除', N'Delete', N'006', null, N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228');
GO
INSERT INTO [dbo].[BI_EditInfo] ([EId], [EName], [ActionName], [SystemId], [Remark], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'3', N'新建', N'Create', N'004', null, N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228');
GO
INSERT INTO [dbo].[BI_EditInfo] ([EId], [EName], [ActionName], [SystemId], [Remark], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'4', N'详细', N'Details', N'003', null, N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228');
GO
INSERT INTO [dbo].[BI_EditInfo] ([EId], [EName], [ActionName], [SystemId], [Remark], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5', N'编辑', N'Edit', N'005', null, N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_Log]
-- ----------------------------
DROP TABLE [dbo].[BI_Log]
GO
CREATE TABLE [dbo].[BI_Log] (
[Id] nvarchar(128) NOT NULL ,
[Title] nvarchar(100) NULL ,
[Remark] nvarchar(50) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_Log
-- ----------------------------

-- ----------------------------
-- Table structure for [dbo].[BI_MenuInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_MenuInfo]
GO
CREATE TABLE [dbo].[BI_MenuInfo] (
[MId] nvarchar(128) NOT NULL ,
[SystemId] nvarchar(20) NOT NULL ,
[MName] nvarchar(50) NOT NULL ,
[ControllerName] nvarchar(50) NOT NULL ,
[ActionName] nvarchar(50) NOT NULL ,
[Parameter] nvarchar(50) NULL ,
[Display] bit NOT NULL ,
[Enabled] bit NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL ,
[AreaName] nvarchar(50) NOT NULL DEFAULT '' 
)


GO

-- ----------------------------
-- Records of BI_MenuInfo
-- ----------------------------
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'1', N'100', N'系统管理', N'Index', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'10', N'100200', N'使用帮助', N'Help', N'Index', null, N'0', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'11', N'100100', N'桌面', N'Desktop', N'Index', null, N'0', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'12', N'200300', N'统计信息', N'SysStatistic', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'2', N'100300', N'用户管理', N'UserInfo', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'3', N'100400', N'角色管理', N'RoleInfo', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'4', N'100500', N'菜单管理', N'MenuInfo', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'5', N'100600', N'部门管理', N'Department', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'6', N'100700', N'通知公告', N'Notice', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'7', N'200', N'系统信息', N'System', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'8', N'200100', N'系统参数', N'WebConfigAppSetting', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO
INSERT INTO [dbo].[BI_MenuInfo] ([MId], [SystemId], [MName], [ControllerName], [ActionName], [Parameter], [Display], [Enabled], [Addate], [Aduser], [Uddate], [Uduser], [AreaName]) VALUES (N'9', N'200200', N'帮助信息', N'SysHelp', N'Index', null, N'1', N'1', N'2013-08-07 13:44:17.000', N'201228', N'2013-08-07 13:44:17.000', N'201228', N'Admin');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_ChainInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_ChainInfo]
GO
CREATE TABLE [dbo].[BI_ChainInfo] (
[Id] nvarchar(128) NOT NULL ,
[MId] nvarchar(128) NULL ,
[EId] nvarchar(128) NULL ,
[Remark] nvarchar(50) NULL ,
[State] bit NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_ChainInfo
-- ----------------------------
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'1', N'1', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'10', N'2', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'11', N'3', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'12', N'3', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'13', N'3', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'14', N'3', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'15', N'3', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'16', N'4', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'17', N'4', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'18', N'4', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'19', N'4', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'2', N'1', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'20', N'4', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'21', N'5', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'22', N'5', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'23', N'5', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'24', N'5', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'25', N'5', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'26', N'6', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'27', N'6', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'28', N'6', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'29', N'6', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'3', N'1', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'30', N'6', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'31', N'7', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'32', N'7', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'33', N'7', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'34', N'7', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'35', N'7', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'36', N'8', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'37', N'8', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'38', N'8', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'39', N'8', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'4', N'1', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'40', N'8', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'41', N'9', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'42', N'9', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'43', N'9', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'44', N'9', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'45', N'9', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'46', N'10', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'47', N'10', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'48', N'10', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'49', N'10', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5', N'1', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'50', N'10', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'51', N'11', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'52', N'11', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'53', N'11', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'54', N'11', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'55', N'11', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'56', N'12', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'57', N'12', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'58', N'12', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'59', N'12', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'6', N'2', N'1', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'60', N'12', N'5', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7', N'2', N'2', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'8', N'2', N'3', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO
INSERT INTO [dbo].[BI_ChainInfo] ([Id], [MId], [EId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'9', N'2', N'4', null, N'0', N'2013-08-27 02:44:40.000', null, N'2013-08-27 02:44:40.000', null);
GO

-- ----------------------------
-- Table structure for [dbo].[BI_Notice]
-- ----------------------------
DROP TABLE [dbo].[BI_Notice]
GO
CREATE TABLE [dbo].[BI_Notice] (
[NoticeID] nvarchar(128) NOT NULL ,
[TypeID] int NOT NULL ,
[Title] nvarchar(100) NOT NULL ,
[Info] nvarchar(MAX) NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL ,
[Enabled] bit NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of BI_Notice
-- ----------------------------
INSERT INTO [dbo].[BI_Notice] ([NoticeID], [TypeID], [Title], [Info], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'1', N'0', N'DbSet<T>.AddOrUpdate()测试记录。', N'默认以主键来判断该记录是否存在，并进行添加更新。', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1');
GO
INSERT INTO [dbo].[BI_Notice] ([NoticeID], [TypeID], [Title], [Info], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'2', N'0', N'新加功能模块及内容可在Configuraion.cs中进行添加', N'默认以主键来判断该记录是否存在，并进行添加更新。', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_NoticeInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_NoticeInfo]
GO
CREATE TABLE [dbo].[BI_NoticeInfo] (
[ID] nvarchar(128) NOT NULL ,
[NoticeName] nvarchar(20) NULL ,
[State] int NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_NoticeInfo
-- ----------------------------

-- ----------------------------
-- Table structure for [dbo].[BI_NoticeType]
-- ----------------------------
DROP TABLE [dbo].[BI_NoticeType]
GO
CREATE TABLE [dbo].[BI_NoticeType] (
[NoticeTypeID] nvarchar(50) NOT NULL ,
[TypeName] int NOT NULL ,
[Rguid] nvarchar(50) NULL 
)


GO

-- ----------------------------
-- Records of BI_NoticeType
-- ----------------------------

-- ----------------------------
-- Table structure for [dbo].[BI_RoleChainInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_RoleChainInfo]
GO
CREATE TABLE [dbo].[BI_RoleChainInfo] (
[Id] nvarchar(128) NOT NULL ,
[RId] nvarchar(128) NOT NULL ,
[CId] nvarchar(128) NOT NULL ,
[Remark] nvarchar(20) NULL ,
[State] bit NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_RoleChainInfo
-- ----------------------------
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0450eff6-d812-4169-b248-325ee1bd7059', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'51', null, N'0', N'2013-09-04 16:17:04.910', null, N'2013-09-04 16:17:04.910', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'063546de-dac8-43db-a25e-e29d98547967', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'8', null, N'0', N'2013-09-04 16:17:05.200', null, N'2013-09-04 16:17:05.200', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'093d7f82-8cfa-431c-95e6-87d40c2abe74', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'6', null, N'0', N'2013-09-04 16:17:05.700', null, N'2013-09-04 16:17:05.700', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0a3e8b75-aace-4c09-a6eb-fe52560ddfb6', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'33', null, N'0', N'2013-09-04 16:18:15.830', null, N'2013-09-04 16:18:15.830', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0a452e45-b70a-4344-8c62-90805ec8c9e1', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'11', null, N'0', N'2013-09-04 16:18:15.793', null, N'2013-09-04 16:18:15.793', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0b009907-62f2-4410-b4d7-4f487d7eba19', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'36', null, N'0', N'2013-09-04 16:17:05.183', null, N'2013-09-04 16:17:05.183', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0d9e7f6f-bcc7-4037-9eca-9f3ac64bc775', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'43', null, N'0', N'2013-09-04 16:18:15.910', null, N'2013-09-04 16:18:15.910', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0e7f406b-77b4-438c-86ad-a6cce85ce668', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'4', null, N'0', N'2013-09-04 16:18:15.727', null, N'2013-09-04 16:18:15.727', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'0ea4275c-4e10-449f-82b7-744f404b226b', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'38', null, N'0', N'2013-09-04 16:18:15.857', null, N'2013-09-04 16:18:15.857', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'126d1ffe-7f85-48d8-b721-2ab711b0c3f7', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'24', null, N'0', N'2013-09-04 16:08:26.423', null, N'2013-09-04 16:08:26.423', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'1c6ee977-8598-4d27-aaa0-c88059aa8acb', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'5', null, N'0', N'2013-09-04 16:18:15.737', null, N'2013-09-04 16:18:15.737', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'1d4ecbb2-9078-4360-b3f2-3a271d7773df', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'14', null, N'0', N'2013-09-04 16:18:15.797', null, N'2013-09-04 16:18:15.797', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'1e8a3a56-8da3-46bc-8beb-eb5a6947323c', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'21', null, N'0', N'2013-09-04 16:08:26.413', null, N'2013-09-04 16:08:26.413', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'21f3f6bd-3c12-4ba3-a260-170325d4ad72', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'39', null, N'0', N'2013-09-04 16:18:15.850', null, N'2013-09-04 16:18:15.850', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'28ecd55f-00f5-4783-a9e2-52a3a592d211', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'23', null, N'0', N'2013-09-04 16:08:26.430', null, N'2013-09-04 16:08:26.430', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'2bfbf022-bb20-4a5d-8def-f62d226af958', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'45', null, N'0', N'2013-09-04 16:17:05.260', null, N'2013-09-04 16:17:05.260', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'2da26ba3-dab7-459d-ade2-db0329330fbc', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'45', null, N'0', N'2013-09-04 16:18:15.953', null, N'2013-09-04 16:18:15.953', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'2e323ed1-1570-48dc-a93c-853584a3a56b', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'42', null, N'0', N'2013-09-04 16:18:15.960', null, N'2013-09-04 16:18:15.960', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'32b58ba4-e23c-4ece-8cf8-3bdbf571d9e5', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'12', null, N'0', N'2013-09-04 16:17:05.600', null, N'2013-09-04 16:17:05.600', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'3344545a-4858-4ac8-95dd-94e3e715eb4a', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'42', null, N'0', N'2013-09-04 16:17:05.267', null, N'2013-09-04 16:17:05.267', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'34cda784-3b81-4c37-a694-9cb629c80a2d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'26', null, N'0', N'2013-09-04 16:17:05.123', null, N'2013-09-04 16:17:05.123', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'37e92ac5-fa8c-4117-8290-436c9a42e387', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'4', null, N'0', N'2013-09-04 16:17:04.790', null, N'2013-09-04 16:17:04.790', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'3ce554d1-a536-40e3-90fe-693ecb752023', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'39', null, N'0', N'2013-09-04 16:17:05.190', null, N'2013-09-04 16:17:05.190', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'405f68ed-ef4f-486f-80d0-fb030996f39d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'5', null, N'0', N'2013-09-04 16:17:04.817', null, N'2013-09-04 16:17:04.817', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'40873354-4412-4478-9359-ec8273bd60c8', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'12', null, N'0', N'2013-09-04 16:18:15.813', null, N'2013-09-04 16:18:15.813', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'450c496b-77fd-4ecf-89a5-a849b588aca8', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'3', null, N'0', N'2013-09-04 16:17:04.803', null, N'2013-09-04 16:17:04.803', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'45605a12-2f0e-4768-9ab8-c521e106804d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'28', null, N'0', N'2013-09-04 16:17:05.133', null, N'2013-09-04 16:17:05.133', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'46c83128-33dc-451a-9e9a-09caa6ff3a36', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'3', null, N'0', N'2013-09-04 16:18:15.733', null, N'2013-09-04 16:18:15.733', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'492a3116-415b-43e8-87a6-93536c87ef09', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'14', null, N'0', N'2013-09-04 16:17:05.470', null, N'2013-09-04 16:17:05.470', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'53243c0f-f1c3-4d25-a9f5-27a4cb8ebf49', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'46', null, N'0', N'2013-09-04 16:17:04.840', null, N'2013-09-04 16:17:04.840', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5911b360-1ff1-4f65-b14f-257ad763bf2e', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'16', null, N'0', N'2013-09-04 16:17:05.670', null, N'2013-09-04 16:17:05.670', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5d4d3e58-52f3-4a57-a082-d08650e5d12d', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'32', null, N'0', N'2013-09-04 16:18:15.840', null, N'2013-09-04 16:18:15.840', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5d8882f9-6200-46d4-91e5-f10ac0c4986b', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'58', null, N'0', N'2013-09-04 16:17:04.990', null, N'2013-09-04 16:17:04.990', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5ebd6d6b-a2bd-4a70-a004-420b96894cd0', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'21', null, N'0', N'2013-09-04 16:17:05.970', null, N'2013-09-04 16:17:05.970', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'5f4d44cd-15e0-47d5-8b0f-d732052ab16d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'35', null, N'0', N'2013-09-04 16:17:05.173', null, N'2013-09-04 16:17:05.173', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'61857671-e057-4328-8fa4-07cec31f6bf8', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'35', null, N'0', N'2013-09-04 16:18:15.837', null, N'2013-09-04 16:18:15.837', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'66129761-4e56-4163-bb9c-70cfb8d7741d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'31', null, N'0', N'2013-09-04 16:17:05.153', null, N'2013-09-04 16:17:05.153', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'66c2c2ff-e3c3-44e5-afe6-2c3a3eed2b3d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'59', null, N'0', N'2013-09-04 16:17:04.983', null, N'2013-09-04 16:17:04.983', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'6cfca134-a119-44a0-95ac-b981cfd5de09', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'17', null, N'0', N'2013-09-04 16:08:26.407', null, N'2013-09-04 16:08:26.407', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'707a6f46-4e85-4a4e-84ca-a2c7d3753265', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'31', null, N'0', N'2013-09-04 16:18:15.820', null, N'2013-09-04 16:18:15.820', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'70a9051c-d2b6-492e-89c5-76cd772f5fab', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'25', null, N'0', N'2013-09-04 16:17:05.110', null, N'2013-09-04 16:17:05.110', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'70e75d3d-a1f8-4b59-8ea9-bdab100518dc', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'18', null, N'0', N'2013-09-04 16:08:26.387', null, N'2013-09-04 16:08:26.387', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'72bdbc49-e950-4770-a950-debceeac2f54', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'44', null, N'0', N'2013-09-04 16:17:05.243', null, N'2013-09-04 16:17:05.243', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'76c1ecd1-73ce-46bd-8aea-870d98dcc96e', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'16', null, N'0', N'2013-09-04 16:08:26.293', null, N'2013-09-04 16:08:26.293', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7792393c-6d25-4e6d-b83a-623e1557717a', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'57', null, N'0', N'2013-09-04 16:17:05.000', null, N'2013-09-04 16:17:05.000', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7a777ed3-6fea-4b0e-b580-48f78bd970f7', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'1', null, N'0', N'2013-09-04 16:17:04.777', null, N'2013-09-04 16:17:04.777', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7d6be51a-d26c-456e-8132-e24ab81b27f7', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'40', null, N'0', N'2013-09-04 16:18:15.863', null, N'2013-09-04 16:18:15.863', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7dd80fda-d437-49b2-b8a3-3c16d0cfb15d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'52', null, N'0', N'2013-09-04 16:17:04.940', null, N'2013-09-04 16:17:04.940', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'7f2da100-fdf4-43c6-bc37-8a45330d83ce', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'9', null, N'0', N'2013-09-04 16:17:05.130', null, N'2013-09-04 16:17:05.130', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'82e3de7c-845a-49f1-932c-10c0513760f6', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', null, N'0', N'2013-09-04 16:18:15.807', null, N'2013-09-04 16:18:15.807', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'87588f3f-178f-4806-9b0c-3eddaa33ce2d', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'7', null, N'0', N'2013-09-04 16:18:15.787', null, N'2013-09-04 16:18:15.787', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'88fcb2d7-638d-4544-9772-9ad37bb36688', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'30', null, N'0', N'2013-09-04 16:17:05.143', null, N'2013-09-04 16:17:05.143', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'8cd4df73-0018-42af-bbdb-fb5b953ae5b0', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'8', null, N'0', N'2013-09-04 16:18:15.753', null, N'2013-09-04 16:18:15.753', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'980a2fcd-ef4b-4683-9f6e-961442a8ede3', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'33', null, N'0', N'2013-09-04 16:17:05.167', null, N'2013-09-04 16:17:05.167', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'9aff6b63-99bb-484e-9eb8-99dcd06ddea6', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'25', null, N'0', N'2013-09-04 16:08:26.437', null, N'2013-09-04 16:08:26.437', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'9e9c5d8d-a2fc-4b5f-bc01-88e8f191d46d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'48', null, N'0', N'2013-09-04 16:17:04.887', null, N'2013-09-04 16:17:04.887', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'a0d3b5e2-4e4d-41c6-b9db-0d0a5c8fff1f', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'2', null, N'0', N'2013-09-04 16:17:04.827', null, N'2013-09-04 16:17:04.827', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'a235c459-20ec-4309-833a-7051cad48b26', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'54', null, N'0', N'2013-09-04 16:17:04.920', null, N'2013-09-04 16:17:04.920', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'a3c11e18-632c-4d5d-b600-ea49516b3bdf', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'53', null, N'0', N'2013-09-04 16:17:04.927', null, N'2013-09-04 16:17:04.927', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'aa0a76ac-60eb-450c-b51e-00dcec153d8b', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'6', null, N'0', N'2013-09-04 16:18:15.743', null, N'2013-09-04 16:18:15.743', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ac27f00e-c1f9-456e-92dc-aaf5e5f072eb', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'29', null, N'0', N'2013-09-04 16:17:05.127', null, N'2013-09-04 16:17:05.127', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ad4da0f2-7926-4f62-a6b1-2e1a5b662dd1', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'60', null, N'0', N'2013-09-04 16:17:04.997', null, N'2013-09-04 16:17:04.997', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'b41ed005-ccd4-41f0-b9bb-a29b495d5260', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'55', null, N'0', N'2013-09-04 16:17:04.933', null, N'2013-09-04 16:17:04.933', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'b41f5d52-d6f7-4c1d-945e-6b2870efdc72', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'7', null, N'0', N'2013-09-04 16:17:05.330', null, N'2013-09-04 16:17:05.330', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'b4530eb7-f09b-47a8-b6da-4014772f152d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'37', null, N'0', N'2013-09-04 16:17:05.227', null, N'2013-09-04 16:17:05.227', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'b6a0509b-10a6-4dcc-8dc1-80e7c96cf31d', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'37', null, N'0', N'2013-09-04 16:18:15.870', null, N'2013-09-04 16:18:15.870', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ba196ff7-d30b-4630-8cfd-a73cbfac2611', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'13', null, N'0', N'2013-09-04 16:17:05.500', null, N'2013-09-04 16:17:05.500', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'bafa7b42-6f30-45b6-a091-30089ecfef78', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'9', null, N'0', N'2013-09-04 16:18:15.747', null, N'2013-09-04 16:18:15.747', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'bf18604f-7e70-48e1-921b-5a7cb3501742', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'44', null, N'0', N'2013-09-04 16:18:15.903', null, N'2013-09-04 16:18:15.903', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c0f2dfaf-8b2c-4e66-ba38-1b56aee1d9b7', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'50', null, N'0', N'2013-09-04 16:17:04.897', null, N'2013-09-04 16:17:04.897', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c212eacc-1173-42dd-aa82-ecc7a309511e', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'20', null, N'0', N'2013-09-04 16:08:26.397', null, N'2013-09-04 16:08:26.397', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c262a6cc-274c-4421-8675-9db3910d4276', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'22', null, N'0', N'2013-09-04 16:08:26.470', null, N'2013-09-04 16:08:26.470', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c2d02b0d-2cac-4ef4-bd9e-841b945ecd89', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'22', null, N'0', N'2013-09-04 16:17:05.117', null, N'2013-09-04 16:17:05.117', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c692d03a-933e-4753-a88d-b6a816624201', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'40', null, N'0', N'2013-09-04 16:17:05.203', null, N'2013-09-04 16:17:05.203', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'c8198706-7878-4b44-81fb-48c38c62933f', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'34', null, N'0', N'2013-09-04 16:17:05.160', null, N'2013-09-04 16:17:05.160', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'cca524b1-cd26-4b4a-aa70-1578f4e58f50', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'24', null, N'0', N'2013-09-04 16:17:05.100', null, N'2013-09-04 16:17:05.100', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'cccc9137-a40b-46f8-9f31-012f4e4071d8', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'18', null, N'0', N'2013-09-04 16:17:05.770', null, N'2013-09-04 16:17:05.770', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'cef6f08d-acba-4f03-91fd-d882d4dc37dd', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'41', null, N'0', N'2013-09-04 16:17:05.233', null, N'2013-09-04 16:17:05.233', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd0c106ca-94bb-408b-bb28-6e56af6bd00e', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'10', null, N'0', N'2013-09-04 16:17:05.270', null, N'2013-09-04 16:17:05.270', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd0d67623-6234-4d20-b190-44153ca4b18d', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'21', null, N'0', N'2013-09-04 16:18:15.817', null, N'2013-09-04 16:18:15.817', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd1bd663e-ed65-4278-80d4-8be0f6ca967a', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'47', null, N'0', N'2013-09-04 16:17:04.903', null, N'2013-09-04 16:17:04.903', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd2615244-f321-4ccb-bcde-6cd5a4905522', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'41', null, N'0', N'2013-09-04 16:18:15.897', null, N'2013-09-04 16:18:15.897', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd383d0f6-527e-4a9c-9dfc-a6831de23729', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'43', null, N'0', N'2013-09-04 16:17:05.250', null, N'2013-09-04 16:17:05.250', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd5d93307-0b93-4e5f-951f-386c758edba0', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'19', null, N'0', N'2013-09-04 16:08:26.337', null, N'2013-09-04 16:08:26.337', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'dbe38536-2143-4b3e-9ae0-9de0c72dcebe', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'34', null, N'0', N'2013-09-04 16:18:15.827', null, N'2013-09-04 16:18:15.827', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e08221d0-a46a-4f96-b8cf-dbc4e1431af8', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'17', null, N'0', N'2013-09-04 16:17:05.900', null, N'2013-09-04 16:17:05.900', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e1c19518-3670-4253-b252-000020875f2f', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'49', null, N'0', N'2013-09-04 16:17:04.847', null, N'2013-09-04 16:17:04.847', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e488988c-6e21-47dc-8730-b9e4dfeda933', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'15', null, N'0', N'2013-09-04 16:17:05.570', null, N'2013-09-04 16:17:05.570', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e505ca09-ac35-4870-a9de-6234e2875cf9', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'23', null, N'0', N'2013-09-04 16:17:05.107', null, N'2013-09-04 16:17:05.107', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e59ea4bf-285c-4ee9-99e8-654d7950c10d', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'38', null, N'0', N'2013-09-04 16:17:05.197', null, N'2013-09-04 16:17:05.197', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e8183bfa-9991-4bf5-953f-30d0495e3a6d', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'36', null, N'0', N'2013-09-04 16:18:15.847', null, N'2013-09-04 16:18:15.847', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e8531f07-6668-449b-80b1-003a7c63b45a', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'11', null, N'0', N'2013-09-04 16:17:05.370', null, N'2013-09-04 16:17:05.370', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e990736e-91a5-4846-b190-90a527ecb794', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'56', null, N'0', N'2013-09-04 16:17:04.973', null, N'2013-09-04 16:17:04.973', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ec87e2f0-7af5-4c77-b2a1-6c5073d3142b', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'27', null, N'0', N'2013-09-04 16:17:05.147', null, N'2013-09-04 16:17:05.147', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ef72df15-65bc-4ff1-9723-d6aae2e79140', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'19', null, N'0', N'2013-09-04 16:17:05.730', null, N'2013-09-04 16:17:05.730', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'f0699ef8-804b-4c92-a21f-b0e67f041a86', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'10', null, N'0', N'2013-09-04 16:18:15.780', null, N'2013-09-04 16:18:15.780', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'f49ff9c6-b4b2-49a1-b05b-fe8c3570ec7f', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'13', null, N'0', N'2013-09-04 16:18:15.803', null, N'2013-09-04 16:18:15.803', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'f59c1fee-3aff-4fa5-80c0-8a733db78f88', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'20', null, N'0', N'2013-09-04 16:17:05.830', null, N'2013-09-04 16:17:05.830', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'f7a59ac0-f511-4a2e-8045-b177c5df7f19', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'1', null, N'0', N'2013-09-04 16:18:15.720', null, N'2013-09-04 16:18:15.720', null);
GO
INSERT INTO [dbo].[BI_RoleChainInfo] ([Id], [RId], [CId], [Remark], [State], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'fb4bdca5-49be-47fd-9f92-563fe26c3b05', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'32', null, N'0', N'2013-09-04 16:17:05.177', null, N'2013-09-04 16:17:05.177', null);
GO

-- ----------------------------
-- Table structure for [dbo].[BI_RoleInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_RoleInfo]
GO
CREATE TABLE [dbo].[BI_RoleInfo] (
[RId] nvarchar(128) NOT NULL ,
[RName] nvarchar(20) NOT NULL ,
[RDesc] nvarchar(50) NULL ,
[Enabled] bit NOT NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_RoleInfo
-- ----------------------------
INSERT INTO [dbo].[BI_RoleInfo] ([RId], [RName], [RDesc], [Enabled], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'会计', N'算账的嘛', N'1', N'2013-08-27 05:42:48.000', N'201228', N'2013-08-27 05:42:48.000', N'201228');
GO
INSERT INTO [dbo].[BI_RoleInfo] ([RId], [RName], [RDesc], [Enabled], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'秘书', N'负责数据业务', N'1', N'2013-08-21 05:42:42.000', N'201228', N'2013-08-21 05:42:42.000', N'201228');
GO
INSERT INTO [dbo].[BI_RoleInfo] ([RId], [RName], [RDesc], [Enabled], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'管理员', N'全面负责业务', N'1', N'2013-08-21 04:54:12.000', N'201228', N'2013-08-21 04:54:12.000', N'201228');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_UDepartment]
-- ----------------------------
DROP TABLE [dbo].[BI_UDepartment]
GO
CREATE TABLE [dbo].[BI_UDepartment] (
[Id] nvarchar(128) NOT NULL ,
[UId] nvarchar(128) NOT NULL ,
[DeptId] nvarchar(128) NULL ,
[Remark] nvarchar(50) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL ,
[Enabled] bit NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of BI_UDepartment
-- ----------------------------
INSERT INTO [dbo].[BI_UDepartment] ([Id], [UId], [DeptId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'31ecd494-4527-43d2-a447-afdd990a1ecd', N'admin', N'1000', null, N'2013-09-06 10:11:54.237', null, N'2013-09-06 10:11:54.237', null, N'1');
GO
INSERT INTO [dbo].[BI_UDepartment] ([Id], [UId], [DeptId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'6254dc70-610e-43d3-a5e7-43c536c9a8dd', N'管理员', N'1002', null, N'2013-09-06 10:12:11.500', null, N'2013-09-06 10:12:11.500', null, N'1');
GO
INSERT INTO [dbo].[BI_UDepartment] ([Id], [UId], [DeptId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'90b3d44f-0424-4beb-ac50-809fb225c105', N'haha', N'1000', null, N'2013-09-06 10:12:06.353', null, N'2013-09-06 10:12:06.353', null, N'1');
GO
INSERT INTO [dbo].[BI_UDepartment] ([Id], [UId], [DeptId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'd36cdb78-4e6a-4fd5-984e-14cbdbe6573c', N'201228', N'1000', null, N'2013-09-06 10:11:47.650', null, N'2013-09-06 10:11:47.650', null, N'1');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_URoleInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_URoleInfo]
GO
CREATE TABLE [dbo].[BI_URoleInfo] (
[Id] nvarchar(128) NOT NULL ,
[UId] nvarchar(128) NOT NULL ,
[RId] nvarchar(128) NULL ,
[Remark] nvarchar(50) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL ,
[Enabled] bit NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of BI_URoleInfo
-- ----------------------------
INSERT INTO [dbo].[BI_URoleInfo] ([Id], [UId], [RId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'41f055da-e911-4f2c-a1d0-03febf8bb35f', N'201228', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', null, N'2013-09-06 10:11:47.640', null, N'2013-09-06 10:11:47.640', null, N'1');
GO
INSERT INTO [dbo].[BI_URoleInfo] ([Id], [UId], [RId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'62202b54-adf5-4e40-9208-c04440671636', N'admin', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', null, N'2013-09-06 10:11:54.230', null, N'2013-09-06 10:11:54.230', null, N'1');
GO
INSERT INTO [dbo].[BI_URoleInfo] ([Id], [UId], [RId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'af00917b-6b7e-4847-bec2-ca866db85ea3', N'管理员', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', null, N'2013-09-06 10:12:11.370', null, N'2013-09-06 10:12:11.370', null, N'1');
GO
INSERT INTO [dbo].[BI_URoleInfo] ([Id], [UId], [RId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'b38d7a74-866d-4e16-ba21-11641e1d22f6', N'haha', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', null, N'2013-09-06 10:12:06.340', null, N'2013-09-06 10:12:06.340', null, N'1');
GO
INSERT INTO [dbo].[BI_URoleInfo] ([Id], [UId], [RId], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [Enabled]) VALUES (N'dc8d5869-9bf5-47b9-a78d-93e8aa8245d6', N'201228', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', null, N'2013-09-06 10:11:47.620', null, N'2013-09-06 10:11:47.620', null, N'1');
GO

-- ----------------------------
-- Table structure for [dbo].[BI_UserInfo]
-- ----------------------------
DROP TABLE [dbo].[BI_UserInfo]
GO
CREATE TABLE [dbo].[BI_UserInfo] (
[UId] nvarchar(128) NOT NULL ,
[UName] nvarchar(50) NOT NULL ,
[UPwd] nvarchar(70) NOT NULL ,
[RealName] nvarchar(50) NOT NULL ,
[Sex] nvarchar(5) NULL ,
[Character] nvarchar(50) NULL ,
[Tel] nvarchar(20) NULL ,
[Email] nvarchar(20) NULL ,
[QQ] nvarchar(20) NULL ,
[WangWang] nvarchar(20) NULL ,
[CompanyName] nvarchar(50) NULL ,
[CompanyInfo] nvarchar(500) NULL ,
[Bankname] nvarchar(30) NULL ,
[BankAccount] nvarchar(50) NULL ,
[Address] nvarchar(200) NULL ,
[Enabled] bit NOT NULL ,
[Description] nvarchar(200) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL 
)


GO

-- ----------------------------
-- Records of BI_UserInfo
-- ----------------------------
INSERT INTO [dbo].[BI_UserInfo] ([UId], [UName], [UPwd], [RealName], [Sex], [Character], [Tel], [Email], [QQ], [WangWang], [CompanyName], [CompanyInfo], [Bankname], [BankAccount], [Address], [Enabled], [Description], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'201228', N'201228', N'201228', N'杨灿', null, null, N'15212290137', N'534578482@qqcom', N'999999999', null, N'思元软件', N'芜湖分公司', N'中国银行', null, N'芜湖市弋江区高新软件园', N'1', null, N'2013-08-27 02:44:40.000', null, N'2013-09-06 10:11:47.430', null);
GO
INSERT INTO [dbo].[BI_UserInfo] ([UId], [UName], [UPwd], [RealName], [Sex], [Character], [Tel], [Email], [QQ], [WangWang], [CompanyName], [CompanyInfo], [Bankname], [BankAccount], [Address], [Enabled], [Description], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'admin', N'201228', N'201228', N'admin', null, null, N'15212290137', N'534578482@qqcom', N'888888888', null, N'芜湖思元', N'芜湖分公司', N'中国银行', null, N'芜湖市弋江区高新软件园', N'1', null, N'2013-08-27 02:44:40.000', null, N'2013-09-06 10:11:54.203', null);
GO
INSERT INTO [dbo].[BI_UserInfo] ([UId], [UName], [UPwd], [RealName], [Sex], [Character], [Tel], [Email], [QQ], [WangWang], [CompanyName], [CompanyInfo], [Bankname], [BankAccount], [Address], [Enabled], [Description], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'haha', N'123', N'201228', N'55555555', null, null, N'1665865', null, null, null, null, null, null, null, null, N'1', null, N'2013-09-13 10:17:56.363', null, N'2013-09-13 10:17:56.363', null);
GO
INSERT INTO [dbo].[BI_UserInfo] ([UId], [UName], [UPwd], [RealName], [Sex], [Character], [Tel], [Email], [QQ], [WangWang], [CompanyName], [CompanyInfo], [Bankname], [BankAccount], [Address], [Enabled], [Description], [Addate], [Aduser], [Uddate], [Uduser]) VALUES (N'管理员', N'201228', N'201228', N'管理员', null, null, null, null, null, null, null, null, null, null, null, N'1', null, N'2013-08-27 04:03:31.000', null, N'2013-09-06 10:12:10.963', null);
GO

-- ----------------------------
-- Table structure for [dbo].[BI_UserInfoLog]
-- ----------------------------
DROP TABLE [dbo].[BI_UserInfoLog]
GO
CREATE TABLE [dbo].[BI_UserInfoLog] (
[Id] nvarchar(128) NOT NULL ,
[Url] nvarchar(100) NOT NULL ,
[ChainInfoId] nvarchar(128) NULL ,
[RecordId] nvarchar(MAX) NULL ,
[SysUserId] nvarchar(MAX) NULL ,
[EnterpriseId] nvarchar(MAX) NULL ,
[Ip] nvarchar(100) NOT NULL ,
[Remark] nvarchar(50) NULL ,
[Addate] datetime NOT NULL ,
[Aduser] nvarchar(20) NULL ,
[Uddate] datetime NOT NULL ,
[Uduser] nvarchar(20) NULL ,
[UserInfo_UId] nvarchar(128) NULL 
)


GO

-- ----------------------------
-- Records of BI_UserInfoLog
-- ----------------------------
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'001e11f4-c68e-4fc3-ae2f-78d5fe248441', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:11:11.330', N'201228', N'2013-09-23 10:11:11.330', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'00b78973-4217-4ce7-965b-6c7c88927531', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:07.673', N'201228', N'2013-09-04 12:24:07.673', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'00c28cf7-a759-4966-b0d0-edabb528da06', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:44.713', N'201228', N'2013-09-04 12:24:44.713', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0103a637-244b-4078-aaa3-1164d99b8565', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:53:33.160', N'201228', N'2013-09-04 15:53:33.160', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'011ab067-cbca-46cb-a107-a7eb0526b04a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:31:05.943', N'201228', N'2013-09-13 15:31:05.943', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0154f569-a9cc-485e-8db8-968c096e4d81', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:55:09.883', N'201228', N'2013-09-10 17:55:09.883', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'02f29849-0e94-44c6-bb59-c64aa33b2c39', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:00:52.343', N'201228', N'2013-09-13 12:00:52.343', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'02fafcad-8480-41e3-b354-58d817338378', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:07:31.523', N'201228', N'2013-09-13 10:07:31.523', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'03960b28-79a4-42ab-8c6e-4c6a09973760', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:14.243', N'201228', N'2013-09-06 10:19:14.243', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'04714c65-c8fc-4690-b8a9-f086f6fa94c1', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:53:30.773', N'201228', N'2013-09-04 15:53:30.773', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'04e04cd7-698f-42d6-bd4e-81b6f6b9fec0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:36:01.500', N'201228', N'2013-09-13 15:36:01.500', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0558cb50-8253-4f0c-85f4-e433f7b20a28', N'/Admin/SysStatistic', N'56', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:17.407', N'201228', N'2013-09-06 11:40:17.407', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'059435e6-f645-4e42-b339-7a2495dcbd9c', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:09:47.387', N'201228', N'2013-09-04 16:09:47.387', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0647d324-77fe-40b2-8a45-ca15d160c45a', N'/Admin/RoleInfo/Details/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'14', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:07:42.733', N'201228', N'2013-09-13 16:07:42.733', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'06aa171e-7462-4163-927b-2f827a560441', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:18:09.363', N'201228', N'2013-09-06 11:18:09.363', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'071d9dbd-305b-48ab-92c3-42dd3ce3528b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:58:02.393', N'201228', N'2013-09-10 15:58:02.393', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0735dba9-88ab-4f9c-8ab9-9a03039c3b00', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:39:55.920', N'201228', N'2013-09-11 10:39:55.920', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'080c7995-ccc2-4e8f-9888-d8a024e912b3', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:44:30.627', N'201228', N'2013-09-10 17:44:30.627', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'08f86ba5-0fe1-4211-b7ac-e71f2303dfa8', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:40:47.843', N'201228', N'2013-09-11 10:40:47.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'090d9a6c-843a-45dd-baa8-6fb059bed428', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:07:38.100', N'201228', N'2013-09-13 16:07:38.100', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'09927a67-762f-4556-8493-a025393e9cea', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:45:29.120', N'201228', N'2013-09-13 11:45:29.120', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'09ce7f2c-ae82-477f-b3da-d2d4963defe8', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:50:57.430', N'201228', N'2013-09-10 15:50:57.430', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0a1058e5-917c-4e57-822f-ef90dcce883c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:04:00.403', N'201228', N'2013-09-13 12:04:00.403', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0b1456fc-b222-4383-aeed-3f3468778530', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:23:52.700', N'201228', N'2013-09-04 12:23:52.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0b285b7a-7877-4db1-87ad-d8de5de2a85a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:56:54.203', N'201228', N'2013-09-13 11:56:54.203', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0bc30609-c024-443d-8747-b8fd816ab2ef', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:49:15.747', N'201228', N'2013-09-10 15:49:15.747', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0bec54d6-168d-487f-b9cb-ea408bf9ce39', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:44:58.880', N'201228', N'2013-09-13 15:44:58.880', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0caea297-199d-4d8f-a7d6-18f1ba64d3cc', N'/Admin/RoleInfo/Edit/e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'15', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:11.787', N'201228', N'2013-09-04 16:08:11.787', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0cce4ce0-4127-4213-be43-9f27c1af5357', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:24:53.943', N'201228', N'2013-09-10 17:24:53.943', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0d5f63be-85e2-427a-8a86-6b012a265364', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:39:59.710', N'201228', N'2013-09-06 11:39:59.710', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0de11c7d-97f3-4bb2-a8c2-423f83f53ce0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:26:07.623', N'201228', N'2013-09-11 10:26:07.623', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0e5b710e-01ec-41d2-bb05-145f99db2525', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:09.693', N'201228', N'2013-09-04 15:31:09.693', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0e7aebbd-adce-41d9-a87b-1bb4da9b43f9', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:26:48.407', N'201228', N'2013-09-13 15:26:48.407', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0edb6fec-d5e2-4d6e-ac5c-875d75d20358', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:16.843', N'201228', N'2013-09-04 16:07:16.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0f65a375-8bae-4f31-b761-acfc2ee516eb', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:04.693', N'201228', N'2013-09-04 16:08:04.693', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0f7d96a4-4a8b-45e2-aa26-30daed725313', N'/Admin/RoleInfo/Edit/e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'15', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:33:44.787', N'201228', N'2013-09-11 10:33:44.787', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'0ff81a13-9240-4443-b6b8-a95876a9512f', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:16.277', N'201228', N'2013-09-04 16:18:16.277', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'114e23ec-3c7d-4427-9943-b69a5fc83fc1', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:04.487', N'201228', N'2013-09-11 10:34:04.487', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'114eaec5-2ea6-42d1-9220-7f545825ed39', N'/Admin/Department/Edit', N'25', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:16.673', N'201228', N'2013-09-04 16:19:16.673', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'11927e93-fb49-44d9-984b-2a2e2b5e409e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:56:52.777', N'201228', N'2013-09-13 15:56:52.777', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'119c191e-5eac-4d24-b715-31043d837c77', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:07.640', N'201228', N'2013-09-04 16:19:07.640', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'11aecc79-35ec-4afc-b9c7-5d64337e5d7a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:42:36.857', N'201228', N'2013-09-10 17:42:36.857', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1285c600-24d2-449d-a6b5-b87abc181e59', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:37.813', N'201228', N'2013-09-04 16:07:37.813', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'12a54391-52e0-4381-a75b-7c14e34211ba', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:25:32.200', N'201228', N'2013-09-10 16:25:32.200', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'12f7d8af-d737-410d-a8f1-6c1ac06a0054', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:04:48.900', N'201228', N'2013-09-10 16:04:48.900', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1329e835-1c1a-4e7f-8b58-a37502296598', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:07:51.580', N'201228', N'2013-09-13 12:07:51.580', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1443be5f-4007-431f-b09c-75e603e403f2', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:45.257', N'201228', N'2013-09-04 16:09:45.257', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'14fe23a5-38d1-4b7a-98bd-0fb2328d8642', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:30:21.273', N'201228', N'2013-09-13 15:30:21.273', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'15036e1b-40bf-4657-a314-4da7d7bb2125', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:31.647', N'201228', N'2013-09-04 16:07:31.647', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1568e436-9df7-4b22-a41e-5f3582f70e87', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:56.310', N'201228', N'2013-09-04 16:17:56.310', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1596d34e-fdc2-4f67-94fb-a0d2b94d21fa', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:01:30.147', N'201228', N'2013-09-04 16:01:30.147', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'159c3bbb-8b64-4ce4-abe7-9958f4ba9dbd', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:40:43.480', N'201228', N'2013-09-13 15:40:43.480', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'17337fa9-706a-4271-bf04-e753a063b40a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:03:37.783', N'201228', N'2013-09-10 16:03:37.783', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'17a35b09-9145-41c7-9d3d-fdde3188bf9f', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:16.610', N'201228', N'2013-09-04 16:07:16.610', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'18341254-17a4-4edb-a0f5-e0677d0afb8a', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:13:31.853', N'201228', N'2013-09-04 16:13:31.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'183f0043-5fd6-4538-abc1-e0608eb2901f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:25:04.987', N'201228', N'2013-09-13 15:25:04.987', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'18a110d1-5799-4ce6-865a-c6ed1b7b0a8a', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:23:54.900', N'201228', N'2013-09-04 12:23:54.900', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'18b0eee2-aaa6-44d2-a1f9-e093971ed24b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:58:02.473', N'201228', N'2013-09-10 15:58:02.473', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'191ed1a5-826a-441c-b919-5427347594d9', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:22.377', N'201228', N'2013-09-06 10:19:22.377', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'194d3fce-25d6-4060-bb95-fe5de2229250', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:29:46.890', N'201228', N'2013-09-11 10:29:46.890', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1a3983e3-9fc9-4da4-a41a-13413f06cb3a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:38:32.923', N'201228', N'2013-09-10 17:38:32.923', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1ace6a0d-16ef-469a-9ce7-ed6b32f01a93', N'/Admin/UserInfo/Edit/%E7%AE%A1%E7%90%86%E5%91%98', N'10', N'管理员', N'201228', N'True', N'::1', null, N'2013-09-06 10:12:10.960', N'201228', N'2013-09-06 10:12:10.960', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1b78f0e2-e486-4b5a-b1f3-f5f3b3de3385', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:16:52.970', N'201228', N'2013-09-04 16:16:52.970', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1bbaf91c-b64d-4182-aa59-e0335089edb9', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:33.943', N'201228', N'2013-09-04 16:18:33.943', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1bd7d12f-aecb-473a-a9b5-1723a463ac11', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:15.440', N'201228', N'2013-09-04 16:05:15.440', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1c9bd51c-cc62-4611-940f-f0ce3d92987c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:22:56.873', N'201228', N'2013-09-10 17:22:56.873', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1cd9218f-c8ee-4d5d-97a0-41004f02882b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:06:40.820', N'201228', N'2013-09-10 14:06:40.820', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1d9441ee-002d-4294-bf6e-ba4c8c4488a2', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:07.830', N'201228', N'2013-09-04 12:24:07.830', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1ddbc702-cbe1-4e73-b346-400deaf820e2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:51:40.493', N'201228', N'2013-09-13 15:51:40.493', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1e9203dd-3af1-4bcb-8ad5-79af65fb5336', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:26.000', N'201228', N'2013-09-04 12:24:26.000', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1f03c558-2b43-4d46-8697-5ffec9db3fbc', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:09:20.873', N'201228', N'2013-09-04 16:09:20.873', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1f38d017-801e-49c0-87e1-2f2a039b7468', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:12.707', N'201228', N'2013-09-04 16:05:12.707', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1fca1e9c-9285-44d5-adbe-ac6b703e6d3e', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:43.530', N'201228', N'2013-09-04 16:07:43.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'1fe92c00-21ad-4240-970a-c68d00d53c9c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:38:37.613', N'201228', N'2013-09-10 14:38:37.613', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'203910fa-9a49-42d1-9115-9c3cb7afe312', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:05.457', N'201228', N'2013-09-11 10:34:05.457', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2065a142-49ac-49e9-b1af-94520dcd2291', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:29.997', N'201228', N'2013-09-13 16:12:29.997', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'20d80d99-60d0-4637-b37c-4d00f7a21348', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 15:51:46.990', N'201228', N'2013-09-04 15:51:46.990', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'23d52f64-43b1-4308-8b5e-ecb627e535cb', N'/Admin/WebConfigAppSetting', N'36', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:32.943', N'201228', N'2013-09-04 16:09:32.943', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'23e41382-e784-4f6e-8f9a-8191eaa55f08', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:59:35.350', N'201228', N'2013-09-10 17:59:35.350', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2490bebb-6def-4adb-a17a-2a24783ddd0f', N'/Admin/RoleInfo/Edit/e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'15', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:49.590', N'201228', N'2013-09-04 16:10:49.590', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'24b7320e-48e2-488b-9934-2bf8039d0679', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:46:06.703', N'201228', N'2013-09-10 17:46:06.703', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'24f4fee5-dcb1-43e1-9bf2-17a55353c78d', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:30:01.723', N'201228', N'2013-09-11 10:30:01.723', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'25f0f1e2-87bd-47b3-be67-42adfd8b628c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:04:21.700', N'201228', N'2013-09-10 14:04:21.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'262ca3fd-f082-4d83-b520-3840fa251ce9', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:46.847', N'201228', N'2013-09-04 16:05:46.847', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'26d271b0-0b1e-4f79-9fd9-a726c14c8343', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:43.843', N'201228', N'2013-09-04 16:11:43.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'26e701c0-7223-4484-b121-98ea5515aafc', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:28.843', N'201228', N'2013-09-04 16:11:28.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'26f315d6-c6ec-4674-9554-7e081af54096', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:56.187', N'201228', N'2013-09-04 16:10:56.187', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'27568bc1-4150-435c-b788-e9288de03cc0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:49:54.517', N'201228', N'2013-09-13 11:49:54.517', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'27645d49-ab57-4f6e-bf81-81dc4c715333', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:52:31.917', N'201228', N'2013-09-10 16:52:31.917', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'279ccdc6-cd25-43b2-bc1c-05bbacbbf64f', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:11:00.850', N'201228', N'2013-09-23 10:11:00.850', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'27c284a1-3c69-48f1-b4fb-5e7ebce61c4c', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:25:49.770', N'201228', N'2013-09-06 11:25:49.770', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'280fa5ac-1ac8-4a66-882f-b2f333c86000', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:17:08.723', N'201228', N'2013-09-04 16:17:08.723', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'28af1d41-b615-4e09-a090-de5332bddde3', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:50:49.900', N'201228', N'2013-09-04 15:50:49.900', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'28b8799a-d980-4d5f-a8f8-6b3d35e2adac', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:04.373', N'201228', N'2013-09-04 16:09:04.373', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'28f602c5-29b5-45a2-9a32-b34f219a8bb6', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:52:12.623', N'201228', N'2013-09-10 15:52:12.623', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'29b500de-6cf3-4367-8acf-48fdaa5c52a6', N'/Admin/UserInfo/Edit/admin', N'10', N'admin', N'201228', N'True', N'::1', null, N'2013-09-06 10:11:50.290', N'201228', N'2013-09-06 10:11:50.290', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2a32239c-c343-4b4f-b297-f7d7d3ab5ffd', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:21:36.537', N'201228', N'2013-09-04 16:21:36.537', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2bb508ea-1841-4174-a6b3-82b7dc73086f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:04:21.200', N'201228', N'2013-09-10 14:04:21.200', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2c76daed-7d47-4513-a6aa-3083c2b32d2c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:03:40.690', N'201228', N'2013-09-13 12:03:40.690', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2cc58e69-ca5a-4c7a-81e7-9b699be0fb71', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:37:56.000', N'201228', N'2013-09-10 17:37:56.000', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2cf8f211-8222-4044-bf74-cb0ac7cf0715', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:32.270', N'201228', N'2013-09-04 15:31:32.270', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2da84358-163f-4138-be97-1581af71978c', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:17:58.290', N'201228', N'2013-09-06 11:17:58.290', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2e30283e-7488-4140-8a75-91d35191bf63', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:01:37.673', N'201228', N'2013-09-10 14:01:37.673', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2e998c54-c7ac-4c30-8e84-c84fa5b6735b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:29:25.963', N'201228', N'2013-09-13 15:29:25.963', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2ef6ede4-24e3-4456-a3cf-264f5ca52cfb', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:35.397', N'201228', N'2013-09-04 12:24:35.397', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'2fcd2b4f-703d-42b6-adf2-f023684f04b4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:31:11.853', N'201228', N'2013-09-13 15:31:11.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'30089c88-0fa9-4cea-b62c-ef5f45bc87b7', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:04:46.313', N'201228', N'2013-09-10 16:04:46.313', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'30805319-5d69-4eba-9bc3-e5ac50b5e184', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:50:56.853', N'201228', N'2013-09-10 15:50:56.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3094e465-3a96-488f-bbc6-267b0f39df06', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:21.700', N'201228', N'2013-09-04 12:24:21.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'30caad3b-c998-44bf-8e3c-fd1f2106c6bc', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:56:11.123', N'201228', N'2013-09-04 15:56:11.123', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'312ab51d-ed5a-4d5d-84ba-fd06b0991344', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:11:27.210', N'201228', N'2013-09-06 10:11:27.210', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'31588b5f-5b79-4ce4-b13b-bf64b9adde1f', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:02.303', N'201228', N'2013-09-04 15:31:02.303', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'31e11ba5-e7e1-4d69-996e-146d462ca134', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:18:35.830', N'201228', N'2013-09-04 16:18:35.830', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'32087f5a-77b7-47f9-9125-552cf38ddfa8', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:05.600', N'201228', N'2013-09-06 10:19:05.600', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'32361242-c935-4e90-8f19-003d7438240d', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:38.493', N'201228', N'2013-09-04 16:19:38.493', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'32af2d66-8193-4315-b1a9-d17308a32a1a', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:34.370', N'201228', N'2013-09-04 16:07:34.370', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'333d8552-b55f-46c7-af38-941477e5884e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:11:32.133', N'201228', N'2013-09-10 14:11:32.133', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'343a7b5a-d874-418f-a600-86ad171f2a5c', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:31.977', N'201228', N'2013-09-13 16:12:31.977', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'34708333-1f35-40be-8adf-d9a3321588fb', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-06 10:11:35.203', N'201228', N'2013-09-06 10:11:35.203', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'354c1ad7-60ff-4943-8640-cc9f83ca9499', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:39.787', N'201228', N'2013-09-04 12:24:39.787', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'35813306-9244-43f7-87ad-f6c0bd151204', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:10:25.920', N'201228', N'2013-09-13 10:10:25.920', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'36a09a71-ac30-458a-b74c-8d3d8884574d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:13:10.887', N'201228', N'2013-09-10 17:13:10.887', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'36b25f66-4af2-4953-8960-faa93b8bd0e4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:11.287', N'201228', N'2013-09-13 16:12:11.287', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'36e94d62-520a-4b42-a0f9-f9e28bb91b73', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:10:22.503', N'201228', N'2013-09-23 10:10:22.503', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'37480950-ab7e-4f08-b16e-d368c2af56ad', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:30.200', N'201228', N'2013-09-13 16:12:30.200', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'398765d7-0c4c-4295-9df4-f7c9d8e47dd8', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:57:37.403', N'201228', N'2013-09-04 15:57:37.403', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'398e55e5-de3d-45bd-8772-9bbcaf96daa1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 09:07:36.457', N'201228', N'2013-09-13 09:07:36.457', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3c1dd548-b07f-4530-8e8d-cb2f996e3651', N'/Admin/Department/Edit/1000', N'25', N'1000', N'201228', N'True', N'::1', null, N'2013-09-04 16:01:11.663', N'201228', N'2013-09-04 16:01:11.663', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3c64aa36-11e3-45d0-9e57-0b1bbdd26e67', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:59:33.570', N'201228', N'2013-09-10 17:59:33.570', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3d5ba1ad-6590-4e6a-811f-5b9513f1a5b4', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:42.963', N'201228', N'2013-09-04 16:10:42.963', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3d8c38d5-8e13-43a0-8673-742d4396b647', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:50:52.337', N'201228', N'2013-09-04 15:50:52.337', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3e5f0164-1a21-47a1-8bd4-2e1d00defd5f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:54:28.207', N'201228', N'2013-09-10 16:54:28.207', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3f6f7d76-a832-4c6e-aa28-0a42481d54e9', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:47.487', N'201228', N'2013-09-04 16:07:47.487', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'3ff71693-0ef5-4577-ad2c-a2d2fe941cce', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:00:06.990', N'201228', N'2013-09-13 12:00:06.990', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'40f65f6f-dc50-4809-8fb2-19260c85dfa2', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:39:54.370', N'201228', N'2013-09-11 10:39:54.370', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'411add5f-1775-4764-a7ed-d033082bbcd4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:24:51.990', N'201228', N'2013-09-10 17:24:51.990', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'41880dc4-dffb-4edd-aa54-5b268cb14f9e', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:48.723', N'201228', N'2013-09-10 11:45:48.723', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'42a1e5d6-67e1-448d-8015-d795d5d26898', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:16:31.400', N'201228', N'2013-09-13 10:16:31.430', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'43215577-f881-4d66-9892-7fb0f3ff8b53', N'/Admin/Department/Edit/1000', N'25', N'1000', N'201228', N'True', N'::1', null, N'2013-09-06 11:25:52.127', N'201228', N'2013-09-06 11:25:52.127', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'43fda182-f96e-4c2a-8e35-fb0b849a9a5f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:13:31.637', N'201228', N'2013-09-13 10:13:31.637', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'44878c79-047c-4601-a3d4-a13620f13f47', N'/Admin/WebConfigAppSetting', N'36', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:20.277', N'201228', N'2013-09-06 11:40:20.277', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'44a05476-3286-41b2-934f-3319478a9ebe', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:28.850', N'201228', N'2013-09-04 16:18:28.850', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'458331b9-7e4c-4bfa-84fa-b64e44150fa8', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:42.620', N'201228', N'2013-09-04 16:05:42.620', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'468f2090-67d3-472e-9e8f-a68a7b1ba63f', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:35:11.987', N'201228', N'2013-09-11 10:35:11.987', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'46a9851a-c8b1-4133-b4e6-010c37cb9cf1', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:42.700', N'201228', N'2013-09-04 16:08:42.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'46cfcc38-aa40-4556-8038-7777e0d59f34', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:30:09.637', N'201228', N'2013-09-13 15:30:09.637', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4719bbb9-25c4-43ea-8457-59621376365b', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:39.223', N'201228', N'2013-09-04 16:19:39.223', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'47bf19d8-0c3a-4220-af77-44adf4e4a362', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:05:48.500', N'201228', N'2013-09-04 16:05:48.500', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'48d1e3aa-d98e-478a-8b5b-18048ca447cf', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:15.520', N'201228', N'2013-09-04 16:10:15.520', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4977dfeb-2847-4a41-9e88-6e8fef58c2a5', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:07:14.193', N'201228', N'2013-09-13 12:07:14.193', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'49a2bc42-1f5b-4a8f-81c6-448d7764fbd0', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:37.353', N'201228', N'2013-09-04 16:08:37.353', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'49b56c5d-b8cd-4c48-a6ca-a82540f975b6', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:11.310', N'201228', N'2013-09-04 16:10:11.310', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'49c8056e-efac-48cb-8ddf-b716d269f43e', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:37.100', N'201228', N'2013-09-04 16:11:37.100', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4aecf76d-1cdc-4df1-8d90-6078084511f3', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:19.230', N'201228', N'2013-09-06 10:19:19.230', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4af0d073-85b0-48de-a2d3-8389ea438e5d', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:39:31.397', N'201228', N'2013-09-11 10:39:31.397', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4b097f4d-b777-4559-b479-57fcf331c49d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:23:59.963', N'201228', N'2013-09-10 17:23:59.963', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4b1f5f8a-9124-49f8-a11b-26d89003ab6c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:28:19.487', N'201228', N'2013-09-13 15:28:19.487', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4b2aa58b-707e-4651-a764-ec1277aadbf3', N'/Admin/RoleInfo/Edit/ee6b1601-4579-43d4-b629-74c2f6e27a49', N'15', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'201228', N'True', N'::1', null, N'2013-09-04 16:17:04.683', N'201228', N'2013-09-04 16:17:04.683', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4b9ba8ba-915d-4ec0-bbe3-44cc135df191', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:09:54.370', N'201228', N'2013-09-04 16:09:54.370', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4bcd45b6-1936-4c2f-8a16-b352b5d6f721', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:18.640', N'201228', N'2013-09-04 16:09:18.640', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4bcfe59c-9d28-4443-b438-b6f50f284d42', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:19:18.970', N'201228', N'2013-09-13 10:19:18.970', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4c990ec8-2e9f-411d-a0d2-0a90f3978367', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:17.997', N'201228', N'2013-09-04 16:18:17.997', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4cf445ea-1146-4a8f-b9f1-53a5d01c0b50', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:03:30.920', N'201228', N'2013-09-10 17:03:30.920', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'4f04dea8-0609-4599-8ca3-0123caa59ade', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:19.200', N'201228', N'2013-09-04 16:11:19.200', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'50a24e86-ced9-4cc7-b224-9cc9f02d26b8', N'/Admin/SysHelp/Index', N'41', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:52.400', N'201228', N'2013-09-11 10:32:52.400', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'50eb0048-4210-4598-ac96-6759936a7dda', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:39:40.533', N'201228', N'2013-09-13 15:39:40.533', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5124ee91-a28e-406c-9d55-c71328072650', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:53.143', N'201228', N'2013-09-04 15:31:53.143', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5165015e-5cbb-4789-88ec-9c7563bcecbf', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:10:55.870', N'201228', N'2013-09-13 10:10:55.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'52b80c8f-9638-474f-8b5a-580e25f19546', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:13:08.770', N'201228', N'2013-09-04 16:13:08.770', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'53019972-d359-4a39-b715-df9ddd4b78bf', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:29.637', N'201228', N'2013-09-04 16:09:29.637', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5308f0e4-00dd-40a4-b332-1832c81ee558', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:06:08.930', N'201228', N'2013-09-13 10:06:08.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'536e55c6-60c4-43db-9ed4-7fd9e7d36853', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:25:41.607', N'201228', N'2013-09-10 17:25:41.607', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'54ac79ae-4c26-438d-960f-0f1050d38d09', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:06:06.237', N'201228', N'2013-09-04 16:06:06.237', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'555bbe8f-4d49-44a8-a6fc-c59286da7f9c', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:31:47.983', N'201228', N'2013-09-11 10:31:47.983', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'559e7698-1c2a-4710-aa36-abd836e55896', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:53:16.513', N'201228', N'2013-09-13 15:53:16.513', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'562f2f86-420b-4466-96bc-72ccbbf5c9a3', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:21:30.413', N'201228', N'2013-09-04 16:21:30.413', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'57e745cb-2af1-4c29-8598-d27339b17db3', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:46:04.437', N'201228', N'2013-09-10 11:46:04.437', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'58399794-22f1-4785-955d-1795346b2762', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:01:07.423', N'201228', N'2013-09-04 16:01:07.423', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5897a6d0-1fe1-427a-ab5a-c056d68761d3', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:22:04.247', N'201228', N'2013-09-10 17:22:04.247', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'590b3b65-99c6-4e1e-8435-9e327572ad0c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:37:21.290', N'201228', N'2013-09-10 16:37:21.290', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5994eaaa-9449-4751-b71b-d9feb18a4bbd', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:50:09.320', N'201228', N'2013-09-13 11:50:09.320', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'59a99a71-f1ce-4a68-b8a7-ccb80f80fbe0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:06:40.930', N'201228', N'2013-09-10 14:06:40.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'59babb32-2df1-4a24-8485-6982abc9a6f1', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:17.557', N'201228', N'2013-09-06 10:19:17.557', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'59c9b9cb-0de1-4dd8-a915-583ec439ace2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:26:01.400', N'201228', N'2013-09-11 10:26:01.400', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'59dcc7bc-d496-4938-a9ab-ac9af0213458', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:43.600', N'201228', N'2013-09-06 11:40:43.600', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5aa1b330-660a-42dc-bee0-c39f62563a2f', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:40.690', N'201228', N'2013-09-04 16:09:40.690', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5ac19dd0-f915-48b2-a002-2a0a73d13803', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:13:08.810', N'201228', N'2013-09-04 16:13:08.810', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5b0ea76e-0b6a-423d-a38f-2f3a3555bbfe', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:16:48.420', N'201228', N'2013-09-04 16:16:48.420', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5b2f0a75-6791-4e5c-b337-790b1c5a3adb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:56:29.580', N'201228', N'2013-09-13 15:56:29.580', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5b9a5f87-59fe-4606-a19b-169bfd18adbc', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:08:12.727', N'201228', N'2013-09-13 10:08:12.727', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5bbe506f-b3ac-47aa-be4c-a983ab124f09', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:35:12.807', N'201228', N'2013-09-11 10:35:12.807', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5dd7ace0-074c-46fc-b364-7831e1f51b56', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:40.270', N'201228', N'2013-09-04 16:05:40.270', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5e058465-29a6-47d7-8085-a04381e92c75', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:31:45.507', N'201228', N'2013-09-11 10:31:45.507', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5e1e8ab0-cbf3-43de-9c85-21107dca905a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:26:16.550', N'201228', N'2013-09-10 17:26:16.550', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5ea7a1e5-862e-40ff-b1bc-56a9dacf01ee', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:04.487', N'201228', N'2013-09-04 15:31:04.487', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5eacc359-32b4-4cec-96ed-6b3bf852eef4', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:31:54.273', N'201228', N'2013-09-11 10:31:54.273', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5f7712a9-8cb5-4646-af36-b68a53078628', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:08.537', N'201228', N'2013-09-04 16:09:08.537', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'5fb4c67d-7959-414b-8d7d-0c690701d98d', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:23.330', N'201228', N'2013-09-04 16:18:23.330', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6017b683-fd45-4bf6-b60b-7dcc62e3632e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:02:19.137', N'201228', N'2013-09-10 17:02:19.137', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'60436574-2748-4ce9-a9b0-f1b3733eb926', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:59:12.733', N'201228', N'2013-09-13 15:59:12.733', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'60679218-00dd-4f65-b82c-f64425cd1050', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:47:39.830', N'201228', N'2013-09-13 11:47:39.830', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'60b9ee8f-7e31-432d-a89a-ee0e04a29e4a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:52:16.163', N'201228', N'2013-09-13 11:52:16.163', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'61102a82-bf22-46e4-9542-b840b515c7c7', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:44:44.100', N'201228', N'2013-09-10 15:44:44.100', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'612f3f18-66d6-44a9-a997-43363da884be', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:27:57.893', N'201228', N'2013-09-13 15:27:57.893', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'614f1896-4c55-4512-af5f-288718bec702', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:18:18.723', N'201228', N'2013-09-06 11:18:18.723', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'615c2916-7454-456d-a2b9-dc9ce9066c14', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:18:22.903', N'201228', N'2013-09-04 16:18:22.903', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'615dd867-aea5-4805-9b4d-c38ac5fed3a1', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:11:18.563', N'201228', N'2013-09-04 16:11:18.563', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'617814df-42fb-4838-bb5d-d8360eaf137f', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:11:47.680', N'201228', N'2013-09-06 10:11:47.680', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'619d2eff-298d-44ba-9829-ac26130fca04', N'/Admin/SysHelp', N'41', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:21.753', N'201228', N'2013-09-06 11:40:21.753', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'62a323ee-10d6-414a-92ed-a6678128f92e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:49:22.703', N'201228', N'2013-09-13 11:49:22.703', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'630a6fb2-7afa-4161-8a77-b1aaef7df7d9', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:40:23.763', N'201228', N'2013-09-10 14:40:23.763', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'63673c35-54f4-42f5-b8d5-672dea0498a9', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:09.963', N'201228', N'2013-09-06 10:19:09.963', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'63a8aa5d-344f-4da7-9525-bfb6fd6f76ee', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:35:17.967', N'201228', N'2013-09-11 10:35:17.967', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'63c7a79e-718b-44a5-a460-14cd9791320c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:37:00.117', N'201228', N'2013-09-13 15:37:00.117', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'64c4482f-ac48-45e8-b160-de7e38864cba', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:07:43.217', N'201228', N'2013-09-10 17:07:43.217', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'64ef53c6-4ec0-4d7a-9a7f-22e0f57bd520', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:01.600', N'201228', N'2013-09-04 12:24:01.600', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'65129e1a-fe3b-4028-88af-0a9266b685cb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:41:49.777', N'201228', N'2013-09-10 17:41:49.777', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6544a596-3575-4c08-8c80-425be3931a7a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:48:39.227', N'201228', N'2013-09-10 17:48:39.227', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'66eb6b62-f570-4f87-bf1a-29fac5a4c6ec', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:36.147', N'201228', N'2013-09-04 16:08:36.147', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'672319b6-d12e-4105-b954-414b01d53b2d', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:29.387', N'201228', N'2013-09-04 16:19:29.387', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6733e221-b766-47d2-a2d5-a83a7a46cf81', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:33:10.833', N'201228', N'2013-09-11 10:33:10.833', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'67464357-7ede-47ec-8713-e55ace100354', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:05:10.113', N'201228', N'2013-09-13 12:05:10.113', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'679dbc5d-b8b3-43ea-82e4-6c2de7c2989f', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:06:57.570', N'201228', N'2013-09-04 16:06:57.570', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'67e0b39e-c168-4fc1-bdf6-63f082b60067', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:49:56.787', N'201228', N'2013-09-10 17:49:56.787', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6892b4e9-7ab4-4320-aaa9-032966e4d6ec', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:06:55.320', N'201228', N'2013-09-04 16:06:55.320', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6925cdf0-afe4-455b-b50f-fde9126a12fd', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:31:52.950', N'201228', N'2013-09-10 17:31:52.950', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'693d82fd-a3de-47d9-9860-dde738ef0f45', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:13:33.670', N'201228', N'2013-09-04 16:13:33.670', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'69f169e2-4676-437b-8630-355de09f5476', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:52.110', N'201228', N'2013-09-11 10:34:52.110', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6a2328b6-676d-448e-bd90-8ab8c359e0cd', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:50:47.270', N'201228', N'2013-09-13 15:50:47.270', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6a86fd67-39bc-4cf0-b611-43711a97d491', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:35.457', N'201228', N'2013-09-04 16:07:35.457', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6aac012b-bf9b-4707-992f-4a261d4951fd', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:24:44.853', N'201228', N'2013-09-04 12:24:44.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6acb9497-b6fa-4da5-a73c-adb283e97269', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:16:30.423', N'201228', N'2013-09-13 15:16:30.423', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6b5796e4-45a3-4485-b2a8-91f0dae7ffb4', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:26.680', N'201228', N'2013-09-04 16:08:26.680', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6b645d67-886a-4368-ae26-ba3081d88e6e', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:29:55.543', N'201228', N'2013-09-11 10:29:55.543', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6c4eeca6-fbc5-4f08-aaa0-5918d6fc30de', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:53:32.203', N'201228', N'2013-09-10 15:53:32.203', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6d0d81e3-fefd-4c83-90fc-cfa67b09a616', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:10:53.253', N'201228', N'2013-09-10 17:10:53.253', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6d323524-537a-4776-86e0-3e73af86d654', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:07:58.377', N'201228', N'2013-09-10 17:07:58.377', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6de1a42f-75c8-4968-aabe-d1eb18bc579c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:00:31.450', N'201228', N'2013-09-13 12:00:31.450', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6defa8d9-2bec-41b2-82f7-c4be293342eb', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:12:46.940', N'201228', N'2013-09-06 10:12:46.940', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6e2a2e52-321b-49cd-a1a0-840e1fbe0266', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:53:58.223', N'201228', N'2013-09-10 16:53:58.223', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6f0114a6-795d-47ee-8982-fda2f6c382c2', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:29.670', N'201228', N'2013-09-04 16:18:29.670', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6f9e812e-b69d-4abd-8d14-dd90a8230101', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:51:09.333', N'201228', N'2013-09-04 15:51:09.333', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'6fd86ee2-d6ea-48dd-b42b-c6b87ea6cd5a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:40:33.653', N'201228', N'2013-09-10 17:40:33.653', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'70614bf4-b1bb-4757-8dc1-be8688921de6', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:50.203', N'201228', N'2013-09-11 10:34:50.203', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'70e2d220-f754-4843-91bb-fd9ec0448789', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:12:14.930', N'201228', N'2013-09-04 16:12:14.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'70e46155-66c1-444f-8511-8317e122e114', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:38:42.230', N'201228', N'2013-09-10 17:38:42.230', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'71716958-a13d-48d3-87d7-5166c3af9233', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:57.913', N'201228', N'2013-09-04 16:10:57.913', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'72a7ac38-1bc1-457c-88e5-ae9e28fc1341', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:11:41.880', N'201228', N'2013-09-04 16:11:41.880', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7336d8b0-4919-47cb-baec-2fed14bad8d5', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-06 10:11:47.423', N'201228', N'2013-09-06 10:11:47.423', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'740636f8-31e2-49bf-b006-a3bfc75b4e4f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:53:32.870', N'201228', N'2013-09-10 15:53:32.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'74d2a4ff-9b03-4e86-90fe-01666aef6469', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:58:27.660', N'201228', N'2013-09-10 17:58:27.660', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'75097840-0cfd-4c0c-b032-f5ea19d0d3b1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:14:27.433', N'201228', N'2013-09-13 13:14:27.433', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7568f870-de1c-4b29-ac9f-d5eb25928b23', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:21:39.733', N'201228', N'2013-09-04 16:21:39.733', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'761f4335-4749-4617-9342-722046d688a6', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:56.480', N'201228', N'2013-09-11 10:32:56.480', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'76b0fa24-9f0f-426b-af92-c4a0feef780c', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:01:00.800', N'201228', N'2013-09-04 16:01:00.800', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7826851a-0ec9-4a99-8e23-c04cfa610f57', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:03:14.893', N'201228', N'2013-09-13 12:03:14.897', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'783e4551-f281-4868-bbd1-2a61e0dde995', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:57:48.903', N'201228', N'2013-09-10 15:57:48.903', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'78700023-b485-4b86-b45e-09e75d4f3806', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:42.263', N'201228', N'2013-09-04 16:09:42.263', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'794ec8b7-c8ce-46a4-a7f4-e9e90925ad15', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:10:03.520', N'201228', N'2013-09-13 10:10:03.520', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'795b4bdb-7d0d-4c77-904c-10ddc3be1a16', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:02:09.573', N'201228', N'2013-09-04 16:02:09.573', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'79796b20-949e-43ff-ab99-505ac5ab5651', N'/Admin/UserInfo/Details/201228', N'9', N'201228', N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:33:52.403', N'201228', N'2013-09-11 10:33:52.403', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7a8e233d-0b49-44f7-857f-f40037d3506d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:44:43.277', N'201228', N'2013-09-10 15:44:43.277', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7b1cba52-5ca0-4533-b079-a4caec34b3d5', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:39:58.453', N'201228', N'2013-09-11 10:39:58.453', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7bcfbf7b-10dd-415b-b28f-0b511b51c182', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:12:31.907', N'201228', N'2013-09-13 10:12:31.907', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7c8ac946-96e4-4258-9c3b-00d5449573fd', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:59:00.793', N'201228', N'2013-09-04 15:59:00.793', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7c9d3cd0-9137-4e98-9a9d-a729fbadf62b', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:59.117', N'201228', N'2013-09-11 10:32:59.117', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7d8f6a24-89d3-43d7-8ff6-57b9eb56ba2e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:43:42.810', N'201228', N'2013-09-10 14:43:42.810', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7ed125de-da27-4890-94ea-66b6dfa72985', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:41:51.180', N'201228', N'2013-09-10 17:41:51.180', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7fc8db3c-7db1-4de0-bb66-7fb16a22caa0', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:16:43.947', N'201228', N'2013-09-04 16:16:43.947', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'7fe24192-a036-4611-9379-88da590ed9cb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:45:10.800', N'201228', N'2013-09-10 17:45:10.800', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8089af7c-babe-4e12-9b9d-c59c30b0d149', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:13:43.170', N'201228', N'2013-09-13 10:13:43.170', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'80ac41f8-d271-495a-999a-c0ea72f950ae', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:15.433', N'201228', N'2013-09-06 10:19:15.433', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'80ad090d-5c1b-4a34-a94e-80f6fdecfd21', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:30:03.573', N'201228', N'2013-09-11 10:30:03.573', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'80e9831f-7ee7-4ed6-bc38-f6d123b6d9d8', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:48:37.843', N'201228', N'2013-09-13 15:48:37.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'80f292db-c058-4133-9da3-b40c8e0c4a3e', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:12.397', N'201228', N'2013-09-11 10:34:12.397', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'80fa6ea8-d0b7-47b4-b0da-7cb6e4628349', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:41:05.180', N'201228', N'2013-09-11 10:41:05.180', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'811b43d7-2aea-45d2-ba73-5b5fd6cf61df', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:25.793', N'201228', N'2013-09-04 16:07:25.793', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'81b62ffa-5754-4899-9259-a3a5cff10b80', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:44.840', N'201228', N'2013-09-04 16:10:44.840', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'826949a3-38a4-4b8d-9b22-2f3861da6e63', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:12:07.627', N'201228', N'2013-09-04 16:12:07.627', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8356e811-3c1d-4a66-b3ae-2a6aad166861', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 18:03:13.747', N'201228', N'2013-09-10 18:03:13.747', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'83751364-9533-4a98-8f10-650cd368d68d', N'/Admin/SysHelp/Details/1', N'44', N'1', N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:00.547', N'201228', N'2013-09-11 10:34:00.547', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'83e7bd4a-859e-4ac6-9b71-0970c775a216', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:39:20.480', N'201228', N'2013-09-10 14:39:20.480', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'84d65e4b-8bae-4a61-b76e-efcd4ec6e333', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:01:36.997', N'201228', N'2013-09-10 14:01:36.997', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'84f35b1d-5792-4660-947d-c2f667abb73a', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:17:43.467', N'201228', N'2013-09-06 11:17:43.467', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'84fd6afc-70c0-472f-9a02-f6b7bf15c725', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:24.630', N'201228', N'2013-09-04 16:18:24.630', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'85233aab-59f1-4980-ac6f-06b238ff2ec3', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:00.747', N'201228', N'2013-09-04 16:07:00.747', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8677e5bf-c5c4-421f-ba98-a1d1392cd6b1', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:40.670', N'201228', N'2013-09-04 16:19:40.670', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'86a87008-73a2-407c-967e-968408dc1d17', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:10:02.103', N'201228', N'2013-09-13 12:10:02.103', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'86f0bf2b-3b80-4784-8a77-c51210031a8a', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:37.570', N'201228', N'2013-09-04 12:24:37.570', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'86f971d9-8278-4589-885f-cfd3e087d07d', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:35.573', N'201228', N'2013-09-04 16:11:35.573', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'875135f8-18b8-4c32-87d0-f28495ce0249', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:18:32.133', N'201228', N'2013-09-13 10:18:32.133', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8860ea9d-0858-46cb-8675-a361c3dec941', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:51:17.393', N'201228', N'2013-09-10 16:51:17.393', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'89b4e903-bc7c-45db-8bb6-0c4ca2768539', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:27:32.930', N'201228', N'2013-09-10 17:27:32.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'89d2529c-2bf0-41e7-b877-86a6d482b3da', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:51.280', N'201228', N'2013-09-10 11:45:51.280', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8b02e5a3-a7d2-46e2-8fb5-c034d3976f2d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:46:57.373', N'201228', N'2013-09-10 14:46:57.373', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8b0fa475-e452-4be1-a498-633044ccaa8a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:30:42.470', N'201228', N'2013-09-10 17:30:42.470', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8b988b89-3eb9-4ef1-8f88-2ef18fa8f980', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:07:36.560', N'201228', N'2013-09-13 10:07:36.560', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8bc476d5-cfb5-4192-9f30-574c5cd60602', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:09.297', N'201228', N'2013-09-04 16:19:09.297', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8c8b182e-e15d-48fa-9be4-173eed7966cb', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:05:17.203', N'201228', N'2013-09-04 16:05:17.203', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8cd47f59-bd0d-4b58-af0c-7af28d626ff3', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:18:39.920', N'201228', N'2013-09-04 16:18:39.920', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8cea0ea1-67b3-4aa9-9729-1050da28c684', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:12:13.837', N'201228', N'2013-09-04 16:12:13.837', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8dafb9b1-c43b-4dee-b378-df74db7ac9c0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:25:14.853', N'201228', N'2013-09-13 15:25:14.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8e60437b-4a38-4777-8759-b06e8e2d75e6', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:00.283', N'201228', N'2013-09-04 16:10:00.283', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8f43e899-64fa-4097-bfe3-368387d029f7', N'/Admin/UserInfo/Edit/admin', N'10', N'admin', N'201228', N'True', N'::1', null, N'2013-09-06 10:11:54.183', N'201228', N'2013-09-06 10:11:54.183', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'8f957b97-9299-45c6-ad48-0f8d49b3f9c0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:58:43.107', N'201228', N'2013-09-13 15:58:43.107', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'902cbf59-9da6-4ce4-92ac-89ceb2a520ae', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:32:58.970', N'201228', N'2013-09-13 15:32:58.970', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'906eb83d-44b0-4e54-9bd6-5cc537793328', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:42:35.727', N'201228', N'2013-09-10 17:42:35.727', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'90afedd9-4ea6-4fd3-be74-cea77beafa4f', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:06.247', N'201228', N'2013-09-06 10:19:06.247', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'91c07696-8af4-4b64-9d02-46dbf4f8a73a', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:09.730', N'201228', N'2013-09-04 16:09:09.730', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'91fd7d1b-30ef-4260-b475-373348be4401', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 13:56:32.130', N'201228', N'2013-09-10 13:56:32.130', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9203cd32-0977-4094-82d2-cc367f275a14', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:40:35.530', N'201228', N'2013-09-10 17:40:35.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'922f359e-b1a9-4b6c-a2e4-a5a22f6bfa7f', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:39:44.507', N'201228', N'2013-09-11 10:39:44.507', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'92497ab5-092a-4c47-a422-7aa6620111f5', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:47.363', N'201228', N'2013-09-04 16:10:47.363', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'92aef09a-0a70-4345-b61e-f091012bfb61', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:40:24.397', N'201228', N'2013-09-10 14:40:24.397', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'93a91424-c7c7-4ac9-98db-da71be20dc4e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:39:41.760', N'201228', N'2013-09-10 16:39:41.760', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'93f6848b-bb46-4609-ac48-38f4f9bb1373', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:56:30.563', N'201228', N'2013-09-13 11:56:30.563', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'93fd3e04-f34b-4835-9e2b-7fc8564c5380', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:58:24.493', N'201228', N'2013-09-10 17:58:24.493', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'94132aee-4b53-4a41-8b8b-355a90a338bc', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:50:22.387', N'201228', N'2013-09-10 17:50:22.387', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'95355434-6ee5-47f3-81db-f8b4bb5c8b11', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:25:39.197', N'201228', N'2013-09-10 17:25:39.197', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9577e05a-9766-4fd2-ab55-e5115a497790', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:32:27.930', N'201228', N'2013-09-13 15:32:27.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'95b819a3-b91f-48a1-a461-e962a38db767', N'/Admin/WebConfigAppSetting/Index', N'36', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:55.530', N'201228', N'2013-09-11 10:32:55.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'95fe06e4-1e14-481c-b625-831423d88ae8', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:11:03.730', N'201228', N'2013-09-23 10:11:03.730', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9639a115-ecd7-48e9-9eee-5255ab9f11f1', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:19:07.523', N'201228', N'2013-09-04 16:19:07.523', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'964ef2e5-da06-4352-b71c-e64d8ec4bb7b', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:37.463', N'201228', N'2013-09-13 16:12:37.463', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'96ecdca0-6cbb-4006-bad5-06d5ec208480', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:17:47.270', N'201228', N'2013-09-06 11:17:47.270', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'97987a97-9119-4896-93eb-592ab084ddc6', N'/Admin/WebConfigAppSetting', N'36', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:30.800', N'201228', N'2013-09-04 16:17:30.800', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'980088e8-6c9a-4a5d-8806-32fde515c3b0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:32:38.793', N'201228', N'2013-09-10 17:32:38.793', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'98ed7c90-22d2-49e9-95e9-a166ec7f6e9b', N'/Admin/RoleInfo/Edit/ee6b1601-4579-43d4-b629-74c2f6e27a49', N'15', N'ee6b1601-4579-43d4-b629-74c2f6e27a49', N'201228', N'True', N'::1', null, N'2013-09-04 16:16:55.743', N'201228', N'2013-09-04 16:16:55.743', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'991a9e53-2547-4d06-956d-6d3d18c90e9f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:54:26.153', N'201228', N'2013-09-13 15:54:26.153', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'995a3f33-b507-47d1-9398-da1837ed0fd1', N'/Admin/RoleInfo/Edit/e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'15', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:55.270', N'201228', N'2013-09-04 16:07:55.270', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'99af8c1b-9fda-4369-afb8-c66b32ec0e9d', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:12:50.413', N'201228', N'2013-09-06 10:12:50.413', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9a56bf75-a7e3-4bd7-b7c1-33456a1c341f', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:07.137', N'201228', N'2013-09-04 16:17:07.137', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9a5777c0-cd84-4ec5-b439-d6cd6d76a8d6', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:46:56.710', N'201228', N'2013-09-10 14:46:56.710', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9bc9cd02-c4f1-4161-b0c1-6b1f9560386a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:15:18.383', N'201228', N'2013-09-13 13:15:18.383', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9c6e4a1c-55c8-45c8-bd40-255f961980c7', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:18:15.663', N'201228', N'2013-09-04 16:18:15.663', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9ca67c20-b986-4744-a286-a7ce547bb802', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:09:29.290', N'201228', N'2013-09-04 16:09:29.290', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9ccac3bb-3d56-4e2b-8778-6eb68e2d2851', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:10:30.343', N'201228', N'2013-09-23 10:10:30.343', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9ccb71cb-b333-4592-a470-5386286a191b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:56:16.483', N'201228', N'2013-09-10 15:56:16.483', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9cd2354c-927c-42d0-bd5d-ad8a3f6d1dc0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:45:57.493', N'201228', N'2013-09-13 11:45:57.493', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9d7adc23-ebfd-42aa-8c44-50da89bea2a4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:43:08.860', N'201228', N'2013-09-13 15:43:08.860', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9e78add2-36fa-4b94-b4c7-5cc612597ca9', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:11:25.853', N'201228', N'2013-09-04 16:11:25.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9ea7e66a-084e-4ec2-90e8-026687e20841', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:31:51.697', N'201228', N'2013-09-10 17:31:51.697', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9eb19b4d-b26e-49ce-827c-b683ca0fdca2', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:54.580', N'201228', N'2013-09-11 10:34:54.580', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9ec7b30e-bf50-420d-bd39-52bbdc864a53', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:52:13.290', N'201228', N'2013-09-10 15:52:13.290', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9f8af051-c3fd-4606-96e2-0c5c82ac7559', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:52.233', N'201228', N'2013-09-10 11:45:52.233', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'9fd1960a-9336-4063-8bfd-d8e6696944cb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:54:53.167', N'201228', N'2013-09-13 11:54:53.167', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a0628a2f-1575-4a09-a5af-3d494d15adbb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:57:48.280', N'201228', N'2013-09-10 15:57:48.280', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a0f9c21b-8cea-4588-a346-3f22c05dba85', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:33:04.930', N'201228', N'2013-09-13 15:33:04.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a10cf220-fbe3-44ee-92eb-c7ba9094dad0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:12:18.267', N'201228', N'2013-09-10 14:12:18.267', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a1c01bed-ee0d-4449-9e1a-864c05aaf4b3', N'/Admin/SysStatistic', N'56', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:11.727', N'201228', N'2013-09-04 16:19:11.727', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a1e7b979-d7c9-4740-92a9-e0d802672ed2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:06:46.843', N'201228', N'2013-09-13 12:06:46.843', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a20d2c1b-fc26-4a1a-accf-9ba3deae747e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:10:50.390', N'201228', N'2013-09-10 17:10:50.390', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a25b60d0-0991-4c54-8707-37db9cf739de', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:35:27.833', N'201228', N'2013-09-13 15:35:27.833', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a25b6fc8-f3a2-4316-916b-7a017c03a673', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:37.597', N'201228', N'2013-09-04 16:19:37.597', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a2979633-ae9d-47b7-b601-1e1092517ed4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:53:23.100', N'201228', N'2013-09-10 17:53:23.100', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a327f648-0766-4c6e-bf2b-cd6bba4cb824', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:13:21.120', N'201228', N'2013-09-13 13:13:21.120', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a3493445-5973-4707-a134-14193b8cbd3c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:10:26.757', N'201228', N'2013-09-13 12:10:26.757', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a3aa2ce5-12cd-40a3-8016-b6d11e666c71', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:00:26.607', N'201228', N'2013-09-13 16:00:26.607', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a3e90cb9-af67-462a-9190-539305d0896f', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:46:07.470', N'201228', N'2013-09-10 11:46:07.470', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a483493f-550c-4423-a989-a013d203e9c7', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:11:05.707', N'201228', N'2013-09-23 10:11:05.707', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a486c5b0-b6a6-4a35-9ac9-8d595bccc94c', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:11:54.257', N'201228', N'2013-09-06 10:11:54.257', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a55a737f-ebc8-4f6d-b43e-4a8e9c321e14', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:42.870', N'201228', N'2013-09-04 16:11:42.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a6401862-cd11-43cc-ae12-4354ddbd32d4', N'/Admin/SysStatistic/Index', N'56', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:08.257', N'201228', N'2013-09-11 10:32:08.257', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a6c8bb85-ca17-48c1-9fbd-8e8b98dc9378', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:09.847', N'201228', N'2013-09-04 16:08:09.847', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a7a1c5e2-991c-4009-a06c-df0d05cf1e3a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:01:47.953', N'201228', N'2013-09-13 16:01:47.953', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a8651979-3979-488e-8c31-934359fcb4a1', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 15:31:13.750', N'201228', N'2013-09-04 15:31:13.750', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a867b8b2-ca94-4c68-b793-61301c1d744a', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:41.537', N'201228', N'2013-09-04 16:09:41.537', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a8b00f0d-c120-493b-89ed-eb4a2648ffd5', N'/Admin/Department/Create', N'23', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:16.563', N'201228', N'2013-09-04 16:19:16.563', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a8cdcad3-ef04-4dcc-8006-021ea23d0f20', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:05.317', N'201228', N'2013-09-06 11:40:05.317', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a8d505f5-1a01-493e-9007-1bf6eb5166eb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:57:50.630', N'201228', N'2013-09-13 11:57:50.630', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a8e7b1fd-ebae-46a2-b266-bd4d4abed804', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:15:45.553', N'201228', N'2013-09-10 17:15:45.553', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'a98468a1-794a-42fd-9d59-732879c4f3e9', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:50.113', N'201228', N'2013-09-04 16:09:50.113', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ac2ba653-ac9f-4ddf-9975-27c883291699', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:49:04.713', N'201228', N'2013-09-10 16:49:04.713', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ac5bf2df-3d8b-41fc-a3fe-b1f6235a3389', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 13:56:18.500', N'201228', N'2013-09-10 13:56:18.500', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ac9f066c-50e7-4d62-bcb3-3f78b913eee1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:01:49.447', N'201228', N'2013-09-10 16:01:49.447', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'aca077aa-6b11-4d0b-8742-56fd966f79f6', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:33:31.500', N'201228', N'2013-09-10 17:33:31.500', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ad598da3-d219-4e13-a8bf-27c3c9139b80', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:01.210', N'201228', N'2013-09-04 16:07:01.210', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ae602374-0bda-4b0a-abeb-cab6b2a5d568', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:15:31.000', N'201228', N'2013-09-13 13:15:31.000', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'aeb9f311-c779-4a63-ac48-8e0dc852807f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:48:46.547', N'201228', N'2013-09-13 11:48:46.547', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'af14c456-eaa2-4eb2-a7f0-16b3219112db', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:01:01.687', N'201228', N'2013-09-04 16:01:01.687', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b09037d7-ad11-4fc8-88d2-16d08617a1ec', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:23:47.960', N'201228', N'2013-09-04 12:23:47.960', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b1976fc4-411e-44f6-92a0-816c89f46aef', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:56:15.870', N'201228', N'2013-09-04 15:56:15.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b1a77ca4-8a53-4517-83fd-a4ca52beaae5', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:07:29.703', N'201228', N'2013-09-13 16:07:29.703', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b1e969c4-5459-4490-a5c2-d1e8708fdca9', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:20.660', N'201228', N'2013-09-06 10:19:20.660', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b1f53c15-f2af-4cb4-b837-078aa6314c1d', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:15.533', N'201228', N'2013-09-04 16:19:15.533', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b2242620-2b02-465a-b8b0-7a591ffdf2f0', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:07:39.983', N'201228', N'2013-09-13 16:07:39.983', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b2bf0e35-2bb8-4a0c-8d9d-6b03f743888c', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:01:32.750', N'201228', N'2013-09-04 16:01:32.750', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b2ed7234-4a0d-4869-8c8a-3773d2f85e25', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:32:31.463', N'201228', N'2013-09-13 15:32:31.463', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b3daadd2-fe92-407a-b4b2-3b257fb77d94', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:10:18.710', N'201228', N'2013-09-23 10:10:18.710', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b3db1deb-48ac-4648-ae15-190ad23677f9', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:19:56.197', N'201228', N'2013-09-13 10:19:56.197', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b40dd6d0-ebe2-42b8-a559-6096ef5d590b', N'/Admin/UserInfo/Edit/haha', N'10', N'haha', N'201228', N'True', N'::1', null, N'2013-09-06 10:11:58.773', N'201228', N'2013-09-06 10:11:58.773', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b4e4b446-fe38-4df7-8759-f507848a2673', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:39:38.353', N'201228', N'2013-09-10 16:39:38.353', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b511bdd5-80e8-4df4-bef0-6696309c660b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:46:34.833', N'201228', N'2013-09-10 16:46:34.833', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b53279b9-19ef-40da-89b4-3c67a1d1a9bc', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:51.343', N'201228', N'2013-09-04 16:17:51.343', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5473d5d-efde-4fd0-b8a4-f687eb91821f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:30:40.830', N'201228', N'2013-09-10 17:30:40.830', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b55e01ec-0261-47a8-ae32-0e23874e1913', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:45:12.613', N'201228', N'2013-09-10 17:45:12.613', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b56ad11d-bbd5-4e9a-b313-334fef6197f6', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 13:56:34.323', N'201228', N'2013-09-10 13:56:34.323', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5847bc7-2a4b-426e-b8e5-91a8cfcd8b9f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:28:26.107', N'201228', N'2013-09-13 15:28:26.107', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5938c5a-337b-4206-9a89-b5ee0c6f6b75', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:12:07.570', N'201228', N'2013-09-13 16:12:07.570', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b595abd2-b2a4-4355-bc11-d30f37bd037f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:50:12.360', N'201228', N'2013-09-10 16:50:12.360', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5b9bc87-12bc-4093-87ee-e5b4ae3e3587', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:51.877', N'201228', N'2013-09-06 11:40:51.877', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5d8d60a-2b19-4c54-8090-2158926620ee', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:37:57.277', N'201228', N'2013-09-10 17:37:57.277', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b5f6275b-bddc-44ed-85b3-4ace32320b93', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:25:53.543', N'201228', N'2013-09-06 11:25:53.543', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b7740ead-c0a0-4027-8719-cbc9ae038373', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:16.390', N'201228', N'2013-09-06 10:19:16.390', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b8058af1-1c0e-41ff-80a4-bd88a591c500', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:15:42.867', N'201228', N'2013-09-10 17:15:42.867', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'b8a82525-74d9-4c7e-8cf0-362683884ef2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:06:26.833', N'201228', N'2013-09-13 10:06:26.833', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ba7d7a62-73bc-489a-88eb-bd1f3feff100', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:54:28.847', N'201228', N'2013-09-10 16:54:28.847', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ba879427-f605-4427-9e37-506b2635b7f0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:50:23.573', N'201228', N'2013-09-10 17:50:23.573', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'baab87ed-220a-45cd-84b4-5c19096d5d32', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:13.897', N'201228', N'2013-09-04 16:17:13.897', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bac31915-7200-40cd-918f-e2d2dcb67505', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:19:03.183', N'201228', N'2013-09-04 16:19:03.183', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bbb710e6-c3d7-4e6f-bfe1-afb4502ccb9c', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:37.880', N'201228', N'2013-09-04 16:05:37.880', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bbbe735c-e673-4bb6-932d-5cebbbf6463e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:00:20.380', N'201228', N'2013-09-10 14:00:20.380', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bc009982-f019-4199-8c5e-aa691b1de602', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:12:11.800', N'201228', N'2013-09-06 10:12:11.800', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bd034ded-a145-492f-8a0d-b3a857a1a5e2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:55:00.427', N'201228', N'2013-09-10 16:55:00.427', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bd3254cb-1ecd-4fc9-b342-923d2c1489d8', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:31:57.447', N'201228', N'2013-09-11 10:31:57.447', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bd9f0a09-15e2-4ff3-a593-ea8118a24a38', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:22:01.650', N'201228', N'2013-09-10 17:22:01.650', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bdd91fb9-ec4a-41dc-8ce2-db6e8b64df67', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:13:19.677', N'201228', N'2013-09-13 10:13:19.677', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'be3e1d61-5811-448c-b034-03afb90f8f12', N'/Admin/WebConfigAppSetting/Index', N'36', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:32:49.723', N'201228', N'2013-09-11 10:32:49.723', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bebabace-4554-47cf-8a74-85554a747c39', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-23 10:10:17.440', N'201228', N'2013-09-23 10:10:17.440', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bf5ebce6-4f26-41da-8e0a-74d2f327a82a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:25:20.400', N'201228', N'2013-09-13 15:25:20.400', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'bf9ea99d-f6fd-4525-912b-5ed409d2ff94', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:49:04.430', N'201228', N'2013-09-10 16:49:04.430', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c056c123-63a4-4eff-9741-9f75981e7b0a', N'/Admin/Department/Index', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:48.583', N'201228', N'2013-09-11 10:34:48.583', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c05b6f02-99a6-4a39-bef0-883ea5b07d9e', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:21:29.247', N'201228', N'2013-09-04 16:21:29.247', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c08ebe8a-b973-4285-9efe-2118b187448d', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:10.440', N'201228', N'2013-09-04 16:07:10.440', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c0e8be8b-138d-471e-87a1-0eefe0f13079', N'/Admin/SysHelp', N'41', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:35.693', N'201228', N'2013-09-04 16:09:35.693', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c196803e-5500-4809-bdf2-78a356c63fdb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:13:24.950', N'201228', N'2013-09-13 10:13:24.950', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c21b82f7-f318-417e-9543-e10670f3f81e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:14:28.803', N'201228', N'2013-09-13 10:14:28.803', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c262e9f2-e2ca-4ba8-8eca-93e86e86f500', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:39:19.850', N'201228', N'2013-09-10 14:39:19.850', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c2a234dc-4083-446a-b365-bb4bea22a44e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:53:21.240', N'201228', N'2013-09-10 17:53:21.240', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c417d6d3-2861-40c4-8f2c-4f2677ed673e', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:16:47.930', N'201228', N'2013-09-04 16:16:47.930', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c487f9f6-55f8-44d8-9aff-03f779b2af5e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:26:18.530', N'201228', N'2013-09-10 17:26:18.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c54641c2-7a87-451f-9315-4f0438e8d943', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 12:23:47.850', N'201228', N'2013-09-04 12:23:47.850', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c591fb01-7900-432b-a904-7d06193ecac7', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 15:56:19.390', N'201228', N'2013-09-04 15:56:19.390', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c5a9e8bc-080e-441d-9e7e-e03aae808f06', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:51.677', N'201228', N'2013-09-04 16:09:51.677', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c61dba3c-f5c4-4790-8f98-69705faabeb1', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-06 11:17:53.300', N'201228', N'2013-09-06 11:17:53.300', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c677eff0-4e99-4d9a-80a4-29ba2ef1d841', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:16:16.497', N'201228', N'2013-09-13 15:16:16.497', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c6f4be01-5af5-492e-a42c-8881d9d6173c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:10:07.413', N'201228', N'2013-09-13 10:10:07.413', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c770f32e-e664-44ed-8508-bbf07074b2f1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 18:03:12.540', N'201228', N'2013-09-10 18:03:12.540', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c80cf522-ebb2-474c-a0d4-e622d92b06ba', N'/Admin/UserInfo/Edit/admin', N'10', N'admin', N'201228', N'True', N'::1', null, N'2013-09-04 16:06:07.977', N'201228', N'2013-09-04 16:06:07.977', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c8c38829-2f93-4a48-b9c7-5276d08a7c0b', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:39.780', N'201228', N'2013-09-04 16:09:39.780', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c950b83f-e5a2-42ef-a83f-056a59a41a25', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:02:02.947', N'201228', N'2013-09-04 16:02:02.947', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c97e6222-8ded-4218-9952-d2dca4077582', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:26:03.817', N'201228', N'2013-09-10 16:26:03.817', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c9bde58a-3feb-4c51-a304-692a65c1a5ff', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:27:52.263', N'201228', N'2013-09-13 15:27:52.263', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c9ccf68a-9f21-4b55-93da-5b87748fbe6a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:51:18.600', N'201228', N'2013-09-10 16:51:18.600', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'c9dfa8de-6d04-4a0a-82ca-c84f82b79a47', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:14:55.737', N'201228', N'2013-09-13 13:14:55.737', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'caf86362-40de-49d0-9a59-92845c2aed35', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:09.433', N'201228', N'2013-09-04 12:24:09.433', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cb564ce0-314d-41c0-a489-31f6aac55fce', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:07:49.733', N'201228', N'2013-09-04 16:07:49.733', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cb72912a-eb79-4d13-9e89-a3e9ffd2465e', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:08.450', N'201228', N'2013-09-04 16:05:08.450', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cbab794e-5ede-4680-bec9-04f50215aefa', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:38.223', N'201228', N'2013-09-04 16:10:38.223', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cbb34bc4-dc55-4be7-9059-f2873c4478e2', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:44.210', N'201228', N'2013-09-04 16:05:44.210', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cbedea2c-6bea-4c7f-8fa6-e9c08cb49e8a', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:13:13.457', N'201228', N'2013-09-04 16:13:13.457', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cc1c62dc-aba1-47b0-97b7-6af3068b7637', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 11:55:50.327', N'201228', N'2013-09-13 11:55:50.327', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cc1e216f-39c0-4df1-a3f9-96c03dee4ffc', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:20:58.753', N'201228', N'2013-09-13 10:20:58.753', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cc571776-6621-4f47-aa36-fe0c64272e80', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:13:15.177', N'201228', N'2013-09-04 16:13:15.177', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cc888e55-d51a-4e51-85cc-bf53f94662ab', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:05.307', N'201228', N'2013-09-04 16:17:05.307', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cd58c4e2-a3d8-4fdc-a5eb-48dd76b8ccfc', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:01.450', N'201228', N'2013-09-04 16:19:01.450', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cdee4351-98bb-43ba-be96-f9bb77fe5686', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:36:29.413', N'201228', N'2013-09-10 17:36:29.413', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ce682727-fef3-45cc-a04d-ec384c781ac7', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:17:54.627', N'201228', N'2013-09-13 10:17:54.627', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ce6d3f9b-bcaa-4844-b40c-f3fcaec97027', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:23:58.273', N'201228', N'2013-09-10 17:23:58.273', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cf03784e-90d3-4b18-8a17-3b98d6733908', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:54:59.823', N'201228', N'2013-09-10 16:54:59.823', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cfaf4bb8-b56d-488c-b57f-fe21f7f132c5', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:31.810', N'201228', N'2013-09-04 16:07:31.810', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'cfd2bf97-7a20-4751-a0c8-167da1f29945', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:04:33.530', N'201228', N'2013-09-10 17:04:33.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd06157f6-a049-49f6-8334-8273acee545d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:08:49.827', N'201228', N'2013-09-13 10:08:49.827', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd0ce7b91-d3c1-4c97-b8c8-f93531834f72', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:40:49.830', N'201228', N'2013-09-11 10:40:49.830', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd1ba55df-33fe-4ebe-9b20-e7c7ef032cd3', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:32:34.353', N'201228', N'2013-09-10 14:32:34.353', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd1ee55eb-a231-402c-8428-172fc20508c1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:01:55.757', N'201228', N'2013-09-13 12:01:55.757', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd2dd86d8-6e76-451f-9def-a64d6d8f7ad7', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:18:17.117', N'201228', N'2013-09-06 11:18:17.117', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd2e0ef02-98bb-44d0-84e6-767a21b7f365', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:10:07.757', N'201228', N'2013-09-04 16:10:07.757', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd32a4b41-786e-4f68-91a9-81b63c5527ed', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:19:46.323', N'201228', N'2013-09-04 16:19:46.323', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd353ae48-67e8-4b59-879b-9b71d60c4ffb', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:19:23.700', N'201228', N'2013-09-06 10:19:23.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd3580e0d-48b4-449b-b8b2-3fc6b23f837f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:08:06.570', N'201228', N'2013-09-13 10:08:06.570', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd39bf262-bb29-4864-80d2-226b8a2ea485', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:11:38.297', N'201228', N'2013-09-04 16:11:38.297', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd4094409-1a0d-4b4c-ad77-fe207fdc6a31', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 15:31:54.543', N'201228', N'2013-09-04 15:31:54.543', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd40ab6c9-403c-45ae-a067-9d5327b3f64c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:02:15.527', N'201228', N'2013-09-13 16:02:15.527', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd413185e-0516-456c-a38f-7825115f93a7', N'/Admin/UserInfo/Edit/admin', N'10', N'admin', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:34.700', N'201228', N'2013-09-04 16:08:34.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd494cae9-e823-452f-b593-ba2fa806c92e', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:11:32.227', N'201228', N'2013-09-06 10:11:32.227', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd4c158c3-3ddc-4b2a-839d-affa3d217869', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:36:27.823', N'201228', N'2013-09-10 17:36:27.823', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd4caae88-b104-4264-a4d1-6585dad760a6', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:52:37.957', N'201228', N'2013-09-10 15:52:37.957', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd50428cf-7b7e-4528-81fc-7b2e089b59f3', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:03.600', N'201228', N'2013-09-04 16:08:03.600', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd543a10c-de83-46e6-b654-d2e3ed0edc3f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:13:30.230', N'201228', N'2013-09-10 17:13:30.230', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd5f526f0-05e2-44fa-b616-25691367307a', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:45.177', N'201228', N'2013-09-04 16:19:45.177', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd69f0cdc-b9ef-4eab-a7f7-a12fc107d43c', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:00.130', N'201228', N'2013-09-04 16:10:00.130', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd6d3475c-e4d6-4bc3-81e6-4d236b6385f4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:01:38.233', N'201228', N'2013-09-10 16:01:38.233', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd7222b9a-3171-4d26-a690-f0827d38be7f', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-06 11:40:44.693', N'201228', N'2013-09-06 11:40:44.693', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd76497e9-4da6-451c-9456-2693f57fd8af', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:17:41.953', N'201228', N'2013-09-06 11:17:41.953', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd874ed23-c30d-4cc7-b2c3-e720e16213a2', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:31:23.547', N'201228', N'2013-09-04 15:31:23.547', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd87bb655-956a-4f10-ad72-f716ccf20931', N'/Admin/UserInfo/Edit/haha', N'10', N'haha', N'201228', N'True', N'::1', null, N'2013-09-06 10:12:06.257', N'201228', N'2013-09-06 10:12:06.257', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd890b3de-4534-467b-8a4a-b72c69fa97b9', N'/Admin/UserInfo/Edit/%E7%AE%A1%E7%90%86%E5%91%98', N'10', N'管理员', N'201228', N'True', N'::1', null, N'2013-09-06 10:12:08.283', N'201228', N'2013-09-06 10:12:08.283', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd8aa7248-8d6d-4042-b2b2-97d18fb01693', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 13:56:19.730', N'201228', N'2013-09-10 13:56:19.730', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd8caa088-071a-4d50-9eb5-038dcfacf87b', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:33:19.347', N'201228', N'2013-09-11 10:33:19.347', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd9009f63-e1f2-4a18-9b9f-95b15ed271d4', N'/Admin/RoleInfo/Details/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'14', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:02:04.613', N'201228', N'2013-09-04 16:02:04.613', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'd9bdc943-c077-4bf2-adf2-f849ccd2bd7e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:38:38.260', N'201228', N'2013-09-10 14:38:38.260', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'db6b15fc-4679-431c-8d67-3bab01a477c7', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:03.303', N'201228', N'2013-09-04 12:24:03.303', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dba33ba9-c736-404e-9e80-ee444dbbd2e5', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:46:06.617', N'201228', N'2013-09-10 11:46:06.617', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dbca54d5-7141-4115-b62c-96845b00f42e', N'/Admin/UserInfo/Details/201228', N'9', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 15:31:33.627', N'201228', N'2013-09-04 15:31:33.627', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dbf96f7d-4938-4144-8d73-ecad5e19dcff', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:46:04.457', N'201228', N'2013-09-10 11:46:04.457', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dc2f5e12-e41d-4c4d-8ed6-5a137371c3f4', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:07:12.407', N'201228', N'2013-09-13 10:07:12.407', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dc37c5fc-0add-4f4f-95e0-c93fff459b9d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:27:30.657', N'201228', N'2013-09-10 17:27:30.657', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dcbdaf73-68bf-434b-985a-8c2df684ff73', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:23:57.597', N'201228', N'2013-09-04 12:23:57.597', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dcdb6910-4035-466f-a311-f2a62072197c', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:43:42.217', N'201228', N'2013-09-10 14:43:42.217', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dd0079e7-23f0-4bdf-bd13-bca5a5038c35', N'/Admin/RoleInfo/Edit/e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'15', N'e47cd7d4-07cb-4d2b-aeda-006ebf03b708', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:26.167', N'201228', N'2013-09-04 16:08:26.167', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dd28265b-da8c-4164-ae56-11aab20b3013', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:12:06.390', N'201228', N'2013-09-06 10:12:06.390', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dd4bc350-2ace-46bd-a27c-2ad7676cae70', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:02:07.983', N'201228', N'2013-09-04 16:02:07.983', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dd8845be-aaf7-41d3-91bc-132f2cf346d0', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 09:07:40.840', N'201228', N'2013-09-13 09:07:40.840', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'dec76ae2-90bf-4f71-986d-505674ceb4f8', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:43.870', N'201228', N'2013-09-10 11:45:43.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'deda9adc-f41d-42bc-a790-283c2844d3f0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:50:11.713', N'201228', N'2013-09-10 16:50:11.713', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'df9bdc5b-40cf-4d7e-a819-1b5c1cd6eca6', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:02.220', N'201228', N'2013-09-11 10:34:02.220', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e006d4f9-20ae-4772-a93f-ea01100028ae', N'/Admin/SysStatistic', N'56', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:31.540', N'201228', N'2013-09-04 16:17:31.540', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e0ee0558-4d49-4835-b9d9-7ab755e2eea8', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:11:31.513', N'201228', N'2013-09-10 14:11:31.513', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e0f7292f-1cd7-4f95-af7c-67b3fe6ee9bc', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:38.283', N'201228', N'2013-09-10 11:45:38.283', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e20caec9-8620-4f18-8632-fb331d98c406', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:13:29.957', N'201228', N'2013-09-04 16:13:29.957', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e29335ff-9c39-43bb-8c5d-f5476a5b5306', N'/Admin/Department', N'21', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 11:45:53.103', N'201228', N'2013-09-10 11:45:53.103', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e2b04703-cb33-4c7f-8f29-2d1c794e702a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:04:31.103', N'201228', N'2013-09-10 17:04:31.103', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e3f09833-11ba-4815-bc60-65d1e6e58bb0', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:53:58.853', N'201228', N'2013-09-10 16:53:58.853', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e44b801e-d9ba-4898-838d-abf7297878d9', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:07:53.220', N'201228', N'2013-09-04 16:07:53.220', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e4555d95-024f-4449-8c7f-556b5d723b2b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:37:01.357', N'201228', N'2013-09-10 16:37:01.357', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e4c1a4c1-dd79-458c-b32e-e88f6c1ff7cb', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:03:35.553', N'201228', N'2013-09-10 16:03:35.553', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e4daf239-6d08-451e-a64d-143940a0b083', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:31:41.397', N'201228', N'2013-09-11 10:31:41.397', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e4fc0de9-2887-41ee-9497-32cbdd2bff8a', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:05:06.870', N'201228', N'2013-09-04 16:05:06.870', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e66ae9ee-fa1f-4428-a68d-a0aa414ee07c', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:29:59.867', N'201228', N'2013-09-11 10:29:59.867', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e7a87ce7-3441-4211-bcbe-470b219806e0', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:12:09.170', N'201228', N'2013-09-04 16:12:09.170', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e82fee4e-e27f-4c35-8d17-2f8189e734f1', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:55:08.170', N'201228', N'2013-09-10 17:55:08.170', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e890c89f-9b3e-43ee-83e9-b264927f107a', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:17:13.840', N'201228', N'2013-09-04 16:17:13.840', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e8f84432-90e8-4aba-bd87-1243257e81b9', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:12:38.477', N'201228', N'2013-09-06 10:12:38.477', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'e9648c48-3ea5-4fab-a63d-e3ff8055a95b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 13:16:38.927', N'201228', N'2013-09-13 13:16:38.927', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ea2bc7f9-71a0-472a-9378-c89d0eff595f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:52:32.657', N'201228', N'2013-09-10 16:52:32.657', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ea6c0dc8-cc0a-4609-a524-a214eefc3689', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:09.330', N'201228', N'2013-09-06 11:40:09.330', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ea779c20-59e8-4f7a-8e35-f846cb4c0b71', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:08:41.910', N'201228', N'2013-09-04 16:08:41.910', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ea91cbb8-8b7b-4835-b57b-cfd3ddc1a4b3', N'/Admin/RoleInfo/Index', N'11', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 09:07:45.773', N'201228', N'2013-09-13 09:07:45.773', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'eb219076-d751-4f4d-97ba-9377a103a83c', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:06.637', N'201228', N'2013-09-04 16:09:06.637', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'eb6de444-2cb1-4a68-94de-3eb3d800e88b', N'/Admin/Department', N'21', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:11:58.857', N'201228', N'2013-09-04 16:11:58.857', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ebb885eb-685e-4e97-ab4e-0bf22cf624b2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 16:07:36.470', N'201228', N'2013-09-13 16:07:36.470', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ebceafee-45dc-417b-a3dc-bfc28c6bb3fa', N'/Admin/Department/Details/1000', N'24', N'1000', N'201228', N'True', N'::1', null, N'2013-09-04 16:11:51.497', N'201228', N'2013-09-04 16:11:51.497', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ec2acce4-60a1-47ec-8dfd-37c3ace6bfb6', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:12:18.213', N'201228', N'2013-09-10 14:12:18.213', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ee816fbf-2b8f-4e61-a6af-198de5cbb261', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:10:15.233', N'201228', N'2013-09-04 16:10:15.233', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ef6199d3-b535-41b8-951a-2db59da9cc01', N'/Admin/RoleInfo', N'11', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:09:45.973', N'201228', N'2013-09-04 16:09:45.973', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'efe5989d-942b-4491-ad19-644d0f70706b', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 13:56:30.247', N'201228', N'2013-09-10 13:56:30.247', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f0e9005f-05fc-4e25-9e36-f180b5d4a722', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 12:11:05.663', N'201228', N'2013-09-13 12:11:05.663', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f106ae1b-858f-4897-8207-90330ed6daec', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:49:54.643', N'201228', N'2013-09-10 17:49:54.643', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f1205efd-5251-4e57-8b90-018ef93e195b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:22:55.113', N'201228', N'2013-09-10 17:22:55.113', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f1714ce6-4d89-4063-af9c-3138ffef0d64', N'/Admin/MenuInfo', N'16', null, N'201228', N'True', N'::1', null, N'2013-09-06 11:40:07.657', N'201228', N'2013-09-06 11:40:07.657', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f404b985-b60c-4842-9a23-573eff203366', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:49:16.347', N'201228', N'2013-09-10 15:49:16.347', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f40bb44a-01e1-4fb2-bb92-15d451515e9d', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 09:07:46.637', N'201228', N'2013-09-13 09:07:46.637', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f4d617e7-9895-4bc1-b938-e1c2ab9f76a1', N'/Admin/UserInfo/Index', N'6', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:35:11.190', N'201228', N'2013-09-11 10:35:11.190', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f4d98376-b7ae-4003-90ca-8e1157db78e8', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:42.337', N'201228', N'2013-09-04 12:24:42.337', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f4de5d5d-35ee-4612-bdf9-c90e70f3a32a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:29:48.910', N'201228', N'2013-09-11 10:29:48.910', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f4e2bc3c-0f2b-4d79-9b41-dfa80450d41b', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:59:46.330', N'201228', N'2013-09-13 15:59:46.330', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f55b0dcf-39ae-409b-8d63-5cabd39fadc3', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:19:09.383', N'201228', N'2013-09-04 16:19:09.383', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f5e779c5-40b5-4653-a282-2f7dd682a604', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 09:07:37.517', N'201228', N'2013-09-13 09:07:37.517', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f64424f5-ef38-42a7-998c-dc43894ad07b', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:20.877', N'201228', N'2013-09-04 12:24:20.877', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f67f6548-095d-4ebe-8d83-3795e1ef6671', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:00:21.130', N'201228', N'2013-09-10 14:00:21.130', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f67fe592-74c4-46a7-92bb-9eed709c4a00', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-04 15:56:09.387', N'201228', N'2013-09-04 15:56:09.387', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f7385774-7cee-4afc-be7f-786b68b5b54f', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 15:52:39.543', N'201228', N'2013-09-13 15:52:39.543', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f7b77fa3-e7f7-4d2c-b438-cc862681d19d', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:52:38.617', N'201228', N'2013-09-10 15:52:38.617', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f7c3f3a3-84e1-4095-8458-1c2aff134b86', N'/Admin/Department/Edit/1000', N'25', N'1000', N'201228', N'True', N'::1', null, N'2013-09-04 15:57:39.867', N'201228', N'2013-09-04 15:57:39.867', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f86f4901-1865-4c61-ba3d-e5e6b4ea8b25', N'/Admin/UserInfo', N'6', null, N'201228', N'True', N'::1', null, N'2013-09-04 16:08:29.510', N'201228', N'2013-09-04 16:08:29.510', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f88972fe-68c3-4ad0-b7e3-ee883ae4728e', N'/Admin/SysStatistic/Index', N'56', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:34:39.710', N'201228', N'2013-09-11 10:34:39.710', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f8f0d722-2e7e-46fa-b792-87385e0b1639', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:18:18.997', N'201228', N'2013-09-04 16:18:18.997', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'f9052138-4aad-415c-aa02-76f3c3db88f5', N'/Admin/Department/Edit/1000', N'25', N'1000', N'201228', N'True', N'::1', null, N'2013-09-04 15:59:02.273', N'201228', N'2013-09-04 15:59:02.273', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fa854c62-64c3-4619-bc4e-18adaec2add1', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 12:24:35.343', N'201228', N'2013-09-04 12:24:35.343', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fb18cdac-a2db-4fa0-a553-de9630701f2e', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:19:02.700', N'201228', N'2013-09-13 10:19:02.700', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fbb18822-15fd-43f6-bed4-b47b72aa715f', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 15:53:47.437', N'201228', N'2013-09-04 15:53:47.437', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fbbf2add-cad3-42df-bfbe-75994074e989', N'/Admin/RoleInfo/Edit/d7b521f5-af32-4afa-9b5d-1fc61300d12e', N'15', N'd7b521f5-af32-4afa-9b5d-1fc61300d12e', N'201228', N'True', N'::1', null, N'2013-09-04 16:17:57.387', N'201228', N'2013-09-04 16:17:57.387', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fc9c9945-352c-4718-84ff-9dca080748c9', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 17:44:32.407', N'201228', N'2013-09-10 17:44:32.407', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fcdc61a0-4bc6-44b9-9e8a-d578450ec30a', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 15:56:17.107', N'201228', N'2013-09-10 15:56:17.107', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fceb9b88-4a78-4fe4-9973-5ebacd4ff440', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-13 10:17:49.587', N'201228', N'2013-09-13 10:17:49.587', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fd09c0b5-a611-4c21-9b58-517c2de28447', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 15:31:27.130', N'201228', N'2013-09-04 15:31:27.130', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fd5b61e6-1899-46bf-9823-4d5e0c25cedc', N'/Admin/MenuInfo/Index', N'16', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-11 10:33:01.513', N'201228', N'2013-09-11 10:33:01.513', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fdd2e248-8aea-4bfb-8a12-17ce7c05ecaf', N'/Admin/UserInfo/Edit/201228', N'10', N'201228', N'201228', N'True', N'::1', null, N'2013-09-04 16:17:52.980', N'201228', N'2013-09-04 16:17:52.980', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fdfb5a84-65df-4e7f-ba76-b4acafa61143', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 16:46:35.463', N'201228', N'2013-09-10 16:46:35.463', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'fe99770d-9fb5-4fd3-82ad-c67a6d12c0e2', N'/Admin', N'1', null, N'201228', N'True', N'127.0.0.1', null, N'2013-09-10 14:32:35.530', N'201228', N'2013-09-10 14:32:35.530', null, null);
GO
INSERT INTO [dbo].[BI_UserInfoLog] ([Id], [Url], [ChainInfoId], [RecordId], [SysUserId], [EnterpriseId], [Ip], [Remark], [Addate], [Aduser], [Uddate], [Uduser], [UserInfo_UId]) VALUES (N'ff336e4e-8cf7-4bcd-882e-7e8a0f61f0d9', N'/Admin', N'1', null, N'201228', N'True', N'::1', null, N'2013-09-06 10:11:28.870', N'201228', N'2013-09-06 10:11:28.870', null, null);
GO

-- ----------------------------
-- Indexes structure for table __MigrationHistory
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[__MigrationHistory]
-- ----------------------------
ALTER TABLE [dbo].[__MigrationHistory] ADD PRIMARY KEY ([MigrationId])
GO

-- ----------------------------
-- Indexes structure for table BI_ChainInfo
-- ----------------------------
CREATE INDEX [IX_EId] ON [dbo].[BI_ChainInfo]
([EId] ASC) 
GO
CREATE INDEX [IX_MId] ON [dbo].[BI_ChainInfo]
([MId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_ChainInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_ChainInfo] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table BI_Department
-- ----------------------------
CREATE INDEX [IX_ManagerIds] ON [dbo].[BI_Department]
([ManagerIds] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_Department]
-- ----------------------------
ALTER TABLE [dbo].[BI_Department] ADD PRIMARY KEY ([DeptId])
GO

-- ----------------------------
-- Indexes structure for table BI_DepartType
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_DepartType]
-- ----------------------------
ALTER TABLE [dbo].[BI_DepartType] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table BI_EditInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_EditInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_EditInfo] ADD PRIMARY KEY ([EId])
GO

-- ----------------------------
-- Indexes structure for table BI_Log
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_Log]
-- ----------------------------
ALTER TABLE [dbo].[BI_Log] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table BI_MenuInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_MenuInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_MenuInfo] ADD PRIMARY KEY ([MId])
GO

-- ----------------------------
-- Indexes structure for table BI_Notice
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_Notice]
-- ----------------------------
ALTER TABLE [dbo].[BI_Notice] ADD PRIMARY KEY ([NoticeID])
GO

-- ----------------------------
-- Indexes structure for table BI_NoticeInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_NoticeInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_NoticeInfo] ADD PRIMARY KEY ([ID])
GO

-- ----------------------------
-- Indexes structure for table BI_NoticeType
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_NoticeType]
-- ----------------------------
ALTER TABLE [dbo].[BI_NoticeType] ADD PRIMARY KEY ([NoticeTypeID])
GO

-- ----------------------------
-- Indexes structure for table BI_RoleChainInfo
-- ----------------------------
CREATE INDEX [IX_RId] ON [dbo].[BI_RoleChainInfo]
([RId] ASC) 
GO
CREATE INDEX [IX_CId] ON [dbo].[BI_RoleChainInfo]
([CId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_RoleChainInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_RoleChainInfo] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table BI_RoleInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_RoleInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_RoleInfo] ADD PRIMARY KEY ([RId])
GO

-- ----------------------------
-- Indexes structure for table BI_UDepartment
-- ----------------------------
CREATE INDEX [IX_DeptId] ON [dbo].[BI_UDepartment]
([DeptId] ASC) 
GO
CREATE INDEX [IX_UId] ON [dbo].[BI_UDepartment]
([UId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_UDepartment]
-- ----------------------------
ALTER TABLE [dbo].[BI_UDepartment] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table BI_URoleInfo
-- ----------------------------
CREATE INDEX [IX_RId] ON [dbo].[BI_URoleInfo]
([RId] ASC) 
GO
CREATE INDEX [IX_UId] ON [dbo].[BI_URoleInfo]
([UId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_URoleInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_URoleInfo] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table BI_UserInfo
-- ----------------------------
CREATE INDEX [IX_UserInfo_UId] ON [dbo].[BI_UserInfo]
([UserInfo_UId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_UserInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_UserInfo] ADD PRIMARY KEY ([UId])
GO

-- ----------------------------
-- Indexes structure for table BI_UserInfoLog
-- ----------------------------
CREATE INDEX [IX_ChainInfoId] ON [dbo].[BI_UserInfoLog]
([ChainInfoId] ASC) 
GO
CREATE INDEX [IX_UserInfo_UId] ON [dbo].[BI_UserInfoLog]
([UserInfo_UId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table [dbo].[BI_UserInfoLog]
-- ----------------------------
ALTER TABLE [dbo].[BI_UserInfoLog] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_ChainInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_ChainInfo] ADD FOREIGN KEY ([EId]) REFERENCES [dbo].[BI_EditInfo] ([EId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[BI_ChainInfo] ADD FOREIGN KEY ([MId]) REFERENCES [dbo].[BI_MenuInfo] ([MId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[BI_ChainInfo] ADD FOREIGN KEY ([EId]) REFERENCES [dbo].[BI_EditInfo] ([EId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[BI_ChainInfo] ADD FOREIGN KEY ([MId]) REFERENCES [dbo].[BI_MenuInfo] ([MId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_Department]
-- ----------------------------
ALTER TABLE [dbo].[BI_Department] ADD FOREIGN KEY ([ManagerIds]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_RoleChainInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_RoleChainInfo] ADD FOREIGN KEY ([RId]) REFERENCES [dbo].[BI_RoleInfo] ([RId]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_UDepartment]
-- ----------------------------
ALTER TABLE [dbo].[BI_UDepartment] ADD FOREIGN KEY ([UId]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_URoleInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_URoleInfo] ADD FOREIGN KEY ([RId]) REFERENCES [dbo].[BI_RoleInfo] ([RId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[BI_URoleInfo] ADD FOREIGN KEY ([UId]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_UserInfo]
-- ----------------------------
ALTER TABLE [dbo].[BI_UserInfo] ADD FOREIGN KEY ([UserInfo_UId]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[BI_UserInfo] ADD FOREIGN KEY ([UserInfo_UId]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[BI_UserInfoLog]
-- ----------------------------
ALTER TABLE [dbo].[BI_UserInfoLog] ADD FOREIGN KEY ([UserInfo_UId]) REFERENCES [dbo].[BI_UserInfo] ([UId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1000', N'公司', N'公司', N'1000', N'4234234', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1001', N'第一开发部', N'第一开发部', N'1000', N'5966103', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1002', N'第二开发部', N'第二开发部', N'1000', N'4345435', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'201228');
GO
INSERT INTO [dbo].[BI_Department] ([DeptId], [Department1], [DeptName], [DeptParentId], [Tel], [Addate], [Aduser], [Uddate], [Uduser], [Enabled], [ManagerIds]) VALUES (N'1003', N'第三开发部', N'第三开发部', N'1002', N'4233434', N'2013-08-27 02:44:40.000', N'201228', N'2013-08-27 02:44:40.000', N'201228', N'1', N'haha');
GO

INSERT INTO BI_EditInfo(EId,EName,ActionName,SystemId,Addate,Aduser,Uddate,Uduser)
VALUES (10,'查','GET','010',GETDATE(),'201228',GETDATE(),'201228')
GO
INSERT INTO BI_ChainInfo(Id,MId,EId,State,Addate,Uddate) VALUES(63,5,10,0,GETDATE(),GETDATE())
GO
INSERT INTO BI_ChainInfo(Id,MId,EId,State,Addate,Uddate) VALUES(64,2,10,0,GETDATE(),GETDATE())
GO