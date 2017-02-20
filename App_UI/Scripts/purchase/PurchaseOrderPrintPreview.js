//显示数据的表的标志
var tableName = "#purchaseOrderTable";

//打印
var printInfo = function () {
    var outOrderID = "";
    outOrderID = $("#data").val();
    $.messager.confirm("友情提示", "您确定要打印画面上的内容吗？", function (printOK) {
        if (printOK) {
            $.post("/Purchase/PurchaseOrder/PrintInfo", { OutOrderID: outOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    self.print();
                } else {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
}

var columns = [[
     ["ProPartID", "物料编号", 130, true],//不可编辑
     ["PartName", "外购件名称", 120, true],
     ["RequestQuantity", "数量", 80, true],
     ["MaterialsSpecReq", "材料规格及要求", 140, true],
     ["UnitPrice", "单价", 80, true, { type: 'validatebox' }],
     ["DeliveryDate", "交货日期", 100, true, { type: 'datebox' }, function (value, row, index) {
         var date;
         var strDt = (value + "").replace(/\/Date\(/, "").replace(/\)\//, "");
         if (isNaN(strDt)) {
             date = strDt.ToDate();
         } else {
             date = new Date(strDt.ToInt());
         }
         var str = date.Format("yyyy-MM-dd");
         row.DeliveryDate = str;
         return str;
     }],
     ["CustomerOrderID", "计划单号", 100, true],
     ["VersionNum", "版本号", 80, true, { type: 'validatebox' }],
     ["Remarks1", "备注", 100, true, { type: 'validatebox' }]
]]

$(function () {
    var temp = $("#data").val();//得到传过来的外购单号
    Common.Functions.createDataGrid({
        target: tableName,
        url: "/Purchase/PurchaseOrder/Query?outOrderID=" + temp + "",
        columns: columns,
        idField: "OutOrderID",
        toolbar: $("#toolbar"),
        pagination: false,
        maxWidth: 950,
        maxHeight: 200,
        onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件

        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试！");
        },
        onCreating: function (data) {
            //do something...
        }
    });
});