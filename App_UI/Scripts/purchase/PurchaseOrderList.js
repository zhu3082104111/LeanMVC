// 定义查询条件的Form
var formName = "#searchForm";

// 定义显示查询结果的Table
var tableName = "#purchaseOrderListTable";

/********************* operations start *************************/
// 【供货商选择】按钮按下的事件
var ShowCompInfoScreen = function () {
    var inputCompName = $("#OutCompanyName").val();
    ShowCompInfo("CompSelectDiv", 1, "", inputCompName, "");
};

// 【查询】按钮按下后的事件
var searchInfo = function () {
    //ajax form提交
    $(formName).form("submit", {
        url: "/Purchase/PurchaseOrderList/Query",
        //除表单外的数据(分页)
        onSubmit: function (param) {
            var pager = $(tableName).datagrid("getPager");
            var pagerOptions = $(pager).pagination("options");
            param.rows = pagerOptions.pageSize;
            param.page = 1;
        },
        success: function (data) {
            //重新加载数据
            $(tableName).datagrid("loadData", eval('(' + data + ')'));
        }
    });
}

// 【添加】按钮按下的事件
var create = function () {
    parent.addTab("外购产品计划单", "/Purchase/PurchaseOrder/Create");
};

// 【外购单号】链接按下后的事件
var edit = function (id) {
    parent.addTab("外购产品计划单", "/Purchase/PurchaseOrder/Edit?OutOrderID=" + id);
};

// 【删除】按钮按下的事件
var deleteFun = function () {
    // 取得画面上的选择对象
    var row = $(tableName).datagrid('getSelections');
    // 未选择删除对象时处理中断，显示错误Message
    if (row == 0) {
        alert("您未选择删除对象。请您选择要删除的对象后再进行删除操作");
        return;
    }

    // 取得所选删除对象的外购单号
    var outOrderID = "";
    if (row.length > 0) {
        for (var i = 0; i < row.length; i++) {
            outOrderID += row[i].OutOrderID + ",";//外购单号
        }
        outOrderID = outOrderID.substring(0, outOrderID.length - 1);
    }

    // 进行删除处理
    $.messager.confirm("友情提示", "您确定要删除选中的外购单吗？", function (deleteOK) {
        if (deleteOK) {
            $.post("/Purchase/PurchaseOrderList/Delete", { OutOrderID: outOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $(tableName).datagrid("reload");
                }
                else {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
}

// 【批准】按钮按下后的事件 
var approve = function () {
    // 取得画面上的选择对象
    var row = $(tableName).datagrid('getSelections');
    // 未选择批准对象时处理中断，显示错误Message
    if (row == 0) {
        alert("您未选择批准对象。请您选择要批准的对象后再进行批准操作");
        return;
    }

    // 取得所选删除对象的外购单号
    var outOrderID = "";
    if (row.length > 0) {
        for (var i = 0; i < row.length; i++) {
            outOrderID += row[i].OutOrderID + ",";//外购单号
        }
        outOrderID = outOrderID.substring(0, outOrderID.length - 1);
    }

    // 进行批准处理
    $.messager.confirm("", "您确定要批准选中的外购单吗？", function (approveOK) {
        if (approveOK) {
            $.post("/Purchase/PurchaseOrderList/Approve", { OutOrderID: outOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $(tableName).datagrid("reload");
                }
                if (_data.Message != "") {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
}

// 【审核】按钮按下后的事件
var review = function () {
    // 取得画面上的选择对象
    var row = $(tableName).datagrid('getSelections');
    // 未选择审核对象时处理中断，显示错误Message
    if (row == 0) {
        alert("您未选择审核对象。请您选择要审核的对象后再进行审核操作");
        return;
    }

    // 取得所选删除对象的外购单号
    var outOrderID = "";
    if (row.length > 0) {
        for (var i = 0; i < row.length; i++) {
            outOrderID += row[i].OutOrderID + ",";//外购单号
        }
        outOrderID = outOrderID.substring(0, outOrderID.length - 1);
    }

    // 进行审核处理
    $.messager.confirm("", "您确定要审核选中的外购单吗？", function (reviewOK) {
        if (reviewOK) {
            $.post("/Purchase/PurchaseOrderList/Audit", { OutOrderID: outOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $(tableName).datagrid("reload");
                }
                if (_data.Message != "") {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
}
/********************* operations end *************************/

//列属性:field,title,width,sortable,editor,formatter 
var columns = [[
    ["OutOrderID", "外购单号", 130, true, { type: "validatebox" }, function (value, row, index) {
        return "<a href='javascript:;'   onclick='edit("+"\""+value+"\""+");'> " + value + " </a>";
    }],
    ["UrgentStatus", "紧急状态", 80, true],
    ["DeptName", "生产部门", 80, true],
    ["CompName", "供货商", 150, true],
    ["OutOrderStatus", "当前状态", 80, true],
    ["EstablishUName", "编制人", 80, true],
    ["EstablishDate", "编制日期", 100, true, { type: "datebox" }, function (value, row, index) {
        //如果日期为空则显示为空
        if (!value) {
            return "";
        }
        var date;
        var strDt = (value + "").replace(/\/Date\(/, "").replace(/\)\//, "");
        if (isNaN(strDt)) {
            date = strDt.ToDate();
        } else {
            date = new Date(strDt.ToInt());
        }
        var str = date.Format("yyyy-MM-dd");
        row.EstablishDate = str;
        return str;
    }],
    ["ApproveDate", "批准日期", 100, true, { type: "datebox" }, function (value, row, index) {
        //如果日期为空则显示为空
        if (!value) {
            return "";
        }
        var date;
        var strDt = (value + "").replace(/\/Date\(/, "").replace(/\)\//, "");
        if (isNaN(strDt)) {
            date = strDt.ToDate();
        } else {
            date = new Date(strDt.ToInt());
        }
        var str = date.Format("yyyy-MM-dd");
        row.ApproveDate = str;
        return str;
    }],
    ["Remarks", "备注", 110, true],
    ["OutOrderStatusCd", "", null, false]//隐藏项 当前状态
]]

$(function () {
    Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
        target: tableName,//table容器,必须
        url: "/Purchase/PurchaseOrderList/Query",//获取数据的地址,必须
        columns: columns,//列属性,必须
        idField: "OutOrderID",//标识字段
        checkbox: true,//是否显示checkbox
        maxWidth: 950,//最大宽度
        maxHeight: 200,//最大高度
        onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件
            var arr = $(formName).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试！");
        },
        onCreating: function (data) {//数据成功加载
            //do something...
        }
    });
    //隐藏 当前状态
    $('#purchaseOrderListTable').datagrid('hideColumn', 'OutOrderStatusCd');
});
    
