using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public class Login
    {
        public static DataTable verificaLogin(string email,string password)
        {
            string sql = $@"SELECT * FROM Utilizadores WHERE 
                     email=@email AND password=HASHBYTES('SHA2_512',@password)
                     AND estado=1";
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
                    ParameterName="@password",
                    SqlDbType=SqlDbType.VarChar,
                    Value=password
                }
            };
            DataTable utilizador = BaseDados.Instance.devolveSQL(sql, parametros);
            if (utilizador == null || utilizador.Rows.Count == 0)
                return null;

            return utilizador;
        }
    }
}