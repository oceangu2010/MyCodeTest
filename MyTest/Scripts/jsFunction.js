﻿/*
将一段数字从右到左每隔三位插入一个逗号
*/ 

window.onload = function() {
    //整个测试由Alucelx提供 
    var testFun = function(callback, str) {
        var tipElem = document.createElement('div'),
            startTime,
            duration = 0;
        for (var j = 5; j > 0; j--) {
            startTime = +new Date();
            for (var i = 10000; i > 0; i--) {
                callback();
            }
            duration = ((+new Date()) - startTime) + duration;
        }
        duration = (duration / 5).toFixed(0);
        tipElem.innerHTML = str + '总耗时：' + duration + ' ms';
        document.body.appendChild(tipElem);
    };

    var str = '3345687687876789123';

    var cuter1 = function(str) { //带刀 
        var len = str.length,
            lastIndex,
            arr = [];
        while (len > 0) {
            lastIndex = len;
            len -= 3;
            arr.unshift(str.substring(len, lastIndex));
        }
        return arr.join(',');
    };


    var cuter2 = function(str) { //abcd 
        return str.replace( /\B(?=(?:\d{3})+$)/g , ',');
    };

    var cuter3 = function(str) { //前叔 
        return str.replace( /(.*)(\d{3})$/ , function() {
            if (arguments[1] && arguments[2]) {
                return arguments[1].replace( /(.*)(\d{3})$/ , arguments.callee) + "," + arguments[2];
            } else {
                return arguments[0];
            }
        });
    };

    var cuter4 = function(str) { //Alucelx 
        return str.split('').reverse().join('').replace( /(\d{3})/g , '$1,').split('').reverse().join('');
    };

    var cuter5 = function(str) { //司徒正美 
        var ret = [];
        while (str) {
            str = str.replace( /\d{1,3}$/g , function(a) {
                ret.unshift(a)
                return ""
            });
        }
        return ret.join(",");
    };
    var cuter6 = function(str) { //司徒正美 
        var n = str.length % 3;
        if (n) {
            return str.slice(0, n) + str.slice(n).replace( /(\d{3})/g , ',$1')
        } else {
            return str.replace( /(\d{3})/g , ',$1').slice(1)
        }
    };
    var cuter7 = function(str) { //司徒正美 
        var ret = ""
        for (var i = 0, n = str.length; i < n; i++) {
            ret += str.charAt(i)
            if (i % 3 === 0) {
                ret += ","
            }
        }
        return ret
    }

    var cuter8 = function(str) { //[[valueOf]] 
        var s2 = [].slice.call(str);
        for (var i = s2.length - 3; i > 0; i -= 3) {
            s2.splice(i, 0, ',');
        }
        return s2.join("")
    }
    var cuter9 = function(str) { //听说 
        var newStr = new Array(str.length + parseInt(str.length / 3));
        newStr[newStr.length - 1] = str[str.length - 1];
        var currentIndex = str.length - 1;
        for (var i = newStr.length - 1; i >= 0; i--) {
            if ((newStr.length - i) % 4 == 0) {
                newStr[i] = ",";
            } else {
                newStr[i] = str[currentIndex--];
            }
        }
        return newStr.join("")
    }
    var cuter10 = function(str) { //Rekey 
        var len = str.length, str2 = '', max = Math.floor(len / 3);
        for (var i = 0; i < max; i++) {
            var s = str.slice(len - 3, len);
            str = str.substr(0, len - 3);
            str2 = (',' + s) + str2;
            len = str.length;
        }
        str += str2;
        return str
    }
    //下面是性能测试 
    testFun(function() {
        cuter1(str);
    }, '方法一');

    testFun(function() {
        cuter2(str);
    }, '方法二');

    testFun(function() {
        cuter3(str);
    }, '方法三');

    testFun(function() {
        cuter4(str);
    }, '方法四');

    testFun(function() {
        cuter5(str);
    }, '方法五');
    testFun(function() {
        cuter6(str);
    }, '方法六');
    testFun(function() {
        cuter7(str);
    }, '方法七');
    testFun(function() {
        cuter8(str);
    }, '方法八');
    testFun(function() {
        cuter9(str);
    }, '方法九');
    testFun(function() {
        cuter10(str);
    }, '方法十');
}