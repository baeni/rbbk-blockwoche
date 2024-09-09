namespace Bücherei.Lib.EntitiesRelational;

public class Buch
{
    public int Id { get; set; }

    public required string Titel { get; set; } = null!;
    
    public int AutorId { get; set; }

    public required Autor Autor { get; set; }
}