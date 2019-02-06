<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="editarlivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.editarlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/codigo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!--Nlivro-->
        <asp:Label runat="server" ID="lbNlivro" /><br />
        <!--Nome-->
        <div class="form-group">
            <label for="tbNome">Nome:</label>
            <asp:TextBox runat="server" ID="tbNome" CssClass="form-control"></asp:TextBox>
        </div>
        <!--ano-->
        <div class="form-group">
            <label for="tbAno">Ano:</label>
            <asp:TextBox runat="server" ID="tbAno" CssClass="form-control"></asp:TextBox>
        </div>
        <!--data aquisição-->
        <div class="form-group">
            <label for="tbData">Data de Aquisição:</label>
            <asp:TextBox runat="server" ID="tbData" CssClass="form-control"></asp:TextBox>
        </div>
        <!--preço-->
        <div class="form-group">
            <label for="tbPreco">Preço:</label>
            <asp:TextBox runat="server" ID="tbPreco" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Autor-->
        <div class="form-group">
            <label for="tbAutor">Autor:</label>
            <asp:TextBox runat="server" ID="tbAutor" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Tipo-->
        <div class="form-group">
            <label for="ddTipos">Tipo:</label>
            <asp:DropDownList runat="server" ID="ddTipos" CssClass="form-control">
                <asp:ListItem Value="Romance">Romance</asp:ListItem>
                <asp:ListItem Value="Policial">Policial</asp:ListItem>
                <asp:ListItem Value="BD">BD</asp:ListItem>
                <asp:ListItem Value="Outro">Outro</asp:ListItem>
            </asp:DropDownList>
        </div>
        <!--Mostra capa atual-->
        <asp:Image runat="server" ID="imgCapa" />
        <!--capa-->
        <div class="form-group">
            <label for="fuCapa">Capa:</label>
            <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control btn btn-default" />
        </div>
        <asp:Label runat="server" ID="lbErro" />
        <asp:Button runat="server" ID="btAtualizar" Text="Atualizar"
                CssClass="btn btn-danger" OnClick="btAtualizar_Click" />
        <asp:Button runat="server" ID="btVoltar" Text="Voltar"
            CssClass="btn btn-info" PostBackUrl="~/areaadmin.aspx" />
</asp:Content>
