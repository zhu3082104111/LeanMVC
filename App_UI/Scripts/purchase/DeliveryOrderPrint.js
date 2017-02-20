//搜索标志
var formName = "#searchForm";
//显示数据的表的标志
var tableName = "#DeliveryOrderTable";

var searchBox = function (value, name) {
    alert(value + ":" + name);
}

//打印
var printInfo = function ()
{
    var deliveryOrderID = $("#data").val();
    $.messager.confirm("友情提示", "您确定要打印画面上的内容吗？", function (printOK) {
        if (printOK) {
            $.post("/Purchase/DeliveryOrder/PrintInfo", { DeliveryOrderID: deliveryOrderID }, function (data) {
                var _data = eval('(' + data + ')');
                console.info(_data);
                if (_data.Result == true) {
                    self.print();//打印
                } else {
                    $.messager.alert("信息", _data.Message);
                }
            });
        }
    });
    
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

var merged = false;
//采购计划单号、物料编码、物料名称、规格、仓库编号、单位、数量、含税价格、核实含税价格、包装情况、实收数量、备注
//列属性:field,title,width,rowspan,colspan,sortable,editor,formatter
var columns = [
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
        ["ActualQuantity", "实收<br/>数量", 60, 2, null, false],
        ["Remarks", "备注", 80, 2, null, false]
    ],
    [//第二行
        ["InnumQuantity", "每件<br/>数量", 60, null, null, false],
        ["Num", "件数", 60, null, null, false],
        ["Scrap", "零头", 60, null, null, false]
    ]
];

$(function () {
    var temp = $("#data").val();//得到传过来的送货单号
    Common.Functions.createDataGrid({//创建datagrid,详细内容参见functions.js
        target: tableName,//table容器,必须
        //获取数据的地址,必须
        url: "/Purchase/DeliveryOrder/Query?DeliveryOrderID=" + temp + "",
        columns: columns,//列属性,必须
        maxWidth: 1090,
        pagination: false,
        onBeforeLoad: function (queryParams) {//可以添加一些额外参数,主要用于查询条件

        },
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        onCreating: function (data) {//数据成功加载
            //开启编辑
            //openEdit();
            //数据可能有多行，共同部分取第一行的值
            //$("#processForm").form("load", data.rows[0]);
            //合并单元格 采购计划单号
            MymergeCells();
        }
    });

});