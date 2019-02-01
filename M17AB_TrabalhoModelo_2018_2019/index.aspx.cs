using System;
using System.Collections.Generic;
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
            //TODO:esconder divlogin quando tiver sessão iniciada

        }
        //TODO: pesquisar
        protected void btPesquisa_Click(object sender, EventArgs e)
        {

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

        }
    }
}