<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dokap.edit.Default" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title><%= core.css.appTitle() %></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/bootstrap-responsive.min.css" rel="stylesheet">
    <link href="assets/css/bootstrap-glyphicons.css" rel="stylesheet">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link href="assets/css/style.css" rel="stylesheet">

    <style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }

      .form-signin {
        max-width: 300px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        border: 1px solid #e5e5e5;
        -webkit-border-radius: 5px;
           -moz-border-radius: 5px;
                border-radius: 5px;
        -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
           -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
                box-shadow: 0 1px 2px rgba(0,0,0,.05);
      }
      .form-signin .form-signin-heading,
      .form-signin .checkbox {
        margin-bottom: 10px;
      }
      .form-signin input[type="text"],
      .form-signin input[type="password"] {
        font-size: 16px;
        height: auto;
        margin-bottom: 15px;
        padding: 7px 9px;
      }

    </style>
</head>
<body>

    <div style="width: 100%; text-align: center; margin-bottom: 20px;">
        <img src="assets/img/dokap_icon.png" />
    </div>
    <div class="container">
      <form class="form-signin" id="form1" runat="server">
        <asp:Literal ID="response" runat="server"></asp:Literal>
        <asp:TextBox ID="username" CssClass="input-block-level" runat="server"></asp:TextBox>
        <asp:TextBox ID="password" CssClass="input-block-level" TextMode="Password" runat="server"></asp:TextBox>

        <label class="checkbox">
            <asp:CheckBox ID="remember" runat="server" /> Remember me
        </label>
          <asp:LinkButton ID="login" runat="server" CssClass="btn btn-large btn-primary" OnClick="login_Click">Sign in</asp:LinkButton>
      </form>

    </div> <!-- /container -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/main.js"></script>
</body>
</html>
