/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：StoreFunction.js
// 文件功能描述：
//            仓库公共js
//      
// 修改履历：2013/12/12 杨灿 新建
/*****************************************************************************/

//单价字段，保留小数点后六位（yc添加）
function prchsUpToDecimal_yc(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        alert('error');
        return false;
    }
    var f_x = Math.round(x * 1000000) / 1000000;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 6) {
        s_x += '0';
    }
    return s_x;
}

//数量、金额字段，自右边开始每隔三个字符添加一个逗号（yc添加）
function QtyToMoney_yc(x) {
    var str = x.toString();
    str = str.replace(/(\d{1,3})(?=(?:\d{3})+(?!\d))/g, '$1,');
    return str;
}