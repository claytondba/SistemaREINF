<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="AjudaParceiro.aspx.cs" Inherits="webREINF.AjudaParceiro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <link href="Content/bootstrap-grid.css" rel="stylesheet" />
    <link href="Content/Grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="container">
        <h2>Ajuda</h2>
        <br />
        <h3>Bem vindo!</h3>
        <br />
        Nesse portal você poderá gerenciar todos os arquivos enviados por seus clientes.<br />
        Para começar, você pode verificar o menu <b><i>Cliente</i></b>, para uma lista de clientes cadastrados.<br />
        Para incluir um novo <i><b>Cliente</b></i>, você pode ir ao menu cliente e selecionar a opção <b><i>Cadastrar</i></b>.<br />
        Os últimos envios de seus clientes irão aparecer no menu lateral <i><b>Últimos Envios</b></i>, é a maneira mais rápida para checar novos arquivos.
        <br />
        <br />
        Qualquer dúvida estamos a disposição! - <i>Grupo Candinho<i/>
    </div>
    <br />
</asp:Content>
