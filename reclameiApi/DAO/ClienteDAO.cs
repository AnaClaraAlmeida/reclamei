using reclameiApi.DB;
using reclameiApi.Models;
using System.Data;

namespace reclameiApi.DAO
{
    public class ClienteDAO : BaseDao<Cliente>
    {
        public override string NomeTabela => "cliente"; //nome da minha tabela

            public override Mapa[] Mapas => new Mapa[]{  //aqui eu declaro os artibutos da minha classe
            new() { Propriedade = "Id", Campo = "id" },
            new() { Propriedade = "Nome", Campo = "nome" },
            new() { Propriedade = "Login", Campo = "login" },
            new() { Propriedade = "Senha", Campo = "senha" }
        };


    }
}
