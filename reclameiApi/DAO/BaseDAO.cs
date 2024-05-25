using Dapper;
using System.Data;
using System.Text;
using reclameiApi.Models;
using reclameiApi.DB;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace reclameiApi.DAO
{
    public abstract class BaseDao<T> where T : IModel
    {
        private static readonly IDbConnection _connection;

        static BaseDao()
        {
            _connection = Connection.GetMysqlConnection();
            _connection.Open();
        }

        public abstract string NomeTabela { get; }
        public abstract Mapa[] Mapas { get; }

        public async Task InserirAsync(T obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Id))
                obj.Id = Guid.NewGuid().ToString();

            string sql = $"INSERT INTO {NomeTabela}" +
                         $" (id{GetInsertNomes()})" +
                         $" VALUES (@Id{GetInsertValores()})";

            await _connection.ExecuteAsync(sql, obj);
        }

        public async Task AlterarAsync(T obj)
        {
            string sql = $"UPDATE {NomeTabela}" +
                         $" SET {GetUpdate()}" +
                         " WHERE id = @Id";

            await _connection.ExecuteAsync(sql, obj);
        }

        public async Task ExcluirAsync(string id)
        {
            string sql = $"DELETE FROM {NomeTabela} WHERE id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IList<T>> RetornarTodosAsync()
        {
            string sql = $"SELECT * FROM {NomeTabela}";

            var objetos = await _connection.QueryAsync<T>(sql);

            return objetos.ToList();
        }

        public async Task<T> RetornarPorIdAsync(string id)
        {
            string sql = $"SELECT * FROM {NomeTabela} WHERE id = @Id";

            var obj = await _connection.QuerySingleAsync<T>(sql, new { Id = id });

            return obj;
        }

        private string GetInsertValores()
        {
            var sb = new StringBuilder();

            foreach (var mapa in Mapas)
                sb.Append($", @{mapa.Propriedade}");

            return sb.ToString();
        }

        private string GetInsertNomes()
        {
            var sb = new StringBuilder();

            foreach (var mapa in Mapas)
                sb.Append($", {mapa.Campo}");

            return sb.ToString();
        }

        private string GetUpdate()
        {
            var sb = new StringBuilder();

            foreach (var mapa in Mapas)
                sb.Append($", {mapa.Campo}=@{mapa.Propriedade}");

            return sb.ToString().Substring(1); 
        }
    }

    public class Mapa
    {
        public string Propriedade { get; set; } = "";
        public string Campo { get; set; } = "";
    }
}

