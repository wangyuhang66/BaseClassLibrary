/**
 * Created by wangy on 2016/9/27.
 */
var ta;
var btn;
 window.onload = function () {
     ta = document.getElementById('ta');
     if(localStorage.text){
        ta.value = localStorage.text;
     }

     btn = document.getElementById('btn');


     btn.onclick = function () {
         alert(ta.value);
         localStorage.text = ta.value;
     }
 }