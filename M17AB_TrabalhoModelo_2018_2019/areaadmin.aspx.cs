using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class areaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validar a sessão do utilizador
            if (Session["perfil"] == null ||
                Session["perfil"].ToString() != "0")
                Response.Redirect("index.aspx");

            if (!IsPostBack)
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
            //paginação da grelha livros
            gvLivros.AllowPaging = true;
            gvLivros.PageSize = 5;
            gvLivros.PageIndexChanging += GvLivros_PageIndexChanging;

            //paginação da grelha utilizadores
            gvUtilizadores.AllowPaging = true;
            gvUtilizadores.PageSize = 5;
            gvUtilizadores.PageIndexChanging += GvUtilizadores_PageIndexChanging;

            //botões de comando
            gvEmprestimos.RowCommand += GvEmprestimos_RowCommand;
        }

        private void GvEmprestimos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //linha
            int linha = int.Parse(e.CommandArgument as string);

            //id emprestimo
            int idemprestimo = int.Parse(gvEmprestimos.Rows[linha].Cells[2].Text);

            //comando
            if (e.CommandName == "alterar")
            {
                Emprestimo.alterarEstadoEmprestimo(idemprestimo);
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            }
            if (e.CommandName == "email")
            {
                DataTable dadosEmprestimo = Emprestimo.devolveDadosEmprestimo(idemprestimo);
                int idUtilizador = int.Parse(dadosEmprestimo.Rows[0]["idutilizador"].ToString());
                DataTable dadosUtilizador = Utilizador.devolveDadosUtilizador(idUtilizador);
                string email = dadosUtilizador.Rows[0]["email"].ToString();
                string mensagem = "Caro leitor deve devolver o livro que tem emprestado.";
                string assunto = "Livro emprestado";
                string password = ConfigurationManager.AppSettings["pwdEmail"].ToString();

                Helper.enviarMail("codemechaniccs@gmail.com", password, email, assunto, mensagem);
            }
        }

        private void GvUtilizadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUtilizadores.PageIndex = e.NewPageIndex;
            atualizaGrelhaUtilizadores();
        }

        private void GvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
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
                if (preco < 0 || preco > Decimal.Parse("99,99"))
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
                if (fuCapa.PostedFile.ContentLength == 0 ||
                    fuCapa.PostedFile.ContentLength > 5000000)
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

            ifCapa.DataImageUrlFormatString = "~/Images/{0}.jpg?" + aleatorio;
            ifCapa.DataImageUrlField = "nlivro";
            ifCapa.ControlStyle.Width = 100;
            gvLivros.Columns.Add(ifCapa);

            gvLivros.DataBind();
        }
        #endregion
        #region utilizadores
        protected void btUtilizadores_Click(object sender, EventArgs e)
        {
            EscondeDivs();

            //mostrar div utilizadores
            divUtilizadores.Visible = true;

            //css botões
            btUtilizadores.CssClass = "btn btn-info active";

            //cache
            Response.CacheControl = "no-cache";

            atualizaGrelhaUtilizadores();
        }
        protected void btUAdicionarUtilizador_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbUEmail.Text;
                if (email == String.Empty || email.Contains("@") == false)
                    throw new Exception("O email indicado não é válido.");

                string nome = tbUNome.Text;
                if (nome == String.Empty || nome.Trim().Length < 2)
                    throw new Exception("O nome indicado não é válido.");

                string morada = tbUMorada.Text;
                if (morada == String.Empty || morada.Trim().Length < 2)
                    throw new Exception("A morada indicada não é válida.");

                string nif = tbUNif.Text;
                int z = int.Parse(nif);
                if (nif.Length != 9)
                    throw new Exception("O nif tem de ter 9 algarismos.");

                string password = tbUPassword.Text;
                if (password.Trim().Length < 5)
                    throw new Exception("A password tem de ter 5 letras");
                //validar perfil
                int perfil = int.Parse(ddPerfil.SelectedValue);
                if (perfil < 0 || perfil > 1)
                    throw new Exception("Perfil inválido");
                //adicionar à bd
                Utilizador.registar(email, nome, morada, nif, password, perfil);

                //mensagem ao utilizador
                lbErro.Text = "Utilizador adicionado com sucesso.";
                lbErro.CssClass = "info";

                //limpar form
                tbUEmail.Text = "";
                tbUMorada.Text = "";
                tbUNif.Text = "";
                tbUNome.Text = "";
                tbUPassword.Text = "";
                ddPerfil.SelectedIndex = 0;

                //atualiza grelha
                atualizaGrelhaUtilizadores();

            }
            catch (Exception erro)
            {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert";
            }
        }
        private void atualizaGrelhaUtilizadores()
        {
            // limpar gridview
            gvUtilizadores.Columns.Clear();
            gvUtilizadores.DataSource = null;
            gvUtilizadores.DataBind();

            DataTable dados = Utilizador.listaTodosUtilizadores();

            if (dados == null || dados.Rows.Count == 0) return;

            //configurar as colunas da gridview e datatable
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            DataColumn dcBloquear = new DataColumn();
            dcBloquear.ColumnName = "Bloquear";
            dcBloquear.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcBloquear);

            DataColumn dcHistorico = new DataColumn();
            dcHistorico.ColumnName = "Historico";
            dcHistorico.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcHistorico);

            //associar o datatable à grid
            gvUtilizadores.DataSource = dados;
            //desativar a geração automatica das colunas
            gvUtilizadores.AutoGenerateColumns = false;

            //gridview
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover"; //título da coluna
            hlRemover.DataTextField = "Remover";    //campo associado
            hlRemover.Text = "Remover Utilizador";   //texto clicavel
            //criar um link removerlivro.aspx?nlivro=1
            hlRemover.DataNavigateUrlFormatString = "removerutilizador.aspx?id={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlRemover);
            //coluna editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar"; //título da coluna
            hlEditar.DataTextField = "Editar";    //campo associado
            hlEditar.Text = "Editar Utilizador";   //texto clicavel
            //criar um link editarlivro.aspx?nlivro=1
            hlEditar.DataNavigateUrlFormatString = "editarutilizador.aspx?id={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlEditar);

            //coluna para bloquear/desbloquear
            HyperLinkField hlBloquear = new HyperLinkField();
            hlBloquear.HeaderText = "Bloquear/Desbloquear"; //título da coluna
            hlBloquear.DataTextField = "Bloquear";    //campo associado
            hlBloquear.Text = "Bloquear/Desbloquear";   //texto clicavel
            hlBloquear.DataNavigateUrlFormatString = "bloquearutilizador.aspx?id={0}";
            hlBloquear.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlBloquear);
            //histórico
            HyperLinkField hlHistorico = new HyperLinkField();
            hlHistorico.HeaderText = "Histórico"; //título da coluna
            hlHistorico.DataTextField = "Historico";    //campo associado
            hlHistorico.Text = "Ver histórico";   //texto clicavel
            hlHistorico.DataNavigateUrlFormatString = "historico.aspx?id={0}";
            hlHistorico.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlHistorico);

            //restantes colunas
            //id
            BoundField bfId = new BoundField();
            bfId.HeaderText = "Id";
            bfId.DataField = "id";
            gvUtilizadores.Columns.Add(bfId);
            //email
            BoundField bfEmail = new BoundField();
            bfEmail.HeaderText = "Email";
            bfEmail.DataField = "email";
            gvUtilizadores.Columns.Add(bfEmail);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvUtilizadores.Columns.Add(bfNome);
            //morada
            BoundField bfMorada = new BoundField();
            bfMorada.HeaderText = "Morada";
            bfMorada.DataField = "morada";
            gvUtilizadores.Columns.Add(bfMorada);
            //nif
            BoundField bfNif = new BoundField();
            bfNif.HeaderText = "Nif";
            bfNif.DataField = "nif";
            gvUtilizadores.Columns.Add(bfNif);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvUtilizadores.Columns.Add(bfEstado);
            //perfil
            BoundField bfPerfil = new BoundField();
            bfPerfil.HeaderText = "Perfil";
            bfPerfil.DataField = "perfil";
            gvUtilizadores.Columns.Add(bfPerfil);

            //associar o datatable à gridview
            gvUtilizadores.DataBind();

        }
        #endregion
        #region emprestimos

        private void atualizaDDLivros()
        {
            ddLivro.Items.Clear();
            DataTable dados = Livro.listaLivrosDisponiveis();
            foreach (DataRow livro in dados.Rows)
            {
                ddLivro.Items.Add(
                    new ListItem(livro[1].ToString(),
                    livro[0].ToString()
                    ));
            }
        }
        private void atualizaDDLeitores()
        {
            ddUtilizador.Items.Clear();
            DataTable dados = Utilizador.listaTodosUtilizadores();
            foreach (DataRow leitor in dados.Rows)
            {
                ddUtilizador.Items.Add(new ListItem(
                    leitor["nome"].ToString(),
                    leitor["id"].ToString()
                    ));
            }
        }
        protected void btEmprestimos_Click(object sender, EventArgs e)
        {
            EscondeDivs();

            //mostrar div emprestimos
            divEmprestimos.Visible = true;

            //css botões
            btEmprestimos.CssClass = "btn btn-info active";

            //cache
            Response.CacheControl = "no-cache";

            atualizaGrelhaEmprestimos();
            atualizaDDLeitores();
            atualizaDDLivros();
        }

        private void atualizaGrelhaEmprestimos()
        {
            gvEmprestimos.Columns.Clear();
            gvEmprestimos.DataSource = null;
            gvEmprestimos.DataBind();

            DataTable dados;

            if (cbEmprestimos.Checked)
                dados = Emprestimo.listaTodosEmprestimosPorConcluirComNomes();     //emprestimos por concluir
            else
                dados = Emprestimo.listaTodosEmprestimosComNomes();      //todos emprestimos

            if (dados == null || dados.Rows.Count == 0) return;

            //alterar estado livro
            ButtonField btEstado = new ButtonField();
            btEstado.HeaderText = "Definir Estado";
            btEstado.Text = "Alterar";
            btEstado.ButtonType = ButtonType.Button;
            btEstado.CommandName = "alterar";
            gvEmprestimos.Columns.Add(btEstado);

            //enviar email
            ButtonField btEmail = new ButtonField();
            btEmail.HeaderText = "Email";
            btEmail.Text = "Email";
            btEmail.ButtonType = ButtonType.Button;
            btEmail.CommandName = "email";
            gvEmprestimos.Columns.Add(btEmail);

            gvEmprestimos.DataSource = dados;
            gvEmprestimos.AutoGenerateColumns = true;
            gvEmprestimos.DataBind();

        }

        protected void cbEmprestimos_CheckedChanged(object sender, EventArgs e)
        {
            atualizaGrelhaEmprestimos();
        }

        protected void btEAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                int idLeitor = int.Parse(ddUtilizador.SelectedValue);
                int idLivro = int.Parse(ddLivro.SelectedValue);
                DateTime dataDevolve = DataDevolve.SelectedDate;
                Emprestimo.adicionarEmprestimo(idLivro, idLeitor, dataDevolve);
                //atualizar grelha
                atualizaGrelhaEmprestimos();
                //atualizar DD
                atualizaDDLivros();
                atualizaDDLeitores();
            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "bg-danger";
            }
        }
        #endregion
        #region consultas
        protected void btConsultas_Click(object sender, EventArgs e)
        {
            EscondeDivs();

            //mostrar div emprestimos
            divConsultas.Visible = true;

            //css botões
            btConsultas.CssClass = "btn btn-info active";

            //cache
            Response.CacheControl = "no-cache";

            atualizaGrelhaConsultas();
        }
        protected void ddConsula_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizaGrelhaConsultas();
        }
        private void atualizaGrelhaConsultas()
        {
            gvConsultas.Columns.Clear();
            int iconsulta = int.Parse(ddConsula.SelectedValue);
            DataTable dados;
            string sql = "";
            switch (iconsulta)
            {
                case 0:
                    sql = @"select nome,count(nlivro) as [nr emprestimos] from utilizadores inner join emprestimos
                                on utilizadores.id=emprestimos.idutilizador
                                group by utilizadores.id,nome
                                order by [nr emprestimos] DESC";
                    break;
                case 1:
                    sql = @"select nome,count(*) as [nr emprestimos] from livros inner join emprestimos
                                on livros.nlivro=emprestimos.nlivro
                                group by livros.nlivro,nome
                                order by [nr emprestimos] DESC";
                    break;
                case 2:
                    sql = @"select emprestimos.*,utilizadores.nome as Leitor,utilizadores.email,livros.nome as Livro
                                from emprestimos
                                inner join utilizadores on utilizadores.id=emprestimos.idutilizador
                                inner join livros on livros.nlivro=emprestimos.nlivro
                                where emprestimos.estado=1 and data_devolve<getdate()
                                order by emprestimos.data_devolve 
                                ";
                    break;
                case 3:
                    sql = @"select nome,data_aquisicao from livros
                                where DATEDIFF(day, data_aquisicao, getdate())< 7";
                    break;
                case 4:
                    sql = @"select avg(datediff(day,data_emprestimo,data_devolve)) from emprestimos";
                    break;
                case 5:
                    sql = @"select nome from utilizadores inner join emprestimos 
                            on emprestimos.idutilizador=utilizadores.id
                            where emprestimos.nlivro=(select top 1 nlivro from livros order by preco desc)";
                    break;
            }
            dados = BaseDados.Instance.devolveSQL(sql);
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }
        #endregion




    }
}