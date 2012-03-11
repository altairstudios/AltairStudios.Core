AltairStudios.Core.Admin.Renderer = function() {
	var content = null;
	var sidebar = null;
	var navbar = null;
	
	
	
	this.configure = function configure() {
		this.navbar = $("#navbar");
		this.sidebar = $("#sidebar");
		this.content = $("#content");
	}
	
	
	
	this.renderGetUsers = function renderGetUsers() {
		var html = "";
		
		html += "<table class='table table-bordered table-striped'>";
		html += "<tr>";
		html += "<th>Email</th>";
		html += "<th>Nombre</th>";
		html += "</tr>";
		html += "<tr>";
		html += "<td>juan@gmail.com</td>";
		html += "<td>Juan Benavides Romero</td>";
		html += "</tr>";
		html += "</table>";
		
		this.content.html(html);
	}
}