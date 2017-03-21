/// <reference path="../DataTables/datatables.js" />


//render Employee login
$("#EmpLogin").click(function () {
    $.ajax({
        type: "GET",
        url: "/Account/Login",
        datatype: "html",
        success: function (data) {
            $("#adminDiv").hide();
            $("#empLoginDiv").html(data);
        },
        error: function () {
            $("#empLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>')
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
            $("#empDiv").hide();
            $("#admLoginDiv").html(data);
        },
        error: function () {
            $("#admLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
});

//get emplist
$("#empList").click(function () {
    $.ajax({
        type: "GET",
        url: "/Admin/EmployeeList",
        datatype: "html",
        success: function (data) {
            $("#adminBody").html(data);
            $("#empListTable").DataTable();
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
});

$("#btnEdit").click(function () {
    $("#lblExpenseAmt").hide();
    $("#txtExpenseAmt").show();
    $("#lblComments").hide();
    $("#txtComments").show();
    $("#beforeEdit").hide();
    $("#afterEdit").show();
});

$("#btnApprove").click(function () {
    $("#lblApprovedAmt").hide();
    $("#txtApprovedAmt").show();
    $("#lblComments").hide();
    $("#txtComments").show();
    $("#beforeEdit").hide();
    $("#afterEdit").show();
});

//get reset password
$("#resetPword").click(function (sender) {
    sender.preventDefault();
    $.ajax({
        type: "GET",
        url: "/Account/ResetPassword",
        datatype: "html",
        success: function (data) {
            $("#adminBody").html(data);
            $("#employeeBody").html(data);
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
});

// grt forgot password

function ForgotPassword() {
    $.ajax({
        type: "GET",
        url: "/Account/ForgotPassword",
        datatype: "html",
        success: function (data) {
            if ($("#empDiv").css("display") === "none") {
                $("#admLoginDiv").html(data);
            }
            else {
                $("#empLoginDiv").html(data);
            }            
        },
        error: function (data) {
            if ($("#empDiv").css("display") === "none") {
                $("#admLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }
            else {
                $("#empLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }            
        }
    });
}

function ForgotPasswordPost()
{
    var token = $("#formForgotPwrd input").val();
    var pwrdJson = {
        __RequestVerificationToken: token,
        EmpId: $("#EmpId").val(),
        Email: $("#Email").val()
    }
    $.ajax({
        type: "post",
        url: "/Account/UserValidate",
        data: pwrdJson,
        datatype: "text",
        success: function (data) {
            alert(data.msg)
            if (data.success) {
                $("#formForgotPwrd").attr('validated', true);
                $("#formForgotPwrd").submit();
            }
            else {
                $("#errorMsg").html('<p class="text-danger">' + data.responseText + '<p>');
            }
        },
        error: function (data) {
            if ($("#empDiv").css("display") === "none") {
                $("#admLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }
            else {
                $("#empLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }
        }
    });
}

//get employee reports
function getReportList(Id) {
    var empJson = { empId: Id }
    $.ajax({
        type: "GET",
        url: "/Employee/GetReportList",
        data: empJson,
        datatype: "html",
        success: function (data) {
            $("#employeeBody").html(data);
            $("#reportTable").DataTable();
        },
        error: function () {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//get report list approver
function getReportListApproval(empRole) {
    var empJson = { approverRole: empRole }
    $.ajax({
        type: "GET",
        url: "/Employee/GetReportListApproval",
        data: JSON.stringify(empJson),
        datatype: "html",
        success: function (data) {
            $("#employeeBody").html(data);
            $("#reportTable").DataTable();
        },
        error: function () {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//get report details
function getReportDetails(Id) {
    var rptId = { reportId: Id }
    $.ajax({
        type: "GET",
        url: "/Employee/GetReportDetails",
        data: rptId,
        datatype: "html",
        success: function (data) {
            $("#employeeBody").html(data);
            $("#afterEdit").hide();
            $("#txtExpenseAmt").hide();
            $("#txtApprovedAmt").hide();
            $("#txtComments").hide();
            
        },
        error: function () {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

function EditReport(isOwn) {
    var ownRpt = isOwn;
    $("#beforeEdit").hide();
    $("#afterEdit").show();
    $("#lblComments").hide();
    $("#txtComments").show();
    if (ownRpt) {
        $("#lblExpenseAmt").hide();
        $("#txtExpenseAmt").show();
    }
    else {
        $("#lblApprovedAmt").hide();
        $("#txtApprovedAmt").show();
    }
}

function UpdateReport()
{
    var model;
    var status = "Approved";
    var rptId = $("#lblRptId").text();
    var approvedAmt = $("#txtApprovedAmt").val();
    if($("#txtApprovedAmt").val()=="TBD" || $("#txtApprovedAmt").val()=="" ||$("#txtApprovedAmt").val()== null)
    {
        approvedAmt = 0;
        status = "Pending";
    }
    if ($("#btnApprove").visible == true) {
        model = {
            ApprovedAmt: approvedAmt,
            Comments: $("#txtComments").val(),
            ReportId: rptId,
            Status:status,
            isOwnRpt: false
        }
    }
    else {
        model = {
            ExpenseAmt: $("#txtExpenseAmt").val(),
            Comments: $("#txtComments").val(),
            ReportId: rptId,
            isOwnRpt: true
        }
    }

    $.ajax({
        type: "post",
        url: "/Employee/UpdateExpenseReport",
        data: model,
        datatype: "text",
        success: function (data) {
            alert(data.msg)
            if (data.success) {
                getReportDetails(rptId);
            }
        },
        error: function (data) {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//get empl details
function EmployeeDetails(Id, isOwn) {
    var empJson = { empId: Id };
    var isOwnEmp = isOwn;
    $.ajax({
        type: "GET",
        url: "/Employee/EmployeeDetails",
        data: empJson,
        datatype: "html",
        success: function (data) {
            $("#adminBody").html(data);
            $("#employeeBody").html(data);
            $("#divUpdate").hide();
            $("#divCancel").hide();
            $("#txtEmail").hide();
            $("#txtPhn").hide();
            if (isOwnEmp) {
                $("#divBacktoList").hide();
            }
            else {
                $("#divEdit").hide();
            }
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}


function EditEmployeeDetails() {
    $("#divBacktoList").hide();
    $("#divEdit").hide();
    $("#lblEmail").hide();
    $("#lblPhn").hide();
    $("#divUpdate").show();
    $("#divCancel").show();
    $("#txtEmail").show();
    $("#txtPhn").show();
}

function updateEmployeeDetails() {
    var model = {
        EmailId: $("#txtEmail").val(),
        ContactNumber: $("#txtPhn").val()
    }
    $.ajax({
        type: "post",
        url: "/Employee/EditEmployeeDetails",
        data: model,
        datatype: "text",
        success: function (data) {
            alert(data.msg)
            if (data.success) {
                EmployeeDetails(data.Id, true);
            }
            else {
                $("#errorMsg").html('<p class="text-danger">' + data.responseText + '<p>');
            }
        },
        error: function (data) {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//user login
function login() {
    var token = $("#formLogin input").val();
    var role;
    if ($("#empDiv").css("display") == "none") {
        role = "ADM";
    }
    else
        role = "EMP";
    var model = {
        __RequestVerificationToken: token,
        UserId: $("#UserId").val(),
        Password: $("#Password").val(),
        RememberMe: $("#RememberMe").val(),
        Role: role
    }
    $.ajax({
        type: "post",
        url: "/Account/UserValidate",
        data: model,
        datatype: "text",
        success: function (data) {
            if (data.success) {
                $("#formLogin").attr('validated', true);
                $("#formLogin").submit();
            }
            else {
                $("#errorMsg").html('<p class="text-danger">'+data.responseText+'</p>' );
            }
        },
        error: function (data) {
            $("#errorMsg").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//reset password post
function ResetPasswordPost() {
    var token = $("#formResetPwrd input").val();
    var model = {
        __RequestVerificationToken: token,
        Password: $("#txtPaswword").val(),
        ConfirmPassword: $("#txtConfirmPwrd").val()
    }
    $.ajax({
        type: "post",
        url: "/Account/ResetPassword",
        data: model,
        datatype: "text",
        success: function (data) {
            alert(data.message);
            if (data.success) {                
                $("#logoutForm").attr('validated', true);
                $("#logoutForm").submit();
            }
        },
        error: function (data) {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

function EmpDeactivate(Id) {
    var empId = { empId: Id}
    $.ajax({
        type: "post",
        url: "/Admin/DeactivateEmployee",
        data: empId,
        datatype: "text",
        success: function (data) {
            if (data.success) {
                $("#empList").trigger('click');
                $("#errorMessage").html('<p class="text-danger">' + data.msg + '<p>');
            }
            else {
                $("#errorMessage").html('<p class="text-danger">' + data.responseText + '<p>');
            }

        },
        error: function (data) {
            $("#errorMessage").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
}

function backToEmpList()
{
    $("#empList").trigger('click');
}



