<%@ Page Title="" Language="C#" MasterPageFile="~/edit/edit.Master" AutoEventWireup="true" CodeBehind="content.aspx.cs" Inherits="dokap.edit.content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ul class="breadcrumb">
        <li><a href="?d=/">Root</a> <span class="divider">/</span></li>
        <%=breadcrumb()  %>
    </ul>

 <fieldset>
    <legend>Contents</legend>
    <div class="items">
        <asp:Label ID="Items" runat="server" Text="Label"></asp:Label>
    </div>

    <div class="btn-group">
      <a href="upload.aspx?<%= currentDirLink()%>" class="btn">Upload</a>
      <a href="create.aspx?t=0&<%= currentDirLink()%>" class="btn">Create dir</a>
      <a href="create.aspx?t=1&<%= currentDirLink()%>" class="btn">Create file</a>
    </div>

</fieldset>












</asp:Content>
