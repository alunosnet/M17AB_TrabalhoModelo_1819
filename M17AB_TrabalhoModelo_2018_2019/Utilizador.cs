using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public class Utilizador
    {
        public static void registar(string email,string nome,string morada,string nif,string password)
        {
            string sql= $@"INSERT INTO utilizadores(email,nome,morada,nif,password,estado,perfil)
            VALUES(@email, @nome, @morada,@nif, HASHBYTES('SHA2_512', @password), 1, 1)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=SqlDbType.VarChar,
                    Value=email
                },
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=SqlDbType.VarChar,
                    Value=nome
                },
                new SqlParameter()
                {
                    ParameterName="@morada",
                    SqlDbType=SqlDbType.VarChar,
                    Value=morada
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=SqlDbType.VarChar,
                    Value=nif
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=SqlDbType.VarChar,
                    Value=password
                }
            };

            BaseDados.Instance.executaSQL(sql, parametros);
        }
    }
}