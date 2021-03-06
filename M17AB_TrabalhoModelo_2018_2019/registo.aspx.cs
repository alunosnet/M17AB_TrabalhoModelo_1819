﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public partial class registo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRegistar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                if (email == String.Empty || email.Contains("@") == false)
                    throw new Exception("O email indicado não é válido.");

                string nome = tbNome.Text;
                if (nome == String.Empty || nome.Trim().Length < 2)
                    throw new Exception("O nome indicado não é válido.");

                string morada = tbMorada.Text;
                if (morada== String.Empty || morada.Trim().Length < 2)
                    throw new Exception("A morada indicada não é válida.");

                string nif = tbNif.Text;
                int z = int.Parse(nif);
                if (nif.Length != 9)
                    throw new Exception("O nif tem de ter 9 algarismos.");

                string password = tbPassword.Text;
                if (password.Trim().Length < 5)
                    throw new Exception("A password tem de ter 5 letras");

                //validar o recaptcha
                var respostaRecaptcha = Request.Form["g-Recaptcha-Response"];
                var eValido = ReCaptcha.Validate(respostaRecaptcha);
                if (!eValido)
                    throw new Exception("Tem de provar que não é um robot...");
                //adicionar à bd
                Utilizador.registar(email, nome, morada, nif, password);

                //mensagem ao utilizador
                lbErro.Text = "Registo efetuado com sucesso.";
                lbErro.CssClass = "info";
                string redirecionaComTempo = @"<script>
                    window.setTimeout(function(){
                        window.location.href='index.aspx';
                        },3000);
                </script>";
                Response.Write(redirecionaComTempo);
            }
            catch (Exception erro)
            {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert";
            }
        }
    }
}