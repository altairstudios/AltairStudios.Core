$(document).on("ready", function() {
	var email = $("#loginUser");
	var password = $("#loginPassword");
	
	$("#loginButton").click(function() {
		$.getJSON("Authorize", {
			Email:email.val(),
			Password: password.val()
		}, function(data) {
			if(data.error) {
				return;
			}
			
			document.location = data.url;
		});
	});
});