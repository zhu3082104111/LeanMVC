var gTitle = "";//记录tab的title,主要用于tab切换
var maskSources = { frame: 0, datagrid: 1 };
var gMaskSource = null;


$(function () {
    var tabs = $("#tabs");
    tabs.tabs({
        onSelect: function (title, index) {//选中时事件,清空上一个标签的内容，并刷新当前页面
            gTitle = title;
        },
        onClose: function (title, index) {
            gTitle = "";
        },
        onLoad: function (panel) {//加载成功后解析自动检索功能
            //loadSearchData();
            //parseAutoSearch(panel);
            //$.ajaxSettings.async = false;
            
        },
        onAdd: function (title, index) {
            showMaskAllOfFrame();//显示遮罩
        },
        onUpdate: function (title, index) {
            showMaskAllOfFrame();//显示遮罩
        }
    });
});

//获取当前的tab元素
function getCurrentTab(tabs) {
    tabs = tabs ? tabs : "#tabs";
    tabs = $(tabs);
    return tabs.tabs("getSelected");
}


function LoadMenu() {
    tabClose();
    tabCloseEven();
    $('#css3menu a').click(function () {
        $('#css3menu a').removeClass('active');
        $(this).addClass('active');
        alert("22");
        var d = _menus[$(this).attr('name')];
        Clearnav();
        addNav(d);
    });

    // 导航菜单绑定初始化
    $("#wnav").accordion({
        animate: false
    });
    var firstMenuName = $('#css3menu a:first').attr('name');
    addNav(_menus[firstMenuName]); //首次加载basic 左侧菜单
    InitLeftMenu();
}
function Clearnav() {
    var pp = $('#wnav').accordion('panels');

    $.each(pp, function (i, n) {
        if (n) {
            var t = n.panel('options').title;
            $('#wnav').accordion('remove', t);
        }
    });

    pp = $('#wnav').accordion('getSelected');
    if (pp) {
        var title = pp.panel('options').title;
        $('#wnav').accordion('remove', title);
    }
}

function GetMenuList(data, menulist) {
    if (data.menus == null)
        return menulist;
    else {
        menulist += '<ul>';
        $.each(data.menus, function (i, sm) {
            if (sm.url != null) {
                menulist += '<li ><a ref="' + sm.menuid + '" href="javascript:void(0)" _rel="'
					+ sm.url + '" ><span class="nav">' + sm.menuname
					+ '</span></a>';
            }
            else {
                menulist += '<li state="closed"><span class="nav">' + sm.menuname + '</span>'
            }
            menulist = GetMenuList(sm, menulist);
        })
        menulist += '</ul>';
    }
    return menulist;
}


//左侧导航加载
function addNav(data) {

    $.each(data, function (i, sm) {
        var menulist1 = "";
        //sm 常用菜单  邮件 列表

        menulist1 = GetMenuList(sm, menulist1);
        menulist1 = "<ul id='tt1' class='easyui-tree' animate='true' dnd='false' data-options='cascadeCheck:false,onSelect:onSelectNode'>" + menulist1.substring(4);
        $('#wnav').accordion('add', {
            title: sm.menuname,
            content: menulist1,
            iconCls: 'icon ' + sm.icon,
            onResize: function (w, h) {//自动适应菜单大小
                var $this = $(this);
                var $children = $this.children();
                var height = 0;
                var isVisible = false;
                if ($this.is(":visible")) {
                    isVisible = true;
                }
                if ($children.length == 0) {
                    //height=
                } else {
                    if (!isVisible) {
                        $this.show();
                    }
                    $children.each(function () {
                        height += $(this).height();
                    });
                    if (!isVisible) {
                        $this.hide();
                    }
                }
                if (height > (h - $this.prev().height()) || height == 0) {
                    $this.height("auto");
                    $this.parents("div.accordion:eq(0)").css("overflow-y", "auto");
                }
                //}
            }
        });

    });

    var pp = $('#wnav').accordion('panels');
    var t = pp[0].panel('options').title;
    $('#wnav').accordion('select', t);

}

//tree：选中节点时触发的方法
function onSelectNode(node) {
    var _str = node.text;
    var _node = $(_str);
    var _url = _node.attr("_rel");
    if (!_url) {
        return;
    }
    var _title = _node.find("span").text();

    var _menuid = _node.attr("ref");
    var _icon = getIcon(_menuid, _icon);

    gTitle = _title;
    var $tabs = $("#tabs");
    if (!$tabs.tabs("exists", _title)) {//不存在则添加
        $tabs.tabs("add", { title: _title, content: createFrame(_url), icon: _icon, closable: true });
    } else {//存在则选中
        $tabs.tabs("select", _title);
    }
    tabClose();
}

// 初始化左侧
function InitLeftMenu() {
    hoverMenuItem();

    /*$('#wnav li a').live('click', function() {
        var tabTitle = $(this).children('.nav').text();

        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");
        var icon = getIcon(menuid, icon);

        addTab(tabTitle, url, icon);
        $('#wnav li div').removeClass("selected");
        $(this).parent().addClass("selected");
    });*/
}

/**
* 菜单项鼠标Hover
*/
function hoverMenuItem() {
    $(".easyui-accordion").find('a').hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
}

// 获取左侧导航的图标Tab
function getIcon(menuid) {
    var icon = 'icon ';
    $.each(_menus, function (i, n) {
        $.each(n, function (j, o) {
            if (o.menus) {
                $.each(o.menus, function(k, m) {
                    if (m.menuid == menuid) {
                        icon += m.icon;
                        return false;
                    }
                });
            }
        });
    });
    return icon;
}

//刷新自定的tab
function updateTab(tab,url, subTitile) {
    if (!tab) {
        return;
    }
    $.extend($(tab).panel("options"), {
        content: createFrame(url)
    });
}

//添加tab，参数：标题，url，图标
function addTab(subtitle, url, icon, tabs) {
    var $tabs;
    var options = {
        title: subtitle,
        content: createFrame(url),
        closable: true,
        icon: icon
    };
    if (tabs) {
        $tabs = $(tabs);
    } else {
        $tabs = $('#tabs');
    }

    if (!$tabs.tabs('exists', subtitle)) {
        $tabs.tabs('add', options);
    } else {
        var $tab = $tabs.tabs("getTab", subtitle);
        $tabs.tabs("update", { tab: $tab, options: options });
        $tabs.tabs("select",subtitle);
    }
    tabClose();
}

//根据title获取iframe；参数：title；返回值：iframe或null；使用返回值前请判空
//使用范例1，获取子页面的某个元素：parent.getFrameByTitle("成品交仓单").$("#WarehouseShowTable").datagrid("reload")
//使用范例2，调用子页面的某个方法：parent.getFrameByTitle("成品交仓单").search_warehouse()
function getFrameByTitle(title,tabs) {
    //var args = $.extend([],["", "#tabs"], arguments);
    title = title ? title : null;
    tabs = tabs ? tabs : "#tabs";
    if (!title) {
        return null;
    }
    var $tab = $(tabs).tabs("getTab", title);
    if (!$tab) {
        return null;
    }
    var index = $(tabs).tabs("getTabIndex", $tab[0]);
    return window.frames[index];
}

//创建ifrmae是显示的遮罩
function showMaskAllOfFrame() {
    gMaskSource = maskSources.frame;
    $.messager.progress({ text: "获取中..." });
}

//datagrid获取数据时创建的遮罩
function showMaskAllOfDatagrid() {
    gMaskSource = maskSources.datagrid;
    $.messager.progress({ text: "获取中..." });
}

//关闭iframe产生的遮罩
function closeMaskAllOfFrame() {
    if (gMaskSource == maskSources.frame) {
        $.messager.progress("close");
    }
}

//关闭datagrid产生的遮罩
function closeMaskAllOfDatagrid() {
    if (gMaskSource == maskSources.datagrid) {
        $.messager.progress("close");
    }
}

//iframe的onload事件
function frameOnLoad(obj) {
    closeMaskAllOfFrame();//关闭遮罩
}

//创建iframe的字符串，参数：url
function createFrame(url) {
    //var strName = url;
    //var eReg = /\/[a-z]+\/[a-z]+/gim;
    //strName = strName.match(eReg);
    //strName = strName.toString().replace(/\//g, '');
    //var s = '<iframe frameborder="0" allowtransparency="true" src="' + url + '" style="width:100%;height:99%;overflow:auto;" name="' + strName + '" id="' + strName + '"></iframe>';
    //return s;
    return '<iframe frameborder="0" allowtransparency="true" src="' + url + '" style="width:100%;height:99%;overflow:auto;" onload="frameOnLoad(this)"></iframe>';
}

//关闭tab,参数:标题(为空时关闭当前窗口)
function closeTab(title) {
    var tabs = $("#tabs");
    var tab;
    if (title) {
        tab = title;
    } else {
        var _tab = tabs.tabs("getSelected");
        tab = tabs.tabs("getTabIndex", _tab);
    }
    tabs.tabs("close", tab);
}

function tabClose() {
    /* 双击关闭TAB选项卡 */
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    });
    /* 为选项卡绑定右键 */
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}

//刷新指定或当前tab,参数：标题
function refreshTab(title) {
    var currTab;
    if (title) {
        currTab = $('#tabs').tabs("getTab", title);
    } else {
        currTab = $('#tabs').tabs('getSelected');
    }
    $("#tabs").tabs("update", {tab:currTab,options: {} });
}

function updateTab(url,title,tab) {
    var $tabs = $("#tabs");
    var $tab=null;
    if (!tab) {
        $tab = $tabs.tabs("getSelected");
    } else {
        $tab = $(tab);
    }
     
    $tabs.tabs("update", {
        tab: $tab,
        options: {
            content: createFrame(url), title: title
        }
    });
}

// 绑定右键菜单事件
function tabCloseEven() {
    // 刷新
    $('#mm-tabupdate').click(function () {
        refreshTab();
    });
    // 关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    });
    // 全部关闭
    $('#mm-tabcloseall').click(function () {
        var tabs = $("#tabs");
        var tabTargets = tabs.tabs("tabs");
        for (var i = 0, len = tabTargets.length; i < len; i++) {
            tabs.tabs("close", 0);
        }
        //$.each(tabTargets, function (index, value) {//保留第一个“欢迎”
        //    tabs.tabs("close", 1);
        //});
    });
    // 关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    // 关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            // msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    // 关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    // 退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    });
}

// 弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}

// 本地时钟
function clockon() {
    var now = new Date();
    var year = now.getFullYear(); // getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10)
        month = "0" + month;
    if (date < 10)
        date = "0" + date;
    if (hour < 10)
        hour = "0" + hour;
    if (minu < 10)
        minu = "0" + minu;
    if (sec < 10)
        sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu
			+ ":" + sec + " " + week;

    $("#bgclock").html(time);

    var timer = setTimeout("clockon()", 200);
}

/************************************************************************Index页面********************************************************/

function SearchText() {
    var TextValue = document.getElementById('textSearch').value;
    var url = "";
    var title1 = "";
    if (TextValue == "" || TextValue == null) {
        alert("您输入的为空或不存在，请从新输入。");
        return;
    }
    else if (TextValue == "001" || TextValue == "1") {
        url = "UI/生产管理/生产计划/生产计划总表.html";
        title1 = "生产计划总表";
    }
    else if (TextValue == "002" || TextValue == "2") {
        url = "UI/生产管理/物料领用/物料领用单.html";
        title1 = "物料领用单";
    }

    var content = createFrame(url);
    var icon1 = getIcon("120");

    if (!$('#tabs').tabs('exists', title1)) {
        $('#tabs').tabs('add', {
            title: title1,
            content: content,
            icon: icon1,
            closable: true
        });
    } else {
        $('#tabs').tabs('select', title1);
    }

    tabClose();
}



//设置登录窗口
function openPwd() {
    $('#w').window({
        title: '修改密码',
        width: 300,
        modal: true,
        shadow: true,
        closed: true,
        height: 160,
        resizable: false
    });
}
//关闭登录窗口
function closePwd() {
    $('#w').window('close');
}



//修改密码
function serverLogin() {
    var $newpass = $('#txtNewPass');
    var $rePass = $('#txtRePass');

    if ($newpass.val() == '') {
        msgShow('系统提示', '请输入密码！', 'warning');
        return false;
    }
    if ($rePass.val() == '') {
        msgShow('系统提示', '请在一次输入密码！', 'warning');
        return false;
    }

    if ($newpass.val() != $rePass.val()) {
        msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
        return false;
    }

    $.post('/ajax/editpassword.ashx?newpass=' + $newpass.val(), function (msg) {
        msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
        $newpass.val('');
        $rePass.val('');
        close();
    })

}
$(function () {
    openPwd();

    $('#editpass').click(function () {
        $('#w').window('open');
    });

    $('#btnEp').click(function () {
        serverLogin();
    })

    $('#btnCancel').click(function () { closePwd(); })

    $('#loginOut').click(function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

            if (r) {
                //location.href = './登陆页面/登陆页面.html';
                location.href = "login.html";
            }
        });
    })
});
/*************************************************************************************************************************************/
/*查询输入框获取/失焦点的边框样式*/
function text_css(obj, objclassname) {
    document.getElementById(obj).className = objclassname;
}
/*========================================================================================*/
function cacheSearchData(tab) {
    var form = $(".Search_Form");

    if (form.length !=0) {
        $(tab).data({ "cache": $(".Search_Form").serializeArray() });
    }
}

function loadSearchData() {
    var tab = $('#tabs').tabs('getSelected').data("cache");
    
    if (typeof (tab) != "undefined") {
        var data = {};
        for (var i = 0;i<tab.length;i++) {
            data[tab[i]["name"]] = tab[i]["value"];
        }
        $(".Search_Form").form("load",data);
    }
    

}
/*========================================================================================*/
