namespace Bücherei.Lib.EntitiesDocument;

public class Autor
{
    public int Id { get; set; }

    public required string Vorname { get; set; }

    public required string Nachname { get; set; }

    public ICollection<Buch> Bücher { get; set; } = [];

}
