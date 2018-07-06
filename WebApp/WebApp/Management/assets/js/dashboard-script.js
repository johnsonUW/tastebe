$(document).ready(function () {
    
    var username = sessionStorage.getItem("username");
    if (username !== null) {
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
            $('#name').append(data.name);
            var html = '<p id="address">餐厅地址:' + data.location + '</p>';
            html += '<p id="owner">老板:' + data.owner + '</p>';
            html += '<p id="phone">联系电话:' + data.phone + "</p>";
            if (username === "Admin") {
                html += '<input id="amount" type="text" name="发放金额: $">';
                html += '<input id="sendId" type="text" name="接受金额的商家id：">';
                html += '<button id="send">确认发放</button>'
            }
            $('.row').html(html);
            sessionStorage.setItem("userId", data.id);
        })
        .fail(function (jqXHR, textStatus, err) {
            alert("系统繁忙，请稍后再试");
        });

    $('#send').click(function () {
        var amount = parseInt($("#amount").val());
        var id = parseInt($("#sendId").val());
        $.ajax({
            url: '/api/v1/admin/outstandingbalance?restaurantId=' + id + '&amount=' + amount,
            type: 'POST',
            async: true,
            success: function (data) {
                var html = "<p>放款成功</p>";
                $('.success').html(html);
            },
            fail: function (jqXHR, textStatus, err) {
                alert("系统繁忙，请稍后再试");
            },
        })
    });
});