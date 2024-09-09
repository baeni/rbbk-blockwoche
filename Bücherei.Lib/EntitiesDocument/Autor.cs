namespace Bücherei.Lib.EntitiesDocument;

public class Autor
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Buch> Buecher { get; set; } = [];
}
