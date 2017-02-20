//搜索结果的Table
var table = "#resultShowTable";
//搜索条件的form
var form = "#searchForm";

/***********operations start***************/
//按搜索条件查询
var searchInfo = function () {
	$(form).form("submit", {//form提交
		url: "/Purchase/SupplierOrderList/Query",
		onSubmit: function (param) {//除表单外的数据(分页)
			var pager = $(table).datagrid("getPager");
			var pagerOptions = $(pager).pagination("options");
			param.rows = pagerOptions.pageSize;
			param.page = 1;
		},
		success: function (data) {
			$(table).datagrid("loadData", eval('(' + data + ')'));//重新加载数据
		}
	});
};

//转到详细计划页面 + <param name="id">跳转到调度单画面所需的调度单号</param>
var showDetailFun = function (id) {
	parent.addTab("外协加工调度单-编辑", "/Purchase/SupplierOrder/SupplierOrderForEdit?supplierOrderId=" + id);
};

//转到新建画面
var AddFun = function () {
	parent.addTab("外协加工调度单-新建", "/Purchase/SupplierOrder/SupplierOrderForCreat");
}

//删除数据
var DeleteFun = function () {
    //获取选中的行
    var row = $(table).datagrid('getSelections');

    var Check = "";//存放需操作的号，以“，”隔开
    for (var i = 0; i < row.length; i++) {
        Check += row[i].SupOrderID + ",";
    }
    if (Check!= "")
    {
        Check = Check.substring(0, Check.length - 1);//不要最后的“，”号
    }
    $.messager.confirm("友情提示", "您确定要删除选中的外协单吗？", function (deleteOK) {
        if (deleteOK) {
            $.post("/Purchase/SupplierOrderList/Delete", { supOrderID: Check }, function (data) {
                var _data = eval('(' + data + ')');//取function得返回值
                if (_data.result == true) {
                    $.messager.alert("信息", _data.Message, "info");
                    $(table).datagrid("reload");//重新加载
                } else {
                    //alert("删除失败,请重新操作");
                    //do something another...
                    $.messager.alert("信息", _data.Message, "info");
                }
            });
        }
    });

};

// 审核数据
var AuditFun = function () {
    // 获取选中的行
    var row = $(table).datagrid('getSelections');
	// 存放操作的调度单号
    var check = "";
    // 将选中的号存放到  check 里
    for (var i = 0; i < row.length; i++) {
        check += row[i].SupOrderID + ",";
    }
    //是否有 有效数据
    if (check != "") {
        //出去最后的“，”号
        check = check.substring(0, check.length - 1);
    }
    $.messager.confirm("", "您确定要审核选中的外协单吗？", function (reviewOK) {
        if (reviewOK) {
            $.post("/Purchase/SupplierOrderList/Audit", { supOrderID: check }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.result == true) {
                    $.messager.alert("信息", _data.Message, "info");
                    $(table).datagrid("reload");//重新加载数据
                    //alert("hello!");
                } else {
                    $.messager.alert("信息", _data.Message, "info");
                }
            });
        }
    });


};

//
var closeFun = function () {

};

// 【供货商选择按钮】按下
var ShowCompInfoScreen = function () {
    var inputCompName = $("#InCompanyName").val();
    ShowCompInfo("CompSelectDiv", 2, "", inputCompName, "");
};

/***********operations end***************/

//获得列属性

//列属性: field(字段名,非空) , title(列名) , width(默认80) , sortable(默认false) , editor(默认null) , formatter(默认-函数)
var columns = [[
    ["SupOrderID", "外协单号", 130, true, null, function (value, row, index) {
        var htmlStr = "";
        htmlStr += ("<a href='#' class='prduplanNo' onclick='showDetailFun(" + "\"" + value + "\"" + ")';" + ">" + value + "</a>");
        return htmlStr;
    }],
    ["OrderType", "单据种类", 110, true, null],
    ["UrgentStatus", "紧急状态", 80, true, null],
    ["SupOrderStatus", "当前状态", 80, true, null],
    ["DepartmentName", "生产部门", 80, true],
    ["InCompanyName", "调入单位", 130, false],
    ["MarkName", "制单人", 80, false],
    ["MarkDate", "制单日期", 100, true, { type: "datebox" }, function (value, row, index) {
        if (row.MarkDate != null) {
            var date;
            var strDt = (value + "").replace(/\/Date\(/, "").replace(/\)\//, "");
            if (isNaN(strDt)) {
                date = strDt.ToDate();
            } else {
                date = new Date(strDt.ToInt());
            }
            var str = date.Format("yyyy-MM-dd");
            row.Addate = str;
            return str;
        }
        else {
            return "";
        }

    }],
     ["PrdMngrName", "生产主管", 80, false],
     ["OptrName", "经办人", 80, false]

]]

$(function () {
    Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
        target: $(table),//table容器,必须
        url: "/Purchase/SupplierOrderList/Query",//获取数据的地址,必须
        columns: columns,//列属性,必须   
        checkbox: true,//是否显示checkbox
        maxWidth: 1020,//最大宽度
        maxHeight: 200,//最大高度
        onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件
            var arr = $(form).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试！");
        },
        onCreating: function (data) {//数据成功加载
            //do something...
        },
        loadFilter: function (data) {
            //gridview 翻页的页码
            var _data = {};
            _data.rows = isUndefined(data.rows) ? data : data.rows;
            _data.total = isUndefined(data.total) ? data : data.total;
            return _data;
        }
    }); 
});

