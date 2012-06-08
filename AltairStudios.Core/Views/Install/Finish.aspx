<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/resources/AltairStudios.Core.Views.Install.Install.master" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<p>
		<%=Translate.t("install_configure_webconfig")%>
		<pre>&lt;configuration&gt;
	&lt;appSettings&gt;
		<%=ViewData["admin-user"]%>
		<%=ViewData["admin-password"]%>
		<%=ViewData["admin-name"]%>
	&lt;/appSettings&gt;
&lt;/configuration&gt;</pre>

		<pre>&lt;/configuration&gt;
	&lt;/connectionStrings&gt;
		<%=ViewData["admin-connectionstring"]%>
	&lt;/connectionStrings&gt;
&lt;/configuration&gt;</pre>
	</p>
</asp:Content>