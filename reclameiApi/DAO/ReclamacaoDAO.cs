using reclameiApi.Models;

namespace reclameiApi.DAO
{
    public class ReclamacaoDAO : BaseDao<Reclamacao>
    {
        public override string NomeTabela => "reclamacao"; //nome da minha tabela

        public override Mapa[] Mapas => new Mapa[]{  //aqui eu declaro os artibutos da minha classe
            new() { Propriedade = "Id", Campo = "id" },
            new() { Propriedade = "Titulo", Campo = "titulo" },
            new() { Propriedade = "Conteudo", Campo = "conteudo" },
            new() { Propriedade = "IdCliente", Campo = "id_cliente" },
            new() { Propriedade = "IdEmpresa", Campo = "id_empresa" },
            new() { Propriedade = "Atendida", Campo = "atendida" }
        };
    }
}
