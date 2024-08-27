namespace Bücherei.Lib.EntitiesDocument;

public class BüchereiDoc
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Autor> Autoren { get; set; } = [];
}
