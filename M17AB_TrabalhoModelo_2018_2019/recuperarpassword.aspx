<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="recuperarpassword.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.recuperarpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label for="tbPassword">Nova Password</label>
    <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" />
    <asp:Button runat="server" ID="btRecuperar" Text="Recuperar" OnClick="btRecuperar_Click" />
    <asp:Label runat="server" ID="lbErro" />
</asp:Content>
