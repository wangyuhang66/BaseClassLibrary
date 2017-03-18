//这是一个简单工厂模式
var XMLHttpFactory = function() {

};

//XMLHttpFactory.createXMLHttp()  根据当前环境的具体情况返回一个XHR对象
XMLHttpFactory.createXMLHttp = function() {
    var XMLHttp = null;
    if (window.XMLHttpRequest) {
        XMLHttp = new XMLHttpRequest();
    } else if (window.ActiveXObject) {
        XMLHttp = new ActiveXObject('Microsoft.XMLHTTP');
    }
    return XMLHttp;
};

var AjaxHander = function() {
    var XMLHttp = XMLHttpFactory.createXMLHttp();
    //具体操作
};