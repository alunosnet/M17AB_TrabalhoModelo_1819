using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class removerutilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: verificar se é admin

            try
            {
                int id = int.Parse(Request["id"].ToString());
                DataTable dados = Utilizador.devolveDadosUtilizador(id);
                if (dados == null || dados.Rows.Count==0)
                    throw new Exception("Utilizador não existe");
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbNUtilizador.Text = dados.Rows[0]["id"].ToString();

            }
            catch(Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro " + erro.Message;
                lbErro.CssClass = "bg-danger";
                //redirecionar
                ScriptManager.RegisterStartupScript(this,
                    typeof(Page), "Redirecionar", "returnMain();", true);
            }
        }

        protected void btRemover_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request["id"].ToString());
                Utilizador.removerUtilizador(id);
                lbErro.Text = "Utilizador removido com sucesso.";
            }
            catch(Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro " + erro.Message;
                lbErro.CssClass = "bg-danger";
            }
            //redirecionar
            ScriptManager.RegisterStartupScript(this,
                typeof(Page), "Redirecionar", "returnMain();", true);
        }
    }
}