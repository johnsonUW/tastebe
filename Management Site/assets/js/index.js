$(document).ready(function(){
    var username = sessionStorage.getItem("username");
    if(username != null){
    	$(".test session").val() = username;
    }
    

    $(".fa-sign-out").click(function(){
    	sessionStorage.clear();
    });
});