namespace Chayanne_P1_P4.Models;

public record AutorGet(int Id, string Nombres, string Nacionalidad, DateTime FechaNacimiento, double Sueldo);

public record AutorSet(string Nombres, string Nacionalidad, DateTime FechaNacimiento, double Sueldo);
