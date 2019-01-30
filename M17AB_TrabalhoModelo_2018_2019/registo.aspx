<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="registo.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.registo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Email-->
    <div class="form-group">
        <label for="tbEmail">Email:</label>
        <asp:TextBox runat="server" ID="tbEmail" 
            CssClass="form-control" TextMode="Email" />
    </div>
    <!--Nome-->
    <div class="form-group">
        <label for="tbNome">Nome:</label>
        <asp:TextBox runat="server" ID="tbNome" 
            CssClass="form-control"  />
    </div>
    <!--Morada-->
     <div class="form-group">
        <label for="tbMorada">Morada:</label>
        <asp:TextBox runat="server" ID="tbMorada" 
            CssClass="form-control"  />
    </div>
    <!--nif-->
     <div class="form-group">
        <label for="tbNif">NIF:</label>
        <asp:TextBox runat="server" ID="tbNif" 
            CssClass="form-control"  />
    </div>
    <!--password-->
     <div class="form-group">
        <label for="tbPassword">Password:</label>
        <asp:TextBox runat="server" ID="tbPassword" 
            CssClass="form-control" TextMode="Password" />
    </div>
    <!--Erro-->
    <asp:Label ID="lbErro" runat="server" />
    <!--Recaptcha-->
    <div class="g-recaptcha" data-sitekey="6Lc1vvoSAAAAAFjyIsG88_b-SoYcW5n89amtzucB"></div>
    <!--Registar-->
    <asp:Button runat="server" ID="btRegistar" Text="Registar"
        CssClass="btn btn-info" OnClick="btRegistar_Click" />
</asp:Content>
