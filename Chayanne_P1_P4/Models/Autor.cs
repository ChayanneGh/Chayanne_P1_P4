namespace Chayanne_P1_P4.Models;

public class AutorGet
{
    // Usamos 'Id' para que coincida exactamente con el campo de la base de datos
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Nacionalidad { get; set; } = string.Empty;

    // Dapper convertirá automáticamente el TEXT de SQLite a DateTime aquí
    public DateTime FechaNacimiento { get; set; }
    public double Sueldo { get; set; }
}

public class AutorSet
{
    public string Nombres { get; set; } = string.Empty;
    public string Nacionalidad { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public double Sueldo { get; set; }
}
