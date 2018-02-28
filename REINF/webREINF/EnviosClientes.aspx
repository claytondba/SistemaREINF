<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="EnviosClientes.aspx.cs" Inherits="webREINF.EnviosClientes" %>

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
        <h2>Arquivos Enviados -
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h2>
        <p>Arquivos enviados para processamento de parceiro</p>
    </div>

    <br />
    <div class="container">

        <asp:Button ID="Button1" CssClass="btn-danger" runat="server" Text="Gerar Evento" OnClick="Button1_Click" />
        

        <div class="row">
            <div class="col-25">
                <asp:Label ID="erroGridLabel" runat="server"></asp:Label>
            </div>

        </div>
        <br /> 
        <br /> 
      
        <asp:GridView ID="GridView1" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID do Envio" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox DataField="selecionados" ID="checks" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nome_arquivo" HeaderText="Arquivo" />
                <asp:BoundField DataField="data_evento" HeaderText="Data do Evento" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/image/download.png" />
                <%--<asp:ButtonField ButtonType="Image" CommandName="seleciona" ImageUrl="~/image/seleciona16.png" Text="Botão" />--%>
            </Columns>
        </asp:GridView>


    </div>
    <br />
</asp:Content>
