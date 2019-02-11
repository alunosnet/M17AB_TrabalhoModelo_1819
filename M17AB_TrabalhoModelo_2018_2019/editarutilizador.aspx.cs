using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class editarutilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null ||
                Session["perfil"].ToString() != "0")
                Response.Redirect("index.aspx");

            try
            {
                if (IsPostBack) return;

                int id = int.Parse(Request["id"].ToString());
                DataTable dados = Utilizador.devolveDadosUtilizador(id);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Utilizador não existe");
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbEmail.Text = dados.Rows[0]["email"].ToString();
                tbMorada.Text = dados.Rows[0]["morada"].ToString();
                tbNif.Text = dados.Rows[0]["nif"].ToString();
                

            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro " + erro.Message;
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
                int id = int.Parse(Request["id"].ToString());

                string email = tbEmail.Text;
                if (email == String.Empty || email.Contains("@") == false)
                    throw new Exception("O email indicado não é válido.");

                string nome = tbNome.Text;
                if (nome == String.Empty || nome.Trim().Length < 2)
                    throw new Exception("O nome indicado não é válido.");

                string morada = tbMorada.Text;
                if (morada == String.Empty || morada.Trim().Length < 2)
                    throw new Exception("A morada indicada não é válida.");

                string nif = tbNif.Text;
                int z = int.Parse(nif);
                if (nif.Length != 9)
                    throw new Exception("O nif tem de ter 9 algarismos.");

                Utilizador.atualizarUtilizador(id, email, nome, morada, nif);

                lbErro.Text = "Utilizador atualizado com sucesso";
                //redirecionar
                ScriptManager.RegisterStartupScript(this,
                    typeof(Page), "Redirecionar", "returnMain();", true);
            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro " + erro.Message;
                lbErro.CssClass = "bg-danger";
                
            }
        }
    }
}