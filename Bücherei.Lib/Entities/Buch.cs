namespace Bücherei.Lib.Entities;

public class Buch
{
    public int Id { get; set; }

    public string Titel { get; set; } = null!;
    
    public Autor Autor { get; set; } = null!;

    public Bücherei Bücherei { get; set; } = null!;
}