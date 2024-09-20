namespace Bücherei.Lib.EntitiesRelational;

public class Autor
{
    public int AutorId { get; set; }

    public required string Vorname { get; set; } = null!;

    public required string Nachname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];

    public ICollection<BuechereiRel> Buechereien { get; set; } = [];
}