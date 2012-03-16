$(document).ready(function() {
	var email = $("#loginUser");
	var password = $("#loginPassword");
	
	$("#loginButton").click(function() {
		$.post("Authorize", {
			Email:email.val(),
			Password: password.val()
		}, function(data) {
			document.location = "../Admin";
		})
	});
});