<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/resources/AltairStudios.Core.Views.Install.Install.master" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<form action="#>" class="form-horizontal">
		<fieldset>
			<legend><%=Translate.t("install_create_admin")%></legend>
			<div id="config-section-server" class="control-group hide">
				<label class="control-label" for="config-server"><%=Translate.t("user")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="user" id="config-user" placeholder="localhost" />
				</div>
			</div>
			<div id="config-section-password" class="control-group hide">
				<label class="control-label" for="config-password"><%=Translate.t("password")%>:</label>
				<div class="controls">
					<input type="text" class="input-xlarge" name="password" id="config-password" placeholder="********" />
				</div>
			</div>
			<div class="form-actions">
				<button type="submit" class="btn btn-primary"><%=Translate.t("save_continue")%></button>
			</div>
		</fieldset>
	</form>
</asp:Content>