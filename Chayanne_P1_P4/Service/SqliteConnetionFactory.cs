using Dapper;
using Microsoft.Data.Sqlite;

namespace Chayanne_P1_P4.Service
{
    public class SqliteConnetionFactory(IConfiguration configuration)
    {
        private SqliteConnection createConnection => new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));

        public async Task<bool> InitializeAsync()
        {
            const string ddl = @"CREATE TABLE IF NOT EXISTS Autores (" +
                                "IdAutor INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "Nombres TEXT NOT NULL," +
                                "Nacionalidad TEXT NOT NULL," +
                                "FechaNacimiento TEXT NOT NULL," +
                                "Sueldo REAL NOT NULL)";

            using var coneccion = createConnection;
            await coneccion.OpenAsync();
            await coneccion.ExecuteAsync(ddl);
            const string existsQuery = "SELECT COUNT(1) FROM sqlite_master WHERE type='table' AND name = 'Autores';";
            var exists = await coneccion.ExecuteScalarAsync<int>(existsQuery);
            return exists > 0;
        }
    }
}
