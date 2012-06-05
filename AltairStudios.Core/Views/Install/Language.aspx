<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/resources/AltairStudios.Core.Views.Install.Install.master" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<form class="form-horizontal">
		<fieldset>
			<legend><%=Translate.t("install")%></legend>
			<div class="control-group">
				<label class="control-label" for="input01"><%=Translate.t("language")%></label>
				<div class="controls">
					<div class="btn-group">
						<button class="btn"><%=Translate.t("select_language")%></button>
						<button class="btn dropdown-toggle" data-toggle="dropdown">
							<span class="caret"></span>
						</button>
						<ul class="dropdown-menu">
							<li><a href="<%=this.Url.Action("Language", "Install", new {language="en-EN"})%>">English (en-EN)</a></li>
							<li class="divider"></li>
							<li><a href="<%=this.Url.Action("Language", "Install", new {language="es-ES"})%>">Español (es-ES)</a></li>
						</ul>
					</div>
				</div>
				<div class="form-actions">
					<button type="submit" class="btn btn-primary"><%=Translate.t("save_continue")%></button>
				</div>
			</div>
		</fieldset>
	</form>
</asp:Content>