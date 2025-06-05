using SigoWebApp.Models;
using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Data;
using SQLitePCL;


namespace SigoWebApp.Data
{ 
    public class DbConnectionService : IDbConnectionService
    {
        private readonly SqliteConnection _connection;

        public DbConnectionService()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            CriaFuncionario();
            RetornaFuncionarios();
            //CriarEmpresa();
            //PreencheFuncionario();
            //PreencheEmpresa();
            FechaConexao();
            
        }

        public void CriaFuncionario() 
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"
                    CREATE TABLE funcionario (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        cpf DOUBLE NOT NULL,
                        nome TEXT NOT NULL,
                        dataNascimento DATE,
                        dataAdmissao DATE,
                        dataDemissao DATE,
                        funcao TEXT,
                        setor TEXT
                    );

                    INSERT INTO funcionario
                    VALUES (1, 15545365028,  'Joao Silva',   '2000-01-01', '2025-04-01', '0001-01-01', 'Analista', 'TI'),
                    (2, 51526308002,  'Jose Silva',   '2000-02-01', '2025-04-01', '0001-01-01', 'QA', 'TI'),
                    (3, 88996343005, 'Jorge Silva',  '2000-03-01', '2025-04-01', '0001-01-01', 'Engenheiro Software', 'TI'),
                    (4,  83364366004, 'Jonas Silva',  '2000-04-01', '2025-04-01', '0001-01-01', 'Programador', 'TI'),
                    (5,  47037936014, 'John Silva',   '2000-05-01', '2025-04-01', '0001-01-01', 'Arquiteto', 'TI'),
                    (6,  74758881006, 'Judas Silva',  '2000-06-01', '2025-04-01', '0001-01-01', 'DBA', 'TI'),
                    (7,  43820465057, 'Joab Silva',   '2000-07-01', '2025-04-01', '0001-01-01', 'Scrum Master', 'TI'),
                    (8,  84029758010, 'Joca Silva',   '2000-08-01', '2025-04-01', '0001-01-01', 'Analista Suporte', 'TI'),
                    (9,  44248384043, 'Jobson Silva', '2000-09-01', '2025-04-01', '0001-01-01', 'Tech Leader', 'TI'),
                    (10, 04298153010, 'Julio Silva',  '2000-10-01', '2025-04-01', '0001-01-01', 'Estagiário', 'TI');
                ";
            command.ExecuteNonQuery();
        }

        public List<Funcionario> RetornaFuncionarios()
        {
            var funcionarios = new List<Funcionario>();
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"
                SELECT *
                FROM funcionario
            ";
            using var reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    funcionarios.Add(new Funcionario
                    {
                        Cpf = reader.GetDouble(1),
                        Nome = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        DataAdmissao = reader.GetDateTime(4),
                        DataDemissao = reader.GetDateTime(5),
                        Funcao = reader.GetString(6),
                        Setor = reader.GetString(7)
                    });
                }
            return funcionarios;
        }

        public List<Funcionario> RetornaFuncionarioPorId(int id)
        {
            var funcionario = new List<Funcionario>();
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"
                SELECT *
                FROM funcionario
                WHERE id = $id
            ";
            command.Parameters.AddWithValue("$id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    funcionario.Add(new Funcionario
                    {
                        Cpf = reader.GetDouble(1),
                        Nome = reader.GetString(2),
                        DataNascimento = reader.GetDateTime(3),
                        DataAdmissao = reader.GetDateTime(4),
                        DataDemissao = reader.GetDateTime(5),
                        Funcao = reader.GetString(6),
                        Setor = reader.GetString(7)
                    });
                }
            }
            return funcionario;
        }

        public void FechaConexao() 
        {
            SqliteConnection.ClearAllPools();
            _connection.Close();
            _connection.Dispose();
        }
    }
}