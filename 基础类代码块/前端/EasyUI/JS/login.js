$(document).ready(function() {
    LoginMng.InitPageEvent();
});

var LoginMng = {

    InitPageEvent: function() {
        $(".email").val($.cookie("username"));
        $(".login").on("keydown", function(e) {
            var key = window.event ? e.keyCode : e.which;
            if (key == 13)
                LoginMng.LoginEx();
        });
        $(".login input[data-act=login]").on("click", function() {
            LoginMng.LoginEx();
        });
    },
    LoginEx: function() {
        $.ajax({
            type: "POST",
            url: "/Home/Logins",
            data: "{ 'email': '" + $(".email").val() + "','pwd': '" + $(".pwd").val() + "'}",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function(result) {
                if (result) {
                    $.cookie("username", $(".email").val(), {
                        expires: 7
                    });
                    window.location = "/Home/Index";
                } else
                    $(".login .prompt").html("登录失败 , 请检查用户名或者密码是否正确 !");
            },
            error: function(ex) {
                $(".login .prompt").html("登录失败 , 请检查用户名或者密码是否正确 !");
            }
        });
    },
}