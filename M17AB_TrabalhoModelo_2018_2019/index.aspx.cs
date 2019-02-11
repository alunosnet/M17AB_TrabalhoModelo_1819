using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] != null)
                divLogin.Visible = false;

            //ordenar resultados
            int? ordena = 0;
            try {
                ordena = int.Parse(Request["ordena"].ToString());
            }
            catch
            {
                ordena = null;
            }



            //listar os livros disponíveis
            atualizaGrelhaLivros(null,ordena);

        }

        private void atualizaGrelhaLivros(string pesquisa=null,int? ordena=null)
        {
            string sql = "";
            DataTable dados;
            //listar todos os livros disponiveis
            if (pesquisa == null)
            {
                //se existir o cookie ultimolivro listar os livros do mesmo autor
                HttpCookie cookieAutor = Request.Cookies["ultimolivro"];
                if (cookieAutor == null)
                    dados = Livro.listaLivrosDisponiveis(ordena);
                else
                    dados = Livro.listaLivrosDoAutor(Server.UrlDecode(cookieAutor.Value));
            }
            else
                dados = Livro.listaLivrosDisponiveis(pesquisa, ordena);

            GerarIndex(dados);
        }

        private void GerarIndex(DataTable dados)
        {
            if(dados==null || dados.Rows.Count == 0)
            {
                divLivros.InnerHtml = "";
                return;
            }
            string grelha = "<div class='container-fluid'>";
            grelha += "<div class='row'>";
            foreach (DataRow livro in dados.Rows)
            {
                grelha += "<div class='col'>";
                grelha += "<img src='/Images/" + livro[0].ToString() + 
                    ".jpg' class='img-fluid'/>";
                grelha += "<p/><span class='stat-title'>"+livro[1].ToString()
                    + "</span>";
                grelha += "<span class='stat-title'>" + 
                    String.Format(" | {0:C}",Decimal.Parse(livro["preco"].ToString()))
                    + "</span>";
                grelha += "<br/><a href='detalheslivro.aspx?id=" + livro[0].ToString()
                    + "'>Detalhes</a>";
                grelha += "</div>";
            }

            grelha += "</div></div>";
            divLivros.InnerHtml = grelha;
        }

        protected void btPesquisa_Click(object sender, EventArgs e)
        {
            atualizaGrelhaLivros(tbPesquisa.Text);
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string password = tbPassword.Text;
                //validar login
                DataTable utilizador = Login.verificaLogin(email, password);
                if (utilizador == null)
                    throw new Exception("Login falhou");

                //guardar dados de sessão

                //nome
                Session["nome"] = utilizador.Rows[0]["nome"].ToString();
                //id
                Session["id"] = utilizador.Rows[0]["id"].ToString();
                //perfil
                Session["perfil"] = utilizador.Rows[0]["perfil"].ToString();

                //redirecionar
                if (Session["perfil"].ToString() == "0")
                    Response.Redirect("areaadmin.aspx");
                else
                    Response.Redirect("areacliente.aspx");
            }
            catch(Exception erro)
            {
                lbErro.Text = "Login falhou. Tente novamente.";//+erro.Message;
                lbErro.CssClass = "alert danger";
            }
        }
        //TODO: recuperar password
        protected void btRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbEmail.Text == String.Empty)
                    throw new Exception("Tem de indicar um email");
                //verificar se email existe
                string email = tbEmail.Text;
                DataTable dados = Utilizador.devolveDadosUtilizador(email);
                if(dados!=null && dados.Rows.Count == 1)
                {
                    Guid g = Guid.NewGuid();

                    Utilizador.recuperarPassword(email, g.ToString());
                    string mensagem = "Clique no link para recuperar a sua password.\n";
                    mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarpassword.aspx?id=";
                    mensagem += Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";
                    string senha = ConfigurationManager.AppSettings["pwdEmail"].ToString();
                    Helper.enviarMail("codemechaniccs@gmail.com", senha, email,
                        "Recuperação de password", mensagem);
                    lbErro.Text = "Foi enviado um email.";

                }
            }catch(Exception erro)
            {
                lbErro.Text = erro.Message;
            }
        }
    }
}