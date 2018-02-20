<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="UltimosEnvios.aspx.cs" Inherits="webREINF.UltimosEnvios" %>

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
        <h2>Arquivos Enviados - Geral</h2>
        <p>Arquivos enviados para processamento de parceiro</p>
    </div>

    <div class="container">
        <asp:GridView ID="GridView1" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID do Envio" />
                <asp:BoundField DataField="razao_social" HeaderText="Razão Social" />
                <asp:BoundField DataField="nome_arquivo" HeaderText="Arquivo" />
                <asp:BoundField DataField="data_evento" HeaderText="Data do Evento" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/image/download.png" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="erroGridLabel" runat="server"></asp:Label>
    </div>
    <br />
</asp:Content>
