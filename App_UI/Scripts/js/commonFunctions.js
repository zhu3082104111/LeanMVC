
var Common = {};
Common.Functions = {};
Common.Variables = {};
Common.Variables.datagrid = {};

Common.Variables.datagrid.defaultColumns = ["", "", 80, false, null, function (v, r, index) { return v; }];
Common.Variables.datagrid.defaultGroupColumns = ["", "", 80,1,1, false, null, function (v, r, index) { return v; }];
Common.Variables.datagrid.options = {maxWidth:0,maxHeight:0,columnsAllWidth:0};
Common.Variables.datagrid.nullDataMessage = "无符合条件数据";
//待删除
(function ($) {
    if (typeof console=="undefined") {
        window.console = {};
        window.console.info = function() {
        };
        window.console.time = function() {
        };
        window.console.timeEnd = function () {
        };
    }
    if (!window.console.time) {
        window.console.time = function () {
        };
        window.console.timeEnd = function () {
        };
    }
    
}(jQuery));


//公共的default属性及方法的更改
(function ($) {
    //日历显示格式
    $.fn.datebox.defaults.formatter = function (date) {
        return date.Format("yyyy-MM-dd");
    };
    
    //分页显示
    $.extend($.fn.pagination.defaults, {
        afterPageText: "/{pages}",
        beforePageText: ""
    });
    
    //datagrid方法扩展
    $.extend($.fn.datagrid.methods, {
        refreshColumn: function (jq, option) {//刷新某一列，如 $(table).datagrid("refreshColumn","name")或$(table).datagrid("refreshColumn",{field:"name",isFooter:true})
            if (!option) {
                return;
            }
            var field = "";
            if (typeof option == "string") {
                field = option;
            } else {
                field = option.field;
            }
            if (!field) {
                return;
            }
            console.time("refreshColumn");
            var $table = $(jq[0]);
            var $tds;
            var rows;
            if (option.isFooter) {
                $tds = $table.datagrid("getColumnJqByField", { field: field, isFooter: true });
                rows = $table.datagrid("getFooterRows");
            } else {
                $tds = $table.datagrid("getColumnJqByField", field);
                rows = $table.datagrid("getData").rows;
            }

            var formatter = $table.datagrid("getColumnOption", field).formatter;
            if (!formatter) {
                formatter = function (value) { return value; };
            }
            for (var i = 0, len = rows.length; i < len; i++) {
                var $div = $tds.eq(i).children();
                var row = rows[i];
                $div.html(formatter.call($table, row[field], row, i));
            }
            console.timeEnd("refreshColumn");
        },
        getColumnJqByField: function (jq, option) {//获取某一列元素,返回td集，如 $(table).datagrid("getColumnJqByField","name")或$(table).datagrid("getColumnJqByField",{field:"name",isFooter:true})
            if (!option) {
                return $();
            }
            var field = "";
            if (typeof option == "string") {
                field = option;
            } else {
                field = option.field;
            }
            if (!field) {
                return $();
            }
            console.time("getColumn");
            var $table = $(jq[0]);
            var $container;
            if (option.isFooter) {
                $container = $table.data("datagrid").dc.footer2;
            } else {
                $container = $table.data("datagrid").dc.body2;
            }
            var $tds = $container.find("td[field=" + field + "]");
            console.timeEnd("getColumn");
            return $tds;
        }
    });
    
    //改写datebox的方法
    $.fn.datebox.defaults.onHidePanel = function () {
        var $combo = $(this).data("combo").combo;
        $combo.find("input.combo-text").val($combo.find("input.combo-value").val());
    };
    
    //表单提交,格式:[url,onSubmit,success,onLoadError]
    $.prototype.formSubmit = function (options) {
        var $form = $(this);
        var _properties = ["url", "onSubmit", "success", "onLoadError"];//所有属性名
        var _defaultValues = { "url": null, "onSubmit": null, "success": null, "onLoadError": null };
        var _option = {};
        for (var i = 0, len = options.length; i < len; i++) {
            _option[_properties[i]] = options[i];
        }
        _option = $.extend(_defaultValues, _option);
        $form.form("submit", {//ajax form提交
            url: _option.url,//目标地址
            onSubmit: _option.onSubmit,//提交之前,参数param
            success: _option.success,//提交成功,参数(返回值)data
            onLoadError: _option.onLoadError//发生错误
        });

    };

}(jQuery));

//创建datagrid
(function ($) {

    //多表头
    //获得列属性,columns格式:[[["field","title",width,rowspan,colspan,sortable,editor,formatter(v,r,index){}],...],[[...],[...]]]
    Common.Functions.getGroupColumns = function (columns) {
        var optNames;//= ["field", "title", "width", "rowspan", "colspan", "sortable", "editor", "formatter"];
        var defaultOpts;
        if (isNullOrEmpty(columns) || columns.length == 0) {
            return null;
        }
        if (columns.length == 1) {//单行
            optNames = ["field", "title", "width", "sortable", "editor", "formatter"];
            defaultOpts = Common.Variables.datagrid.defaultColumns;
        } else {//两行
            optNames = ["field", "title", "width", "rowspan", "colspan", "sortable", "editor", "formatter"];
            defaultOpts = Common.Variables.datagrid.defaultGroupColumns;
        }
        var returnColumns = [];
        var columnsWidth = 0;
        for (var _i = 0, _len = columns.length; _i < _len; _i++) {
            var tempColumns = [];
            var c = columns[_i];
            var _width = 0;
            for (var i = 0, len = c.length; i < len; i++) {
                var _column = c[i];
                var col = {};
                col[optNames[0]] = _column[0];
                for (var j = 1, l = _column.length; j < l; j++) {
                    if (j == 1) {
                        if (_column[j]) {
                            col[optNames[j]] = _column[j];
                        } else {
                            col[optNames[j]] = _column[0];
                        }
                    } else {
                        if (_column[j]) {
                            col[optNames[j]] = _column[j];
                        } else {
                            col[optNames[j]] = defaultOpts[j];
                        }
                    }
                }
                col.width = col.width ? col.width : 80;
                if (col.colspan == 1 || isUndefined(col.colspan)) {
                    columnsWidth += col.width+1;
                }
                col.haligh = "center";
                col.align = "center";
                tempColumns.push(col);
            }
            returnColumns.push(tempColumns);
        }
        //Common.Variables.datagrid.columnsAllWidth = columnsWidth.max();
        Common.Variables.datagrid.options.columnsAllWidth = columnsWidth;
        return returnColumns;
    };

    //创建datagrid，注意:JsonResult返回格式为jr.Data=new {rows=list,total=20}
    Common.Functions.createDataGrid = function (options) {//除非特殊需要，基本不需要修改
        if (!options.target) {
            return false;
        }
        var _target = $(options.target);
        if(_target.length==0) {
            _target = $(_target.selector);
        }
        if (_target.length == 0) {
            return false;
        }
        var _options = $.extend({
                striped: true, fitColumns: false, nowrap: true, autoRowHeight: false,showFooter:false,
                columns: null,frozenColumns:null, url: "", idField: null, toolbar: null,
                pagination: true, pagePosition: "bottom", pageNumber: 1, pageSize: 10, pageList: [10, 20, 30, 40,50],
                remoteSort: true, sortName: null, sortOrder: "asc", singleSelect: false, checkOnSelect: true, selectOnCheck: true,
                rowStyler:null,showNullMessage:true,
                loadFilter: function (data) {
                    var _data = {};
                    if (typeof data.rows == "undefined") {
                        _data.rows = data.Data.rows;
                    } else {
                        _data.rows = data.rows;
                    }
                    if (typeof data.total == "undefined") {
                        _data.total = data.Data.total;
                    } else {
                        _data.total = data.total;
                    }
                    return _data;
                },
                onDblClickRow: function (rowIndex, rowData) { return true; },
                onClickRow: function (rowIndex, rowData) { return true; },
                onAfterEdit: function (rowIndex, rowData, changes) { },
                onBeforeLoad: function (param) { },
                onLoadError:function(){},
                onCreating: function (data) { },//创建之前要完成的事件
                onSortColumn: function (sort, order) { },
                onUnselect: function (rowIndex, rowData) { },
                onSelect: function (rowIndex, rowData) { },
                maxWidth:0,maxHeight:0
            }, options);
            
            Common.Variables.datagrid.options.maxWidth = _options.maxWidth;
            Common.Variables.datagrid.options.maxHeight = _options.maxHeight;
        
            var _columns;
            var _frozenColumns;
            if (_options.columns == null || _options.columns.length == 0) {
                return;
            } else {
                _columns = Common.Functions.getGroupColumns(_options.columns);
            
                if (_options.checkbox) {//是否显示checkbox
                    Common.Variables.datagrid.options.columnsAllWidth += 30;
                    var _rowspan = 1;
                    if (typeof _columns[0][0].rowspan == "undefined" || typeof _columns[0][0].colspan == "undefined") {
                        _rowspan = 1;
                    } else {
                        _rowspan = (_columns[0][0].rowspan != 1 || _columns[0][0].colspan != 1) ? 2 : 1;
                    }
                        
                    var newColumns;
                    var isFrozen = false;
                    if (_options.frozenColumns != null) {
                        newColumns = _options.frozenColumns[0];
                        isFrozen = true;
                    } else {
                        newColumns = _columns[0];
                    }
                    newColumns = [{ field: "checkbox", title: "", rowspan: _rowspan, width: 30, checkbox: true }].concat(newColumns);//.concat(_columns[0]);
                    if (isFrozen) {
                        _options.frozenColumns[0] = newColumns;
                    } else {
                        _columns[0] = newColumns;
                    }
                
                    //_columns[0] = newColumns; 
                }
            }
        
        
            //为datagrid添加父元素并设置其宽度
            var _w;
            var $div = $("<div style='margin:auto;'></div>");
            if (_options.maxWidth > Common.Variables.datagrid.options.columnsAllWidth) {//设置父元素宽度
                _w = Common.Variables.datagrid.options.columnsAllWidth + 2;
            } else {
                _w = options.maxWidth;
            }
            $div.width(_w);
            _target.parent().append($div);
            _target.appendTo($div);

            //待创建的datagrid属性
            var _opts = {
                striped: _options.striped, fitColumns: _options.fitColumns,nowrap:_options.nowrap,autoRowHeight:_options.autoRowHeight,
                pagination: _options.pagination, pagePosition: _options.pagePosition,
                pageNumber: _options.pageNumber, pageSize: _options.pageSize,
                pageList: _options.pageList, idField: _options.idField,
                singleSelect: _options.singleSelect, checkOnSelect: _options.checkOnSelect, selectOnCheck: _options.selectOnCheck,
                remoteSort: _options.remoteSort,sortName:_options.sortName,sortOrder:_options.sortOrder,
                loadFilter: _options.loadFilter,showFooter:_options.showFooter,
                columns: _columns,frozenColumns:_options.frozenColumns,
                url: _options.url,
                method: "post",
                toolbar: _options.toolbar,
                rowStyler: _options.rowStyler,
                loadFilter:function(data) {
                    var dat = _options.loadFilter.call(_target, data);
                    return dat;
                },
                showLoadMsg:false,//扩展的，用于指示是否显示 自带的“mask”
                onLoadError: function () {
                    parent.closeMaskAllOfDatagrid();//关闭遮罩
                    $(this).datagrid("loaded");
                    _options.onLoadError.call(_target);
                },
                onBeforeLoad: function (param) {
                    //$(this).datagrid("options")["showLoadMsg"] = false;
                    
                    ClearSearchPossible();//放大镜:当显示的为空时，清空隐藏的input
                    //$(this).data("_loaded", false);
                    var result = _options.onBeforeLoad.call(_target, param);
                    if (result === false) {
                        return false;
                    }
                    parent.showMaskAllOfDatagrid();//显示遮罩
                    return result;
                },
                onLoadSuccess: function (data) {
                    if (_options.showNullMessage === true) {
                        if (data.rows.length == 0) {
                            DisplayNullData(this);//数据为空时显示的信息
                        }
                    }
                    var r = _options.onCreating.call(this, data);
                    if (r === false) {
                        parent.closeMaskAllOfDatagrid();//关闭遮罩
                        return;
                    }
                    
                    var _data = data;
                    var _width;
                    var _height;
                    var $this = $(this);
                    var _maxHeight = $this.data("maxHeight"); //Common.Variables.datagrid.options.maxHeight;
                    var _maxWidth = $this.data("maxWidth");//Common.Variables.datagrid.options.maxWidth;
                    var allColumnsWidth = $this.data("columnsAllWidth");//Common.Variables.datagrid.options.columnsAllWidth;
                    //console.info(_maxHeight + "----" + (_data.rows.length*30));
                    if (_maxHeight != 0 && _data.rows.length * 30 >= _maxHeight+30) {
                        _height = _maxHeight+35;
                        if (_options.toolbar) {
                            _height += $(_options.toolbar).height();
                        }
                    } else {
                        _height="auto";
                    }
                    if (allColumnsWidth>_maxWidth) {
                        _width = _maxWidth;
                    } else {
                        _width = allColumnsWidth + 2;
                        if (_height != "auto") {
                            _width += 20;
                        } 
                    }
                    $(this).datagrid("resize", { width: _width, height: _height });
                    parent.closeMaskAllOfDatagrid();//关闭遮罩
                },
                onDblClickRow: function (rowIndex, rowData) {
                    var r = _options.onDblClickRow.call(_target, rowIndex, rowData);
                    if (r === false) {
                    } else {
                        _target.datagrid("beginEdit", rowIndex);
                        _target.data("lastRowIndex", rowIndex);
                    }
                    return true;
                },
                onClickRow: function (rowIndex, rowData) {
                    var r = _options.onClickRow.call(_target, rowIndex, rowData);
                    if (!(r === false)) {
                        if (_target.data("lastRowIndex") != null) {
                            _target.datagrid("endEdit", _target.data("lastRowIndex"));
                            _target.data("lastRowIndex",null);
                        }
                    }
                },
                onAfterEdit: function (rowIndex, rowData, changes) {
                    _options.onAfterEdit.call(_target,rowIndex, rowData, changes);
                },
                onSortColumn: function(sort, order) {
                    _options.onSortColumn.call(_target,sort,order);
                },
                onUnselect: function(rowIndex, rowData) {
                    _options.onUnselect.call(this, rowIndex, rowData);
                },
                onSelect: function(rowIndex, rowData) {
                    _options.onSelect.call(this, rowIndex, rowData);
                },
                loading: function(jq) {
                    console.info("loading");
                }
            };
            _target.datagrid(_opts);
            _target.data("maxHeight", Common.Variables.datagrid.options.maxHeight);
            _target.data("maxWidth", Common.Variables.datagrid.options.maxWidth);
            _target.data("columnsAllWidth", Common.Variables.datagrid.options.columnsAllWidth);
            _target.data("lastRowIndex",null);
        //console.info(Common.Variables.datagrid.options);
    };
    
}(jQuery));
 
//合并单元格方法组
(function ($) {
    //多列排序
    sort = function(r1, r2) {
        var r = 0;
        for (var i = 0; i < sortFields.length; i++) {
            var sn = sortFields[i];
            var so = sortOrders[i];
            var col = $(dataGrid).datagrid("getColumnOption", sn);
            var _sort = col.sorter || function(a, b) {
                return a == b ? 0 : (a > b ? 1 : -1);
            };
            r = _sort(r1[sn], r2[sn]) * (so == "asc" ? 1 : -1);
            if (r != 0) {
                return r;
            }
        }
        return r;
    };

    getMergeCells = function(options) {
        //var options=$.extend({fields:[],target:null},options);
        //if(options.fields.length==0||options.target===null){
        //	return false;
        //}

        var _target = $(options.target); //表格
        var _opt = _target.datagrid("options"); //datagrid的options属性,用来操作idField
        var _data = deepClone(_target.datagrid("getData").rows); //数据
        if (_data.length <= 1) {
            return null;
        }
        var _fields = deepClone(options.fields); //合并的字段
        var _mergeCells = []; //待返回的“规则”

        var fieldIndex = 0; //字段序号,用于使用下一个字段
        var startIndex = 0; //查找开始index
        var endIndex = _data.length; //查找的结束index
        var mergeIndex = 0; //"规则"的index,用于下一个字段的单元格查找范围
        var searchStart = false; //标识当前字段查找开始
        var total = 0; //全部记录
        while (1) {
            var rowCount = 0;
            var rowIndex = -1;
            for (var i = startIndex, len = endIndex; i < len; i++) { //查找范围
                if (i + 1 != len && _data[i][_fields[fieldIndex]] == _data[i + 1][_fields[fieldIndex]]) { //字段值相等
                    if (rowIndex == -1) {
                        rowIndex = i;
                    }
                    rowCount++;
                } else { //字段值不相等,则记录“规则”
                    if (rowCount > 0) {
                        _mergeCells.push({ field: _fields[fieldIndex], index: rowIndex, rowspan: rowCount + 1 });
                        //mergeIndex++;
                    }else{
						_mergeCells.push({field:_fields[fieldIndex],index:i,rowspan:0});
					}
                    rowIndex = -1;
                    rowCount = 0;
                }
            }
            if (!searchStart) {
                total = _mergeCells.length;
                searchStart = true;
            }
            if (mergeIndex >= total) {
                fieldIndex++;
                searchStart = false;
            }

            if (fieldIndex >= _fields.length) {
                break;
            }
            startIndex = _mergeCells[mergeIndex].index;
            endIndex = _mergeCells[mergeIndex].rowspan + startIndex;
            mergeIndex++;
        }
        return _mergeCells;
    }

    var first = true;
    var sortFields = [];//待排序的字段,在sort方法里调用
    var sortOrders = [];//待排序字段的排序规则,在sort方法里调用
    var dataGrid = {};//待排序的datagrid,在sort方法里调用
    //单元格合并
    mergeCells = function(options) {
        var options = $.extend({ fields: [], sortOrders: [], target: null }, options);
        if (options.fields.length == 0 || options.target == null) {
            return;
        }
        options.target = $(options.target);
        //if(first){
        if (options.sortOrders.length == 0) {
            for (var i = 0; i < options.fields.length; i++) {
                options.sortOrders.push("asc");
            }
        }
        sortFields = options.fields;
        sortOrders = options.sortOrders;
        dataGrid = options.target;
        dataGrid.datagrid("getData").rows.sort(sort); //全部按正序排列
        //first=false;
        //}
        var _mergeCells = getMergeCells(options); //获取单元格合并规则
        if (_mergeCells == null) {
            return;
        }
        options.target.datagrid("loadData", options.target.datagrid("getData"));
        for (var i = 0; i < _mergeCells.length; i++) { //按规则合并
            options.target.datagrid("mergeCells", _mergeCells[i]);
        }
    };
}(jQuery));

//输入框的自动检索功能,使用方法:<input model="autoSearch-user" data-options="{onEnd:onEnd,field:'Name'}"/>
//model以autoSearch-为前缀,user对应一个类型,data-options为参数，onEnd为结束事件(调用者为当前input),field决定将哪个字段的值赋给input
(function ($) {
    var autoSearch = {
        table: null,
        currentTarget: null,
        styleSheet: null,
        className: "model",
        tableClass: "mytable",
        tdClass: "mytd",
        trClass: "mytr",
        hiddenClass:"autoSearchHiddenInput",
        endEventName: "onEnd",
        maxRowNum: 10,
        maxHeight: 200,
        formatter: function(index, row) {
            var width = (this.currentTarget.width() / 2 - 1) + "px";
            return "<td class='" + this.tdClass + "' style=\"width:" + width + "\">" + row.Name + "</td><td class='" + this.tdClass + "' style=\"width:" + width + ";text-align:left;\">" + row.Id + "</td>";
        },
        fields: {
            user: {id:"UserID",name:"UserName"}
        },
        parse: function (ele) {//解析input的自动检索功能
            var $grandParent = ele ? $(ele) : $("body");//input的父节点
            var $ele = $grandParent.find(("input[" + this.className + "^=autoSearch]"));
            if ($ele.length == 0) {
                return;
            }
            $ele.each(function() {
                var model = ($(this).attr(autoSearch.className).split("-"))[1];
                if (typeof model == "undefined") {
                    model = "user";
                }
                $(this).data({ model: model });
                var options = $(this).data("options");
                if (options) {
                    options = eval('(' + options + ')');
                    if (options.valueField) {
                        var $hidden = $("<input name='" + options.valueField + "' type='hidden' class="+autoSearch.hiddenClass+" />");
                        options=$.extend({ defaultValue: "" }, options);
                        $hidden.val(options["defaultValue"]);
                        $hidden.insertAfter(this);
                        $(this).data("valueEle", $hidden);//保存隐藏的input元素
                    }
                }
            });

            //初始化table
            var $table = $("#forAutoSearch");
            if ($table.length == 0) {
                $table = $("<div id='ScrollGunDong_id' style='border-collapse:collapse;border:0px solid #95B8E7;border-bottom:none;overflow-y:auto;overflow-x:hidden;position:absolute;display:none;z-index:10000;width:112px;;max-height:" + this.maxHeight + "px;'><table id='forAutoSearch' cellspacing=0  ></table></div>").appendTo("body").find("table");
                $table.addClass(this.tableClass);
            }
            this.table = $table;
            //为table添加事件(click和mouseover事件)
            $table.parent().on("click", "tr", function(event) {
                var $this = $(this);
                if ($this.is(":hidden")) { //隐藏元素，删除的元素仍在jquery缓存中
                    return;
                }
                var $table = autoSearch.table;
                var index = $this.attr("index") - 0;
                $table.children().remove().end().parent().hide(); //.hide();//移除字元素并隐藏
                //autoSearch.currentTarget.val($table.data("data")[index].Name);//为搜索框赋值
                $("#tooltip").remove();//隐藏自动搜索下拉框
                var value;
                var field;
                var options = autoSearch.currentTarget.data("options");
                if (typeof options != "undefined") {
                    options = eval('(' + options + ')');
                    field = options.field ? options.field : "Name";
                    value = $table.data("data")[index][field]; 
                    var onEndEvent = options[autoSearch.endEventName] ? options[autoSearch.endEventName] : null; //外界事件
                    if (onEndEvent != null) {
                        onEndEvent.call(autoSearch.currentTarget, $table.data("data")[index]);
                    }
                } else {
                    value = $table.data("data")[index]["Name"];
                }
                
                if (autoSearch.currentTarget.data("valueEle").val() != $table.data("data")[index]["Id"]) {//值改变，触发onchange事件
                    setTimeout(function () {
                        autoSearch.currentTarget.data("valueEle").trigger("change", value);
                    }, 50);
                }
                autoSearch.currentTarget.val(value).data("lastValue", value);//为当前input赋值
                var $hidden = autoSearch.currentTarget.data("valueEle");//隐藏的input
                if ($hidden) {
                    $hidden.val($table.data("data")[index]["Id"]);//为搜索框赋值
                }
            }).on("mouseover", "tr", function(e) { //改变背景色<显示内容提示（panjun修改）>
                var $this = $(this);
                var content1 = $this.children("td:eq(0)").text();//第一列内容
                var content2 = $this.children("td:eq(1)").text();//第二列内容
                var toolTip = "<div id='tooltip' width='300px' height='12px' style='margin-left:15px;font-size:12px;position:absolute;border:solid #aaa 1px;background-color:#F9F9F9;z-index:10000;'>" + content1 + "  " + content2 + "</div>";
                $("body").append(toolTip); 
		        $("#tooltip").css({ 
			        "top" :e.pageY + "px",//坐标 
			        "left" :e.pageX + "px" 
		        });
		        $(".mytable tr").mouseout(function () {
		            $("#tooltip").remove();//隐藏自动搜索下拉框
		        });
		        $(".mytable tr").mousemove(function (e) {
		            $("#tooltip").css({
		                "top": (e.pageY + 5) + "px",
		                "left": (e.pageX + 2) + "px"
		            });
		        });
               
                $this.addClass("mouseover").siblings().removeClass("mouseover");                
            });
            //初始样式表
            var $style=$("head").find("#autoSearchStyle");
            if ($style.length == 0) {
                var style = "";
                style += ("<style id='autoSearchStyle'>");
                style += ("#forAutoSearch {background-color: white;} ");
                style += ("#forAutoSearch .mouseover{background-color:skyblue;} ");
               // style += (".mytable{border:1px solid #95B8E7;} ");
                style += ("</style>");
                
                $style = $(style).appendTo("head");
            }
            this.styleSheet = $style;

            //$ele.parents(".panel-body:eq(0)").on("scroll", function(event) {//滚动条事件，保证table紧随input
            //    var $target = $(event.target);
            //    if (autoSearch.table.is(":visible")) {
            //        var $div = autoSearch.table.parent();
            //        $div.css({ left: (autoSearch.table.data("left") - $target.scrollLeft()) + "px", top: (autoSearch.table.data("top") - $target.scrollTop()) + "px" }); 
            //    }
            //});

            $ele.data("lastValue",null);
            //为input添加事件
            $ele.on("keyup", function (event) {//将propertychange input改为keyup（propertychange可用js触发，input不行）
                if (event.which == 13) {
                    return;
                }
                var $this = $(this);
                var $val = $this.val().replace(/^\s*/, "").replace(/\s*$/, "");
                if ($val && $val != $this.data("lastValue")) {
                    autoSearch.getData($this.data("model"), $this.val());
                } else {//空值，隐藏table
                    if ($val=="") {
                        autoSearch.table.children().remove().end().parent().hide();
                    }
                }
                $this.data("lastValue", $val);
            }).on("click focus", function (event) {
                var $this = $(this);
                autoSearch.currentTarget = $this;
            }).on("blur", function (event) {
                var $this = $(this);
                var $hide = $this.data("valueEle");
                
                if (autoSearch.table.is(":visible")) {
                    setTimeout(function() {
                        if (autoSearch.table.is(":visible")) {
                            $this.data("lastValue", "");
                            var rIndex = autoSearch.table.find("tr.mouseover").index();
                            var row = autoSearch.table.data("data")[rIndex];
                            $this.val(row["Name"]);
                            $hide.val(row["Id"]);
                            autoSearch.table.children().remove().end().parent().hide();
                            //$("#ScrollGunDong_id").hide();
                        }
                        if ($hide) {
                            if (!$this.val()) {
                                $hide.val("");
                            } else {
                                if (!$hide.val()) {
                                    $this.val("");
                                }
                            }
                        }
                    }, 200);
                } else {
                    if ($hide) {
                        if (!$this.val()) {
                            $hide.val("");
                        } else {
                            if (!$hide.val()) {
                                $this.val("");
                            }
                        }
                    }
                }
            }).on("keydown", function (event) {//添加上下及回车事件
                var $table = autoSearch.table;
                if ($table.is(":visible")) {
                    var key = event.which;
                    var scroll_height = 10;//每次按键的滚动长度
                    var gundong = document.getElementById("ScrollGunDong_id"); 
                    
                    switch (key) {
                        case 38://上
                            var currTr = $table.find("tr.mouseover");
                            if (currTr.index() > 0) {
                                currTr.prev().addClass("mouseover").end().removeClass("mouseover");
                               // gundong.scrollTop = gundong.scrollTop - scroll_height;//向上滚动
                            }
                            break;
                        case 40://下
                            var currTr = $table.find("tr.mouseover");
                            if (currTr.index() <$table.find("tr").length-1) {
                                currTr.next().addClass("mouseover").end().removeClass("mouseover");
                                //gundong.scrollTop = gundong.scrollTop + scroll_height;//向下滚动
                            }
                            break;
                        case 13://回车
                            var currTr = $table.find("tr.mouseover");
                            currTr.trigger("click");
                            $("#tooltip").remove();//隐藏自动搜索下拉框
                            return false;
                            break;
                        default:
                    }
                }
            });
        },
        getData: function (model, val) {//获取数据，参数：类型，检索的值
            
            $.ajax({
                url: location.protocol + "//" + location.host + "/Common/AutoComplete/Get",
                async: true,
                type: "post",
                global: true,
                data: { Type: model, Keyword: val },//post的参数
                success: function (data, textStatus) {
                    autoSearch.currentTarget.data("valueEle").val("");
                    var data = eval('(' + data + ')');
                    data = data.rows;
                    var $this = autoSearch.currentTarget;
                    var $div = autoSearch.table.parent();
                    autoSearch.currentTarget = $this;
                    var position = $this.offset();
                    //$div.appendTo($this.parent());//加入到输入框的父级
                    autoSearch.table.parent().css({ left: position.left + "px", top: (position.top + $this.height() + 2) + "px" });
                    autoSearch.table.data({ left: position.left, top: position.top + $this.height() + 5 });
                    ////////////////////
                    var trStr = "";
                    var rHeight;
                    var rowCount;
                    for (var i = 0, len = data.length; i < len; i++) {
                        trStr += ("<tr index='" + i + "' class='" + autoSearch.trClass + "'>");
                        trStr += autoSearch.formatter(i, data[i]);
                        trStr += ("</tr>");
                    }
                    autoSearch.table.children().remove();
                    $(trStr).appendTo(autoSearch.table);
                    $div.show();
                    //var divHeight;
                    //rHeight = autoSearch.table.find("tr:eq(0)").height();
                    //rowCount = data.length;
                    //rowCount = rowCount > autoSearch.maxRowNum ? autoSearch.maxRowNum : rowCount;
                    //$div.height(rHeight * rowCount+10);
                    autoSearch.table.find("tr:eq(0)").addClass("mouseover");
                    autoSearch.table.data("data", data);
                    autoSearch.table.find("tr:gt(" + autoSearch.maxRowNum + ")").hide();
                }
            });

        }
    };

    window.parseAutoSearch= function(parent) {
        autoSearch.parse(parent);
    };

    $(function () {
        parseAutoSearch();
    });
    
    //自定义编辑器
    $.extend($.fn.datagrid.defaults.editors, {
        autoComplete: {
            init: function (container, options) {
                var $editorContainer = $('<span></span>');

                var $input = $("<input model='autoSearch-"+options.model+"' />");

                $editorContainer.append($input);
                $editorContainer.appendTo(container);
                parseAutoSearch($editorContainer);
                return $input;
            },
            getValue: function (target) {
                return $(target).val();
            },
            setValue: function (target, value) {
                $(target).val(value);
            },
            resize: function (target, width) {
                var span = $(target);
                //if ($.boxModel == true) {
                //    span.width(width - (span.outerWidth() - span.width()) - 10);
                //} else {
                //    span.width(width - 10);
                //}
                span.width(width - 10);
            }
        }
    });

}(jQuery));

//其他方法(小功能/语句很少)
(function ($) {
    //将DateTime转为字符串(包括c#传来的)
    window.DtToString = function(dateTime) {
        if (!dateTime) {
            return "";
        }
        if (/^\d{4}(-|\/|\.)(\d{1,2})(-|\/|\.)(\d{1,2})$/.test(dateTime)) {
            return DateFormatter(dateTime);
        }
        var str = (dateTime + "").replace(/\/Date\(/, "").replace(/\)\//, "");
        var dtStr;
        try {
            dtStr = DateFormatter(new Date(parseInt(str)));
        } catch(e) {
            dtStr = dateTime;
        }
        if (dtStr == "1-01-01" || dtStr=="1900-01-01") {
            dtStr = "";
        }
        return dtStr;
    };
    
    //将c#的DateTime转为日期字符串，参数:table，rows数据,需要矫正的字段(日期类型)
    window.AdaptDt = function (table, rows, fields) {
        if (!fields) {
            return;
        }
        console.time("AdaptDt");
        var fieldArr = fields.split(",");
        var fieldLen = fieldArr.length;
        for (var i = 0, len = rows.length; i < len; i++) {
            var row = rows[i];
            for (var j = 0; j < fieldLen; j++) {
                row[fieldArr[j]] = DtToString(row[fieldArr[j]]);
            }
        }
        console.timeEnd("AdaptDt");
    };

    //当表格数据为空时显示的数据格式（暂未将冻结列加入）,参数：table,message(可空,默认为"无符合条件数据"(红色))
    window.DisplayNullData = function (table,message) {
        console.time("showNullData");
        var $table = $(table);
        if (!$table) {
            return ;
        }
        var $tr = $("<tr class='datagrid-row'><td style='text-align:center;color:red;'></td></tr>");
        var mess = message ? message : Common.Variables.datagrid.nullDataMessage;
        var fields = $table.datagrid("getColumnFields"); 
        try {
            var dc = $table.data("datagrid").dc;
            dc.body2.children("table").append($tr).width(dc.header2.find("tr:eq(0)").width());
            $tr.children("td").attr("colspan", fields.length).html(mess);
        } catch(e) {
            
        }
        console.timeEnd("showNullData");
    };

    //日期格式化函数(为了统一日期格式),参数:日期(字符串或date)
    window.DateFormatter = function(date) {
        var dt = date;
        if (typeof date === "string") {
            dt = Date.Parse(date);
        } 
        return $.fn.datebox.defaults.formatter(dt);
    };

    //放大镜:当显示的为空时，清空隐藏的input,待修改（实际的hidden input尚未体现）
    window.ClearSearchPossible = function() {
        console.time("searchInput");
        var $searchSpan = $("form").find("span.Search_InputSpan");
        for (var i = 0, len = $searchSpan.length; i < len; i++) {
            //if (!$($searchSpan[i]).children("input:eq(0)").val()) {
            //    $($searchSpan[i]).children("input:hidden").val("");
            //} 
            var $prev = $($searchSpan[i]).prev();
            if (!$($searchSpan[i]).children("input").val()) {
                if ($prev.is(":hidden")) {
                    $prev.val("");
                }
            }
        }
        console.timeEnd("searchInput");
    };

    //按name填充值，参数：数据object，上下文（要填充的范围）
    window.FillByName = function(data, content) {
        console.time("FillByName");
        if (data != null && !(data.constructor === {}.constructor)) {
            return;
        }
        var $content = $(content ? content : "body");
        //var $haveNames=$content.find("input,span,label").filter("*[name]");
        var $spanLabels = $content.find("span[name],label[name]");// = $haveNames.filter("span,label");
        //var $inputs = $haveNames.filter("input");
        for (var i = 0, len = $spanLabels.length; i < len; i++) {
            var $ele = $($spanLabels[i]);
            $ele.text(data[$ele.attr("name")]);
        }
        //for (var i = 0, len = $inputs.length; i < len; i++) {
        //    var $ele = $($inputs[i]);
        //    $ele.val(data[$ele.attr("name")]);
        //}
        
        console.timeEnd("FillByName");
    };

}(jQuery));


