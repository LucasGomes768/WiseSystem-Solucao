namespace SigoWebApp.Models
{
    public class Empresa
    {
        public double Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnae { get; set; }
        public int GrauRisco { get; set; }
        public Endereco Endereco { get; set; }
        public List<Funcionario> Funcionarios { get; set; }
    }
}
