<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCliente.Master" AutoEventWireup="true" CodeBehind="RelatoriosClientes.aspx.cs" Inherits="webREINF.RelatoriosClientes" %>

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
        <h2>Relatórios</h2>
        <p>Relatórios gerenciais.</p>
    </div>
    <br />
    <div class="container">
        <p>Relatório de ocorrências processadas pelo parceiro.</p>
        <asp:GridView ID="GridView1" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" >
            <Columns>
                <asp:BoundField DataField="cnpj" HeaderText="CNPJ" />
                <asp:BoundField DataField="razaosocial" HeaderText="Razão" />
                <asp:BoundField DataField="codigo" HeaderText="Código" />
                <asp:BoundField DataField="descricao" HeaderText="Descrição" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
