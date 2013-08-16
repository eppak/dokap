<%@ Page Title="" Language="C#" MasterPageFile="~/edit/edit.Master" AutoEventWireup="true" CodeBehind="create.aspx.cs" Inherits="dokap.edit.create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <ul class="breadcrumb">
          <li><a href="content.aspx?d=/">Root</a> <span class="divider">/</span></li>
          <%=breadcrumb()  %>
        </ul>

 <fieldset>
    <legend>Create item</legend>
    <label>Name</label>
    <asp:textbox runat="server" ID="Name"></asp:textbox>

    <div style="display: <%= (Request.QueryString["t"] == "1") ? "block" : "none" %>">
        <label class="control-label">Tempalte</label>
        <asp:DropDownList ID="extensions" runat="server"></asp:DropDownList>
    </div>  
    
    <label class="control-label"></label>
    <asp:linkbutton runat="server" ID="btnCreate" CssClass="btn" OnClick="btnCreate_Click">Create</asp:linkbutton>
    <a href="<%=backDirLink() %>" class="btn">Back</a>
   
  </fieldset>







</asp:Content>
