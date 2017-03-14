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
//get empl details
function EmployeeDetails(Id) {
    var empJson = { empId: Id }
    $.ajax({
        type: "GET",
        url: "/Employee/EmployeeDetails",
        data: empJson,
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
}

//user login
function login() {
    var token = $("#__AjaxAntiForgeryForm input").val();
    var model = {
        __RequestVerificationToken: token,
        UserId: $("#UserId").val(),
        Password: $("#Password").val(),
        RememberMe: $("#RememberMe").val()
    }
    $.ajax({
        type: "post",
        url: "/Account/UserValidate",
        data: model,
        datatype: "text",
        success: function (data) {
            if (data.success) {
                $("#__AjaxAntiForgeryForm").attr('validated', true);
                $("#__AjaxAntiForgeryForm").submit();
            }
            else
            {
                $("#errorMsg").html('<p class="text-danger">' + data.responseText + '<p>');
            }
            
        },
        error: function (data) {
            $("#errorMsg").html('<h1>Error Occured. Please check the connection.</h1>')
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



