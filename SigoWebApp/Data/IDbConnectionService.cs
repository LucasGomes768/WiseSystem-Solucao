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
        public List<Empresa> RetornaEmpresas();
        public List<Empresa> RetornaEmpresaPorId(int id);
        public string CriaEmpresa(Empresa empresa);
        public string DeletarEmpresa(int id);
    }
}