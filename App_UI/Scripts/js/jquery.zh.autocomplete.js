/**
 * 简单实用的支持中文的自动完成插件
 * 
 * @param url
 *            获得数据的URL
 * @param q
 *            查询用关键字名称
 * @param boxId
 *            外边框ID
 * @param tblId
 *            内表格ID
 * @param format
 *            返回数据格式,默认为null,如返回JSON数据为[{id:'aaa',name:'ccccc'}]
 *            format:['name','id'],其中第一个项会作为text显示出来,其它项会作为afterSelect中对象属性
 * @param processData
 *            对返回的数据进行预处理 最后 return 处理过的数据即可
 * @param showList
 *            如何显示数据
 * @param select
 *            选中一条记录时触发
 * @param afterSelect
 *            选中一条记录后触发
 * @param rowHeight
 *            行高
 * @param maxLength
 *            最大行数
 * @param zIndex
 *            层的zIndex属性，防止被其他层覆盖
 * 
 * @example
 * $("#input_id").zhAutoComplete({url:'test.php'});
 * 
 * @example
 * override actions
 * $("#input_id").zhAutoComplete({url:'test.php',processData:function(data}{},afterSelect:
 * function(td){td.id ....});
 */

jQuery.fn.zhAutoComplete = function (options) {
    var keyWordOld = null;
    var input = $(this);
    $(input).attr("autocomplete", "off");
    var offset = {};
    offset.width = $(this).width();
    var settings = {
        url: 'test.php',
        q: 'name',
        format: null,
        maxLength: 10,
        type: 'json',
        boxId: '_zhAC_Box',
        zIndex: 10,
        tblId: '_ZhAC_Table',
        offset: offset,
        rowHeight: 18,
        processData: function (data) {
            return data;
        },
        showList: function (data) {
            console.log(data);
            console.log(data); 
            console.log("123");
            if ($("#" + settings.boxId).length > 0)
                $("#" + settings.boxId).remove();
            if (data.length == 0)
                return;
            var top = document.getElementById(input[0].id)
					.getBoundingClientRect().top;
            var left = document.getElementById(input[0].id)
					.getBoundingClientRect().left;
            var width = settings.offset.width - 1;

            var box = $("<div id='" + settings.boxId + "'></div>");
            var table = $("<table id='" + settings.tblId + "' style='font-size: 12px;font-family: \"宋体\";'></table>");
            if (data.length > settings.maxLength) {
                $(box).css({
                    'background-color': '#FFFFFF',
                    position: 'absolute',
                    border: '1px solid #817F82',
                    'z-index': settings.zIndex,
                    top: top + $(input).height() + 5,
                    left: left,
                    width: width + 1,
                    height: settings.maxLength * settings.rowHeight,
                    'overflow-y': 'auto'
                });
            } else {
                $(box).css({
                    'background-color': '#FFFFFF',
                    position: 'absolute',
                    border: '1px solid #817F82',
                    'z-index': settings.zIndex,
                    top: top + $(input).height() + 5,
                    left: left,
                    width: width + 1
                });
            }
            $(table).attr("cellpadding", 2).attr("cellspacing", 0).attr(
					"width", "100%").attr("border", 0);
            for (i = 0; i < data.length; i++) {
            //for (i = 0; i < 1; i++) {
                var tr = $("<tr></tr>");
                tr.css({
                    cursor: 'pointer',
                    'padding-top': '2px',
                    'padding-buttom': '2px'
                });
                if (settings.format) {
                    var td = $("<td style='padding-left:5px;'>"
							+ data[i][settings.format[0]] + "</td>");
                    for (j = 1; j < settings.format.length; j++) {
                        $(td).attr(settings.format[j],
								data[i][settings.format[j]]);
                    }
                } else {
                    var td = $("<td style='padding-left:5px;'>" + data[i]
							+ "</td>");
                }
                $(tr).append(td);
                $(table).append(tr);
            }

            $(box).append(table);
            $("body").append(box);

            $("#" + settings.tblId + " tr").bind("mouseover", function () {
                $(this).css("background-color", "#E2EAFF");
            }).bind("mouseout", function () {
                $(this).css("background-color", "#FFFFFF");
            });

            $("#" + settings.tblId + " tr td").bind("click", function () {
                keyWordOld = $(this).text();
            });
            $("#" + settings.tblId + " tr td").bind("click",
					settings.selectParam);
            $(document).bind("click", function () {
                $(box).remove();
            });
        },
        selectParam: function () {
            $(input).val($(this).text());
            settings.afterSelect($(this));
        },
        afterSelect: function (td) {
            console.log("calling afterSelect function...");
        }
    };

    options = options || {};
    $.extend(settings, options);
    $(this).bind("keyup", function (event) {
        if (event.keyCode >= 37 && event.keyCode <= 40)
            return;
        var keyword = $.trim($(this).val());
        if (!keyword) {
            return;
        } else {
            if (keyWordOld == keyword) {
                return;
            }
            keyWordOld = keyword;
        }
        param = settings.q + "=" + keyword;
        $.get(settings.url, param, function (data) {
            var ds = settings.processData(data);
            settings.showList(ds);
        }, settings.type);
    });
};
