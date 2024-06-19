using Dapper;
using reclameiApi.DB;
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
            new() { Propriedade = "IdCliente", Campo = "idCliente" },
            new() { Propriedade = "IdEmpresa", Campo = "idEmpresa" },
            new() { Propriedade = "Atendida", Campo = "atendida" }
        };

        public async Task<List<Reclamacao>> GetAllByEmpresaAsync(string id)
        {
            string sql = $"SELECT * FROM {NomeTabela} WHERE idEmpresa = @IdEmpresa";

            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();
                var reclamacoes = await connection.QueryAsync<Reclamacao>(sql, new { IdEmpresa = id });

                var listaAtualizada = reclamacoes.ToList();
                foreach (Reclamacao r in listaAtualizada)
                {
                    r.Cliente = await new ClienteDAO().RetornarPorIdAsync(r.IdCliente);
                    r.Empresa = await new EmpresaDAO().RetornarPorIdAsync(r.IdEmpresa);
                }

                return listaAtualizada;
            }
        }

        public async Task<List<Reclamacao>> GetAllByClienteAsync(string id)
        {
            string sql = $"SELECT * FROM {NomeTabela} WHERE idCliente = @IdCliente";

            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();
                var reclamacoes = await connection.QueryAsync<Reclamacao>(sql, new { IdCliente = id });

                var listaAtualizada = reclamacoes.ToList();
                foreach (Reclamacao r in listaAtualizada)
                {
                    r.Cliente = await new ClienteDAO().RetornarPorIdAsync(r.IdCliente);
                    r.Empresa = await new EmpresaDAO().RetornarPorIdAsync(r.IdEmpresa);
                }

                return listaAtualizada;
            }
        }

        public async Task<List<Reclamacao>> GetAllAsync()
        {
            string sql = $"SELECT * FROM {NomeTabela}";

            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();
                var reclamacoes = await connection.QueryAsync<Reclamacao>(sql);

                var listaAtualizada = reclamacoes.ToList();
                foreach(Reclamacao r in listaAtualizada)
                {
                   r.Cliente = await new ClienteDAO().RetornarPorIdAsync(r.IdCliente);
                   r.Empresa = await new EmpresaDAO().RetornarPorIdAsync(r.IdEmpresa);       
                }
                

                return listaAtualizada;
            }
        }
    }
}
