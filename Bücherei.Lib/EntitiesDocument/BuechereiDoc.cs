namespace Bücherei.Lib.EntitiesDocument;

public class BuechereiDoc
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Autor> Autoren { get; set; } = new List<Autor>();
}
