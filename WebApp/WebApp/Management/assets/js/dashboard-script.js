$(document).ready(function () {
    
    var username = sessionStorage.getItem("username");
    if (username != null) {
        $(".display_username").append(username);
    } else {
        $(window).attr('location', 'index.html');
        return;
    }

    $(".fa-sign-out").click(function () {
        sessionStorage.clear();
        $(window).attr('location', 'index.html');
        return;
    });

    var uri = '/api/v1/restaurants/user/'+username;

    $.getJSON(uri)
        .done(function (data) {
            console.log(data);
            $('#name').append(data.name);
            $('#address').append(data.location);
            $('#owner').append(data.owner);
            $('#phone').append(data.phone);
        })
        .fail(function (jqXHR, textStatus, err) {
            alert("系统繁忙，请稍后再试");
        });

});