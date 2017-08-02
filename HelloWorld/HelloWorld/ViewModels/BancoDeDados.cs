using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
    public class BancoDeDados
    {
        protected string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConFinSMN;Integrated Security=True";


        private readonly SqlConnection _connection;


        private SqlCommand Command { get; set; }

        public BancoDeDados()
        {
            _connection = Connect();
        }

        private SqlConnection Connect()
        {
            var connection = new SqlConnection(ConnectionString);


            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public void ExecutarProcedure(string NomeProcedure)
        {
            Command = new SqlCommand(NomeProcedure, _connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        public void AddParametro(string nomeParametro, object valor)
        {
            Command.Parameters.Add(new SqlParameter(nomeParametro, valor ?? DBNull.Value));
        }

        public void ExecutarSemRetorno()
        {
            Command.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteReader()
        {
            return Command.ExecuteReader();
        }
    }
}