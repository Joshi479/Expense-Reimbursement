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
