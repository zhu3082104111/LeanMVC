using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
namespace Model.Produce
{

    public class VM_AssemSmallBillForTableShow 
    {
        
    
    }

    public class VM_AssemSmallBillForSearch 
    {
    
    
    }

    public class VM_AssemSmallBillForDetailShow 
    {
    
    }

    public class VM_AssemBigBillForTableShow
    {
        public string AssemBillID { get; set; }//工票ID 

        public string CustomOrderNO { get; set; }//客户订单号

        public string DispatchID { get; set; }//调度单号

        public string Dispatcher { get; set; }

        public string Checker { get; set; }

        public string TeamLeader { get; set; }

        public string CheckResult { get; set; }

        public string ProductType { get; set; }//产品型号

        public string ProductName { get; set; }//产品名称

        public int LoadCount { get; set; }//装配总数量

        public string IsOver { get; set; }//是否完结
       
    }

    public class VM_AssemBigBillForSearch
    {
        [EntityProperty("AssemBillID")]
        public string AssemBillID { get; set; }//工票ID   总装工票

        [EntityProperty("CustomerOrderNum")]
        public string CustomOrderNO { get; set; }//客户订单号  总装调度单

        [EntityProperty("AssemblyDispatchID")]
        public string DispatchID { get; set; }//调度单号   总装调度单

        [EntityProperty("ProdAbbrev",PropertyOperator.NULL, typeof(ProdInfo))]
        public string ProductType { get; set; }//产品型号  product

        [EntityProperty("DepartId",PropertyOperator.EQUAL)]
        public string ProduceUnit { get; set; }//生产单元  product表

        [EntityProperty("EndFlag")]
        public string IsOver { get; set; }//是否完结

    }

    public class VM_AssemBigBillForDetailShow
    {
        public string AssemBillID { get; set; }//工票ID 

        public string ProductType { get; set; }//产品型号

        public Decimal PlanCount { get; set; }//计划数量

        public Decimal LoadCount { get; set; }//装配总数量

        public string Remark { get; set; }//备注

        public string ProductCheckResult { get; set; }//成品检验结果

        public string DispatcherID { get; set; }//调度员ID

        public string CheckerID { get; set; }//检验员ID

        public string TeamLeaderID { get; set; }//组长ID

        public string IsOver { get; set; }//是否完结

    }

    public class VM_AssemBigBillPartForDetailShow 
    {
        public string ProcessName { get; set; }//工序

        public int ProcessOrderNO { get; set; }//工序顺序号

        public string QuotNo { get; set; }//定额编号

        public string ProjectNO { get; set; }//项目序号

        public DateTime? OperateDate { get; set; }//日期

        public string Operator { get; set; }//操作员

        public decimal RealOperateCount { get; set; }//实际操作数量
    
    }
}
