using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_2018_2019
{
    public class Livro
    {
        public static int adicionarLivro(string nome, int ano, DateTime data, decimal preco, string autor, string tipo)
        {
            string sql = "INSERT INTO Livros (nome,ano,data_aquisicao,preco,estado,autor,tipo) VALUES ";
            sql += "(@nome,@ano,@data,@preco,@estado,@autor,@tipo);SELECT CAST(SCOPE_IDENTITY() AS INT);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value= nome},
                new SqlParameter() {ParameterName="@ano",SqlDbType=SqlDbType.Int,Value= ano},
                new SqlParameter() {ParameterName="@data",SqlDbType=SqlDbType.DateTime,Value= data},
                new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco},
                new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=1},
                new SqlParameter() {ParameterName="@autor",SqlDbType=SqlDbType.VarChar,Value= autor},
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value= tipo},
            };
            return BaseDados.Instance.executaEDevolveSQL(sql, parametros);
        }

        internal static DataTable listaLivrosDoAutor(string pesquisa)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1 and autor like @nome";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@nome",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return BaseDados.Instance.devolveSQL(sql, parametros);
        }

        internal static DataTable listaLivrosDisponiveis(string pesquisa,int? ordena=null)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1 and nome like @nome";
            if(ordena!=null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by autor";
            }

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@nome",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return BaseDados.Instance.devolveSQL(sql,parametros);
        }

        public static DataTable listaLivros()
        {
            string sql = $@"SELECT nlivro,nome,ano,data_aquisicao,preco,autor,tipo,
                        case 
                            when estado=0 then 'Emprestado'
                            when estado=1 then 'Disponível'
                            when estado=2 then 'Reservado'
                        end as estado
                            FROM Livros";
            return BaseDados.Instance.devolveSQL(sql);
        }
        public static DataTable devolveDadosLivro(int nlivro)
        {
            string sql = "SELECT * FROM Livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            return BaseDados.Instance.devolveSQL(sql, parametros);
        }
        public static void removerLivro(int nlivro)
        {
            string sql = "DELETE FROM Livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static void atualizaLivro(int nlivro, string nome, int ano, DateTime data, decimal preco, string autor, string tipo)
        {
            string sql = "UPDATE Livros SET nome=@nome,ano=@ano,data_aquisicao=@data,preco=@preco,";
            sql += "autor=@autor, tipo=@tipo ";
            sql += " WHERE nlivro=@nlivro;";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value= nome},
                new SqlParameter() {ParameterName="@ano",SqlDbType=SqlDbType.Int,Value= ano},
                new SqlParameter() {ParameterName="@data",SqlDbType=SqlDbType.DateTime,Value= data},
                new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco},
                new SqlParameter() {ParameterName="@autor",SqlDbType=SqlDbType.VarChar,Value=autor},
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value=tipo},
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro}
            };
            BaseDados.Instance.executaSQL(sql, parametros);
        }
        public static DataTable listaLivrosDisponiveis(int? ordena=null)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1";
            if (ordena != null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by autor";
            }
            return BaseDados.Instance.devolveSQL(sql);
        }
    }

}