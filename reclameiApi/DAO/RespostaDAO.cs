using Dapper;
using reclameiApi.DB;
using reclameiApi.Models;

namespace reclameiApi.DAO
{
    public class RespostaDAO : BaseDao<Resposta>
    {
        public override string NomeTabela => "reclamacao"; //nome da minha tabela

        public override Mapa[] Mapas => new Mapa[]{  //aqui eu declaro os artibutos da minha classe
            new() { Propriedade = "Id", Campo = "id" },
            new() { Propriedade = "Conteudo", Campo = "conteudo" },
            new() { Propriedade = "IdReclamacao", Campo = "idReclamacao" },
            new() { Propriedade = "IdEmpresa", Campo = "idEmpresa" }
        };

        public async Task<List<Resposta>> GetRespostas(string idReclamacao)
        {
            string sql = $"SELECT * FROM {NomeTabela} WHERE idReclamacao = @IdReclamacao";

            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();
                var respostas = await connection.QueryAsync<Resposta>(sql, new { IdReclamacao = idReclamacao });

                return respostas.ToList();
            }
        }
    }
}
