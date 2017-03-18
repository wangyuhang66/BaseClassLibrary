//声明事件对象
var EventEmitter = require('events').EventEmitter;
var event = new EventEmitter();

//注册事件
event.on('some_event', function() {
    console.log('妹纸，我可以撩你吗？');
    console.log('A：可以！！！');
    console.log('B：同A！');
    console.log('C：同B！');
});

//触发事件
setTimeout(function() {
    event.emit('some_event');
}, 1000);