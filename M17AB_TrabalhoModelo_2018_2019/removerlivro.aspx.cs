﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class removerlivro : System.Web.UI.Page
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

                lbNLivro.Text = dados.Rows[0]["nlivro"].ToString();
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                //capa
                string ficheiro = @"~\Images\" + nlivro + ".jpg";
                imgCapa.ImageUrl = ficheiro;

            }catch(Exception erro)
            {
                lbErro.Text = "Erro: " + erro.Message;
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
                int nlivro = int.Parse(Request["id"].ToString());
                //remover livro
                Livro.removerLivro(nlivro);
                //remover a capa
                string ficheiro = Server.MapPath(@"~\Images\" + nlivro + ".jpg");
                File.Delete(ficheiro);
                //mostrar mensagem de sucesso
                lbErro.Text = "Livro removido com sucesso.";
                //voltar à areaadmin.aspx
                ScriptManager.RegisterStartupScript(this,
                    typeof(Page), "Voltar", "returnMain();", true);
            }
            catch(Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "bg-danger";
            }
        }
    }
}