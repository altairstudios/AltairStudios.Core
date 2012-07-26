<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Blog/Blog.master" %>
<%@ Import Namespace="AltairStudios.Core.Orm" %>
<%@ Import Namespace="AltairStudios.Core.Tests.Web.Models.Blog" %>
<asp:Content id="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<%
		ModelList<Post> posts = (ModelList<Post>)ViewData["posts"];
	%>
	<% for(int i = 0; i < posts.Count; i++) { %>
	<article>
		<header>
			<h2><a href="<%=Url.Action("Post", "Blog")%>/<%=posts[i].Url%>"><%=posts[i].Title%></a></h2>
		</header>
		<section><%=posts[i].Content%></section>
	</article>
	<% } %>
</asp:Content>