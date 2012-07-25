<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
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
		<link rel="stylesheet" type="text/css" href="<%=MvcApplication.Path%>/Content/css/docs.css" />
		
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
		<div class="container">
			<div class="row-fluid" id="content">
				<div class="marketing">
  					<h1>AltairStudios.Core Tests</h1>
					<p class="marketing-byline">All test examples that you need.</p>
					<div class="row">
						<div class="span4">
							<img class="bs-icon" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.img.glyphicons.png.glyphicons_330_blog.png" />
							<h2>Blog sample</h2>
							<p>A simple blog sample to see AltairStudios.Core powerfull. You can <a href="<%=Url.Action("Index", "Blog")%>">test it</a>.</p>
						</div>
					</div>
				</div>
				<footer>
					<p>© Altair Studios 2012</p>
				</footer>
			</div>
		</div>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.jquery.js"> </script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.bootstrap.js"> </script>
		
	</body>
</html>