<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="HomeParceiro.aspx.cs" Inherits="webREINF.HomeParceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <h2>Bem vindo ao Portal de Gerenciamento REINF</h2>
        <p>Navegue pelos menus acima.</p>
    </div>
    <br />
    <div class="container" >
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Nome da Empresa</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Senha" ID="empresaTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Usuário</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Senha" ID="usuarioTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
     </div>

 
</asp:Content>
