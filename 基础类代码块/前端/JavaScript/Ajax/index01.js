//Ajax 在js得核心

//Ajax 简介

//Microsoft 浏览器上创建 XMLHttpRequest 对象
var xmlHttp = false;

try {
    xmlHttp = new ActiveXObject('Msxml2.XMLHTTP');
} catch (error) {
    try {
        xmlHttp = new ActiveXObject('Microsoft.XMLHTTP');
    } catch (error1) {
        xmlHttp = false;
    }
}


//支持多种浏览器的方式创建 XMLHttpRequest 对象
/* Create a new XMLHttpRequest object to talk to the Web server */
var xmlHttp = false;
/*@cc_on @*/
/*@if (@_jscript_version >= 5)
try {
  xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
} catch (e) {
  try {
    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
  } catch (e2) {
    xmlHttp = false;
  }
}
@end @*/
if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
    xmlHttp = new XMLHttpRequest();
}

//发出 Ajax 请求
function callServer() {
    // Get the city and state from the web form
    var city = document.getElementById("city").value;
    var state = document.getElementById("state").value;
    // Only go on if there are values for both fields
    if ((city == null) || (city == "")) return;
    if ((state == null) || (state == "")) return;
    // Build the URL to connect to
    var url = "/scripts/getZipCode.php?city=" + escape(city) + "&state=" + escape(state);
    // Open a connection to the server
    xmlHttp.open("GET", url, true);
    // Setup a function for the server to run when it's done
    xmlHttp.onreadystatechange = updatePage;
    // Send the request
    xmlHttp.send(null);
}

//处理服务器响应
function updatePage() {
    if (xmlHttp.readyState == 4) {
        var response = xmlHttp.responseText;
        document.getElementById("zipCode").value = response;
    }
}
//启动一个 Ajax 过程
<form> <p >
    City: < input type="text" name="city" id="city" size="25" onChange="callServer();" /> </p> <p >
    State: < input type="text" name="state" id="state" size="25" onChange="callServer();" /> </p> <p >
    Zip Code: <input type="text" name="zipCode" id="city" size="5" />
</p> </form >