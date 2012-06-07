<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/resources/AltairStudios.Core.Views.Install.Install.master" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<form action="<%=this.Url.Action("Database", "Install")%>" class="form-horizontal">
		<fieldset>
			<legend><%=Translate.t("install_select_database")%></legend>
			<div class="control-group">
				<ul class="thumbnails">
					<li class="span3">
						<a href="#" class="thumbnail" data-config-section="mysql">
							<img src="<%=Url.Content("~/Resource/load/AltairStudios.Core.resources.img.sql.mysql.png")%>" alt="" />
						</a>
					</li>
					<li class="span3">
						<a href="#" class="thumbnail" data-config-section="sqlserver">
							<img src="<%=Url.Content("~/Resource/load/AltairStudios.Core.resources.img.sql.sqlserver.png")%>" alt="" />
						</a>
					</li>
				</ul>
			</div>
			<hr/>
			<div id="config-section-server" class="control-group hide">
				<label class="control-label" for="config-server"><%=Translate.t("server")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="server" id="config-server" placeholder="localhost" />
				</div>
			</div>
			<div id="config-section-database" class="control-group hide">
				<label class="control-label" for="config-database"><%=Translate.t("database")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="database" id="config-database" placeholder="core_database" />
				</div>
			</div>
			<div id="config-section-user" class="control-group hide">
				<label class="control-label" for="config-user"><%=Translate.t("user")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="user" id="config-user" placeholder="root" />
				</div>
			</div>
			<div id="config-section-password" class="control-group hide">
				<label class="control-label" for="config-password"><%=Translate.t("password")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="password" id="config-password" placeholder="********" />
				</div>
			</div>
			<div class="form-actions">
				<input type="hidden" id="config-section-type" id="config-section-dbtype" name="dbtype" value="" />
				<button type="submit" class="btn btn-primary"><%=Translate.t("save_continue")%></button>
			</div>
		</fieldset>
	</form>
</asp:Content>


<asp:Content id="scriptContent" ContentPlaceHolderID="scriptContent" runat="server">
	$("a.thumbnail").on("click", function() {
		var element = $(this);
		var type = element.attr("data-config-section");
		var sections = {
			dbtype: $("#config-section-type"),
			server: $("#config-section-server"),
			database: $("#config-section-database"),
			user: $("#config-section-user"),
			password: $("#config-section-password")
		}
		
		sections.server.addClass("hide");
		sections.database.addClass("hide");
		sections.user.addClass("hide");
		sections.password.addClass("hide");
		
		sections.dbtype.val(type);
		
		if(type == "mysql" || type == "sqlserver") {
			sections.server.removeClass("hide");
			sections.database.removeClass("hide");
			sections.user.removeClass("hide");
			sections.password.removeClass("hide");
		}
	});
</asp:Content>