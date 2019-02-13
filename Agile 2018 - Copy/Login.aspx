<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Agile_2018.WebForm1" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %></title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <!--Import Google Icon Font-->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/0.100.2/css/materialize.min.css">
    <link rel="stylesheet" runat="server" media="screen" href="~/Content/css/Site.css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <!--Import jQuery before materialize.js-->
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/0.100.2/js/materialize.min.js"></script>

</head>
<body class="grey lighten-5">
    <div class="valign-wrapper row login-box">
        <div class="col card s10 pull-s1 m10 pull-m1 l10 pull-l1">
            <form role="form" runat="server" defaultbutton="login">
                <div class="card-content">
                    <span class="card-title">Login</span>
                    <div class="row login-hightlight">
                        <div class="input-field col s12">
                            <i class="material-icons prefix green-text">account_circle</i>
                            <input runat="server" id="username" class="validate" type="text" />
                            <label for="username">Username</label>
                        </div>
                        <div class="input-field col s12">
                            <i class="material-icons prefix green-text">lock</i>
                            <input runat="server" id="password" class="validate" type="text" />
                            <label for="password">Password</label>
                        </div>
                    </div>
                    <div class="row center-align">
                        <div class="input-field col s12" style="color: red">
                            <asp:Label runat="server" ID="errorLabel"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="card-action right-align">
                    <div class="left">
                        <input runat="server" id="checkbox" class="filled-in checkbox-green" type="checkbox">
                        <label for="checkbox">Remember Me</label>
                    </div>
                    <asp:LinkButton runat="server" ID="login" OnClick="LoginControl_Authenticate" type="submit" class="btn green waves-effect waves-light">
                        Login
                    </asp:LinkButton>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
