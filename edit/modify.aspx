<%@ Page Title="" Language="C#" MasterPageFile="~/edit/edit.Master" AutoEventWireup="true" CodeBehind="modify.aspx.cs" Inherits="dokap.edit.modify" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<ul class="breadcrumb">
    <li><a href="content.aspx?d=/">Root</a> <span class="divider">/</span></li>
    <%=breadcrumb()  %>
</ul>


<ul class="nav nav-tabs">
  <li class="<%=(Request.QueryString["m"] != "1") ? "active" : "" %>">
    <a href="?d=<%=Request.QueryString["d"]%>&s=<%=Request.QueryString["s"]%>">Editor</a>
  </li>
  <li  class="<%=(Request.QueryString["m"] == "1") ? "active" : "" %>">
    <a href="?m=1&d=<%=Request.QueryString["d"]%>&s=<%=Request.QueryString["s"]%>">Source</a>
  </li>
</ul>

    
 <fieldset>
   <legend>Edit item</legend>
     <asp:Literal ID="editableItems" runat="server" EnableViewState="false"></asp:Literal>
    <label class="control-label"></label>
    <asp:linkbutton runat="server" ID="btnSave" CssClass="btn" OnClick="btnCreate_Click">Save</asp:linkbutton>

    <a href="<%= core.config.RemoteURL + currentFile() %>" class="btn" target="_blank">Preview</a>
    <a href="<%=backFileLink() %>" class="btn">Back</a>
  </fieldset>


</asp:Content>
