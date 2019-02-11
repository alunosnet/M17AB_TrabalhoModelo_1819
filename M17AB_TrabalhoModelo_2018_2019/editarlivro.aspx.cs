using System;
using System.Data;
using System.Web.UI;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class editarlivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null ||
                Session["perfil"].ToString() != "0")
                Response.Redirect("index.aspx");

            //testar postback
            if (IsPostBack) return;
            //carregar os dados do livro a remover
            try
            {
                int nlivro = int.Parse(Request["id"].ToString());
                DataTable dados = Livro.devolveDadosLivro(nlivro);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Livro não foi encontrado.");

                lbNlivro.Text = dados.Rows[0]["nlivro"].ToString();
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbAno.Text = dados.Rows[0]["ano"].ToString();
                tbAutor.Text = dados.Rows[0]["autor"].ToString();
                tbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"]
                        .ToString()).ToShortDateString();
                tbPreco.Text = dados.Rows[0]["preco"].ToString();
                ddTipos.Text = dados.Rows[0]["tipo"].ToString();

                //capa
                string ficheiro = @"~\Images\" + nlivro + ".jpg";
                imgCapa.ImageUrl = ficheiro;
                imgCapa.Width = 100;
            }
            catch (Exception erro)
            {
                lbErro.Text = "Erro: " + erro.Message;
                lbErro.CssClass = "bg-danger";
                //redirecionar
                ScriptManager.RegisterStartupScript(this,
                    typeof(Page), "Redirecionar", "returnMain();", true);
            }
        }

        protected void btAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int nlivro = int.Parse(Request["id"].ToString());
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
                if (fuCapa.HasFile == true)
                {
                    if (fuCapa.PostedFile.ContentLength == 0 ||
                        fuCapa.PostedFile.ContentLength > 5000000)
                        throw new Exception("O ficheiro deve ter entre 1 e 5MB.");
                    if (fuCapa.PostedFile.ContentType != "image/jpeg" &&
                        fuCapa.PostedFile.ContentType != "image/png" &&
                        fuCapa.PostedFile.ContentType != "image/jpg")
                        throw new Exception("Só pode enviar jpeg e png.");
                    //copiar
                    string ficheiro = Server.MapPath(@"~\Images\");
                    ficheiro += nlivro + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }
                //atualizar o livro na bd
                Livro.atualizaLivro(nlivro, nome, ano, data, preco, autor, tipo);
                //mostrar mensagem
                lbErro.Text = "Livro atualizado com sucesso";
                //redirecionar
                ScriptManager.RegisterStartupScript(this,
                    typeof(Page), "Voltar", "returnMain();", true);

            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "bg-danger";
            }
        }
    }
}