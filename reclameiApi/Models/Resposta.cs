namespace reclameiApi.Models
{
    public class Resposta : IModel
     {
        public string Id { get; set; } = "";
        public string Conteudo { get; set;}

        public string IdReclamacao { get; set; }
        public string? IdEmpresa { get; set; }
    }
}