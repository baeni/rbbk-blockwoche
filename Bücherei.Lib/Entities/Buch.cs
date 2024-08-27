namespace Bücherei.Lib.Entities;

public class Buch
{
    public Buch() {}

    public int Id { get; set; }

    public string Titel { get; set; } = null!;
    
    public int AutorId { get; set; }

    public required Autor Autor { get; set; }
}