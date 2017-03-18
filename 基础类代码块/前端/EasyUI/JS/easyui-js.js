$(document).ready(function() {
    ObjectMng.InitPageEvent();
});

var ObjectMng = {

    InitPageEvent: function() {
        $("#main .frame-top a[data-act=search]").on("click", function() {

        });

        $("# .actionbar-full a[data-act=save]").on("click", function() {

        });

        $("# .actionbar-full a[data-act=cancel]").on("click", function() {

        });
    },

    //隐藏弹出框
    InitPageCtrl: function() {
        $("#").dialog({
            modal: true,
            closed: true
        });
    },

    //加载主数据
    InitPageData: function() {
        $("#").datagrid({
            url: "",
            // queryParams: { starttime: $( "#startTime" ).datebox("getValue"), endtime: $( "#endTime" ).datebox("getValue")}
            queryParams: {},
            title: "",
            width: 970,
            height: 740,
            loadMsg: "加载中....",
            fitColumns: false,
            rownumbers: true,
            singleSelect: true,
            idField: "Id",
            columns: [
                [{
                    field: "Id",
                    title: "ID",
                    width: 80,
                    align: "center"
                }, {
                    field: "",
                    title: "",
                    width: 80,
                    align: "center"
                }, {
                    field: "",
                    title: "",
                    width: 100,
                    align: "center"
                }, {
                    field: "",
                    title: "",
                    width: 100,
                    align: "center"
                }, {
                    field: "",
                    title: "",
                    width: 100,
                    align: "center"
                }, {
                    field: "Op",
                    title: "操作",
                    width: 160,
                    align: "center",
                    formatter: function(value, rec, rowIdx) {
                        return "<a href='javascript:WxReplyMmg.Edit(" + rowIdx + ")' >编辑</a>" +
                            "<a href='javascript:WxReplyMmg.Delete(" + rowIdx + ")'>删除</a>";
                    }
                }, ]
            ],

            //
            onLoadSuccess: function(data) {
                if (data) {
                    $.each(data.rows, function(index, item) {
                        if (item.IsHot) {
                            $('#hottaglist').datagrid('checkRow', index);
                        }
                    });
                }
            },

            toolbar: [{
                text: "添加",
                iconCls: "icon-add",
                handler: function() {

                }
            }]
        });
    },

    //增加
    AppendPageData: function() {
        $("# input[name=]").val("");
        $("# input[name=]").val("");
        $("# input[name=]").val("");
        $("# textarea[name=]").val("");
        $("#").dialog({
            title: "添加信息"
        });
        $("#").dialog("open");
    },

    //修改
    EditPageData: function() {
        TryAutoFillControl($("#").datagrid("getSelected"), "");
        $("#").dialog({
            title: "编辑信息"
        });
        $("#").dialog("open");
    },

    //提交数据
    SaveData: function() {
        var dataCarrier = new Object();
        dataCarrier.lecture = SerializeFormObjs($("# :input").serializeArray());

        var postUrl;
        if (dataCarrier.lecture.Id == "")
            postUrl = "";
        else postUrl = "";

        $.ajax({
            type: "POST",
            url: postUrl,
            data: JSON.stringify(dataCarrier),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function(result) {
                //成功的话 关闭弹框 刷新数据
                $("#").datagrid("reload");
                $("#").dialog("close");
                $.messager.alert("提示", result, "info");


                //拉取数据给select赋值
                for (var i = 0; i < result.length; i++) {
                    $("# select[name=CategoryLv2]").append($("<option>").text(result[i].CategoryName).val(result[i].Id));
                }
            },


            error: function(ex) {
                $("#").dialog("close");
                $.messager.alert('提示', "操作失败", "error");
            }
        });
    },

    //删除
    Delete: function() {

        var dataCarrier = new Object();
        dataCarrier.tag = SerializeFormObjs($("# :input").serializeArray());

        $.messager.confirm("提示", "你确定删除此条记录吗？", function(r) {
            if (r) {
                $.ajax({
                    type: "POST",
                    url: "",
                    data: JSON.stringify(dataCarrier),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function(result) {
                        $("#").datagrid("deleteRow", index);
                        $.messager.alert("提示", result, "info");
                    },
                    error: function(ex) {
                        $.messager.alert('提示', "操作失败", "error");
                    }
                });
            }
        });
    },

    //取消
    CancelEdit: function() {
        $("#").dialog("close");
    }
};



//取时间
starttime: $("#startTime").datebox("getValue");
endtime: $("#endTime").datebox("getValue")

//精度问题
parseFloat();
toFixed(1);

//age++先执行运算符+然后在自身加1，++age先自身加1然后再通过+运算符加1。

//datebox赋值：
var startData = FormatJSONDateToDate($("#courselist").datagrid("getRows")[rowIdx].StartDate);
$("#StartDate").datebox("setValue", myformatter(startData));

function myformatter(date) {
    var y = date.split('-')[0];
    var m = date.split('-')[1];
    var d = date.split('-')[2];
    return m + '/' + d + '/' + y;
}