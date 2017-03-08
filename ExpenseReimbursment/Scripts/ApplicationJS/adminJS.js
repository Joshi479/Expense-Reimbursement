$(document).load(function () {
    $.ajax({
        type: "GET",
        url: "/Admin/RegisterEmployee",
        datatype: "html",
        success: function (data) {
            $("#adminBody").html(data);
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});

// edit emp click

$('#editEmp').click(function () {
    $.ajax({
        type: "GET",
        url: "/Admin/EditEmployeeDetails",
        datatype: "html",
        success: function (data) {
            $("#adminBody").html(data);
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});

$('#editEmp').click(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/EditEmployeeDetails", //change ajax function
        success: function (data) {
            $("#adminBody").html(data);
        },
        error: function () {
            $("#adminBody").html('<h1>Error Occured. Please check the connection.</h1>')
        }
    });
});