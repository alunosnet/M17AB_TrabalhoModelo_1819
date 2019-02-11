using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class detalheslivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request["id"].ToString());
                DataTable dados = Livro.devolveDadosLivro(id);
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbAno.Text = dados.Rows[0]["ano"].ToString();
                lbAutor.Text = dados.Rows[0]["autor"].ToString();
                string ficheiro = @"~\Images\" + dados.Rows[0]["nlivro"].ToString() + ".jpg";
                imgCapa.ImageUrl = ficheiro;
                imgCapa.Width = 200;
                //criar cookie
                HttpCookie cookie = new HttpCookie("ultimolivro", Server.UrlEncode(lbAutor.Text));
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
            }
            catch
            {
                Response.Redirect("index.aspx");
            }
        }
    }
}