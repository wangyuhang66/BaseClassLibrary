//使用 JavaScript 和 Ajax 发出异步请求


//创建新的 XMLHttpRequest 对象
var requst = new XMLHttpRequest();
//创建 XMLHttpRequest 的 Java 伪代码
XMLHttpRequest requst = new XMLHttpRequest();

//创建具有错误处理能力的 XMLHttpRequest
var requst = false;
try {
    requst = new XMLHttpRequest();
} catch (failed) {
    requst = false;
}
if (!requst) {
    alert('ERROR initializing XMLHttpRequest!');
}

//增加对 Microsoft 浏览器的支持
var requst = false;
try {
    requst = new XMLHttpRequest();
} catch (error) {
    try {
        request = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (othermicrosoft) {
        try {
            request = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (failed) {
            request = false;
        }
    }
}
if (!requst) {
    alert("Error initializing XMLHttpRequest!");

}

//将 XMLHttpRequest 创建代码移动到方法中
var requst;

function createRequst() {
    try {
        requst = new XMLHttpRequest();
    } catch (error) {
        try {
            requst = new ActiveXObject('Msxml2.XMLHTTP');
        } catch (error) {
            try {
                request = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (failed) {
                request = false;
            }
        }
    }
    if (!request)
        alert("Error initializing XMLHttpRequest!");
}

//使用 XMLHttpRequest 的创建方法

var request;

function createRequest() {
    try {
        request = new XMLHttpRequest();
    } catch (trymicrosoft) {
        try {
            request = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (othermicrosoft) {
            try {
                request = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (failed) {
                request = false;
            }
        }
    }
    if (!request)
        alert("Error initializing XMLHttpRequest!");
}

function getCustomerInfo() {
    createRequest();
    // Do something with the request variable
}

//建立请求 URL
var request = false;
try {
    request = new XMLHttpRequest();
} catch (trymicrosoft) {
    try {
        request = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (othermicrosoft) {
        try {
            request = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (failed) {
            request = false;
        }
    }
}
if (!request)
    alert("Error initializing XMLHttpRequest!");

function getCustomerInfo() {
    var phone = document.getElementById("phone").value;
    var url = "/cgi-local/lookupCustomer.php?phone=" + escape(phone);
}

//Break Neck Pizza 表单
<
body >
    <
    p > < img src = "breakneck-logo_4c.gif"
alt = "Break Neck Pizza" / > < /p> <
form action = "POST" >
    <
    p > Enter your phone number:
    <
    input type = "text"
size = "14"
name = "phone"
id = "phone"
onChange = "getCustomerInfo();" / >
    <
    /p> <
p > Your order will be delivered to: < /p> <
div id = "address" > < /div> <
p > Type your order in here: < /p> <
p > < textarea name = "order"
rows = "6"
cols = "50"
id = "order" > < /textarea></p >
    <
    p > < input type = "submit"
value = "Order Pizza"
id = "submit" / > < /p> < /
form > <
    /body>

//XMLHttpRequest 对象的 open() 方法来完成。该方法有五个参数：
//request-type：发送请求的类型。典型的值是 GET 或 POST，但也可以发送 HEAD 请求。 
//url：要连接的 URL。 
//asynch：如果希望使用异步连接则为 true，否则为 false。该参数是可选的，默认为 true。 
//username：如果需要身份验证，则可以在此指定用户名。该可选参数没有默认值。 
//password：如果需要身份验证，则可以在此指定口令。该可选参数没有默认值

// 打开请求

function getCustomerInfo() {
    var phone = document.getElementById("phone").value;
    var url = "/cgi-local/lookupCustomer.php?phone=" + escape(phone);
    request.open("GET", url, true);
}
//发送请求
function getCustomerInfo() {
    var phone = document.getElementById("phone").value;
    var url = "/cgi-local/lookupCustomer.php?phone=" + escape(phone);
    request.open("GET", url, true);
    request.send(null);
}
//发送请求
function getCustomerInfo() {
    var phone = document.getElementById("phone").value;
    var url = "/cgi-local/lookupCustomer.php?phone=" + escape(phone);
    request.open("GET", url, true);
    request.send(null);
}
//回调方法的代码
var request = false;
try {
    request = new XMLHttpRequest();
} catch (trymicrosoft) {
    try {
        request = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (othermicrosoft) {
        try {
            request = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (failed) {
            request = false;
        }
    }
}
if (!request)
    alert("Error initializing XMLHttpRequest!");

function getCustomerInfo() {
    var phone = document.getElementById("phone").value;
    var url = "/cgi-local/lookupCustomer.php?phone=" + escape(phone);
    request.open("GET", url, true);
    request.onreadystatechange = updatePage;
    request.send(null);
}

function updatePage() {
    alert("Server is done!");
}
//检查就绪状态
function updatePage() {
    if (request.readyState == 4)
        alert("Server is done!");
}

// 检查 HTTP 状态码
function updatePage() {
    if (request.readyState == 4)
        if (request.status == 200)
            alert("Server is done!");
}

//增加一点错误检查
function updatePage() {
    if (request.readyState == 4)
        if (request.status == 200)
            alert("Server is done!");
        else if (request.status == 404)
        alert("Request URL does not exist");
    else
        alert("Error: status code is " + request.status);
}

//处理服务器响应
function updatePage() {
    if (request.readyState == 4) {
        if (request.status == 200) {
            var response = request.responseText.split("|");
            document.getElementById("order").value = response[0];
            document.getElementById("address").innerHTML =
                response[1].replace(/\n/g, "");
        } else
            alert("status is " + request.status);
    }
}