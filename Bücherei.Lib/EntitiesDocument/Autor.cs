namespace Bücherei.Lib.EntitiesDocument;

public class Autor
{
    public int Id { get; set; }

    public required string Vorname { get; set; } = null!;

    public required string Nachname { get; set; } = null!;

    public ICollection<Buch> Buecher { get; set; } = [];
}
