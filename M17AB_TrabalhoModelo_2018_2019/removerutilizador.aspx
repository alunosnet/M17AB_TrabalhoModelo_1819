<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="removerutilizador.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.removerutilizador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/codigo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Apagar utilizador</h1>
    Nº: <asp:Label runat="server" ID="lbNUtilizador" /><br />
    Nome: <asp:Label runat="server" ID="lbNome" /><br />
    
    <asp:Label runat="server" ID="lbErro" /><br />
    <asp:Button CssClass="btn btn-danger" runat="server" ID="btRemover" Text="Remover" 
        OnClick="btRemover_Click" />
    <asp:Button CssClass="btn btn-info" runat="server" ID="btCancelar" Text="Voltar" 
         PostBackUrl="~/areaadmin.aspx" />
</asp:Content>
