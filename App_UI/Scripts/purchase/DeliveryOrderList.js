//搜索form
var search_Form_Name = "#Search_Form";
var tableName = "#DeliveryOrderListTable";

//-------------------------------
// 供货商选择按钮的FunCtion
//-------------------------------
var ShowCompInfoScreen = function () {
    // 取得画面上输入的供货商信息
    var inputCompName = $("#DeliveryCompany").val();
    // popup弹出供货商选择画面。
    ShowCompInfo("CompSelectDiv", 3, "", inputCompName, "");
};

//点击送货单号转到送货单页面
var showDetailFun = function (deliveryOrderNo) {
    parent.addTab("送货单", "/Purchase/DeliveryOrder/Edit?DeliveryOrderID=" + deliveryOrderNo);
};

//新建
var AddFun = function () {
    parent.addTab("送货单", "/Purchase/DeliveryOrder/Init?");
};

//查询
var search_Shd_Fun = function () {
    $(tableName).datagrid("load");
}

//删除
var DeleteFun = function () {
    //得到所选数据的外购单号
    var row = $(tableName).datagrid('getSelections');
    var checkedDeliceryOrder = "";
    if (row.length > 0) {
        for (var i = 0; i < row.length; i++) {
            //送货单号
            checkedDeliceryOrder += row[i].DeliveryOrderID + ",";
        }
        checkedDeliceryOrder = checkedDeliceryOrder.substring(0, checkedDeliceryOrder.length - 1);
    }
    $.messager.confirm("友情提示", "您确定要删除选中的送货单吗？", function (deleteOK) {
        if (deleteOK) {
            $.post("/Purchase/DeliveryOrderList/Delete", { DeliveryOrderID: checkedDeliceryOrder }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $(tableName).datagrid("reload");
                } else {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
}

//审核
var CheckFun = function () {
    //得到所选数据的送货单号
    var row = $(tableName).datagrid('getSelections');
    var check = "";
    if (row.length > 0) {
        for (var i = 0; i < row.length; i++) {
            //送货单号
            check += row[i].DeliveryOrderID + ",";
        }
        check = check.substring(0, check.length - 1);
    }
    $.messager.confirm("", "您确定要审核选中的送货单吗？", function (reviewOK) {
        if (reviewOK) {
            $.post("/Purchase/DeliveryOrderList/Audit", { DeliveryOrderID: check }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $(tableName).datagrid("reload");
                } else {
                    if (_data.Message != "") {
                        $.messager.alert("信息", _data.Message);
                    }
                }
            });
        }
    });
}


//获得列属性
var getColumns = function () {
    //列属性: field(字段名,非空) , title(列名) , width(默认80) , sortable(默认false) , editor(默认null) , formatter(默认-函数)
    var _columns = [[
        ["DeliveryOrderID", "送货单号", 120, true, null, function (value, row, index) {
            return "<a href='javascript:;'  onclick='showDetailFun(" + "\"" + value + "\"" + ");'> " + value + " </a>";
        }],
        ["DeliveryOrderType", "送货单区分", 120, true],
        ["OrderNo", "订单号", 120, true],
        ["DeliveryCompanyID", "供货商名称", 120, true],
        ["BatchID", "送货单状态", 120, true],
        ["DeliveryUID", "送货人", 80, false],
        ["DeliveryDate", "送货日期", 120, true, { type: "text" }, function (value, row, index) {
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
            row.Addate = str;
            return str;
        }],
        ["VerifyUID", "审核人", 80, false],
        ["VerifyDate", "审核日期", 120, true, { type: "datebox" }, function (value, row, index) {
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
            row.Addate = str;
            return str;
        }],
        ["IspcUID", "检查员", 80, false],
        ["PrccUID", "核价员", 80, false],
        ["WhkpUID", "仓管员", 80, false]
    ]];

    return _columns;
};

$(function () {

    //创建datagrid,详细内容参见functions.js
    Common.Functions.createDataGrid({
        //table容器,必须
        target: tableName,
        url: "/Purchase/DeliveryOrderList/Query",
        //列属性,必须    
        columns: getColumns(),
        //是否显示checkbox
        checkbox: true,
        //最大宽度
        maxWidth: 1050,
        //最大高度
        maxHeight: 300,
        singleSelect: false,
        //可以添加一些额外参数,主要用于查询条件
        onBeforeLoad: function (queryParams) {
            var arr = $(search_Form_Name).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        //数据成功加载
        onCreating: function (data) {
        }
    });
});