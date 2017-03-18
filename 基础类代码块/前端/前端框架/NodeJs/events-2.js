ar events = require('events');

var restaurant = new events.EventEmitter();

/*
 EventEmitter.emit(event, [arg1], [arg2], [...])   触发指定事件
 参数1：event  字符串，事件名
 参数2：可选参数，按顺序传入回调函数的参数
 返回值：该事件是否有监听
 */
var i=1;
restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
    console.log('点菜第'+(i++)+'步');
});

restaurant.emit('order', '红烧肉', 1);
