/// <reference path="../DataTables/datatables.js" />

//employee registration validation
function validate() {
    var fName = document.getElementById("fName").value;
    var regex = /^[a-zA-Z]*$/;
    if (regex.test(fName)) {
        //document.getElementById("notification").innerHTML = "Watching.. Everything is Alphabet now";
        return true;
    } else {
        document.getElementById("fName_Msg").innerHTML = "Alphabets Only";
        return false;
    }
}
//render Employee login
$("#EmpLogin").click(function () {
    $.ajax({
        type: "GET",
        url: "/Account/Login",
        datatype: "html",
        success: function (data) {
           // $("#adminDiv").hide();
            $("#admLoginDiv").hide();
            $("#empLoginDiv").show();
            $("#empLoginDiv").html(data);
            $("#EmpLogin").addClass('red');
            $("#AdminLogin").removeClass('red');
        },
        error: function () {
            $("#empLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});
//onload
$(document).ready(function () {
    $("#loginDiv").hide();
});
//login1 button
$("#login1").click(function () {
    $("#loginDiv").show();
    $("#myCarousel").hide();


});

//render admin login
$("#AdminLogin").click(function () {
    $.ajax({
        type: "GET",
        url: "/Account/Login",
        datatype: "html",
        success: function (data) {
           // $("#empDiv").hide();
            $("#empLoginDiv").hide();
            $("#admLoginDiv").show();
            $("#admLoginDiv").html(data);
            $("#EmpLogin").removeClass('red');
            $("#AdminLogin").addClass('red');
           
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
        error: function () {c
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
});
//on click of edit button
$("#btnEdit").click(function () {
    $("#lblExpenseAmt").hide();
    $("#txtExpenseAmt").show();
    $("#lblComments").hide();
    $("#txtComments").show();
    $("#beforeEdit").hide();
    $("#afterEdit").show();
});
//on click of approve button
$("#btnUpdatebefore").click(function () {
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

//About
function getAbout(div) {
    var myDiv = document.getElementById(div);
    $.ajax({
        type: "GET",
        url: "/Home/About",
        datatype: "html",
        success: function (data) {
            $("#myCarousel").hide();
            myDiv.innerHTML = data;
        },
        error: function () {
            myDiv.innerHTML = '<h1>Error Occured. Please check the connection.</h1>';
        }
    });
}
//contact
function getContact(div) {
    var myDiv = document.getElementById(div);
    $.ajax({
        type: "GET",
        url: "/Home/Contact",
        datatype: "html",
        success: function (data) {
            $("#myCarousel").hide();
            myDiv.innerHTML = data;
        },
        error: function () {
            myDiv.innerHTML = '<h1>Error Occured. Please check the connection.</h1>';
        }
    });
}
// get forgot password
function ForgotPassword() {
    $.ajax({
        type: "GET",
        url: "/Account/ForgotPassword",
        datatype: "html",
        success: function (data) {
            if ($("#empLoginDiv").css("display") === "none") {
                $("#admLoginDiv").html(data);
            }
            else {
                $("#empLoginDiv").html(data);
            }            
        },
        error: function (data) {
            if ($("#empLoginDiv").css("display") === "none") {
                $("#admLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }
            else {
                $("#empLoginDiv").html('<h1>Error Occured. Please check the connection.</h1>');
            }            
        }
    });
}
//on click of email password
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
        url: "/Account/ForgotPassword",
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
            if ($("#empLoginDiv").css("display") === "none") {
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
    var role = empRole;
    var empJson = { approverRole: role }
    $.ajax({
        type: "GET",
        url: "/Employee/GetReportListApproval",
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

//get report details
function getReportDetails(Id) {
    sessionStorage.setItem('rptData', $('#employeeBody').html());
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
//edit report
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
//update report
function UpdateReport(empRole)
{
    var role = empRole;
    var own;
    var model;
    var status = "Approved";
    var rptId = $("#lblRptId").text();
    var approvedAmt = $("#txtApprovedAmt").val();
    if($("#txtApprovedAmt").val()=="TBD" || $("#txtApprovedAmt").val()=="" ||$("#txtApprovedAmt").val()== null)
    {
        approvedAmt = 0;
        status = "Pending";
    }
    if ($("#btnApprove").val() == "Approve") {
        own = false;
        model = {
            ApprovedAmt: approvedAmt,
            Comments: $("#txtComments").val(),
            ReportId: rptId,
            Status: status,
            EmpId: $("#empId").text(),
            isOwnRpt: own
        }
    }
    else {
        own = true;
        model = {
            ExpenseAmt: $("#txtExpenseAmt").val(),
            Comments: $("#txtComments").val(),
            ReportId: rptId,
            isOwnRpt: own
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
                if (own) {
                    getReportList($("#empId").text());
                }
                else {
                    getReportListApproval(role);
                }                   
            }
        },
        error: function (data) {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>');
        }
    });
}

//get empl details
function EmployeeDetails(Id, isOwn) {
    if ($('#employeeBody').html() != undefined) {
        sessionStorage.setItem('empData', $('#employeeBody').html());
    }
    else {
        $('.text-danger').text('');
        sessionStorage.setItem('empData', $('#adminBody').html());
    }
    
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
//on click of edit employee
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
//on update employee
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
    if ($("#empLoginDiv").css("display") == "none") {
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
//employee deactivation
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
//navigate to employee list
function backToEmpList()
{
    $('#adminBody').html(sessionStorage.getItem('empData'));
    if (sessionStorage.getItem('empData') != null) {
        $('#employeeBody').html(sessionStorage.getItem('empData'));
        sessionStorage.removeItem('empData');
    }
    else {
        $('#employeeBody').html(sessionStorage.getItem('rptData'));
        sessionStorage.removeItem('rptData');
    }
}



