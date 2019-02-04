<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="removerlivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.removerlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function returnMain() {
            window.setTimeout(
                function () {
                    window.location.href = 'areaadmin.aspx';
                },3000
            );
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Apagar livro</h1>
    Nº Livro: <asp:Label runat="server" ID="lbNLivro" /><br />
    Nome: <asp:Label runat="server" ID="lbNome" /><br />
    Capa: <asp:Image runat="server" Width="100" ID="imgCapa" /><br />
    <asp:Label runat="server" ID="lbErro" /><br />
    <asp:Button CssClass="btn btn-danger" runat="server" ID="btRemover" Text="Remover" OnClick="btRemover_Click" />
    <asp:Button CssClass="btn btn-info" runat="server" ID="btCancelar" Text="Voltar" 
         PostBackUrl="~/areaadmin.aspx" />
</asp:Content>
