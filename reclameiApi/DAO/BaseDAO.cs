using reclameiApi.DB;
using System.Data;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using reclameiApi.Models;

namespace reclameiApi.DAO
{
    public abstract class BaseDao<T> where T : IModel
    {
 

        static BaseDao()
        {

        }

        public abstract string NomeTabela { get; }
        public abstract Mapa[] Mapas { get; }

        public async Task InserirAsync(T obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Id))
                obj.Id = Guid.NewGuid().ToString();

            string sql = $"INSERT INTO {NomeTabela}" +
                         $" ({GetInsertNomes()})" +
                         $" VALUES ({GetInsertValores()})";

            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(sql, obj);
            }
        }

        private string GetInsertNomes()
        {
            var sb = new StringBuilder();
            sb.Append("id"); // Adiciona explicitamente a coluna 'id'

            foreach (var mapa in Mapas)
            {
                if (mapa.Campo != "id")
                {
                    sb.Append($", {mapa.Campo}");
                }
            }

            return sb.ToString();
        }

        private string GetInsertValores()
        {
            var sb = new StringBuilder();
            sb.Append("@Id"); // Adiciona explicitamente o valor da coluna 'id'

            foreach (var mapa in Mapas)
            {
                if (mapa.Campo != "id")
                {
                    sb.Append($", @{mapa.Propriedade}");
                }
            }

            return sb.ToString();
        }

        public async Task AlterarAsync(T obj)
        {
            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();

                string sql = $"UPDATE {NomeTabela}" +
                             $" SET {GetUpdate()}" +
                             " WHERE id = @Id";

                await connection.ExecuteAsync(sql, obj);
            }
        }

        public async Task ExcluirAsync(string id)
        {
            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();

                string sql = $"DELETE FROM {NomeTabela} WHERE id = @Id";

                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IList<T>> RetornarTodosAsync()
        {
            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();

                string sql = $"SELECT * FROM {NomeTabela}";

                var objetos = await connection.QueryAsync<T>(sql);

                return objetos.ToList();
            }
        }

        public async Task<T> RetornarPorIdAsync(string id)
        {
            using (var connection = Connection.GetMysqlConnection())
            {
                connection.Open();

                string sql = $"SELECT * FROM {NomeTabela} WHERE id = @Id";

                var obj = await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });

                return obj;
            }
        }

        private string GetUpdate()
        {
            var sb = new StringBuilder();

            foreach (var mapa in Mapas)
            {
                if (mapa.Campo != "id")
                {
                    sb.Append($", {mapa.Campo}=@{mapa.Propriedade}");
                }
            }

            return sb.ToString().Substring(2); // Remove a vírgula inicial
        }
    }

    public class Mapa
    {
        public string Propriedade { get; set; } = "";
        public string Campo { get; set; } = "";
    }
}
