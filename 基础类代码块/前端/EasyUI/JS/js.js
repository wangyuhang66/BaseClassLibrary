//当一个内部函数被其外部函数之外的变量引用时，就形成了一个闭包。
function a() {
    function b() {
        alert("hello word!!!");
    }
    return b;
}
var c = a();
c(); //执行完出什么效果？？？

$('.pitchOn').on('click', function() {
    if ($('.viewOperate', this).is(":hidden")) {
        $('.viewOperate', this).show();

    } else {
        $('.viewOperate', this).hide();
    }
});

$('').on('click', function(event) { //点击区域时
    if ($('', this).is(":hidden")) { //判断该区域的按钮是否显示
        $('.', this).show(); // 否  显示

    } else {
        $('.', this).hide(); // 是   隐藏
    }

    $(body).not($(this)).hide()； //隐藏不是该区域的按钮


    if ($().hasClass()) { //判断该区域的显示黄条
        // 已经显示了，不用管， 未显示就让他显示
    }
    $(body).not($(this)).removeClass() //清除不是该区域的显示【黄条】

})

$('#published').on("click", function() {
    var name = $('.content').val().trim();
    if (name.length <= 0) {
        alert("请输入要发布的内容！！！");
        $('#published').attr("disabled", "disabled");
    } else {
        _this._Create();
    }
    $('#published').removeAttr("disabled");
});

$('#published').attr("disabled", "disabled");
$('.content').on(fous, function() {
    $('#published').removeAttr("disabled");
    var name = $('.content').val().trim();
    if (name.length > 0) {
        $('#published').on("click", function() {
            _this._Create();
        })
    } else {
        $('#published').attr("disabled", "disabled");
    }
});

$("#questexp .userexp textarea").on("keyup", function() {
    $("#questexp button[data-act=save]").show();
    if ($(this).val() == "")
        $("#questexp button[data-act=save]").prop("disabled", true);
    else $("#questexp button[data-act=save]").prop("disabled", false);
})

$(".content").on("keyup", function() {
    $("#questexp button[data-act=save]").show();
    if ($(this).val() == "")
        $("#questexp button[data-act=save]").prop("disabled", true);
    else $("#questexp button[data-act=save]").prop("disabled", false);
})


var name = document.getElementById('文本框ID').value;
url = "www.baidu.com?name =" + name;











< script >
    $(document).ready(function() {
        //获取验证码
        var nums = "";
        for (i = 0; i < 4; i++) {
            nums += Math.round(Math.random() * 8 + 1);
        }
        $("#num").html(nums);
        $("#btn").click(function() {
            var dataCarrier = new Object();
            dataCarrier.yanzheng = $("#txt").val().trim();
            dataCarrier.user = $("#t1").val().trim();
            dataCarrier.pwd = $("#t2").val().trim();

            if (nums == dataCarrier.yanzheng) {
                $.ajax({
                    type: "get",
                    url: "Handler1.ashx/ProcessRequest",
                    data: JSON.stringify(dataCarrier),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function(result) {
                        if (result == "登录成功") {
                            $("#div1").hide();
                            $("#div2").hide();
                        }
                    },
                    error: function(err) {
                        window.close();
                        alert(err);
                    }
                });
            }
        });
    });

public string ProcessRequest(string user, string pwd) {
    Model.Login loginInfo = new Model.Login();
    loginInfo.name = user;
    loginInfo.pwd = pwd;

    LoginBLL lgbll = new LoginBLL();
    DataSet ds = lg.select(loginInfo); //这对吗？  你这个方法是传一个对象进去？？？

    if (ds.Tables[0].Rows.Count > 0) {
        //context.Response.Write("登录成功");
        return "登录成功"
    } else {
        //context.Response.Write("登录失败");
        return "登录失败"
    }
}

public bool IsReusable {
    get {
        return false;
    }
}





InitPageEvent: function() {
    var texttexttxtobj = {
        textdivName: "contentarea", //外层容器的class
        textareaName: "textName", //textarea的class
        textnumName: "textnum", //数字的class
        textnum: 10000, //数字的最大数目
    }

    //定义变量
    var $onthis; //指向当前
    var $textdivName = texttexttxtobj.textdivName; //外层容器的class
    var $textareaName = texttexttxtobj.textareaName; //textarea的class
    var $textnumName = texttexttxtobj.textnumName; //数字的class
    var $textnum = texttexttxtobj.textnum; //数字的最大数目
    //判断是不是中文
    function isChinese(str) {
        var reCh = /[u00-uff]/;
        return !reCh.test(str);
    }

    function numChange() {
        //初始定义长度为0
        var strlen = 0;
        var txtval = $.trim($onthis.val());
        for (var i = 0; i < txtval.length; i++) {
            if (isChinese(txtval.charAt(i)) == true) {
                //中文为2个字符
                strlen = strlen + 2;
            } else {
                //英文一个字符
                strlen = strlen + 1;
            }
        }
        //中英文相加除2取整数
        strlen = Math.ceil(strlen / 2);
        if ($textnum - strlen < 0) {
            //超出的样式
            $par.html("超出 <b style='color:red;font-weight:lighter' class=" + $textnumName + ">" + Math.abs($textnum - strlen) + "</b> 字");
        } else {
            //正常时候
            $par.html("还可以输入 <b class=" + $textnumName + ">" + ($textnum - strlen) + "</b> 字");
        }
        $b.html($textnum - strlen);
    }
    $("." + $textareaName).on("focus", function() {
        //获取当前的数字
        $b = $(this).parents("." + $textdivName).find("." + $textnumName);
        $par = $b.parent();
        //获取当前的textarea
        $onthis = $(this);
        var setNum = setInterval(numChange, 500);
    });

},