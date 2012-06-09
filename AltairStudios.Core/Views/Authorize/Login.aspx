<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AltairStudios.Core.Mvc" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<!DOCTYPE html>
<html lang="es">
	<head>
		<meta charset="utf-8" />
		<title>Login</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0" />
		<meta name="description" content="AltairStudios.Core - Administration module" />
		<meta name="author" content="Altair Studios" />
		
		<link rel="stylesheet" type="text/css" href="<%=Url.Content("~/Resource/load/AltairStudios.Core.resources.css.bootstrap.css")%>" />
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
		<div class="container-fluid">
			<section>
				<div class="row-fluid">
					<div class="span4">&nbsp;</div>
					<div class="span4">
						<form>
							<fieldset>
								<legend><%=Translate.t("access")%></legend>
								<div class="control-group">
									<label class="control-label" for="loginUser"><%=Translate.t("user")%>:</label>
									<div class="controls">
										<input name="Email" class="input-xlarge focused" id="loginUser" type="text" placeholder="Email" autocomplete="off" />
									</div>
								</div>
								<div class="control-group">
									<label class="control-label" for="loginPassword"><%=Translate.t("password")%>:</label>
									<div class="controls">
										<input name="Password" class="input-xlarge focused" id="loginPassword" type="password" placeholder="**********" autocomplete="off" />
									</div>
								</div>
								<div class="control-group form-inline">
									<label class="control-label" for="loginRemember"><%=Translate.t("remember")%>: <input name="Remember" class="input-small" id="loginRemember" type="checkbox" /></label>
								</div>
								<div class="control-group">
									<a id="loginButton" class="btn btn-primary" href="#"><i class="icon-off icon-white"></i> <%=Translate.t("access")%></a>
								</div>
							</fieldset>
						</form>
		 			</div>
		 			<div class="span4">&nbsp;</div>
				</div>
			</section>
		</div>
		
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.jquery.js"> </script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.bootstrap.js"> </script>
		<script type="text/javascript" src="<%=MvcApplication.Path%>/Resource/load/AltairStudios.Core.resources.js.login.js"> </script>
			
	</body>
</html>