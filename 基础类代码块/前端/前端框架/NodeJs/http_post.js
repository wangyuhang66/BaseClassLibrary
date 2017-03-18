//调用http模块
var http = require('http');

var server = http.createServer();
server.on('request', function(request, response) {
    if(request.method == 'POST') {
        var data_post = '';
        request.on('data', function(data){
            data_post += data;
        });
        request.on('end', function(){
            //服务端打印参数
            console.log('Get body:'+data_post);
            // 发送 HTTP 头部
            // HTTP 状态值: 200 : OK
            // 内容类型: text/plain
            response.writeHead(200, {'Content-Type': 'text/plain'});

            // 把请求参数返回给客户端
            response.end(data_post+'\n');
        })

    }
    else{
        response.writeHead(404, {'Content-Type': 'text/plain'});
        response.end('Not found !\n');
    }
}).listen(8000);

console.log('Http server is started.');
