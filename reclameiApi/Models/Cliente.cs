namespace reclameiApi.Models
{
    public class Cliente : Usuario, IModel
    {
        private List<Reclamacao> Reclamacoes { get; set; }
    }
}
