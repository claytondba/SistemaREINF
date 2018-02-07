<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="PerceiroAcesso.aspx.cs" Inherits="webREINF.PerceiroAcesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="info">

            <p>
                <br />
                <strong>Info!</strong>
                Cadastre uma noava senha!!!<br />
                <br />
            </p>
        </div>
        <h2>Cadastre uma nova senha!</h2>

    </div>
    <div class="container" style="background-color: white">
        <asp:TextBox type="password" class="txt" placeholder="Senha" ID="senhaTextBox" runat="server"></asp:TextBox>
        <asp:TextBox type="password" class="txt" placeholder="Confirmação de Senha" ID="senhaTextBox2" runat="server"></asp:TextBox>
        <label>
            <asp:Label runat="server" ID="labelSenha" Text="" />
        </label>
    </div>

    <div class="container">
        <input type="submit" value="Cadastrar">
    </div>
</asp:Content>
