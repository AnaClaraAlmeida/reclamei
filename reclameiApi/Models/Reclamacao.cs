namespace reclameiApi.Models
{
    public class Reclamacao : IModel
    {
        public string? Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Conteudo { get; set; } = "";
        public bool Atendida { get; set; }
        public Cliente? Cliente { get; set;}
        public string IdCliente { get; set; }
        public Empresa? Empresa { get; set; }
        public string IdEmpresa { get; set; }
       // public List<Resposta>? Respostas { get; set; }
    }
}