using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class bloquearutilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: verificar se é admin
            try
            {
                int id = int.Parse(Request["id"].ToString());
                Utilizador.ativarDesativarUtilizador(id);
            }
            catch
            {

            }
            Response.Redirect("areaadmin.aspx");
        }
    }
}