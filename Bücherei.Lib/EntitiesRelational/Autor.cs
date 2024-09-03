namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public Autor() {}
    
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];

    public ICollection<BuechereiRel> Buechereien { get; set; } = [];
}