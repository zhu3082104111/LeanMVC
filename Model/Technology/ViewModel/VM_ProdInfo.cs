namespace Model.Technology
{
    #region 表单查询类
    public class VM_ProdInfoForSearchTableProdInfo
    {
        public string DepartmentID { get; set; } //部门ID

        public string ProductAbbreviation { get; set; } //产品略称

        public string ProductCategoryID { get; set; } //产品类别ID

        public string ProductName { get; set; } //产品名称
    } //end VM_ProdInfoForSearchTableProdInfo
    #endregion


    #region 表格显示类
    public class VM_ProdInfoForTableProdInfo 
    {
        public string DepartmentID { get; set; } //部门ID

        public string DepartmentName { get; set; } //部门名称

        public string OldModelID { get; set; } //旧产品名称

        public string ProductAbbreviation { get; set; } //产品略称

        public string ProductCategoryID { get; set; } //产品类别ID

        public string ProductCategoryName { get; set; } //产品类别名称

        public string ProductID { get; set; } //产品ID

        public string ProductName { get; set; } //产品名称

        public string Specifica { get; set; } //材料及规格型号

        public string UnitName { get; set; } //产品单位ID

    } //end VM_ProdInfoForTableProdInfo
    #endregion

}
