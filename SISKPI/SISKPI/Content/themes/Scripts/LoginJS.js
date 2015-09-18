/* 检查浏览器是否支持 */
var isIE = !!window.ActiveXObject;
var isIE6 = isIE && !window.XMLHttpRequest;
if (isIE6) {
    window.location.href = '@Url.Action("Page500", "Error", new { Area = "Common" })';
}
//回车键
document.onkeydown = function (e) {
    if (!e) e = window.event; //火狐中是 window.event
    if ((e.keyCode || e.which) == 13) {
        var obtnSearch = document.getElementById("Log_Submit")
        obtnSearch.focus(); //让另一个控件获得焦点就等于让文本输入框失去焦点
        obtnSearch.click();
    }
}
//初始化
$(function () {
    $("#Code").bind('keyup', function () {
        if ($("#Code").val().length == 4) {
            return CheckUserDataValid();
        }
    });

    //登录帐号获得/失去焦点时，提示的处理
    $("#Account")
        .focus(function () {
            if ($("#Account").val() == '') {
                $("#lblAccount").text('');
            }
        })
        .blur(function () {
            if ($("#Account").val() == '') {
                $("#lblAccount").text('输入登录账户');
            }
        });
    //登录密码获得/失去焦点时，提示的处理
    $("#Pwd")
        .focus(function () {
            if ($("#Pwd").val() == '') {
                $("#lblPwd").text('');
            }
        })
        .blur(function () {
            if ($("#Pwd").val() == '') {
                $("#lblPwd").text('输入登录密码');
            }
        });
    //验证码获得/失去焦点时，提示的处理
    $("#Code")
        .focus(function () {
            if ($("#Code").val() == '') {
                $("#lblCode").text('');
            }
        })
        .blur(function () {
            if ($("#Code").val() == '') {
                $("#lblCode").text('验证码');
            }
        });

    //登录帐号按键时，提示的处理
    $("#Account")
        .bind('keyup', function (e) {
            e = window.event || e;
            if ($("#Account").val() != '') {
                $("#lblAccount").text('');
            } else {
                $("#lblAccount").text('输入登录账户');
            }
        });
    //登录密码按键时，提示的处理
    $("#Pwd")
        .bind('keyup', function (e) {
            e = window.event || e;
            if ($("#Pwd").val() != '') {
                $("#lblPwd").text('');
            } else {
                $("#lblPwd").text('输入登录密码');
            }
        });
    //验证码按键时，提示的处理
    $("#Code")
        .bind('keyup', function (e) {
            e = window.event || e;
            if ($("#Code").val() != '') {
                $("#lblCode").text('');
            } else {
                $("#lblCode").text('验证码');
            }
        });
});
//有验证码登录
function LoginBtnCode() {
    var Account = $("#Account").val();
    var Pwd = $("#Pwd").val();
    var code = $("#Code").val();
    if (Account == "") {
        $("#Account").focus();
        showTopMsg("登录账户不能为空", 4000, 'error');
        return false;
    } else if (Pwd == "") {
        $("#Pwd").focus();
        showTopMsg("登录密码不能为空", 4000, 'error');
        return false;
    } else if (code == "") {
        $("#Code").focus();
        showTopMsg("验证码不能为空", 4000, 'error');
        return false;
    } else if (code.length != 4) {
        $("#Code").focus();
        showTopMsg("验证码必须为4位", 4000, 'error');
        return false;
    } else {
        return true;
    }
}
//无验证码登录
function LoginBtn() {
    var Account = $("#Account").val();
    var Pwd = $("#Pwd").val();
    if (Account == "") {
        $("#Account").focus();
        showTopMsg("登录账户不能为空", 4000, 'error');
        return false;
    } else if (Pwd == "") {
        $("#Pwd").focus();
        showTopMsg("登录密码不能为空", 4000, 'error');
        return false;
    }else {
        return true;
    }
}

/**
数据验证完整性，有验证码
**/
function CheckUserDataValidCode() {
    if (!LoginBtn()) {
        return false;
    }
    else {
        CheckingLogin(1);
        var Account = $("#Account").val();
        var Pwd = $("#Pwd").val();
        var code = $("#Code").val();
        var parm = 'action=login&Account=' + escape(Account) + '&Pwd=' + escape(Pwd) + '&code=' + escape(code);
        getAjax('LoginSession', parm, function (rs) {
            if (parseInt(rs) == 1) {
                $("#Code").focus();
                showTopMsg("验证码输入不正确", 4000, 'error');
                $("#Code").val("");
                ToggleCode("Verify_codeImag", 'VerifyCode.ashx');
                CheckingLogin(0);
            } else if (parseInt(rs) == 2) {
                $("#Account").focus();
                showTopMsg("登录账户被停用", 4000, 'error');
                CheckingLogin(0);
            } else if (parseInt(rs) == 4) {
                $("#Account").focus();
                showTopMsg("账户或密码有错误", 4000, 'error');
                resetInput();
                CheckingLogin(0);
            } else if (parseInt(rs) == 6) {
                $("#Account").focus();
                showTopMsg("该用户已经登录", 4000, 'error');
                CheckingLogin(0);
            } else if (parseInt(rs) == 3) {
                setInterval(Load, 1000);
            } else if (parseInt(rs) == 7) {
                $("#Account").focus();
                showTopMsg("内部错误,登录失败", 4000, 'error');
                CheckingLogin(0);
            } else {
                CheckingLogin(0);
                alert(rs);
                window.location.href = window.location.href.replace('#', '');
            }
        });
    }
}


/**
数据验证完整性，无验证码
**/
function CheckUserDataValid(path,loadpath) {
    if (!LoginBtn()) {
        return false;
    }
    else {
        CheckingLogin(1);
        var Account = $("#Account").val();
        var Pwd = $("#Pwd").val();
        var parm = 'action=login&Account=' + escape(Account) + '&Pwd=' + escape(Pwd);
        getAjax(path, parm, function (rs) {
            if (parseInt(rs) == 2) {
                $("#Account").focus();
                showTopMsg("登录账户被停用", 4000, 'error');
                CheckingLogin(0);
            } else if (parseInt(rs) == 4) {
                $("#Account").focus();
                showTopMsg("账户或密码有错误", 4000, 'error');
                resetInput();
                CheckingLogin(0);
            } else if (parseInt(rs) == 6) {
                $("#Account").focus();
                showTopMsg("该用户已经登录", 4000, 'error');
                CheckingLogin(0);
            } else if (parseInt(rs) == 3) {
                //setInterval(Load, 1000);
                Load(loadpath);
            } else if (parseInt(rs) == 7) {
                $("#Account").focus();
                showTopMsg("内部错误,登录失败", 4000, 'error');
                CheckingLogin(0);
            } else {
                CheckingLogin(0);
                alert(rs);
                window.location.href = window.location.href.replace('#', '');
            }
        });
    }
}


      

//登陆加载
function Load(path) {
    location.href = path;
    return false;
}
//清空
function resetInput() {
    $("#Account").focus(); //默认焦点
    $("#Account").val("");
    $("#Pwd").val("");
    $("#Code").val("");
}
function CheckingLogin(id) {
    if (id == 1) {
        $("#Log_Submit").attr("disabled", "disabled")
        $("#Log_Submit").attr("class", "signload");
        $(".load").show();
    } else {
        $("#Log_Submit").removeAttr('disabled');
        $("#Log_Submit").attr("class", "sign");
        $(".load").hide();
    }
}