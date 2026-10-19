namespace Chayanne_P1_P4.Sql;

public class AutorSql
{
    public const string GetTable = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores";

    public const string GetRow = "SELECT IdAutor, Nombres, Nacionalidad, FechaNacimiento, Sueldo FROM Autores WHERE IdAutor = @IdAutor";

    public const string Insert = "INSERT INTO Autores (Nombres, Nacionalidad, FechaNacimiento, Sueldo) VALUES (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo); SELECT last_insert_rowid();";

    public const string Update = "UPDATE Autores SET Nombres = @Nombres, Nacionalidad = @Nacionalidad, FechaNacimiento = @FechaNacimiento, Sueldo = @Sueldo WHERE IdAutor = @IdAutor";

    public const string Delete = "DELETE FROM Autores WHERE IdAutor = @IdAutor";
}
