using MySql.Data.MySqlClient;
using System.Data;

namespace reclameiApi.DB;

public static class Connection
{
    private static string _connectionString = "Server=localhost;Port=3306;Database=reclamei;User=root;Password=root;";

    public static IDbConnection GetMysqlConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}