using System.Reflection.Metadata;
using Dapper;
using Microsoft.Data.Sqlite;
using Chayanne_P1_P4.Models;

namespace Chayanne_P1_P4.Service;

public class AutoresService (IConfiguration configuration)
{
    private readonly string _conectionString;
    private SqliteConnection createConection => new SqliteConnection(_conectionString);
    public async Task<bool> CreateAsync(Autor autor)
    {
        const string svc = @"Insert into Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo)" +
                            " Values (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo)";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, autor);
        return resultado > 0;
    }

    public async Task<bool> UpdateAsync(Autor autor)
    {
        const string svc = "UPDATE Autores SET" +
                            "Nombres = @Nombres," +
                            " Nacionalidad = @Nacionalidad," +
                            " FechaNacimiento = @FechaNacimiento," +
                            " Sueldo = @Sueldo" +
                            " WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, autor);
        return resultado > 0;
    }

    public async Task<bool> DeletAsync(Autor autor)
    {
        const string svc = @"IF EXISTS (SELECT 1 FROM Autores WHERE Id = @Id)" +
                                " DELETE FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        var resultado = await coneccion.ExecuteAsync(svc, autor.IdAutor);
        return resultado > 0;
    }

    public async Task<EnumerableQuery<Autor>> GetAsync()
    {
        const string svc = "SELECT * FROM Autores";
        using var coneccion = createConection;
        return new EnumerableQuery<Autor>(await coneccion.QueryAsync<Autor>(svc));
    }
    public async Task<Autor?> GetIdAsync(Autor autor)
    {
        const string svc = @"SELECT * FROM Autores WHERE Id = @Id";
        using var coneccion = createConection;
        return await coneccion.QueryFirstOrDefaultAsync<Autor>(svc, autor.IdAutor);
    }
}
