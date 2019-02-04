using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class areaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: validar a sessão do utilizador

            if(!IsPostBack)
                EscondeDivs();

            ConfigurarGrids();
        }
        void EscondeDivs()
        {
            divLivros.Visible = false;
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
        }
        void ConfigurarGrids()
        {
            //TODO:
        }
        #region livros
        protected void btLivros_Click(object sender, EventArgs e)
        {
            EscondeDivs();

            //mostrar div livros
            divLivros.Visible = true;

            //css botões
            btLivros.CssClass = "btn btn-info active";

            //cache
            Response.CacheControl = "no-cache";

            atualizaGrelhaLivros();
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e)
        {
            try
            {
                //validar form
                //nome
                string nome = tbNome.Text;
                if (nome == String.Empty || nome.Length > 100)
                    throw new Exception("O nome tem de ter pelo menos 1 letra e no máximo 100.");
                //ano
                int ano = int.Parse(tbAno.Text);
                if (ano < 0 || ano > DateTime.Now.Year)
                    throw new Exception("O ano tem de ser superior a 0 e inferior ao ano atual.");
                //data aquisição
                DateTime data;
                data = DateTime.Parse(tbData.Text);
                if (data > DateTime.Now)
                    throw new Exception("A data de aquisição tem de ser inferior ou igual à data de hoje.");
                //preço
                Decimal preco = Decimal.Parse(tbPreco.Text);
                if (preco < 0 || preco>Decimal.Parse("99,99"))
                    throw new Exception("O preço tem de ser superior ou igual a zero e inferior a 100.");
                //autor
                String autor = tbAutor.Text;
                if (autor == String.Empty || autor.Length > 100)
                    throw new Exception("O nome do autor tem de ter pelo menos 1 letra e no máximo 100.");
                //tipo
                String tipo = ddTipos.Text;
                if (tipo == String.Empty || tipo.Length > 50)
                    throw new Exception("Selecione o tipo de livro.");
                //capa
                if (fuCapa.HasFile == false)
                    throw new Exception("Tem de escolher uma capa para o livro.");
                if(fuCapa.PostedFile.ContentLength==0 ||
                    fuCapa.PostedFile.ContentLength>5000000)
                    throw new Exception("O ficheiro deve ter entre 1 e 5MB.");
                if (fuCapa.PostedFile.ContentType != "image/jpeg" &&
                    fuCapa.PostedFile.ContentType != "image/png" &&
                    fuCapa.PostedFile.ContentType != "image/jpg")
                    throw new Exception("Só pode enviar jpeg e png.");

                //adicionar livro
                int id = Livro.adicionarLivro(nome, ano, data, preco, autor, tipo);
                //copiar capa livro
                string ficheiro = Server.MapPath(@"~\Images\");
                ficheiro += id + ".jpg";
                fuCapa.SaveAs(ficheiro);
                //atualizar grelha livros
                atualizaGrelhaLivros();
                //limpar o form
                tbAno.Text = "";
                tbAutor.Text = "";
                tbData.Text = "";
                tbNome.Text = "";
                tbPreco.Text = "";
                
            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "bg-danger";
            }
        }
        private void atualizaGrelhaLivros()
        {
            //gvLivros.DataSource = Livro.listaLivros();
            //gvLivros.DataBind();
            //limpar grid
            gvLivros.Columns.Clear();
            gvLivros.DataSource = null;
            gvLivros.DataBind();

            //consulta bd
            DataTable dados = Livro.listaLivros();

            //adicionar duas (remover / editar)
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            gvLivros.DataSource = dados;
            gvLivros.AutoGenerateColumns = false;

            //configurar as colunas da gridview
            //coluna remover
            HyperLinkField hlRemover = new HyperLinkField();
            //título da coluna
            hlRemover.HeaderText = "Remover";
            //campo associado no datatable
            hlRemover.DataTextField = "Remover";
            //texto clicavel
            hlRemover.Text = "Remover";
            //criar um link removerlivro.aspx?id=1
            hlRemover.DataNavigateUrlFormatString = "removerlivro.aspx?id={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "nlivro" };
            //adicionar a gridview
            gvLivros.Columns.Add(hlRemover);

            //coluna editar
            HyperLinkField hlEditar = new HyperLinkField();
            //título da coluna
            hlEditar.HeaderText = "Editar";
            //campo associado no datatable
            hlEditar.DataTextField = "Editar";
            //texto clicavel
            hlEditar.Text = "Editar";
            //criar um link editarlivro.aspx?id=1
            hlEditar.DataNavigateUrlFormatString = "editarlivro.aspx?id={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nlivro" };
            //adicionar a gridview
            gvLivros.Columns.Add(hlEditar);

            //nlivro
            BoundField bfNlivro = new BoundField();
            bfNlivro.HeaderText = "Nº Livro";
            bfNlivro.DataField = "nlivro";
            gvLivros.Columns.Add(bfNlivro);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvLivros.Columns.Add(bfNome);

            //ano
            BoundField bfAno = new BoundField();
            bfAno.HeaderText = "Ano";
            bfAno.DataField = "ano";
            gvLivros.Columns.Add(bfAno);

            //data aquisição
            BoundField bfData = new BoundField();
            bfData.HeaderText = "Data Aquisição";
            bfData.DataField = "data_aquisicao";
            bfData.DataFormatString = "{0:dd-MM-yyyy}";
            gvLivros.Columns.Add(bfData);
            //preço
            BoundField bfPreco = new BoundField();
            bfPreco.HeaderText = "Preço";
            bfPreco.DataField = "preco";
            bfPreco.DataFormatString = "{0:C}";
            gvLivros.Columns.Add(bfPreco);
            //autor
            BoundField bfAutor = new BoundField();
            bfAutor.HeaderText = "Autor";
            bfAutor.DataField = "autor";
            gvLivros.Columns.Add(bfAutor);
            //tipo
            BoundField bfTipo = new BoundField();
            bfTipo.HeaderText = "Tipo";
            bfTipo.DataField = "tipo";
            gvLivros.Columns.Add(bfTipo);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvLivros.Columns.Add(bfEstado);
            //capa
            ImageField ifCapa = new ImageField();
            ifCapa.HeaderText = "Capa";
            //para evitar cache das imagens
            int aleatorio = new Random().Next(999999);

            ifCapa.DataImageUrlFormatString = "~/Images/{0}.jpg?"+aleatorio;
            ifCapa.DataImageUrlField = "nlivro";
            ifCapa.ControlStyle.Width = 100;
            gvLivros.Columns.Add(ifCapa);

            gvLivros.DataBind();
        }
        #endregion
        #region utilizadores
        protected void btUtilizadores_Click(object sender, EventArgs e)
        {

        }
        protected void btUAdicionarUtilizador_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region emprestimos
        protected void btEmprestimos_Click(object sender, EventArgs e)
        {

        }
        protected void cbEmprestimos_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btEAdicionar_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region consultas
        protected void btConsultas_Click(object sender, EventArgs e)
        {

        }
        protected void ddConsula_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion




    }
}