//标志:添加还是修改
var addOrUpdate;
//添加、修改标志
var form4Add = "#form4Add";
//搜索标志
var formName = "#searchForm";
var model = ["add", "update"];
//显示数据的表的名字
var tableName = "#purchaseScheduleTable";
//搜索
var searchFun = function () {
    $(formName).form("submit", {//ajax form提交
        url: "/Purchase/PurchaseSchedule/Query",
        onSubmit: function (param) {//除表单外的数据(分页)
            var pager = $(tableName).datagrid("getPager");
            var pagerOptions = $(pager).pagination("options");
            param.rows = pagerOptions.pageSize;
            param.page = 1;//pagerOptions.pageNumber;
        },
        success: function (data) {
            $(tableName).datagrid("loadData", eval('(' + data + ')'));//重新加载数据
        }
    });
}

//弹出"添加外购进度信息"窗口
var createFun = function () {
    //$(form4Add).form("clear");//清空表单
    var dialog = $("#dlg");
    var form = $(form4Add);
    form.form("clear");
    dialog.find("div.ftitle").text("添加外购产品进度信息");//修改dialog标题
    dialog.dialog({ modal: true }).dialog("open");//模式化打开dialog
    addOrUpdate = model[0];
};

//弹出"修改外购进度信息"窗口,参数：外购单号
var updateFun = function (OutOrderID) {//uid
    var dialog = $("#dlg");
    var form = $(form4Update);
    OutOrderID = OutOrderID;
    form.form("clear");
    $("#comboDept").combobox("reload", "/Purchase/PurchaseSchedule/Query");
    dialog.find("div.ftitle").text("修改外购产品进度信息");
    dialog.dialog({ modal: true }).dialog("open");
    $.messager.progress();//加载进度条
    addOrUpdate = model[1];
    $.ajax({
        async: true,
        url: "/Purchase/PurchaseSchedule/Query",
        data: { OutOrderID: OutOrderID },
        dataType: "json",
        type: "post",
        success: function (data) {
            var _data = data.rows[0];
            var strDt = (_data.DeliveryDate + "").replace(/\/Date\(/, "").replace(/\)\//, "");
            if (isNaN(strDt)) {
                date = strDt.ToDate();
            } else {
                date = new Date(strDt.ToInt());
            }
            _data.DeliveryDate = date.Format("yyyy-MM-dd");
            form.form("load", _data);
            $.messager.progress("close");
        },
        error: function () {
            $.messager.progress("close");
            console.info(arguments);
            alert("请求服务器失败，请刷新页面重试!");
        }
    });
};

//确定按钮,添加和修改数据,待修改
var ensure = function () {
    if (addOrUpdate == model[0]) {//添加外购单号信息    
        $(form4Add).formSubmit([
         "/Purchase/PurchaseSchedule/Create",
            function (param) {//除表单外的数据
                return $(form4Add).form("validate");
            },
            function (data) {
                var data = eval('(' + data + ')');
                if (data.result == true) {//返回的结果(true/false)
                    $(tableName).datagrid("reload");
                    $("#dlg").dialog("close");
                } else {
                    //do something...
                    console.info(arguments);
                    alert("添加失败");
                }
                console.info(arguments);
            },
            function () {
                alert("error");
                console.info(arguments);
            }

        ]);
    } else {//更新外购进度信息
        $(form4Update).formSubmit([
            "/Purchase/PurchaseSchedule/Edit",
            function (param) {
                param.OutOrderID = OutOrderID;//把wgdh作为额外数据post 
                if ($(form4Update).find("input:checkbox").is(":checked")) {
                    param.Enabled = true;
                } else {
                    param.Enabled = false;
                }
                return $(form4Update).form("validate");
            },
            function (data) {
                var _data = eval('(' + data + ')');
                if (_data.result == true) {//返回的结果(true/false)
                    $(tableName).datagrid("reload");
                    $("#dlg").dialog("close");
                } else {
                    //do something...
                }
            }
        ]);

    }
};

//删除根据Enable修改某条记录
var deleteFun = function () {
    var row = $(tableName).datagrid('getSelections');
    if (row.length > 0) {
        var checkID = "";
        for (var i = 0; i < row.length; i++) {
            checkID += row[i].OutOrderID + ",";
        }

        checkID = checkID.substring(0, checkID.length - 1);
        //发送异步请求传递给后台我们所选中的数据
        $.messager.confirm("友情提示", "您确认要删除这些信息吗？", function (deleteOK) {
            if (deleteOK) {
                $.post("/Purchase/PurchaseSchedule/Delete", { OutOrderID: checkID }, function (data) {
                    var _data = eval('(' + data + ')');
                    console.info(_data);
                    if (_data.result == true) {
                        $(tableName).datagrid("reload");
                    } else {
                        alert("删除失败,请重新操作");
                        //do something another...
                    }
                });
            }
        });
    }
    else {
        $.messager.alert("友情提示", "请您选择要删除的数据");
    }
}

////外购单号、客户订单号、外购单位ID、产品ID、交货日期、单据要求数量、实际入库数量
//列属性:field,title,width,sortable,editor,formatter
var columns = [[
    ["OutOrderID", "外购单号", 120, true],
    ["CustomerOrderID", "客户订单号", 120, true],
    ["OutCompanyID", "外购单位ID", 120, true],
    ["ProductID", "产品ID", 120, true],
    ["DeliveryDate", "交货日期", 100, true, { type: "datebox" }, function (value, row, index) {
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
    ["RequestQuantity", "单据要求数量", 120, true],
    ["ActualQuantity", "实际入库数量", 120, true]
]]



$(function () {
    Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
        target: tableName,//table容器,必须   
        url: "/Purchase/PurchaseSchedule/Query",//获取数据的地址,必须  
        columns: columns,//列属性,必须          
        idField: "OutOrderID",//标识字段  "UId"      
        toolbar: $("#toolbar"),//工具栏               
        checkbox: true,//是否显示checkbox
        maxWidth: 950,//最大宽度
        maxHeight: 500,//最大高度
        onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件
            var arr = $(formName).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        onCreating: function (data) {//数据成功加载
            //do something...
        },
        onDblClickRow: function (rowIndex, rowData) {
            var id = rowData.OutOrderID;//key
            updateFun(id);
            return false;
        }
    });
        
});
