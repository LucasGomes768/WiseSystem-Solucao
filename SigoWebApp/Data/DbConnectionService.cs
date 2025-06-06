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
                    VALUES (1, 'Rua Cristal', 'Brilhante', 'Mina', 'DF', '00222050'),
                    (2, 'Rua Ametista', 'Brilhante', 'Mina', 'DF', '00222060'),
                    (3, 'Rua Esmeralda', 'Brilhante', 'Mina', 'DF', '00222070'),
                    (4, 'Rua Safira', 'Brilhante', 'Mina', 'DF', '00222080'),
                    (5, 'Rua Quartzo', 'Brilhante', 'Mina', 'DF', '00222090');
                ";
                // Código original alterado
                // Tabela de endereços não era criada
                // Comando antigo: createEndereco = _connection.CreateCommand();
                createEndereco.ExecuteNonQuery();

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

            // Verificar se existe tabela das empresas existe
            var comCheck = _connection.CreateCommand();
            comCheck.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='empresa'";

            var comResult = comCheck.ExecuteScalar();

            if (comResult == null)
            {
                var createEmpresas = _connection.CreateCommand();
                createEmpresas.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS empresa (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        cnpj DOUBLE NOT NULL,
                        razaoSocial TEXT NOT NULL,
                        nomeFantasia TEXT NOT NULL,
                        cnae TEXT NOT NULL,
                        grauRisco INTEGER NOT NULL,
                        idEndereco INTEGER
                    );

                    INSERT INTO empresa
                    VALUES (1, 15179942000156,  'Xavier Assis Academia LTDA',   'Academia Xavier', '9313-1/00', 2, 3),
                    (2, 31861960000170,  'Campos Barroso Atacado EPP',   'Atacado Campos', '4692-3/00', 1, 4),
                    (3, 28446294000107, 'Malaquias Boelho Pinturas LTDA',  'Pinturas Malaquias', '4330-4/04', 1, 5),
                    (4, 52193691000190, 'Pires Grilo Consultoria EPP',  'Consultoria Pires', '7020-4/00', 1, 1);
                ";
                createEmpresas.ExecuteNonQuery();
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

        public List<Empresa> RetornaEmpresas()
        {
            var empresas = new List<Empresa>();
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"
                SELECT *
                FROM empresa
            ";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                empresas.Add(new Empresa
                {
                    Cnpj = reader.GetDouble(1),
                    RazaoSocial = reader.GetString(2),
                    NomeFantasia = reader.GetString(3),
                    Cnae = reader.GetString(4),
                    GrauRisco = reader.GetInt32(5)
                });
            }
            return empresas;
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

        public List<Empresa> RetornaEmpresaPorId(int id)
        {
            var empresa = new List<Empresa>();
            _connection.Open();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"
                SELECT *
                FROM empresa
                WHERE id = $id
            ";
            command.Parameters.AddWithValue("$id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    empresa.Add(new Empresa
                    {
                        Cnpj = reader.GetDouble(1),
                        RazaoSocial = reader.GetString(2),
                        NomeFantasia = reader.GetString(3),
                        Cnae = reader.GetString(4),
                        GrauRisco = reader.GetInt32(5)
                    });
                }
            }
            return empresa;
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

        public string CriaEmpresa(Empresa empresa)
        {
            using var insertEmpresa = _connection.CreateCommand();
            insertEmpresa.CommandText =
                @"
                    INSERT INTO empresa (cnpj, razaoSocial, nomeFantasia, cnae, grauRisco, idEndereco)
                    VALUES ($cnpj, $razaoSocial, $nomeFantasia, $cnae, $grauRisco, $idEndereco)
                ";
            
            insertEmpresa.Parameters.AddWithValue("$cnpj", empresa.Cnpj);
            insertEmpresa.Parameters.AddWithValue("$razaoSocial", empresa.RazaoSocial);
            insertEmpresa.Parameters.AddWithValue("$nomeFantasia", empresa.NomeFantasia);
            insertEmpresa.Parameters.AddWithValue("$cnae", empresa.Cnae);
            insertEmpresa.Parameters.AddWithValue("$grauRisco", empresa.GrauRisco);
            insertEmpresa.Parameters.AddWithValue("$idEndereco", 1);

            try
            {
                insertEmpresa.ExecuteNonQuery();

                insertEmpresa.CommandText =
                @"
                    SELECT last_insert_rowid()
                ";
                var id = insertEmpresa.ExecuteScalar();
                //var id = 11;

                return "Empresa cadastrada com sucesso. Id: " + id.ToString();
            }
            catch (SqliteException ex)
            {
                return "Erro ao inserir empresa.";
            }

        }

        public string DeletarEmpresa(int id)
        {
            var empresa = RetornaEmpresaPorId(id);

            if (empresa.Count == 0)
            {
                return $"Empresa com id {id} não existe";
            }

            using var deleteEmpresa = _connection.CreateCommand();
            deleteEmpresa.CommandText = @" DELETE FROM empresa WHERE id = $id";

            deleteEmpresa.Parameters.AddWithValue("$id", id);

            try
            {
                deleteEmpresa.ExecuteNonQuery();

                return "Empresa removida com sucesso.";
            }
            catch (SqliteException ex)
            {
                return "Erro ao remover empresa";
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