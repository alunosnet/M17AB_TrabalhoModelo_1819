<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="areacliente.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.areacliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Área Cliente</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btEmprestimos" CssClass="btn btn-info"
            Text="Reservar" OnClick="btEmprestimos_Click" />
        <asp:Button runat="server" ID="btHistorico" CssClass="btn btn-info"
            Text="Histórico" OnClick="btHistorico_Click" />
    </div>
    <div id="divEmprestimo" runat="server">
        <h2>Reservar livros</h2>
        <asp:GridView CssClass="table" runat="server" ID="gvEmprestar" />
    </div>

    <div id="divHistorico" runat="server">
        <h2>Histórico</h2>
        <asp:GridView CssClass="table" runat="server" ID="gvHistorico" />
    </div>
</asp:Content>
