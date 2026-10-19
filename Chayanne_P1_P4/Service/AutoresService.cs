using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Sql;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Chayanne_P1_P4.Service;

public class AutoresService(IConfiguration configuration)
{
    private SqliteConnection createConnection => new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));

    public async Task<Autor?> CreateAsync(Autor autorDto)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteScalarAsync<int>(AutorSql.Insert, autorDto);

        if (resultado < 1)
        {
            return null;
        }
        ;

        return autorDto with { IdAutor = resultado };
    }

    public async Task<IEnumerable<Autor>> GetAsync()
    {
        using var connection = createConnection;
        var resultado = await connection.QueryAsync<Autor>(AutorSql.GetTable);

        return resultado;
    }

    public async Task<Autor?> GetIdAsync(long IdAutor)
    {
        using var connection = createConnection;
        Autor autor = await connection.QueryFirstOrDefaultAsync<Autor>(AutorSql.GetRow, new { IdAutor });

        if (autor == null)
        {
            return null;
        }
        ;

        return autor;
    }

    public async Task<bool> UpdateAsync(Autor autorDto)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteAsync(AutorSql.Update, autorDto);

        if (resultado < 1)
        {
            return false;
        }

        return true;
    }
    public async Task<bool> DeleteAsync(long IdAutor)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteAsync(AutorSql.Delete, new { IdAutor });

        if (resultado < 1)
        {
            return false;
        }
        ;

        return true;
    }
}