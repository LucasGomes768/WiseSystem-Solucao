using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Data;
using SigoWebApp.Models;
using System.Net;

namespace SigoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IDbConnectionService _context;

        public FuncionarioController(IDbConnectionService context)
        {
            _context = context;
            _context.CriaTabelas();
        }

        [HttpGet]
        public List<Funcionario> RetornaFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            funcionarios = _context.RetornaFuncionarios();

            return funcionarios;
        }

        [HttpGet("{id}")]
        public List<Funcionario> RetornaFuncionarioPorId(int id)
        {
            List<Funcionario> funcionario = new List<Funcionario>();
            funcionario = _context.RetornaFuncionarioPorId(id);

            return funcionario;
        }

        [HttpPost]
        public string CriaFuncionario([FromBody]Funcionario funcionario)
        {
            var result = _context.CriaFuncionario(funcionario);
            return result;
        }
    }
}
