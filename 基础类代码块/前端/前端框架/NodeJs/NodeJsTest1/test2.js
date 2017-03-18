var http =require('http');
http.createServer(function (rep,res) {
  res.writeHead(200,{'Content-Type':'text/html'});
  res.write('<h1>nodejs,我们有服务了</h1>');
  res.end('<p>视频出处： PCAT</p>');
}).listen(5858);
