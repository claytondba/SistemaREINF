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
                <img src="image/logo_grupocandinho.gif" />
                <br />
                <br />
                <h5>Login Web REINF App</h5>
                <hr />
                <div class="login-inner">
                    <asp:TextBox class="form-control email" placeholder="Digite o Login" ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:TextBox type="password" class="form-control password" placeholder="Senha" ID="TextBox2" runat="server"></asp:TextBox>

  
                    
                    <label class="checkbox-inline">
                        <input type="checkbox" id="remember" value="Remember me" />
                        Lembrar me
				
                    </label>
                    <asp:Button class="btn btn-block btn-lg btn-success submit" ID="Button1" runat="server" Text="Button" />
                    <a href="#" class="btn btn-sm btn-primary register">Registrar</a>
                    <a href="#" class="btn btn-sm btn-default forgot">Esqueceu sua senha??</a>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
