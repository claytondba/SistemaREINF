<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="CadastroCliente.aspx.cs" Inherits="webREINF.CadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <h2>Incluir novo acesso de Cliente</h2>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-25">
                <label for="fname" class="col-auto">Contato</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Nome do Contato da Empresa" ID="nomeTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Razão Social</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Nome da Empresa" ID="razaoTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Email</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Email" ID="emailTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">CNPJ</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="99.999.999/9999-99" ID="cnpjTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Telefone</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="" ID="telefoneTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Login</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="text" CssClass="txt" placeholder="Login para Acesso" ID="loginTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Senha Inicial</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="password" CssClass="txt" placeholder="*Será alterada no primeiro acesso" ID="senhaTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Ativado?</label>
            </div>
            <div class="col-75">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Ativado?" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="Button1" runat="server" Text="Cadastrar" OnClick="Button1_Click" />
            </div>
        </div>
    </div>
</asp:Content>
