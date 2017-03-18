var fs = require('fs');

fs.readFile('file.txt','UTF-8',function (err,data) {
  if (err) {
    console.log('read file err');
  }else {
    console.log(data);
  }
});

var data1 = fs.readFileSync('file.txt','UTF-8');
console.log('end');
console.log(data1);
