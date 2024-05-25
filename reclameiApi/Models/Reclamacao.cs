namespace reclameiApi.Models
{
    public class Reclamacao : IModel
    {
        public string? Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Conteudo { get; set; } = "";
        public bool Atendida { get; set; }
        public Cliente Reclamante { get; set;}
        public string IdReclamante { get; set; }
        public Empresa Reclamada { get; set; }
        public string IdReclamada { get; set; }
        public List<Resposta> Respostas { get; set; }
    }
}