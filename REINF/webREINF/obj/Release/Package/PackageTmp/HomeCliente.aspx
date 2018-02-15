<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCliente.Master" AutoEventWireup="true" CodeBehind="HomeCliente.aspx.cs" Inherits="webREINF.HomeCliente" %>

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
    <div class="container">
        <h2>
            <asp:Label ID="titleLabel" runat="server" Text=""></asp:Label></h2>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
</asp:Content>
