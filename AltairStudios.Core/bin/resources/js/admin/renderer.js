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
		
		if(data.Sidebar) {
			html += this.elementRenderSidebar(data.Sidebar);
			html += "<div class='span9'>";
		} else {
			html += "<div>";
		}
		
		if(data.Notice) {
			html += this.elementRenderNotice(data.Notice)
		}
	
		if(data.Content) {
			html += "<div class='row-fluid'><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--></div><!--/row--><div class='row-fluid'><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--><div class='span4'><h2>Heading</h2><p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p><p><a class='btn' href='#'>View details »</a></p></div><!--/span--></div><!--/row-->";
		}
		
		html += "</div><!--/span-->";

		this.content.html(html);
	}
	
	
	this.elementRenderNotice = function elementRenderNotice(data) {
		var html = "";
		
		html += "<div class='hero-unit'><h1>" + data.Title + "</h1><p>" + data.Text + "</p><p><a href='" + data.Link.Anchor + "' class='btn btn-primary btn-large' title='" + data.Link.Title + "'>" + data.Link.Name + "</a></p></div>";
		
		return html;
	}
	
	
	this.elementRenderSidebar = function renderElementSidebar(data) {
		var html = "";
		
		html += "<div class='span3' id='sidebar'><div class='well sidebar-nav'><ul class='nav nav-list'><li class='nav-header'>Menú</li><li class='active'><a href='#'>Home</a></li><li><a href='#'>Usuarios</a></li></ul></div><!--/.well --></div><!--/span-->";
		
		return html;
	}
}