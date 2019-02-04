<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="M17AB_TrabalhoModelo_2018_2019.areaadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área do administrador</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btLivros" Text="Livros" 
            CssClass="btn btn-info" OnClick="btLivros_Click" />
        <asp:Button runat="server" ID="btUtilizadores" Text="Utilizadores" 
            CssClass="btn btn-info" OnClick="btUtilizadores_Click" />
        <asp:Button runat="server" ID="btEmprestimos" Text="Emprestimos" 
            CssClass="btn btn-info" OnClick="btEmprestimos_Click" />
        <asp:Button runat="server" ID="btConsultas" Text="Consultas" 
            CssClass="btn btn-info" OnClick="btConsultas_Click" />
    </div>
        <!--Livros-->
    <div id="divLivros" runat="server">
        <h2>Livros</h2>
        <asp:GridView ID="gvLivros" runat="server" CssClass="table table-responsive"></asp:GridView>
        <h3>Adicionar</h3>
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
        <!--capa-->
        <div class="form-group">
            <label for="fuCapa">Capa:</label>
            <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control btn btn-default" />
        </div>
        
        <asp:Button runat="server" ID="btAdicionarLivro" Text="Adicionar"
            CssClass="btn btn-info" OnClick="btAdicionarLivro_Click" />
    </div>
    <!--Livros-->
    <!--utilizadores-->
    <div id="divUtilizadores" runat="server">
        <h2>Utilizadores</h2>
        <asp:GridView CssClass="table table-responsive" runat="server" ID="gvUtilizadores" />
        <h3>Adicionar</h3>
        <!--Email-->
        <div class="form-group">
            <label for="tbUEmail">Email:</label>
            <asp:TextBox runat="server" ID="tbUEmail" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Nome-->
        <div class="form-group">
            <label for="tbUNome">Nome:</label>
            <asp:TextBox runat="server" ID="tbUNome" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Morada-->
        <div class="form-group">
            <label for="tbUMorada">Morada:</label>
            <asp:TextBox runat="server" ID="tbUMorada" CssClass="form-control"></asp:TextBox>
        </div>
        <!--nif-->
        <div class="form-group">
            <label for="tbUNif">NIF:</label>
            <asp:TextBox runat="server" ID="tbUNif" CssClass="form-control"></asp:TextBox>
        </div>
        <!--password-->
        <div class="form-group">
            <label for="tbUPassword">Password:</label>
            <asp:TextBox TextMode="Password" runat="server" ID="tbUPassword" CssClass="form-control"></asp:TextBox>
        </div>
        <!--perfil-->
        <div class="form-group">
            <label for="ddPerfil">Perfil</label>
            <asp:DropDownList runat="server" ID="ddPerfil" CssClass="form-control">
                <asp:ListItem Value="0">Administrador</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">Leitor</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button CssClass="btn btn-danger" runat="server" ID="btUAdicionarUtilizador" Text="Adicionar" OnClick="btUAdicionarUtilizador_Click" />
    </div>
    <!--utilizadores-->
    <!--Empréstimos-->
    <div id="divEmprestimos" runat="server">
        <h2>Empréstimos</h2>
        Só listar empréstimos por concluir:<asp:CheckBox runat="server" ID="cbEmprestimos" AutoPostBack="true" OnCheckedChanged="cbEmprestimos_CheckedChanged" />
        <asp:GridView runat="server" ID="gvEmprestimos" CssClass="table table-responsive" />
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="ddLivro">Livro:</label>
            <asp:DropDownList runat="server" ID="ddLivro" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="ddUtilizador">Leitor:</label>
            <asp:DropDownList runat="server" ID="ddUtilizador" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="DataDevolve">Data de Devolução:</label>
            <asp:Calendar runat="server" ID="DataDevolve"></asp:Calendar>
        </div>
        <asp:Button runat="server" ID="btEAdicionar" Text="Adicionar" CssClass="btn btn-danger" OnClick="btEAdicionar_Click" />
    </div>
    <!--Empréstimos-->
    <!--Consultas-->
    <div id="divConsultas" runat="server">
        <h3>Consultas</h3>
        <br />
        <asp:DropDownList runat="server" ID="ddConsula" AutoPostBack="true"
            CssClass="form-control" OnSelectedIndexChanged="ddConsula_SelectedIndexChanged">
            <asp:ListItem Value="0">Top de Leitores</asp:ListItem>
            <asp:ListItem Value="1">Top de Livros</asp:ListItem>
            <asp:ListItem Value="2">Empréstimo fora de prazo</asp:ListItem>
            <asp:ListItem Value="3">Livros da última semana</asp:ListItem>
            <asp:ListItem Value="4">Tempo médio de empréstimo</asp:ListItem>
            <asp:ListItem Value="5">Leitores que levaram livro mais caro</asp:ListItem>
        </asp:DropDownList>
        <asp:GridView runat="server" ID="gvConsultas" CssClass="table table-responsive"></asp:GridView>
    </div>
    <!--Consultas-->
    <!--erros-->
    <asp:Label runat="server" ID="lbErro"></asp:Label>
</asp:Content>
