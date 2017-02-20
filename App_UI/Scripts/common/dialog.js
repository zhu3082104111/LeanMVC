//-------------------------------------------------------------------------
// 显示供货商选择画面
//
// divID：   嵌套供货商选择画面的dialgo的div的ID 
// compType：供货商种类（1：外购  2：外协） 
// compID：  供货商ID
// compName：供货商名称
// pdtID：   可提供的物料ID
//-------------------------------------------------------------------------
var ShowCompInfo = function (divID, compType, compID, compName, pdtID) {
    var url = "/Technology/CompanyInfo/ShowCompInfo4Sel?compType=" + compType + "&compID=" + compID + "&compName=" + compName + "&pdtID=" + pdtID;
    $("#" + divID).dialog({
        title: "<span style='font-family:Tahoma,Verdana,微软雅黑,新宋体;font-size:14px;margin:0px;padding:0px;'>供货商选择画面</span>",
        cache: false,
        resizable: false,
        //fit: true,
        height: 400,
        href: url,
        modal: true,
        width: 500,
        //事件
        onClose: function () {
        }
    });
    $("#" + divID).dialog("open");
};


//-------------------------------------------------------------------------
// 选择供货商，返回选中的供货商，并关闭供货商子查询画面
// 
// retID:   返回的供货商ID（父画面的供货商ID<input name="retID">）
// retName：返回的供货商名称（父画面的供货商名称<input name="retName">）
// index:   索引（多行时，行号）
// divID：  要关闭的div的ID 
//-------------------------------------------------------------------------
var SelCompAndCloseDialogFun = function (retID, retName, idx, divID) {
    var cnt = $("#CompInfoTable").datagrid("getSelections").length;
    if (cnt != 1) {
        alert("请您选择所需要的供货商");
    } else {
        var selectedCompID = $("#CompInfoTable").datagrid("getSelections")[0]["CompID"];
        var selectedCompName = $("#CompInfoTable").datagrid("getSelections")[0]["CompName"];
        // 设定选择的返回值
        var idArray = $("input[name='" + retID + "']");
        $(idArray[idx]).val(selectedCompID);
        var nameArray = $("input[name='" + retName + "']");
        $(nameArray[idx]).val(selectedCompName);

        //关闭弹出框
        $("#" + divID).dialog("close");
    }
};

//-------------------------------------------------------------------------
// 并关闭dialog方法
// 
// divID：  要关闭的div的ID 
//-------------------------------------------------------------------------
var CloseDialogFun = function (divID) {
    //关闭弹出框
    $("#" + divID).dialog("close");
}

//-------------------------------------------------------------------------
// 显示物料选择画面
//
// divID：   嵌套供物料选择画面的dialgo的div的ID 
// abbrev：物料编号
// name：  物料名称
//-------------------------------------------------------------------------
var ShowProdPartInfo = function (divID, abbrev, name) {
    var url = "/Technology/ProdAndPartInfo/ShowProdAndPartInfo4Sel?abbrev=" + abbrev + "&name=" + name;
    $("#" + divID).dialog({
        title: "<span style='font-family:Tahoma,Verdana,微软雅黑,新宋体;font-size:14px;margin:0px;padding:0px;'>物料选择画面</span>",
        cache: false,
        resizable: false,
        //fit: true,
        height: 400,
        href: url,
        modal: true,
        width: 600,
        //事件
        onClose: function () {
        }
    });
    $("#" + divID).dialog("open");
};

//-------------------------------------------------------------------------
// 选择物料，返回选中的物料，并关闭物料子查询画面
// 
// retID:       返回的物料ID（父画面的物料ID<input name="retID">）
// retAbbrev:   返回的物料编号（父画面的物料编号<input name="retAbbrev">）
// retName：    返回的物料名称（父画面的物料名称<input name="retName">）
// index:       索引（多行时，行号）
// divID：      要关闭的div的ID 
//-------------------------------------------------------------------------
var SelProdPartAndCloseDialogFun = function (retID, retAbbrev, retName, idx, divID) {
    var cnt = $("#PPInfoTable").datagrid("getSelections").length;
    if (cnt != 1) {
        alert("请您选择所需要的供货商");
    } else {
        var selectedID = $("#PPInfoTable").datagrid("getSelections")[0]["Id"];
        var selectedAbbrev = $("#PPInfoTable").datagrid("getSelections")[0]["Abbrev"];
        var selectedName = $("#PPInfoTable").datagrid("getSelections")[0]["Name"];
        // 设定选择的返回值
        var idArray = $("input[name='" + retID + "']");
        $(idArray[idx]).val(selectedID);
        var idArray = $("input[name='" + retAbbrev + "']");
        $(idArray[idx]).val(selectedAbbrev);
        var nameArray = $("input[name='" + retName + "']");
        $(nameArray[idx]).val(selectedName);

        //关闭弹出框
        $("#" + divID).dialog("close");
    }
};





/**********************************杜兴军 2014-1-10 建 开始*****************************************/
var CommonDlgFun = {};
//显示“客户订单”子画面
//使用范例1: CommonDlgFun.ShowOrderDlg("#dlg")
//使用范例2: CommonDlgFun.ShowOrderDlg.call(element,"#dlg")
CommonDlgFun.ShowOrderDlg = function (dlg, orderId) {
    var $dlg = dlg ? $(dlg) : ($("#_forOrderDlg") ? $("#_forOrderDlg") : $("<div id='_forOrderDlg'></div>").appendTo("body"));
    $dlg.data("currTarget", $(this)).data("OrderID",orderId);
    $dlg.dialog({
        //draggable:false,
        cache: false,
        height: 450,
        width: 930,
        href: "/Market/OrderBill/ShowOrderBillDialog",
        modal: true,
        closed: true,
        title: "<span style=\" font-family:Tahoma,Verdana,微软雅黑,新宋体;font-size:16px;margin:0px;padding:0px; \">客户订单一览</span>",
        onBeforeOpen: function () {
            var top = $(document).scrollTop() + ($(window)._outerHeight() - $(this).dialog("options").height) / 2;
            var left = $(document).scrollLeft() + ($(window)._outerWidth() - $(this).dialog("options").width) / 2;
            if (top < 10) {
                top = $(document).scrollTop() + 10;
            }
            if (left < 10) {
                left = $(document).scrollLeft() + 10;
            }
            $(this).dialog("move", { top: top, left: left });
        },
        onClose: function() {
            //$(this).dialog("destroy",false);//销毁dialog
        }
    });
    $dlg.dialog("open");
    return $dlg;
};

//获取选中的“客户订单”记录
//返回值：null或一条记录
CommonDlgFun.GetSelectedOrder = function() {
    var $table = OrderDlgSpace ? OrderDlgSpace.GetDatagrid() : null;
    if (!$table) {
        return null;
    }
    $table = $($table);
    try {
        return $table.datagrid("getSelected");
    } catch(e) {
    }
    return null;
};

/**********************************杜兴军 2014-1-10 建 结束*****************************************/