var events = require('events');

//开一家餐馆
var restaurant = new events.EventEmitter();

/*
 EventEmitter.emit(event, [arg1], [arg2], [...])   触发指定事件
 参数1：event  字符串，事件名
 参数2：可选参数，按顺序传入回调函数的参数
 返回值：该事件是否有监听
 */
//定义点菜事件
restaurant.on('order', function(name, num) {
    console.log(num + "号桌，点菜：" + name );
});
//定义埋单事件
restaurant.on('pay', function(num) {
    console.log(num + "号桌, 埋单");
});
//1号桌，点菜
restaurant.emit('order', '红烧肉', 1);
//2号桌，点菜
restaurant.emit('order', '炒青菜', 2);
//3号桌，点菜
restaurant.emit('order', '土豆丝', 3);
//1号桌，埋单
restaurant.emit('pay', 1);
//2号桌，埋单
restaurant.emit('pay', 2);
//1号桌，埋单
restaurant.emit('pay', 3);
