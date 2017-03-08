$(document).load(function () {
    $.ajax({
        type: "GET",
        url: "/Employee/GenerateExpenseReport",
        datatype: "html",
        success: function (data) {
            $("#employeeBody").html(data);
        },
        error: function () {
            $("#employeeBody").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});

