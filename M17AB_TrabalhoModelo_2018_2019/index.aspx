<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Login-->
    <div id="divLogin" runat="server" class="float-right col-sm-4 table-bordered divLogin">
        Email:<asp:TextBox runat="server" ID="tbEmail" TextMode="Email" CssClass="form-control" />
        Password:<asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control" />
        <asp:Button runat="server" ID="btLogin" Text="Login" 
            CssClass="btn" OnClick="btLogin_Click" />
        <asp:Button runat="server" ID="btRecuperar" Text="Recuperar password" 
            CssClass="btn btn-danger" OnClick="btRecuperar_Click" />
        <br /><asp:Label runat="server" ID="lbErro"></asp:Label>
    </div>
    <!--Pesquisa-->
    <div class="float-left col-sm-8 input-group">
        <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button runat="server" ID="btPesquisa" Text="Pesquisar"
                 CssClass="btn btn-info" OnClick="btPesquisa_Click" />
        </span>
    </div>
    <!--Lista dos livros-->
    <div id="divLivros" runat="server" class="float-left col-md-8"></div>
</asp:Content>
