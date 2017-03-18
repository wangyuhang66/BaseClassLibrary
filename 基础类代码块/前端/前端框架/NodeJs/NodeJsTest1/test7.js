//HTTP

//引入模块
var http = require('http');

//创建服务
http.createServer(function(req, res) {
    console.log(req.url);
    //响应头
    res.writeHead(200, {
        'Content-Type': 'text/html'
    });

    //相应内容
    res.write('<h1>NodeJS</h1>');

    //响应结束
    res.end('<p>End</p>');

    //监听端口
}).listen(3000);

console.log("Http server is listenging at port 3000.");