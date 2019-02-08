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
        public static void registar(string email, string nome, string morada, string nif, string password,int perfil)
        {
            string sql = $@"INSERT INTO utilizadores(email,nome,morada,nif,password,estado,perfil)
            VALUES(@email, @nome, @morada,@nif, HASHBYTES('SHA2_512', @password), 1, @perfil)";
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
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=SqlDbType.Int,
                    Value=perfil
                }
            };

            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static DataTable listaTodosUtilizadores()
        {
            string sql = "SELECT id, email,nome, morada, nif,  estado, perfil FROM utilizadores";
            return BaseDados.Instance.devolveSQL(sql);
        }
        public static DataTable listaTodosUtilizadoresDisponiveis()
        {
            string sql = $@"SELECT id, email,nome, morada, nif,  estado, perfil 
            FROM utilizadores where estado=1";
            return BaseDados.Instance.devolveSQL(sql);
        }
        public static void atualizarUtilizador(int id,string email,string nome,string morada,string nif)
        {
            string sql = @"UPDATE utilizadores SET email=@email,nome=@nome,morada=@morada,nif=@nif 
                            WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome },
                new SqlParameter() {ParameterName="@morada",SqlDbType=SqlDbType.VarChar,Value=morada },
                new SqlParameter() {ParameterName="@nif",SqlDbType=SqlDbType.VarChar,Value=nif },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static DataTable devolveDadosUtilizador(int id)
        {
            string sql = "SELECT * FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = BaseDados.Instance.devolveSQL(sql, parametros);
            if (dados.Rows.Count == 0)
            {
                return null;
            }
            return dados;
        }
        public static int estadoUtilizador(int id)
        {
            string sql = "SELECT estado FROM utilizadores WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            DataTable dados = BaseDados.Instance.devolveSQL(sql, parametros);
            return int.Parse(dados.Rows[0][0].ToString());
        }
        public static void ativarDesativarUtilizador(int id)
        {
            int estado = estadoUtilizador(id);
            if (estado == 0) estado = 1;
            else estado = 0;
            string sql = "UPDATE utilizadores SET estado = @estado WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=estado },
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id }
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static void removerUtilizador(int id)
        {
            string sql = "DELETE FROM Utilizadores WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value= id},
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static void recuperarPassword(string email, string guid)
        {
            string sql = "UPDATE utilizadores set lnkRecuperar=@lnk WHERE email=@email";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static void atualizarPassword(string guid, string password)
        {
            string sql = "UPDATE utilizadores set password=HASHBYTES('SHA2_512',@password),estado=1,lnkRecuperar=null WHERE lnkRecuperar=@lnk";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=password},
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static DataTable devolveDadosUtilizador(string email)
        {
            string sql = "SELECT * FROM utilizadores WHERE email=@email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email }
            };
            DataTable dados = BaseDados.Instance.devolveSQL(sql, parametros);
            return dados;
        }
    }
}