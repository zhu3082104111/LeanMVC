// 定义查询条件的Form
var form = "#searchForm";

// 定义显示查询结果的Table
var table = "#supplierAccoutingListTable";

/***********operations start***************/
// 【物料编号选择按钮】按下的事件
var ShowProdPartInfoScreenByAbbrev = function () {
    ShowProdPartInfo("ProdPartSelectDiv", $("#MaterielNo").val(), "");
};

// 【物料名称选择按钮】按下的事件
var ShowProdPartInfoScreenByName = function () {
    ShowProdPartInfo("ProdPartSelectDiv", "", $("#MaterielName").val());
};

// 【供货商选择按钮】按下的事件
var ShowCompInfoScreen = function () {
    var inputCompName = $("#SupCompName").val();
    ShowCompInfo("CompSelectDiv", 2, "", inputCompName, "");
};

// 【外协单号】链接按下的事件
var getDetails = function (supOrderNo) {
    parent.addTab("外协计划台帐详细", "/Purchase/SupplierAccoutingDetail/ShowDetail?supOrderNo=" + supOrderNo);
};

// 【查询】按钮的事件
var search_Shd_Fun = function () {
    $(table).datagrid("load");
}
/***********operations end***************/

//获得列属性
var getColumns = function () {
    //列属性: field(字段名,非空) , title(列名) , width(默认80) , sortable(默认false) , editor(默认null) , formatter(默认-函数)
    var _columns = [[
            ["UrgentStatus", "紧急<br>状态", 50, true],
            ["SupOrderNo", "外协单号", 130, true, { type: "validatebox" }, function (value, row, index) {
                return "<a href='javascript:;'  onclick='getDetails(" + "\"" + value + "\"" + ");'> " + value + " </a>";
            }],
            ["SupCompName", "外协单位", 100, true],
            ["MaterialNo", "物料编号", 80, true],
            ["MaterialName", "物料名称", 120, false],
            ["ProcessID", "加工<br>工艺", 80, true],
            ["MaterialsSpecReq", "材料规格及要求", 120, false],
            ["PlanQuantity", "订货数量", 80, false],
            ["DeliveryDate", "交货日期", 80, true],
            ["ArrivalQuantity", "到货累计", 80, false],
            ["MarginQuantity", "订单差额", 80, false],
            ["CompletStatus", "完成状态", 80, true],
            ["Remarks", "备注", 80, false]
    ]];

    return _columns;
};

$(function () {
    Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
        target: table,//table容器,必须
        //获取数据的地址,必须
        url: "/Purchase/SupplierAccoutingList/Query",
        columns: getColumns(),//列属性,必须
        checkbox: false,//是否显示checkbox
        //最大宽度
        maxWidth: 950,
        //最大高度
        maxHeight: 200,
        singleSelect: false,
        onBeforeLoad: function (queryParams) {
            var arr = $(form).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        // 数据加载过滤
        loadFilter: function (data) {
            var _data = {};
            _data.rows = isUndefined(data.rows) ? data : data.rows;
            _data.total = isUndefined(data.total) ? data : data.total;
            AdaptDt(table, _data.rows, "DeliveryDate")
            return _data;
        },
        // 显示行的背景色的设置
        rowStyler: function (index, row) {
            var bgColorFlg = row["BGColorFlag"];
            if (bgColorFlg == "Y") {
                return "background-color:yellow;";
            } else if (bgColorFlg == "R") {
                return "background-color:#DC143C;";
            }
        }
    });
});