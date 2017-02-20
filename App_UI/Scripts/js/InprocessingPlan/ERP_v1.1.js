/*
    注意:css要求：
    position:absolute/relative;（必须）  父节点left：必须赋值，子节点（两个，左、右）left：必须赋值

    初始化：new new ERP.Util(obj);
    obj的属性包括：
        provider：html节点，父节点，主要用于控制位置，宽度/高度均设为auto(必须)
        scroller：html节点，滚动条所在节点
        parentId：string，父节点id
        headArray：array，表头文字集合
        refreshAll：function，根据div位置刷新所有数据
        refreshValue：function，根据div位置刷新指定数据
*/
var ERP = new Object();//命名空间
var caller_Position = null; //拖动对象--位置
var caller_Range = null;//拖动对象--宽度
var tempX; //鼠标X坐标与DIV LEFT的距离，保持相对一致
var delegate = null;//方法委托,用于动态调用右移和左移函数
ERP.Variables = new Object();//声明空间(命名空间)
//ERP.Variables.Parent = null;
ERP.Variables.scroller = null;//存放滚动条所在父节点
ERP.Variables.scrollerMaxLeft = null;//滚动条的最大位置，用于确定div的移动范围
ERP.Variables.userParams = null;//??
ERP.Variables.headerColumns = null;//表头信息，用于确定div的位置
ERP.Variables.headerArray = null;//??
ERP.Variables.viewType = null;//视图类型
ERP.Variables.tableWidth = null;//Table宽度
ERP.Variables.columnsWidth = null;//??
ERP.Variables.tableHeight=null;
//ERP.Variables.columnsLength = null;
ERP.Variables.data = null;//??
ERP.Variables.lastData = null;//记录dblclick事件中改变之前的值，用于不符合条件时还原
ERP.Variables.minDate = null;//记录最小日期，用于估算滚动条的位置

ERP.Variables.changedRecord = [];//记录改变的记录

ERP.Variables.UserIds = {fyy:1,dxj:2};
ERP.Variables.UserId = null;

ERP.parent = null;//外层节点
ERP.parentId = null;//外层节点ID

ERP.Functions = new Object();
ERP.Functions.refreshAll = null;//?
ERP.Functions.refreshValue = null;//?
ERP.Functions.userFunction = null;//?

//字段集合
ERP.Variables.fieldNames = {
    id: "id", startDate: "startDate", endDate: "endDate"
};

//获取滚动条的最大长度
ERP.Functions.scrollMaxLeft = function() {
    var currentLeft = $(ERP.Variables.scroller).scrollLeft();
    $(ERP.Variables.scroller).scrollLeft(1000000);
    var maxLeft = $(ERP.Variables.scroller).scrollLeft();
    $(ERP.Variables.scroller).scrollLeft(currentLeft);
    return maxLeft;
};

//工具类
ERP.Util = function(obj) { //   

    ERP.Util.Initialize(obj);
    
    if (checkNull(obj.provider) != null) {
        ERP.parent = $(obj.provider);
    }
    if (checkNull(obj.scroller) != null) {
        ERP.Variables.scroller = $(obj.scroller);
        $(obj.scroller).scrollLeft(1000000);
        ERP.Variables.scrollerMaxLeft = $(obj.scroller).scrollLeft();
        $(obj.scroller).scrollLeft(0);
    }
    ERP.Variables.scroller = ERP.parent.find("div[class='e-table-viewport']");
    //this.set = function(obj) {
    //    var s = new set(obj);
    //};
    //this.set(obj);

    setTimeout(function() {
        new divInitializeAll();
        ERP.Variables.scrollerMaxLeft = ERP.Functions.scrollMaxLeft();
    }, 1);
};

//初始化
ERP.Util.Initialize = function(obj) {
    if (!ERP.Variables.UserId) {
        ERP.Variables.UserId = ERP.Variables.UserIds.dxj;
    }
    document.onmouseup = function(e) {
        var caller = caller_Position || caller_Range;
        if (caller) {
            //ERP.Variables.Parent = caller;
            $(document).off().on("selectstart", function(e) { return true; });
            $("body").off().on("dragstart", function(e) { return true; });
            document.all ? caller.releaseCapture() : window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            //$("body").removeClass(".noselect");
            if (caller_Position) {
                $(caller_Position).css("cursor", "default");
            }
            $("body").css("-moz-user-select", "-moz-all");
            if (ERP.Functions.userFunction != null) {
                userFun = new ERP.Functions.userFunction(ERP.Variables.userParams);
            }

            ERP.Variables.changedRecord=ERP.Variables.changedRecord.Distinct();
            caller_Position = null;
            caller_Range = null;
        }
    };

    document.onmousemove = function(e) {
        if (caller_Position || caller_Range) {
            //(e || event).stopPropagation();
        }
        if (!caller_Position && caller_Range) { //改变宽度
            delegate(caller_Range, e);
            var id = $(caller_Range.parentNode).attr('sourceID');
            refreshValue(id);

            var fieldNames = ERP.Variables.fieldNames;//字段
            ERP.Variables.changedRecord.push(id);
            var ids = id.split(",");
            var record = null;
            switch (ERP.Variables.UserId) {
                case ERP.Variables.UserIds.fyy:
                    var obj = {};
                    obj[fieldNames.ids.orderId] = ids[0];
                    obj[fieldNames.ids.orderDetail] = ids[1];
                    obj[fieldNames.ids.exportId] = ids[2];
                    record = ERP.Variables.data.FindByObject(obj)[0];
                    //record = ERP.Variables.data.FindByObject({ "ClientOrderID": ids[0], "OrderNumber": ids[1] })[0];
                    break;
                case ERP.Variables.UserIds.dxj:
                    var obj = {};
                    obj[fieldNames.ids.orderId] = ids[0];
                    obj[fieldNames.ids.orderDetail] = ids[1];
                    obj[fieldNames.ids.exportId] = ids[2];
                    record = ERP.Variables.data.FindByObject(obj)[0];
                    break;
            }
            if (!record) {
                return;
            }
            var target = $("*[targetID='" + id + "']");
            var _fieldNames = ERP.Variables.fieldNames;
            record[_fieldNames.startDate] = $(target[0]).val();
            record[_fieldNames.endDate] = $(target[1]).val();
            return;
        }
        if (caller_Position && (!caller_Range)) { //改变位置
            var div = $(caller_Position);
            var erpParentNode = ERP.parent; //外层节点（最大范围）
            var e = e || event;
            var clientX = e.clientX;
            var left = clientX - tempX; //相对于可视窗口的left
            var erpParentLeft = erpParentNode.offset().left;
            var erpScrollLeft = 0;
            if (ERP.Variables.scroller != null) {
                erpScrollLeft = ERP.Variables.scroller.scrollLeft();
            }
            if (left + erpScrollLeft <= erpParentLeft) { //左边界
                div.css("left", 0 + "px");
            } else {
                var right = left + parseInt(div.width()); //clientX+div.width()-tempX;
                var maxRight = erpParentNode.width() + erpParentNode.offset().left + ERP.Variables.scrollerMaxLeft;
                if (right + erpScrollLeft >= maxRight) {
                    div.css("left", maxRight - div.width() - erpParentLeft - 5);
                    ERP.Variables.scroller.scrollLeft(ERP.Variables.scrollerMaxLeft - 2);
                } else {
                    div.css("left", (left - erpParentLeft + erpScrollLeft) + "px");
                }
            }

            var id = div.attr("sourceID");
            refreshValue(id);

            var fieldNames = ERP.Variables.fieldNames;
            ERP.Variables.changedRecord.push(id);
            var ids = id.split(",");
            var record=null;
            switch (ERP.Variables.UserId) {
                case ERP.Variables.UserIds.fyy:
                    var obj = {};
                    obj[fieldNames.ids.orderId] = ids[0];
                    obj[fieldNames.ids.orderDetail] = ids[1];
                    obj[fieldNames.ids.exportId] = ids[2];
                    record = ERP.Variables.data.FindByObject(obj)[0];
                    break;
                case ERP.Variables.UserIds.dxj:
                    var obj = {};
                    obj[fieldNames.ids.orderId] = ids[0];
                    obj[fieldNames.ids.orderDetail] = ids[1];
                    obj[fieldNames.ids.exportId] = ids[2];
                    record = ERP.Variables.data.FindByObject(obj)[0];
                    break;
            }
            if (!record) {
                return;
            }
            var target = $("*[targetID='" + id + "']");
            var _fieldNames = ERP.Variables.fieldNames;
            record[_fieldNames.startDate] = $(target[0]).val();
            record[_fieldNames.endDate] = $(target[1]).val();
        }

    };
    //ERP.Variables.scroller = $("#tableDiv div[class='e-table-viewport']"); /**********************************************/
};

//拖动改变位置
ERP.Util.move_Position = function(obj, e) {
    $(document).on("selectstart", function(e) { return false; }); //HTML5属性:设置后拖动时不会选中任何内容
    //$("body").addClass(".noselect");
    $("body").on("dragstart", function(e) { return false; }); //HTML5属性:拖动时不允许拖拽新窗口打开
    document.all ? obj.setCapture() : window.captureEvents(Event.MOUSEMOVE); //整个屏幕有效（事件）
    $("body").css("-moz-user-select", "-moz-none");
    if (caller_Range) { //子节点事件标志
        return;
    }
    caller_Position = obj; //绑定拖动对象
    caller_Range = null;
    e = e || event; //浏览器兼容
    tempX = e.clientX - $(obj).offset().left;
    $(caller_Position).css("cursor", "move");

};

//拖动改变范围
ERP.Util.move_Range = function(obj, e, right) {
    caller_Position = null;
    caller_Range = obj;
    if (right) { //右移
        delegate = function(obj, e) {
            rightMove(obj, e);
        };
    } else { //左移
        delegate = function(obj, e) {
            leftMove(obj, e);
        };
    }
};

//拖动DIV初始化-全部
var divInitializeAll = function() {
    var data = ERP.Variables.data;
    //data = erpTable100.data.source;
    var fieldNames = ERP.Variables.fieldNames;
    for (var i = 0; i < data.length; i++) {
        switch (ERP.Variables.UserId) {
            case ERP.Variables.UserIds.fyy:
                divIntializeOne(data[i][fieldNames.ids.orderId] + "," + data[i][fieldNames.ids.orderDetail] + "," + data[i][fieldNames.ids.exportId]);
                break;
            case ERP.Variables.UserIds.dxj:
                divIntializeOne(data[i][fieldNames.ids.orderId] + "," + data[i][fieldNames.ids.orderDetail]+","+data[i][fieldNames.ids.exportId]);
                break;
        }
        
    }

};

//拖动DIV初始化-单个
var divIntializeOne = function(id) {
    var source = $("div[sourceID='" + id + "']");
    var averageWidth = erpTable101.getColumnAllWidth() / erpTable101.columns.length;
    var viewType = ERP.Variables.viewType;
    var headerInfo = new Edo.data.DataTable(ERP.Variables.headerColumns);
    var _fieldNames = ERP.Variables.fieldNames;
    var data = ERP.Variables.data;
    var ids = id.split(",");

    var record = null;
    switch (ERP.Variables.UserId) {
        case ERP.Variables.UserIds.fyy:
            var obj = {};
            obj[_fieldNames.ids.orderId] = ids[0];
            obj[_fieldNames.ids.orderDetail] = ids[1];
            obj[_fieldNames.ids.exportId] = ids[2];
            record = data.FindByObject(obj);
            break;
        case ERP.Variables.UserIds.dxj:
            var obj = {};
            obj[_fieldNames.ids.orderId] = ids[0];
            obj[_fieldNames.ids.orderDetail] = ids[1];
            obj[_fieldNames.ids.exportId] = ids[2];
            record = data.FindByObject(obj);//data.FindObject(_fieldNames.id, ids[0]).FindObject("OrderNumber",ids[1]);
            break;
    }
    if (!record) {
        return;
    }
    record = record[0];
    
    var startStr = record[_fieldNames["startDate"]];
    var endStr = record[_fieldNames["endDate"]];
    var startDate = Date.Parse(startStr);
    var endDate = Date.Parse(endStr);
    if (startDate > endDate) {
        startDate = Date.Parse(endStr);
        endDate = Date.Parse(startStr);
    }

    if (viewType == 'MONTH') {
        var recordDay = headerInfo.find({ year: startDate.getFullYear() });
        var beforeWidth = recordDay.width;
        var firstMonth = recordDay.firstMonth;
        var nowMonth = startDate.getMonth() + 1;
        var nowDay = startDate.getDate();
        var maxDay = Date.GetMonthDays(nowMonth, startDate.getFullYear());
        var divLeft = beforeWidth + nowMonth - firstMonth - 1 + (nowDay - 1) / maxDay;
        source.css("left", divLeft * averageWidth + "px");

        recordDay = headerInfo.find({ year: endDate.getFullYear() });
        beforeWidth = recordDay.width;
        firstMonth = recordDay.firstMonth;
        nowMonth = endDate.getMonth() + 1;
        nowDay = endDate.getDate();
        var divRight = beforeWidth + nowMonth - firstMonth - 1 + nowDay / maxDay;
        var divWidth = divRight - divLeft;
        source.css("width", divWidth * averageWidth + "px");
    } else {
        var recordDay = headerInfo.find({ year: startDate.getFullYear(), month: startDate.getMonth() + 1 });
        
        var beforeWidth = recordDay.width;
        var firstDay = recordDay.firstDay;
        var nowDay = startDate.getDate();
        var divLeft = beforeWidth + nowDay - firstDay - 1;
        if (viewType == "WEEK") {
            divLeft = beforeWidth - 1 + (nowDay - firstDay) / 7;
            //divLeft = beforeWidth + Math.floor((nowDay - firstDay) / 7) - 1 + (nowDay - firstDay) % 7 / 7;
        }
        source.css("left", divLeft * averageWidth + "px");

        recordDay = headerInfo.find({ year: endDate.getFullYear(), month: endDate.getMonth() + 1 });
        beforeWidth = recordDay.width;
        firstDay = recordDay.firstDay;
        nowDay = endDate.getDate();
        var divRight = beforeWidth + nowDay - firstDay;
        if (viewType == "WEEK") {
            divRight = beforeWidth - 1 + (nowDay - firstDay) / 7;
            //divRight = beforeWidth + Math.floor((nowDay - firstDay) / 7) - 1 + (nowDay - firstDay) % 7 / 7;
        }
        var divWidth = divRight - divLeft;
        source.css("width", divWidth * averageWidth + "px");

    }
};

//方法:设置
var set = function(obj) {
    if (checkNull(obj.refreshAll) == null) {
        ERP.Functions.refreshAll = function() {
            refreshAll();
        };
    } else {
        ERP.Functions.refreshAll = obj.refreshAll;
    }

    if (checkNull(obj.refreshValue) == null) {
        ERP.Functions.refreshValue = obj.refreshValue;
    } else {
        ERP.Functions.refreshValue = obj.refreshValue;
    }

    if (checkNull(obj.parentId) != null) {
        ERP.parentId = obj.parentId;
        ERP.parent = $("#" + obj.parentId);
    }

    if (checkNull(obj.provider) != null) {
        ERP.parent = $(obj.provider);
    }

    if (checkNull(obj.scroller) != null) {
        ERP.Variables.scroller = $(obj.scroller);
        $(obj.scroller).scrollLeft(1000000);
        ERP.Variables.scrollerMaxLeft = $(obj.scroller).scrollLeft();
        $(obj.scroller).scrollLeft(0);
    }

    if (checkNull(obj.userFunction) != null) {
        ERP.Functions.userFunction = obj.userFunction;
    }

    if (checkNull(obj.userParams) != null) {
        ERP.Variables.userParams = obj.userParams;
    }

    if (checkNull(obj.headerArray) != null) {
        ERP.Variables.headerArray = obj.headerArray;
    }
};

//是否为空
var checkNull = function(obj) {
    if (typeof(obj) != 'undefined') {
        return obj;
    } else {
        return null;
    }
};

//方法：右移
var rightMove = function(obj, e) {
    var div = $(obj);
    var e = e || event;
    var erpParentNode = ERP.parent; //父节点
    var parentNode = $(div[0].parentNode);
    var erpParentLeft; //外级节点
    var erpParentRight;
    var clientX = e.clientX;
    var erpScrollLeft = 0;
    if (ERP.Variables.scroller != null) {
        erpScrollLeft = ERP.Variables.scroller.scrollLeft();
    }
    if (ERP.parent == null) {
        erpParentLeft = 0;
        erpParentRight = $(window).width();
    } else {
        erpParentLeft = erpParentNode.offset().left;
        erpParentRight = erpParentLeft + erpParentNode.width();
    }
    var right = clientX + erpScrollLeft;
    var maxRight = erpParentNode.width() + erpParentNode.offset().left + ERP.Variables.scrollerMaxLeft;
    if (right >= maxRight) {
        parentNode.width(maxRight - parentNode.offset().left - erpScrollLeft);
    } else {
        var left = parentNode.offset().left;
        if (clientX < left + 3) { //(自身)左边界，留有余地，不至于让div消失
            parentNode.width("3px");
        } else {
            parentNode.css("width", (clientX - parentNode.offset().left) + "px"); //x坐标-自身left-外部left
        }
    }
};

//方法：左移
var leftMove = function(obj, e) {
    var e = e || event;
    var parentNode = $(obj.parentNode); //父节点
    var erpParentNode = ERP.parent;
    var right;
    //var right = parentNode.width() + parentNode.offset().left;//右边界
    var clientX = e.clientX;
    var erpParentLeft = 0;
    var erpScrollLeft = 0;
    if (ERP.Variables.scroller != null) {
        erpScrollLeft = ERP.Variables.scroller.scrollLeft();
    }
    if (erpParentNode != null) {
        erpParentLeft = erpParentNode.offset().left;
    }
    //right = parentNode.width() + parentNode.offset().left-1;//比parseInt(parentNode.css("width")) + parseInt(parentNode.css("left")) + erpParentLeft多1
    right = parseInt(parentNode.css("width")) + parseInt(parentNode.css("left")) + erpParentLeft;
    if (clientX >= right - 3 - erpScrollLeft) { //(自身)右边界 
        parentNode.css("width", "3px");
        parentNode.css("left", (right - erpParentLeft - 3) + "px");
    } else {
        if (erpScrollLeft == 0 && clientX <= erpParentLeft) { //左边界
            parentNode.width((right - erpParentLeft) + "px");
            parentNode.css("left", "0px");
        } else {
            parentNode.css("width", (right - clientX - erpScrollLeft) + "px");
            parentNode.css("left", (clientX - erpParentLeft + erpScrollLeft) + "px");
            //ERP.Variables.scroller.scrollLeft(parentNode.offset().left-erpParentLeft);
        }
    }
};

//方法：更新数据（单条),可自定义
var refreshValue = function(id) {

    var source = $("div[sourceID='" + id + "']");
    var target = $("*[targetID='" + id + "']"); //集合：开始时间和结束时间
    var headerColumns = ERP.Variables.headerColumns; //new Edo.data.DataTable(erpTable101.columns);alert(erpTable101.columns[0].header);
    var averageWidth = erpTable101.getColumnAllWidth() / erpTable101.columns.length;
    //alert(source.offset().left);
    var left = parseFloat(source.css("left"));
    var right = left + $(source).width();
    var viewType = ERP.Variables.viewType;
    var integer;
    var number;
    var startDate;
    var endDate;
    try {
        if (viewType == "MONTH") {
            number = left / averageWidth;
            integer = Math.floor(number);
            number = number - integer;
            var preMonth = headerColumns[integer].firstMonth;
            var maxDay = Date.GetMonthDays(preMonth, headerColumns[integer].year);
            var day = Math.round(maxDay * number) + 1; //(left - averageWidth * integer) / averageWidth
            startDate = new Date(headerColumns[integer].year, preMonth - 1, day).Format("yyyy-MM-dd");

            number = right / averageWidth;
            integer = Math.floor(number);
            number = number - integer;
            preMonth = headerColumns[integer].firstMonth;
            maxDay = Date.GetMonthDays(preMonth, headerColumns[integer].year);
            day = Math.round(maxDay * number) + 1; //(right - averageWidth * integer) / averageWidth
            endDate = new Date(headerColumns[integer].year, preMonth - 1, day).Format("yyyy-MM-dd");

            target.filter("input[model=start]").val(startDate);
            target.filter("input[model=end]").val(endDate);
        } else {
            if (viewType == "WEEK") {
                number = left / averageWidth;
                integer = Math.floor(number);
                //number -= integer;
                integer += 1;
                var index = getWidthIndexFromSet(integer, headerColumns);
                var record = headerColumns[index];
                var month = record.month;
                var firstDay = record.firstDay;
                var nowDay = firstDay + 7 * (number - record.width + 1);
                nowDay = Math.round(nowDay);
                if (nowDay > Date.GetMonthDays(month, record.year)) {
                    record = headerColumns[index + 1];
                    month = record.month;
                    firstDay = record.firstDay;
                    nowDay = firstDay + 7 * (number - record.width + 1);
                }
                nowDay = Math.round(nowDay);
                startDate = new Date(record.year, month - 1, nowDay).Format("yyyy-MM-dd");

                number = right / averageWidth;
                integer = Math.floor(number);
                integer += 1;
                var index = getWidthIndexFromSet(integer, headerColumns);
                var record = headerColumns[index];
                var month = record.month;
                var firstDay = record.firstDay;
                var nowDay = firstDay + 7 * (number - record.width + 1);
                nowDay = Math.round(nowDay);
                if (nowDay > Date.GetMonthDays(month, record.year)) {
                    record = headerColumns[index + 1];
                    month = record.month;
                    firstDay = record.firstDay;
                    nowDay = firstDay + 7 * (number - record.width + 1);
                }
                nowDay = Math.round(nowDay);
                endDate = new Date(record.year, month - 1, nowDay).Format("yyyy-MM-dd");
                target.filter("input[model=start]").val(startDate);
                target.filter("input[model=end]").val(endDate);

            } else {
                number = left / averageWidth;
                integer = Math.floor(number) + 1;
                var index = getWidthIndexFromSet(integer, headerColumns);
                var record = headerColumns[index];
                var firstDay = record.firstDay;
                var nowDay = firstDay + number - record.width + 1;
                startDate = new Date(record.year, record.month - 1, nowDay).Format("yyyy-MM-dd");

                number = right / averageWidth;
                integer = Math.floor(number) + 1;
                index = getWidthIndexFromSet(integer, headerColumns);
                record = headerColumns[index];
                firstDay = record.firstDay;
                nowDay = firstDay + number - record.width + 1;
                endDate = new Date(record.year, record.month - 1, nowDay).Format("yyyy-MM-dd");

                target.filter("input[model=start]").val(startDate);
                target.filter("input[model=end]").val(endDate);
            }
        }
    } catch(e) {
    }

};

//方法：更新数据(全部),可自定义
var refreshAll = function() {
    var sources = $("div[sourceID]");
    for (var i = 0; i < sources.length; i++) {
        refreshValue($(sources[i]).attr("sourceID"));
        //new ERP.Functions.refreshValue($(sources[i]).attr("sourceID"));
    }

};

//得到某月的天数
Date.GetMonthDays = function(month) {
    var date = new Date();
    var month = arguments[0];
    var year = arguments[1] ? arguments[1] : date.getFullYear();
    var now = new Date(year, month - 1, 1);
    var next = new Date(year, month, 1);
    return (next - now) / 24 / 3600 / 1000;
};

Date.prototype.AddDays = function(days) {
    this.setDate(this.getDate() + days);
    return this;
};

//将数字转为天数
Number.prototype.toDay = function() { //一天=24h*3600min*1000ms
    var number = this;
    return number / 24 / 3600 / 1000;
};

//将数字转为周数
Number.prototype.toWeek = function() { //一周=一天*7
    var number = this;
    return number.toDay() / 7;

};

//将数字转为月数
Number.prototype.toMonth = function() { //一周=一天*30(大约)
    var number = this;
    return number.toDay() / 30;

};

//找出第一个满足条件的数组元素
Array.prototype.FindObject = function(key, value) {
    if (arguments.length != 2) {
        return null;
    }
    if (key) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (this[i][key] == value) {
                return this[i];
            }
        }
    } else {
        for (var i = 0, len = this.length; i < len; i++) {
            if (this[i] == value) {
                return this[i];
            }
        }

    }
    return null;
};

Array.prototype.FindByObject = function(obj) {
    var arr = [];
    var str = "";
    for (var key in obj) {
        str += "_this['" + key + "']==" + "'" + obj[key] + "'" + "&& ";
    }
    str = str.replace(/&& $/, "");
    for (var i = 0, len = this.length; i < len; i++) {
        var _this = this[i];
        if (eval('(' + str + ')')) {
            arr.push(this[i]);
        }
    }
    return arr;
};

//去指定字段去重[{},{}](字段为空时对简单的数组["a","b"]去重)
Array.prototype.Distinct = function(keyName) {
    var hash = {};
    var ret = [];
    if (keyName) {
        for (var i = 0, len = this.length; i < len; i++) {
            var key = "k" + this[i][keyName];
            if (hash[key]!==1) {
                ret.push(this[i]);
                hash[key] = 1;
            }
        }
    } else {
        for (var i = 0, len = this.length; i < len; i++) {
            var key = "k" + this[i];
            if (hash[key] !== 1) {
                ret.push(this[i]);
                hash[key] = 1;
            }
        }
    }
    return ret;
};

////去指定字段(多字段,逗号分隔)去重[{},{}]，返回新数组(字段为空时对简单的数组["a","b"]去重)
Array.prototype.Distinct = function(fields) {
    var hash = {};
    var ret = [];
    if (fields) {//按字段去重
        var fieldArr = fields.split(",");
        var str = "'k'";
        for (var i=0,len=fieldArr.length;i<len;i++) {
            str += "+'-'+obj['"+fieldArr[i]+"']";
        }
        for (var i = 0, len = this.length; i < len; i++) {
            var obj = this[i];
            var key = eval(str);
            if (hash[key] !== 1) {
                ret.push(this[i]);
                hash[key] = 1;
            }
        }
    } else {//普
        for (var i = 0, len = this.length; i < len; i++) {
            var key = "k" + this[i];
            if (hash[key] !== 1) {
                ret.push(this[i]);
                hash[key] = 1;
            }
        }
    }
    return ret;
};

//按指定索引排序(仅对日期属性有效)：dataIndex为要排序的列,rule为"asc"和"desc"
Edo.data.DataTable.prototype.SortFiled = function(dataIndex, rule) {
    this.sort(function(pre, next) {
        var d1 = Date.Parse(eval("pre." + dataIndex));
        var d2 = Date.Parse(eval("next." + dataIndex));
        if (typeof rule != "undefined") {
            if (rule.toUpperCase() == "DESC") {
                return d1 < d2;
            }
        }
        return d1 > d2;
    });
};

Edo.data.DataTable.prototype.SortById = function(rule) {
    this.sort(function(pre, next) {
        var id1 = pre.id;
        var id2 = next.id;
        if (typeof rule != "undefined") {
            if (rule.toUpperCase() == "DESC") {
                return id1 < id2;
            }
        }
        return id1 > id2;
    })
};

//改写Edo.lists.Table的刷新方法
Edo.lists.Table.prototype.Refresh = function() {
    this.refresh(true);
    var delatHeight = 0;
    var width = erpTable101.getColumnAllWidth() < ERP.Variables.tableWidth ? erpTable101.getColumnAllWidth() : (delatHeight = 20, ERP.Variables.tableWidth);
    this.set("width", width +2);
    this.set("height", ERP.Variables.tableHeight + 20);
    $("#monthTable").width(this.width);
    divInitializeAll();
    setTimeout(function () {
        var $table = $("#monthTable");
        $table.children(":first").width($table.width());

        ERP.Variables.scrollerMaxLeft = ERP.Functions.scrollMaxLeft();

        //大致估算滚动条的位置
        var _width = ERP.Variables.scrollerMaxLeft+$table.width();
        var _viewType = ERP.Variables.viewType;
        var now = new Date();
        now = new Date(now.getFullYear(), now.getMonth(), now.getDate());
        var minDate = ERP.Variables.minDate;
        var delatDate = now - minDate;
        delatDate = delatDate.toDay();
        //delatDate += 10;
        //switch (_viewType) {
        //    case 'DAY':
        //        delatDate = delatDate.toDay();
        //        break;
        //    case 'WEEK':
        //        delatDate = delatDate.toWeek();
        //        break;
        //    case 'MONTH':
        //        delatDate = delatDate.toMonth();
        //        break;
        //    default :
        //}
        var l = erpTable101.columns.length;
        if (delatDate >= l) {
            ERP.Variables.scroller.scrollLeft(_width);
        } else {
            ERP.Variables.scroller.scrollLeft(delatDate * _width / l); //大致补偿一下
        }
    }, 5);
};

//createTable,obj：renderDiv(Div区域),renderMsg(信息显示区域),data(数据),viewType(视图模式),columnWidth(列宽)
//data:id,startDate,endDate
var createTable = function (obj) {
    //销毁已存在组件
    if (typeof erpTable101 != "undefined") {
        erpTable101.destroy();
    }
    if (!obj.data || obj.data.length==0) {
        return null;
    }
    var renderDiv = obj.renderDiv ? $(obj.renderDiv)[0] : $("body")[0];
    var dataEdo = obj.data ? obj.data.slice(0) : [];
    var viewType = obj.viewType ? obj.viewType : 'month';
    var columnsWidth = obj.columnWidth ? obj.columnWidth : 80;
    ERP.Variables.columnsWidth = columnsWidth;
    var columnsData = getColumnsData({ data: dataEdo, columnsWidth: columnsWidth, viewType: viewType });
    if (columnsData == null) {
        return null;
    }
    ERP.Variables.data = obj.data;
    ERP.Variables.tableHeight = $("div.DMPD").find(".datagrid-view").height() - 18;
    ERP.Variables.tableWidth = obj.width ? obj.width : 300;
    
    Edo.create({
        id: "erpTable101",
        type: 'table',
        render: renderDiv,
        autoColumns: false,
        horizontalLine: false,
        verticalLine: false,
        horizontalScrollPolicy: "on",
        enableColumnDragDrop: false,
        columns: columnsData[0],
        onbodyclick: function(opt) {
            /*if (!currentTabTitle) {
                return;
            }
            var selectRow = $(opt.target).parents("tr");
            var selectIndex = selectRow[0].rowIndex;
            var className;
            if (currentTabTitle == "月计划") {
                className = ".DMPD";
            } else {
                className = ".DWPD";
            }
            var datagridTr = $(className).find(".datagrid-body").find("tr").eq(selectIndex);
            selectRow[selectRow.hasClass("selected") ? "removeClass" : "addClass"]("selected");
            datagridTr[datagridTr.hasClass("selected") ? "removeClass" : "addClass"]("selected");*/
            $("#erpTable101").children("a").remove();
            return false;
        },
        data: obj.data
    });
    erpTable101.Refresh();
    
    //var ddd = erpTable100.data.SortById("desc");
    return erpTable101;
};

//双击事件响应
var dbClick = function(obj) {
    var target = $(obj);
    var _obj = {};
    _obj.id = target.attr("targetID");
    _obj._class = target.attr("class");
    _obj.value = target.val();
    ERP.Variables.lastData = _obj;
    //outOfCalendar = false;
    //openCalendar(target);
    //WdatePicker();
};

//用户输入，数据改变时重新加载
var dataChange = function(obj,newVal) {
    var target = $(obj);
    var _class = target.attr("model");

    if (!_class) {
        return;
    }
    _class = _class.toUpperCase();
    var _value = newVal;//target.val();
    var _id = target.attr("targetID");

    var current = Date.Parse(_value);
    var _fieldNames = ERP.Variables.fieldNames;
    var ids = _id.split(",");
    var record; //= ERP.Variables.data.FindByObject({ "OrderNo": ids[0], "OrderNumber": ids[1] })[0];
    switch (ERP.Variables.UserId) {
        case ERP.Variables.UserIds.fyy:
            var obj = {};
            obj[_fieldNames.ids.orderId] = ids[0];
            obj[_fieldNames.ids.orderDetail] = ids[1];
            obj[_fieldNames.ids.exportId] = ids[2];
            record = ERP.Variables.data.FindByObject(obj)[0];
            break;
        case ERP.Variables.UserIds.dxj:
            var obj = {};
            obj[_fieldNames.ids.orderId] = ids[0];
            obj[_fieldNames.ids.orderDetail] = ids[1];
            obj[_fieldNames.ids.exportId] = ids[2];
            record = ERP.Variables.data.FindByObject(obj)[0];
            break;
    }
    if (record == null) return;
    if (_class == 'START') {
        var next = Date.Parse(record[_fieldNames.endDate]); //($($("*[sourceID="+_id+"]")[0]).val());
        if (current > next) { //开始日期晚于
            //target.val(ERP.Variables.lastData.value);
            return false;
        }
        record[_fieldNames.startDate] = _value;
        //erpTable100.data.update(record, _fieldNames.startDate, _value);

    } else {
        var before = Date.Parse(record[_fieldNames.startDate]);
        if (current < before) {
            //target.val(ERP.Variables.lastData.value);
            return false;
        }
        record[_fieldNames.endDate] = _value;
        //erpTable100.data.update(record, _fieldNames.endDate, _value);
    }

    //刷新数据
    //ERP.Variables.data = erpTable100.data;
    ERP.Variables.changedRecord.push(_id);
    EdoColumnsFirst = getColumnsData({ data: ERP.Variables.data, columnsWidth: ERP.Variables.columnsWidth, viewType: ERP.Variables.viewType }); //getColumnsForWeek({ columnsWidth: 50, startDate: endDate, endDate: startDate, viewType: this.selectedItem.viewType });
    erpTable101.set("columns", EdoColumnsFirst[0]);
    erpTable101.Refresh();
    return true;
};

//根据数据集得到列属性,obj属性：data(数据集),columnsWidth(列宽),viewType(视图类型：day、week、month)
//返回值：列 头文字
var getColumnsData = function(obj) {
    if (!obj.data) {
        return null;
    }
    var data = obj.data.slice(0);
    //ERP.Variables.data = data;
    var startDateStr; // = obj.startDate ? obj.startDate : null; //判断时间段是否存在,不存在则退出
    var endDateStr; // = obj.endDate ? obj.endDate : null;
    var _fieldNames = ERP.Variables.fieldNames;
    data = new Edo.data.DataTable(data);
    data.SortFiled(_fieldNames.startDate);
    startDateStr = data.getAt(0)[_fieldNames.startDate];
    ERP.Variables.minDate = Date.Parse(startDateStr);
    data.SortFiled(_fieldNames.endDate, "desc");
    endDateStr = data.getAt(0)[_fieldNames.endDate];
    data.SortById("asc");
    /*if (startDateStr == null || endDateStr == null) {
            return null;
        }*/
    var viewType = obj.viewType ? obj.viewType.toUpperCase() : 'DAY';
    ERP.Variables.viewType = viewType;
    var returnValue = []; //程序返回值
    var created = false;
    var EdoColumnsInfo = []; //记录每月开始日期及相应的宽度
    var EdoColumnsText = []; //列显示的文字（日期）
    var firstDay = null; //所给时间段(传入的时间参数)的第一天
    var firstMonth = null;
    var count = 0; //记录宽度（基准值的倍数）
    var width = obj.columnsWidth ? obj.columnsWidth : 30; //列宽度
    ERP.Variables.columnsWidth = width;
    var startDt = Date.Parse(startDateStr); //字符串解析为日期，此方法为自定义方法
    var endDt = Date.Parse(endDateStr);
    if (startDt > endDt) {
        startDt = Date.Parse(endDateStr);
        endDt = Date.Parse(startDateStr);
    }
    var HtmlStrBegin = "<div class='resizeDiv {0}' sourceID='";
    var HtmlStrEnd = "' onmousedown='ERP.Util.move_Position(this,event);' >"
        + "<div class='selectHeader' onmousedown='ERP.Util.move_Range(this,event,false)'></div>"
        + "<div class='selectLast' onmousedown='ERP.Util.move_Range(this,event,true)'/>"
        + "</div>";
    var bgClasses = ["fullMaterial", "someMaterial", "noMaterial",""];//拖动条的背景class
    var yearTemp = startDt.getFullYear(); //记录年,为MONTH视图做准备
    var firstIn = true;


    var beforeDays = 3;//提前的日期
    var endDays = 3;//延后的日期

    var baseDt = startDt.Clone().AddDays(-beforeDays);
    endDt = endDt.AddDays(endDays);
    ERP.Variables.minDate = baseDt;
    var curr = { year: baseDt.getFullYear(), month: baseDt.getMonth() + 1, day: baseDt.getDate() };
    var info = [];
    var text = [];

    var tempArray = [];
    var daysOfMonth = Date.GetMonthDays(curr.month, curr.year);
    count = 1;
    info.push({ year: curr.year, month: curr.month, firstDay: curr.day, width: count });
    for (var i = 0, len = ((endDt - baseDt).toDay()) + 2; i < len; i++) {
        var obj = {};
        obj.header = curr.day;
        obj.width = width;
        obj.headAlign = "center";
        obj.enableResize = false;
        if (!created) {
            obj.renderer = function(value, record, column, rowIndex, data, table) {
                var str = "";
                switch (ERP.Variables.UserId) {
                    case ERP.Variables.UserIds.fyy:
                        sourceId = record[_fieldNames.ids.orderId] + "," + record[_fieldNames.ids.orderDetail] + "," + record[_fieldNames.ids.exportId];
                        str = HtmlStrBegin.replace("{0}","")+ sourceId + HtmlStrEnd;
                        break;
                    case ERP.Variables.UserIds.dxj:
                        var classNameIndex = 0;
                        var materialQuanlity = record["MaterialQuanlity"] - 0;
                        var processQuanlity = record["ProduceQuanlity"] - 0;
                        var sourceId;
                        if (materialQuanlity == 0) {
                            classNameIndex = 2;//物料为0
                        } else {
                            if (materialQuanlity >= processQuanlity) {
                                classNameIndex = 0;//物料已到
                            } else {
                                classNameIndex = 1;//部分物料已到
                            }
                        }
                        sourceId = record[_fieldNames.ids.orderId] + "," + record[_fieldNames.ids.orderDetail] + "," + record[_fieldNames.ids.exportId];
                        str = HtmlStrBegin.replace("{0}",bgClasses[classNameIndex]) + sourceId + HtmlStrEnd;
                        break;
                }
                return str;
            };
            created = true;
        }
        tempArray.push(obj);
        curr.day++;
        count++;
        if (curr.day > daysOfMonth) {
            text.push({ header: curr.year + "." + curr.month, headerAlign: "center",enableResize:false, columns: tempArray.slice(0) });
            tempArray = [];

            curr.month++;
            if (curr.month > 12) {
                curr.month = 1;
                curr.year++;
            }
            curr.day = 1;
            daysOfMonth = Date.GetMonthDays(curr.month, curr.year);
            info.push({ year: curr.year, month: curr.month, firstDay: curr.day, width: count });
        }
    }
    text.push({ header: curr.year + "." + curr.month, columns: tempArray.slice(0) });

    returnValue.push(text);
    returnValue.push(info);
    ERP.Variables.headerColumns = info;
    return returnValue;





    while (1) { //循环退出条件：开始日期增大后大于结束日期
        var colText = {}; //存放第一行表头信息
        colText.columns = []; //第二行列 文字 集合
        colText.headerAlign = "center";
        colText.autoColumns = "true";

        if (viewType == 'MONTH') {
            for (var i = startDt.getMonth(), endMonth = endDt.getMonth() + 1, endYear = endDt.getFullYear(); (i != endMonth) || yearTemp < endYear; i++) {
                if (i != 12) {
                    var obj = {};
                    obj.header = (i + 1) + '月';
                    obj.minWidth = width;
                    obj.width = width;
                    obj.enableResize = false;

                    if (!created) {
                        obj.renderer = function(value, record, column, rowIndex, data, table) {
                            return HtmlStrBegin + record[_fieldNames.id] + "-" + record["OrderNumber"] + HtmlStrEnd;
                        };
                        created = true;
                    }

                    colText.columns.push(obj);
                    count++;
                    //if (firstMonth == null || i == 0) {//开始日期及每年1月时记录
                    var o = {};
                    o.width = count;
                    o.year = yearTemp;
                    o.firstMonth = i + 1;
                    if (firstMonth == null) {
                        firstMonth = -1;
                        o.width = 1;
                    } else {
                        //o.firstMonth = 1;
                        o.width = count;
                    }
                    EdoColumnsInfo.push(o);
                    //}
                }
                if (i == 12) {

                    colText.header = yearTemp;
                    yearTemp++;
                    EdoColumnsText.push(colText);
                    colText = {};
                    colText.columns = []; //第二行列 文字 集合
                    colText.headerAlign = "center";
                    colText.autoColumns = "true";
                    i = -1;
                }

            }

            colText.header = yearTemp;
            EdoColumnsText.push(colText);


            returnValue.push(EdoColumnsText); //{ header: '1', columns: [{ header: '1-1'}] }, { header: '1', columns: [{ header: '1-1'}]}]
            //returnValue.push(EdoColumnsInfo);
            ERP.Variables.headerColumns = EdoColumnsInfo;
            return returnValue;
            //startDt.setMonth(startDt.getMonth() + 1);
            //startDt.setDate(1);


        } else {
            //startDt.setDate(startDt.getDate() - 7);

            var dayOfWeek = startDt.getDay(); //当前日期为星期几
            if (!created && dayOfWeek == 0) { //当开始日期为周日时，向前推一周（只执行一次）
                startDt.setDate(startDt.getDate() - 7);
            }

            var weekOfYear = startDt.getWeekOfYear(); //年中的周数
            var dateTemp = Date.Parse(startDt.toDateString()); //用于开始日期一次增大后的寄存单元

            //var colText = {}; //存放第一行表头信息

            startDt.setDate(startDt.getDate() - dayOfWeek + 1); //得到此日期所在周的开始日期
            dateTemp.setDate(dateTemp.getDate() - dayOfWeek + 7); //得到此日期所在周的结束日期
            var day1 = startDt.getDate(); //每周星期一的日期
            var day2 = dateTemp.getDate(); //每周星期日的日期
            var maxDate = Date.GetMonthDays(startDt.getMonth() + 1, startDt.getFullYear()); //指定月份的最大天数(即最后一天)
            //列属性
            /*colText.columns = []; //第二行列 文字 集合
                colText.headerAlign = "center";
                colText.autoColumns = "true";*/
            if (viewType == 'DAY') {
                //colText.header = weekOfYear + "周/" + startDt.Format("yyyy.MM") +
                //    (startDt.getMonth() != dateTemp.getMonth() ? ("-" + dateTemp.Format("MM")) : '');
                colText.header = startDt.getFullYear();

                if (firstDay == null) {
                    firstDay = startDt.getDate();
                }

                var temp = Date.Parse(startDt.Format("yyyy-MM-dd"));
                temp.AddDays(-1);
                //var day1 = startDt.getDate(); //每周星期一的日期
                //var day2 = dateTemp.getDate(); //每周星期日的日期
                //var maxDate = Date.GetMonthDays(startDt.getMonth() + 1, startDt.getFullYear()); //指定月份的最大天数(即最后一天)
                for (var i = startDt.getDate(), len = dateTemp.getDate() + 1; i != len; i++) { //周处理
                    var obj = {}; //第二行列 文字
                    var colInfo = {}; //开始日期和宽度
                    obj.header = temp.AddDays(1).Format("MM.dd");

                    obj.width = width;
                    obj.enableResize = false;
                    if (!created) {
                        obj.renderer = function(value, record, column, rowIndex, data, table) {
                            return HtmlStrBegin + record[_fieldNames.id] + "-" + record["OrderNumber"] + "'></div>"; //HtmlStrEnd;
                        };
                        created = true;
                    }
                    colText.columns.push(obj);
                    if (day1 > day2) {
                        if (i == maxDate) {
                            i = 0;
                        }
                        //obj.header = (monthTemp + 1)+"."+obj.header;
                    }
                    count++;
                    if (i == firstDay) {
                        colInfo.year = startDt.getFullYear();
                        colInfo.month = startDt.getMonth() + 1;
                        colInfo.firstDay = firstDay;
                        colInfo.width = count;
                        EdoColumnsInfo.push(colInfo);
                        firstDay = -1;
                    }
                    if (i == 1) {
                        colInfo.year = dateTemp.getFullYear();
                        colInfo.month = dateTemp.getMonth() + 1;
                        colInfo.firstDay = 1;
                        colInfo.width = count;
                        EdoColumnsInfo.push(colInfo);
                    }
                }
                EdoColumnsText.push(colText);


            }
            if (viewType == 'WEEK') {
                colText.header = startDt.Format("yyyy");
                //colText.header = weekOfYear + "w/" + startDt.Format("yy.MM");
                //colText.columns = []; //第二行列 文字 集合
                //colText.headerAlign = "center";
                //colText.autoColumns = "true";


                //var day1 = startDt.getDate(); //每周星期一的日期
                //var day2 = dateTemp.getDate(); //每周星期日的日期

                count++;
                if (firstDay == null) { //
                    var colInfo = {};
                    firstDay = startDt.getDate();
                    colInfo.year = startDt.getFullYear();
                    colInfo.month = startDt.getMonth() + 1;
                    colInfo.firstDay = firstDay;
                    colInfo.width = count;
                    EdoColumnsInfo.push(colInfo);
                    firstDay = -1;
                    //if (day1 > day2) {
                    count++;
                    //}
                }

                if (day1 > day2 || day2 == maxDate) { //跨月
                    var colInfo = {};
                    dateTemp.setDate(dateTemp.getDate() + 1);
                    colInfo.year = dateTemp.getFullYear();
                    colInfo.month = dateTemp.getMonth() + 1;
                    colInfo.firstDay = dateTemp.getDate();
                    colInfo.width = count;
                    dateTemp.setDate(dateTemp.getDate() - 1);
                    EdoColumnsInfo.push(colInfo);
                }

                /*************************************/
                var obj = {}; //第二行的列
                obj.width = width;
                obj.header = startDt.Format("MM.dd");
                obj.enableResize = false;
                if (!created) {
                    obj.renderer = function(value, record, column, rowIndex, data, table) {
                        return HtmlStrBegin + record[_fieldNames.id] + "-" + record["OrderNumber"] + HtmlStrEnd;
                    };
                    created = true;
                }
                colText.columns.push(obj);
                EdoColumnsText.push(colText);
                /*************************************/
            }

            if (dateTemp > endDt) { //程序退出
                var columnsText = [];
                for (var j = Date.Parse(startDateStr).getFullYear(), len = Date.Parse(endDateStr).getFullYear(); j <= len; j++) {
                    var _obj = {};
                    _obj.header = j;
                    _obj.columns = [];
                    _obj.headerAlign = "center";
                    _obj.autoColumns = true;
                    columnsText.push(_obj);
                }
                //重新按年份分组
                for (var j = 0, len = EdoColumnsText.length; j < len; j++) {
                    var header = EdoColumnsText[j].header;
                    for (var k = 0, l = columnsText.length; k < l; k++) {
                        if (header == columnsText[k].header) {
                            columnsText[k].columns = columnsText[k].columns.concat(EdoColumnsText[j].columns);
                        }
                    }
                }
                returnValue.push(columnsText);
                returnValue.push(EdoColumnsInfo);
                ERP.Variables.headerColumns = EdoColumnsInfo;
                return returnValue;
            }
            startDt.setDate(startDt.getDate() + 7); //日期一次加7，得到下一个星期
        }

    }
};

//判断数值在集合中的位置，返回最小index
var getWidthIndexFromSet = function (value,dataSet) {
    for (var i = 0; i < dataSet.length-1; i++) {
        if (dataSet[i].width <= value && dataSet[i + 1].width > value) {
            return i;
        }
    }
    return i;
}