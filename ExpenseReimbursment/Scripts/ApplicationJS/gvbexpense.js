

//render Employee login
$("#EmpLogin").click(function () {
    $.ajax({
        type: "GET",
        url: "/Account/Login",
        datatype: "html",
        success: function (data) {
            $("#AdminLogin").hide();
            $("#loginDiv").html(data);
        },
        error: function () {
            $("#loginDiv").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});

//render admin login
$("#AdminLogin").click(function () {
    $.ajax({
        type: "GET",
        url: "/Account/Login",
        datatype: "html",
        success: function (data) {
            $("#EmpLogin").hide();
            $("#loginDiv").html(data);
        },
        error: function () {
            $("#loginDiv").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});