namespace Bücherei.Lib.EntitiesRelational;

public class Buch
{
    public int BuchId { get; set; }

    public required string Titel { get; set; } = null!;
    
    public int AutorId { get; set; }

    public Autor Autor { get; set; }
}