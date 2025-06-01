using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Models;
using System.Net;

namespace SigoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet]
        public List<Funcionario> RetornaFuncionarios()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public List<Funcionario> RetornaFuncionario(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        HttpStatusCode SalvaFuncionario(Empresa empresa)
        {
            throw new NotImplementedException();
        }
    }
}
