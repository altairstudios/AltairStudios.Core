<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AltairStudios.Core.Mvc" %>
<%@ Import Namespace="AltairStudios.Core.Orm.Models" %>
<!DOCTYPE html>
<html lang="es">
	<head>
		<meta charset="utf-8" />
		<title>Admin</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />
		<meta name="description" content="AltairStudios.Core - Administration module" />
		<meta name="author" content="Altair Studios" />
		
		<link rel="stylesheet" type="text/css" href="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.css.bootstrap.css" />
		<link rel="stylesheet" type="text/css" href="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.css.bootstrap-responsive.css" />
		
		<style type="text/css">
			body {
				padding-top: 60px;
				padding-bottom: 40px;
			}
			.sidebar-nav {
			 	padding: 9px 0;
			}
		</style>
	</head>
	<body>	
		<div id="navbar" class="navbar navbar-fixed-top">
			<div class="navbar-inner">
				<div class="container-fluid">
					<a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
					</a>
					<a class="brand" href="#">Administrador</a>
					<div class="nav-collapse">
						<ul class="nav">
							<li class="active">
								<a href="<%=MvcApplication.Path%>/Admin/Desktop#!/home">Home</a>
							</li>
							<li class="dropdown" id="menuDatabase">
								<a class="dropdown-toggle" data-toggle="dropdown" href="#menuDatabase">Database<strong class="caret"></strong></a>
								<ul class="dropdown-menu">
								<%
									AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model> models = (AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model>)ViewData["models"];
									for(int i = 0; i < models.Count; i++) {
								%>
										<li><a href="<%=MvcApplication.Path%>/Admin/Desktop#!/database-viewer/<%=models[i].ToString()%>"><%=models[i].ToString()%></a></li>
								<%
									}
								%>
								</ul>
							</li>
						</ul>
						<%
							User user = (User)ViewData["user"];
						%>
						<p class="navbar-text pull-right">Loggeado como <a href="<%=MvcApplication.Path%>/Admin/Desktop#!/logout"><%=user.Name%> <%=user.Surname%></a></p>
					</div>
	        	</div>
			</div>
		</div>
		
		<div class="container-fluid">
			<div class="row-fluid" id="content">
		</div>
			
		<hr />
			
		<footer>
			<p>© Altair Studios 2012</p>
		</footer>
	
		<script type="text/javascript">var coreProcess = null;var AltairStudios = { Core: { Admin: { Plugins: {} } } }; var path = "<%=MvcApplication.Path%>";</script>

		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.jquery.js"> </script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.bootstrap.js"> </script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.bootstrap-dropdown.js"></script>
			
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.admin.renderer.js"></script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.admin.core.js"></script>
		
		<%		
		if(ConfigurationManager.AppSettings["altairstudios.core.feedback.disable"] != "true") {
		%>
		<script type="text/javascript">
			var uvOptions = {};
			(function() {
				var uv = document.createElement("script"); uv.type = "text/javascript"; uv.async = true;
				uv.src = ("https:" == document.location.protocol ? "https://" : "http://") + "widget.uservoice.com/G4Ce3YuUFULbfnRobWXGQ.js";
				var s = document.getElementsByTagName("script")[0]; s.parentNode.insertBefore(uv, s);
			})();
		</script>
		<%
			}
		%>
	</body>
</html>