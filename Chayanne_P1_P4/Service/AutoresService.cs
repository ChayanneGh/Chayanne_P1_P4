using Chayanne_P1_P4.Models;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Chayanne_P1_P4.Service;

public class AutoresService(IConfiguration configuration)
{
    private SqliteConnection createConection => new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));

    public async Task<bool> InitializeAsync()
    {
        const string ddl = @"CREATE TABLE IF NOT EXISTS Autores (" +
                            "IdAutor INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "Nombres TEXT NOT NULL," +
                            "Nacionalidad TEXT NOT NULL," +
                            "FechaNacimiento TEXT NOT NULL," +
                            "Sueldo REAL NOT NULL)";

        using var coneccion = createConection;
        await coneccion.OpenAsync();
        await coneccion.ExecuteAsync(ddl);
        const string existsQuery = "SELECT COUNT(1) FROM sqlite_master WHERE type='table' AND name = 'Autores';";
        var exists = await coneccion.ExecuteScalarAsync<int>(existsQuery);
        return exists > 0;
    }

    public async Task<bool> CreateAsync(Autor autor)
    {
        const string query = @"INSERT INTO Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo) " +
                            "VALUES (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo)";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(query, autor);
        return resultado > 0;
    }

    public async Task<bool> UpdateAsync(int Id, Autor autor)
    {
        // Agregado espacio en blanco antes de Nombres para evitar errores de concatenación
        const string query = "UPDATE Autores SET " +
                            "Nombres = @Nombres, " +
                            "Nacionalidad = @Nacionalidad, " +
                            "FechaNacimiento = @FechaNacimiento, " +
                            "Sueldo = @Sueldo " +
                            "WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(query,
            new
            {
                Id = Id,
                Nombres = autor.Nombres,
                Nacionalidad = autor.Nacionalidad,
                FechaNacimiento = autor.FechaNacimiento,
                Sueldo = autor.Sueldo
            });
        return resultado > 0;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        // Corregido para sintaxis SQLite estándar
        const string query = "DELETE FROM Autores WHERE IdAutor = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(query, new { Id = Id });
        return resultado > 0; // Devolverá true si eliminó 1 fila
    }

    public async Task<List<Autor>> GetAsync()
    {
        const string query = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores";
        using var coneccion = createConection;

        var resultado = await coneccion.QueryAsync<Autor>(query);
        var tabla = resultado.ToList();

        if (!tabla.Any())
        {
            throw new Exception("No se encontraron autores");
        }
        return tabla;
    }

    public async Task<Autor?> GetIdAsync(int id)
    {
        const string query = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores WHERE IdAutor = @Id";
        using var coneccion = createConection;
        return await coneccion.QueryFirstOrDefaultAsync<Autor>(query, new { Id = id });
    }
}
