<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historico.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.historico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="areaadmin.aspx">Voltar</a>
    <asp:GridView runat="server" ID="gvHistorico" CssClass="table"></asp:GridView>
</asp:Content>
