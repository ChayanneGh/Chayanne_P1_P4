using Chayanne_P1_P4.Models;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Chayanne_P1_P4.Service;

public class AutoresService(IConfiguration configuration)
{
    private SqliteConnection createConection => new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));

    public async Task<bool> InitializeAsync()
    {
        const string svc = @"CREATE TABLE IF NOT EXISTS Autores (" +
                            "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "Nombres TEXT NOT NULL," +
                            "Nacionalidad TEXT NOT NULL," +
                            "FechaNacimiento TEXT NOT NULL," +
                            "Sueldo REAL NOT NULL)";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc);
        return resultado > 0;
    }

    public async Task<bool> CreateAsync(AutorSet autor)
    {
        const string svc = @"INSERT INTO Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo) " +
                            "VALUES (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo)";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, autor);
        return resultado > 0;
    }

    public async Task<bool> UpdateAsync(int Id, AutorSet autor)
    {
        // Agregado espacio en blanco antes de Nombres para evitar errores de concatenación
        const string svc = "UPDATE Autores SET " +
                            "Nombres = @Nombres, " +
                            "Nacionalidad = @Nacionalidad, " +
                            "FechaNacimiento = @FechaNacimiento, " +
                            "Sueldo = @Sueldo " +
                            "WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc,
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

    public async Task<bool> DeletAsync(int Id)
    {
        // Corregido para sintaxis SQLite estándar
        const string svc = "DELETE FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, new { Id = Id });
        return resultado > 0; // Devolverá true si eliminó 1 fila
    }

    public async Task<List<AutorGet>> GetAsync()
    {
        // Corregido: Agregado alias 'Id AS IdAutor' para que Dapper mapée correctamente tu record
        const string svc = "SELECT Id AS IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores";
        using var coneccion = createConection;

        var resultado = await coneccion.QueryAsync<AutorGet>(svc);
        List<AutorGet> tabla = resultado.ToList(); // Corregido: Conversión explícita a List

        if (tabla == null || !tabla.Any())
        {
            throw new Exception("No se encontraron autores");
        }
        return tabla;
    }

    public async Task<AutorGet?> GetIdAsync(int id)
    {
        // Corregido: Agregado alias aquí también
        const string svc = @"SELECT Id AS IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        return await coneccion.QueryFirstOrDefaultAsync<AutorGet>(svc, new { Id = id });
    }
}
