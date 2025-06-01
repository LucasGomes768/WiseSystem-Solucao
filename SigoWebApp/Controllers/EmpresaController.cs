using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Models;
using System.Net;

namespace SigoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        [HttpGet]
        public List<Empresa> RetornaEmpresas()
        {
            throw new NotImplementedException();
        }

        //[HttpGet]
        //public Empresa RetornaEmpresa(int id) 
        //{
        //    throw new NotImplementedException(); 
        //}

        [HttpPost]
        HttpStatusCode SalvaEmpresa(Empresa empresa)
        {
            throw new NotImplementedException();
        }

    }
}
