var dataCarrier = new Object();
dataCarrier.name = '';
dataCarrier.age  = ;

$.ajax({
    type: "POST",
    url: postUrl,
    data: JSON.stringify(dataCarrier),
    dataType: "json",
    contentType: "application/json;charset=utf-8",
    success: function(result) {
        //easyui 弹出
        $.messager.alert("提示", result, "info");

        alert(" result");
    },
    error: function(ex) {
        //easyui 弹出
        $.messager.alert('提示', ex, "error");

        alert(ex);
    }
});



//转换时间
function formatNumToDate(value){
    var da=new Date(parseInt(value.replace('/Date(','').replace(')/','').split('+')[0]));
    return da;
}

formatNumToDate('/Date(1467302400000)/');

//正则转换时间
'/Date(1467302400000)/'.match(/\/Date\((\d+)\)\//)
