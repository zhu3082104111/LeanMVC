// 定义显示查询结果的Table
var table = "#purchaseAccoutingDetailsTable";

//获得列属性
var getColumns = function () {
    //列属性: field(字段名,非空) , title(列名) , width(默认80) , sortable(默认false) , editor(默认null) , formatter(默认-函数)
    var _columns = [[
            ["DeliveryOrderNo", "送货单号", 130, false],
            ["DeliveryDate", "送货日期", 90, false],
            ["DeliveryQuantity", "送货数量", 80, false],
            ["QCOrderNo", "质检单号", 130, false],
            ["QCDate", "质检日期", 90, false],
            ["QCResult", "质检结果", 80, false],
            ["StorageOrderNo", "入库单号", 130, false],
            ["StorageDate", "入库日期", 90, false],
            ["StorageQuantity", "入库数量", 80, false]
    ]];

    return _columns;
};


$(function () {
    // 取得参数的外协单号
    var supOrderNo = $("#hdnSupOrderNo").val();
    // 需要合并的对象列
    var fields = ["MaterialNo", "MaterialName", "MaterialsSpecReq", "ProcessingNo", "OrderQuantity", "OrderDate"];
    // 创建datagrid
    Common.Functions.createDataGrid({
        // table容器,必须
        target: table,
        // 获取数据的地址,必须
        url: "/Purchase/SupplierAccoutingDetail/ShowSupplierAccoutingDetail?supOrderNo=" + supOrderNo,
        // 列属性,必须
        columns: getColumns(),
        //固定表示列
        frozenColumns: [[
            { field: 'MaterialNo', title: '物料编号', width: 80, align: 'left', halign: 'center' },
            { field: 'MaterialName', title: '物料名称', width: 120, align: 'left', halign: 'center' },
            { field: 'ProcessingNo', title: '加工工艺', width: 80, align: 'left', halign: 'center' },
            { field: 'MaterialsSpecReq', title: '材料规格及要求', width: 120, align: 'left', halign: 'center' },
            { field: 'OrderQuantity', title: '订货数量', width: 80, align: 'right', halign: 'center' },
            { field: 'OrderDate', title: '交货日期', width: 90, align: 'center', halign: 'center' }
        ]],
        //标识字段
        idField: "MaterialNo",
        //分页显示
        pagination: false,
        //是否显示checkbox
        checkbox: false,
        //是否同一行显示数据
        nowrap: false,
        //最大宽度
        maxWidth: 2000,
        //最大高度
        maxHeight: 260,
        singleSelect: false,
        //加载失败
        onLoadError: function () {
            alert("请求服务器失败，请刷新页面重试!");
        },
        //数据成功加载 
        onCreating: function (data) {
            //合并单元格
            setTimeout(function () { mergeCells({ fields: fields, target: table }) }, 10);
        },
        loadFilter: function (data) {
            var _data = {};
            _data.rows = isUndefined(data.rows) ? data : data.rows;
            _data.total = isUndefined(data.total) ? data : data.total;
            //格式化日期
            AdaptDt(table, _data.rows, "OrderDate");
            AdaptDt(table, _data.rows, "DeliveryDate");
            AdaptDt(table, _data.rows, "QCDate");
            AdaptDt(table, _data.rows, "StorageDate");
            return _data;
        }
    });
});