using SigoWebApp.Models;

namespace SigoWebApp.Data
{
    public interface IDbConnectionService
    {
        public void CriaFuncionario();
        public List<Funcionario> RetornaFuncionarios();
        public void FechaConexao();
    }
}