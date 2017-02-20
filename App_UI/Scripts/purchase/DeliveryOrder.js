// 查询Form
var formName = "#searchForm";
// 显示数据的表的标志
var tableName = "#DeliveryOrderTable";

// 参数.送货单号
var paramDeliveryOrderNo = $("#paramDeliveryOrderNo").val();

//存放导入数据
var strInfo = "";

//新建标志 为true代表新建
var symbolCreate = false;

//页面隐藏采购计划单号
var orderNo = "";

//跳转至打印预览页面
var printPreviewEdit = function () {
    var temp = $("#data").val();//得到传过来的外购单号
    parent.addTab("送货单", "/Purchase/DeliveryOrder/PrintPreview?DeliveryOrderID=" + temp + "");
};

// 【供货商选择按钮】按下
var ShowCompInfoScreen = function () {
    var inputCompName = $("#DeliveryCompanyName").val();
    var orderNo = $("#OrderNo").val();
    //编辑页面
    if (orderNo == undefined)
    {
        var row = $(tableName).datagrid('getRows');
        var check = row[0].OrderNo.substring(6, 8);
        if (check == "WG") {
            ShowCompInfo("DeliSelectDiv", 1, "", inputCompName, "");
        }
        else if (check == "WX") {
            ShowCompInfo("DeliSelectDiv", 2, "", inputCompName, "");
        }
    }
    //新建页面
    if (orderNo != "") {
        var check = orderNo.substring(6, 8);
        if (check == "WG") {
            ShowCompInfo("DeliSelectDiv", 1, "", inputCompName, "");
        }
        else if (check == "WX") {
            ShowCompInfo("DeliSelectDiv", 2, "", inputCompName, "");
        }
        else { alert("采购计划单号有误"); }
    }
    else {
        alert("请填写采购计划单号");
    }
};

//添加行 导入时用到
var addRow = function () {
    var row = $(tableName).datagrid('getRows');
    $(tableName).datagrid("insertRow", {
        index: row.length, // index start with 0
        row: {
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

//打开编辑
var openEdit = function () {
    var row = $(tableName).datagrid('getRows');
    if (row.length > 0) {
        for (var i = 0 ; i < row.length; i++) {
            $(tableName).datagrid("beginEdit", i);
        }
    }
}

//新建
var creatInfo = function () {
    $.messager.confirm("友情提示", "是否保存当前送货单信息？", function (deleteOK) {
        if (deleteOK) {
            symbolCreate = true;
            saveEdit();
        }
        else {
            parent.addTab("送货单", "/Purchase/DeliveryOrder/Init");
        }
    })
}

//审核
var auditInfo = function () {
    $.messager.confirm("友情提示", "您确定要对此送货单进行审核吗？", function (auditOK) {
        if (auditOK) {
            //得到送货单号
            var deliveryOrderID = $("#data").val();
            $.post("/Purchase/DeliveryOrder/Audit", { DeliveryOrderID: deliveryOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    parent.addTab("送货单", "/Purchase/DeliveryOrderList/Init?");
                } else {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    })
}

//删除
var deleteFun = function () {
    //得到所选数据的送货单号
    deliveryOrderID = $("#data").val();
    $.messager.confirm("友情提示", "您确定要对此送货单进行删除吗？", function (deleteOK) {
        if (deleteOK) {
            $.post("/Purchase/DeliveryOrder/Delete", { DeliveryOrderID: deliveryOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    parent.addTab("送货单", "/Purchase/DeliveryOrderList/Init");
                } else {
                    $.messager.alert("信息", _data.Message);
                    //do something another...
                }
            });
        }
    });
}

//保存方法 新建
var saveCreate = function () {
    $.messager.confirm("友情提示", "您确定要保存画面上的内容吗？", function (saveOK) { 
    var orderNumber = $("#OrderNo").val();
    var row = $(tableName).datagrid('getRows');
    if (row.length > 0) {
        //定义一个数组用于存放table中的数据
        var orderList = [];
        for (var i = 0; i < row.length; i++) {
            var tds = $("div.datagrid-view2").find("div.datagrid-body").find("tr.datagrid-row:eq(" + i + ")").children();
            //得到每行数据
            var orderk = {};
            for (var k = 0; k < tds.length; k++) {
                orderk[k] = $(tds[k]).find("input").val();
            }
            var order = {};
            order["MaterielID"] = strInfo[i].MaterielID;
            order["Materiel"] = strInfo[i].Materiel;
            order["MaterielName"] = strInfo[i].MaterielName;
            order["MaterialsSpec"] = strInfo[i].MaterialsSpec;
            order["WarehouseID"] = strInfo[i].WarehouseID;
            order["Unit"] = strInfo[i].Unit;
            order["Quantity"] = strInfo[i].Quantity;
            order["UnitID"] = (strInfo[i].UnitID).substring(0, (strInfo[i].UnitID).length-1);
            order["InnumQuantity"] = orderk["9"];
            order["Num"] = orderk["10"];
            order["Scrap"] = orderk["11"];
            order["ActualQuantity"] = orderk["12"];
            order["Remarks"] = orderk["13"];
            //将order加进orderList
            orderList.push(order);

        }

        $.ajax({
            async: true,
            url: "/Purchase/DeliveryOrder/Save",
            data: {
                OrderNo: orderNo,
                OrderNumber:orderNumber,
                DeliveryDate: $("#DeliveryDate").datetimebox('getValue'),
                DeliveryUID: $("#DeliveryMan").val(),
                TelNo: $("#TelNo").val(),
                orderList: orderList
            },
            type: "post",
            success: function (data) {
                var _data = eval('(' + data + ')');
                if (_data.Result == true) {
                    $.messager.alert("信息", _data.Message);
                } else {
                    //title：显示在标题面板的标题文本;msg：提示框显示的消息文本;icon：提示框显示的图标,可用的值是：error,question,info,warning;fn：当窗口关闭时触发的回调函数
                    $.messager.alert("信息", _data.Message);
                    //do something another...
                }
            }
        });
        console.info(rowData);
    }
    });
}

//保存方法 编辑 您确定要保存画面上的内容吗？
var saveEdit = function () {
    $.messager.confirm("友情提示", "您确定要保存画面上的内容吗？", function (saveOK) { 
    var row = $(tableName).datagrid('getRows');
    if (row.length > 0) {
        var orderList = [];
        //每件数量、件数、零头、实收数量
        for (var i = 0; i < row.length; i++) {
            $("#DeliveryOrderTable").datagrid("endEdit", i);
            var order = {};
            order["RowIndex"] = i;
            order["InnumQuantity"] = row[i].InnumQuantity;
            order["Num"] = row[i].Num;
            order["Scrap"] = row[i].Scrap;
            order["ActualQuantity"] = row[i].ActualQuantity;
            order["MaterielID"] = row[i].MaterielID;
            order["Remarks"] = row[i].Remarks;
            orderList.push(order);
        }
        $.ajax({
            async: true,
            url: "/Purchase/DeliveryOrder/Save",
            data: {
                orderList: orderList,
                DeliveryDate: $("#DeliveryDate").val(),
                DeliveryCompanyID: $("#DeliveryCompanyName").val(),
                DeliveryUID: $("#DeliveryUID").val(),
                TelNo: $("#TelNo").val(),
                DeliveryOrderID: $("#data").val()
            },
            type: "post",
            success: function (data) {
                var _data = eval('(' + data + ')');
                if (_data.Result == true) {
                    if (symbolCreate == true) {
                        parent.addTab("送货单", "/Purchase/DeliveryOrder/Init");
                    }
                    $.messager.alert("信息", _data.Message, "info", function () {
                        window.location.reload();//刷新数据
                    });
                } else {
                    symbolCreate = false;
                    //title：显示在标题面板的标题文本;msg：提示框显示的消息文本;icon：提示框显示的图标,可用的值是：error,question,info,warning;fn：当窗口关闭时触发的回调函数
                    $.messager.alert("信息", _data.Message, "info", function () {
                        openEdit();
                    });
                    //do something another...
                }
            }
        });
    }
    });
}


//导入
var importInfo = function () {
    //删除原来table中的数据
    var row = $(tableName).datagrid('getRows');
    var copyRows = [];
    for (var j = 0; j < row.length; j++) {
        copyRows.push(row[j]);
    }
    for (var i = 0; i < copyRows.length; i++) {
        var index = $('#DeliveryOrderTable').datagrid('getRowIndex', copyRows[i]);
        $('#DeliveryOrderTable').datagrid('deleteRow', index);
    }

    orderNo = $("#OrderNo").val();
    //清空表中原有数据
    //$('#DeliveryOrderTable').datagrid('loadData', { total: 0, rows: [] });
    $.ajax({
        async: true,
        url: "/Purchase/DeliveryOrder/ImportInfo",
        data: {
            OrderNo: orderNo
        },
        type: "post",
        success: function (data) {
            var _data = eval('(' + data + ')');
            if (_data.Result == true) {
                strInfo = _data.Message;
                for (var i = 0; i < _data.Message.length; i++) {
                    addRow();
                }
                for (var i = 0; i < _data.Message.length; i++) {
                    $("#DeliveryCompanyName").val(_data.Message[i].DeliveryCompanyID);
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['OrderNo'] = orderNo;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['MaterielID'] = _data.Message[i].MaterielID;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['Materiel'] = _data.Message[i].Materiel;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['MaterielName'] = _data.Message[i].MaterielName;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['MaterialsSpec'] = _data.Message[i].MaterialsSpec;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['WarehouseID'] = _data.Message[i].WarehouseID;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['Unit'] = _data.Message[i].Unit;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['Quantity'] = _data.Message[i].Quantity;
                    $("#DeliveryOrderTable").datagrid('getRows')[i]['UnitID'] = (_data.Message[i].UnitID).substring(0, (_data.Message[i].UnitID).length - 1);
                    $('#DeliveryOrderTable').datagrid('refreshRow', i);
                    openEdit();
                }
                
            } else {
                $.messager.alert("信息", _data.Message);
            }
        }
    });
}

//添加行
var addRow = function () {
    var row = $(tableName).datagrid('getRows');
    $(tableName).datagrid("insertRow", {
        index: row.length, // index start with 0
        row: {
            
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

//合并单元格
var merged = false;
var MymergeCells = function () {
    setTimeout(function () {
        if (!merged) {
            mergeCells({ fields: ["OrderNo"], target: tableName });
            merged = true;
        } else {
            merged = false;
        }
    }, 10);
}

//采购计划单号、物料编码、物料名称、规格、仓库编号、单位、数量、含税价格、核实含税价格、包装情况、实收数量、备注
//列属性:field,title,width,rowspan,colspan,sortable,editor,formatter
var columnsEdit = [
    [//第一行       
        ["OrderNo", "采购计划单号", 120, 2, null, false],
        ["MaterielID", "物料编码", 100, 2, null, false],
        ["MaterielName", "物料名称", 120, 2, null, false],
        ["MaterialsSpec", "规格", 100, 2, null, false],
        ["WarehouseID", "仓库<br/>编号", 60, 2, null, false],
        ["Unit", "单位", 60, 2, null, false],
        ["Quantity", "数量", 60, 2, null, false],
        ["PriceWithTax", "含税<br/>价格", 60, 2, null, false],
        ["CkPriceWithTax", "核实<br/>含税<br/>价格", 60, 2, null, false],
        [null, "包装情况", null, null, 3, false],
        ["ActualQuantity", "实收<br/>数量", 60, 2, null, false, { type: 'validatebox' }],
        ["Remarks", "备注", 84, 2, null, false, { type: 'validatebox' }]
    ],
    [//第二行
        ["InnumQuantity", "每件<br/>数量", 60, null, null, false, { type: 'validatebox' }],
        ["Num", "件数", 60, null, null, false, { type: 'validatebox' }],
        ["Scrap", "零头", 60, null, null, false, { type: 'validatebox' }]
    ]
];

//列属性:field,title,width,rowspan,colspan,sortable,editor,formatter
var columnsCreate = [
    [//第一行       
        ["UnitID", "", null, 2],//隐藏项 
        ["Materiel","",null,2],//隐藏项 物料ID
        ["MaterielID", "物料编码", 120, 2, null, false],
        ["MaterielName", "物料名称", 120, 2, null, false],
        ["MaterialsSpec", "规格", 100, 2, null, false],
        ["WarehouseID", "仓库<br/>编号", 50, 2, null, false],
        ["Unit", "单位", 40, 2, null, false],
        ["Quantity", "数量", 60, 2, null, false],
        ["PriceWithTax", "含税<br/>价格", 60, 2, null, false],
        ["CkPriceWithTax", "核实<br/>含税<br/>价格", 60, 2, null, false],
        [null, "包装情况", null, null, 3, false],
        ["ActualQuantity", "实收数量", 80, 2, null, true, { type: 'validatebox' }],
        ["Remarks", "备注", 80, 2, null, true, { type: 'validatebox' }]
    ],
    [//第二行
        ["InnumQuantity", "每件数量", 80, null, null, true, { type: 'validatebox' }],
        ["Num", "件数", 80, null, null, true, { type: 'validatebox' }],
        ["Scrap", "零头", 80, null, null, true, { type: 'validatebox' }]
    ]
];


var merged = false;
$(function () {
    var temp = $("#data").val();//得到传过来的送货单号
    if (temp != "") {
        Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
            target: tableName,//table容器,必须
            //获取数据的地址,必须
            url: "/Purchase/DeliveryOrder/Query?DeliveryOrderID=" + temp + "",
            columns: columnsEdit,//列属性,必须
            pagination: false,
            onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件

            },
            onLoadError: function () {
                alert("请求服务器失败，请刷新页面重试!");
            },
            onCreating: function (data) {//数据成功加载
                //开启编辑
                openEdit();
                //合并单元格 采购计划单号
                MymergeCells();
            }
        });
    }
    else {
        Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
            target: tableName,//table容器,必须
            //获取数据的地址,必须
            url: "/Purchase/DeliveryOrder/Query?DeliveryOrderID='undefined'",
            columns: columnsCreate,//列属性,必须
            pagination: false,
            autoRowHeight: true,
            maxWidth: 1025,//最大宽度
            maxHeight: 350,//最大高度
            onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件

            },
            onLoadError: function () {
                alert("请求服务器失败，请刷新页面重试!");
            },
            onCreating: function (data) {//数据成功加载
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
        //隐藏 
        $('#DeliveryOrderTable').datagrid('hideColumn', 'UnitID');
        $('#DeliveryOrderTable').datagrid('hideColumn', 'Materiel');
    }
});
