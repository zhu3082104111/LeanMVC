//日期格式化
Date.prototype.Format = function(formatStr) {
    var str = formatStr ? formatStr : "yyyy-MM-dd";
    var week = ['日', '一', '二', '三', '四', '五', '六'];
    var date = this;
    str = str.replace(/yyyy|YYYY/, date.getFullYear());
    str = str.replace(/yy|YY/, (date.getYear() % 100) > 9 ? (date.getYear() % 100).toString() : '0' + (date.getYear() % 100));

    str = str.replace(/MM/, date.getMonth() > 8 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1));
    str = str.replace(/M/g, date.getMonth() + 1);

    str = str.replace(/w|W/g, week[date.getDay()]);

    str = str.replace(/dd|DD/, date.getDate() > 9 ? date.getDate().toString() : '0' + date.getDate());
    str = str.replace(/d|D/g, date.getDate());

    str = str.replace(/hh|HH/, date.getHours() > 9 ? date.getHours().toString() : '0' + date.getHours());
    str = str.replace(/h|H/g, date.getHours());
    str = str.replace(/mm/, date.getMinutes() > 9 ? date.getMinutes().toString() : '0' + date.getMinutes());
    str = str.replace(/m/g, date.getMinutes());

    str = str.replace(/ss|SS/, date.getSeconds() > 9 ? date.getSeconds().toString() : '0' + date.getSeconds());
    str = str.replace(/s|S/g, date.getSeconds());

    return str;
};

//日期贝拷贝拷
Date.prototype.Clone = function() {
    return new Date(this.getTime());
};

//字符串解析为Date
Date.Parse = function (dateStr) {
    if (dateStr) {
        var date = dateStr.replace(/(-|\.)/g, '/');
        return new Date(date);
    }
    return "";
};

//得到某月的天数
Date.GetMonthDays = function(month) {
    var date = new Date();
    var month = arguments[0];
    var year = arguments[1] ? arguments[1] : date.getFullYear();
    var now = new Date(year, month - 1, 1);
    var next = new Date(year, month, 1);
    return (next - now).ToDay();
};

//添加天数
Date.prototype.AddDays = function (days) {
    this.setDate(this.getDate() + days);
    return this;
};

//添加月
Date.prototype.AddMonths = function (months) {
    this.setMonth(this.getMonth() + months);
    return this;
};

//得到当前日期所在周的周一
Date.prototype.GetFirstDayOfWeek = function () {
    var dayOfWeek = this.getDay();
    var delat = 0;
    if (dayOfWeek == 0) {
        delat = 7;
    }
    this.setDate(this.getDate() - this.getDay() + 1 - delat);
    return this;
};

//数字转为天数
Number.prototype.ToDay = function () {
    return (this) / 3600 / 1000 / 24;
};

//四舍五入为整数
Number.prototype.ToInt = function () {
    return Math.round(this);
};


Number.prototype.ToDate = function () {
    return this;
};

Number.prototype.Format = function () {
    return this.toString();
};

//转日期
String.prototype.ToDate = function () {
    var str = this + "";
    var a = /^\d{4}(-|\/|.)\d{1,2}(-|\/|.)\d{1,2}$/.test(str);
    if (!a) {
        return str;
    }
    return Date.Parse(str);
};


String.prototype.Format = function () {
    return this.toString();
};

//转int
String.prototype.ToInt = function () {
    return parseInt(this + "");
};

//转数字
String.prototype.ToNumber = function () {
    return this - 0;
};

String.prototype.ToJson = function () {
    return eval('(' + this + ')');
};

String.prototype.Eval = function () {
    return eval('(' + this + ')');
};

//最大、最小值(仅对日期有效),数据源格式[{},{}]
Array.prototype.GetRangeBy = function (field) {
    if (this.length == 0) {
        return null;
    }
    var _f = field;
    var min = this[0][_f].ToDate();
    var max = this[0][_f].ToDate();
    for (var i = 1, len = this.length; i < len; i++) {
        var temp = this[i][_f].ToDate();
        if (min > temp) {
            min = temp;
        }
        if (max < temp) {
            max = temp;
        }
    }
    return { min: min.Format("yyyy-MM-dd"), max: max.Format("yyyy-MM-dd") };
};

//按字段分组，返回已分组数据
Array.prototype.GroupBy = function (field) {
    var _array = [];
    var _field = field;
    var _hash = {};
    for (var i = 0, len = this.length; i < len; i++) {
        var _w = this[i][_field] + "";
        if (_hash[_w] ==1) {
            _array[_hash[_w]].push(this[i]);
        } else {
            var l = _array.length;
            _array[l] = [];
            _array[l].push(this[i]);
            _hash[_w] = l;
        }
    }
    return _array;
};

Array.prototype.max = function() { //最大值
    return Math.max.apply({}, this);
};

Array.prototype.min = function() { //最小值
    return Math.min.apply({}, this);
};

//对日期类型的字段进行排序，参数：field(字段)，rule("asc","desc")
Array.prototype.SortDateField = function(field, rule) {
    var args = $.extend([null, "asc"], arguments);
    if (args[0] == null) {
        return null;
    }
    var rule = args[1] == "asc" ? 1 : -1;
    this.sort(function(prev, next) {
        var prevDt = Date.Parse(prev[field]);
        var nextDt = Date.Parse(next[field]);
        return (prevDt - nextDt) * rule;
    });
    return this;
};

//数组筛选,lamda语句的形式
Array.prototype.Where = function (expression) {
    if (!expression) {
		return this;
	}
	var retArr = [];
	var str = "" + expression;
	var strArr = str.split("=>");
	var head = strArr[0];
	var body = strArr[1];
	var evalStr="";
	evalStr+=("for (var i=0,len=this.length;i<len;i++) {");
	evalStr+=("eval(head+'=this[i];');");
	evalStr+=("if (eval(body)) {");
	evalStr+=("retArr.push(this[i]);");
	evalStr+=("}}");
	eval(evalStr);
		
	return retArr;
};

//数组求和,lamda语句的形式
Array.prototype.Sum = function(expression) {
    var sum = 0;
    if (!expression) {
        for (var i = 0, len = this.length;i<len; i++) {
            if (!isNaN(this[i])) {
                sum += (this[i] - 0);
            } 
        }
        return sum;
    }
    var str = "" + expression;
    var strArr = str.split("=>");
    var head = strArr[0];
    var body = strArr[1];
    for (var i = 0, len = this.length;i<len; i++) {
        eval(head + "=this[i];");
        var t = eval(body);
        if (!isNaN(t)) {
            sum += (t-0);
        }
    }
    return sum;
};

Array.prototype.FindObject = function (key, value) {
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

Array.prototype.FindByObject = function (obj) {
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

Array.prototype.FindByObject = function(obj) {
    if (!obj) {
        return this;
    }
    var retArr = [];
    var keys = [];
    var values = [];
    for (key in obj) {
        keys.push(key);
        values.push(obj[key]);
    }
    var keyLen = keys.length;
    for (var i = 0, len = this.length; i < len; i++) {
        var result = true;
        var item = this[i];
        for (var j = 0; j < keyLen; j++) {
            if (item[keys[j]] != values[j]) {
                result = false;
                break;;
            }
        }
        if (result) {
            retArr.push(item);
        }
    }
    return retArr;
};

//去指定字段去重[{},{}](字段为空时对简单的数组["a","b"]去重)
Array.prototype.Distinct = function (keyName) {
    var hash = {};
    var ret = [];
    if (keyName) {
        for (var i = 0, len = this.length; i < len; i++) {
            var key = "k" + this[i][keyName];
            if (hash[key] !== 1) {
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
Array.prototype.Distinct = function (fields) {
    var hash = {};
    var ret = [];
    if (fields) {//按字段去重
        var fieldArr = fields.split(",");
        var str = "'k'";
        for (var i = 0, len = fieldArr.length; i < len; i++) {
            str += "+'-'+obj['" + fieldArr[i] + "']";
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

//获得指定范围内的随机数
Math.Random = function (min, max) {
    var delat = max - min;
    return Math.random() * delat + min;
};

//深度拷贝(抛开引用)
var deepClone = function(source) {
    if (source === null || source === undefined) {
        return null;
    }
    if (typeof(source) != 'object') {
        return source;
    }
    if (source.constructor == Array) { //构造函数
        var ret = [];
        for (var i = 0; i < source.length; i++) {
            ret[i] = arguments.callee(source[i]); //递归调用
        }
    } else {
        var ret = {};
        for (var d in source) {
            ret[d] = arguments.callee(source[d]);
        }
    }
    return ret;
};

//是否为空:"",0,null,undefined
var isNullOrEmpty = function (source) {
    var r = true;
    if (source) {
        r = false;
    }
    return r;
};

//是否为undefined
var isUndefined = function (source) {
    return (typeof source == "undefined");
};

