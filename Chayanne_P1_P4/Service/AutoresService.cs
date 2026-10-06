using Chayanne_P1_P4.Models;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Chayanne_P1_P4.Service;

public class AutoresService(IConfiguration configuration)
{
    //private readonly string _conectionString;
    private SqliteConnection createConection => new SqliteConnection("SqliteConnection");

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
        const string svc = @"Insert into Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo)" +
                            " Values (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo)";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, autor);
        return resultado > 0;
    }

    public async Task<bool> UpdateAsync(int Id, AutorSet autor)
    {
        const string svc = "UPDATE Autores SET" +
                            "Nombres = @Nombres," +
                            " Nacionalidad = @Nacionalidad," +
                            " FechaNacimiento = @FechaNacimiento," +
                            " Sueldo = @Sueldo" +
                            " WHERE Id = @Id";
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
        const string svc = @"IF EXISTS (SELECT 1 FROM Autores WHERE Id = @Id)" +
                                " DELETE FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, new { Id = Id });
        return resultado > 0;
    }

    public async Task<EnumerableQuery<AutorGet>> GetAsync()
    {
        const string svc = "SELECT * FROM Autores";
        using var coneccion = createConection;
        return new EnumerableQuery<AutorGet>(await coneccion.QueryAsync<AutorGet>(svc));
    }
    public async Task<AutorGet?> GetIdAsync(int id)
    {
        const string svc = @"SELECT * FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        return await coneccion.QueryFirstOrDefaultAsync<AutorGet>(svc, new { Id = id });
    }
}
