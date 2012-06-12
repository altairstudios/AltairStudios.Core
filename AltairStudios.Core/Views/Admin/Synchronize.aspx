<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AltairStudios.Core.I18n" %>
<%@ Import Namespace="AltairStudios.Core.Mvc" %>
<%@ Import Namespace="AltairStudios.Core.Orm.Models" %>
<div class="row-fluid">
	<table class='table table-bordered table-striped'>
		<tr>
			<th>Object</th>
			<th>Sync</th>
		</tr>
		<tr>
			<%
				AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model> models = (AltairStudios.Core.Orm.ModelList<AltairStudios.Core.Orm.Model>)ViewData["models"];
				for(int i = 0; i < models.Count; i++) {
			%>
				<tr>
					<td><%=models[i].ToString()%></td>
					<td><a class="btn btn-inverse synchronize-button" href="#!/synchronize-model/<%=models[i].ToString()%>"><i class="icon-refresh icon-white"></i>sync</a></a></td>
				</tr>
			<%
				}
			%>
		</tr>
	</table>
</div>
<script type="text/javascript">
	$(".synchronize-button").on("click", function() {
		var me = $(this);
		me.removeClass("btn-inverse");
		me.addClass("btn-success");
	});
</script>