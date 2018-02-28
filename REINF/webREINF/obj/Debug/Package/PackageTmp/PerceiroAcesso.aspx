﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteParceiro.Master" AutoEventWireup="true" CodeBehind="PerceiroAcesso.aspx.cs" Inherits="webREINF.PerceiroAcesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/SiteParceiro.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    <div class="container">
        <div class="info">
            <p>
                <br />
                <strong>Info!</strong>
                <br />
                <asp:Label ID="infoLabel" runat="server" Text="Label"></asp:Label>
                <br />
            </p>
        </div>
        <h2>Cadastre uma nova senha!</h2>

    </div>
    -->
    <br />
    <div class="container">
        <h2>Configurando seu primeiro acesso!</h2>
        <p>Para começar, você vai precisar de uma nova senha.</p>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" CssClass="alert-warning" />
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-25">
                <label for="fname" class="col-auto">Nova Senha</label>
            </div>
            <div class="col-75">
                <asp:TextBox type="password" CssClass="txt" placeholder="Senha" ID="senhaTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="lname" class="col-auto">Confirmar Senha</label>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="As senhas não conferem!" ControlToCompare="senhaTextBox" ControlToValidate="senhaTextBox2" ForeColor="#CC3300" ValidationGroup="1">*</asp:CompareValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O Campo 'Senha' deve ter pelo menos 6 caracteres" ControlToValidate="senhaTextBox2" ValidationExpression=".{6,}" ForeColor="#CC3300" ValidationGroup="1">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="O Campo 'Senha' não pode estar em branco!" ControlToValidate="senhaTextBox2" ForeColor="#CC3300" ValidationGroup="1">*</asp:RequiredFieldValidator>
            </div>
            <div class="col-75">
                <asp:TextBox type="password" CssClass="txt" placeholder="Senha" ID="senhaTextBox2" runat="server"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="Button1" runat="server" Text="Cadastrar" OnClick="Button1_Click" ValidationGroup="1" />
            </div>
        </div>
    </div>
</asp:Content>