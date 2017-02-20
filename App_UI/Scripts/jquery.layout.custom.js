/// <reference path="jquery-1.9.1.js" />
/// <reference path="jquery.cookie.js" />

$.ajaxSetup({ cache: false });

$(document).ajaxStart(function () {
    $("#page-loading").show();
});

$(document).ajaxStop(function () {
    $("#button a").button();
    $("#button_create").button({
        icons: {
            primary: "ui-icon-document"
        }
    });
    $("#button_export").button({
        icons: {
            primary: "ui-icon-extlink"
        }
    });
    $("#button_search").button({
        icons: {
            primary: "ui-icon-search"
        }
    });
    $("#button_back").button({
        icons: {
            primary: "ui-icon-triangle-1-w"
        }
    });
    $("#button_save").button({
        icons: {
            primary: "ui-icon-disk"
        }
    });
    $("#page-loading").hide();
});

$(document).ajaxError(function (event, request) {
    $("#errormsg").dialog({
        title: "系统信息",
        autoOpen: false,
        modal: true,
        width: 800,
        height: 400,
    });
    if (request.responseText == "") {
        $("#errormsg").html("您的网络无法访问到服务器，请稍后再试！");
    }
    else {
        $("#errormsg").html(request.responseText);
    }
    $("#errormsg style").remove();
    $("#errormsg").dialog("open");
});

$(document).ready(function () {
    $('body').layout({
        applyDemoStyles: false
        , south__spacing_open: 0		// no resizer-bar when open (zero height)
        , north__spacing_open: 0		// no resizer-bar when open (zero height)
    });

    if ($.cookie("theme") != null) {
        $("#theme").attr("href", $.cookie("theme"));
        $("#changetheme").val($.cookie("theme"));
    }

    $("#changetheme").change(function () {
        var theme = $(this).val();
        $.cookie("theme", theme);
        $("#theme").attr("href", theme);
    });
});