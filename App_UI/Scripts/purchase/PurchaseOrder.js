﻿//显示数据的表的标志
var tableName = "#purchaseOrderTable";
var editRow = undefined; //定义全局变量：当前编辑的行

//带搜索图标的框
var searchBox = function (value, name) {
    alert(value + ":" + name);
}

// 【供货商选择按钮】按下
var ShowCompInfoScreen = function () {
    var inputCompName = $("#OutCompanyName").val();
    ShowCompInfo("CompSelectDiv", 1, "", inputCompName, "");
};

// 【物料选择按钮】按下
var ShowProdPartInfoScreen = function (obj) {
    var rowObj = obj.parentNode.parentNode.parentNode
    editRow = $(rowObj).index();
    ShowProdPartInfo("ProdPartSelectDiv", "", "");
}


//批准 
var approve = function () {
    //得到当前数据的外购单号
    var outOrderID = "";
    var approveUID = "";
    outOrderID = $("#data").val();
    approveUID = $("#ApproveUID").val();
    $.messager.confirm("", "您确定要对此外购单进行批准吗？", function (approveOK) {
        if (approveOK) {
            $.post("/Purchase/PurchaseOrder/Approve", { OutOrderID: outOrderID, ApproveUID: approveUID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    window.location.reload();//刷新数据
                } else {
                    //alert("操作失败，请重新操作");
                    if (_data.Message != "") {
                        $.messager.alert("信息", _data.Message);
                    }
                }
            });
        }
    });
}

//审核
var review = function () {
    //得到当前数据的外购单号
    var check = "";
    var verifyUID = "";
    check = $("#data").val();
    verifyUID = $("#VerifyUID").val();
    $.messager.confirm("", "您确定要对此外购单进行审核吗？", function (reviewOK) {
        if (reviewOK) {
            $.post("/Purchase/PurchaseOrder/Audit", { OutOrderID: check, VerifyUID: verifyUID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    window.location.reload();//刷新数据
                } else {
                    if (_data.Message != "") {
                        $.messager.alert("信息", _data.Message);
                    }
                }
            });
        }
    });
}

//打开编辑
var openEdit = function () {
    var row = $(tableName).datagrid('getRows');
    if (row.length > 0) {
        for (var i = 0 ; i < row.length; i++) {
            $(tableName).datagrid("beginEdit", i);
        }
    }
}

//添加行
var addRow = function () {
    var date;
    date = new Date();
    var str = date.Format("yyyy-MM-dd");
    var row = $(tableName).datagrid('getRows');
    $(tableName).datagrid("insertRow", {
        index: row.length, // index start with 0
        row: {
            "DeliveryDate": str//"DLY_DATE"为当前时间
        }
    });
    var index = row.length - 1;
    //将新插入的那一行开户编辑状态
    $(tableName).datagrid("beginEdit", index);
    //给当前编辑的行赋值
    editRow = index;
    setTimeout(function () {
        $.parser.parse(".datagrid");//解析datagrid
    }, 10);
}

//保存方法 新建
var saveCreate = function () {
    $.messager.confirm("友情提示", "您确定要保存画面上的内容吗？", function (deleteOK) {
        if (deleteOK) {
            var row = $(tableName).datagrid('getRows');
            if (row.length > 0) {
                //定义一个数组用于存放table中的数据
                var orderList = [];
                var selectors = ["input[name=PPNo]", "input[name=PName]","input","input","input","input","input[class=combo-value]","input","input","input"];
                for (var i = 0; i < row.length; i++) {
                    var tds = $("div.datagrid-view2").find("div.datagrid-body").find("tr.datagrid-row:eq(" + i + ")").children();
                    //得到每行数据
                    var orderk = {};
                    for (var k = 0; k < tds.length; k++) {
                        orderk[k] = $(tds[k]).find(selectors[k]).val();
                    }
                    if (orderk[0] != "" || orderk[2] != "" || orderk[3] != "" || orderk[4] != "" || orderk[5] != "" || orderk[7] != "" || orderk[8] != "" || orderk[9] != "") {
                        var order = {};
                        order["ProductPartID"] = orderk[0];
                        order["RequestQuantity"] = orderk[2];
                        order["MaterialsSpecReq"] = orderk[3];
                        order["UnitPrice"] = orderk[4];
                        order["Evaluate"] = orderk[5];
                        order["DeliveryDate"] = orderk[6];
                        order["PlanNo"] = orderk[7];//计划单号
                        order["VersionNum"] = orderk[8];
                        order["Remarks"] = orderk[9];
                        //将order加进orderList
                        orderList.push(order);
                    }
                }

                $.ajax({
                    async: true,
                    url: "/Purchase/PurchaseOrder/Save",
                    data: {
                        UrgentStatus: $("#UrgentStatus").combobox('getValue'),
                        CustomerOrder: $("#CustomerOrder").val(),
                        DepartmentID: $("#DepartmentID").combobox('getValue'),
                        OutCompanyID: $("#OutCompanyID").val(),
                        ApproveUID: $("#ApproveUID").val(),
                        VerifyUID: $("#VerifyUID").val(),
                        EstablishUID: $("#EstablishUID").val(),
                        SignUID: $("#SignUID").val(),
                        Remarks2: $("#RMRS").val(),
                        FaxNo: $("#FAXNO").val(),
                        orderList: orderList
                    },
                    type: "post",
                    success: function (data) {
                        var _data = eval('(' + data + ')');
                        if (_data.Result == true) {
                            $.messager.alert("信息", _data.Message);
                        } else {
                            //title：显示在标题面板的标题文本;msg：提示框显示的消息文本;icon：提示框显示的图标,可用的值是：error,question,info,warning;fn：当窗口关闭时触发的回调函数
                            $.messager.alert("信息", _data.Message, "info", function () {
                                //开启编辑
                                openEdit();
                            });
                            //do something another...
                        }
                    }
                });
                console.info(rowData);
            }
        }
    });
}
//保存方法 编辑
var saveEdit = function () {
    $.messager.confirm("友情提示", "您确定要保存画面上的内容吗？", function (deleteOK) {
        if (deleteOK) {
            var row = $(tableName).datagrid('getRows');
            if (row.length > 0) { //定义一个数组用于存放table中的数据
                var orderList = [];
                var remarks2 = $("#Remarks2").val();
                var faxNo = $("#FaxNo").val();
                for (var i = 0; i < row.length; i++) {
                    $("#purchaseOrderTable").datagrid("endEdit", i);
                    var order = {};
                    order["RowIndex"] = i;
                    order["ProductPartID"] = row[i].ProPartID;
                    order["RequestQuantity"] = row[i].RequestQuantity;
                    order["MaterialsSpecReq"] = row[i].MaterialsSpecReq;
                    order["UnitPrice"] = row[i].UnitPrice;
                    order["Evaluate"] = row[i].Evaluate;
                    order["DeliveryDate"] = row[i].DeliveryDate;
                    order["PlanNo"] = row[i].PlanNo;
                    order["VersionNum"] = row[i].VersionNum;
                    order["Remarks"] = row[i].Remarks;
                    order["CustomerOrder"] = "";
                    order["CustomerOrderDetail"] = "";
                    for (var j = 0; j < row[i].CustomerOrder.length; j++)
                    {
                        order["CustomerOrder"] += row[i].CustomerOrder[j]+",";
                        order["CustomerOrderDetail"] += row[i].CustomerOrderDetail[j]+",";
                    }
                    order["CustomerOrder"] = order["CustomerOrder"].substring(0, order["CustomerOrder"].length - 1);
                    order["CustomerOrderDetail"] = order["CustomerOrderDetail"].substring(0, order["CustomerOrderDetail"].length - 1);
                    //将order加进orderList
                    orderList.push(order);

                }
                $.ajax({
                    async: true,
                    url: "/Purchase/PurchaseOrder/Save",
                    data: {
                        DepartmentID: document.getElementById("DeptName").innerHTML,
                        OutCompanyID: document.getElementById("CompName").innerHTML,
                        ApproveUID: $("#ApproveUID").val(),
                        VerifyUID: $("#VerifyUID").val(),
                        EstablishUID: $("#EstablishUID").val(),
                        SignUID: $("#SignUID").val(),
                        Remarks2: remarks2,
                        FaxNo: faxNo,
                        OutOrderID: $("#data").val(),
                        orderList: orderList
                    },
                    type: "post",
                    success: function (data) {
                        var _data = eval('(' + data + ')');
                        if (_data.Result == true) {
                            $.messager.alert("信息", _data.Message, "info", function () {
                                window.location.reload();//刷新数据
                            });
                        } else {
                            //title：显示在标题面板的标题文本;msg：提示框显示的消息文本;icon：提示框显示的图标,可用的值是：error,question,info,warning;fn：当窗口关闭时触发的回调函数
                            $.messager.alert("信息", _data.Message, "info", function () {
                                openEdit();
                            });
                            //do something another...
                        }
                    }
                });
            }
        }
    });
}

//跳转至打印预览页面  新建
var printPreviewCreate = function () {
    parent.addTab("外购产品计划单", "/Purchase/PurchaseOrder/PrintPreview?OutOrderID=undefined");
};

//跳转至打印预览页面,审核后才能跳转  编辑
var printPreviewEdit = function () {
    var outSourceNo = $("#data").val();//得到传过来的外购单号
    parent.addTab("外购产品计划单", "/Purchase/PurchaseOrder/PrintPreview?OutOrderID=" + outSourceNo + "");
};

//列属性:field(字段,非空) ,title(列名,非空),width(默认80),sortable(默认false),editor(默认null),formatter(默认-函数)
var columnsEdit = [[
     ["CustomerOrder", "", null, false],//隐藏列，客户订单号
     ["CustomerOrderDetail","",null,false],//隐藏列，客户订单详细号
     ["ProPartID", "物料编号", 130, true],//不可编辑
     ["PartName", "外购件名称", 125, true],
     ["RequestQuantity", "数量", 80, true],
     ["MaterialsSpecReq", "材料规格及要求", 140, true],
     ["UnitPrice", "单价", 80, true, { type: 'validatebox' }],
     ["Evaluate", "估价", 120, true, { type: 'validatebox' }],
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
     ["PlanNo", "计划单号", 100, true, { type: 'validatebox' }],
     ["VersionNum", "版本号", 80, true, { type: 'validatebox' }],
     ["Remarks", "备注", 100, true, { type: 'validatebox' }]
]]

var columnsCreate = [[
    ["ProPartID", "物料编号", 130, true, null, function (value, row, index) {
        return "<input type='text' name='ProductPartNo' id='ProductPartNo' disabled='true' size='13'/>" +
            "<img src='/Scripts/js/themes/icons/search_icon.png' onclick='ShowProdPartInfoScreen(this);'/>" +
            "<input type='hidden' id='PPNo' name='PPNo'/>";
    }],
    ["PartName", "外购件名称", 160, true, null, function (value, row, index){
        return "<input type='text' name='PName' id='PName' disabled='disabled' />";
    }],
    ["RequestQuantity", "数量", 120, true, { type: 'validatebox' }],
    ["MaterialsSpecReq", "材料规格及要求", 150, true, { type: 'validatebox' }],
    ["UnitPrice", "单价", 120, true, { type: 'validatebox' }],
    ["Evaluate", "估价", 120, true, { type: 'validatebox' }],
    ["DeliveryDate", "交货日期", 120, false, { type: 'datebox' }, function (value, row, index) {
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
    ["PlanNo", "计划单号", 120, true, { type: 'validatebox' }],
    ["VersionNum", "版本号", 120, true, { type: 'validatebox' }],
    ["Remarks", "备注", 120, true, { type: 'validatebox' }]
]]

$(function () {
    //得到传过来的外购单号
    var temp = $("#data").val();
    if (temp != "") {
        Common.Functions.createDataGrid({
            target: tableName,
            url: "/Purchase/PurchaseOrder/GetOutOrderInfoByID?OutOrderID=" + temp + "",
            columns: columnsEdit,
            idField: "OutOrderID",
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
                //开启编辑
                openEdit();
            }
        });
        //隐藏 客户订单号 客户订单详细号
        $('#purchaseOrderTable').datagrid('hideColumn', 'CustomerOrder');
        $('#purchaseOrderTable').datagrid('hideColumn', 'CustomerOrderDetail');
    }
    else {
        Common.Functions.createDataGrid({
            target: tableName,
            url: "/Purchase/PurchaseOrder/GetOutOrderInfoByID?OutOrderID='undefined'",
            columns: columnsCreate,
            idField: "OutOrderID",
            pagination: false,
            maxWidth: 950,
            maxHeight: 300,
            onBeforeLoad: function (queryParams) {
                //可以添加一些额外参数,主要用于查询条件
            },
            onLoadError: function () {
                alert("请求服务器失败，请刷新页面重试！");
            },
            onCreating: function (data) {
                //do something...
                for (var i = 0; i < 5; i++) {
                    //页面加载的时候就有一行
                    addRow();
                }
                setTimeout(function () {
                    //解析datagrid
                    $.parser.parse(".datagrid");
                }, 20);
            }
        });
    }

});