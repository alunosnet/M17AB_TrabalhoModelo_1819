using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class historico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validar a sessão do utilizador
            if (Session["perfil"] == null ||
                Session["perfil"].ToString() != "0")
                Response.Redirect("index.aspx");
            try
            {
                int id = int.Parse(Request["id"].ToString());
                gvHistorico.DataSource = Emprestimo.listaTodosEmprestimosComNomes(id);
                gvHistorico.DataBind();
            }
            catch
            {
                Response.Redirect("areaadmin.aspx");
            }
        }
    }
}