<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="HomeParceiro.aspx.cs" Inherits="webREINF.HomeParceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Bem vindo ao Portal de Gerenciamento REINF</h2>
        <p>Escolha as opções desejadas...</p>
    </div>

    <div class="container" style="background-color: white">
        <input type="text" placeholder="Name" name="name" required>
        <input type="text" placeholder="Email address" name="mail" required>
        <label>
            <input type="checkbox" checked="checked">
            Daily Newsletter
        </label>
    </div>

    <div class="container">
        <input type="submit" value="Subscribe">
    </div>
</asp:Content>
