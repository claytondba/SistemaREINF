<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="ConsultaClientes.aspx.cs" Inherits="webREINF.ConsultaClientes" %>

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
        <h2>Consulta de Clientes</h2>
        <p>Consulta de clientes cadastrados pelo parceiro.</p>
    </div>
    <br />
    <div class="container">
        <asp:GridView ID="GridView1" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="login" HeaderText="Login" />
                <asp:BoundField DataField="razao_social" HeaderText="Razão Social" />
                <asp:BoundField DataField="cnpj" DataFormatString="{0:99.999.999/9999-99}" HeaderText="CNPJ" />
                <asp:BoundField DataField="nome" HeaderText="Contato" />
                <asp:BoundField DataField="telefone" HeaderText="Telefone" />
                <asp:BoundField DataField="data_cadastro" HeaderText="Data Cadastro" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/image/pencil16.png" />
                <asp:ButtonField ButtonType="Image" CommandName="excluir" ImageUrl="~/image/delete.png" Text="Botão" />
                <asp:ButtonField ButtonType="Image" CommandName="download" ImageUrl="~/image/download.png" />                
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
