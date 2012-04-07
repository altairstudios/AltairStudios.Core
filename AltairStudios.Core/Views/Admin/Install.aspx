<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AltairStudios.Core.Mvc" %>
<%@ Import Namespace="AltairStudios.Core.Orm" %>
<!DOCTYPE html>
<html lang='es'>
	<head>
		<meta charset='utf-8' />
		<title>Instalador</title>
		<meta name='viewport' content='width=device-width, initial-scale=1.0' />
		<meta name='description' content='AltairStudios.Core - Administration module' />
		<meta name='author' content='Altair Studios' />
		
		<link rel='stylesheet' type='text/css' href='<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.css.bootstrap.css' />
		<link rel='stylesheet' type='text/css' href='<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.css.bootstrap-responsive.css' />
		
		<style type='text/css'>
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
		<div class='container-fluid'>
			<section>
				<div class='row-fluid'>
					<div class='span4'>&nbsp;</div>
					<div class='span4'>
						<form>
							<fieldset>
								<legend>Instalar</legend>
								<div class='control-group'>
									<label class='control-label' for='loginUser'>Añade la siguiente linea al webconfig:</label>
									<label class='control-label' for='loginUser'>
										&lt;connectionStrings&gt;<br/>&nbsp;&nbsp;&nbsp;&nbsp;&lt;add name="SqlServerConnection" connectionString="Datasource=[server];Database=[database];uid=[user];pwd=[password];Pooling=true;Min Pool Size=0;Max Pool Size=100;" providerName="MySql.Data.MySqlClient"/&gt;<br/>&lt;/connectionStrings&gt;
									</label>
								</div>
							</fieldset>
						</form>
					</div>
				<div class='span4'>
				<%
					AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model> models = (AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model>)ViewData["models"];
					for(int i = 0; i < models.Count; i++) {
				%>
						<code><%=models[i].createTable()%></code>
				<%
					}
				%>
				</div>
			</section>
		</div>

		<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js'> </script>
		<script type='text/javascript' src='<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.bootstrap.js'> </script>
			
	</body>
</html>