AltairStudios.Core.Admin.Renderer = function() {
	var content = null;
	var sidebar = null;
	var navbar = null;
	
	
	
	this.configure = function configure() {
		this.navbar = $("#navbar");
		this.sidebar = $("#sidebar");
		this.content = $("#content");
	}
	
	
	
	this.renderGetUsers = function renderGetUsers(data) {
		var html = "";
		var i = 0;
		
		html += "<table class='table table-bordered table-striped'>";
		html += "<tr>";
		html += "<th>Email</th>";
		html += "<th>Nombre</th>";
		html += "</tr>";
		
		for(i = 0; i < data.length; i++) {
			html += "<tr>";
			html += "<td>" + data[i].Email + "</td>";
			html += "<td>" + data[i].Name + " " + data[i].Surname + "</td>";
			html += "</tr>";
		}
		html += "</table>";
		
		this.content.html(html);
	}
	
	
	this.renderHome = function renderHome(data) {
		var html = "";
		
		html += "<div class='hero-unit'><h1>Bienvenido!</h1><p>Te damos la bienvenida a nuestro administrador. Puedes realizar cualquier operación de una forma sencilla desde cualquier parte del menú. Si quieres saber mas, puedes contactar con nosotros mediante soporte o visitar nuestra web.</p><p><a href='http://www.altairstudios.es' class='btn btn-primary btn-large'>Visitanos »</a></p></div>";
		html += "<div class='row-fluid'><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--></div><!--/row--><div class='row-fluid'><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--></div><!--/row-->";
		
		this.content.html(html);
	}
}