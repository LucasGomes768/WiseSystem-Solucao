using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Data;
using SigoWebApp.Models;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SigoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IDbConnectionService _context;

        public EmpresaController(IDbConnectionService context)
        {
            _context = context;
            _context.CriaTabelas();
        }

        [HttpGet]
        public List<Empresa> RetornaEmpresas()
        {
            List<Empresa> empresas = new List<Empresa>();
            empresas = _context.RetornaEmpresas();

            return empresas;
        }

        [HttpGet("{id}")]
        public List<Empresa> RetornaEmpresa(int id) 
        {
            List<Empresa> empresa = new List<Empresa>();
            empresa = _context.RetornaEmpresaPorId(id);

            return empresa;
        }

        [HttpPost]
        public string SalvaEmpresa(Empresa empresa)
        {
            var result = _context.CriaEmpresa(empresa);
            return result;
        }

        [HttpDelete]
        public string RemoverEmpresa(int id)
        {
            var result = _context.DeletarEmpresa(id);
            return result;
        }

    }
}
