namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public int AutorId { get; set; }

    public required string Vorname { get; set; } = null!;

    public required string Nachname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = new List<Buch>();

    public ICollection<BuechereiRel> Buechereien { get; set; } = new List<BuechereiRel>();
}