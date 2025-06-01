using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Data;
using SigoWebApp.Models;
using System.Data.Common;
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
        }

        [HttpGet]
        public List<Funcionario> RetornaFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            //_context.FechaConexao();
            _context.CriaFuncionario();
            funcionarios = _context.RetornaFuncionarios();
            
            return funcionarios;
            //_context.FechaConexao();

        }

        //[HttpGet]
        //public List<Funcionario> RetornaFuncionario(int id)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost]
        HttpStatusCode SalvaFuncionario(Empresa empresa)
        {
            throw new NotImplementedException();
        }
    }
}
