var fuhao = {

};
fuhao.huofang = function(params) {
    return '馒头';
};
fuhao.chuliangshi = function() {
    return '面粉';
};
fuhao.mantou = function() {
    this.chuliangshi();
    this.huofang();
};
//
fuhao.men = {
    return this.mantou();
};