﻿// 被选中的行号
var sel;

// 显示供货商选择画面
var ShowCompInfoScreen = function (obj) {
    // tr对象（<tr><td><div><img/></div></td></tr>）
    var rowObj = obj.parentNode.parentNode.parentNode
    // 选中行的索引
    var idx = $(rowObj).index();
    sel = idx;
    var ppID = getProdPartID(idx);
    var compName = $($("input[name=CompName]")[idx]).val();
    ShowCompInfo("CompSelectDiv", 2, "", compName, ppID);
};


var table = "#supplierSchedulingTable";

/***********operations start***************/
// 取得指定行的产品零件ID
var getProdPartID = function (idx) {
    //得到表格中各行的值
    var row = $(table).datagrid('getRows');
    return row[idx].PAPId;
};


//排产功能
var save = function () {
    //得到表格中各行的值
    var row = $(table).datagrid('getRows');
    if (row.length > 0) {
        //定义一个数组用于存放table中的数据
        var orderList = [];
        for (var i = 0; i < row.length; i++) {
            //order用于取本行的数据
            var order = {};

            //供货商ID
            order["OutCompanyID"] = document.getElementsByName("CompId")[i].value;

            $(table).datagrid("endEdit", i);

            //外协加工调度单表，外协加工调度单详细表所需数据
            //外协加工调度单号
            order["SuppOrderID"] = "";

            //外协加工调度单详细表所需数据
            //客户订单号
            order["CustomerOrderID"] = row[i].COId.substring(0, row[i].COId.indexOf("-"));
            //客户订单明细号
            order["CustomerOrderDetailID"] = row[i].COId.substring(row[i].COId.indexOf("-") + 1);
            //产品零件ID
            order["ProductPartID"] = row[i].PAPId;
            //产品ID
            order["ProductID"] = row[i].ProductID;
            //材料规格与要求
            order["Specifica"] = row[i].MtrlFrmt;
            //工序ID
            order["PdProcID"] = row[i].PdProcID;
            //单价
            order["UnitPrice"] = row[i].UnitPrice;
            //估价
            order["Evaluate"] = row[i].EstiPrice;
            //要求数量
            order["RequestQuantity"] = row[i].PurchaseNum;
            //交货日期
            order["DeliveryDate"] = row[i].DeliveryDate;

            //外协加工调度单表所需数据
            //紧急状态
            order["UrgentStatus"] = row[i].Urgency;
            //生产部门ID
            order["DepartmentID"] = row[i].DeptId;

            //本行索引
            order["RowID"] = i + 1;
            //待购数量
            order["WaitingNum"] = row[i].WaitingNum;

            //将order加进orderList
            orderList.push(order);

        }
    }
    $.messager.confirm("提示", "您确定要将画面上显示的内容进行排产？", function (saveOK) {
        if (saveOK) {
            $.post("/Purchase/SupplierScheduling/Save", { orderList: orderList }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    $.messager.alert("信息", _data.Message, "info", function () {
                    });
                } else {
                    //title：显示在标题面板的标题文本;msg：提示框显示的消息文本;icon：提示框显示的图标,可用的值是：error,question,info,warning;fn：当窗口关闭时触发的回调函数
                    $.messager.alert("信息", _data.Message, "info", function () {
                        openEdit();
                    });
                    //do something another...
                }
            });
        }
    });
}

//添加行
var addRow = function (idx) {
    //获得表里数据
    var row = $(table).datagrid('getRows');
    //将作为基础的那一行关闭编辑状态
    $(table).datagrid("endEdit", idx);
    //判断作为基础的那一行的采购数量
    if (!(row[idx].PurchaseNum < row[idx].WaitingNum)) {

        //将作为基础的那一行开启编辑状态
        $(table).datagrid("beginEdit", idx);
        setTimeout(function () {
            // 解析datagrid
            $.parser.parse(".datagrid");
            // 刷新[操作]列的Index
            $(table).datagrid("refreshColumn", "Operation");
        }, 10);

        alert("上一行的采购数量必须小于待购数量!");
        return;
    }
    if (row[idx].PurchaseNum == 0 || row[idx].WaitingNum == null) {
        //将作为基础的那一行开启编辑状态
        $(table).datagrid("beginEdit", idx);
        setTimeout(function () {
            // 解析datagrid
            $.parser.parse(".datagrid");
            // 刷新[操作]列的Index
            $(table).datagrid("refreshColumn", "Operation");
        }, 10);
        alert("上一行的采购数量不得为0或为空!");
        return;
    }
    //增加行
    $(table).datagrid("insertRow", {
        index: idx + 1, // index start with 0
        row: {
            "Urgency": row[idx].Urgency,
            "DeptId": row[idx].DeptId,
            "PAPId": row[idx].PAPId,
            "ProductID": row[idx].ProductID,
            "COId": row[idx].COId,
            "PrdtType": row[idx].PrdtType,
            "MarterId": row[idx].MarterId,
            "MarterName": row[idx].MarterName,
            "MtrlFrmt": row[idx].MtrlFrmt,
            "PdProcID": row[idx].PdProcID,
            "StartDate": row[idx].StartDate,
            "EndDate": row[idx].EndDate,
            "WaitingNum": row[idx].WaitingNum - row[idx].PurchaseNum,//
            "Operation": "1",
            "List": row[idx].List,
            "PAPId": row[idx].PAPId
        }
    });
    //将作为基础的那一行开启编辑状态
    $(table).datagrid("beginEdit", idx);
    //将新插入的那一行开启编辑状态
    $(table).datagrid("beginEdit", idx + 1);
    setTimeout(function () {
        // 解析datagrid
        $.parser.parse(".datagrid");
        // 刷新[操作]列的Index
        $(table).datagrid("refreshColumn", "Operation");
    }, 10);
};

// 删除行
var delRow = function (idx) {
    //获得表里数据
    var row = $(table).datagrid('getRows');
    //将欲删除的那一行关闭编辑状态
    $(table).datagrid("endEdit", idx);
    //将欲修改的那一行关闭编辑状态
    $(table).datagrid("endEdit", idx - 1);
    //修改行
    $(table).datagrid('updateRow', {
        index: idx - 1,
        row: {
            "PurchaseNum": parseFloat(row[idx - 1].PurchaseNum) + parseFloat(row[idx].WaitingNum)
        }
    });
    //将欲修改的那一行开启编辑状态
    $(table).datagrid("beginEdit", idx - 1);
    //删除行
    $(table).datagrid("deleteRow", idx);
    setTimeout(function () {
        // 解析datagrid
        $.parser.parse(".datagrid");//解析datagrid
        // 刷新[操作]列的Index
        $(table).datagrid("refreshColumn", "Operation");
    }, 10);
}

//打开编辑
var openEdit = function () {
    var row = $(table).datagrid('getRows');
    if (row.length > 0) {
        for (var i = 0 ; i < row.length; i++) {
            $(table).datagrid("beginEdit", i);
        }
    }
};

/***********operations end***************/

//获得列属性
//列属性: field(字段名,非空) , title(列名) , width(默认80) , sortable(默认false) , editor(默认null) , formatter(默认-函数)
var _columns = [
    [//第一行
        ["UrgencyStatus", "", null, 2, null],//隐藏列,紧急状态
        ["DeptName", "", null, 2, null],//隐藏列,生产部门ID
        ["ProductPartID", "", null, 2, null],//隐藏列,产品零件ID(主键)
        ["ProductID", "", null, 2, null],//隐藏列,产品ID(主键)
        [null, "预排产计划", 130, null, 3],
        [null, "外协计划", 210, null, 6]

    ], [//第二行
        ["PlanDateS", "开始日", 90, null, null, false],
        ["PlanDateE", "结束日", 90, null, null, false],
        ["WaitingQuantity", "待外协数量", 80, null, null, false],
        ["Operation", "操作", 40, null, null, false, null, function (value, row, index) {
            if (value == 1) {
                return "<a href='javascript:void(0);' onclick='delRow(" + index + ");'>删除</a>";
            } else {
                return "<a href='javascript:void(0);' onclick='addRow(" + index + ");'>拆分</a>";
            }
        }],
        ["RequestQuantity", "外协数量", 80, null, null, false, { type: 'numberbox' }],
        ["Company", "外协单位", 120, null, null, false, null, function (value, row, index) {
            var ret = "<input id='CompId' name='CompId' type='hidden'/>" +
                 "<input id='CompName' type='text' name='CompName' size='10' maxlength='15' readonly='readonly'/>" +
                 "<img src='/Scripts/js/themes/icons/search_icon.png' onclick='ShowCompInfoScreen(this);'/>";
            return ret;
        }],
        ["UnitPrice", "单价", 40, null, null, false, { type: 'numberbox' }],
        ["EstiPrice", "估价", 40, null, null, false, { type: 'numberbox' }],
        ["DeliveryDate", "交货日期", null, null, null, false, { type: 'datebox', options: { editable: false } }]
    ]
];

$(function () {
    // 获取参数的排产对象
    var schejuleObj = $("#hdnScheduleObj").val();
    // 创建datagrid,详细内容参见functions.js
    Common.Functions.createDataGrid({
        // table容器,必须
        target: table,
        // 获取数据的地址,必须
        url: "/Purchase/SupplierScheduling/GetSupSchedulingInfo?schedulingObject=" + scheduleObj,
        //固定表示列
        frozenColumns: [[
            { field: 'CustOrderNo', title: '客户订单号', width: 150, align: 'left', halign: 'center' },
            { field: 'PrdtType', title: '产品型号', width: 80, align: 'left', halign: 'center' },
            { field: 'MaterialNo', title: '物料编号', width: 80, align: 'left', halign: 'center' },
            { field: 'MaterialName', title: '物料名称', width: 80, align: 'left', halign: 'center' },
            { field: 'MaterialsSpecReq', title: '材料规格和要求', width: 120, align: 'left', halign: 'center' },
            { field: 'PdProcID', title: '本道<br>工序名', width: 80, align: 'left', halign: 'center' },
            { field: 'PlanNum', title: '计划数量', width: 80, align: 'right', halign: 'center' }
        ]],
        // 列属性,必须
        columns: _columns,
        // 标识字段
        idField: "CustOrderNo",
        // 是否显示checkbox
        checkbox: false,
        // 是否同一行显示数据
        nowrap: false,
        // 分页显示
        pagination: false,
        // 最大宽度
        maxWidth: 1050,
        // 最大高度
        maxHeight: 200,
        checkOnSelect: false,
        // 加载失败
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        // 数据成功加载
        onCreating: function (data) {
            openEdit();
            setTimeout(function () {
                $.parser.parse(".datagrid");//解析datagrid
            }, 20);
        },
        loadFilter: function (data) {
            var _data = {};
            _data.rows = isUndefined(data.rows) ? data : data.rows;
            _data.total = isUndefined(data.total) ? data : data.total;
            //格式化日期
            AdaptDt(table, _data.rows, "PlanDateS");
            AdaptDt(table, _data.rows, "PlanDateE");
            AdaptDt(table, _data.rows, "DeliveryDate");
            return _data;
        }
    });
    //隐藏 产品零件ID，紧急状态，生产部门ID，产品ID 四列
    $(table).datagrid('hideColumn', 'ProductPartID');
    $(table).datagrid('hideColumn', 'UrgencyStatus');
    $(table).datagrid('hideColumn', 'DeptId');
    $(table).datagrid('hideColumn', 'ProductID');
});