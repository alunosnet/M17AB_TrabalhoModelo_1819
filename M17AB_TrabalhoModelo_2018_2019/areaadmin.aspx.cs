using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class areaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO: validar a sessão do utilizador

            if(!IsPostBack)
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
            //TODO:
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
            //TODO:atualizar grelha livros
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e)
        {

        }

        #endregion
        #region utilizadores
        protected void btUtilizadores_Click(object sender, EventArgs e)
        {

        }
        protected void btUAdicionarUtilizador_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region emprestimos
        protected void btEmprestimos_Click(object sender, EventArgs e)
        {

        }
        protected void cbEmprestimos_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btEAdicionar_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region consultas
        protected void btConsultas_Click(object sender, EventArgs e)
        {

        }
        protected void ddConsula_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion




    }
}