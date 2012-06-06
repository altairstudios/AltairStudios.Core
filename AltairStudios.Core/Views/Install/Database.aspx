<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/resources/AltairStudios.Core.Views.Install.Install.master" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<form action="<%=this.Url.Action("Language", "Database", new {language="en"})%>" class="form-horizontal">
		<fieldset>
			<legend><%=Translate.t("install")%></legend>
			<div class="control-group">
				<ul class="thumbnails">
  <li class="span3">
    <a href="#" class="thumbnail">
      <img src="http://placehold.it/260x180" alt="">
    </a>
  </li>
  <li class="span3">
    <a href="#" class="thumbnail">
      <img src="http://placehold.it/260x180" alt="">
    </a>
  </li>
  <li class="span3">
    <a href="#" class="thumbnail">
      <img src="http://placehold.it/260x180" alt="">
    </a>
  </li>
  <li class="span3">
    <a href="#" class="thumbnail">
      <img src="http://placehold.it/260x180" alt="">
    </a>
  </li>
  
</ul>
				<!--<div class="form-actions">
					<button type="submit" class="btn btn-primary"><%=Translate.t("save_continue")%></button>
				</div>-->
			</div>
		</fieldset>
	</form>
</asp:Content>