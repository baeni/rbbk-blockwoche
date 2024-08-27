namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public Autor() {}
    
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public ICollection<Buch> Bücher { get; set; } = [];

    public ICollection<Bücherei> Büchereien { get; set; } = [];
}