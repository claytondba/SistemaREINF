<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webREINF.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Login.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <div class="login">
                <br />
                <img src="image/grupo.png" />
                <br />
                <br />
                <h5>Login Web REINF App</h5>
                <hr />
                <div class="login-inner">
                    <asp:TextBox class="form-control email" placeholder="Digite o Login" ID="loginTextBox" runat="server"></asp:TextBox>
                    <asp:TextBox type="password" class="form-control password" placeholder="Senha" ID="senhaTextBox" runat="server"></asp:TextBox>



                    <label class="checkbox-inline">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </label>
                    <asp:Button class="btn btn-block btn-lg btn-success submit" ID="Button1" runat="server" Text="Entrar" OnClick="Button1_Click" />
                    <a href="#" class="btn btn-sm btn-primary register">Registrar</a>
                    <a href="#" class="btn btn-sm btn-default forgot">Esqueceu sua senha??</a>
                    <label class="checkbox-inline">
                    </label>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
