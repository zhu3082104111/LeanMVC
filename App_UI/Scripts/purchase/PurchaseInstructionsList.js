// 定义查询条件的Form
var search_Form_Name = "#Search_Form";

// 定义显示查询结果的Table
var data_Table_Name = "#Data_Table";

/***********operations start***************/
// 【查询】按钮按下的事件
var search_Btn_Fun = function () {
    $(data_Table_Name).datagrid("load");
};

// 【全选checkbox】事件
var selFun = function () {
    var isChecked = document.getElementById("checkAll").checked;
    if (isChecked == true) {
        $("input[name='chk_list']").attr("checked", true);
    } else {
        $("input[name='chk_list']").attr("checked", false);
    }
};

// 【物料编号选择按钮】按下的事件
var ShowProdPartInfoScreenByAbbrev = function () {
    ShowProdPartInfo("ProdPartSelectDiv", $("#MaterialNo").val(), "");
};

// 【物料名称选择按钮】按下的事件
var ShowProdPartInfoScreenByName = function () {
    ShowProdPartInfo("ProdPartSelectDiv", "", $("#MaterialName").val());
};

// 【供货商选择按钮】按下的事件
var ShowCompInfoScreen = function () {
    var inputCompName = $("#CompName").val();
    ShowCompInfo("CompSelectDiv", 1, "", inputCompName, "");
};

// 【外购单号】链接按下的事件
var showOutOrderInfo = function (outOrderNo) {
    parent.addTab("外购产品计划单", "/Purchase/PurchaseOrder/Edit?OutOrderID=" + outOrderNo);
};

// 【合并排产】按钮按下的事件
var showPurchaseSchedulingFun = function () {
    var row = $(data_Table_Name).datagrid('getRows');
    //数组，所有checkBox的值
    var checkBox = document.getElementsByName("chk_list");
    //选中/未选中区分标志
    var bo = false;
    $(data_Table_Name).datagrid("endEdit", i);
    if (row.length > 0) {
        //postList用于存放table中的数据
        var schedulingList = [];
        //deptNameList用于存放已知的部门名称
        var deptNameList = [];
        for (var i = 0; i < checkBox.length; i++) {
            //post用于存放一行中需要的数据
            var scheduling = {};
            //如果checkBox被选中
            if (checkBox[i].checked) {
                //区分标志设为true
                bo = true;
                //获得当前行部门名称
                var deptName = row[checkBox[i].value].DeptName;
                //如果取得是第一行
                if (i == 0) {
                    //获得需要的数据
                    scheduling["CustomerOrderID"] = row[checkBox[i].value].CustOrderNo.substring(0, row[checkBox[i].value].CustOrderNo.indexOf("-"));
                    scheduling["CustomerOrderDetailID"] = row[checkBox[i].value].CustOrderNo.substring(row[checkBox[i].value].CustOrderNo.indexOf("-") + 1);
                    scheduling["ProductPartID"] = row[checkBox[i].value].ProductPartID;
                    //将post加进postList
                    schedulingList.push(scheduling);
                    //当前行部门名称放进deptNameList，备下一轮检索
                    deptNameList.push(deptName);
                    //如果取得不是第一行
                } else {
                    //用当前deptName检索deptNameList,发现不同的部门名称并终止操作
                    for (var j = 0; j < deptNameList.length; j++) {
                        if (deptName != deptNameList[j]) {
                            alert("生产部门不同不能合并排产！");
                            return null;
                        }
                    }
                    //若部门名称相同，便获得需要的数据
                    scheduling["CustomerOrderID"] = row[checkBox[i].value].CustOrderNo.substring(0, row[checkBox[i].value].CustOrderNo.indexOf("-"));
                    scheduling["CustomerOrderDetailID"] = row[checkBox[i].value].CustOrderNo.substring(row[checkBox[i].value].CustOrderNo.indexOf("-") + 1);
                    scheduling["ProductPartID"] = row[checkBox[i].value].ProductPartID;
                    //将post加进postList
                    schedulingList.push(scheduling);
                }
            }
        }
        //如果未选，提示并终止操作
        if (bo == false) {
            alert("未选中任何外购计划！");
            return null;
        }
    }
    //将欲传递的数据(各记录的三个主键)封装成字符串
    var schedulingObject = "";
    for (var i = 0; i < schedulingList.length; i++) {
        // 将客户订单号，客户订单详细号，产品零件ID拼接成字符串，每个记录以[;]分号隔开
        schedulingObject += schedulingList[i]["CustomerOrderID"] + schedulingList[i]["CustomerOrderDetailID"] + schedulingList[i]["ProductPartID"] + ";";
    }
    //传递给外购排产界面
    toPurchaseScheduling(schedulingObject, deptName);
};

// 实现跳转，进入外购排产页面
var toPurchaseScheduling = function (schedulingObject, deptName) {
    parent.addTab("外购排产", "/Purchase/PurchaseScheduling/PurchaseScheduling?schedulingObject=" + schedulingObject + "&deptName=" + deptName);
};
/***********operations end***************/

//获得列属性
var TABLE_COLUMNS = [
    [//第一行
        ["ProductPartID", "", null, 2, null],//隐藏列,产品零件ID(主键)
        ["AllSelect", "全选<br/><input type = 'checkbox' id = 'checkAll' name = 'checkAll' value = '' onclick='selFun();'/>", 50, 2, null, false, null, function (value, row, index) {
            //判断待购数量是否为0，根据情况添加checkBox
            if (row.WaitingNum > 0) {
                return "<input type = 'checkbox' name = 'chk_list' value = '" + index + "'/>";
            }
            if (row.WaitingNum == 0) {
                return "-";
            }
        }],
        ["CustOrderNo", "客户订单号", 140, 2, null, true, null],
        ["DeptName", "生产部门", 80, 2, null, true, null],
        ["PrdtType", "产品型号", 80, 2, null, true],
        ["MaterialNo", "物料编号", 80, 2, null, true],
        ["MaterialName", "物料名称", 100, 2, null],
        ["MaterialsSpecReq", "材料规格和要求", 120, 2, null],
        ["PlanQuantity", "计划<br>数量", 80, 2, null],
        [null, "预排产计划", 180, null, 2],
        [null, "排产状态", 200, null, 2]
    ],
    [//第二行
        ["PlanDateS", "开始日", 90, null, null, true],
        ["PlanDateE", "结束日", 90, null, null, true],
        ["OutOrderList", "外购计划单号", 120, null, null, false, { type: "validatebox" }, function (value, row, index) {
            var str = "";
            for (var i = 0, len = value.length; i < len; i++) {
                if (i == len) {
                    str += "<a href='javascript:void(0);'  onclick='showOutOrderInfo(" + "\"" + value[i]["OutOrderID"] + "\"" + ");'>" + value[i]["OutOrderID"] + "</a>";
                } else {
                    str += "<a href='javascript:void(0);'  onclick='showOutOrderInfo(" + "\"" + value[i]["OutOrderID"] + "\"" + ");'>" + value[i]["OutOrderID"] + "</a><br/>";
                }
            }
            //若无返回的外购计划单号，显示“未排产”
            if (str == "") {
                return "未排产";
            } else {
                return str;
            }
        }],
        ["WaitingQuantity", "待购数量", 80, null, null, false, null]
    ]
];

$(function () {
    // 创建datagrid
    Common.Functions.createDataGrid({
        // table容器,必须
        target: data_Table_Name,
        // 获取数据的地址,必须
        url: "/Purchase/PurchaseInstructionList/Query",
        // 列属性,必须
        columns: TABLE_COLUMNS,
        // 标识字段
        idField: "CustOrderNo",
        // 是否同一行显示数据
        nowrap: false,
        // 是否显示checkbox
        checkbox: false,
        // 最大宽度
        maxWidth: 1000,
        // 最大高度
        maxHeight: 200,
        // 加载前
        onBeforeLoad: function (queryParams) {
            // 写入查询条件
            var arr = $(search_Form_Name).serializeArray();
            $.each(arr, function () {
                queryParams[this.name] = this.value;
            });
        },
        // 加载失败
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        // 数据成功加载
        onCreating: function (data) {

        },
        loadFilter: function (data) {
            var _data = {};
            _data.rows = isUndefined(data.rows) ? data : data.rows;
            _data.total = isUndefined(data.total) ? data : data.total;
            //格式化日期
            AdaptDt(data_Table_Name, _data.rows, "PlanDateS");
            AdaptDt(data_Table_Name, _data.rows, "PlanDateE");
            return _data;
        }
    });
    //隐藏 产品零件ID
    $(data_Table_Name).datagrid('hideColumn', 'ProductPartID');
});