//这是一个简单工厂模式
var XMLHttpFactory = function() {

};

//如果真的要调用这个方法会抛出一个错误， 它不能被实例化， 只能用来派生子类
XMLHttpFactory.prototype = {
    createFactory: function() {
        throw new Error('This is abstract class');
    }
};

//派生子类 

var XHRhandler = function() {
    XMLHttpFactory.call(this);
};
XHRhandler.prototype = new XMLHttpFactory();

//重新定义createFactory方法
XHRhandler.prototype.constructor = XHRhandler;

XHRhandler.prototype.createFactory = function() {
    var XMLHttp = null;
    if (window.XMLHttpRequest) {
        XMLHttp = new XMLHttpRequest();
    } else if (window.ActiveXObject) {
        XMLHttp = new ActiveXObject('Microsoft.XMLHTTP');
    }
    return XMLHttp;
};