<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCliente.Master" AutoEventWireup="true" CodeBehind="GeraEventos.aspx.cs" Inherits="webREINF.GeraEventos" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>

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
        <h2>Conversor de Arquivos .xls</h2>
        <p>Geração de Eventos do Cliente selecionado</p>

        <div class="row">
            <div class="col-25">
                <asp:FileUpload ID="FileUpload2" CssClass="btn-danger" runat="server" />

            </div>
        </div>

        <asp:Button ID="Button2" CssClass="btn-danger" runat="server" Text="Gerar Evento" OnClick="Button2_Click" />


        <asp:Label ID="errorLabel" runat="server"></asp:Label>


        <br />
    </div>

    <div class="container">
        <asp:GridView ID="GridView2" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID do Envio" />
                <asp:BoundField DataField="nome_arquivo" HeaderText="Arquivo" />
                <asp:BoundField DataField="data_evento" HeaderText="Data do Evento" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/image/download.png" />
                <asp:ButtonField ButtonType="Image" CommandName="excluir" ImageUrl="~/image/delete.png" Text="Botão" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="erroGridLabel" runat="server"></asp:Label>
    </div>
    <br />
</asp:Content>
