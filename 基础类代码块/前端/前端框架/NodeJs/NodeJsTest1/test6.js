//读取文件

var fs = require('fs');

//调用读取文件得方法
fs.readFile('context.txt', function(err, data) {
    if (err) {
        console.log(err);
    } else {
        console.log(data);
    }
});

fs.readFile('context.txt', 'UTF-8', function(err, data) {
    if (err) {
        console.log(err);
    } else {
        console.log(data);
    }
});


try {
    var data = fs.readFileSync('content.txt', 'UTF-8');
    console.log(data + '同步读取');
} catch (error) {
    console.log(error);
}