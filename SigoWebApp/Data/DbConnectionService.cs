using Microsoft.Data.Sqlite;
using SigoWebApp.Models;
using System.Xml.Linq;


namespace SigoWebApp.Data
{ 
    public class DbConnectionService : IDbConnectionService
    {
        private readonly SqliteConnection _connection;

        public DbConnectionService()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            
            //var command = _connection.CreateCommand();
            //command.CommandText = "PRAGMA foreign_keys = ON;";
            //command.ExecuteNonQuery();

        }

        public void CriaTabelas() 
        {
            _connection.Open();

            // Verifica se a tabela existe
            var checkCmd = _connection.CreateCommand();
            checkCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='funcionario'";

            var result = checkCmd.ExecuteScalar();

            if (result != null)
            {

            }
            else
            {
                var createEndereco = _connection.CreateCommand();
                createEndereco.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS endereco(
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    logradouro TEXT,
                    bairro TEXT,
                    cidade TEXT,
                    uf TEXT,
                    cep DOUBLE
                );

                    INSERT INTO endereco
                    VALUES (1, 'Rua Cristal', 'Brilhante', 'Mina', 'DF', '00222050')
                    VALUES (2, 'Rua Ametista', 'Brilhante', 'Mina', 'DF', '00222060')
                    VALUES (3, 'Rua Esmeralda', 'Brilhante', 'Mina', 'DF', '00222070')
                    VALUES (4, 'Rua Safira', 'Brilhante', 'Mina', 'DF', '00222080')
                    VALUES (5, 'Rua Quartzo', 'Brilhante', 'Mina', 'DF', '00222090')
                ";
                createEndereco = _connection.CreateCommand();

                var createFuncionario = _connection.CreateCommand();
                createFuncionario.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS funcionario (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        cpf DOUBLE NOT NULL,
                        nome TEXT NOT NULL,
                        dataNascimento DATE,
                        dataAdmissao DATE,
                        dataDemissao DATE,
                        funcao TEXT,
                        setor TEXT,
                        idEndereco INTEGER
                    );

                    INSERT INTO funcionario
                    VALUES (1, 15545365028,  'Joao Silva',   '2000-01-01', '2025-04-01', '0001-01-01', 'Analista', 'TI',1),
                    (2, 51526308002,  'Jose Silva',   '2000-02-01', '2025-04-01', '0001-01-01', 'QA', 'TI',2),
                    (3, 88996343005, 'Jorge Silva',  '2000-03-01', '2025-04-01', '0001-01-01', 'Engenheiro Software', 'TI',3),
                    (4,  83364366004, 'Jonas Silva',  '2000-04-01', '2025-04-01', '0001-01-01', 'Programador', 'TI',4),
                    (5,  47037936014, 'John Silva',   '2000-05-01', '2025-04-01', '0001-01-01', 'Arquiteto', 'TI',5),
                    (6,  74758881006, 'Judas Silva',  '2000-06-01', '2025-04-01', '0001-01-01', 'DBA', 'TI',5),
                    (7,  43820465057, 'Joab Silva',   '2000-07-01', '2025-04-01', '0001-01-01', 'Scrum Master', 'TI',4),
                    (8,  84029758010, 'Joca Silva',   '2000-08-01', '2025-04-01', '0001-01-01', 'Analista Suporte', 'TI',3),
                    (9,  44248384043, 'Jobson Silva', '2000-09-01', '2025-04-01', '0001-01-01', 'Tech Leader', 'TI',2),
                    (10, 04298153010, 'Julio Silva',  '2000-10-01', '2025-04-01', '0001-01-01', 'Estagiário', 'TI',1);
                ";
                createFuncionario.ExecuteNonQuery();

            }
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

        public string CriaFuncionario(Funcionario funcionario)
        {
            using var insertFuncionario = _connection.CreateCommand();
            insertFuncionario.CommandText =
                @"
                    INSERT INTO funcionario
                    VALUES (11,$cpf, $nome, $dataNascimento, $dataAdmissao, $dataDemissao,  $funcao, $setor, $idEndereco)
                ";

            insertFuncionario.Parameters.AddWithValue("$cpf", funcionario.Cpf);
            insertFuncionario.Parameters.AddWithValue("$nome", funcionario.Nome);
            insertFuncionario.Parameters.AddWithValue("$dataNascimento", funcionario.DataNascimento);
            insertFuncionario.Parameters.AddWithValue("$dataAdmissao", funcionario.DataAdmissao);
            insertFuncionario.Parameters.AddWithValue("$dataDemissao", funcionario.DataDemissao);
            insertFuncionario.Parameters.AddWithValue("$funcao", funcionario.Funcao);
            insertFuncionario.Parameters.AddWithValue("$setor", funcionario.Setor);
            insertFuncionario.Parameters.AddWithValue("$idEndereco", 1);

            try
            {
                insertFuncionario.ExecuteNonQuery();

                insertFuncionario.CommandText =
                @"
                    SELECT last_insert_rowid()
                ";
                //var id = (int)insertFuncionario.ExecuteScalar();
                var id = 11;

                return "funcionario criado com sucesso id: " + id.ToString();
            }
            catch (SqliteException ex)
            {
                return "Erro ao inserir produto.";
            }
            
        }

        public void FechaConexao() 
        {
            SqliteConnection.ClearAllPools();
            _connection.Close();
            _connection.Dispose();
        }
    }
}