namespace reclameiApi.Models
{
    public class Usuario : IModel
    {
        public string Id { get; set; } = "";
        public string Nome { get; set; } = "";
        public string Login { get; set; } = "";
        public string Senha { get; set; } = "";
    }
}