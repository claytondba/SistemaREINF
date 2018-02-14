<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCliente.Master" AutoEventWireup="true" CodeBehind="ArquivosClientes.aspx.cs" Inherits="webREINF.ArquivosClientes" %>

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
        <h2>Upload de Arquivos .xls</h2>
        <p>Upload para validação em parceiros credenciados</p>

        <div class="row">
            <div class="col-25">
                <asp:FileUpload ID="FileUpload1" CssClass="input-group" runat="server" />

            </div>
        </div>
        <div>
            <div class="col-75">
                <asp:Button ID="Button1" runat="server" Text="Enviar Arquivo" OnClick="Button1_Click" />
                <asp:Label ID="errorLabel" runat="server"></asp:Label>
            </div>
        </div>

        <br />
    </div>

    <div class="container">
        <asp:GridView ID="GridView1" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID do Envio" />
                <asp:BoundField DataField="data_evento" HeaderText="Data do Evento" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/image/pencil16.png" />
                <asp:ButtonField ButtonType="Image" CommandName="excluir" ImageUrl="~/image/delete.png" Text="Botão" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="erroGridLabel" runat="server"></asp:Label>
    </div>
    <br />
</asp:Content>
