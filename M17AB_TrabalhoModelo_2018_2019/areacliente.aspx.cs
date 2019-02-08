using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class areacliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null)
                Response.Redirect("index.aspx");

            EsconderDivs();

            ConfigurarGrids();
        }
        void EsconderDivs()
        {
            divEmprestimo.Visible = false;
            divHistorico.Visible = false;
            btEmprestimos.CssClass = "btn btn-info";
            btHistorico.CssClass = "btn btn-info";
        }
        void ConfigurarGrids()
        {
            gvEmprestar.RowCommand += GvEmprestar_RowCommand;
        }

        private void GvEmprestar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //linha
            int linha = int.Parse(e.CommandArgument as string);

            //id
            int idlivro = int.Parse(gvEmprestar.Rows[linha].Cells[1].Text);

            //id utilizador
            int idutilizador = int.Parse(Session["id"].ToString());

            //verificar o comando
            if (e.CommandName == "reservar")
            {
                //registar a reserva
                Emprestimo.adicionarReserva(idlivro, idutilizador, DateTime.Now.AddDays(7));
            }
        }

        protected void btEmprestimos_Click(object sender, EventArgs e)
        {
            divEmprestimo.Visible = true;
            btEmprestimos.CssClass = "btn btn-info active";
            atualizaGrelhaEmprestimos();
        }

        private void atualizaGrelhaEmprestimos()
        {
            gvEmprestar.Columns.Clear();
            gvEmprestar.DataSource = null;
            gvEmprestar.DataBind();

            gvEmprestar.DataSource = Livro.listaLivrosDisponiveis();

            //botão para reservar livro
            ButtonField btReservar = new ButtonField();
            btReservar.HeaderText = "Reservar livro";
            btReservar.Text = "Reservar";
            btReservar.ButtonType = ButtonType.Button;
            btReservar.CommandName = "reservar";
            gvEmprestar.Columns.Add(btReservar);

            gvEmprestar.DataBind();
        }

        protected void btHistorico_Click(object sender, EventArgs e)
        {
            divHistorico.Visible = true;
            btHistorico.CssClass = "btn btn-info active";
            atualizaGrelhaHistorico();
        }

        private void atualizaGrelhaHistorico()
        {
            gvHistorico.Columns.Clear();
            gvHistorico.DataSource = null;
            gvHistorico.DataBind();

            int idUtilizador = int.Parse(Session["id"].ToString());
            gvHistorico.DataSource = Emprestimo.listaTodosEmprestimosComNomes(idUtilizador);
            gvHistorico.DataBind();
        }
    }
}