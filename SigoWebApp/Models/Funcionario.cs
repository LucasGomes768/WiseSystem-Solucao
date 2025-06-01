namespace SigoWebApp.Models
{
    public class Funcionario
    {
        public double Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime DataDemissao { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }
        public Endereco Endereco { get; set; }

        //public List<ExameOperacional> ExamesOperacionais { get; set; }
    }
}
