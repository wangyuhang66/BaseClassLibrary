var fs = require("fs");

fs.readFile('package.json', function (err, data) {
    if (err)
        return console.error(err);
    console.log('仆人:国王，我的任务完成了。这是结果\n'+data.toString());
});

console.log("国王：任务交待完了，我去睡觉啦!");
