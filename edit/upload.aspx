<%@ Page Title="" Language="C#" MasterPageFile="~/edit/edit.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="dokap.edit.upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<ul class="breadcrumb">
    <li><a href="content.aspx?d=/">Root</a> <span class="divider">/</span></li>
    <%=breadcrumb()  %>
</ul>

 <fieldset>
    <legend>Upload</legend>
    <label>File</label>
     <asp:fileupload runat="server" ID="uploaded"></asp:fileupload>

    <label class="control-label"></label>



    <asp:linkbutton runat="server" ID="btnUpload" CssClass="btn" OnClick="btnUpload_Click">Load</asp:linkbutton>
    <a href="<%=backDirLink() %>" class="btn">Back</a>
   
  </fieldset>


</asp:Content>
