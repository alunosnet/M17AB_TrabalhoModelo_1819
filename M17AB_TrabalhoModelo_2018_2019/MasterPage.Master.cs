using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //testar se aceitou cookies
            if (Request.Cookies["m17abaviso"] != null)
                div_aviso.Visible = false;
        }
        //aceitar cookie
        protected void bt1_Click(object sender, EventArgs e)
        {
            div_aviso.Visible = false;
            HttpCookie novo = new HttpCookie("m17abaviso");
            novo.Expires = DateTime.Now.AddYears(1);
            novo.Value = "nada";
            Response.Cookies.Add(novo);
        }
    }
}