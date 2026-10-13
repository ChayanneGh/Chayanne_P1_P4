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

    public async Task<Autor> CreateAsync(Autor autor)
    {
        const string query = @"INSERT INTO Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo) 
                               VALUES (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo);";
        using var coneccion = createConection;
        await coneccion.OpenAsync();

        var resultado = await coneccion.ExecuteAsync(query, new
        {
            autor.Nombres,
            autor.Nacionalidad,
            autor.FechaNacimiento,
            autor.Sueldo
        });

        if (resultado <= 0)
            throw new Exception("No se pudo insertar el autor.");

        // Obtener el id insertado por la base de datos
        var id = await coneccion.ExecuteScalarAsync<long>("SELECT last_insert_rowid();");
        return new Autor((int)id, autor.Nombres, autor.Nacionalidad, autor.FechaNacimiento, autor.Sueldo);
    }

    public async Task<bool> UpdateAsync(int Id, Autor autor)
    {
        const string query = "UPDATE Autores SET " +
                            "Nombres = @Nombres, " +
                            "Nacionalidad = @Nacionalidad, " +
                            "FechaNacimiento = @FechaNacimiento, " +
                            "Sueldo = @Sueldo " +
                            "WHERE IdAutor = @Id";
        using var coneccion = createConection;
        await coneccion.OpenAsync();
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
        const string query = "DELETE FROM Autores WHERE IdAutor = @Id";
        using var coneccion = createConection;
        await coneccion.OpenAsync();
        var resultado = await coneccion.ExecuteAsync(query, new { Id = Id });
        return resultado > 0;
    }

    public async Task<List<Autor>> GetAsync()
    {
        const string query = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores";
        using var coneccion = createConection;
        await coneccion.OpenAsync();

        var resultado = await coneccion.QueryAsync<Autor>(query);
        return resultado.ToList();
    }

    public async Task<Autor?> GetIdAsync(int id)
    {
        const string query = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores WHERE IdAutor = @Id";
        using var coneccion = createConection;
        await coneccion.OpenAsync();
        return await coneccion.QueryFirstOrDefaultAsync<Autor>(query, new { Id = id });
    }
}