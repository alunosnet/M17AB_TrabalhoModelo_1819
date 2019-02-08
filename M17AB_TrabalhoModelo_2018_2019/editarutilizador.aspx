<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="editarutilizador.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.editarutilizador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/codigo.js"></script>
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
    <!--Erro-->
    <asp:Label ID="lbErro" runat="server" />
    <asp:Button runat="server" ID="btVoltar" Text="Voltar" 
        CssClass="btn btn-info" PostBackUrl="~/areaadmin.aspx" />
    <asp:Button runat="server" ID="btAtualizar" Text="Atualizar" 
        CssClass="btn btn-danger" OnClick="btAtualizar_Click" />
</asp:Content>
