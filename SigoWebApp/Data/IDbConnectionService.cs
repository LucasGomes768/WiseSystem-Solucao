using SigoWebApp.Models;

namespace SigoWebApp.Data
{
    public interface IDbConnectionService
    {
        public void CriaTabelas();
        public List<Funcionario> RetornaFuncionarios();
        public List<Funcionario> RetornaFuncionarioPorId(int id);
        public string CriaFuncionario(Funcionario funcionario);
        public void FechaConexao();
    }
}