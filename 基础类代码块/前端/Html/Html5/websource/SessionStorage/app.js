/**
 * Created by wangy on 2016/9/27.
 */

var num = 0;
var txt;
var btn;

window.onload = function () {
    txt = document.getElementById('txt');
    btn = document.getElementById('addbtn');
    if(sessionStorage.num){
        num = sessionStorage.num;
    }else {
        num = 0;
    }

    btn.onclick = function () {
        num++;
        sessionStorage.num = num;
        shownum();
    }
}

function shownum() {
    txt.innerHTML = num;
}
