var fs = require("fs");

var data = fs.readFileSync('package.json');

console.log('仆人:国王，我完成任务了。这是结果\n'+data.toString());
console.log("国王：哎，我终于可以去睡觉了!");
